namespace Framework.Common.UI.Products.Shared.Managers.AnzNavigationManager
{
    using System;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium.Internal;

    /// <summary>
    /// AnzEdgeNavigationManager
    /// All methods use ClickLinkByText because JavascriptClick (which is used in BaseModuleRegressionDialog.ClickElement()/DriverExtensions.Click(element))
    /// works incorrect. Click on IWebElement with not full link
    /// will navigate ANZ user to Practical Law product
    /// todo replace usage of AnzEdgeNavigationManager with EdgeNavigationManager after IWebElement.Click (Selenium default click) will be used
    /// </summary>
    public class AnzEdgeNavigationManager : BaseNavigationManager
    {
        /// <summary>
        /// Link text for 'View All'
        /// </summary>
        public const string ViewAllLinkText = "View all";

        /// <summary>
        /// Link text for 'View All favourites'
        /// </summary>
        public const string ViewAllfavourites = "View all favourites";

        /// <summary>
        /// Link text for 'Alerts'
        /// </summary>
        public const string AlertsLinkText = "Alerts";

        private static AnzEdgeNavigationManager instance;

        private AnzEdgeNavigationManager()
        {
        }

        /// <summary>
        /// Returns instance instance
        /// </summary>
        /// <returns> The <see cref="AnzEdgeNavigationManager"/>. </returns>
        public static AnzEdgeNavigationManager Instance => instance ?? (instance = new AnzEdgeNavigationManager());

        /// <summary>
        /// Home Page
        /// </summary>
        private static EdgeHomePage HomePage => new EdgeHomePage();

        /// <summary>
        /// Go to Common Favorites page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToCommonFavoritesPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeFavoritesDialog>(EdgeHeaderTabs.MyLinks).ClickLinkByText<T>(ViewAllfavourites);

        /// <summary>
        /// Go to Folders page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToFoldersPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders)
                    .ClickLinkByText<T>(ViewAllLinkText);

        /// <summary>
        /// Go to History page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToHistoryPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History).ClickLinkByText<T>(ViewAllLinkText);

        /// <summary>
        ///  Go to Alerts page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToAlertsPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeNotificationsDialog>(EdgeHeaderTabs.Notifications).ClickLinkByText<T>(AlertsLinkText);

        /// <summary>
        /// 
        /// </summary>
        public override NotificationCenterPage GoToNotificationsPage() =>
            HomePage.Header.ClickHeaderTab<EdgeNotificationsDialog>(EdgeHeaderTabs.Notifications)
            .ClickLinkByText<NotificationCenterPage>(ViewAllLinkText);

        /// <summary>
        /// Navigate to community page
        /// </summary>
        public override T GoToCommunityPage<T>()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Navigate to subscription page
        /// </summary>
        public override MyPlanPage GoToSubscriptionPage()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Navigate to category page and open doc
        /// </summary>
        public EdgeCommonDocumentPage GoToCategoryPageAndOpenRandomDocFromSearchResults(
            string contentType,
            string query)
        {
            var searchResultsPage = AnzEdgeNavigationManager.Instance.GoToCategoryPage<EdgeCommonBrowsePage>(contentType)
                                                            .Header
                                                            .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(query);

            return searchResultsPage.ResultList.ClickTitleLinkByIndex<EdgeCommonDocumentPage>(
                new Random().Next(searchResultsPage.ResultList.Count() - 1));
        }


        /// <summary>
        /// Navigate to contentType page
        /// E.g. Cases or Secondary Sources>Commentary, > used as delimiter
        /// </summary>
        public T GoToCategoryPage<T>(
            string contentTypePath) where T : ICreatablePageObject
        {
            string[] path = contentTypePath.Split('>');

            var categoryPage = HomePage.BrowseTabPanel
                                       .SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes)
                                       .ClickBrowseCategory<EdgeCommonBrowsePage>(path[0]);

            foreach (string subcategory in path.Skip(1))
            {
                // The Law of Australia and A to Z of New Zealand Law are depend from compartment
                var atozOrTla = subcategory.Split('/');

                categoryPage = categoryPage.ClickCategory<EdgeCommonBrowsePage>(
                    categoryPage.Url.Contains("wlau") ? atozOrTla.First() : atozOrTla.Last());
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigate to category page
        /// </summary>
        public T GoToCategoryPage<T>(
            ContentTypeEdge contentType,
            string subcategory = "") where T : ICreatablePageObject
        {
            var categoryPage = HomePage.BrowseTabPanel
                                       .SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes)
                                       .ClickBrowseCategory<EdgeCommonBrowsePage>(contentType);

            if (!string.IsNullOrEmpty(subcategory))
            {
                categoryPage.ClickCategory<EdgeCommonBrowsePage>(subcategory);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
