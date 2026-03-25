namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;

    /// <summary>
    /// The full text search results page.
    /// </summary>
    public class FullTextSearchResultsPage : FullTextSearchPage
    {
        /// <summary>
        /// Gets the full text search results section.
        /// </summary>
        public FullTextSearchResultsComponent FullTextSearchResultsSection { get; private set; }
            = new FullTextSearchResultsComponent();
    }
}