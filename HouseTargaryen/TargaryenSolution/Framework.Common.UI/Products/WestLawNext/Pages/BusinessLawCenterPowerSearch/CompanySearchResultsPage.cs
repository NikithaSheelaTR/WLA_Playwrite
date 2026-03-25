namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;

    /// <summary>
    /// The company search results page.
    /// </summary>
    public class CompanySearchResultsPage : CompanySearchPage
    {
        /// <summary>
        /// Gets the company search results section.
        /// </summary>
        public CompanySearchResultsComponent CompanySearchResultsSection { get; private set; }
            = new CompanySearchResultsComponent();
    }
}