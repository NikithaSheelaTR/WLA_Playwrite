namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.FolderHistory;
    using Framework.Common.UI.Products.WestlawEdge.Components.History;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Indigo History Page
    /// </summary>
    public class EdgeCommonHistoryPage : EdgeCommonAuthenticatedWestlawNextPage, ICommonHistoryPage
    {
        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// Gets the doc toolbar.
        /// </summary>
        public EdgeToolbarComponent EdgeToolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// Documents Table
        /// </summary>
        public EdgeFolderGridComponent DocumentsTable { get; } = new EdgeFolderGridComponent();

        /// <summary>
        /// History Table
        /// </summary>
        public EdgeHistoryGridComponent HistoryTable { get; } = new EdgeHistoryGridComponent();

        /// <summary>
        /// reference to the leftHistorySection 
        /// </summary>
        public LeftHistoryComponent LeftHistory { get; } = new LeftHistoryComponent();

        /// <summary>
        /// History Tab Panel
        /// TODO: After the Graphical History feature is released in August, all components and methods except the HistoryTabPanel should be moved to the CurrentHistoryTabComponent
        /// </summary>
        public HistoryTabPanel HistoryTabPanel { get; } = new HistoryTabPanel();

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="gridItemTitle">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        public void DragAndDropGridItemToRecentFolder(
            string targetFolder,
            string gridItemTitle,
            CopyOrMoveEnum copyOrMoveEnum = CopyOrMoveEnum.Copy)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)));

            this.DragAndDropToFolder(
                new EdgeRecentFoldersDialog().GetFolderElement(targetFolder),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)),
                copyOrMoveEnum);
        }
    }
}