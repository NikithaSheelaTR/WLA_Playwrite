namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.SearchResultsPageFacets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The overview search result page.
    /// </summary>
    public sealed class OverviewSearchResultPage : BaseSearchResultPage
    {
        private static readonly By PageHeadingLocator = By.XPath("//div[@class='co_search_result_heading_content']/h1");

        private static readonly By BackToCatPageLinkLocator = By.Id("coid_website_backtoCategoryPageLink");

        /// <summary>
        /// The result list.
        /// </summary>
        public IOverviewSearchResultList ResultList => new OverviewSearchResultList();

        /// <summary>
        /// Gets the Toolbar component
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Gets the View Results Facet
        /// </summary>
        public ViewFacetComponent ViewResultsFacet { get; } = new ViewFacetComponent();
        
        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetPageHeading()
            => DriverExtensions.WaitForElement(PageHeadingLocator).GetText();

        /// <summary>
        /// Clicks the back to browse page link
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>A new page</returns>
        public T ClickBackToBrowsePageLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToCatPageLinkLocator);
            DriverExtensions.Click(BackToCatPageLinkLocator, By.TagName("a"));
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}