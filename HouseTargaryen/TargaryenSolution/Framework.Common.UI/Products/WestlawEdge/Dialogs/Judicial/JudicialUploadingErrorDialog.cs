namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Judicial
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial pre uploading error dialog
    /// </summary>
    public class JudicialUploadingErrorDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//*[@class='co_overlayBox_optionsBottom']//button");

        private static readonly By FailedTextLocator = By.XPath("//*[@class='co_overlayBox_content']");

        /// <summary>
        /// Gets the close button.
        /// </summary>
        public IButton CloseButton { get; } = new Button(CloseButtonLocator);   

        /// <summary>
        /// Failed message
        /// </summary>
        public ILabel FailedMessageLabel { get; } = new Label(FailedTextLocator);
    }
}
