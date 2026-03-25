namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Folders
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNextMobile.Pages.Email;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// Folder Page
    /// </summary>
    public class FoldersPage : MobileBasePage
    {
        private static readonly By CreateFolderLinkLocator = By.Id("coid_website_createFolderLink");

        private static readonly By CurrentFolderTitleLocator = By.CssSelector(".hdr.noBrd");

        private static readonly By DocumentLinkContainerLocator = By.Id("co_mobile_folderDocuments");

        private static readonly By DocumentLinkLocator = By.CssSelector(".docLink");

        private static readonly By EmailListLinkLocator = By.Id("coid_website_emailListLink");

        private static readonly By FoldersContainerLocator = By.Id("co_mobile_folderDocuments");

        private static readonly By FoldersLocator = By.CssSelector("#co_mobile_folderDocuments a.icn");

        private static readonly By UnlinkedFoldersLocator = By.CssSelector("#co_mobile_folderDocuments span");
        
        /// <summary>
        /// Clicks the Create Folder link
        /// </summary>
        /// <returns>A CreateFolderPage</returns>
        public CreateFolderPage ClickCreateFolder()
        {
            DriverExtensions.WaitForElement(CreateFolderLinkLocator).Click();
            return new CreateFolderPage();
        }

        /// <summary>
        /// Click the specified document
        /// </summary>
        /// <param name="document"> The name of the document </param>
        /// <returns> The <see cref="MobileDocumentPage"/>. </returns>
        public MobileDocumentPage ClickDocument(string document)
        {
            DriverExtensions.WaitForElement(new ByChained(FoldersContainerLocator, By.LinkText(document))).Click();
            return new MobileDocumentPage();
        }

        /// <summary>
        /// Clicks the Email List link
        /// </summary>
        /// <returns>A EmailListPage</returns>
        public EmailPage ClickEmailList()
        {
            DriverExtensions.WaitForElement(EmailListLinkLocator).Click();
            return new EmailPage();
        }

        /// <summary>
        /// Clicks the folder with the specified name
        /// </summary>
        /// <typeparam name="T">A FoldersPage class</typeparam>
        /// <param name="folderName">The name of the folder to click</param>
        /// <returns>A new MattersPage object</returns>
        public T ClickFolder<T>(string folderName) where T : FoldersPage
        {
            DriverExtensions.WaitForElement(new ByChained(DocumentLinkContainerLocator, By.LinkText(folderName))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Gets the name of the current folder
        /// </summary>
        /// <returns>The name of the folder</returns>
        public string GetCurrentFolder() => DriverExtensions.GetImmediateText(CurrentFolderTitleLocator);

        /// <summary>
        /// Gets a list of documents in the folder
        /// </summary>
        /// <returns>A list of the names of the documents</returns>
        public List<string> GetDocuments()
            => DriverExtensions.GetElements(DocumentLinkLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Gets a list of the folders on the page
        /// </summary>
        /// <returns>A list with the names of the folders</returns>
        public List<string> GetFolders()
            => DriverExtensions.GetElements(FoldersLocator).Select(e => e.Text.Trim()).ToList();

        /// <summary>
        /// Gets a list of folders that are not an anchor
        /// </summary>
        /// <returns>A list with the names of the unlinked folders</returns>
        public List<string> GetUnlinkedFolders()
            => DriverExtensions.GetElements(UnlinkedFoldersLocator).Select(e => e.Text.Trim()).ToList();
    }
}