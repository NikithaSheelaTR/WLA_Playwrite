namespace Framework.Common.UI.Products.Shared.Items.FolderHistory
{
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// History Event Component for All History
    /// </summary>
    public class AllHistoryTableItem : SearchesHistoryTableItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AllHistoryTableItem"/> class. 
        /// History Table Item
        /// </summary>
        /// <param name="tableEntryContainer">
        /// The table Entry Container.
        /// </param>
        public AllHistoryTableItem(IWebElement tableEntryContainer) : base(tableEntryContainer)
        {
        }

        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        public Events Event => DriverExtensions.SafeGetElement(this.Container, By.XPath(this.ColumnsMap[Columns.Event].LocatorString)).Text.GetEnumValueByText<Events>();
    }
}