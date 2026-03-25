namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage
{
    using OpenQA.Selenium;

    /// <summary>
    /// Get Started Favorites Tab Panel
    /// </summary>
    public class PrecisionGetStartedFavoritesTabComponent : PrecisionBaseGetStartedPanelComponent
    {

        private static readonly By ContainerLocator = By.Id("panel_Favorites");

        /// <summary>
        /// The tab name
        /// </summary>
        protected override string TabName => "Favorites";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
