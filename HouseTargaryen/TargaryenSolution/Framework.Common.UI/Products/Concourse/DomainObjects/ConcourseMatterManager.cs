namespace Framework.Common.UI.Products.Concourse.DomainObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse;
    using Framework.Common.UI.Products.Shared.DomainObjects;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Concourse Matter Manager
    /// </summary>
    public class ConcourseMatterManager : BaseUiServiceManager
    {
        /// <summary>
        /// The Ajax Token.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string ajaxToken;

        /// <summary>
        /// The Page Event Identifier.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string pcid;

        /// <summary>
        /// The Requested Identity.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string requestedIdentity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConcourseMatterManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        public ConcourseMatterManager(TestExecutionContext testExecutionContext, CobaltProductInfo product)
            : base(testExecutionContext, product, CobaltProductId.Concourse)
        {
        }

        /// <summary>
        /// Gets the session cookies.
        /// </summary>
        private CookieContainer SessionCookies { get; set; }

        /// <summary>
        /// Removes all user matters
        /// </summary>
        public void DeleteAllMatters()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            IEnumerable<Matter> allUserMatterNames =
                websiteClient.GetUserMattersModel(this.pcid, this.ajaxToken, this.requestedIdentity, out errorMessage)
                             .Matters.Where(mat => mat.Permissions.Delete)
                             .Distinct();

            this.DeleteMatters(websiteClient, allUserMatterNames, new StringBuilder(errorMessage));
        }

        /// <summary>
        /// Delete All Items In Trash
        /// </summary>
        public void DeleteAllTrashItems()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            IEnumerable<TrashItem> allUserMatterNames =
                websiteClient.GetUserTrashItemsModel(this.pcid, this.ajaxToken, out errorMessage).TrashItems;
            this.DeleteTrashItems(websiteClient, allUserMatterNames, new StringBuilder(errorMessage));
        }

        /// <summary>
        /// Removes specified Concourse Matters
        /// </summary>
        /// <param name="matters">Concourse Matters</param>
        public void DeleteMatters(IEnumerable<string> matters)
        {
            this.UpdateExecutionContext();

            if (!matters?.Any() ?? false)
            {
                return;
            }

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            var strBuilder = new StringBuilder("\n");

            Matter[] allUserMatters =
                websiteClient.GetUserMattersModel(this.pcid, this.ajaxToken, this.requestedIdentity, out errorMessage)
                             .Matters.Where(mat => mat.Permissions.Delete)
                             .ToArray();

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                strBuilder.Append(errorMessage);
            }

            IEnumerable<Matter> mattersToDelete = allUserMatters.Where(mat => matters.Contains(mat.ShortName));

            this.DeleteMatters(websiteClient, mattersToDelete, strBuilder);
        }

        /// <summary>
        /// Removes specified Trash Items
        /// </summary>
        /// <param name="trashItems">Trash Items</param>
        public void DeleteTrashItems(IEnumerable<string> trashItems)
        {
            this.UpdateExecutionContext();

            if (!trashItems?.Any() ?? false)
            {
                return;
            }

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage;
            var strBuilder = new StringBuilder("\n");

            TrashItem[] allTrashItems =
                websiteClient.GetUserTrashItemsModel(this.pcid, this.ajaxToken, out errorMessage)?.TrashItems?.ToArray();

            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                strBuilder.Append(errorMessage);
            }

            IEnumerable<TrashItem> trashItemsToDelete = allTrashItems?.Where(ti => trashItems.Contains(ti.Title));

            this.DeleteTrashItems(websiteClient, trashItemsToDelete, strBuilder);
        }

        /// <summary>
        /// Get the collection of names of concourse matters
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllMatters()
        {
            this.UpdateExecutionContext();

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage = null;

            Matter[] allUserMatters =
                websiteClient.GetUserMattersModel(this.pcid, this.ajaxToken, this.requestedIdentity, out errorMessage)?
                    .Matters.ToArray();

            if (!string.IsNullOrEmpty(errorMessage))
            {
                Logger.LogError(errorMessage);
            }

            return allUserMatters?.Select(cp => cp.ShortName) ?? new List<string>();
        }

        /// <summary>
        /// The delete matters.
        /// </summary>
        /// <param name="websiteClient"> The website client. </param>
        /// <param name="mattersToDelete"> The matters to delete. </param>
        /// <param name="strBuilder"> The string builder. </param>
        private void DeleteMatters(
            WebsiteClient websiteClient,
            IEnumerable<Matter> mattersToDelete,
            StringBuilder strBuilder)
        {
            if (mattersToDelete.Any())
            {
                foreach (Matter matter in mattersToDelete)
                {
                    strBuilder.Append(
                        $"Attempt to delete concourse matter '{matter.ShortName}' was ended with status: {websiteClient.DeleteConcourseMatter(this.pcid, this.ajaxToken, matter.Id)}\n");
                }
            }

            if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
            {
                Logger.LogInfo(string.Empty);
                Logger.LogInfo("Concourse matters deleting:\n" + strBuilder);
            }
        }

        private void DeleteTrashItems(
            WebsiteClient websiteClient,
            IEnumerable<TrashItem> trashItemsToDelete,
            StringBuilder strBuilder)
        {
            if (trashItemsToDelete.Any())
            {
                foreach (TrashItem trashItem in trashItemsToDelete)
                {
                    strBuilder.Append(
                        $"Attempt to delete concourse trash item '{trashItem.Title}' was ended with status: "
                        + $"{websiteClient.DeleteConcourseTrashItem(this.pcid, this.ajaxToken, new { resourceId = trashItem.ResourceId, resourceType = trashItem.ResourceType, resourceTypeName = trashItem.ResourceTypeName, trashId = trashItem.Id })}\n");
                }
            }

            if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
            {
                Logger.LogInfo(string.Empty);
                Logger.LogInfo("Concourse trash items deleting:\n" + strBuilder);
            }
        }

        /// <summary>
        /// Refresh the session cookies
        /// </summary>
        private void UpdateExecutionContext()
        {
            this.SessionCookies = BrowserPool.CurrentBrowser.GetCookies().GetCookieContainerFromCookies();
            string pageSource = DriverExtensions.WaitForElement(By.TagName("html")).GetAttribute("innerHTML");
            this.pcid = SecurityHeadersUtils.GetPcid(pageSource);
            this.ajaxToken = SecurityHeadersUtils.GetAjaxToken(pageSource);
            this.requestedIdentity = SecurityHeadersUtils.GetRequestedIdentity(pageSource);
        }
    }
}