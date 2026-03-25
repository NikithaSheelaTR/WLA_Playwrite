namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.SearchResultsPageFacets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// KeyNumberSearchResultPageWithFacet
    /// </summary>
    public sealed class ResearchRecommendationsSearchResultPage : KeyNumberSearchResultPage
    {
        private static readonly By PageHeadingScopeIconLocator = By.CssSelector(".co_search_result_heading_content .co_scopeIcon");

        /// <summary>
        /// View Results Facet
        /// </summary>
        public ViewFacetComponent ViewResultsFacet { get; } = new ViewFacetComponent();

        /// <summary>
        /// Verify KeyNumber scope icon is displayed.
        /// </summary>
        /// <returns>true if key number icon displayed, false otherwise</returns>
        public bool IsResearchRecommendationScopeIconDisplayed() => DriverExtensions.IsDisplayed(PageHeadingScopeIconLocator, 5);
    }
}