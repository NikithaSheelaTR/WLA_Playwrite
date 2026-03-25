namespace Framework.Common.UI.Products.Shared.Items.FolderHistory
{
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Folder Grid Item for Research Organizer page
    /// </summary>
    public class FolderGridItem : BaseGridItem
    {
        private static readonly By CheckboxLocator = By.XPath(".//td[@class='co_detailsTable_select' or @class = 'TableCell--Select']/input");

        private static readonly By FolderItemLocator = By.XPath(".//div[normalize-space(@class)='co_tree_element co_tree_closedFolder']");

        /// <summary>
        /// Initializes a new instance of the <see cref="AllHistoryTableItem"/> class. 
        /// History Table Item
        /// </summary>
        /// <param name="tableEntryContainer">
        /// The table Entry Container.
        /// </param>
        public FolderGridItem(IWebElement tableEntryContainer)
            : base(tableEntryContainer)
        {
        }

        /// <summary>
        /// True if Checkbox selected, false otherwise
        /// </summary>
        public bool Checkbox => DriverExtensions.IsDisplayed(this.Container, CheckboxLocator)
                                    && DriverExtensions.WaitForElement(this.Container, CheckboxLocator).Selected;

        /// <summary>
        /// Get the type of and grid: document or folder
        /// </summary>
        public GridType Type => DriverExtensions.IsDisplayed(this.Container, FolderItemLocator) ? GridType.Folder : GridType.Document;

        /// <summary>
        /// Get the folder name
        /// </summary>
        public string FolderName
        {
            get
            {
                IWebElement webElement = DriverExtensions.SafeGetElement(this.Container, By.XPath(this.ColumnsMap[Columns.Folder].LocatorString));
                return webElement != null ? webElement.Text : string.Empty;
            }
        }
    }
}