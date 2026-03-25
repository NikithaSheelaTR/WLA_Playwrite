namespace Framework.Common.Api.Endpoints.Foldering
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.Foldering.DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The foldering client.
    /// </summary>
    public class FolderingClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";
        private readonly Dictionary<string, string> headers = new Dictionary<string, string> { { "Content-Type", "application/jsonrequest" } };

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderingClient"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public FolderingClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Foldering, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Foldering),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// Adds a km document to folder.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <param name="folderId"> The folder id. </param>
        /// <param name="parameterValue"> The parameter Value. </param>
        public void AddItemToFolder(string onePassUsername, string folderId, string parameterValue)
        {
            string url = $"/Foldering/v5/{onePassUsername}/folders/user/{folderId}/items?_action=add";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = this.headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Creates a folder via bulkload.
        /// </summary>
        /// <param name="onePassUsername"> The one pass password. </param>
        /// <param name="parameterValue"> The parameter Value. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string CreateFolderViaBulkload(string onePassUsername, string parameterValue)
        {
            string url = $"Foldering/v1/{onePassUsername}/bulkload";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = this.headers,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);
            string mask = string.Equals(this.LastResponse.StatusDescription, "OK") ? "{0}" : "{0} with status code: {1}";

            string responseDescription = string.Format(
                mask,
                this.LastResponse.StatusDescription,
                this.LastResponse.StatusCode);

            return responseDescription;
        }

        /// <summary>
        /// Deletes folder.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <param name="folderId"> The folder id. </param>
        /// <param name="folderName"> The folder name. </param>
        public void DeleteFolder(string onePassUsername, string folderId, string folderName)
        {
            string url = $"Foldering/v3/{onePassUsername}/folders/user/{folderId}";
            string postBody = $"_action=delete&name={folderName}";
            var additionalHeaders = new Dictionary<string, string> { { "Content-Type", "application/x-www-form-urlencoded" } };

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = additionalHeaders,
                Parameters = new List<Parameter> { new Parameter { Name = "application/x-www-form-urlencoded; charset=utf-8", Value = postBody, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Deletes invitation for sharing for folder.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <param name="folderId"> The folder id. </param>
        /// <param name="collaborator"> The collaborator. </param>
        /// <returns> The <see cref="string"/>. </returns>
        public string DeleteInvitationForSharingForFolder(string onePassUsername, string folderId, Collaborator collaborator)
        {
            string url =
                $"Foldering/v3/{onePassUsername}/folders/shared/{folderId}/collaborators/{collaborator.CollaboratorId}?includeChildren=false&isOwner=true&includeCopy=false&name={collaborator.Email}&isGroup=false&type={collaborator.Type}&role={collaborator.Role}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = this.headers
            });

            this.LastResponse = this.RestClient.Execute(request);
            return this.LastResponse.StatusDescription;
        }

        /// <summary>
        /// Deletes all files from Trash folder
        /// </summary>
        /// <param name="onePassUsername">user</param>
        public void DeleteFilesFromTrash(string onePassUsername)
        {
            string url = $"Foldering/v3/{onePassUsername}/folders/trash/items";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = this.headers
            });

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        /// Moves files with specified doc guids to Trash folder
        /// </summary>
        /// <param name="onePassUsername">user</param>
        /// <param name="folderId">folder id</param>
        /// <param name="docGuids">doc guids</param>
        public void MoveFilesToTrash(string onePassUsername, string folderId, List<string> docGuids)
        {
            string url = $"Foldering/v3/{onePassUsername}/folders/trash/items";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Headers = this.headers,
                Parameters = new List<Parameter> { new Parameter { Name = "_action", Value = "add" } }
            });

            docGuids.ForEach(guid => request.AddParameter("ids", $"{folderId}|{guid}"));

            this.LastResponse = this.RestClient.Execute(request);
        }

        /// <summary>
        ///  Gets all child folders for the target folder.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <param name="rootFolderId"> The root folder id. </param>
        /// <returns> The collection of the <see cref="ChildFolder"/>. </returns>
        public ChildFolder[] GetChildFolders(string onePassUsername, string rootFolderId)
        {
            string location = string.Equals(rootFolderId, "root", StringComparison.InvariantCultureIgnoreCase)
                                  ? "ancestors"
                                  : "children";

            string url = $"/Foldering/v3/{onePassUsername}/folders/user/{rootFolderId}/{location}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            ChildFolder[] response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, ChildFolder[]>(this.LastResponse.Content);

            return response;
        }

        /// <summary>
        /// Get Folder Document Guids
        /// </summary>
        /// <param name="onePassUserName"> onePass User Name </param>
        /// <param name="folderId"> folder Id </param>
        /// <param name="size">number of guids to return</param>
        /// <returns> Folder Documents Response </returns>
        public FolderDocumentsResponse GetFolderDocumentGuids(string onePassUserName, string folderId, int size = 50)
        {
            string url = $"/Foldering/v3/{onePassUserName}/folders/user/{folderId}/documents/guids?size={size}";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            FolderDocumentsResponse response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, FolderDocumentsResponse>(
                    this.LastResponse.Content);

            return response;
        }

        /// <summary>
        /// The get folder collaborators.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <param name="folderId"> The folder id. </param>
        /// <returns> The collection of the <see cref="Collaborator"/>. </returns>
        public ReadOnlyCollection<Collaborator> GetFolderCollaborators(string onePassUsername, string folderId)
        {
            string url = $"/Foldering/v3/{onePassUsername}/folders/shared/{folderId}/collaborators?isOwner=true";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            CollaboratorsContainer response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, CollaboratorsContainer>(
                    this.LastResponse.Content);

            return new ReadOnlyCollection<Collaborator>(response.Collaborators);
        }

        /// <summary>
        /// Gets root folder id.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <returns> The <see cref="RootFolderResponse"/>. </returns>
        public RootFolderResponse GetRootFolderInfo(string onePassUsername)
        {
            string url = $"/Foldering/v3/{onePassUsername}/folders/user/root/ancestors";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.RestClient.ExecuteUntilTrue(
                () =>
                    {
                        this.LastResponse = this.RestClient.Execute(request);
                        return this.LastResponse.StatusCode == HttpStatusCode.OK
                               && !string.IsNullOrEmpty(this.LastResponse.Content);
                    },
                5);

            RootFolderResponse[] response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, RootFolderResponse[]>(
                    this.LastResponse.Content);

            return response.First();
        }

        /// <summary>
        /// Gets all shared folders for user.
        /// </summary>
        /// <param name="onePassUsername"> The one pass username. </param>
        /// <returns> The <see cref="string"/> status. </returns>
        public ChildFolder[] GetSharedFolders(string onePassUsername)
        {
            string url = $"Foldering/v3/{onePassUsername}/folders/shared/root/ancestors";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            ChildFolder[] response =
                ObjectSerializer.DeserializeObject<DataContractJsonSerializer, ChildFolder[]>(this.LastResponse.Content);

            return response;
        }

        /// <summary>
        /// Performs super delete request for user.
        /// </summary>
        /// <param name="onePassUserName"> The one pass password. </param>
        /// <returns> The status<see cref="string"/>. </returns>
        public string SuperDelete(string onePassUserName)
        {
            string url = $"/Foldering/v1/{onePassUserName}/user";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url,
                Headers = this.headers 
            });

            //As the site by default is always set to QEDB , adding this cookie explicitly to override it.
            request.AddCookie("site", string.Format("{0}", Environment.Site.ToLower()));
            request.AddCookie("ig", string.Format("{1}_{0}_1", Environment.Site.ToLower(), Environment.Name.ToLower()));

            this.RestClient.ExecuteUntilTrue(
              () =>
              {
                  this.LastResponse = this.RestClient.Execute(request);
                  return this.LastResponse.StatusCode == HttpStatusCode.OK
                           && this.LastResponse.Content.ToLower().Contains("success");
              },
              5);

            Logger.LogInfo($"Attempt to call Super Delete, response code:{this.LastResponse.StatusCode}");

            return this.LastResponse.Content;
        }

        /// <summary>
        /// Saves favorite search query for user. If response is 200 returns ID for the saved search, otherwise throws HttpResponseException.
        /// </summary>
        /// <param name="onePassUserName"> The one pass username. </param>
        /// <param name="searchQuery"> Search query </param>
        /// <param name="statusCode"> status Code </param>
        /// <returns> string </returns>
        public string AddFavoriteSearch(string onePassUserName, string searchQuery, out HttpStatusCode statusCode)
        {
            string url = $"/Foldering/v1/{onePassUserName}/search/favorite";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = new { query = searchQuery },
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);

            statusCode = this.LastResponse.StatusCode;
            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                FavoriteSearchItem item = ObjectSerializer.DeserializeObject<DataContractJsonSerializer, FavoriteSearchItem>(
                    this.LastResponse.Content);

                return item.Id;
            }

            return string.Empty;
        }

        /// <summary>
        /// Retrieves favorite searches for user.
        /// </summary>
        /// <param name="onePassUserName"> The one pass username. </param>
        /// <param name="statusCode"> status Code </param>
        /// <returns> List of Favorite Search Items </returns>
        public List<FavoriteSearchItem> RetrieveFavoriteSearches(string onePassUserName, out HttpStatusCode statusCode)
        {
            string url = $"/Foldering/v1/{onePassUserName}/search/favorite";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.Execute(request);

            statusCode = this.LastResponse.StatusCode;
            if (this.LastResponse.StatusCode == HttpStatusCode.OK)
            {
                FavoriteSearchItemContainer item = ObjectSerializer.DeserializeObject<DataContractJsonSerializer, FavoriteSearchItemContainer>(
                    this.LastResponse.Content);

                return item.Items;
            }

            return null;
        }

        /// <summary>
        /// Deletes favorite search query by Id for user.
        /// </summary>
        /// <param name="onePassUserName"> The one pass username. </param>
        /// <param name="queryId"> Query id </param>
        public void DeleteFavoriteSearch(string onePassUserName, string queryId)
        {
            string url = $"/Foldering/v1/{onePassUserName}/search/favorite/" + queryId;

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.DELETE,
                Resource = url
            });

            this.RestClient.Execute(request);
        }

        /// <summary>
        /// Resets the stored KeyCite flag values for a given folder
        /// </summary>
        /// <param name="username">OnePass UserName</param>
        /// <param name="containerId">Folder Container ID</param>
        /// <param name="itemId">Document ID or Snippet ID </param>
        /// <param name="flagColor">Flag color</param>
        /// <param name="shared">Is folder shared</param>
        public void ResetFolderAnalysisKeyciteFlags(string username, string containerId, string itemId = "", string flagColor = "", bool shared = false)
        {
            string url = $"/Foldering/v1/{username}/folderAnalysis/keycite/flags/internal?containerId={containerId}&shared={shared}";
            string itemIdUrl = $"&itemId={itemId}";
            string flagColorUrl = $"&flagColor={flagColor}";

            if (!string.IsNullOrWhiteSpace(itemId))
            {
                url = url + itemIdUrl;
            }

            if (!string.IsNullOrWhiteSpace(flagColor))
            {
                url = url + flagColorUrl;
            }

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.PUT,
                Resource = url
            });

            this.RestClient.Execute(request);
        }

        /// <summary>
        /// Retrive Pui icons status code
        /// </summary>
        /// <param name="userGuid"></param>
        /// <param name="requestBody"></param>
        /// <returns></returns>
        public HttpStatusCode RetrivePuiIconsStatusCode(string userGuid, PuiRequest requestBody)
        {
            string url = $"/Foldering/v5/{userGuid}/icons";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = requestBody,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.Execute(request);
            return this.LastResponse.StatusCode;
        }
    }
}