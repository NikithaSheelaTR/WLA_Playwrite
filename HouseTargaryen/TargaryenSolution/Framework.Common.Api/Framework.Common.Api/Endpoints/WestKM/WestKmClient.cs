namespace Framework.Common.Api.Endpoints.WestKM
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils.Extensions;

    using RestSharp;

    /// <summary>
    /// WestKmClient
    /// </summary>
    public class WestKmClient : BaseCobaltServiceClient
    {
        private const string ProdEnvironmentToken =
            "Is31+D0LYig5HGCQD4XcCtiBL6NFAPTswS9lt0v4kfOXrCqRkD+dk20opeTQdt33UqniRec3Qel4Mbk2ULXCxfCtbx0kWZbw67SX3acigM2xdnm32AAtR3Pviqx2gnU3Qr/QV7cVReK3MlWRazQOEVM/KYzOYMn1YXJSbbWpDfNaNqjhSKJnF5rbt/mw1Wkcvxv0NkzGJnpV/EFqfYSFyH3/dPO1V1jWcwIv77k45MQ=";

        private const string LowerEnvironmentToken =
            "Is31+D0LYig5HGCQD4XcCtiBL6NFAPTswS9lt0v4kfOXrCqRkD+dk20opeTQdt33UqniRec3Qel4Mbk2ULXCxfCtbx0kWZbw67SX3acigM2xdnm32AAtR4rXbx3Z93o1KWQlXwH2u1JrbfbWF5/CVKoCOwPBIJNfVGpDTCx3E+FOJd1U6zsiR9VJ4YxEEinZKp7ashLdk9AFhst70+kzkrWi+OTIk62oeQDL422FIGU=";

        /// <summary>
        /// Initializes a new instance of the <see cref="WestKmClient"/> class. 
        /// </summary>
        /// <param name="environment"> Environment </param>
        /// <param name="productId"> Product Id </param>
        ///  <param name="cobaltCookies"> Cookies </param>
        /// <param name="securityHeaders"> Security Headers </param>
        public WestKmClient(EnvironmentInfo environment, CobaltProductId productId, CookieContainer cobaltCookies, NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Website, CobaltProductId.WestKm, cobaltCookies, securityHeaders)
        {
            string token = !environment.IsLower ? ProdEnvironmentToken : LowerEnvironmentToken;

            this.SecurityHeaders.Add("WESTKM_AUTH_DATA", token);
            this.SecurityHeaders.Add("x-cobalt-ptid", "professionals-integration-test-ptid");
        }

        /// <summary>
        /// Get response
        /// </summary>
        /// <param name="requestBody"> Request body </param>
        /// <returns> The <see cref="IRestResponse"/>. </returns>
        public IRestResponse GetCiteRecognitions(string requestBody)
            => this.GetResponse(requestBody, "/rid", DataFormat.Xml);

        /// <summary>
        /// Get response
        /// </summary>
        /// <param name="requestBody"> Request body  </param>
        /// <returns> The <see cref="IRestResponse"/>. </returns>
        public IRestResponse GetKeyCiteFlags(string requestBody)
            => this.GetResponse(requestBody, "/kcflags", DataFormat.Xml);

        /// <summary>
        /// Get response
        /// </summary>
        /// <param name="requestBody"> Request body  </param>
        /// <returns> The <see cref="IRestResponse"/>. </returns>
        public IRestResponse GetValidates(string requestBody)
            => this.GetResponse(requestBody.DesEncrypt(), "/validation", DataFormat.Json);

        private IRestResponse GetResponse(string body, string url, DataFormat dataFormat)
        {
            string type = string.Empty;
            switch (dataFormat)
            {
                case DataFormat.Json:
                    type = "application/json";
                    break;
                case DataFormat.Xml:
                    type = "application/xml";
                    break;
            }

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                DataFormat = dataFormat,
                Parameters = new List<Parameter> { new Parameter { Name = type, Value = body, Type = ParameterType.RequestBody } }
            });

            return this.RestClient.Execute(request);
        }
    }
}
