namespace Framework.Common.Api.Endpoints.Website
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text.RegularExpressions;
    using Framework.Common.Api.Endpoints.Website.DataModel;
    using Framework.Common.Api.Endpoints.Website.DataModel.CustomPages;
    using Framework.Common.Api.Endpoints.Website.DataModel.DynamicQa;
    using Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse;
    using Framework.Common.Api.Enums;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using RestSharp;

    /// <summary>
    /// The website client.
    /// </summary>
    public sealed partial class WebsiteClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        /// <summary>
        /// Initializes a new instance of the <see cref="WebsiteClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public WebsiteClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Website, productId, cobaltCookies, securityHeaders)
        {
            this.RestClient.UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36";
        }

        /// <summary>
        /// Delete Concourse Matter by GUID
        /// </summary>
        /// <param name="pcid">Page Event Identifier</param>
        /// <param name="ajaxToken">Ajax token.</param>
        /// <param name="concourseMatterId">Concourse matter id to delete.</param>
        /// <returns>The response status</returns>
        public string DeleteConcourseMatter(string pcid, string ajaxToken, string concourseMatterId)
        {
            string url = $"/api/v1/matters/{concourseMatterId}";
            string referer = $"{this.BaseUrl}/Matterroom/Matters/";

            var headers = new Dictionary<string, string>
                             {
                                 { "Referer", referer },
                                 { "X-Cobalt-AjaxToken", ajaxToken },
                                 { "x-cobalt-exectype", "async" },
                                 { "x-cobalt-modernize-page", "true" },
                                 { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                             };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.NoContent });

            string responseDescription = string.Equals(this.LastResponse.StatusDescription, "OK")
                                             ? $"{this.LastResponse.StatusDescription}"
                                             : $"{this.LastResponse.StatusDescription} - {this.LastResponse.StatusCode}";

            return responseDescription;
        }

        /// <summary>
        /// Get question answering V2 refresh question response
        /// </summary>
        /// <param name="signOnToken">Sign on token</param>
        /// <param name="requestBody">Questions in JSON format</param>
        /// <returns>New instance of RefreshQuestionV2ResponseModel.</returns>
        public RefreshQuestionV2ResponseModel GetQuestionAnsweringV2RefreshQuestionsResponse(string signOnToken, RefreshQuestionsV2RequestModel requestBody)
        {
            string url = $"/QuestionAnswering/v2/RefreshQuestions?signonToken={signOnToken}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = requestBody,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<RefreshQuestionV2ResponseModel>(LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }           
        }

        /// <summary>
        /// Delete specified Concourse Trash Item
        /// </summary>
        /// <param name="pcid">Page Event Identifier</param>
        /// <param name="ajaxToken">Ajax token.</param>
        /// <param name="trashItem">Concourse matter to delete.</param>
        /// <returns>The response status</returns>
        public string DeleteConcourseTrashItem(string pcid, string ajaxToken, object trashItem)
        {
            string url = "/api/v1/RecycleBin/item";

            var headers = new Dictionary<string, string>
                              {
                                  { "X-Cobalt-AjaxToken", ajaxToken },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = headers,
                Data = trashItem,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, new[] { HttpStatusCode.NoContent });

            string responseDescription = string.Equals(this.LastResponse.StatusDescription, "OK")
                                             ? $"{this.LastResponse.StatusDescription}, {this.LastResponse.Content}"
                                             : $"{this.LastResponse.StatusDescription} - {this.LastResponse.StatusCode}";

            return responseDescription;
        }

        /// <summary>
        /// Removes custom page by set Custom Page GUID number
        /// </summary>
        /// <param name="pcid">Page Event Identifier</param>
        /// <param name="customPageGuid">Custom Page GUID</param>
        /// <returns>The response status</returns>
        public string DeleteCustomPageByGuid(string pcid, string customPageGuid)
        {
            string url = $"/AccountPreferences/v1/custompages/{customPageGuid}";
            string referer = $"{this.BaseUrl}/CustomPages/{customPageGuid}?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
                              {
                                  { "Referer", referer },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-modernize-page", "true" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.Execute(request);

            string responseDescription = string.Equals(this.LastResponse.StatusDescription, "No Content")
                                             ? $"{this.LastResponse.StatusDescription}"
                                             : $"{this.LastResponse.StatusDescription} - {this.LastResponse.StatusCode}";

            return responseDescription;
        }

        /// <summary>
        /// The get custop pages
        /// </summary>
        /// <param name="pcid"> Inner HTML </param>
        /// <param name="errorMessage"> error Message </param>
        /// <returns> Custom Page Model </returns>
        public CustomPageModel[] GetAllCustomPages(string pcid, out string errorMessage)
        {
            string url = "/v1/CustomPages";
            string referer = $"{this.BaseUrl}/Search/Home.html?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
            {
                { "Referer", referer },
                { "x-cobalt-exectype", "async" },
                { "x-cobalt-modernize-page", "true" },
                { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
            };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.Execute(request);

            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                errorMessage = null;
                CustomPageModel[] response =
                    ObjectSerializer.DeserializeObject<DataContractJsonSerializer, CustomPageModel[]>(
                        this.LastResponse.Content);

                return response;
            }

            errorMessage =
                $"Attempt to get Custom Pages info FAILED: {this.LastResponse.StatusDescription} with status code: {this.LastResponse}\n";

            return null;
        }

        /// <summary>
        /// The Get all SuperAdmin custom page
        /// </summary>
        /// <param name="pcid"> Inner HTML </param>
        /// <returns> Custom Page Model </returns>
        public CustomPageSuperAdminModel GetAllSuperAdminCustomPage(string pcid)
        {
            string url = "/AccountPreferences/v1/custompages/superadmin";
            string referer = $"{this.BaseUrl}/CustomPages/Firm?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
            {
                { "Referer", referer },
                { "x-cobalt-exectype", "async" },
                { "x-cobalt-modernize-page", "true" },
                { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
            };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            try
            {
                return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, CustomPageSuperAdminModel>(
                    this.LastResponse.Content); 
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Create a Custom page request without content pages
        /// </summary>
        /// <param name="body"> The json. </param>
        /// <param name="pcid"> The pcid. </param>
        public void CreateCustomPageItem(object body, string pcid)
        {
            string url = "/AccountPreferences/v1/custompages";
            string referer = $"{this.BaseUrl}/Search/Home.html?contextData=(sc.Default)&transitionType=Default";

            var headers = new Dictionary<string, string>
            {
                { "Referer", referer },
                { "x-cobalt-exectype", "async" },
                { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
             };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = body,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Update a Custom page 
        /// </summary>
        /// <param name="adminCustomPageModel"> The json. </param>
        /// <param name="pcid"></param>
        public void UpdateCustomPageItem(string pcid, AdminCustomPageModel adminCustomPageModel)
        {
            string url = $"/AccountPreferences/v1/custompages/{adminCustomPageModel.CustomPageId}/acl";
            string referer = $"{this.BaseUrl}/Search/Home.html?contextData=(sc.Default)&transitionType=Default";

            var headers = new Dictionary<string, string>
            {
                { "Referer", referer },
                { "x-cobalt-exectype", "async" },
                { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
            };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.PUT,
                Resource = url,
                Data = adminCustomPageModel.CustomPagesAcl,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Add COntent Seaction to a custom Page (without category pages)
        /// </summary>
        /// <param name="putBody"> The put Body.</param>
        /// <param name="pcid"> The pcid.</param>
        /// <param name="pageGuid"> The page Guid.</param>
        public void AddContentToCustomPage(object putBody, string pcid, string pageGuid)
        {
            string url = $"/AccountPreferences/v1/custompages/{pageGuid}";
            string referer = $"{this.BaseUrl}/CustomPages/{pageGuid}?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
                              {
                                  { "Referer", referer },
                                  { "x-cobalt-exectype", "sync" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.PUT,
                Resource = url,
                Data = putBody,
                Headers = headers,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Gets the html-string response of portal manager items of specified in 'postBody' parameter type.
        /// </summary>
        /// <param name="pcid">The Page Event Identifier.</param>
        /// <param name="ptid">The Parent Transaction Identifier.</param>
        /// <param name="errorMessage">The error message if any error.</param>
        /// <param name="parameterValue">The post request body that specifies the type of Portal Manager items.</param>
        /// <returns>The array of Portal Manager Items.</returns>
        public PortalManagerItemModel[] GetAllPortalManagerItems(
        string pcid,
        string ptid,
        out string errorMessage,
        string parameterValue)
        {
            var regexForType = new Regex("(?<=:\")([^\"]*)");
            string itemType = regexForType.Match(parameterValue).ToString().Trim();
            string url = "/PortalManager/LoadTab";
            string referer = $"{this.BaseUrl}/PortalManager/Home.html?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
                              {
                                  { "Referer", referer },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-modernize-page", "true" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" },
                                  { "x-cobalt-ptid", $"{ptid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                errorMessage = null;
                var resultCol = new List<PortalManagerItemModel>();

                var regex = new Regex("\"module_.+\\s*.+(?=<\\/a>)");
                var regexForName = new Regex("(?<=\"co_moduleName\">)\\s+[^<]+");
                var regexForGuid = new Regex("(?<=module_)[\\w]{32}");

                foreach (Match theMatch in regex.Matches(this.LastResponse.Content))
                {
                    string source = theMatch.ToString();
                    string itemId = regexForGuid.Match(source).ToString().Trim();
                    string itemName = regexForName.Match(source).ToString().Trim();
                    if (!string.IsNullOrWhiteSpace(itemId) && !string.IsNullOrWhiteSpace(itemName))
                    {
                        resultCol.Add(new PortalManagerItemModel { Guid = itemId, Name = itemName, Type = itemType });
                    }
                }

                return resultCol.ToArray();
            }

            errorMessage =
                $"ERROR:\tAttempt to get Portal Manager Items info FAILED:\n{this.LastResponse.StatusDescription} with status code: {this.LastResponse}";

            return null;
        }

        /// <summary>
        /// Delete portal manager item by Portal Manager Item GUID number
        /// </summary>
        /// <param name="pcid">Page Event Identifier</param>
        /// <param name="ptid">Parent Transaction Identifier</param>
        /// <param name="portalManagerItem">Portal Manager Item</param>
        /// <returns>The response status</returns>
        public string DeletePortalManagerItemByGuid(string pcid, string ptid, PortalManagerItemModel portalManagerItem)
        {
            string url = $"/v1/{portalManagerItem.Type}/Delete/{portalManagerItem.Guid}";
            string referer = $"{this.BaseUrl}/PortalManager/Home?transitionType=Default&contextData=(sc.Default)";

            var headers = new Dictionary<string, string>
                              {
                                  { "Referer", referer },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-modernize-page", "true" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" },
                                  { "x-cobalt-ptid", $"{ptid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            string responseDescription = string.Equals(this.LastResponse.StatusDescription, "OK")
                                             ? $"{this.LastResponse.StatusDescription}"
                                             : $"{this.LastResponse.StatusDescription} - {this.LastResponse.StatusCode}";

            return responseDescription;
        }

        /// <summary>
        /// Get dockets batch info
        /// </summary>
        /// <param name="docketsId">Dockets Ids</param>
        /// <returns>Docket batch info model</returns>
        public DocketBatchInfoModel[] GetDocketsBatchInfo(params string[] docketsId)
        {
            string url = "/V1/Docket/Update/Batch";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = docketsId,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            return ObjectSerializer.DeserializeJsonObject<NluJsonConverter, DocketBatchInfoModel[]>(
                this.LastResponse.Content);
        }

        /// <summary>
        /// This method gets access to the document by guid through the website
        /// </summary>
        /// <param name="guid"> Guid </param>
        public void GetDocument(string guid)
        {
            string url =
               $"/Document/{guid}/View/FullText.html?transitionType=Default&contextData=(sc.Default)";
            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Get dockets pdfs batch info
        /// </summary>
        /// <param name="parameterValue"> The parameter Value </param>
        /// <returns> Pdfs batch info models </returns>
        public DocketsPdfsBatchInfoModel[] GetDocketsPdfsBatchInfo(string parameterValue)
        {
            string url = "/V1/Docket/Pdf/Batch";
            string parameterName = "application/json; charset=utf-8";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = parameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);

            return ObjectSerializer.DeserializeJsonObject<NluJsonConverter, DocketsPdfsBatchInfoModel[]>(this.LastResponse.Content);
        }

        /// <summary>
        /// Submit dockets batch updating
        /// </summary>
        /// <param name="docketsModels">Dockets info models</param>
        /// <returns>Dockets batch updating model</returns>
        public DocketBatchUpdatingModel SubmitDocketsBatchUpdating(params DocketBatchInfoModel[] docketsModels)
        {
            string url = @"/V1/Docket/Update";

            foreach (DocketBatchInfoModel docket in docketsModels)
            {
                docket.contextData = "(sc.Search)";
                docket.ListPageSourceKey = string.Empty;
                docket.SearchId = string.Empty;
            }

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = docketsModels,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            return ObjectSerializer.DeserializeJsonObject<NluJsonConverter, DocketBatchUpdatingModel>(this.LastResponse.Content);
        }

        /// <summary>
        /// Submit dockets pdfs batch downloading
        /// </summary>
        /// <param name="docketPdfs">Dockets pdfs</param>
        /// <returns>Pdfs submission status model</returns>
        public PdfsSubmitionStatusModel SubmitDocketsPdfsBatchDownloading(params DocketsPdfsBatchInfoModel[] docketPdfs)
        {
            string url = @"/Docket/Pdf/BatchDownload/submit";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = docketPdfs,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            return ObjectSerializer.DeserializeJsonObject<NluJsonConverter, PdfsSubmitionStatusModel>(this.LastResponse.Content);
        }

        /// <summary>
        /// Waiting until dockets batch update successfully
        /// </summary>
        /// <param name="updatesIds">updates Ids</param>
        public void WaitForDocketBatchUpdate(List<string> updatesIds)
        {
            string url = @"/V1/Docket/BatchStatus";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = updatesIds,
                DataFormat = DataFormat.Json
            });

            this.RestClient.ExecuteUntilTrue(
                () =>
                {
                    this.LastResponse = this.RestClient.Execute(request);
                    return this.LastResponse.StatusCode == HttpStatusCode.OK
                           && !this.LastResponse.Content.Contains("PENDING");
                });
        }

        /// <summary>
        /// Waiting until dockets Pdfs batch update succesfully 
        /// </summary>
        /// <param name="requestsIds">pdfs requests ids</param>
        public void WaitForDocketPdfsBatchUpdate(List<string> requestsIds)
        {
            string url = @"/Docket/Pdf/BatchDownloadStatus";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = requestsIds,
                DataFormat = DataFormat.Json
            });

            this.RestClient.ExecuteUntilTrue(
                () =>
                {
                    this.LastResponse = this.RestClient.Execute(request);
                    PdfBatchDownloadStatusModel status =
                        ObjectSerializer.DeserializeJsonObject<NluJsonConverter, PdfBatchDownloadStatusModel>(
                            this.LastResponse.Content);
                    return this.LastResponse.StatusCode == HttpStatusCode.OK && status.Complete == requestsIds.Count;
                });
        }

        /// <summary>
        /// Set WebsitePreferences.
        /// </summary>
        /// <param name="vertical">The vertical Name. </param>
        /// <param name="preferenceName"> The preference Name. </param>
        /// <param name="preferenceValue"> The preference Value. </param>
        public void SetPreferences(VerticalName vertical, PreferenceName preferenceName, string preferenceValue)
        {
            string url = "/V1/Preference";
            string parameterValue = $"{{\"VerticalName\":\"{vertical}\",\"PreferenceName\":\"{preferenceName}\",\"PreferenceValue\":{preferenceValue}}}";
            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, timeoutBetweenRequests: 1000);

            Logger.LogInfo(
                $"Attempt to set preference - {preferenceName} : {preferenceValue}. Status: {this.LastResponse.StatusCode}");
        }

        /// <summary>
        /// Set user preferences.
        /// </summary>
        /// <param name="preferenceName"> The settings Name. </param>
        /// <param name="preferenceValue"> The settings Value. </param>
        public void SetUserSettings(PreferenceName preferenceName, string preferenceValue)
        {
            string url = "/V1/UserSettings";

            string parameterValue = $"{{\"Preferences\":{{\"{preferenceName}\":{preferenceValue}}}}}";

            var headers = new Dictionary<string, string> { { "Content-Type", "application/json" } };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, timeoutBetweenRequests: 1000);

            Logger.LogInfo(
                $"Attempt to set user setting - {preferenceName} : {preferenceValue}. Status: {this.LastResponse.StatusCode}");
        }

        /// <summary>
        /// Convert grabbed form data into dictionary id:value
        /// Gets The User Matters Model.
        /// </summary>
        /// <param name="pcid">The Page Event Identifier.</param>
        /// <param name="ajaxToken">The Ajax token.</param>
        /// <param name="requestedIdentity">The requested identity.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns>The User Matters Model.</returns>
        public UserMattersModel GetUserMattersModel(
            string pcid,
            string ajaxToken,
            string requestedIdentity,
            out string errorMessage)
        {
            string referer = $"{this.BaseUrl}/Matterroom/Matters/";
            string url =
                $"/api/v1/{requestedIdentity}/matters?startIndex=0&resultsPerPage=10&filters=status%5E%3AOpen%5E%2Cstatus%5E%3AClosed%2Cstatus%3AOpen%2Cstatus%3AClosed&sort=shortName%3Aasc";

            var headers = new Dictionary<string, string>
                              {
                                  { "Referer", referer },
                                  { "X-Cobalt-AjaxToken", ajaxToken },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.Execute(request);

            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                errorMessage = null;
                return
                    ObjectSerializer.DeserializeObject<DataContractJsonSerializer, UserMattersModel>(
                        this.LastResponse.Content);
            }

            errorMessage =
                $"Attempt to get Concourse Matters FAILED: {this.LastResponse.StatusDescription} with status code: {this.LastResponse}\n";

            return null;
        }

        /// <summary>
        /// Gets User Trash Items Model.
        /// </summary>
        /// <param name="pcid">The Page Event Identifier.</param>
        /// <param name="ajaxToken">Ajax token.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <returns> UserTrashItemsModel </returns>
        public UserTrashItemsModel GetUserTrashItemsModel(string pcid, string ajaxToken, out string errorMessage)
        {
            string url = "/api/v1/RecycleBin?startIndex=0&resultsPerPage=50&filters=type%3APROJECTS";

            var headers = new Dictionary<string, string>
                              {
                                  { "X-Cobalt-AjaxToken", ajaxToken },
                                  { "x-cobalt-exectype", "async" },
                                  { "x-cobalt-pcid",  $"{pcid ?? string.Empty}" }
                              };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = headers
            });

            this.LastResponse = this.RestClient.Execute(request);

            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                errorMessage = null;
                return
                    ObjectSerializer.DeserializeObject<DataContractJsonSerializer, UserTrashItemsModel>(
                        this.LastResponse.Content);
            }

            errorMessage =
                $"Attempt to get Concourse Matters FAILED: {this.LastResponse.StatusDescription} with status code: {this.LastResponse}\n";

            return null;
        }
    }
}