namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Content Types Tab Panel
    /// </summary>
    public class PrecisionGetStartedContentTypesTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_ContentTypes");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Content types";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
