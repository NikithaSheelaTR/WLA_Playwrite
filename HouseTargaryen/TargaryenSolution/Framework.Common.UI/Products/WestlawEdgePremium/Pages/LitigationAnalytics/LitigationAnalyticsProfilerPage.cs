namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.ProfileTabs;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Legal Analytics Profiler Page
    /// </summary>
    public class LitigationAnalyticsProfilerPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By TitleLocator = By.XPath("//div[@id='co_la_profileHeader']//h1");
        private readonly string expectedPartOfUrl = "Analytics/Profiler";

        /// <summary>
        /// The AnalyticsTitle
        /// </summary>
        public string AnalyticsTitle => DriverExtensions.WaitForElement(TitleLocator).Text;

        /// <summary>
        /// ProfileTabPanel
        /// </summary>
        public LitigationAnalyticsProfileTabPanel ProfileTabPanel => new LitigationAnalyticsProfileTabPanel();

        /// <summary>
        /// Content view tab panel.
        /// </summary>
        public LitigationAnalyticsContentViewTabPanel ContentViewTabPanel => new LitigationAnalyticsContentViewTabPanel();

        ///<summary>
        ///AnalyticsProfileTabPage
        /// </summary>
        public BaseAnalyticsProfileTabPage AnalyticsProfileTabPage => new BaseAnalyticsProfileTabPage();

        /// <summary>
        /// Is AnalyticsProfiler Page Displayed
        /// </summary>
        /// <returns>bool</returns>
        public bool IsProfilerPage() => BrowserPool.CurrentBrowser.Url.Contains(this.expectedPartOfUrl) && !this.IsErrorPage;
    }
}