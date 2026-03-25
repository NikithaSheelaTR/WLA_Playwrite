namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Unable To Analyze Document Dialog
    /// upload file with unsupported format -> UnableToAnalyzeDocumentDialog
    /// </summary>
    public class UnableToAnalyzeDocumentDialog : BaseModuleRegressionDialog
    {
        private static readonly By FailedTextLocator = By.XPath("//*[@class='co_overlayBox_content']/h2");
        private static readonly By CloseButtonLocator = By.XPath("//*[@class='co_overlayBox_optionsBottom']//button");

        /// <summary>
        /// Gets the close button.
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Failed message
        /// </summary>
        public ILabel FailedMessageLabel => new Label(FailedTextLocator);        
    }
}