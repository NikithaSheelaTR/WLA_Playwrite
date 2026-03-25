namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Header
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Items;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Indigo Recent Folders Dialog is expanded in Header.
    /// </summary>
    public class EdgeRecentFoldersDialog : BaseEdgeHeaderDialog
    {
        private const string FolderLctMask = "//ul[@class='Folder-flex-navigation']/li[contains(.,\"{0}\")]";
        private const string FolderIndexLctMask = "//ul[@class='Folder-flex-navigation']/li[{0}]/span[@class='Folders-flex-navigationListItemText']";
        private const string FolderItemLctMask = "//li[contains(@class,'Folders-flex-navigationListItem') and contains(@aria-label, '{0}')]";
        private static readonly By FolderItemLocator = By.XPath("//ul[@class='Folder-flex-navigation']/li");
        private static readonly By HeaderLocator = By.XPath("//div[@class='co_globalNavDropdownBox_content']//h3");
        private static readonly By DisplayedContentLocator = By.XPath("//div[@class='Tab-viewAll']/parent::div//h3");
        private static readonly By FolderItemsLocator = By.XPath(".//ul[@class='Folder-data']/li");
        private static readonly By ContainerLocator = By.Id("co_recentFoldersContainer");
        private static readonly By CloseButtonLocator = By.XPath("//button[contains(@title,'Close folders')]");
        private static readonly By RecentFoldersContentLocator = By.ClassName("co_globalNavDropdownBox_content");
        private static readonly By ViewThisFolderButtonLocator = By.XPath(".//button[@id='viewThisFolderButton']");
        private static readonly By DragDropBoxTextLocator = By.XPath("//span[@id = 'co_dragDropBox_countText']");

        /// <summary>
        ///  Get list of recent documents
        /// </summary>
        /// <returns>A list of documents in the folder.</returns>
        public ItemsCollection<RecentFolderDocumentItem> RecentFolderDocumentItem => new ItemsCollection<RecentFolderDocumentItem>(this.Container, FolderItemsLocator);

        /// <summary>
        /// Close Button Locator
        /// </summary>
        public IButton CloseButton => new Button(CloseButtonLocator);

        /// <summary>
        /// Container
        /// </summary>
        protected override IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Gets names of all folders.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public List<string> GetFoldersNames() => DriverExtensions.GetElements(FolderItemLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Get header text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHeaderText() => DriverExtensions.WaitForElement(this.Container, HeaderLocator).Text;

        /// <summary>
        /// Get drag drop text locator
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDragDropBoxText() => DriverExtensions.WaitForElement(DragDropBoxTextLocator).Text;

        /// <summary>
        /// Get Folder IWebElement with the specific name
        /// To use only in DragAndDropToFolder
        /// </summary>
        /// <param name="folderName">The folder Name.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        internal IWebElement GetFolderElement(string folderName)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(FolderItemLctMask, folderName)));

        /// <summary>
        /// Gets no research content text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNoResearchContentMessageText() => DriverExtensions.WaitForElement(DisplayedContentLocator).Text;

        /// <summary>
        /// Click folder by name
        /// </summary>
        /// <param name="folderName"> Folder name </param>
        /// <returns>
        /// The <see cref="EdgeRecentFoldersDialog"/>.
        /// </returns>
        public EdgeRecentFoldersDialog ClickFolderByName(string folderName) =>
            this.ClickElement<EdgeRecentFoldersDialog>(By.XPath(string.Format(FolderLctMask, folderName)));

        /// <summary>
        /// Click view this folder button
        /// </summary>
        /// <returns></returns>
        public EdgeResearchOrganizerPage ClickViewThisFolderButton() =>
            this.ClickElement<EdgeResearchOrganizerPage>(ViewThisFolderButtonLocator);

        /// <summary>
        /// Gets title of a folder by it's index. First index is 1 
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFolderTitleByIndex(int index)
            => DriverExtensions.GetText(By.XPath(string.Format(FolderIndexLctMask, index)));

        /// <summary>
        /// Get items' names of a specific folder.
        /// </summary>
        /// <param name="folderName">
        /// folder name
        /// </param>
        /// <returns>Count of items the folder contains</returns>
        public int GetFolderItemsCountByName(string folderName)
        {
            this.ClickFolderByName(folderName);
            return RecentFolderDocumentItem.Count;
        }

        /// <summary>
        /// Check expected color of background 
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="color">Color</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsBackgroundHasExpectedColor(string folderName, string color) =>
            DriverExtensions.GetElement(By.XPath(string.Format(FolderLctMask, folderName))).GetComputedStylePropertyValue("background-color").Equals(color);

        /// <summary>
        /// is recent folders content displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRecentFoldersContentDisplayed() =>
            DriverExtensions.IsDisplayed(this.Container, RecentFoldersContentLocator);

        /// <summary>
        /// is view this folder button displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsViewThisFolderButtonDisplayed() =>
            DriverExtensions.IsDisplayed(this.Container, ViewThisFolderButtonLocator);
    }
}
