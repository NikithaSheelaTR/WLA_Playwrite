namespace Framework.Common.Api.Endpoints.Report
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;

    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;
    using Framework.Common.Api.Endpoints.Report.DataModel;
    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.RequestModels;
    using Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils;
    using Newtonsoft.Json.Linq;
    using RestSharp;

    /// <summary>
    /// The report client
    /// </summary>
    public sealed class ReportClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportClient"/> class. 
        /// </summary>
        /// <param name="environment"> environment  </param>
        /// <param name="productId"> productId  </param>
        /// <param name="cobaltCookies"> cobaltCookies  </param>
        /// <param name="securityHeaders"> securityHeaders  </param>
        public ReportClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Report, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Report),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// The get cache key by document.
        /// </summary>
        /// <param name="name"> The name. </param>
        /// <param name="path"> The path. </param>
        /// <param name="userCredential"> The user credential. </param>
        /// <param name="reportType">ReportType</param>
        /// <returns> DocumentKeyInfo </returns>
        public DocumentKeyInfo GetDocumentCacheKeyByUploadingDocument(string name, string path, IUserCredential userCredential, string reportType = "user")
        {
            string url = $"/Report/BriefAnalyzer/v1/ProcessDocument?ClientId={userCredential.ClientId}&reportType={reportType}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url
            });

            request.AddFileBytes("files[]", File.ReadAllBytes(path), name, "application/msword");
            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.Created }, 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.Created))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.Content}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<DocumentKeyInfo>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// The get document state info.
        /// </summary>
        /// <param name="documentKeyInfo"> The document cache key info. </param>
        /// <param name="userCredential"> The user credential. </param>
        /// <returns> DocAnalyzerAnalyzedData </returns>
        public DocAnalyzerAnalyzedData GetDocumentMetadata(DocumentKeyInfo documentKeyInfo, IUserCredential userCredential)
        {
            string url = $"/Report/BriefAnalyzer/v1/Reports/{documentKeyInfo.DocumentGuid}/metadata";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<DocAnalyzerAnalyzedData>
                    (this.LastResponse.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// The get document state info.
        /// </summary>
        /// <param name="documentKeyInfo"> The document cache key info. </param>
        /// <param name="userCredential"> The user credential. </param>
        /// <returns> The <see cref="DocAnalyzerAnalyzedData"/>. </returns>
        public DocAnalyzerAnalyzedData GetDocumentData(DocumentKeyInfo documentKeyInfo, IUserCredential userCredential)
        {
            string url =
                $"/Report/BriefAnalyzer/v1/Reports/{documentKeyInfo.DocumentGuid}?contextData=(sc.Default)&transitionType=Default&clientId={userCredential.ClientId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<DocAnalyzerAnalyzedData>(
                    this.LastResponse.Content);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// The get quotes from document.
        /// </summary>
        /// <param name="quoteCheckDocGuid"> QuoteCheck doc guid. </param>
        /// <param name="userCredential"> The user credential. </param>
        /// <returns> The quotes <see cref="DocAnalyzerQuoteCheckData"/>. </returns>
        public List<DocAnalyzerQuoteCheckData> GetQuoteCheckData(string quoteCheckDocGuid, IUserCredential userCredential)
        {
            string url =
                $"/Report/BriefAnalyzer/v1/Reports/{quoteCheckDocGuid}?contextData=(sc.Default)&transitionType=Default&clientId={userCredential.ClientId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                var response = JObject.Parse(this.LastResponse.Content)["sections"][0]["data"][0]["orchestrationData"]["quotations"].ToString();
                return ObjectSerializer.DeserializeJsonToObject<List<DocAnalyzerQuoteCheckData>>(
                    response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Get quotes from document.
        /// </summary>
        /// <param name="quoteCheckDocGuid"> QuoteCheck doc guid. </param>
        /// <param name="userCredential"> The user credential. </param>
        /// <returns> The quotes <see cref="QuotationsData"/>. </returns>
        public List<QuotationsData> GetQuotationsData(string quoteCheckDocGuid, IUserCredential userCredential)
        {
            string url =
                $"/Report/BriefAnalyzer/v1/Reports/{quoteCheckDocGuid}?contextData=(sc.Default)&transitionType=Default&clientId={userCredential.ClientId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                var response = JObject.Parse(this.LastResponse.Content)["sections"][0]["data"][0]["orchestrationData"]["quotations"].ToString();
                return ObjectSerializer.DeserializeJsonToObject<List<QuotationsData>>(
                    response);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Get Judicial ReportId
        /// </summary>
        /// <param name="name"></param>
        /// <param name="path"></param>
        /// <param name="userCredential"></param>
        /// <returns></returns>
        public JudicialUploadFileResponseInfo UploadFileToJudicial(string name, string path, IUserCredential userCredential)
        {
            string url = $"/Report/BriefAnalyzer/v1/Judicial/Upload/File?clientId={userCredential.ClientId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url
            });

            request.AddFile("file", path + "//" + name);

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
        /// gets judicial id for current report
        /// </summary>
        /// <param name="checkInfo"></param>
        /// <returns></returns>
        public FileStatusInfo StartCheckJudicialReport(JudicialStartCheckInfo checkInfo)
        {
            string url = "/Report/BriefAnalyzer/v1/Judicial/Upload/StartCheck";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url
            });

            request.AddJsonBody(checkInfo);

            this.LastResponse = this.RestClient.Execute(request);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.Content}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<FileStatusInfo>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }        

        /// <summary>
        /// Rerun report
        /// </summary>
        /// <param name="rerunRequest"></param>
        /// <returns> DocumentKeyInfo </returns>
        public DocumentKeyInfo GetRerunReportInfo(RerunReportRequest rerunRequest)
        {
            string url = "/Report/BriefAnalyzer/v1/Reports/RerunReport";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                DataFormat = DataFormat.Json,
                Data = rerunRequest
            });

            this.LastResponse = this.RestClient.Execute(request);
            
            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.Content}");
            }

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<DocumentKeyInfo>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        } 
    }
}
