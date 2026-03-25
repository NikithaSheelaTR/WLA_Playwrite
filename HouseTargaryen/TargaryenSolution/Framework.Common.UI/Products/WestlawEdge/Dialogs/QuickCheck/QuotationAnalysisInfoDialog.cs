namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Quotation analysis information dialog
    /// </summary>
    public class QuotationAnalysisInfoDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator = By.XPath("//button[text()='Close']");
        private static readonly By InfoMessageLocator  = By.XPath("//div[@class='co_overlayBox_content']");

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Info message label
        /// </summary>
        public ILabel InfoMessageLabel => new Label(InfoMessageLocator);
    }
}