namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;

    /// <summary>
    /// The company search within filings results page.
    /// </summary>
    public class CompanyFilingsSearchWithinResultsPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// Gets the company filings search within results section.
        /// </summary>
        public CompanyFilingsSearchWithinResultsComponent CompanyFilingsSearchWithinResultsSection { get; private set; }
            = new CompanyFilingsSearchWithinResultsComponent();
    }
}