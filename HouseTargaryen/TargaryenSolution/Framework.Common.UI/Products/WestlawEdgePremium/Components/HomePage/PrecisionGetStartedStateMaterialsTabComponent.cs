namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started State Materials Tab Panel
    /// </summary>
    public class PrecisionGetStartedStateMaterialsTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_StateMaterials");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "State materials";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
