namespace Framework.Common.Api.Endpoints
{
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Endpoints.Document;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    /// <summary>
    /// The document api manager.
    /// </summary>
    public class DocumentApiManager
    {
        /// <summary>
        /// The method gets access to the document and returns full html response.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param> 
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        /// <param name="guid"> The guid. </param>
        /// <param name="chunkNumber"> The chunk number. </param>
        /// <returns> The <see cref="FullDocumentResponse"/>. </returns>
        public static FullDocumentResponse GetFullHtmlResponse (
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders,
            string guid,
            int chunkNumber = -1)
        {
            var websiteClient = new WebsiteClient(environment, productId, cobaltCookies, securityHeaders);
            websiteClient.GetDocument(guid);

            var documentClient = new DocumentClient(
                environment,
                productId,
                cobaltCookies,
                securityHeaders);

            return documentClient.GetFullHtmlDocumentResponse(guid, chunkNumber);
        }
    }
}
