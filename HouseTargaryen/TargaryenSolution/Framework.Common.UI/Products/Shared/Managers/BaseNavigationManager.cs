namespace Framework.Common.UI.Products.Shared.Managers
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums.Browses;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Base navigation Manager class
    /// </summary>
    public abstract class BaseNavigationManager
    {
        private EnumPropertyMapper<BrowsePages, WebElementInfo> browsePageMap;

        /// <summary>
        /// DateRangeOptionsMap
        /// </summary>
        private EnumPropertyMapper<BrowsePages, WebElementInfo> BrowsePageMap
            => this.browsePageMap = this.browsePageMap ?? EnumPropertyModelCache.GetMap<BrowsePages, WebElementInfo>();

        /// <summary>
        /// Navigates user to BrowsePage Directly
        /// </summary>
        /// <param name="page">Enum value</param>
        /// <typeparam name="T">Page</typeparam>
        /// <returns>Page instance</returns>
        public T NavigateToBrowsePageDirectly<T>(BrowsePages page) where T : IBrowseCategoryPage
        {
            string domain = BrowserPool.CurrentBrowser.Url.Substring(
                0,
                BrowserPool.CurrentBrowser.Url.IndexOf("/", 9, StringComparison.Ordinal));
            string url = domain
                         + $"/Browse/Home/{this.BrowsePageMap[page].Text}?transitionType=Default&contextData=(sc.Default)";
            BrowserPool.CurrentBrowser.GoToUrl(url);
            Logger.LogInfo("User was navigated to " + url);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigates user to Browse Advanced Search Page Directly
        /// </summary>
        /// <param name="page">Enum value</param>
        /// <typeparam name="T">Page</typeparam>
        /// <returns>Page instance</returns>
        public T NavigateToBrowseAdvancedSearchPageDirectly<T>(BrowsePages page) where T : IAdvancedSearchPage
        {
            string domain = BrowserPool.CurrentBrowser.Url.Substring(
                0,
                BrowserPool.CurrentBrowser.Url.IndexOf("/", 9, StringComparison.Ordinal));
            string url = domain
                         + $"/Search/AdvancedSearchPage.html?originUrlPath=%2FBrowse%2FHome%2F{this.BrowsePageMap[page].Text.Replace("/", "%2F")}&categoryPageUrl=Home%2F{this.BrowsePageMap[page].Text.Replace("/", "%2F")}&contextData=(sc.Default)&transitionType=Default&contentType={this.BrowsePageMap[page].Text.Replace("/", "%2F")}";
            BrowserPool.CurrentBrowser.GoToUrl(url);
            Logger.LogInfo("User was navigated to " + url);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The navigate to document directly.
        /// </summary>
        /// <typeparam name="T">
        /// Page
        /// </typeparam>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <param name="isKm">
        /// km document.
        /// </param>
        /// <returns>Page instance</returns>
        public T NavigateToDocumentDirectly<T>(string guid, bool isKm = false) where T : ICommonDocumentPage
        {
            string domain = BrowserPool.CurrentBrowser.Url.Substring(
                0,
                BrowserPool.CurrentBrowser.Url.IndexOf("/", 9, StringComparison.Ordinal));
            string docType = isKm ? "KMDocument" : "Document";
            BrowserPool.CurrentBrowser.GoToUrl($"{domain}/{docType}/{guid}/View/FullText.html?transitionType=Default&contextData=(sc.Default)");
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Opens an invalid WestlawNext document directly using WebDriver's GoToUrl 
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="guid"> Document GUID </param>
        /// <returns> Error page object </returns>
        public T NavigateToInvalidDocumentDirectly<T>(string guid) where T : INotFindDocument
        {
            this.NavigateToDocumentDirectly<CommonDocumentPage>(guid);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary> Opens a WestlawNext out of plan document directly using WebDriver's GoToUrl </summary>
        /// <param name="guid"> document GUID </param>
        /// <param name="isKm">km document</param>
        /// <returns> DocumentPage object </returns>
        public OutOfPlanDialog NavigateToOutOfPlanDocumentDirectly(string guid, bool isKm = false)
        {
            this.NavigateToDocumentDirectly<CommonDocumentPage>(guid, isKm);
            return new OutOfPlanDialog();
        }

        /// <summary>
        /// GoTo Community page
        /// </summary>
        /// <typeparam name="T">A type that implements ICommunityPage</typeparam>
        /// <returns>New instance of T</returns>
        public abstract T GoToCommunityPage<T>() where T : ICommunityPage;

        /// <summary>
        /// Go to Alerts page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of T page </returns>
        public abstract T GoToAlertsPage<T>() where T : IAlertCenterPage;

        /// <summary>
        /// Go to Common Favorites page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>The New instance of T page.</returns>
        public abstract T GoToCommonFavoritesPage<T>() where T : ICommonFavoritesPage;

        /// <summary>
        /// Go to Folders page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>The New instance of T page.</returns>
        public abstract T GoToFoldersPage<T>() where T : IResearchOrganizerPage;

        /// <summary>
        /// Go to History page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The New instance of T page. </returns>
        public abstract T GoToHistoryPage<T>() where T : ICommonHistoryPage;

        /// <summary>
        /// Go to Notification page
        /// </summary>
        /// <returns> The New instance of T page </returns>
        public abstract NotificationCenterPage GoToNotificationsPage();

        /// <summary>
        /// Go to Subscription Page
        /// </summary>
        /// <returns> The <see cref="MyPlanPage"/>. </returns>
        public abstract MyPlanPage GoToSubscriptionPage();
    }
}