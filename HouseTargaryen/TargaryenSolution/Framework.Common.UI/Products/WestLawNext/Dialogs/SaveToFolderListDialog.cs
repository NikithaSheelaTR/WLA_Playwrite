namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The save to folder list dialog.
    /// </summary>
    public class SaveToFolderListDialog : BaseModuleRegressionDialog
    {
        private const string FolderLinkLctMask =
            "//ul[contains(@class,'co_recentFoldersList ')]/li[@class='co_selectedTextMenuListItem']/button[contains(.,\"{0}\")]";

        private static readonly By ViewAllFoldersLinkLocator = By.XPath("//*[text()='View All Folders']");

        /// <summary>
        /// The is view all folders displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsViewAllFoldersDisplayed() => DriverExtensions.IsDisplayed(ViewAllFoldersLinkLocator);

        /// <summary>
        /// The save snippet to folder.
        /// </summary>
        /// <typeparam name="T"> PO type </typeparam>
        /// <param name="folderToSave"> The folder to save. </param>
        /// <returns> New instance of the page object </returns>
        public T SaveSnippetToFolder<T>(string folderToSave) where T : ICreatablePageObject =>
            this.ClickElement<T>(By.XPath(string.Format(FolderLinkLctMask, folderToSave)));

        /// <summary>
        /// The click view all folders.
        /// </summary>
        /// /// <typeparam name="T"> PO type </typeparam>
        /// <returns> New instance of the page object </returns>
        public T ClickViewAllFolders<T>() where T : SaveToFolderDialog
            => this.ClickElement<T>(ViewAllFoldersLinkLocator);
    }
}
