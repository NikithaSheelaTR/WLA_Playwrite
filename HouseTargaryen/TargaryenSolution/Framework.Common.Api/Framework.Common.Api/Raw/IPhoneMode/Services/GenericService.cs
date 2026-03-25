namespace Framework.Common.Api.Raw.IPhoneMode.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Constants;
    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.Net;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Enums;
    using RestSharp.Extensions;

    /// <summary>
    /// Simple Service
    /// </summary>
    public class GenericService
    {
        /// <summary>
        /// Request Headers
        /// </summary>
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// URL that these services target
        /// </summary>
        private readonly string hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericService"/> class. 
        /// Constructs the object
        /// </summary>
        /// <param name="module">
        /// The module.
        /// </param>
        /// <param name="environmentId">Environment ID</param>
        public GenericService(Constants.Modules module, EnvironmentId environmentId)
        {

            // string isDcExit = Environment.GetEnvironmentVariable(EnvironmentConstants.IsDcExit);
            string isDcExit = null;
            if (environmentId.Equals(EnvironmentId.DemoAWS) | environmentId.Equals(EnvironmentId.QedAWS) | environmentId.Equals(EnvironmentId.QedAWS2))
            {
                isDcExit = "Yes";
            }
            else
            {
                isDcExit = "No";
            }
            this.headers = new WebHeaderCollection();
            string environment = environmentId.GetUrlForWestlawNext();
            string environmentName = environmentId.ToString().ToLower();
            string hostId = environmentName.StartsWith("demo") || environmentName.StartsWith("ci") || environmentName.StartsWith("demoaws") ? "30962" : "92615";

            // Define Host URL and Request Headers
            if (isDcExit != null && isDcExit.Equals("Yes"))
            {
                if(environmentName=="qedaws")
                {
                    environmentName = "qed";
                }
                else
                {
                    environmentName = "demo";
                }

                switch (module)
                {
                    case Constants.Modules.Alert:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"alert-int-next-{environmentName}-westlaw-com.{hostId}.aws-int.thomsonreuters.com");
                        break;
                    case Constants.Modules.WebContent:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"webcontent-int-next-{environmentName}-westlaw-com.{hostId}.aws-int.thomsonreuters.com");
                        break;
                    case Constants.Modules.Westlaw:
                        this.hostUrl = environment;
                        break;
                    case Constants.Modules.Search:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"search-int-next-{environmentName}-westlaw-com.{hostId}.aws-int.thomsonreuters.com");
                        break;
                    default:
                        this.hostUrl = environment;
                        break;
                }
            }
            else
            {
                switch (module)
                {
                    case Constants.Modules.Alert:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"alert.int.next.{environmentName}.westlaw.com");
                        break;
                    case Constants.Modules.WebContent:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"webcontent.int.next.{environmentName}.westlaw.com");
                        break;
                    case Constants.Modules.Westlaw:
                        this.hostUrl = environment;
                        break;
                    case Constants.Modules.Search:
                        this.hostUrl = environment;
                        this.headers.Add("x-cobalt-host", $"search.int.next.{environmentName}.westlaw.com");
                        break;
                    default:
                        this.hostUrl = environment;
                        break;
                }
            }
            this.headers.Add("x-cobalt-product-container", Constants.WestlawnextProductid);
        }

        /// <summary>
        /// SendRequest allows you to pass in a HttpMethod type
        /// </summary>
        /// <param name="requestType"> request Type </param>
        /// <param name="url"> URL </param>
        /// <param name="request"> request </param>
        /// <param name="headersList"> headers List </param>
        /// <param name="cookies"> cookies </param>
        /// <param name="acceptableResponses"> The acceptable Responses. </param>
        /// <returns> Endpoint Response </returns>
        public EndpointResponse<string> SendRequest(
            HttpUtils.HttpMethod requestType,
            string url,
            GenericRequest request = null,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            switch (requestType)
            {
                case HttpUtils.HttpMethod.Post:
                    return this.Post(url, request, headersList, cookies, acceptableResponses);
                case HttpUtils.HttpMethod.Get:
                    return this.Get(url, headersList, cookies, acceptableResponses);
                case HttpUtils.HttpMethod.Delete:
                    return this.Delete(url, headersList, cookies, acceptableResponses);
                default:
                    return null;
            }
        }

        /// <summary>
        /// DELETE
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <param name="acceptableResponses">acceptable Responses</param>
        /// <returns>Endpoint Response</returns>
        private EndpointResponse<string> Delete(
            string url,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            url = this.hostUrl + url;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.DeleteRequest<string>(url, this.headers, cookies, acceptableResponses);
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <param name="acceptableResponses">acceptable Responses</param>
        /// <returns>Endpoint Response</returns>
        private EndpointResponse<string> Get(
            string url,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            url = this.hostUrl + url;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<string>(url, this.headers, cookies, acceptableResponses);
        }

        /// <summary>
        /// POST
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <param name="acceptableResponses">acceptable Responses</param>
        /// <returns>Endpoint Response</returns>
        private EndpointResponse<string> Post(
            string url,
            GenericRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            url = this.hostUrl + url;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<string>(
                url,
                request.GetRequestBody(),
                this.headers,
                cookies,
                acceptableResponses: acceptableResponses);
        }
    }
}