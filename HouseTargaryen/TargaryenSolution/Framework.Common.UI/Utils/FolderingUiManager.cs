namespace Framework.Common.UI.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Endpoints.CobaltServices;
    using Framework.Common.Api.Endpoints.Foldering;
    using Framework.Common.Api.Endpoints.Foldering.DataModel;
    using Framework.Common.Api.Endpoints.Uds;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Raw.Foldering.Utilities;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Net;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Extensions;

    /// <summary>
    /// Represents the Research Organizer Manager
    /// </summary>
    public class FolderingUiManager : FolderingManager
    {
        /// <summary>
        /// The environment.
        /// </summary>
        private readonly EnvironmentInfo environment;

        /// <summary>
        /// The product.
        /// </summary>
        private readonly CobaltProductInfo product;

        /// <summary>
        /// The actual test environment.
        /// </summary>
        private EnvironmentInfo actualTestEnvironment;

        /// <summary>
        /// The current session id.
        /// </summary>
        private string currentSessionId;

        /// <summary>
        /// Last user with updated session
        /// </summary>
        private IOnePassUserInfo lastUserUpdated;

        /// <summary>
        /// Session is updated
        /// </summary>
        private bool sessionUpdated;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderingUiManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        public FolderingUiManager(TestExecutionContext testExecutionContext, CobaltProductInfo product)
        {
            if (!FolderingManager.IsFolderingAvailableFor(product?.Id))
            {
                throw new ArgumentException(
                    $"Product '{product?.Id.ToString() ?? "NULL"}' is not supported",
                    nameof(product));
            }

            this.product = product;
            this.sessionUpdated = false;
            this.environment = testExecutionContext.TestEnvironment;
        }

        /// <summary>
        /// The add km to folder using add to folder.
        /// </summary>
        /// <param name="folderName">
        /// The folder name.
        /// </param>
        /// <param name="docGuid">
        /// The doc GUID.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="user">
        /// The user.
        /// </param>
        public void AddDocToFolderUsingAddToFolder(
            string folderName,
            string docGuid,
            string type,
            IOnePassUserInfo user)
        {
            this.UpdateCurrentCallExecutionContext(user);

            string folderId = this.GetFolderId(folderName, user);
            var body = new Dictionary<string, object>();
            var itemJson = new Dictionary<string, object>
                               {
                                   { "documentGuid", docGuid },
                                   { "metadata", new Dictionary<string, string>() },
                                   { "originalContext", "Search" },
                                   { "type", type },
                                   { "novusSearchHandle", user.PrismGuid }
                               };

            var itemArray = new List<Dictionary<string, object>> { itemJson };

            body.Add("items", itemArray);
            body.Add("sourceWidget", "saveTo");

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            folderingClient.AddItemToFolder(user.UserName, folderId, body.ToJson());
        }

        /// <summary>
        /// Adds search query to favorites 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="searchQuery"></param>
        public void AddSearchToFavorites(IOnePassUserInfo user, string searchQuery)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            HttpStatusCode statusCode;

            folderingClient.AddFavoriteSearch(user.UserName, searchQuery, out statusCode);
        }

        /// <summary>
        /// The create folder via API calls.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        /// <param name="user">The info about owner of newly created folder.</param>
        /// <param name="numberDocs">The number docs.</param>
        public void CreateFolderViaBulkload(string folderName, IOnePassUserInfo user, int numberDocs = 0)
        {
            string folderPath = "/" + folderName;
            this.UpdateCurrentCallExecutionContext(user);

            var body = new Dictionary<string, string>
                           {
                               { "count", numberDocs.ToString() },
                               { "folderPath", folderPath },
                               { "folderType", "user" },
                               { "start", 11.ToString() }
                           };

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            string responseStatus = folderingClient.CreateFolderViaBulkload(user.UserName, body.ToJson());

            Logger.LogInfo(
                $"Attempt to create folder '{folderName}' via bulkload is ended with status: {responseStatus}");
        }

        /// <summary>
        /// The create shared folder with pending Shared across Organizations users.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        /// <param name="numberDocs">The number docs.</param>
        /// <param name="user">The info about owner of newly created folder.</param>
        /// <param name="dictCollabEmailRoles">The dictionary of collaborator email roles.</param>
        /// <returns>The array of tokens.</returns>
        public string[] CreateSharedAcrossOrganizationsFolder(
            string folderName,
            int numberDocs,
            IOnePassUserInfo user,
            Dictionary<string, SharingRoles> dictCollabEmailRoles)
        {
            // Create Folder
            this.CreateFolderViaBulkload(folderName, user, numberDocs);

            string encryptedId = this.GetFolderId(folderName, user);

            // Share folder with users
            string[] tokens =
                dictCollabEmailRoles.Select(
                    email =>
                        this.ShareFolderAcrossOrganizations(
                            user,
                            encryptedId,
                            email.Value,
                            this.currentSessionId)).ToArray();
            return tokens;
        }

        /// <summary>
        /// The create shared with internal users folder.
        /// </summary>
        /// <param name="folderName">The folder name.</param>
        /// <param name="numberDocs">The number docs.</param>
        /// <param name="user">The info about owner of newly created folder.</param>
        /// <param name="dictCollabRoles">The dictionary of collaborator roles.</param>
        /// <returns>The share token, this will be need for accepting/denying URLs <see cref="string"/>.</returns>
        public string CreateSharedInternalFolder(
            string folderName,
            int numberDocs,
            IOnePassUserInfo user,
            Dictionary<WlnUserInfo, SharingRoles> dictCollabRoles)
        {
            var response = new StringBuilder();

            // Create Folder
            this.CreateFolderViaBulkload(folderName, user, numberDocs);

            string encryptedId = this.GetFolderId(folderName, user);

            // Share folder with users
            foreach (KeyValuePair<WlnUserInfo, SharingRoles> collaborator in dictCollabRoles)
            {
                this.ShareFolderWithInternalUser(user, this.currentSessionId, encryptedId, collaborator);
            }

            // Return share token, this will be need for accepting/denying URLs
            return response.ToString();
        }

        /// <summary>
        /// Deletes folder
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="user">User info</param>
        public void DeleteFolder(string folderName, IOnePassUserInfo user)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            string encryptedId = this.GetFolderId(folderName, user);

            folderingClient.DeleteFolder(user.UserName, encryptedId, folderName);
        }

        /// <summary>
        /// Deletes files from folder (removes from Trash as well)
        /// </summary>
        /// <param name="user"><see cref="IOnePassUserInfo"/></param>
        /// <param name="folderName">foler name</param>
        public void DeleteDocumentsFromFolder(IOnePassUserInfo user, string folderName)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            string folderId = this.GetFolderId(folderName, user);
            List<string> docGuids = folderingClient.GetFolderDocumentGuids(user.UserName, folderId).DocumentGuids;

            folderingClient.MoveFilesToTrash(user.UserName, folderId, docGuids);
            folderingClient.DeleteFilesFromTrash(user.UserName);
        }

        /// <summary>
        /// Stop share folder
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="user">User info</param>
        public void StopShareFolder(string folderName, IOnePassUserInfo user)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            string encryptedId = this.GetFolderId(folderName, user);

            var colloborators = folderingClient.GetFolderCollaborators(user.UserName, encryptedId).Where(x => !x.FirstName.Equals(user.UserName));

            foreach (Collaborator collaborator in colloborators)
            {
                folderingClient.DeleteInvitationForSharingForFolder(user.UserName, encryptedId, collaborator);
            }

            folderingClient.DeleteFolder(user.UserName, encryptedId, folderName);
        }


        /// <summary>
        /// The get root folder name.
        /// </summary>
        /// <param name="userInfo">The user.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetRootFolderName(IOnePassUserInfo userInfo)
        {
            return this.GetRootFolderInfo(userInfo).Label;
        }

        /// <summary>
        /// The get root folder id.
        /// </summary>
        /// <param name="rootFolderId">The root Folder Id.</param>
        /// <param name="folderName">The folder Name.</param>
        /// <param name="user">The user.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetChildId(string rootFolderId, string folderName, IOnePassUserInfo user)
        {
            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            ChildFolder[] childFolders = folderingClient.GetChildFolders(user.UserName, rootFolderId);

            List<string> id =
                childFolders.Where(
                                folder =>
                                    string.Equals(folder.Label, folderName, StringComparison.CurrentCultureIgnoreCase))
                            .Select(folder => folder.CategoryId)
                            .ToList();

            return id.FirstOrDefault();
        }

        /// <summary>
        /// The get data room work product token.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="sessionId">The session id.</param>
        /// <returns>The <see cref="WorkProductTokenResponse"/>.</returns>
        private WorkProductTokenResponse GetDataRoomWorkProductToken(IOnePassUserInfo user, string sessionId)
        {
            var udsServicesClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                user,
                sessionId,
                this.product,
                this.actualTestEnvironment);

            return udsServicesClient.GetWorkProductToken(sessionId /*, site*/);
        }

        /// <summary>
        /// The get folder id via web request.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="user">The user.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetFolderId(string path, IOnePassUserInfo user)
        {
            string[] folderStructure = path.Split('/');
            RootFolderResponse rootFolder = this.GetRootFolderInfo(user);

            if (folderStructure.Length == 1 && rootFolder.Label.Equals(folderStructure.First())) return rootFolder.CategoryId;

            string rootId = rootFolder.CategoryId;
            string currentFolderId = rootId;

            foreach (string folderName in folderStructure)
            {
                string folderId = this.GetChildId(currentFolderId, folderName, user);

                if (string.IsNullOrEmpty(folderId))
                {
                    throw new ArgumentException("The folder does not exist on this address");
                }

                currentFolderId = folderId;
            }

            return currentFolderId;
        }

        /// <summary>
        /// The get shared folder id via web request.
        /// </summary>
        /// <param name="folderName">The folderName.</param>
        /// <param name="user">The user.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string GetSharedFolderId(string folderName, IOnePassUserInfo user)
        {
            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            List<string> listId = folderingClient.GetSharedFolders(user.UserName).Where(folder =>
                                           string.Equals(folder.Label, folderName, StringComparison.CurrentCultureIgnoreCase))
                                   .Select(folder => folder.CategoryId)
                                   .ToList();

            return listId.FirstOrDefault();
        }

        private RootFolderResponse GetRootFolderInfo(IOnePassUserInfo user)
        {
            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            RootFolderResponse rootFolderInfo = folderingClient.GetRootFolderInfo(user.UserName);

            return rootFolderInfo;
        }

        /// <summary>
        /// Deletes all favorite searches for user
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="HttpResponseException"></exception>
        public void DeleteAllFavoriteSearches(IOnePassUserInfo user)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                user,
                this.currentSessionId,
                this.product,
                this.actualTestEnvironment);

            HttpStatusCode statusCode;

            List<FavoriteSearchItem> items = folderingClient.RetrieveFavoriteSearches(user.UserName, out statusCode);

            if (!statusCode.Equals(HttpStatusCode.OK))
            {
                throw new HttpResponseException(statusCode, $"Retrieve favorite searches endpoint returns {statusCode}");
            }

            foreach (FavoriteSearchItem item in items)
            {
                folderingClient.DeleteFavoriteSearch(user.UserName, item.Id);
            }
        }

        /// <summary>
        /// Resets the stored KeyCite flag values for a given folder
        /// </summary>
        /// <param name="user"></param>
        /// <param name="folderName"></param>
        /// <param name="flag"></param>
        /// <param name="docGuid"></param>
        /// <param name="shared"></param>
        public void ResetFolderAnalysisKeyciteFlags(IOnePassUserInfo user, string folderName, KeyCiteFlag flag, string docGuid = "", bool shared = false)
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
               user,
               this.currentSessionId,
               this.product,
               this.actualTestEnvironment);

            string folderId = this.GetFolderId(folderName, user);

            folderingClient.ResetFolderAnalysisKeyciteFlags(user.UserName, folderId, docGuid, flag.GetEnumTextValue(), shared);
        }

        /// <summary>
        /// Resets the stored KeyCite flag values for a given shared folder
        /// </summary>
        /// <param name="user"></param>
        /// <param name="folderName"></param>
        /// <param name="flag"></param>
        /// <param name="docGuid"></param>
        public void ResetFolderAnalysisKeyciteFlagsForSharedFolder(IOnePassUserInfo user, string folderName, KeyCiteFlag flag, string docGuid = "")
        {
            this.UpdateCurrentCallExecutionContext(user);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
               user,
               this.currentSessionId,
               this.product,
               this.actualTestEnvironment);

            string folderId = this.GetSharedFolderId(folderName, user);

            folderingClient.ResetFolderAnalysisKeyciteFlags(user.UserName, folderId, docGuid, flag.GetEnumTextValue(), true);
        }

        /// <summary>
        /// The share folder across organizations.
        /// </summary>
        /// <param name="owner">The owner.</param>
        /// <param name="encryptedFolderId">The encrypted folder id.</param>
        /// <param name="role">The role.</param>
        /// <param name="sessionId">The session id.</param>
        /// <returns>The <see cref="string"/>.</returns>
        private string ShareFolderAcrossOrganizations(
            IOnePassUserInfo owner,
            string encryptedFolderId,
            SharingRoles role,
            string sessionId)
        {
            var body = new Dictionary<string, object>
                           {
                               {
                                   "initiatorEmailAddress",
                                   "initiatorEmail"
                                   + StringExtensions.GenerateRandomAlphaNumericString()
                                   + "@thomsonreuters.com"
                               },
                               {
                                   "recipientEmailAddress",
                                   "inviteeEmail"
                                   + StringExtensions.GenerateRandomAlphaNumericString()
                                   + "@gmail.com"
                               }
                           };

            // Retrieve the work product token and the DataRoom user ID 
            WorkProductTokenResponse workProductTokenjson = this.GetDataRoomWorkProductToken(owner, sessionId);

            string workProductToken = workProductTokenjson.WorkProductToken;
            string dataroomId = workProductTokenjson.TokenHolderUserId;

            // Set header with workProductToken
            NameValueCollection securityHeader = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.CobaltServices,
                owner,
                this.actualTestEnvironment,
                this.product,
                sessionId,
                workProductToken);

            var cobaltServicesClient = ApiClientFactory.GetInstance<CobaltServicesClient>(
                securityHeader,
                this.product,
                this.actualTestEnvironment);

            // Retrieve share token, this will be needed for accepting/denying URLs
            string token = cobaltServicesClient.InitiateFolderSaoShare(dataroomId, body.ToJson());

            // Retrieve member Id 
            string externalsShareId = cobaltServicesClient.GetExternalsShareId(dataroomId, token);

            // Initialize Pending Share 
            body = new Dictionary<string, object>
                       {
                           { "permissionRole", role.GetEnumTextValue().ToUpper() },
                           { "memberType", "PENDING_SHARE" }
                       };

            cobaltServicesClient.SetSaoFolderPermissions(dataroomId, encryptedFolderId, externalsShareId, body.ToJson());

            // Return share token, this will be needed for accepting/denying URLs
            return token;
        }

        /// <summary>
        /// The share folder with internal user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="sessionId">The session id.</param>
        /// <param name="folderId">The folder id.</param>
        /// <param name="collaboratorInfo">The collaborator.</param>
        private void ShareFolderWithInternalUser(
            IOnePassUserInfo user,
            string sessionId,
            string folderId,
            KeyValuePair<WlnUserInfo, SharingRoles> collaboratorInfo)
        {
            // Build request json 
            var body = new Dictionary<string, object>
                           {
                               { "permissionRole", collaboratorInfo.Value.GetEnumTextValue().ToUpper() },
                               { "memberType", "USER" }
                           };

            // Get work product Token
            WorkProductTokenResponse workProductTokenjson = this.GetDataRoomWorkProductToken(user, sessionId);

            string workProductToken = workProductTokenjson.WorkProductToken;
            string dataroomId = workProductTokenjson.TokenHolderUserId;

            NameValueCollection securityHeader = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.CobaltServices,
                user,
                this.actualTestEnvironment,
                this.product,
                sessionId,
                workProductToken);

            var cobaltServicesClient = ApiClientFactory.GetInstance<CobaltServicesClient>(
                securityHeader,
                this.product,
                this.actualTestEnvironment);

            ContactsInfo contacts = cobaltServicesClient.GetContactsForUser(dataroomId);
            string contactPrismguid =
                contacts.Contacts.Where(
                            contact =>
                                string.Equals(
                                    contact.FirstName,
                                    collaboratorInfo.Key.FirstName,
                                    StringComparison.InvariantCultureIgnoreCase)
                                && string.Equals(
                                    contact.LastName,
                                    collaboratorInfo.Key.LastName,
                                    StringComparison.InvariantCultureIgnoreCase))
                        .Select(contact => contact.UserInformation.UserGuid)
                        .FirstOrDefault();

            // Send PUT request and return response
            cobaltServicesClient.ShareFolderWithInternalUser(dataroomId, folderId, contactPrismguid, body.ToJson());
        }

        /// <summary>
        /// The get session id for user.
        /// </summary>
        /// <param name="user">The user.</param>
        private void UpdateCurrentCallExecutionContext(IOnePassUserInfo user)
        {

            if (this.sessionUpdated && user == this.lastUserUpdated)
                return;
            this.lastUserUpdated = user;
            this.actualTestEnvironment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    DriverExtensions.GetSiteCookieValue(),
                    this.environment);

            this.currentSessionId = new CobaltSessionManager(this.actualTestEnvironment, this.product, user)
                                    .GetCurrentUiSessionInfo(DriverExtensions.GetCookieValue("Co_SessionToken")).SessionId;
            this.sessionUpdated = true;
        }

    }
}