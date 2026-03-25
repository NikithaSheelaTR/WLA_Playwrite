namespace Framework.Common.Api.Endpoints.Nlu
{
    using System;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.Nlu.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The NLU client
    /// </summary>
    public sealed class NluClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NluClient"/> class. 
        /// </summary>
        /// <param name="environment"> environment  </param>
        /// <param name="productId"> product Id  </param>
        /// <param name="cobaltCookies"> cobalt Cookies  </param>
        /// <param name="securityHeaders"> security Headers  </param>
        public NluClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Nlu, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Nlu),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// The GET Nlu intent v3
        /// </summary>
        /// <param name="query"> query </param>
        /// <returns> NluIntentV3 </returns>
        public NluIntentV3 GetNluIntentV3(string query)
        {
            string url =
                $"/NLU/v3/understanding/intent?query={query.Replace(" ", "%20")}%3F&intent=WestSearch&intent=TRDiscover&intent=LegalAnalytics ";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"/v3/understanding/intent endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonObject<NluJsonConverter, NluIntentV3>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }
    }
}