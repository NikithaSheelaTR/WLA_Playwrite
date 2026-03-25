namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents;

    /// <summary>
    /// The company search page.
    /// </summary>
    public class CompanySearchPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// Gets the company search section.
        /// </summary>
        public CompanySearchComponent CompanySearchSection { get; private set; } = new CompanySearchComponent();
    }
}