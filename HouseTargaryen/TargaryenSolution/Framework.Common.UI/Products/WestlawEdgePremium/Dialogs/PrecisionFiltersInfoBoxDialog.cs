namespace Framework.Common.UI.Products.WestlawEdgePremium.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision filters infobox dialog
    /// </summary>
    public class PrecisionFiltersInfoBoxDialog : BaseModuleRegressionDialog
    {
        private static readonly By TitleLocator = By.XPath("//*[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By InfoMessageLabelLocator = By.XPath("//div[@class='co_overlayBox_content']");
        private static readonly By CloseButtonLocator = By.XPath("//button[@class='co_primaryBtn' and text()='Close']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLocator);

        /// <summary>
        /// Info message label
        /// </summary>
        public ILabel InfoMessageLabel => new Label(InfoMessageLabelLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);
    }
}
