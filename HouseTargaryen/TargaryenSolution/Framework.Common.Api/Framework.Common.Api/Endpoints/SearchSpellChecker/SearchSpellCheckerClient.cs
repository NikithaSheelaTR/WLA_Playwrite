namespace Framework.Common.Api.Endpoints.SearchSpellChecker
{
    using System.Collections.Specialized;
    using System.Net;
    using Framework.Common.Api.Endpoints.TypeAhead;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// The SearchSpellChecker Client
    /// </summary>
    public class SearchSpellCheckerClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAheadClient"/> class. 
        /// </summary>
        /// <param name="environment"> environment </param>
        /// <param name="productId"> product Id </param>
        /// <param name="cobaltCookies"> cobalt Cookies </param>
        /// <param name="securityHeaders"> security Headers </param>
        public SearchSpellCheckerClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.SearchSpellChecker, productId, cobaltCookies, securityHeaders)
        {
            {
                this.BaseUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(
                        CobaltModuleId
                            .SearchSpellChecker),
                    TestConfigurationRepository.DefaultInstance.FindProduct(
                        this.ProductId),
                    environment).Uri;
            }
        }
    }
}
