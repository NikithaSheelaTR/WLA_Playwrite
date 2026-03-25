namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Folders
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page after we add a document to a folder.
    /// </summary>
    public class AddToFolderConfirmationPage : MobileBasePage
    {
        private static readonly By BackToDocumentButtonLocator = By.Id("folderConfirmBackToDocLink");

        /// <summary>
        /// If we get the "already exists in" message, we return false
        /// </summary>
        /// <returns>False if the document already is apart of the folder, false otherwise</returns>
        public bool IsDocumentAddedSuccessfully() => !DriverExtensions.IsTextOnPage("already exists in");

        /// <summary>
        /// Click on the Back To Document button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickBackToDocumentButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToDocumentButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}