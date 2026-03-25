namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Quick access full dialog
    /// </summary>
    public class QuickAccessFullDialog : BaseModuleRegressionDialog
    {
        private static readonly By TitleLocator = By.XPath("//h2[contains(@id, 'coid_lightboxAriaLabel')]");
        private static readonly By MessageLocator = By.XPath("//div[@class = 'co_quickAccess_lightbox_container']");
        private static readonly By OkButtonLocator = By.XPath("//button[@id = 'co_quickAccess_confirmationButton']");        
        private static readonly By CloseButtonLocator = By.XPath("//button[text() = 'Close']");

        /// <summary>
        /// Title label
        /// </summary>
        public ILabel TitleLabel => new Label(TitleLocator);

        /// <summary>
        /// Message label
        /// </summary>
        public ILabel MessageLabel => new Label(MessageLocator);

        /// <summary>
        /// OK button
        /// </summary>
        public IButton OkButton { get; } = new Button(OkButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton { get; } = new Button(CloseButtonLocator);
    }
}
