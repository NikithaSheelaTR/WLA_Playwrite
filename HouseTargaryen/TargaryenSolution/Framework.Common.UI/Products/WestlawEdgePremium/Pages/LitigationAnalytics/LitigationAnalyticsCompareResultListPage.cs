namespace Framework.Common.UI.Products.WestlawEdgePremium.Pages.LitigationAnalytics
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.SearchResultPageComponents.ComparePageComponents;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Compare result list page
    /// </summary>
    public class CompareResultListPage : EdgeCommonSearchResultPage
    {
        private static readonly By TitleLocator = By.XPath("//*[@class='CompanySearchResults-header']/h1");
        private static readonly By ResultListContainerLocator = By.XPath("//div[@class = 'co_searchResultsList']");
        private static readonly By LoadingSpinnerLocator = By.XPath("//div[@class ='la-Loading co_clearfix la-Loading-extra-space']");
        private readonly string ExpectedPartOfUrl = "Analytics/Profiler";
        /// <summary>
        /// The AnalyticsTitle
        /// </summary>
        public string AnalyticsTitle => DriverExtensions.WaitForElement(TitleLocator).Text;

        /// <summary>
        /// ProfileTabPanel
        /// </summary>
        public CompareResultListComponent CompareResultComponent => new CompareResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// ProfileTabPanel
        /// </summary>
        public CompanyResultListComponent CompanySelectionComponent => new CompanyResultListComponent();

        ///<summary>
        ///MySelections Component
        /// </summary>
        public MySelectionsComponent MySelectionsComponent => new MySelectionsComponent();

        /// <summary>
        /// Loading Spinner
        /// </summary>
        public ILabel LoadingSpinner => new Label(LoadingSpinnerLocator);
        /// <summary>
        /// Is AnalyticsProfiler Page Displayed
        /// </summary>
        /// <returns>bool</returns>
        public bool IsProfilerPage() => BrowserPool.CurrentBrowser.Url.Contains(this.ExpectedPartOfUrl) && !this.IsErrorPage;
    }
}