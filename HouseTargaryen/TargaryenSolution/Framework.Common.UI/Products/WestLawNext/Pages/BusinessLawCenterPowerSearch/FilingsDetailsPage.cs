namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents;

    /// <summary>
    /// The filings details page.
    /// </summary>
    public class FilingsDetailsPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// Gets the exhibits table section.
        /// </summary>
        public ExhibitsTableComponent ExhibitsTableSection { get; private set; } = new ExhibitsTableComponent();
    }
}