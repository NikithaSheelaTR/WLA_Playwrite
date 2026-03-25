namespace Framework.Common.UI.Products.Shared.Components.CategoryPage
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    using Framework.Core.Utils.Execution;
    using System.IO;

    /// <summary>
    /// Image upload component
    /// </summary>
    public class TrademarkImageUploadComponent : BaseModuleRegressionComponent
    {

        private static readonly By ErrorMessageLabelLocator = By.XPath(".//div[contains(@id,'image_upload_error')]//div[@class ='co_infoBox_message']");

        private static readonly By UploadButtonLocator = By.XPath(".//input[@type='file' and @id='image_upload_input']");

        private static readonly By ChooseFileLabelLocator = By.XPath(".//label[contains(@class,'UploadFile-button')]");

        private static readonly By DragAndDropLabelLocator = By.XPath(".//h3[@class='UploadFile-header']");

        private static readonly By CancelUploadButtonLocator = By.XPath(".//button[contains(@class,'UploadFileLoading-button')]");

        private static readonly By DeleteImageButtonLocator = By.XPath(".//button[contains(@id,'image_upload_delete_button')]");

        private static readonly By ImagePreviewLocator = By.XPath(".//div[contains(@class,'UploadFilePreview') and @id]");

        private static readonly By LoadingLabelLocator = By.XPath(".//p[@class='UploadFileLoading-text']");

        private static readonly By RestrictionsLabelsLocator = By.XPath(".//p[@class='UploadFile-text']");

        private static readonly By ImageFilenameLabelLocator = By.XPath(".//div[contains(@id,'image_info')]");

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//div[@class='TrademarkImageUpload']");

        /// <summary>
        /// Choose File label
        /// </summary>
        public ILabel ChooseFileLabel => new Label(this.ComponentLocator, ChooseFileLabelLocator);

        /// <summary>
        /// Drag and Drop label
        /// </summary>
        public ILabel DragAndDropLabel => new Label(this.ComponentLocator, DragAndDropLabelLocator);

        /// <summary>
        /// Error message label
        /// </summary>
        public ILabel ErrorMessageLabel => new Label(this.ComponentLocator, ErrorMessageLabelLocator);

        /// <summary>
        /// Upload image button
        /// </summary>
        public IButton UploadImageButton => new Button(this.ComponentLocator, UploadButtonLocator);

        /// <summary>
        /// Image filename label
        /// </summary>
        public ILabel ImageFilenameLabel => new Label(this.ComponentLocator, ImageFilenameLabelLocator);

        /// <summary>
        /// Cancel uploading button
        /// </summary>
        public IButton CancelUploadImageButton => new Button(this.ComponentLocator, CancelUploadButtonLocator);

        /// <summary>
        /// Delete preview image button
        /// </summary>
        public IButton DeleteImageButton => new Button(this.ComponentLocator, DeleteImageButtonLocator);

        /// <summary>
        /// Trademark image label
        /// </summary>
        public ILabel TrademarkPreviewImage => new Label(this.ComponentLocator, ImagePreviewLocator);

        /// <summary>
        /// Loading image label
        /// </summary>
        public ILabel LoadingImageLabel => new Label(this.ComponentLocator, LoadingLabelLocator);

        /// <summary>
        /// Restrictions labels
        /// </summary>
        public List<Label> RestrictionLabelList =>
            DriverExtensions.GetElements(this.ComponentLocator, RestrictionsLabelsLocator)
                            .Select(webEl => new Label(webEl)).ToList();

        /// <summary>
        /// Wait for upload image process is finished and return true if image uploaded successfully
        /// </summary>
        /// <returns>True if success</returns>
        public bool IsImageUploadedSuccessfully(int msToWait = 30000)
        {
            SafeMethodExecutor.Execute(() => this.TrademarkPreviewImage.WaitDisplayed(msToWait));
            return !this.TrademarkPreviewImage.GetCssValue("display").Contains("none");
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="filePath"></param>
        public void UploadImage(string filePath)
        {
            IWebElement element = DriverExtensions.GetElement(this.ComponentLocator);
            DriverExtensions.WaitForElement(element, UploadButtonLocator).SendKeys(filePath);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Method to upload image
        /// </summary>
        /// <param name="relativePath">relative to the deployment directory file path</param>
        /// <param name="errorMessage">String to get retrieve message</param>
        /// <param name="msToWait">Implicit wait for uploading process to complete</param>
        /// <returns></returns>
        public bool UploadImage(string relativePath, ref string errorMessage, int msToWait = 30000)
        {
            this.StartImageUploadingProcess(relativePath);
            if (this.IsImageUploadedSuccessfully(msToWait))
            {
                return true;
            }

            errorMessage = this.ErrorMessageLabel.Text;
            return false;
        }

        /// <summary>
        /// start image uploading
        /// </summary>
        /// <param name="relativePath"></param>
        public void StartImageUploadingProcess(string relativePath)
        {
            string filePath = System.IO.Directory.GetCurrentDirectory() + relativePath;
            if (System.IO.File.Exists(filePath))
            {
                this.UploadImageButton.SendKeys(filePath);
            }
            else
            {
                throw new FileNotFoundException(filePath + " file is not found");
            }
        }
    }
}
