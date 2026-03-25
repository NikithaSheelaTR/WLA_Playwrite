namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Custom Pages Tab Panel
    /// </summary>
    public class PrecisionGetStartedCustomPagesTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_CustomPages");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Custom pages";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
