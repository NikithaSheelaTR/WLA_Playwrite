namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Federal Materials Tab Panel
    /// </summary>
    public class PrecisionGetStartedFederalMaterialsTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_FederalMaterials");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Federal materials";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}

