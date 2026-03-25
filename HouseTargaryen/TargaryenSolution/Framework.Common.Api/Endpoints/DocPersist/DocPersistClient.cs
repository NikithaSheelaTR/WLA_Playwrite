namespace Framework.Common.Api.Endpoints.DocPersist
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml;

    using Framework.Common.Api.Endpoints.Document.DataModel.Constants;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    using RestSharp;

    /// <summary>
    /// The doc persist client.
    /// </summary>
    public class DocPersistClient : BaseCobaltServiceClient
    {
        private readonly Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/jsonrequest" } };

        /// <summary>
        /// Initializes a new instance of the <see cref="DocPersistClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public DocPersistClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.DocPersist, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(
                        CobaltModuleId.DocPersist),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// Gets the cookies.
        /// </summary>
        public CookieContainer Cookies => this.CobaltCookies;

        /// <summary>
        /// Gets the current environment.
        /// </summary>
        public EnvironmentInfo CurrentEnvironment => this.Environment;

        /// <summary>
        /// Gets the headers.
        /// </summary>
        public NameValueCollection Headers => this.SecurityHeaders;

        /// <summary>
        /// Get Persisted Document Xml
        /// </summary>
        /// <param name="guid">Documnet guid parameter</param>
        /// <param name="checksum">checksum id parameter</param>
        /// <returns> XmlDocument </returns>
        public XmlDocument GetPersistedDocumentXml(string guid, string checksum)
        {
            string url = "/" + DocumentConstants.PersistXmlPath + guid + "/" + checksum + "/blob";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = this.headers
            });

            this.LastResponse = this.RestClient.Execute(request);

            var xd = new XmlDocument();
            xd.LoadXml(this.LastResponse.Content);

            return xd;
        }
    }
}