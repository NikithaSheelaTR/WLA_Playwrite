namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Practice Areas Tab Panel
    /// </summary>
    public class PrecisionGetStartedPracticeAreasTabComponent : PrecisionBaseGetStartedPanelComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_PracticeAreas");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Practice areas";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}

