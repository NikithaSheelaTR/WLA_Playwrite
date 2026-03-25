namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Tools Tab Panel
    /// </summary>
    public class PrecisionGetStartedToolsTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_Tools");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Tools";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}