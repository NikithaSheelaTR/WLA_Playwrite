namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages.Folders;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages.Recent;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Homepage for WLN Mobile
    /// </summary>
    public class MobileHomePage : MobileBasePageWithHeader
    {
        private static readonly By BrowseContentLinkLocator = By.Id("coid_website_browseHomeLink");

        private static readonly By ChangeClientIdButtonLocator = By.Id("coid_website_changeClientIDLink");

        private static readonly By FieldValidationErrorLocator = By.ClassName("field-validation-error");

        private static readonly By FoldersLinkLocator = By.Id("coid_website_foldersLink");

        private static readonly By KeyCiteADocumentLinkLocator = By.Id("coid_website_keyCiteLink");

        private static readonly By MattersLinkLocator = By.Id("coid_website_mattersLink");

        private static readonly By RecentDocumentsLinkLocator = By.Id("coid_website_recentDocumentsLink");

        private static readonly By RecentSearchesLinkLocator = By.Id("coid_website_recentSearchesLink");

        private static readonly By SearchButtonLocator = By.Id("coid_website_searchButton");

        private static readonly By SearchIdLocator = By.Id("q");

        /// <summary>
        /// Click on the Browse Content link
        /// </summary>
        /// <typeparam name="T"> Page type</typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickBrowseContentLink<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(BrowseContentLinkLocator);

        /// <summary>
        /// Click on the Folders link
        /// </summary>
        /// <returns> The <see cref="FoldersPage"/>. </returns>
        public FoldersPage ClickFoldersLink() => this.ClickElement<FoldersPage>(FoldersLinkLocator);

        /// <summary>
        /// Click on the KeyCite A Document link
        /// </summary>
        /// <returns> The <see cref="KeyCiteADocumentPage"/>. </returns>
        public KeyCiteADocumentPage ClickKeyCiteADocumentLink()
            => this.ClickElement<KeyCiteADocumentPage>(KeyCiteADocumentLinkLocator);

        /// <summary>
        /// Click on the Matters link
        /// </summary>
        /// <returns> The <see cref="MattersPage"/>. </returns>
        public MattersPage ClickMattersLink() => this.ClickElement<MattersPage>(MattersLinkLocator);

        /// <summary>
        /// Click on the RecentSearchesLinkLocator
        /// </summary>
        /// <returns> The <see cref="RecentDocumentsPage"/>. </returns>
        public RecentDocumentsPage ClickRecentDocumentsLink()
            => this.ClickElement<RecentDocumentsPage>(RecentDocumentsLinkLocator);

        /// <summary>
        /// Click on the RecentSearchesLinkLocator
        /// </summary>
        /// <returns> The <see cref="RecentSearchesPage"/>. </returns>
        public RecentSearchesPage ClickRecentSearchesLink()
            => this.ClickElement<RecentSearchesPage>(RecentSearchesLinkLocator);

        /// <summary>
        /// Get text from the folders link
        /// </summary>
        /// <returns> Folder name </returns>
        public string GetFoldersLinkText() => DriverExtensions.GetText(FoldersLinkLocator);

        /// <summary>
        /// Get text from the folders link
        /// </summary>
        /// <returns> Matters text </returns>
        public string GetMattersText() => DriverExtensions.GetText(MattersLinkLocator);

        /// <summary>
        /// Gets all of the validation messages
        /// </summary>
        /// <returns>List of validation messages</returns>
        public List<string> GetValidationMessages()
            => DriverExtensions.GetElements(FieldValidationErrorLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// checks if the change client ID link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsChangeClientIdLinkDisplayed() => DriverExtensions.IsDisplayed(ChangeClientIdButtonLocator, 5);

        /// <summary>
        /// Use if you expect your search to go directly to a document
        /// </summary>
        /// <param name="searchTerm"> searchTerm </param>
        /// <returns> The <see cref="MobileDocumentPage"/>. </returns>
        public MobileDocumentPage SearchCitation(string searchTerm)
        {
            DriverExtensions.SetTextField(searchTerm, SearchIdLocator);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return new MobileDocumentPage();
        }
    }
}