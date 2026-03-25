namespace Framework.Common.Api.Utilities
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;

    using Framework.Common.Api.Interfaces;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The request builder.
    /// </summary>
    public class RequestBuilder : IRequestBuilder
    {
        private const string JsonContentType = "application/json";
        private const string XmlContentType = "text/xml";

        /// <summary>
        /// The security headers.
        /// </summary>
        private readonly NameValueCollection securityHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestBuilder"/> class.
        /// </summary>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        public RequestBuilder(NameValueCollection securityHeaders)
        {
            this.securityHeaders = securityHeaders;
        }

        /// <summary>
        /// Build Request
        /// </summary>
        /// <param name="arguments"> Request Arguments </param>
        /// <returns> IRestRequest </returns>
        public IRestRequest BuildRequest(RequestArguments arguments)
        {
            var request = new RestRequest(arguments.Resource, arguments.Method) { RequestFormat = arguments.DataFormat };
            this.AddSecurityHeaders(request);
            this.AddHeaders(arguments, request);
            this.AddParameters(request, arguments);
            this.AddJsonBodyFromFile(request, arguments);
            this.AddBody(request, arguments);

            return request;
        }

        /// <summary>
        /// Add Body
        /// </summary>
        /// <param name="request"> IRestRequest </param>
        /// <param name="arguments"> Request Arguments </param>
        private void AddBody(IRestRequest request, RequestArguments arguments)
        {
            string serializedObject = string.Empty;
            string contentType = null;

            if (arguments.Data != null)
            {
                switch (arguments.DataFormat)
                {
                    case DataFormat.Json:
                        serializedObject = ObjectSerializer.SerializeJsonObject(arguments.Data);
                        contentType = JsonContentType;
                        break;

                    case DataFormat.Xml:
                        serializedObject = request.XmlSerializer.Serialize(arguments.Data);
                        contentType = XmlContentType;
                        break;
                }

                request.AddParameter(contentType, serializedObject, ParameterType.RequestBody);
            }
        }

        /// <summary>
        /// Add Security Headers If Present
        /// </summary>
        /// <param name="request"> request </param>
        private void AddSecurityHeaders(RestRequest request)
        {
            if (this.securityHeaders != null)
            {
                foreach (string key in this.securityHeaders)
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        request.AddHeader(key, this.securityHeaders[key]);
                    }
                }
            }
        }

        /// <summary>
        /// Add Headers If Present
        /// </summary>
        /// <param name="arguments"> Arguments </param>
        /// <param name="request"> Rest Request </param>
        private void AddHeaders(RequestArguments arguments, IRestRequest request)
        {
            if (arguments.Headers != null)
            {
                foreach (KeyValuePair<string, string> head in arguments.Headers)
                {
                    request.AddHeader(head.Key, head.Value);
                }
            }
        }

        /// <summary>
        /// Add Parameters
        /// </summary>
        /// <param name="request"> The request. </param>
        /// <param name="arguments"> The arguments. </param>
        private void AddParameters(IRestRequest request, RequestArguments arguments)
        {
            if (arguments.Parameters != null)
            {
                foreach (Parameter item in arguments.Parameters)
                {
                    request.AddParameter(item.Name, item.Value, item.Type);
                }
            }
        }


        /// <summary>
        /// Add Json Body to a Request
        /// </summary>
        /// <param name="request"> Rest Requst </param>
        /// <param name="arguments"> The arguments. </param>
        private void AddJsonBodyFromFile(IRestRequest request, RequestArguments arguments)
        {
            if (arguments.JsonSourcePath != null)
            {
                string json = null;

                using (var reader = new StreamReader(arguments.JsonSourcePath))
                {
                    json = reader.ReadToEnd();
                }

                request.AddParameter(JsonContentType, json, ParameterType.RequestBody);
            }
        }
    }
}
