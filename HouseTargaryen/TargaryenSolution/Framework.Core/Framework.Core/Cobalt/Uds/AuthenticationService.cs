namespace Framework.Core.Cobalt.Uds
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    using Framework.Core.Net;
    using Newtonsoft.Json;



    /// <summary>
    /// Provides the ability to retrieve Prism information necessary in the construction of a UDS Session.
    /// </summary>
    internal sealed class AuthenticationService
    {
        private readonly string hostUrl;

        /// <summary>
        /// Creates an instance of the service responsible for communicating with UDS <c>authentication</c> endpoints. The <c>hostUrl</c> differs by environment and by product.
        /// </summary>
        /// <param name="hostUrl">A host URL for UDS, e.g. <c>http://uds.int.next.qed.westlaw.com/UDS</c>.</param>
        public AuthenticationService(string hostUrl)
        {
            this.hostUrl = hostUrl;
        }

        /// <summary>
        /// Retrieves Prism authentication information based upon Prism credentials. An <see cref="ArgumentNullException"/> will be thrown if any of the fields are <c>null</c>. Additionally, an <see cref="ArgumentException"/> will be thrown if any of the fields are an empty or blank string.
        /// </summary>
        /// <param name="prismUsername">A Prism username.</param>
        /// <param name="prismPassword">A Prism password corresponding with the provided Prism username.</param>
        /// <returns>A set of Prism authentication information.</returns>
        public AuthenticationInfo RetrieveAuthenticationInfo(string prismUsername, string prismPassword)
        {
            // validate input
            if (prismUsername == null)
            {
                throw new ArgumentNullException("prismUsername");
            }

            if (prismUsername.Trim().Length == 0)
            {
                throw new ArgumentException("The prismUsername cannot be an empty or blank string.", "prismUsername");
            }

            if (prismPassword == null)
            {
                throw new ArgumentNullException("prismPassword");
            }

            if (prismPassword.Trim().Length == 0)
            {
                throw new ArgumentException("The prismPassword cannot be an empty or blank string.", "prismPassword");
            }

            // construct request
            string url = this.hostUrl + "/v1/authentication/" + prismUsername + "/" + prismPassword;
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Get, url, MimeType.Application.Json);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    Stream stream = response.GetResponseStream();
                    if (stream == null)
                    {
                        throw new IOException("No AuthenticationInfo data was received in response from UDS.");
                    }

                    using (var streamReader = new StreamReader(stream, Encoding.UTF8))
                    {
                        return AuthenticationService.PersistJsonToAuthenticationInfo(streamReader);
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }

        /// <summary>
        /// Deserializes a JSON string into Authentication Information. A <see cref="MissingFieldException"/> is thrown if an unexpected field is encountered in the JSON.
        /// </summary>
        /// <param name="jsonData">A serialized form of Authentication Information.</param>
        /// <returns>Authentication Information.</returns>
        private static AuthenticationInfo PersistJsonToAuthenticationInfo(StreamReader jsonData)
        {
            using (var jsonReader = new JsonTextReader(jsonData))
            {
                var authenticationInfo = new AuthenticationInfo();
                jsonReader.Read();
                while (jsonReader.TokenType != JsonToken.EndObject)
                {
                    if (jsonReader.TokenType == JsonToken.PropertyName)
                    {
                        string fieldName = (string)jsonReader.Value;
                        jsonReader.Read();
                        switch (fieldName)
                        {
                            case "PrismGuid":
                                authenticationInfo.PrismGuid = (string)jsonReader.Value;
                                break;
                            case "PrismAuthToken":
                                authenticationInfo.PrismAuthToken = (string)jsonReader.Value;
                                break;
                            case "PrismAuthFailureReason":
                                authenticationInfo.PrismAuthFailureReason = (string)jsonReader.Value;
                                break;
                            case "PrismUserId":
                                authenticationInfo.PrismUserId = (string)jsonReader.Value;
                                break;
                            case "FirstName":
                                authenticationInfo.FirstName = (string)jsonReader.Value;
                                break;
                            case "LastName":
                                authenticationInfo.LastName = (string)jsonReader.Value;
                                break;
                            case "EndDate":
                                authenticationInfo.EndDate = (DateTime)jsonReader.Value;
                                break;
                            case "PrismAuthStatusCode":
                                authenticationInfo.PrismAuthStatusCode = (int)((long)jsonReader.Value);
                                break;
                            default:
                                throw new MissingFieldException("Field cannot be parsed into AuthenticationInfo: '" + fieldName + "'.");
                        }
                    }

                    jsonReader.Read();
                }

                return authenticationInfo;
            }
        }
    }
}
