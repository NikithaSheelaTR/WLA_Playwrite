namespace Framework.Common.UI.Raw.WestlawEdge.Pages.TrDiscover
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Components.TrDiscover;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// TrdSearchResultPage
    /// </summary>
    public class TrdSearchResultPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ResultListContainerLocator = By.XPath("//*[@id = 'coid_website_searchResults']");

        /// <summary>
        /// Toolbar component
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; private set; } = new EdgeToolbarComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public EdgeLegacyResultListComponent ResultList => new EdgeLegacyResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; private set; } = new FooterToolbarComponent();

        /// <summary>
        /// Applyed Filters Message 
        /// </summary>
        public AppliedFiltersMessageComponent AppliedFiltersMessageComponent { get; } = new AppliedFiltersMessageComponent();

        /// <summary>
        /// Checks if correct page loads
        /// </summary>
        /// <returns></returns>
        public bool IsTrdPage() => BrowserPool.CurrentBrowser.Url.Contains("contentType=TRDISCOVER-CASE");
    }
}
