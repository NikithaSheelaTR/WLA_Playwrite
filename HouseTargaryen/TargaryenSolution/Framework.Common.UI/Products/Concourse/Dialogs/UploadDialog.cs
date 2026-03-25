namespace Framework.Common.UI.Products.Concourse.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Upload Widget used to upload user documents to a Matter or Matter folder
    /// </summary>
    public class UploadDialog : BaseModuleRegressionDialog
    {
        private static readonly By CloseButtonLocator =
            By.XPath("//div[@id='coid_lightboxOverlay']//input[@class='co_dropdownBox_close']");

        private static readonly By ErrorMessageLocator = By.ClassName("error");

        private static readonly By FileBrowserLocator =
            By.XPath("//div[@id='coid_lightboxOverlay']//input[@class='co_dropdownBox_addFiles']");

        private static readonly By FileUploadStatusLocator = By.Id("file_upload_status_success_count");

        private static readonly By UploadButtonLocator =
            By.XPath("//div[@id='coid_lightboxOverlay']//input[@class='co_primaryBtn co_dropdownBox_ok']");

        /// <summary>
        /// Enters the file path in the file browser
        /// </summary>
        /// <param name="filePath">File path</param>
        public void ChooseFile(string filePath) => DriverExtensions.WaitForElement(FileBrowserLocator).SendKeys(filePath);

        /// <summary>
        /// Click on close button
        /// </summary>
        /// <returns>The <see cref="ResearchOrganizerPage"/>.</returns>
        public ResearchOrganizerPage ClickCloseButton() => this.ClickElement<ResearchOrganizerPage>(CloseButtonLocator);

        /// <summary>
        /// Click on upload button
        /// </summary>
        public void ClickUploadButton()
        {
            DriverExtensions.WaitForElementDisplayed(UploadButtonLocator).Click();
            DriverExtensions.WaitForElementDisplayed(FileUploadStatusLocator);
        }

        /// <summary>
        /// Determines that error message is present
        /// </summary>
        /// <param name="errorMessage">The error Message.</param>
        /// <returns>True if Error message is presented, otherwise - false</returns>
        public bool IsErrorMessageDisplayed(string errorMessage)
            => DriverExtensions.WaitForElementDisplayed(ErrorMessageLocator).Text.Contains(errorMessage);

        /// <summary>
        /// Check that upload was successful
        /// </summary>
        /// <returns>True if upload was successful, otherwise - false</returns>
        public bool IsUploadSuccessful() => DriverExtensions.IsTextInElement(FileUploadStatusLocator, "Uploaded");

        /// <summary>
        /// Enters the file path in the file browser and then click the upload button 
        /// </summary>
        /// <param name="filePath">location of the file to be uploaded</param>
        /// <returns>The <see cref="ResearchOrganizerPage"/>.</returns>
        public ResearchOrganizerPage UploadDocument(string filePath)
        {
            this.ChooseFile(filePath);
            DriverExtensions.WaitForElementDisplayed(UploadButtonLocator).Click();
            return this.ClickCloseButton();
        }
    }
}