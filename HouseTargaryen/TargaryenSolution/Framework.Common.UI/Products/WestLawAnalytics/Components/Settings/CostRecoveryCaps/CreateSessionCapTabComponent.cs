namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create A Session Cap Tab Component
    /// </summary>
    public class CreateSessionCapTabComponent : CreateCapBaseTabComponent
    {
        private static readonly By ApplyCapsToTransactionalSessionsCheckboxLocator = By.Id("wa_transactionalSessions");

        private static readonly By ApplyCapsToHourlySessionsCheckboxLocator = By.Id("wa_hourlySessions");

        private static readonly By ContainerLocator = By.CssSelector("#wa_pricingTabsMainContent, #wa_pricingTabsMainFooter");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Create A Session Cap";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verifies that the Apply Caps To Hourly Sessions Checkbox is Displayed
        /// </summary>
        /// <returns> True if the Apply Caps To Hourly Sessions Checkbox is Displayed </returns>
        public bool IsApplyCapsToHourlySessionsCheckboxDisplayed()
            => DriverExtensions.IsDisplayed(ApplyCapsToHourlySessionsCheckboxLocator);

        /// <summary>
        /// Verifies that the Apply Caps To Transactional Sessions Checkbox is Displayed
        /// </summary>
        /// <returns> True if the Apply Caps To Transactional Sessions Checkbox is Displayed </returns>
        public bool IsApplyCapsToTransactionalSessionsCheckboxDisplayed()
            => DriverExtensions.IsDisplayed(ApplyCapsToTransactionalSessionsCheckboxLocator);

        /// <summary>
        /// Verifies that the Apply Caps To Hourly Sessions Checkbox is selected.
        /// </summary>
        /// <returns> True if the Apply Caps To Hourly Sessions Checkbox is selected </returns>
        public bool IsApplyCapsToHourlySessionsCheckboxSelected()
            => DriverExtensions.IsCheckboxSelected(ApplyCapsToHourlySessionsCheckboxLocator);

        /// <summary>
        /// Verifies that the Apply Caps To Transactional Sessions Checkbox checked
        /// </summary>
        /// <returns> True if the Apply Caps To Transactional Sessions Checkbox is checked </returns>
        public bool IsApplyCapsToTransactionalSessionsCheckboxSelected()
            => DriverExtensions.IsCheckboxSelected(ApplyCapsToTransactionalSessionsCheckboxLocator);

        /// <summary>
        /// Sets the Apply Caps To Hourly Sessions Checkbox
        /// </summary>
        /// <param name="selected"> The desired value of the checkbox (True for selected and false for deselected) </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SetApplyCapsToHourlySessionsCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, ApplyCapsToHourlySessionsCheckboxLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Sets the Apply Caps To Transactional Sessions Checkbox
        /// </summary>
        /// <param name="selected"> The desired value of the checkbox (True for selected and false for deselected) </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage SetApplyCapsToTransactionalSessionsCheckbox(bool selected)
        {
            DriverExtensions.SetCheckbox(selected, ApplyCapsToTransactionalSessionsCheckboxLocator);
            return new CostRecoveryCapsPage();
        }
    }
}
