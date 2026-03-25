namespace Framework.Common.Api.Endpoints.CaseNoteBook
{
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Xml;

    using Framework.Common.Api.Endpoints.CaseNoteBook.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    using RestSharp;
    using RestSharp.Extensions.MonoHttp;

    /// <summary>
    /// Document Client provide document end point
    /// </summary>
    public class CaseNoteBookClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseNoteBookClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public CaseNoteBookClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Website, CobaltProductId.CaseNotebook, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(
                        CobaltModuleId.Website),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// The post case notebook url creator.
        /// </summary>
        /// <param name="parameterValue"> The parameter value. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string PostCaseNotebookUrlCreator(string parameterValue)
        {
            const string Url = "/UrlCreator/V1/entity";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = Url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);

            var xd = new XmlDocument();
            xd.LoadXml(this.LastResponse.Content);

            return HttpUtility.UrlDecode(xd.InnerXml);
        }

        /// <summary>
        /// The post key cite flags request status.
        /// </summary>
        /// <param name="parameterValue"> The request value. </param>
        /// <param name="requestStatus"> The request status. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool PostKeyCiteFlagsRequestStatus(string parameterValue, RequestStatus requestStatus)
        {
            const string Url = "/keyciteflags";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = Url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);

            bool validations = false;

            switch (requestStatus)
            {
                case RequestStatus.Successful:

                    validations = this.LastResponse.Content.Contains("<status>000</status>")
                                  & this.LastResponse.Content.Contains("<message>Request successful</message>");
                    break;
                case RequestStatus.Failed:

                    validations = this.LastResponse.Content.Contains("<status>023</status>")
                                  & this.LastResponse.Content.Contains(
                                      "<message>Request failed -- malformed request</message>");
                    break;
                case RequestStatus.PartialSuccess:

                    validations = this.LastResponse.Content.Contains("<status>010</status>")
                                  & this.LastResponse.Content.Contains("<message>Partial success</message>");
                    break;
            }

            return validations;
        }
    }
}