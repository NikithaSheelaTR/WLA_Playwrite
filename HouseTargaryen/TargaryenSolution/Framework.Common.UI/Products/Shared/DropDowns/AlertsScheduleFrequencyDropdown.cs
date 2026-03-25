namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using Framework.Common.UI.Products.Shared.Enums.Alerts;

    using OpenQA.Selenium;

    /// <summary>
    /// Frequency select dropdown on Create Alert page
    /// </summary>
    public class AlertsScheduleFrequencyDropdown : BaseModuleRegressionDropdown<AlertsScheduleFrequencyOptions>
    {
        private static readonly By AlertScheduleFrequencyDropdownLocator = By.CssSelector("#frequencySelect");

        /// <summary>
        /// Alert Schedule Frequency Dropdown Locator
        /// </summary>
        protected override By DropdownLocator => AlertScheduleFrequencyDropdownLocator;
    }
}