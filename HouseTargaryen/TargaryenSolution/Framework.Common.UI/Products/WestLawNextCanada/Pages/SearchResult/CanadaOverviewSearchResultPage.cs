namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.SearchResult
{
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.ResultList;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Overview Search Result Page
    /// </summary>
    public class CanadaOverviewSearchResultPage : BaseSearchResultPage
    {
        private static readonly By PageHeadingLocator = By.XPath("//div[@class='co_search_result_heading_content']/h1");

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new CanadaEdgeHeaderComponent Header { get; } = new CanadaEdgeHeaderComponent();


        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetPageHeading()
            => DriverExtensions.WaitForElement(PageHeadingLocator).GetText();
        /// <summary>
        /// The result list.
        /// </summary>
        public CanadaOverviewSearchResultListComponent ResultList => new CanadaOverviewSearchResultListComponent();
    }
}