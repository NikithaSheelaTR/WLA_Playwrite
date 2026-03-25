namespace Framework.Common.UI.Products.Shared.Managers
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Pages.MedicalLitigator;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.BrowseComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.ResultPages;
    using ContentTypesTabPanel = WestlawEdge.Components.BrowseWidget.ContentTypesTabPanel;

    /// <summary>
    /// Edge Navigation Manager
    /// </summary>
    public class EdgeNavigationManager : BaseNavigationManager
    {
        private const string SecondarySources = "Secondary Sources";
        private const string ViewCommunityLink = "View Community";
        private const string CommunityPageTitle = "https://auth.thomsonreuters.com/u/login?...";

        private static EdgeNavigationManager instance;

        private EdgeNavigationManager()
        {
        }

        /// <summary>
        /// Returns instance instance
        /// </summary>
        /// <returns> The <see cref="EdgeNavigationManager"/>. </returns>
        public static EdgeNavigationManager Instance => instance ?? (instance = new EdgeNavigationManager());

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
            HomePage.Header.ClickHeaderTab<EdgeFavoritesDialog>(EdgeHeaderTabs.MyLinks).FavoritesViewAllLink.Click<T>();

        /// <summary>
        /// Go to Folders page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToFoldersPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders).ViewAllLink.Click<T>();

        /// <summary>
        /// Go to History page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns> Page instance </returns>
        public override T GoToHistoryPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History).ViewAllLink.Click<T>();

        /// <summary>
        ///  Go to Alerts page
        /// </summary>
        /// <typeparam name="T"> Page object </typeparam>
        /// <returns>Page instance</returns>
        public override T GoToAlertsPage<T>() =>
            HomePage.Header.ClickHeaderTab<EdgeNotificationsDialog>(EdgeHeaderTabs.Notifications).ClickViewAlertsLink<T>();

        /// <summary>
        /// Go to community page
        /// </summary>
        /// <typeparam name="T">An instance that implements ICommunityPage</typeparam>
        /// <returns>New instance of T</returns>
        public override T GoToCommunityPage<T>() =>
            HomePage.Header.ClickHeaderTab<CommunityDialog>(EdgeHeaderTabs.Community)
                    .ClickLinkAndOpenNewBrowserTab<T>(ViewCommunityLink, CommunityPageTitle);

        /// <summary>
        /// Go to Notifications page
        /// </summary>
        /// <returns> The <see cref="NotificationCenterPage"/>. </returns>
        public override NotificationCenterPage GoToNotificationsPage() =>
            HomePage.Header.ClickHeaderTab<EdgeNotificationsDialog>(EdgeHeaderTabs.Notifications).ViewAllLink
                    .Click<NotificationCenterPage>();

        /// <summary>
        /// Go to Users Subscription page
        /// </summary>
        /// <returns> The <see cref="MyPlanPage"/>. </returns>
        public override MyPlanPage GoToSubscriptionPage() => HomePage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSubscriptionLink();

        /// <summary>
        /// Go To Secondary Sources Page
        /// </summary>
        /// <returns> EdgeSecondarySourcesCategoryPage </returns>
        public EdgeSecondarySourcesCategoryPage GoToSecondarySourcesPage() =>
            HomePage.BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(BrowseTab.ContentTypes).ClickBrowseCategory<EdgeSecondarySourcesCategoryPage>(SecondarySources)
                    .ToolsAndResourcesComponent.ClickLink<EdgeSecondarySourcesCategoryPage>(ToolsLink.FullSecondarySourcesLibrary);

        /// <summary>
        /// Navigate To Medical Litigator Page
        /// </summary>
        /// <returns> EdgeMedLitCategoryPage </returns>
        public MedLitCategoryPage GoToMedicalLitigatorPage() => HomePage.BrowseTabPanel.SetActiveTab<PracticeAreasTabPanel>(BrowseTab.PracticeAreas)
                .ClickBrowseCategory<MedLitCategoryPage>(PracticeArea.MedicalLitigation);

        /// <summary>
        /// Get Quick Check landing page
        /// </summary>
        /// <typeparam name="T">Quick check landing page</typeparam>
        /// <returns>quick check landing page</returns>
        public T GoToQuickCheckPage<T>()
            where T : ICreatablePageObject =>
            HomePage.QuickCheckLink.Click<T>();

        /// <summary>
        /// Navigate to Selected History Page 
        /// </summary>
        /// <param name="typeOfHistory">History type like searches, documents, etc</param>
        /// <returns> The <see cref="EdgeCommonHistoryPage"/>. </returns>
        public EdgeCommonHistoryPage GoToSelectedHistoryPage(string typeOfHistory) =>
            this.GoToPageFromViewThisHistoryButton<EdgeCommonHistoryPage>(EdgeHeaderTabs.History, typeOfHistory);

        /// <summary>
        /// Navigate to Selected History Page 
        /// </summary>
        /// <typeparam name="T"> Page instance</typeparam>
        /// <param name="tab">Edge Header tab</param>
        /// <param name="typeOfHistory">History type like searches, documents, etc</param>
        /// <returns> Page of type T </returns>
        private T GoToPageFromViewThisHistoryButton<T>(EdgeHeaderTabs tab, string typeOfHistory) where T : ICreatablePageObject
        {
            var edgeRecentHistoryDialog =
                HomePage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(tab);
            return edgeRecentHistoryDialog.SelectTypeOfHistory(typeOfHistory).ClickViewThisHistoryButton<T>();
        }
    }
}