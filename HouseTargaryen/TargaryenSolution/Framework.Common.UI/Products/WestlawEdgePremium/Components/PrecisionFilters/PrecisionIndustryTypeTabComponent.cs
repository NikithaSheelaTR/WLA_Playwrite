namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Industry Type tab component
    /// </summary>
    public class PrecisionIndustryTypeTabComponent : BasePrecisionFiltersTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'Tab-container') and not(contains(@class, 'PrecisionOverview-Tabs'))]//*[@id='panel_athensIndustryType']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Industry type";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
