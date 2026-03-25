namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer - Upload A Document Tab
    /// </summary>
    public class UploadADocumentTab : BaseComplaintAnalyzerUploadPageTab
    {
        private static readonly By TabContainerLocator = By.XPath("//saf-card-v3[@data-testid='upload-tabs-container'] | //saf-card[@data-testid='upload-tabs-container']");
        private static readonly By AddFileButtonLocator = By.XPath(".//input[@data-testid='file-upload-input']");
        private static readonly By UploadFileErrorMessagesLocator = By.XPath(".//saf-status-v3[@data-testid='upload-file-validation'] | .//saf-status[@data-testid='upload-file-validation']");
        private static readonly By UploadedFileNameLocator = By.XPath(".//p[contains(@class, 'UploadDocument-module__fileName')]");
        /// <summary>
        /// Upload[Add file] button
        /// </summary>
        public IButton AddFileButton => new Button(this.ComponentLocator, AddFileButtonLocator);

        /// <summary>
        /// Error Message Label
        /// </summary>
        public ILabel UploadFileErrorMessage => new Label(this.ComponentLocator, UploadFileErrorMessagesLocator);

        /// <summary>
        /// Uploaded File Name
        /// </summary>
        public ILabel UploadedFileName => new Label(this.ComponentLocator, UploadedFileNameLocator);

        /// <summary>
        /// Upload File
        /// </summary>
        /// <param name="path"></param>
        /// <returns>Upload a Document Tab</returns>
        public UploadADocumentTab UploadFile(string path)
        {
            AddFileButton.SendKeys(path);
            
            return this;
        }

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Upload a document";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;
    }
}
