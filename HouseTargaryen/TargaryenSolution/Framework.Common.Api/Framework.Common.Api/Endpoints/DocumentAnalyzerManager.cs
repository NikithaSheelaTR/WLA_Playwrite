namespace Framework.Common.Api.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using Framework.Common.Api.Endpoints.DataOrchestration;
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels;
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels;
    using Framework.Common.Api.Endpoints.Document;
    using Framework.Common.Api.Endpoints.Foldering;
    using Framework.Common.Api.Endpoints.Report;
    using Framework.Common.Api.Endpoints.Report.DataModel;
    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.RequestModels;
    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels;
    using Framework.Common.Api.Endpoints.Search;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Enums;
    using Framework.Common.Api.Raw.BusinessLawTransition.Contracts;
    using Framework.Common.Api.Raw.BusinessLawTransition.Requests;
    using Framework.Common.Api.Raw.BusinessLawTransition.Services;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Net;

    using JudicialTabResponse = DataOrchestration.DataModel.Judicial.ResponseModels.JudicialTabResponse;

    /// <summary>
    /// The document analyzer manager.
    /// </summary>
    public class DocumentAnalyzerManager
    {
        /// <summary>
        /// The report client.
        /// </summary>
        public readonly ReportClient ReportClient;

        /// <summary>
        /// The data orchestration client.
        /// </summary>
        public readonly DataOrchestrationClient DataOrchestrationClient;

        /// <summary>
        /// The foldering client.
        /// </summary>
        public readonly FolderingClient FolderingClient;

        /// <summary>
        /// The search client.
        /// </summary>
        public readonly SearchClient SearchClient;

        /// <summary>
        /// The search client.
        /// </summary>
        public readonly DocumentClient DocumentClient;

        private readonly IUserCredential userCredential;

        private EnvironmentInfo EnvironmentInfo;

        private const string SecurityUserName = "56F3A52C7171A2C0B70EEB367796D7139D56E9B7D7FC047A2F7949F8BB1AC498";
        private const string SecurityPassword = "FB4F366409D96E3DFE250D4B71D69955";

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentAnalyzerManager"/> class.
        /// </summary>
        /// <param name="userCredential">
        /// The user credential.
        /// </param>
        /// <param name="cobaltProductInfo">
        /// The cobalt product info.
        /// </param>
        /// <param name="sessionInfo">
        /// The session info.
        /// </param>
        /// <param name="environmentInfo">
        /// The environment info.
        /// </param>
        /// <param name="cookie">
        /// The cookie.
        /// </param>
        public DocumentAnalyzerManager(
            IUserCredential userCredential,
            CobaltProductInfo cobaltProductInfo,
            UdsSessionInfo sessionInfo,
            EnvironmentInfo environmentInfo,
            CookieContainer cookie)
        {
            this.userCredential = userCredential;

            this.EnvironmentInfo = environmentInfo;

            this.ReportClient = ApiClientFactory.GetInstance<ReportClient>(
                this.userCredential.ToOnePassUserInfo(),
                sessionInfo.SessionId,
                cobaltProductInfo,
                environmentInfo,
                cookie);

            this.DataOrchestrationClient = ApiClientFactory.GetInstance<DataOrchestrationClient>(
                this.userCredential.ToOnePassUserInfo(),
                sessionInfo.SessionId,
                cobaltProductInfo,
                environmentInfo,
                cookie);

            this.FolderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                this.userCredential.ToOnePassUserInfo(),
                sessionInfo.SessionId,
                cobaltProductInfo,
                environmentInfo,
                cookie);

            this.SearchClient = ApiClientFactory.GetInstance<SearchClient>(
                this.userCredential.ToOnePassUserInfo(),
                sessionInfo.SessionId,
                cobaltProductInfo,
                environmentInfo,
                cookie);

            this.DocumentClient = ApiClientFactory.GetInstance<DocumentClient>(
                this.userCredential.ToOnePassUserInfo(),
                sessionInfo.SessionId,
                cobaltProductInfo,
                environmentInfo,
                cookie);
        }

        /// <summary>
        /// Get analyzed info by document.
        /// </summary>
        /// <param name="documentKeyInfo">Document key info </param>
        /// <returns>The document state info</returns>
        public DocAnalyzerAnalyzedData GetAnalyzedInfoByUploadingDocument(DocumentKeyInfo documentKeyInfo)
        {
            this.WaitUntilDocumentAnalyzed(documentKeyInfo);
            return this.ReportClient.GetDocumentData(documentKeyInfo, this.userCredential);
        }        

        /// <summary>
        /// The wait until document analyzed.
        /// </summary>
        /// <param name="documentKeyInfo">
        /// The document key info.
        /// </param>
        /// <param name="numberOfAttempts">
        /// The number of attempts.
        /// </param>
        /// <param name="timeoutBetweenRequests">
        /// The timeout between requests.
        /// </param>
        private void WaitUntilDocumentAnalyzed(
        DocumentKeyInfo documentKeyInfo,
        int numberOfAttempts = 120,
        int timeoutBetweenRequests = 10000)
        {
            DocAnalyzerAnalyzedData docAnalyzerAnalyzedData = this.ReportClient.GetDocumentMetadata(
                                                          documentKeyInfo,
                                                          this.userCredential);

            for (int i = 0; !docAnalyzerAnalyzedData.State.Equals("Complete") && i < numberOfAttempts - 1; i++)
            {
                if (docAnalyzerAnalyzedData.State.Equals("Error"))
                {
                    throw new Exception($"{docAnalyzerAnalyzedData.Name} was not uploaded. {docAnalyzerAnalyzedData.DocumentMetadata.ErrorResponse.ToString()}");
                }

                Thread.Sleep(timeoutBetweenRequests);
                docAnalyzerAnalyzedData = this.ReportClient.GetDocumentMetadata(documentKeyInfo, this.userCredential);
            }
        } 

        /// <summary>
        /// Get result list guid
        /// </summary>
        /// <param name="firstParty">
        /// </param>
        /// <param name="secondParty">
        /// </param>
        /// The client Id.
        /// <returns>
        /// </returns>
        public ResultListInfo GetJudicialResultListGuid(PartyInfoRequest firstParty, PartyInfoRequest secondParty)
        {
            var startCheckInfo = new DataOrchestrationResultListInfo()
            {
                Expiry = new ExpiryReportInfo()
                {
                    Duration = DateTime.UtcNow,
                    Type = "Session"
                },
                OrchestrationData = new OrchestrationDataRequest()
                {
                    ReportName = $"{firstParty.Name} v {secondParty.Name}",
                    ClientId = this.userCredential.ClientId,
                    Parties = new List<PartyInfoRequest>()
                   {
                       firstParty,
                       secondParty
                   }
                }
            };
            return this.DataOrchestrationClient.StartCheckJudicialReport(startCheckInfo, this.GetSecurityToken());
        }

        /// <summary>
        /// Create party info
        /// </summary>
        /// <returns>PartyInfoRequest</returns>
        public PartyInfoRequest CreateJudicialPartyInfoInstance() => new PartyInfoRequest();        

        /// <summary>
        /// Get Security Token for DA
        /// </summary>
        /// <returns></returns>
        public string GetSecurityToken()
        {
            var securityTokenRequest = new SecurityTokenRequest(SecurityUserName, SecurityPassword);
            var securityService = new SecurityService(this.EnvironmentInfo);
            EndpointResponse<SecurityTokenContract> securityToken = securityService.GetSecurityToken(securityTokenRequest);
            return securityToken.ResponseBody.Token;
        }
    }
}
