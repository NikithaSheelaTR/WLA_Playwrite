namespace Framework.Common.UI.Products.TaxnetPro.Components.HomePage
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    /// <summary>
    /// Home page component marketing lightbox
    /// </summary>
    public class HomePageComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@class='co_overlayBox_container']");
        private static readonly By CloseButtonLocator = By.Id("coid_marketingLightbox_closeButton");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Close Button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);
    }
}
