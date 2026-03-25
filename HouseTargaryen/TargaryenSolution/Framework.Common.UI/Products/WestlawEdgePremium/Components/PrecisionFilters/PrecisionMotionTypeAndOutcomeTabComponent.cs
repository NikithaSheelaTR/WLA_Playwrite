namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Motion type and outcome tab component
    /// </summary>
    public class PrecisionMotionTypeAndOutcomeTabComponent : BasePrecisionFiltersTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'Tab-container') and not(contains(@class, 'PrecisionOverview-Tabs'))]//*[@id='panel_athensMotionType']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Motion type & outcome";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
