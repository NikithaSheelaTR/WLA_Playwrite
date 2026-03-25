namespace Framework.Common.Api.Endpoints
{
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Interfaces;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    using RestSharp;

    /// <summary>
    /// The base cobalt service client.
    /// </summary>
    public abstract class BaseCobaltServiceClient
    {
        /// <summary>
        /// The base url.
        /// </summary>
        protected string BaseUrl;

        /// <summary>
        /// The cobalt cookies.
        /// </summary>
        protected CookieContainer CobaltCookies;

        /// <summary>
        /// The environment.
        /// </summary>
        protected EnvironmentInfo Environment;

        /// <summary>
        /// The product id.
        /// </summary>
        protected CobaltProductId ProductId;

        /// <summary>
        /// The rest client.
        /// </summary>
        protected IRestClient RestClient;

        /// <summary>
        /// The security headers.
        /// </summary>
        protected NameValueCollection SecurityHeaders;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCobaltServiceClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="moduleId"> The module id. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        protected BaseCobaltServiceClient(
            EnvironmentInfo environment,
            CobaltModuleId moduleId,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(moduleId),
                    TestConfigurationRepository.DefaultInstance.FindProduct(productId),
                    environment).Uri;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            this.Environment = environment;
            this.ProductId = productId;
            this.CobaltCookies = cobaltCookies ?? new CookieContainer();

            this.RestClient = new RestClient(this.BaseUrl) { CookieContainer = this.CobaltCookies };
            this.SecurityHeaders = securityHeaders;
            this.RequestBuilder = new RequestBuilder(this.SecurityHeaders);
        }

        /// <summary>
        /// Gets or sets the last response.
        /// </summary>
        protected IRestResponse LastResponse { get; set; }

        /// <summary>
        /// Request Builder
        /// </summary>
        protected IRequestBuilder RequestBuilder { get; set; }

        /// <summary>
        /// Base URL to print
        /// </summary>
        internal string BaseEndpointURL { get => this.BaseUrl; }
    }
}