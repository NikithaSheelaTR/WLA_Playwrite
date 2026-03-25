namespace Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.LegalAnalytics;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Legal Analytics Profiler Page
    /// </summary>
    public class AnalyticsProfilerPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By TitleLocator = By.XPath("//div[@id='co_la_profileHeader']/h1/div");
        private readonly string expectedPartOfUrl = "Analytics/Profiler";
        private static readonly By KnowYourJudgeTabLocator = By.XPath("//li[@id='co_la_knowYourJudge_tab']");

        /// <summary>
        /// The AnalyticsTitle
        /// </summary>
        public string AnalyticsTitle => DriverExtensions.WaitForElement(TitleLocator).Text;

        /// <summary>
        /// ProfileTabPanel
        /// </summary>
        public LitigationAnalyticsProfileTabPanel ProfileTabPanel => new LitigationAnalyticsProfileTabPanel();

        /// <summary>
        /// The return to la view page.
        /// </summary>
        /// <returns>
        /// The <see cref="AnalyticsViewPage"/>.
        /// </returns>
        public AnalyticsViewPage ReturnToLaViewPage() => BrowserPool.CurrentBrowser.GoBack<AnalyticsViewPage>();

        /// <summary>
        /// Is AnalyticsProfiler Page Displayed
        /// </summary>
        /// <returns>bool</returns>
        public bool IsProfilerPage() => BrowserPool.CurrentBrowser.Url.Contains(this.expectedPartOfUrl) && !this.IsErrorPage;

        /// <summary>
        /// Know your judge Tab label
        /// </summary>        
        public ILabel KnowYourJudgeTab => new Label(KnowYourJudgeTabLocator);

        /// <summary>
        /// Is Know Your Judge Tab Enabled 
        /// </summary>
        /// <returns>bool</returns>
        public bool IsKnowYourJudgeTabEnabled => KnowYourJudgeTab.GetAttribute("aria-label").Contains("unavailable");

    }
}
