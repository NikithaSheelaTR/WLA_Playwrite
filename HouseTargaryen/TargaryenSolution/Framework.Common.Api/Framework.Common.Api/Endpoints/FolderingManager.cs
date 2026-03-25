namespace Framework.Common.Api.Endpoints
{
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    using Framework.Common.Api.Endpoints.Foldering;
    using Framework.Common.Api.Endpoints.Foldering.DataModel;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;

    /// <summary>
    /// The foldering manager.
    /// </summary>
    public class FolderingManager
    {
        /// <summary>
        /// The products that support Foldering and super delete call.
        /// </summary>
        protected static readonly CobaltProductId[] FolderingAvailableProducts =
            {
                CobaltProductId.WestlawNext,
                CobaltProductId.WlnTax,
                CobaltProductId.WestlawEdge,
                CobaltProductId.WestlawPrecisionAws
            };

        /// <summary>
        /// Return true if the product has foldering functionality
        /// </summary>
        /// <param name="productId">The product id.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsFolderingAvailableFor(CobaltProductId? productId)
        {
            return productId != null && FolderingAvailableProducts.Contains(productId.Value);
        }

        /// <summary>
        /// Perform super delete for user without start session via API
        /// Start session =&gt; Perform SuperDelete =&gt; Finish session
        /// </summary>
        /// <param name="userInfo">The user.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="product">The product.</param>
        /// /// <returns> The call status<see cref="string"/></returns>
        public static string PerformSuperDelete(
            IOnePassUserInfo userInfo,
            EnvironmentInfo environment,
            CobaltProductInfo product)
        {
            if (!FolderingManager.IsFolderingAvailableFor(product?.Id))
            {
                throw new ArgumentException($"Product '{product?.Id}' is not supported", nameof(product));
            }

            var sessionManager = new CobaltSessionManager(environment, product, userInfo);

            sessionManager.StartSession();

            string sessionId = sessionManager.SessionInfo.SessionId;

            EnvironmentInfo actualTestEnvironment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    sessionManager.SessionInfo.Site,
                    environment);

            var folderingClient = ApiClientFactory.GetInstance<FolderingClient>(
                userInfo,
                sessionId,
                product,
                actualTestEnvironment);

            string responseStatus = folderingClient.SuperDelete(userInfo.UserName);

            Logger.LogInfo($"Super Delete Call: {responseStatus} ");

            // Workaround for broken shared folders. Some folders are located at the shared section, but they aren't shared 
            // Those folders should be deleted directly
            foreach (ChildFolder folderInfo in folderingClient
                                               .GetSharedFolders(userInfo.UserName)
                                               .Where(folder => !folder.IsShared))
            {
                folderingClient.DeleteFolder(userInfo.UserName, folderInfo.CategoryId, folderInfo.Label);
            }

            string rootFolderId = folderingClient.GetRootFolderInfo(userInfo.UserName).CategoryId;

            ReadOnlyCollection<Collaborator> collaborators = folderingClient.GetFolderCollaborators(
                userInfo.UserName,
                rootFolderId);

            foreach (Collaborator collaborator in collaborators.Where(
                col => !string.Equals(col.Role, "owner", StringComparison.InvariantCultureIgnoreCase)))
            {
                folderingClient.DeleteInvitationForSharingForFolder(userInfo.UserName, rootFolderId, collaborator);
            }

            sessionManager.KillSession();

            return responseStatus;
        }
    }
}