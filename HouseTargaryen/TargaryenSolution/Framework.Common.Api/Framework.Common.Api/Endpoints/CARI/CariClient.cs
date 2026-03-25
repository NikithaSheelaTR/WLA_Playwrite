namespace Framework.Common.Api.Endpoints.CARI
{
    using Framework.Common.Api.Endpoints.CARI.DataModel.ResponseModls;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Newtonsoft.Json;
    using RestSharp;
    using System.Collections.Specialized;
    using System.Net;

    /// <summary>
    ///  CARI client (external flows).
    /// </summary>
    public class CariClient : BaseCobaltServiceClient
    {
        private const string AuthorizationHeader = "Authorization";
        private const string TokenTypeHeader = "x-token-type";
        private const string LegalClientHeader = "x-legal-client-id";
        private const string DefaultTokenType = "ciam";
        private const string DefaultLegalClientId = "NOCLIENTID";
        private const string ParameterName = "application/json";

        /// <summary>
        /// Initializes a new instance of the <see cref="CariClient"/> class.
        /// </summary>
        public CariClient(EnvironmentInfo environment, CobaltProductId productId, CookieContainer cobaltCookies, NameValueCollection securityHeaders) : base(environment, CobaltModuleId.Alerts, productId, cobaltCookies, securityHeaders)
        {
            ServicePointManager.SecurityProtocol |= SecurityProtocolType.Tls12;
            this.BaseUrl = environment.Id.ToString().ToLower().Contains("demo") ? "https://guided-research-int.plexus-cari-ppuse1.97328.aws-int.thomsonreuters.com/skill/guided-research" : "https://api.cars-qa.thomsonreuters.com/skill/guided-research";
            this.RestClient = new RestClient(this.BaseUrl);
        }

        /// <summary>
        /// Sends a “Create Flow” request and deserializes the response.
        /// </summary>
        public DeepResearchCreateFlowResponseInfo CreateFlow(string tokenType = DefaultTokenType, string legalClientId = DefaultLegalClientId)
        {
            string url = "/US/v1/external/flows";
            string requestBody = "{ \"query\": \"What are the requirements of the functional employee doctrine?\", \"reportType\": \"level100\", \"jurisdictions\": [\"ALLCASES\"] }";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                DataFormat = DataFormat.Json
            });

            request.AddHeader(AuthorizationHeader, this.GetBearerToken());
            request.AddHeader(TokenTypeHeader, tokenType);
            request.AddHeader(LegalClientHeader, legalClientId);
            request.AddParameter(ParameterName, requestBody, ParameterType.RequestBody);

            this.LastResponse = this.RestClient.Execute(request);
            return JsonConvert.DeserializeObject<DeepResearchCreateFlowResponseInfo>(this.LastResponse.Content);
        }

        /// <summary>
        /// Sends a “Get Flow” request.
        /// </summary>
        public IRestResponse GetFlow(string flowId, string tokenType = DefaultTokenType, string legalClientId = DefaultLegalClientId)
        {
            string url = $"/US/v1/external/flows/{flowId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            request.AddHeader(AuthorizationHeader, this.GetBearerToken());
            request.AddHeader(TokenTypeHeader, tokenType);
            request.AddHeader(LegalClientHeader, legalClientId);

            this.LastResponse = this.RestClient.Execute(request);
            return this.LastResponse;
        }

        /// <summary>
        /// Get bearer token.
        /// </summary>
        private string GetBearerToken() 
        {
            var token = global::System.Environment.GetEnvironmentVariable("BEARER_TOKEN");
            return $"Bearer {token}";
        }
    }
}