namespace Framework.Common.Api.Endpoints.CobaltServices
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;
    using System.Web.Script.Serialization;

    using Framework.Common.Api.Endpoints.CobaltServices.DataModel;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The cobalt services client.
    /// </summary>
    public class CobaltServicesClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";
        private readonly Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/jsonrequest" } };

        /// <summary>
        /// Initializes a new instance of the <see cref="CobaltServicesClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public CobaltServicesClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.CobaltServices, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(
                        CobaltModuleId.CobaltServices),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// Create Child folder under the parent folder
        /// </summary>
        /// <param name="dataroomUserId">Dataroom user id</param>
        /// <param name="parentFolderId">Parent Folder ID</param>
        /// <param name="newFolderName">Folder name</param>
        /// <param name="workProductToken">workProductToken</param>
        /// <param name="userPrizmGuid">User Prizm GUID</param>
        /// <returns> <see cref="HttpStatusCode"/>Status Code</returns>
        public HttpStatusCode CreateChildFolder(
            string dataroomUserId,
            string parentFolderId,
            string newFolderName,
            string workProductToken,
            string userPrizmGuid)
        {
            string endpointUrl = $"/DataRoom/v8/p.dataroom.{dataroomUserId}/folders";

            var requestBody = new
                                  {
                                      parent = new { parentId = parentFolderId, parentType = "folders" },
                                      folderName = newFolderName,
                                      inheritPermissions = true,
                                      isRootFolder = false
                                  };

            IRestRequest request = this.RequestBuilder.BuildRequest(
                new RequestArguments
                    {
                        Method = Method.POST,
                        DataFormat = DataFormat.Json,
                        Data = requestBody,
                        Resource = endpointUrl,
                    });

            this.SecurityHeaders.Set("x-cobalt-security-userguid", userPrizmGuid);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", workProductToken);

            return this.RestClient.Execute(request).StatusCode;
        }

        /// <summary>
        /// Delete DataRoom User Property
        /// E.g. - DocketsOrderPreferences, SendRunnerPreferences
        /// </summary>
        /// <param name="dataroomId"> The DataRoom Id. </param>
        /// <param name="propertyName"> Property Name </param>
        /// <returns>Request status code</returns>
        public HttpStatusCode DeleteDataRoomUserProperty(string dataroomId, DataRoomUserProperties propertyName)
        {
            string url = $"/DataRoom/v4/p.dataroom.{dataroomId}/settings/user/property/{propertyName}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            // HTTP 204 No Content: The server successfully processed the request, but is not returning any content (PUT / DELETE)
            return this.LastResponse.StatusCode;
        }

        /// <summary>
        /// Upload user documents
        /// </summary>
        /// <param name="dataroomId">dataroom Id</param>
        /// <param name="folderId">folder Id</param>
        /// <param name="file">file</param>
        /// <param name="fileName">file name</param>
        /// <param name="optionsName">options name</param>
        /// <param name="directUrl">direct url</param>
        /// <param name="searchUrl">search url</param>
        /// <param name="publishingProfile">publishing profile</param>
        /// <param name="workProductToken">workProductToken</param>
        /// <param name="userPrizmGuid">Prizm GUID</param>
        /// <returns>ID of the uploaded file</returns>
        public string UploadUserDocument(
            string dataroomId,
            string folderId,
            byte[] file,
            string fileName,
            string optionsName,
            string directUrl,
            string searchUrl,
            string publishingProfile,
            string workProductToken,
            string userPrizmGuid)
        {
            string url = $"/DataRoom/v15.0/p.dataroom.{dataroomId}/folders/{folderId}/items/userdocuments";
            var value = new { directUrl = directUrl, searchUrl = searchUrl, publishingProfile = publishingProfile };
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string optionsValue = serializer.Serialize(value);

            IRestRequest request =
                this.RequestBuilder.BuildRequest(new RequestArguments { Method = Method.POST, Resource = url, });

            this.SecurityHeaders.Set("x-cobalt-security-userguid", userPrizmGuid);
            request.AddHeader("Content-Type", "multipart/form-data");
            request.AddHeader("Authorization", workProductToken);
            var optionsParameter =
                new Parameter { Name = optionsName, Value = optionsValue, Type = ParameterType.GetOrPost };
            request.AddParameter(optionsParameter);
            request.AddFile("file[]", file, fileName);
            return (string)this.RestClient.Execute(request).Headers.FirstOrDefault(x => x.Name == "ID")?.Value;
        }

        /// <summary>
        /// Retrive User folders Information
        /// </summary>
        /// <param name="dataRoomId"></param>
        /// <param name="rootType"></param>
        /// <param name="rootsCreatedByCurrentUser"></param>
        /// <param name="productName"></param>
        /// <param name="workProductToken"></param>
        /// <param name="userPrizmmId"></param>
        /// <returns><see cref="UserFoldersInfo"/></returns>
        public UserFoldersInfo GetUserFoldersInformation(
            string dataRoomId,
            string rootType,
            string rootsCreatedByCurrentUser,
            string productName,
            string workProductToken,
            string userPrizmmId)
        {
            string url = $"/DataRoom/v7/p.dataroom.{dataRoomId}/folders/roots";

            IRestRequest request =
                this.RequestBuilder.BuildRequest(new RequestArguments { Method = Method.GET, Resource = url, });
            request.AddHeader("x-cobalt-security-userguid", userPrizmmId);
            request.AddHeader("Authorization", workProductToken);
            request.AddQueryParameter("rootType", rootType);
            request.AddQueryParameter("rootsCreatedByCurrentUser", rootsCreatedByCurrentUser);
            request.AddQueryParameter("productName", productName);

            return this.RestClient.Execute<UserFoldersInfo>(request).Data;
        }

        /// <summary>
        /// Retrieve user folder details
        /// </summary>
        /// <param name="dataRoomId">Dataroom id</param>
        /// <param name="parentFolderId">Container Id</param>
        /// <param name="workProductToken">Authorization</param>
        /// <param name="userPrizmmId">User prizm GUID</param>
        /// <returns><see cref="FolderDetailListInfo"/></returns>
        public FolderDetailListInfo GetUserFolderDetailsInfo(
            string dataRoomId,
            string parentFolderId,
            string workProductToken,
            string userPrizmmId)
        {

            string url = $"/DataRoom/v7/p.dataroom.{dataRoomId}/folders/{parentFolderId}/children";

            IRestRequest request =
                this.RequestBuilder.BuildRequest(new RequestArguments { Method = Method.GET, Resource = url, });
            request.AddHeader("x-cobalt-security-userguid", userPrizmmId);
            request.AddHeader("Authorization", workProductToken);

            return this.RestClient.Execute<FolderDetailListInfo>(request).Data;
        }

        /// <summary>
        /// The get contacts for user.
        /// </summary>
        /// <param name="dataroomId"> The dataroom id. </param>
        /// <returns> The <see cref="ContactsInfo"/>. </returns>
        public ContactsInfo GetContactsForUser(string dataroomId)
        {
            string url = $"/DataRoom/v14/p.dataroom.{dataroomId}/contacts";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = this.headers,
            });

            this.LastResponse = this.RestClient.Execute(request);

            ContactsInfo response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, ContactsInfo>(this.LastResponse.Content);

            return response;
        }

        /// <summary>
        /// The get externals share id.
        /// </summary>
        /// <param name="dataroomId"> The dataroom id. </param>
        /// <param name="shareToken"> The share token. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetExternalsShareId(string dataroomId, string shareToken)
        {
            string url = $"/DataRoom/v1/p.dataroom.{dataroomId}/firmusers/externalshares/{shareToken}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url,
                Headers = this.headers,
            });

            this.LastResponse = this.RestClient.Execute(request);
            ShareId desirializeToJsonResponse =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, ShareId>(this.LastResponse.Content);

            return desirializeToJsonResponse.Id;
        }

        /// <summary>
        /// The initiate folder sao share.
        /// </summary>
        /// <param name="dataroomId"> The dataroom id. </param>
        /// <param name="parameterValue"> The parameter value </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string InitiateFolderSaoShare(string dataroomId, string parameterValue)
        {
            string url = $"/DataRoom/v1/p.dataroom.{dataroomId}/firmusers/externalshares";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = this.headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);

            return
                this.LastResponse.Headers.First(
                        head => string.Equals(head.Name, "Id", StringComparison.InvariantCultureIgnoreCase))
                    .Value.ToString();
        }

        /// <summary>
        /// The initiate folder internal share.
        /// </summary>
        /// <param name="dataroomId"> The dataroom id </param>
        /// <param name="folderId"> The folder id </param>
        /// <param name="memberId"> The member id </param>
        /// <param name="parameterValue"> The parameter value </param>
        public void SetSaoFolderPermissions(string dataroomId, string folderId, string memberId, string parameterValue)
        {
            string url = $"/DataRoom/v1/p.dataroom.{dataroomId}/folders/{folderId}/permissions/{memberId}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.PUT,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            // Restsharp response for PUT call is very long (6 minutes). Using extension  method for IRestClient for execute PUT call
            this.LastResponse = this.RestClient.ExecuteCustomPut(request);
        }

        /// <summary>
        /// The share folder with internal user.
        /// </summary>
        /// <param name="dataroomId"> The dataroom id </param>
        /// <param name="folderId"> The folder id </param>
        /// <param name="contactPrismGuid"> The contact prism GUID </param>
        /// <param name="postBody"> The post body </param>
        public void ShareFolderWithInternalUser(
            string dataroomId,
            string folderId,
            string contactPrismGuid,
            string postBody)
        {
            string url = $"/DataRoom/v1/p.dataroom.{dataroomId}/folders/{folderId}/permissions/{contactPrismGuid}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.PUT,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = postBody, Type = ParameterType.RequestBody } }
            });

            // Restsharp response for PUT call is very long (6 minutes). Using extension  method for IRestClient for execute PUT call
            this.LastResponse = this.RestClient.ExecuteCustomPut(request);
        }
    }
}