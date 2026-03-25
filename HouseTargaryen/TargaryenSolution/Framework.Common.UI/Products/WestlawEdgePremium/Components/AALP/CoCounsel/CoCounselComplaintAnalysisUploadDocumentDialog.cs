namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel Complaint Analysis Upload Document Dialog
    /// </summary>
    public class CoCounselComplaintAnalysisUploadDocumentDialog : BaseModuleRegressionDialog
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class, 'file-upload-dialog__paper')]");
        private static readonly By UploadFileTexboxLocator = By.XPath(".//input[@type='file']");
        private static readonly By DoneButtonLocator = By.XPath(".//*[@data-testid='file-upload-close-button']");

        /// <summary>
        /// Upload File Area textbox
        /// </summary>
        public ITextbox UploadFileTextbox => new Textbox(this.ComponentLocator, UploadFileTexboxLocator);

        /// <summary>
        /// Done button
        /// </summary>
        public IButton DoneButton => new Button(this.ComponentLocator, DoneButtonLocator);

        /// <summary>
        /// Uploads the file.
        /// </summary>
        /// <typeparam name="T">
        /// The uploading progress dialog
        /// </typeparam>
        /// <param name="path">
        /// The path to file.
        /// </param>
        /// <returns>
        /// The document analyzer recommendations page.
        /// </returns>
        public T UploadFile<T>(string path) where T : BaseModuleRegressionDialog
        {
            this.UploadFileTextbox.SendKeys(path);
            return DriverExtensions.CreatePageInstance<T>();
        }
      
        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => ContainerLocator;
    }
}
