namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.PrecisionFilters
{
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Area of law tab component
    /// </summary>
    public class PrecisionAreaOfLawTabComponent : BasePrecisionFiltersTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[contains(@class, 'Tab-container') and not(contains(@class, 'PrecisionOverview-Tabs'))]//*[@id='panel_athensAreaOfLaw']");

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Area of law";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
