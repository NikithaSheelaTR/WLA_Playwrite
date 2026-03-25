namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents;

    /// <summary>
    /// The full text search page.
    /// </summary>
    public class FullTextSearchPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// Gets the full text search section.
        /// </summary>
        public FullTextSearchComponent FullTextSearchSection { get; private set; } = new FullTextSearchComponent();
    }
}