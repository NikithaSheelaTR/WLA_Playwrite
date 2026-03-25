namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Fact pattern tab component
    /// </summary>
    public class PrecisionFactPatternTabComponent : BasePrecisionFiltersTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'Tab-container') and not(contains(@class, 'PrecisionOverview-Tabs'))]//*[@id='panel_athensFactPattern']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Fact pattern";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
