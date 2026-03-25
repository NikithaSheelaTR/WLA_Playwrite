namespace Framework.Common.UI.Products.Shared.Managers
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Enums.HomePage;
    using Framework.Common.UI.Products.Shared.Pages.Tools;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// WLN Navigation Manager
    /// </summary>
    public class WestlawNavigationManager : BaseNavigationManager
    {
        private static WestlawNavigationManager instance;

        private const string ViewCommunityLink = "View Community";
        private const string CommunityPageTitle = "https://auth.thomsonreuters.com/u/login?...";

        private WestlawNavigationManager()
        {
        }

        /// <summary>
        /// Returns instance instance
        /// </summary>
        /// <returns> The <see cref="WestlawNavigationManager"/>. </returns>
        public static WestlawNavigationManager Instance => instance ?? (instance = new WestlawNavigationManager());

        /// <summary>
        /// Click on Alert Link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The instance of IAlertCenterPage </returns>
        public override T GoToAlertsPage<T>() => this.ClickNavigationLink<T>(GlobalNavigationLink.Alerts);


        /// <summary>
        /// Go to community dialog
        /// </summary>
        /// <typeparam name="T">An instance that implements ICommunityPage</typeparam>
        /// <returns></returns>
        public override T GoToCommunityPage<T>() => this.ClickNavigationLink<CommunityDialog>(GlobalNavigationLink.CommunityArrow)
            .ClickLinkAndOpenNewBrowserTab<T>(ViewCommunityLink, CommunityPageTitle);

        /// <summary>
        /// GoTo CommonFavoritesPage
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The instance of ICommonFavoritesPage </returns>
        public override T GoToCommonFavoritesPage<T>() => this.ClickNavigationLink<T>(GlobalNavigationLink.Favorites);

        /// <summary>
        /// GoTo FoldersPage
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The Instance of IResearchOrganizerPage </returns>
        public override T GoToFoldersPage<T>() => this.ClickNavigationLink<T>(GlobalNavigationLink.Folders);

        /// <summary>
        /// GoTo HistoryPage
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The Instance of ICommonHistoryPage </returns>
        public override T GoToHistoryPage<T>() => this.ClickNavigationLink<T>(GlobalNavigationLink.History);

        /// <summary>
        /// Go to Subscription Page page via Tools
        /// </summary>
        /// <returns> The <see cref="MyPlanPage"/>. </returns>
        public override MyPlanPage GoToSubscriptionPage()
            => new BrowseTabPanel().SetActiveTab<ToolsTabPanel>(BrowseTab.Tools).ClickBrowseCategory<MyPlanPage>(Tool.MyContent);

        #region not supported functionality
        /// <summary>
        /// GoToNotificationsPage
        /// </summary>
        /// <returns> The <see cref="NotificationCenterPage"/>. </returns>
        public override NotificationCenterPage GoToNotificationsPage()
        {
            throw new NotImplementedException("This functionality doesn't implement in Westlaw");
        }
        #endregion

        /// <summary>
        /// Clicks a link
        /// </summary>
        /// <param name="link">Link navigate to</param>
        /// <typeparam name="T">Page object</typeparam>
        /// <returns>Page instance</returns>
        private T ClickNavigationLink<T>(GlobalNavigationLink link)
            where T : ICreatablePageObject
        {
            new WestlawNextHeaderComponent().ClickGlobalNavigationLink(link);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}