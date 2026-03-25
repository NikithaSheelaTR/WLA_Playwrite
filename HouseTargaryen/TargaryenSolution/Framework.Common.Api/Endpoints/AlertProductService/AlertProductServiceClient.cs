namespace Framework.Common.Api.Endpoints.AlertProductService
{
    using Framework.Common.Api.Endpoints.AlertProductService.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using RestSharp;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization.Json;

    /// <summary>
    /// Alert Product Service Client
    /// </summary>
    public class AlertProductServiceClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AlertProductServiceClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public AlertProductServiceClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.AlertProductService, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.AlertProductService),
                TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                environment).Uri;
        }

        /// <summary>
        /// Get specific alert data model by guid.
        /// </summary>
        /// <param name="alertType"> The Alert type. </param>
        /// <param name="alertGuid"> The Alert Guid. </param>
        /// <returns> Alert data model. </returns>
        public SpecificAlertDataModel RetrieveAlertByGuid(string alertType, string alertGuid)
        {
            string url = $"/AlertProductService/v3/alerts/{alertType}/{alertGuid}";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments { Method = Method.GET, Resource = url });

            this.LastResponse = this.RestClient.Execute(request);

            return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, SpecificAlertDataModel>(
                this.LastResponse.Content);
        }

        /// <summary>
        /// Update specific alert.
        /// </summary>
        /// <param name="alertType"> The Alert type. </param>
        /// <param name="data"> The request body. </param>
        public void UpdateSpecificAlert(string alertType, SpecificAlertDataModel data)
        {
            string url = $"/AlertProductService/v3/alerts/update/{alertType}";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = url,
                    Data = data,
                    DataFormat = DataFormat.Json
                });

            this.LastResponse = this.RestClient.Execute(request);
        }
    }
}
