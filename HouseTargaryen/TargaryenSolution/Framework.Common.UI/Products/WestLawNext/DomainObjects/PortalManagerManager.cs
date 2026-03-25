namespace Framework.Common.UI.Products.WestLawNext.DomainObjects
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text;

    using Framework.Common.Api.Endpoints;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Endpoints.Website.DataModel;
    using Framework.Common.Api.Raw.Foldering.Utilities;
    using Framework.Common.UI.Products.Shared.DomainObjects;
    using Framework.Common.UI.Products.Shared.Enums.Search;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// The PortalManager Manager
    /// </summary>
    public class PortalManagerManager : BaseUiServiceManager
    {
        /// <summary>
        /// The Page Event Identifier.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string pcid;

        /// <summary>
        /// The Parent Transaction Identifier.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string ptid;

        /// <summary>
        /// Initializes a new instance of the <see cref="PortalManagerManager"/> class.
        /// </summary>
        /// <param name="testExecutionContext">The test execution context.</param>
        /// <param name="product">The product.</param>
        public PortalManagerManager(TestExecutionContext testExecutionContext, CobaltProductInfo product)
            : base(testExecutionContext, product, CobaltProductId.WestlawNext)
        {
        }

        /// <summary>
        /// Gets the session cookies.
        /// </summary>
        public CookieContainer SessionCookies { get; private set; }

        /// <summary>
        /// Deletes set PortalManager items
        /// </summary>
        /// <param name="portalManagerItems">Portal Manager Item Names</param>
        public void DeletePortalManagerItems(
            IEnumerable<KeyValuePair<PortalManagerTabs, IEnumerable<string>>> portalManagerItems)
        {
            this.UpdateExecutionContext();

            if (!portalManagerItems?.Select(x => x.Value.Any()).Any(cur => cur) ?? false)
            {
                return;
            }

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            var strBuilder = new StringBuilder();

            foreach (var portal in portalManagerItems)
            {
                if (portal.Value.Any())
                {
                    string errorMessage = null;
                    string portalManagerModuleType =
                        EnumPropertyModelCache.GetMap<PortalManagerTabs, WebElementInfo>()[portal.Key].Id;

                    var postBody = new Dictionary<string, string> { { "tabName", portalManagerModuleType } };
                    PortalManagerItemModel[] portalManagerAllItems = websiteClient.GetAllPortalManagerItems(
                        this.pcid,
                        this.ptid,
                        out errorMessage,
                        postBody.ToJson());

                    strBuilder.Append(errorMessage ?? string.Empty);

                    IEnumerable<PortalManagerItemModel> portalManagerItemsToDelete =
                        portalManagerAllItems?.Where(
                            portalManagerItem => portal.Value?.Contains(portalManagerItem.Name) ?? false);

                    if (portalManagerItemsToDelete.Any())
                    {
                        strBuilder.Append($"Portal Manager '{portal.Key}' Items deleting:\n");
                        foreach (PortalManagerItemModel portalManagerItem in portalManagerItemsToDelete)
                        {
                            strBuilder.Append(
                                $"Attempt to delete portal manager item '{portalManagerItem.Name}' "
                                + $"(guid = {portalManagerItem.Guid}) was ended with status: "
                                + $"{websiteClient.DeletePortalManagerItemByGuid(this.pcid, this.ptid, portalManagerItem)}\n");
                        }
                    }
                }
            }

            if (!string.IsNullOrWhiteSpace(strBuilder.ToString()))
            {
                Logger.LogInfo(string.Empty);
                Logger.LogInfo("Portal Manager items deleting:\n" + strBuilder);
            }
        }

        /// <summary>
        /// Get the collection of names of custom pages
        /// </summary>
        /// <param name="portalManagerTab">The Portal Manager tab.</param>
        /// <returns>The collection of names of Portal Manager items of specified type.</returns>
        public IEnumerable<string> GetListOfPortalManagerItemsNames(PortalManagerTabs portalManagerTab)
        {
            this.UpdateExecutionContext();

            var portalManagerTabsMap = EnumPropertyModelCache.GetMap<PortalManagerTabs, WebElementInfo>();
            var postBody = new Dictionary<string, string> { { "tabName", portalManagerTabsMap[portalManagerTab].Id } };

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                null,
                this.product,
                this.environment,
                this.SessionCookies);

            string errorMessage = null;

            PortalManagerItemModel[] resultCol = websiteClient.GetAllPortalManagerItems(
                this.pcid,
                this.ptid,
                out errorMessage,
                postBody.ToJson());
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                Logger.LogError(errorMessage);
            }

            return resultCol?.Select(portalManagerItem => portalManagerItem.Name) ?? new List<string>();
        }

        /// <summary>
        /// Update current session cookies
        /// </summary>
        private void UpdateExecutionContext()
        {
            this.SessionCookies = BrowserPool.CurrentBrowser.GetCookies().GetCookieContainerFromCookies();
            this.pcid =
                SecurityHeadersUtils.GetPcid(
                    DriverExtensions.WaitForElement(By.TagName("html")).GetAttribute("innerHTML"));
            this.ptid = SecurityHeadersUtils.GetPtid();
        }
    }
}