namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Folders
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The page where you select which folder you want to add a document to
    /// </summary>
    public class AddToFolderPage : MobileBasePage
    {
        private static readonly By SaveButtonLocator = By.Id("coid_website_addDocToFolderLink");
        
        /// <summary>
        /// Click on the Save button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSaveButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Opens the folder with the given name
        /// </summary>
        /// <param name="folderName">The folder name</param>
        /// <returns>A new AddToFolderPage page</returns>
        public AddToFolderPage OpenFolder(string folderName)
        {
            DriverExtensions.WaitForElement(By.LinkText(folderName)).Click();
            return this;
        }
    }
}