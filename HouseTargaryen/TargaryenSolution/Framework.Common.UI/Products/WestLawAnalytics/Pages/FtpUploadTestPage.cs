namespace Framework.Common.UI.Products.WestLawAnalytics.Pages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// FTP Upload Test Page
    /// </summary>
    public class FtpUploadTestPage : BasePage
    {
        private static readonly By ResponseBoxLocator = By.XPath("//*[@id='response']");

        private static readonly By ChooseFileButtonLocator = By.XPath("//input[@id ='userfile']");

        private static readonly By StartUploadButtonLocator = By.XPath("//input[@id ='upload']");

        private static readonly By StorageIdFieldLocator = By.XPath("//input[@type='text' and @name ='storageId']");

        /// <summary>
        /// Sent text to "Storage Id" field.
        /// </summary>
        /// <param name="storageId">
        /// The storage Id.
        /// </param>
        public void SentTextStorageIdField(string storageId) =>
            DriverExtensions.WaitForElement(StorageIdFieldLocator).SendKeys(storageId);

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="FtpUploadTestPage"/>.
        /// </returns>
        public FtpUploadTestPage UploadFile(string filePath)
        {
            DriverExtensions.WaitForElement(ChooseFileButtonLocator).SendKeys(filePath);
            this.ClickStartUploadButton();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Click on 'Start Upload' button
        /// </summary>
        public void ClickStartUploadButton() => DriverExtensions.WaitForElement(StartUploadButtonLocator).Click();

        /// <summary>
        /// Get text from "Response" section
        /// </summary>
        /// <returns>
        /// Text from section <see cref="string"/>.
        /// </returns>
        public string GetResponseInfoMessageText() => DriverExtensions.GetText(ResponseBoxLocator);

        #region IsDisplayed
        /// <summary>
        /// Check is 'Choose File' button displayed
        /// </summary>
        /// <returns>
        /// True - if the button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsChooseFileButtonDisplayed() => DriverExtensions.IsDisplayed(ChooseFileButtonLocator);

        /// <summary>
        /// Check is 'Start Upload' button displayed
        /// </summary>
        /// <returns>
        /// True - if the button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsStartUploadButtonDisplayed() => DriverExtensions.IsDisplayed(StartUploadButtonLocator);

        /// <summary>
        /// Check is 'StorageId' field displayed
        /// </summary>
        /// <returns>
        /// True - if the button is displayed <see cref="bool"/>.
        /// </returns>
        public bool IsStorageIdFieldDisplayed() => DriverExtensions.IsDisplayed(StorageIdFieldLocator);
        #endregion IsDisplayed
    }
}