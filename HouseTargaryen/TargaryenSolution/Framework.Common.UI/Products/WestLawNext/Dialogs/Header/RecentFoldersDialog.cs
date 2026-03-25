namespace Framework.Common.UI.Products.WestLawNext.Dialogs.Header
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.WestLawNext.Components.Header;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The Recent Folders Dialog is expanded in Header.
    /// </summary>
    public class RecentFoldersDialog : BaseModuleRegressionDialog
    {
        private const string FolderItemLctMask = "//div[@id='co_recentFoldersList']/button[a[contains(text(),\"{0}\")]]";

        /// <summary>
        /// Container Locator
        /// </summary>
        protected static readonly By ContainerLocator =
            By.XPath("//li[@id='co_recentFoldersContainer']//div[@class='co_dropdownBoxExpanded']");

        private static readonly By HeaderLocator = By.XPath(".//h2");

        private static readonly By ViewAllLinkLocator = By.LinkText("View all");

        private static readonly By FolderItemLocator = By.XPath("//div[@id='co_recentFoldersList']/button");

        private static readonly By HoverOverFolderMessageLocator =
            By.XPath(".//li[contains(@class,'co_recentItemsInital')]");

        private static readonly By RecentFoldersContentLocator = By.ClassName("co_globalNavDropdownBox_content");

        private static readonly By PopUpMessageLocator = By.XPath(
            "//*[contains(@class, 'co_foldering_popupMessageContainer')]//*[@class = 'co_infoBox_message']");

        private static readonly By DragDropBoxContentLocator = By.Id("co_dragDropBox_countText");

        private static readonly By CloseButtonLocator = By.XPath(
            "//button[@class='co_overlayBox_closeButton' and text()='Close Recent Folders window']");

        private static readonly By DisplayedContentLocator = By.XPath(
            "//div[contains(@id,'co_recentItemsListContainer') and @style='display: block;']");

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentFoldersDialog"/> class.
        /// </summary>
        public RecentFoldersDialog()
        {
            DriverExtensions.WaitForElementDisplayed(ContainerLocator);
        }

        private IWebElement Container => DriverExtensions.WaitForElement(ContainerLocator);

        /// <summary>
        /// Get header text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHeaderText() => DriverExtensions.WaitForElement(this.Container, HeaderLocator).Text;

        /// <summary>
        /// Get 'All Link' Text.
        /// </summary>
        /// <returns>string</returns>
        public string GetViewAllLinkText() => DriverExtensions.WaitForElement(this.Container, ViewAllLinkLocator).Text;

        /// <summary>
        /// Clicks 'View All' link
        /// </summary>
        /// <returns>New instance of ResearchOrganizerPage</returns>
        public ResearchOrganizerPage ClickViewAllLink() =>
            this.ClickElement<ResearchOrganizerPage>(
                DriverExtensions.WaitForElement(this.Container, ViewAllLinkLocator));

        /// <summary>
        /// Get items' names of a specific folder.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>List of items' names</returns>
        public List<string> GetFolderItemsNames(string name)
        {
            this.HoverFolder(name);
            DriverExtensions.WaitForElement(DisplayedContentLocator);
            return DriverExtensions.GetElements(this.Container, DisplayedContentLocator, By.XPath(".//ul/li"))
                                   .Select(item => item.Text).ToList();
        }

        /// <summary>
        /// Gets title of a folder by it's index. First index is 0
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFolderTitleByIndex(int index) => this.GetRecentFolderItems().ElementAt(index).LinkFolderName;

        /// <summary>
        /// Gets names of all folders.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<string> GetFoldersNames()
        {
            var list = new List<string>();
            this.GetRecentFolderItems().ToList().ForEach(u => list.Add(u.LinkFolderName));
            return list;
        }

        /// <summary>
        /// Gets no research content text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNoResearchContentMessageText() =>
            DriverExtensions.WaitForElement(DisplayedContentLocator).Text;

        /// <summary>
        /// Gets hover over folder message text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHoverOverFolderMessageText() =>
            DriverExtensions.WaitForElement(this.Container, HoverOverFolderMessageLocator).Text;

        /// <summary>
        /// is recent folders content displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsRecentFoldersContentDisplayed() =>
            DriverExtensions.IsDisplayed(RecentFoldersContentLocator, 5);

        /// <summary>
        /// is scroll bar displayed.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsScrollBarDisplayed(string name)
        {
            this.HoverFolder(name);
            IWebElement folderItemsList = DriverExtensions.GetElement(this.Container, DisplayedContentLocator);
            return DriverExtensions.WaitForElement(RecentFoldersContentLocator).Size.Height
                   < folderItemsList.GetElementScrollHight();
        }

        /// <summary>
        /// Get hover over folder message text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetDuplicateMessageText() => DriverExtensions.WaitForElementDisplayed(PopUpMessageLocator).Text;

        /// <summary>
        /// The get no research content text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetReviewerMessageText() =>
            DriverExtensions.WaitForElementDisplayed(DragDropBoxContentLocator).Text;

        /// <summary>
        /// Hover Folder by name
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <returns>The <see cref="RecentFoldersDialog"/>.</returns>
        public RecentFoldersDialog HoverFolder(string folderName)
        {
            this.GetRecentFolderItems().First(i => i.LinkFolderName.Equals(folderName)).Hover();
            return this;
        }

        /// <summary>
        /// Click folder by name
        /// </summary>
        /// <param name="folderName"> Folder name </param>
        public void ClickFolderByName(string folderName) =>
            this.GetRecentFolderItems().First(i => i.LinkFolderName.Equals(folderName)).Click();

        /// <summary>
        /// Check expected color of background 
        /// </summary>
        /// <param name="folderName">Folder name</param>
        /// <param name="color">Color</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsBackgroundHasExpectedColor(string folderName, string color) => this
            .GetRecentFolderItems().First(i => i.LinkFolderName.Equals(folderName)).IsHighlightedColorExpected(color);

        /// <summary>
        /// Click close button
        /// </summary>
        public void ClickCloseButton() => this.ClickElement(CloseButtonLocator);

        /// <summary>
        /// ClickFolderByIndex
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="index">index</param>
        /// <returns>New instance of T page</returns>
        public T ClickFolderByIndex<T>(int index) where T : ICreatablePageObject
        {
            this.GetRecentFolderItems().ElementAt(index).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Folder IWebElement with specific name
        /// To use only in DragAndDropToFolder
        /// </summary>
        /// <param name="folderName">The folder Name.</param>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public IWebElement GetFolderElement(string folderName)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(FolderItemLctMask, folderName)));

        /// <summary>
        /// Gets the names of the folder items.
        /// </summary>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private IEnumerable<RecentFolderItem> GetRecentFolderItems()
        {
            DriverExtensions.WaitForElement(FolderItemLocator);
            return DriverExtensions.GetElements(ContainerLocator, FolderItemLocator)
                                   .Select(item => new RecentFolderItem(item));
        }
    }
}