namespace Framework.Common.UI.Products.WestlawEdge.Components.FolderHistory
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.FolderHistory;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.WestlawEdge.DropDowns;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    using TRGR.Quality.QedArsenal.QualityLibrary.WebDriver.Utils.Core;

    /// <summary>
    /// The center research organizer section for Indigo.
    /// </summary>
    public class EdgeFolderGridComponent : FolderGridComponent
    {
        private const string ImpliedOverrulingLctMask = "//td[@class='TableCell--Description' and .//*[contains(.,{0})]]//a[contains(@class, 'co_impliedOverrulingsFlagSm')]";
        private const string ColumnHeaderLctMask = ".//th[contains(@class,'{0}')]";
        private const string GridRowLctMask = "//div[@id='co_researchOrg_detailsTable']//tr[.//td[@class='{0}']]";

        private static readonly By ItemsLocator = By.XPath("//tbody//tr[contains(@id, 'datatable-row') and .//td[not(contains(@class, 'co_detailsTable_subHeader'))]]");
        private static readonly By TableLocator = By.XPath("//table[contains(@class,'Table a11yTableSortable')]");
        private static readonly By SortButtonLocator = By.XPath("//button[@class = 'a11yTableSortable-Button']");

        /// <summary>
        ///  Get list of edge folder grid items
        /// </summary>
        /// <returns>list of edge folder grid items</returns>
        public ItemsCollection<EdgeFolderGridItem> FolderGridItems => new ItemsCollection<EdgeFolderGridItem>(this.ComponentLocator, ItemsLocator);

        /// <summary>
        /// Date dropdown
        /// </summary>
        public DateDropdown DateDropdown =>
            new DateDropdown(
                DriverExtensions.GetElement(
                    By.XPath(
                        string.Format(
                            ColumnHeaderLctMask,
                            this.FolderGridColumnsMap[FolderPageGridColumns.Date].LocatorString))));

        /// <summary>
        /// Gets the FolderPageGridColumns enumeration to WebElementInfo map.
        /// </summary>
        protected override EnumPropertyMapper<FolderPageGridColumns, WebElementInfo> FolderGridColumnsMap
            => EnumPropertyModelCache.GetMap<FolderPageGridColumns, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Checks if the Implied overruling flag is displayed for the item with the given name
        /// </summary>
        /// <returns> True if flag is displayed, false otherwise. </returns>
        public bool IsImpliedOverrulingFlagDisplayedForItem(string itemName) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ImpliedOverrulingLctMask, itemName), 5);

        /// <summary>
        /// Sort grid by column
        /// </summary>
        /// <param name="gridColumn">grid column</param>
        /// <returns></returns>
        public EdgeResearchOrganizerPage SortGridByColumn(FolderPageGridColumns gridColumn)
        {
            DriverExtensions.GetElement(
                By.XPath(string.Format(ColumnHeaderLctMask, this.FolderGridColumnsMap[gridColumn].LocatorString)),
                SortButtonLocator).Click();

            return new EdgeResearchOrganizerPage();
        }

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public override List<FolderGridModel> GetGridModels() =>
            this.IsGridEmpty()
                ? new List<FolderGridModel>()
                : DriverExtensions.GetElements(ItemsLocator).Select(item => new EdgeFolderGridItem(item).ToModel<FolderGridModel>()).ToList();

        /// <summary>
        /// Clicks on specific column header
        /// </summary>
        /// <param name="gridColumn">Table's column</param>
        public override void ClickGridColumnHeader(FolderPageGridColumns gridColumn)
        {
            DriverExtensions.WaitForElement(
                                DriverExtensions.WaitForElement(TableLocator), By.XPath(string.Format(ColumnHeaderLctMask, this.FolderGridColumnsMap[gridColumn].LocatorString)))
                            .Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Find the grow row data based on searching for text in the given column.
        /// </summary>
        /// <param name="gridColumn">Column to look for the provided text.</param>
        /// <param name="columnText">Search text.</param>
        /// <returns>Element with the row data</returns>
        protected override IWebElement GetResultsGridRow(FolderPageGridColumns gridColumn, string columnText)
        {
            DriverExtensions.WaitForJavaScript();
            string locatorString = string.Format(GridRowLctMask, this.FolderGridColumnsMap[gridColumn].LocatorString);
            IReadOnlyCollection<IWebElement> elements = DriverExtensions.GetElements(By.XPath(locatorString));

            int i = 0;

            foreach (IWebElement e in elements)
            {
                i++;
                if (e.Text.Contains(columnText))
                {
                    return DriverExtensions.WaitForElement(By.XPath(locatorString + "[" + i + "]"));
                }
            }

            return null;
        }

        /// <summary>
        /// Add or Edit note
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName"></param>
        /// <param name="description"></param>
        /// <param name="actionsMenuOption"></param>
        /// <returns>PageInstance</returns>
        public T AddOrEditNote<T>(string itemName, string description, ActionsMenuOption actionsMenuOption) where T : ICreatablePageObject
        {
            var folderRow = this.FolderGridItems.FirstOrDefault(x => x.Title.Contains(itemName)) ?? this.FolderGridItems.FirstOrDefault(x => x.Summary.Contains(itemName));
            folderRow.ActionsMenu.SelectOption(actionsMenuOption);
            folderRow.AddNoteToItem.AddNoteTextbox.Clear();
            folderRow.AddNoteToItem.AddNoteTextbox.SendKeys(description);
            folderRow.AddNoteToItem.SaveButton.Click();

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}