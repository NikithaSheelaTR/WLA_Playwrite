namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Folders
{
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Create folder page
    /// </summary>
    public class CreateFolderPage : MobileBasePage
    {
        private static readonly By CreateFolderButtonLocator = By.Id("coid_website_createFolderButton");

        private static readonly By FolderNameTextBoxLocator = By.Id("fn");

        /// <summary>
        /// Click on the Create Folder button
        /// </summary>
        /// <returns> The <see cref="ResearchOrganizerPage"/>. </returns>
        public FoldersPage ClickCreateFolderButton()
        {
            DriverExtensions.WaitForElement(CreateFolderButtonLocator).Click();
            return new FoldersPage();
        }

        /// <summary>
        /// Set text to folder name textbox
        /// </summary>
        /// <param name="folderName"> Folder Name </param>
        public void SetTextToFolderNameTextBox(string folderName)
            => DriverExtensions.SetTextField(folderName, FolderNameTextBoxLocator);
    }
}