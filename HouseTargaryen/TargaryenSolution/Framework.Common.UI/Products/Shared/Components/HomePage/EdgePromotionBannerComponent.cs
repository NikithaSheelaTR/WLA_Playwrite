namespace Framework.Common.UI.Products.Shared.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Describes Edge Promotion banner on the Home page in Westlaw Classic
    /// </summary>
    public class EdgePromotionBannerComponent : BaseModuleRegressionComponent
    {
        private static readonly By PromotionBannerComponentLocator = By.XPath("//div[contains(@id,'edgePromotingBannerContainer')]");
        private static readonly By RedirectButtonLocator = By.XPath("//button[contains(@class,'PromotionWidget-button')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => PromotionBannerComponentLocator;

        /// <summary>
        /// Redirect button 
        /// </summary>
        public IButton RedirectButton => new Button(RedirectButtonLocator);

        /// <summary>
        /// Gets text from the edge promotion banner
        /// </summary>
        /// <returns>The <see cref="string"/>Text from the banner</returns>
        public string GetTextFromBanner() => DriverExtensions.GetText(PromotionBannerComponentLocator);
    }
}