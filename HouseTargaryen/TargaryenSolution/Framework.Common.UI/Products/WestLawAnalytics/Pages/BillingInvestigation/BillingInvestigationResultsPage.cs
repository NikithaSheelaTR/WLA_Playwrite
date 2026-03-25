namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation
{
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    ///     Billing Investigator Results Page
    /// </summary>
    public class BillingInvestigationResultsPage : BasePage
    {
        private static readonly By NoResultsElementLocator = By.Id("co_billingInvestigationNoResults");

        private static readonly By SearchResultsGridLocator = By.Id("co_billingInvestigationSessionGrid");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BillingInvestigationResultsPage"/> class.
        /// </summary>
        public BillingInvestigationResultsPage()
        {
            DriverExtensions.WaitForElementDisplayed(SearchResultsGridLocator);
        }

        /// <summary>
        /// Billing Investigation Results Summary Component
        /// </summary>
        public BillingInvestigationResultsSummaryComponent ResultsSummary { get; } = new BillingInvestigationResultsSummaryComponent();

        /// <summary>
        /// Delivery Dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown(); 

        /// <summary>
        /// Results table component
        /// </summary>
        public BillingResultGridComponent BillingResultGridComponent { get; set; } = new BillingResultGridComponent();

        /// <summary>
        ///     Returns true if the search results are present
        /// </summary>
        /// <returns>True if search results are present.</returns>
        public bool AreResultsDisplayed()
            => DriverExtensions.IsDisplayed(SearchResultsGridLocator) && !DriverExtensions.IsDisplayed(NoResultsElementLocator);
    }
}