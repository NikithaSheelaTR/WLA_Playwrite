namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using OpenQA.Selenium;

    /// <summary>
    /// Create A Monthly Cap Tab Component
    /// </summary>
    public class CreateMonthlyCapTabComponent : CreateCapBaseTabComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#wa_pricingTabsMainContent, #wa_pricingTabsMainFooter");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Create A Monthly Cap";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
