namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Next Generation KeyCite dialog
    /// </summary>
    public class PrecisionNextGenerationKeyCiteDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.Id("coid_keyCiteFeatureModal");
        private static readonly By CloseButtonLocator = By.XPath(".//button[@class='co_secondaryBtn']");
        private static readonly By TitleLabelLocator = By.XPath(".//*[contains(@id, 'coid_lightboxAriaLabel')]");        

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(ContainerLocator, TitleLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(ContainerLocator, CloseButtonLocator);
    }
}
