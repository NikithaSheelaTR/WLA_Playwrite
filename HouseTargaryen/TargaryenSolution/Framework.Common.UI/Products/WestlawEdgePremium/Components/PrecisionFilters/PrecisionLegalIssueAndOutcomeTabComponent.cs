namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Legal issue and outcome tab component
    /// </summary>
    public class PrecisionLegalIssueAndOutcomeTabComponent : BasePrecisionFiltersTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'Tab-container') and not(contains(@class, 'PrecisionOverview-Tabs'))]//*[@id='panel_athensLegalIssue']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Legal issue & outcome";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
