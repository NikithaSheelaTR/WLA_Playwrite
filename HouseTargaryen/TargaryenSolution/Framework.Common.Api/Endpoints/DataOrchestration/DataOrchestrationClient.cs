namespace Framework.Common.Api.Endpoints.DataOrchestration
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels;
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels;
    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels;
    using Framework.Common.Api.Enums;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    using RestSharp;

    using JudicialTabResponse = DataModel.Judicial.ResponseModels.JudicialTabResponse;

    /// <inheritdoc />
    /// <summary>
    /// The Data Orchestration API client
    /// </summary>
    public sealed class DataOrchestrationClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Gets the quotations view mode enumeration to BaseTextModel map.
        /// </summary>
        private EnumPropertyMapper<QuotationsViewMode, BaseTextModel> QuotationsViewModeMap =>
            EnumPropertyModelCache.GetMap<QuotationsViewMode, BaseTextModel>("Api");

        /// <summary>
        /// Initializes a new instance of the <see cref="DataOrchestrationClient"/> class
        /// </summary>
        /// <param name="environment">The environment under test</param>
        /// <param name="productId">The product under test</param>
        /// <param name="cobaltCookies">Cookies</param>
        /// <param name="securityHeaders">Headers</param>
        public DataOrchestrationClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.DataOrchestration, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                TestConfigurationRepository.DefaultInstance.FindModule(
                    CobaltModuleId
                        .DataOrchestration),
                TestConfigurationRepository.DefaultInstance.FindProduct(
                    this.ProductId),
                environment).Uri;
        }

        /// <summary>
        /// Get quotations report response
        /// </summary>
        /// <param name="quotationsId">quotationId</param>
        /// <param name="from">from value</param>
        /// <param name="size">size value</param>
        /// <param name="viewMode">viewMode</param>
        /// <param name="securityToken">securityToken</param>
        /// <returns>Quotations report response</returns>
        public string GetQuotationsReportResponse(
            string quotationsId,
            int from,
            int size,
            QuotationsViewMode viewMode,
            string securityToken)
        {
            string url = $"/DataOrchestration/v1/briefanalyzer/report/quotations/{quotationsId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.GET,
                    Resource = url,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "Security-Token",
                            Value = securityToken,
                            Type = ParameterType.HttpHeader
                        }
                    }
                });
            request.AddParameter("from", from);
            request.AddParameter("size", size);
            request.AddParameter("filterCategory", this.QuotationsViewModeMap[viewMode].Text);
            request.AddParameter("sortOrder", "orderOfAppearance");
            request.AddParameter("citationId", "");
            request.AddParameter("searchByCitation", "False");

            IRestResponse response = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.OK }, 5);

            return response.Content;
        }

        /// <summary>
        /// Get file status uploading document to judicial
        /// </summary>
        /// <param name="resultListGuid">resultListGuid</param>
        public UploadedReportInfo GetJudicialFileStatus(string resultListGuid)
        {
            string url = $"/DataOrchestration/v1/briefanalyzer/judicial/{resultListGuid}/getStatus";

            IRestRequest request =
                this.RequestBuilder.BuildRequest(new RequestArguments { Method = Method.GET, Resource = url });
            this.LastResponse = this.RestClient.Execute(request);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<UploadedReportInfo>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// gets judicial id for current report
        /// </summary>
        /// <param name="checkInfo"></param>
        /// <param name="securityToken">securityToken</param>
        /// <returns></returns>
        public ResultListInfo StartCheckJudicialReport(DataOrchestrationResultListInfo checkInfo, string securityToken)
        {
            string url = "/DataOrchestration/v1/judicial/resultlist";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = url,
                    Data = checkInfo,
                    DataFormat = DataFormat.Json,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "Security-Token",
                            Value = securityToken,
                            Type = ParameterType.HttpHeader
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<ResultListInfo>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Upload file to Judicial
        /// </summary>
        /// <param name="name">file name</param>
        /// <param name="path">file path</param>
        /// <param name="userCredential">user credentials</param>
        /// <param name="securityToken">securityToken</param>
        /// <returns></returns>
        public JudicialUploadFileResponseInfo UploadFileToJudicial(
            string name,
            string path,
            IUserCredential userCredential,
            string securityToken)
        {
            string url = $"/DataOrchestration/v1/briefanalyzer/quickcheck/processdocument";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = url,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "Security-Token",
                            Value = securityToken,
                            Type = ParameterType.HttpHeader
                        }
                    }
                });

            request.AddFileBytes("file[]", File.ReadAllBytes($"{path}/{name}"), name, "application/msword");
            request.AddParameter("clientId", $"{userCredential.ClientId}");
            request.AddParameter("reportType", "judicial");

            this.LastResponse = this.RestClient.Execute(request);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.Content}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<JudicialUploadFileResponseInfo>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// GetJudicialTabInfo
        /// </summary>
        /// <param name="resultListGuid">resultListGuid</param>
        /// <param name="tab">TabOption</param>
        /// <param name="clientId">clientId</param>
        /// <param name="securityToken">securityToken</param>
        /// <returns>tab info</returns>
        public JudicialTabResponse GetJudicialTabInfo(
            string resultListGuid,
            JudicialTabOption tab,
            string clientId,
            string securityToken)
        {
            string url = $"/DataOrchestration/v1/judicial/resultlist/{resultListGuid}";

            var sortKeyOption = new SortKeyInfo();
            sortKeyOption.SetOption(tab);
            sortKeyOption.ClientId = clientId;

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = url,
                    Data = new ResultListRequestInfo()
                    {
                        Filter = new FilterInfo()
                        {
                            Results = new List<ResultInfo>()
                            {
                                new ResultInfo() { Index = "1" }
                            }
                        },
                        SortKey = sortKeyOption
                    },
                    DataFormat = DataFormat.Json,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "Security-Token",
                            Value = securityToken,
                            Type = ParameterType.HttpHeader
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<JudicialTabResponse>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// SubmitToQuickCheck
        /// </summary>
        /// <param name="requestModel">requestModel</param>
        /// <param name="securityToken">securityToken</param>
        /// <returns></returns>
        public string SubmitToQuickCheck(SubmitToQuickCheckAnalyzeRequest requestModel, string securityToken)
        {
            var url = "/DataOrchestration/v1/briefanalyzer/quickcheck/submitWestlawDocument";

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                {
                    Method = Method.POST,
                    Resource = url,
                    DataFormat = DataFormat.Json,
                    Data = requestModel,
                    Parameters = new List<Parameter>
                    {
                        new Parameter
                        {
                            Name = "Security-Token",
                            Value = securityToken,
                            Type = ParameterType.HttpHeader
                        }
                    }
                });

            this.LastResponse = this.RestClient.Execute(request);
            try
            {
                return ObjectSerializer.DeserializeJsonToObject<JudicialUploadFileResponseInfo>(
                    this.LastResponse.Content).ReportId;
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }


    }
}