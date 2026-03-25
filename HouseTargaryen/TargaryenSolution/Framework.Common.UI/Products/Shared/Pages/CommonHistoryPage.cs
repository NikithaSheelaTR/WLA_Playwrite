namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// History page accessed from the "History" link on the top of the page
    /// </summary>
    public class CommonHistoryPage : CommonAuthenticatedWestlawNextPage, ICommonHistoryPage
    {
        private static readonly By HistoryTitleLocator = By.CssSelector("h1.co_historyTitle");

        private static readonly By MsgHourlyChargesSuspendedLocator
            = By.XPath("//div[@id='co_hourlyBillingSuspendedMessage' and contains(., 'Hourly charges are suspended while on this page.')]");

        /// <summary>
        /// Documents Table
        /// </summary>
        public FolderGridComponent DocumentsTable { get; } = new FolderGridComponent();

        /// <summary>
        /// History Table
        /// </summary>
        public HistoryGridComponent HistoryTable { get; } = new HistoryGridComponent();

        /// <summary>
        /// reference to the leftHistorySection 
        /// </summary>
        public LeftHistoryComponent LeftHistory { get; } = new LeftHistoryComponent();

        /// <summary>
        /// Toolbar
        /// </summary>
        public Toolbar Toolbar { get; } = new Toolbar();

        /// <summary>
        /// Gets History Title Text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetHistoryPageTitle() => DriverExtensions.WaitForElement(HistoryTitleLocator).Text;

        /// <summary>
        /// Checks whether the message of "Hourly charges are suspended while on this page" is displayed
        /// </summary>
        /// <returns>
        /// the flag whether or not the message is displayed.
        /// </returns>
        public bool IsMessageHourlyChargesDisplayed()
            => DriverExtensions.IsDisplayed(MsgHourlyChargesSuspendedLocator, 5);

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
                new RecentFoldersDialog().GetFolderElement(targetFolder),
                DriverExtensions.WaitForElement(By.PartialLinkText(gridItemTitle)), 
                copyOrMoveEnum);
        }
    }
}