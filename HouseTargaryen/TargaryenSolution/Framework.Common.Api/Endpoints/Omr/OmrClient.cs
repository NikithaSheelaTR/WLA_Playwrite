namespace Framework.Common.Api.Endpoints.Omr
{
    using System.Collections.Specialized;
    using System.Net;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// The OMR module client
    /// </summary>
    public class OmrClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OmrClient"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="cobaltCookies">
        /// The cobalt cookies.
        /// </param>
        /// <param name="securityHeaders">
        /// The security headers.
        /// </param>
        public OmrClient(EnvironmentInfo environment, CobaltProductInfo product, CookieContainer cobaltCookies, NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Omr, CobaltProductId.None, cobaltCookies, securityHeaders)
        {
        }
    }
}