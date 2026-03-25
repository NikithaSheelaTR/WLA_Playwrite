namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsHomePageComponents.Tabs;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;


    /// <summary>
    /// Litigation Analytics Search page
    /// </summary>
    public class LitigationAnalyticsSearchPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        /// <summary>
        /// Gets the header.
        /// </summary>
        public LitigationAnalyticsEntitiesTabsComponent HeaderPanel { get; } = new LitigationAnalyticsEntitiesTabsComponent();
    }
}