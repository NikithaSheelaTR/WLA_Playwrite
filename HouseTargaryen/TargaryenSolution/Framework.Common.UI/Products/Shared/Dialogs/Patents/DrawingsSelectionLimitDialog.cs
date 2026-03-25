namespace Framework.Common.UI.Products.Shared.Dialogs.Patents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Dialog appears after 200+ items selected
    /// </summary>
    public class DrawingsSelectionLimitDialog : BaseModuleRegressionDialog
    {
        private static readonly By DialogContainerLocator = By.XPath("//div[contains(@id,'IPDrawings-checkboxWarn-Lightbox')]");

        private static readonly By DialogHeadlineLocator = By.XPath(".//div[contains(@class,'headline')]//h3");

        private static readonly By DialogCloseButtonLocator =
            By.XPath(".//div[contains(@class,'headline')]//button[contains(@id,'LightboxClose')]");

        private static readonly By DialogWarningLabelLocator =
            By.XPath(".//div[contains(@class,'content')]//div[contains(@class,'LightboxText')]");

        /// <summary>
        /// Dialog headline Label
        /// </summary>
        public ILabel DialogLabel => new Label(DialogContainerLocator, DialogHeadlineLocator);

        /// <summary>
        /// Dialog x-close button
        /// </summary>
        public IButton DialogCloseButton => new Button(DialogContainerLocator, DialogCloseButtonLocator);

        /// <summary>
        /// Warning message
        /// </summary>
        public ILabel DialogWarningMessageLabel => new Label(DialogContainerLocator, DialogWarningLabelLocator);
    }
}