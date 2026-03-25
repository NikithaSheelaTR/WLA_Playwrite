namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents;

    /// <summary>
    /// The company filings result list page.
    /// </summary>
    public class CompanyFilingsPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// Gets the company filings section.
        /// </summary>
        public CompanyFilingsComponent CompanyFilingsSection { get; private set; } = new CompanyFilingsComponent();

        /// <summary>
        /// Gets the search within filings section.
        /// </summary>
        public SearchWithinFilingsComponent SearchWithinFilingsSection { get; private set; } = new SearchWithinFilingsComponent();
    }
}