namespace Framework.Common.UI.Products.Shared.Components.Docket
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Docket;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Pages.Dockets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DocketsRequestsGridComponent
    /// </summary>
    public class DocketsRequestsGridComponent : BaseModuleRegressionComponent
    {
        private const string ColumnTitleLctMask = ".//th[contains(@class,'{0}')]";

        private const string ColumnLctMask = ".//td[@class='{0}']";

        private static readonly By CheckboxLocator = By.XPath(".//td[@class='co_detailsTable_select']/input");

        private static readonly By PdfLocator = By.XPath(".//td[@class='co_detailsTable_caseTitle']/a/img");

        private static readonly By DescriptionLinkLctMask = By.XPath(".//td[@class='co_detailsTable_caseTitle']/a[@href]");

        private static readonly By EmptyTableLocator = By.ClassName("empty");

        private static readonly By ContainerLocator
            = By.XPath("//div[@class='co_scrollWrapper' and (./*[@id='coid_browseConfig_DKT'] or ./*[@id='coid_browseConfig_PDF'])]");

        private EnumPropertyMapper<DocketsRequestsGridColumns, WebElementInfo> columnsMap;

        private EnumPropertyMapper<DocketsRequestsTables, WebElementInfo> tablesMap;

        /// <summary>
        /// Docket Requests Grid Columns mapper
        /// </summary>
        protected EnumPropertyMapper<DocketsRequestsGridColumns, WebElementInfo> ColumnsMap
            => this.columnsMap =
                    this.columnsMap ?? EnumPropertyModelCache.GetMap<DocketsRequestsGridColumns, WebElementInfo>();

        /// <summary>
        /// Docket Requests Tables mapper
        /// </summary>
        protected EnumPropertyMapper<DocketsRequestsTables, WebElementInfo> TablesMap
            => this.tablesMap =
                    this.tablesMap ?? EnumPropertyModelCache.GetMap<DocketsRequestsTables, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Verify that grid column is displayed
        /// </summary>
        /// <param name="gridColumn"> Grid Column. </param>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise  </returns>
        public bool IsColumnDisplayed(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
            => DriverExtensions.IsDisplayed(
                    By.Id(this.TablesMap[docketTable].Id),
                    By.XPath(string.Format(ColumnTitleLctMask, this.ColumnsMap[gridColumn].LocatorString)));

        /// <summary>
        /// Click on the grid column title
        /// </summary>
        /// <param name="gridColumn"> Grid column. </param>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns>Dockets Requests Page</returns>
        public DocketsRequestsPage ClickColumnTitle(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
        {
            this.GetColumnTitleElement(gridColumn, docketTable).Click();
            return new DocketsRequestsPage();
        }

        /// <summary>
        /// Is sorted by column
        /// </summary>
        /// <param name="gridColumn"> Grid Column to click. </param>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns>Dockets Requests Page</returns>
        public bool IsSortedByColumn(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
            => !this.GetColumnTitleElement(gridColumn, docketTable).GetAttribute("aria-sort").Equals("none");

        /// <summary>
        /// Get the text in the specified column of the given row
        /// </summary>
        /// <param name="gridColumn"> Column of the grid </param>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="index"> Row index. </param>
        /// <returns> Text in the column </returns>
        public string GetGridRowColumnTextByIndex(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable, int index)
            => this.GetGridColumn(gridColumn, docketTable)[index].Text;

        /// <summary>
        /// Get text from the all row for the specific column
        /// </summary>
        /// <param name="gridColumn"> Grid column. </param>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> List of row text </returns>
        public List<string> GetAllGridRowColumnsText(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
            => this.GetGridColumn(gridColumn, docketTable).Select(g => g.Text).ToList();

        /// <summary>
        /// Set PDF check-box by index
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="index"> Row index. </param>
        /// <param name="setTo"> True to check, false otherwise. </param>
        /// <returns>Dockets Requests Page</returns>
        public DocketsRequestsPage SetPdfCheckboxByIndex(DocketsRequestsTables docketTable, int index, bool setTo)
        {
            DriverExtensions.GetElement(DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), CheckboxLocator)[index]).SetCheckbox(setTo);
            return new DocketsRequestsPage();
        }

        /// <summary>
        /// Is checkbox selected by index
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="index"> Row index. </param>
        /// <returns> True if selected, false otherwise </returns>
        public bool IsCheckboxSelectedByIndex(DocketsRequestsTables docketTable, int index)
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), CheckboxLocator)[index]);
        
        /// <summary>
        /// Is description contains PDF by index
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="index"> Row index. </param>
        /// <returns> True if contains, false otherwise  </returns>
        public bool IsDescriptionContainsPdfByIndex(DocketsRequestsTables docketTable, int index)
            => DriverExtensions.IsDisplayed(DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), PdfLocator)[index]);

        /// <summary>
        /// Is description contains link by index
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="index"> Row index. </param>
        /// <returns> True if contains, false otherwise  </returns>
        public bool IsDescriptionContainsLinkByIndex(DocketsRequestsTables docketTable, int index)
            => DriverExtensions.IsDisplayed(DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), DescriptionLinkLctMask)[index]);

        /// <summary>
        /// Is check-box selected by index
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="indexArray"> Row index. </param>
        /// <returns> True if selected, false otherwise  </returns>
        public bool IsCheckBoxSelectedByIndex(DocketsRequestsTables docketTable, params int[] indexArray)
            => indexArray.ToList().TrueForAll(i => DriverExtensions.IsCheckboxSelected(DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), CheckboxLocator)[i]));

        /// <summary>
        /// Is empty table displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise  </returns>
        public bool IsEmptyTableDisplayed(DocketsRequestsTables docketTable)
            => DriverExtensions.IsDisplayed(By.Id(this.TablesMap[docketTable].Id), EmptyTableLocator);

        /// <summary>
        /// Get all values of checkboxes
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> The list of statuses. </returns>
        public List<bool> GetAllCheckboxesStatuses(DocketsRequestsTables docketTable)
            => DriverExtensions.GetElements(By.Id(this.TablesMap[docketTable].Id), CheckboxLocator).Select(x => DriverExtensions.IsCheckboxSelected(x)).ToList();

        /// <summary>
        /// Get empty table text
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        public string GetEmptyTableText(DocketsRequestsTables docketTable)
            => DriverExtensions.GetElement(By.Id(this.TablesMap[docketTable].Id), EmptyTableLocator).Text;

        /// <summary>
        /// Get grid column
        /// </summary>
        /// <returns> Elements list from the grid. </returns>
        /// <param name="gridColumn"> Column of the grid. </param>
        /// <param name="docketTable"> The docket Table. </param>
        private List<IWebElement> GetGridColumn(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
            =>
                DriverExtensions.GetElements(
                    By.Id(this.TablesMap[docketTable].Id),
                    By.XPath(string.Format(ColumnLctMask, this.ColumnsMap[gridColumn].LocatorString))).ToList();

        /// <summary>
        /// Get column title element
        /// </summary>
        /// <returns> Column title element </returns>
        /// <param name="gridColumn"> Column of the grid. </param>
        /// <param name="docketTable"> The docket Table. </param>
        private IWebElement GetColumnTitleElement(DocketsRequestsGridColumns gridColumn, DocketsRequestsTables docketTable)
            =>
                DriverExtensions.GetElement(
                    By.Id(this.TablesMap[docketTable].Id),
                    By.XPath(string.Format(ColumnTitleLctMask, this.ColumnsMap[gridColumn].LocatorString)));
    }
}