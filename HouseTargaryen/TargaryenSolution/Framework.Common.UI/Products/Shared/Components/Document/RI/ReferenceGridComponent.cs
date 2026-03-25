namespace Framework.Common.UI.Products.Shared.Components.Document.RI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Enums.RI;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Items.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// ReferenceGrid
    /// </summary>
    public class ReferenceGridComponent : BaseModuleRegressionComponent
    {
        private const string PaginationLinkLctMask = "//ul[@id='coid_navFooter_pagination']//a[@aria-label='Page {0}']";
        private const string GridRowTitleLctMask = "./td[{0}]//a[contains(@class, 'co_relatedInfo_grid_documentLink')]";
        private const string CitationLctMask = "./td[{0}]//span[contains(@class,'Citation') or contains(@class,'citation')]";
        private const string CheckboxLctMask = "./td[{0}]//input[@type='checkbox']";
        private const string RowDocketLctMask = "./td[{0}]//span[contains(@class, 'coid_relatedInfo_docket') or contains(@class, 'co_relatedInfo_docket')]";
        private const string RelatedInfoCourtLineLctMask = "./td[{0}]//span[contains(@class,'coid_relatedInfo_courtline_')]";
        private const string RowItemNumberLctMask = "./td[{0}]//span[contains(@id, 'coid_relatedInfo_resultList_rank') or contains(@class, 'co_relatedInfo_resultList_rank')]";
        private const string HighLightedTermLinkLctMask = "./td[{0}]//a[@class='co_snippet_link']";
        private const string HighLightedTermLctMask = "./td[{0}]//div[@class='co_snippet']//span[contains(@class, 'co_searchTerm')]";
        private const string DepthColumnDescendingClassName = "co_detailsTable_depth sortable descending";
        private const string DepthColumnAscendingClassName = "co_detailsTable_depth sortable ascending";
        private const string DateColumnDescendingClassName = "co_detailsTable_date sortable descending";
        private const string DateColumnAscendingClassName = "co_detailsTable_date sortable ascending";

        private static readonly By MainContentLoadingSpinnerLocator = By.CssSelector("#coid_relatedInfo_loading_spinner");
        private static readonly By FacetsLoadingSpinnerLocator = By.CssSelector(".co_progressIndicator");
        private static readonly By CitingRefResultLinkLocator = By.CssSelector(".co_relatedInfo_grid_documentLink");
        private static readonly By DepthTableHeaderLocator = By.XPath("//th[contains(@class,'co_detailsTable_depth')]");
        private static readonly By EmptyDepthBarLocator = By.XPath(".//td[@class='co_detailsTable_depth empty']//div[contains(@class,'co_td_empty')]");
        private static readonly By DepthBarLocator = By.XPath(".//td[@class='co_detailsTable_depth']//div[contains(@class,'co_relatedInfo_dotBar_')]");
        private static readonly By HeadNotesLocator = By.XPath(".//ol[@class='co_relatedInfo_grid_headnoteList']//li");
        private static readonly By DateTableHeaderLocator = By.XPath("//th[contains(@class, 'co_detailsTable_date')]");
        private static readonly By NegativeTreatmentIconLocator = By.XPath(".//*[@class='co_detailsTable_negativeStatus']");
        private static readonly By RedFlagIconLocator = By.XPath(".//img[contains(@src, 'flag_red_small.png')]");
        private static readonly By RedFlagLinkLocator = By.XPath(".//a[./img[contains(@src, 'flag_red_small.png')]]");
        private static readonly By AddedToFolderIconLocator = By.XPath(".//img[contains(@src, 'images/co_document_icon_foldered.png') and @title='Saved to Folder']");
        private static readonly By PreviousViewIconLocator = By.XPath(".//li[@class='co_document_icon_previouslyviewed']//img[contains(@src, 'images/co_document_icon_previouslyviewed.png')]");
        private static readonly By HoveredHeadNoteLocator = By.XPath("//div[@class='co_relatedInfo_grid_headnote']");
        private static readonly By ViewAllLinkLocator = By.LinkText("View all");
        private static readonly By TitleItemLocator = By.XPath("//td[contains(@class,'co_detailsTable_content')]");

        private static readonly By ResultListCheckboxLocator = By.XPath(".//input[contains(@id,'coid_relatedInfo_resultList_checkbox_')]");
        private static readonly By ItemsPerPageDropdownLocator = By.XPath("//div[@id='coid_navFooter_itemCountDiv']/select");
        private static readonly By FooterPaginationDropdownLocator = By.Id("coid_navFooter_pagination");
        private static readonly By FooterPaginationLinksLocator = By.XPath("id('coid_navFooter_pagination')//*[self::a[contains(@id, 'pageNum')] or self::strong]");
        private static readonly By PaginationFirstPageButtonLocator = By.XPath("//ul[@id='coid_navFooter_pagination']//a[@oldtitle='First Page']");
        private static readonly By PaginationNextPageButtonLocator = By.XPath("//ul[@id='coid_navFooter_pagination']//a[@oldtitle='Next Page']");
        private static readonly By PaginationPreviousPageButtonLocator = By.XPath("//ul[@id='coid_navFooter_pagination']//a[@oldtitle='Previous Page']");
        private static readonly By SelectAllCheckboxLocator = By.Id("coid_relatedinfo_results_select_all");
        private static readonly By OutOfPlanGridRowsLocator = By.XPath("//tr/td[@class='co_detailsTable_content']//div[@class='co_outOfPlanLabel']/ancestor::tr");

        private readonly string gridTableXpath;

        private EnumPropertyMapper<ReferenceGridColumns, WebElementInfo> columnsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => By.XPath(this.gridTableXpath);

        /// <summary>
        /// Initializes a new instance of the <see cref="ReferenceGridComponent"/> class. 
        /// </summary>
        /// <param name="gridTableXpath"> Locator to identify the table element for the grid </param>
        internal ReferenceGridComponent(string gridTableXpath)
        {
            this.gridTableXpath = gridTableXpath;
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Reference Grid Columns mapper
        /// </summary>
        protected EnumPropertyMapper<ReferenceGridColumns, WebElementInfo> ColumnsMap
            =>
                this.columnsMap =
                    this.columnsMap ?? EnumPropertyModelCache.GetMap<ReferenceGridColumns, WebElementInfo>();

        private SelectElement ItemsPerPageDropdown => new SelectElement(DriverExtensions.WaitForElement(ItemsPerPageDropdownLocator));

        /// <summary>
        /// Verify that grid column is displayed
        /// </summary>
        /// <param name="gridColumn"> Grid Column </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsColumnDisplayed(ReferenceGridColumns gridColumn)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(this.ColumnsMap[gridColumn].LocatorMask, this.gridTableXpath)), 5);

        /// <summary>
        /// Click on the grid column
        /// </summary>
        /// <param name="gridColumn"> Grid column </param>
        public void ClickColumn(ReferenceGridColumns gridColumn)
        {
            DriverExtensions.DoubleClick(DriverExtensions.WaitForElement(By.XPath(string.Format(this.ColumnsMap[gridColumn].LocatorMask, this.gridTableXpath))));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get Grid Column text
        /// </summary>
        /// <param name="gridColumn"> Grid Column to click </param>
        /// <returns> Grid Column text </returns>
        public string GetGridColumnText(ReferenceGridColumns gridColumn)
            => DriverExtensions.GetText(By.XPath(string.Format(this.ColumnsMap[gridColumn].LocatorMask, this.gridTableXpath)));

        /// <summary>
        /// Verify that 'Select All' checkbox is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSelectAllCheckboxDisplayed() => DriverExtensions.IsDisplayed(SelectAllCheckboxLocator);

        /// <summary>
        /// Verify that 'Select All' checkbox is selected
        /// </summary>
        /// <returns> True if selected, false otherwise </returns>
        public bool IsSelectAllCheckboxSelected() => DriverExtensions.IsCheckboxSelected(SelectAllCheckboxLocator);

        /// <summary>
        /// Is Citing References table element present
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTablePresent() => DriverExtensions.IsElementPresent(By.XPath(this.gridTableXpath));

        /// <summary>
        /// Is Citing References table Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsTableDisplayed() => DriverExtensions.GetElement(By.XPath(this.gridTableXpath)).IsDisplayed();

        /// <summary>
        /// Verify that Pagination First Page Button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPaginationFirstPageButtonDisplayed() => DriverExtensions.IsDisplayed(PaginationFirstPageButtonLocator, 5);

        /// <summary>
        /// Verify that Pagination Next Page Button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPaginationNextPageButtonDisplayed() => DriverExtensions.IsDisplayed(PaginationNextPageButtonLocator, 5);

        /// <summary>
        /// Verify that Pagination Previous Page Button is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPaginationPreviousPageButtonDisplayed() => DriverExtensions.IsDisplayed(PaginationPreviousPageButtonLocator, 5);

        /// <summary>
        /// Click on the Pagination Next Page Button
        /// </summary>
        public void ClickPaginationNextPageButton()
        {
            DriverExtensions.WaitForElement(PaginationNextPageButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify that Footer Pagination Dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsFooterPaginationDisplayed() => DriverExtensions.IsDisplayed(FooterPaginationDropdownLocator, 5);

        /// <summary>
        /// Get Per Page Dropdown Options
        /// </summary>
        /// <returns> Dropdown options </returns>
        public List<string> GetPerPageDropdownOptions() => this.ItemsPerPageDropdown.Options.Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get selected Items Per Page Dropdown option
        /// </summary>
        /// <returns> Selected option </returns>
        public string GetSelectedPerPageDropdownOption() => this.ItemsPerPageDropdown.SelectedOption.Text;

        /// <summary>
        /// Verify that Items Per Page Container is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsItemsPerPageDropdownDisplayed() => DriverExtensions.IsDisplayed(ItemsPerPageDropdownLocator, 5);

        /// <summary>
        /// Click on the result in the list of citing refs
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <param name="index"> Index of the item to click </param>
        /// <returns> New instance of the page </returns>
        public T ClickResultByIndex<T>(int index) where T : ICreatablePageObject
        {
            IList<IWebElement> elements = DriverExtensions.GetElements(CitingRefResultLinkLocator).ToList();
            DriverExtensions.Click(elements[index]);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// get on the result in the list of citing refs
        /// </summary>
        /// <param name="title"> title of the item to click </param>
        /// <returns> New instance of the page </returns>
        public ReferenceItem GetGridItemByTitle(string title)
        {
            IList<IWebElement> elements = DriverExtensions.GetElements(TitleItemLocator).ToList();
            IList<ReferenceItem> allItems = elements.Select(element => (ReferenceItem)Activator.CreateInstance(typeof(ReferenceItem), element)).ToList();
            return allItems.First(i => i.Title.Text.Equals(title));
        }

        /// <summary>
        /// Verify that grid row exists
        /// </summary>
        /// <param name="gridColumn"> Grid Column </param>
        /// <param name="columnText"> Column name </param>
        /// <returns> True if exist, false otherwise </returns>
        public bool IsGridRowExist(ReferenceGridColumns gridColumn, string columnText)
            => this.GetResultsGridRow(gridColumn, columnText) != null;

        /// <summary>
        /// Get Grid Rows Count
        /// </summary>
        /// <returns> Count of the Grid Row </returns>
        public int GetGridRowsCount() => this.GetGridRows().Count;

        /// <summary>
        /// Verify that row is selected
        /// </summary>
        /// <param name="gridRowIndex"> Row number </param>
        /// <returns> True if selected, false otherwise </returns>
        public bool IsGridRowSelected(int gridRowIndex) => this.GetGridRows()[gridRowIndex].Selected;

        /// <summary>
        /// Get text from the grid row for Negative Treatment
        /// </summary>
        /// <param name="gridColumns"> Grid Column </param>
        /// <param name="negativeTreatmentTitle"> Negative treatment title </param>
        /// <param name="index"> result number </param>
        /// <returns> Text from the row </returns>
        public string GetGridRowColumnTextForNegativeTreatment(ReferenceGridColumns gridColumns, string negativeTreatmentTitle, int index)
        {
            List<IWebElement> citingRefGridItems = this.GetGridRows();
            IWebElement citingRefGridItem = citingRefGridItems.Where(i => this.GetGridRowTitle(i) == negativeTreatmentTitle).ToList()[index];
            return this.GetGridRowColumnText(citingRefGridItem, gridColumns);
        }

        /// <summary>
        /// Get text form the grid row with red flag 
        /// </summary>
        /// <param name="gridColumns"> Grid column </param>
        /// <returns> Text from the row </returns>
        public string GetGridRowColumnTextForRedFlag(ReferenceGridColumns gridColumns)
            => this.GetGridRowColumnText(this.GetRedFlagElement(), gridColumns);

        /// <summary>
        /// Get the text in the specified column of the given row
        /// </summary>
        /// <param name="gridColumns"> Column of the grid </param>
        /// <param name="index"> Row index </param>
        /// <returns> Text in the column </returns>
        public string GetGridRowColumnTextByIndex(ReferenceGridColumns gridColumns, int index)
        {
            IWebElement gridRow = this.GetGridRows()[index];
            return this.GetGridRowColumnText(gridRow, gridColumns);
        }

        /// <summary>
        /// Get text from the all row for the specific column
        /// </summary>
        /// <param name="gridColumns"> Grid column </param>
        /// <returns> List of row text </returns>
        public List<string> GetAllGridRowColumnsText(ReferenceGridColumns gridColumns)
        {
            List<IWebElement> gridRows = this.GetGridRows();
            return gridRows.Select(g => this.GetGridRowColumnText(g, gridColumns)).ToList();
        }

        /// <summary>
        /// Get text from the row 
        /// </summary>
        /// <param name="gridColumns"> Grid column </param>
        /// <param name="text"> Column text </param>
        /// <returns> Text from the row </returns>
        public string GetGridRowColumnTextByName(ReferenceGridColumns gridColumns, string text)
        {
            IWebElement gridRow = this.GetResultsGridRow(ReferenceGridColumns.Title, text);
            return this.GetGridRowColumnText(gridRow, gridColumns);
        }

        /// <summary>
        /// Determine if the Sorting arrow is shown on the header of the Depth column
        /// </summary>
        /// <returns> True if the Sorting arrow is shown on the header of the Depth column, false otherwise </returns>
        public bool IsDepthSortingArrowExist()
        {
            string depthTableHeaderClassAttr = DriverExtensions.WaitForElement(DepthTableHeaderLocator).GetAttribute("class");
            return DepthColumnDescendingClassName.Contains(depthTableHeaderClassAttr)
                || depthTableHeaderClassAttr.Equals(DepthColumnAscendingClassName);
        }

        /// <summary>
        /// Determine if the Sorting arrow is shown on the header of the Date column
        /// </summary>
        /// <returns> True if the Sorting arrow is shown on the header of the Date column, false otherwise </returns>
        public bool IsDateSortingArrowExist()
        {
            string dateTableHeaderClassAttr = DriverExtensions.WaitForElement(DateTableHeaderLocator).GetAttribute("class");
            return DateColumnDescendingClassName.Contains(dateTableHeaderClassAttr)
                   || dateTableHeaderClassAttr.Equals(DateColumnAscendingClassName);
        }

        /// <summary>
        /// Verify that grid row contains negative treatment icon
        /// </summary>
        /// <param name="gridColumns"> Grid Columns </param>
        /// <param name="title"> Column title</param>
        /// <returns> True if icon is displayed, false otherwise </returns>
        public bool IsGridRowContainsNegativeTreatmentIcon(ReferenceGridColumns gridColumns, string title)
        {
            IWebElement gridRow = this.GetResultsGridRow(gridColumns, title);
            return this.IsGridRowContainsElement(gridRow, NegativeTreatmentIconLocator);
        }

        /// <summary>
        /// Verify that grid row contains negative treatment icon
        /// </summary>
        /// <param name="index"> Row number </param>
        /// <returns> True if icon is displayed for the specific row, false otherwise </returns>
        public bool IsGridRowContainsNegativeTreatmentIconByIndex(int index)
        {
            return this.IsGridRowContainsElement(this.GetGridRows()[index], NegativeTreatmentIconLocator);
        }

        /// <summary>
        /// Verify that all rows contain negative treatment icon 
        /// </summary>
        /// <returns> True if icon is displayed, false otherwise </returns>
        public bool IsAllGridRowContainsNegativeTreatmentIcon()
            => this.GetGridRows().All(g => this.IsGridRowContainsElement(g, NegativeTreatmentIconLocator));

        /// <summary>
        /// Verify that all rows don't contain negative treatment icon
        /// </summary>
        /// <returns> True if icon is missing, false otherwise </returns>
        public bool IsAllGridDontRowContainsNegativeTreatmentIcon()
            => this.GetGridRows().All(g => !this.IsGridRowContainsElement(g, NegativeTreatmentIconLocator));

        /// <summary>
        /// Verify that at least one row contains negative treatment icon
        /// </summary>
        /// <returns> True if at least one icon is displayed, false otherwise </returns>
        public bool IsAnyGridRowContainsNegativeTreatmentIcon()
            => this.GetGridRows().Any(g => this.IsGridRowContainsElement(g, NegativeTreatmentIconLocator));

        /// <summary>
        /// Verify that at least one row doesn't contain negative treatment icon
        /// </summary>
        /// <returns> True if at least one icon is missing, false otherwise </returns>
        public bool IsAnyGridDontRowContainsNegativeTreatmentIcon()
            => this.GetGridRows().Any(g => !this.IsGridRowContainsElement(g, NegativeTreatmentIconLocator));

        /// <summary>
        /// Verify that grid row contains red flag
        /// </summary>
        /// <param name="gridColumns"> Grid column </param>
        /// <param name="title"> Column title </param>
        /// <returns> True if red flag is displayed, false otherwise </returns>
        public bool IsGridRowContainsRedFlag(ReferenceGridColumns gridColumns, string title)
        {
            IWebElement gridRow = this.GetResultsGridRow(gridColumns, title);
            return this.IsGridRowContainsElement(gridRow, RedFlagIconLocator);
        }

        /// <summary>
        /// Click on the red flag image in the given row.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="gridRowIndex">Row index in the results grid</param>
        /// <returns>The new instance of T page</returns>
        public T ClickGridRowRedFlagByIndex<T>(int gridRowIndex) where T : ICreatablePageObject
        {
            IWebElement gridRow = this.GetGridRows()[gridRowIndex];
            if (this.IsGridRowContainsRedFlag(gridRow))
            {
                DriverExtensions.GetElement(gridRow, RedFlagLinkLocator).Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that the grid row contains the 'Saved to Folder' icon
        /// </summary>
        /// <returns> True if icon exists, false otherwise </returns>
        public bool IsGridRowWithRedFlagContainsAddedToFolderIcon()
            => this.IsGridRowContainsElement(this.GetRedFlagElement(), AddedToFolderIconLocator);

        /// <summary>
        /// Verify that grid row contains previously viewed icon
        /// </summary>
        /// <param name="gridColumn"> Grid column </param>
        /// <param name="title"> Title </param>
        /// <returns> True if previously viewed icon is displayed, false otherwise </returns>
        public bool IsGridRowContainsPreviouslyViewIcon(ReferenceGridColumns gridColumn, string title)
            => this.IsGridRowContainsElement(this.GetResultsGridRow(gridColumn, title), PreviousViewIconLocator);

        /// <summary>
        /// Verify that grid row contains previously viewed icon
        /// </summary>
        /// <param name="rowIndex"> Row number </param>
        /// <returns> True if previously viewed icon is displayed, false otherwise </returns>
        public bool IsGridRowContainsPreviouslyViewIconByIndex(int rowIndex)
            => this.IsGridRowContainsElement(this.GetGridRows()[rowIndex], PreviousViewIconLocator);

        /// <summary>
        /// Get Grid Row title
        /// </summary>
        /// <param name="index"> row index </param>
        /// <returns> Title </returns>
        public string GetGridRowTitleByIndex(int index) => this.GetGridRowTitle(this.GetGridRows()[index]);

        /// <summary>
        /// Get grid row title by name
        /// </summary>
        /// <param name="name"> name </param>
        /// <returns> Title </returns>
        public string GetGridRowTitleByName(string name)
        {
            IWebElement gridRow = this.GetGridRows().FirstOrDefault(i => this.GetGridRowTitle(i).Equals(name));
            return this.GetGridRowTitle(gridRow);
        }

        /// <summary>
        /// Get grid row text
        /// </summary>
        /// <returns> Grid row text </returns>
        public string GetGridRowTitleForRedFlag() => this.GetGridRowTitle(this.GetRedFlagElement());

        /// <summary>
        /// Get citation list
        /// </summary>
        /// <param name="gridColumn"> Grid column </param>
        /// <param name="text"> text </param>
        /// <returns> Citation list </returns>
        public List<string> GetGridRowCitationList(ReferenceGridColumns gridColumn, string text)
            => this.GetGridRowCitationList(this.GetResultsGridRow(gridColumn, text));

        /// <summary>
        /// Get citation list
        /// </summary>
        /// <returns> Citation list </returns>
        public List<string> GetGridRowCitationListForRedGlag() => this.GetGridRowCitationList(this.GetRedFlagElement());

        /// <summary>
        /// Get citation list
        /// </summary>
        /// <param name="index"> Row index </param>
        /// <returns> Citation list </returns>
        public List<string> GetGridRowCitationListByIndex(int index)
            => this.GetGridRowCitationList(this.GetGridRows()[index]);

        /// <summary>
        /// Get citation in the Title column by the name of the given row
        /// </summary>
        /// <param name="name"> Row name </param>
        /// <returns> Citation title </returns>
        public string GetGridRowCitationTextByName(string name)
        {
            IWebElement gridRow = this.GetGridRows().FirstOrDefault(i => this.GetGridRowTitle(i).Equals(name));
            return this.GetGridRowCitationText(gridRow);
        }

        /// <summary>
        /// Get citation in the Title column by the name of the given row
        /// </summary>
        /// <param name="index"> Row index </param>
        /// <returns> Citation title </returns>
        public string GetGridRowCitationTextByIndex(int index)
        {
            IWebElement gridRow = this.GetGridRows()[index];
            return this.GetGridRowCitationText(gridRow);
        }

        /// <summary>
        /// Get count of bar number
        /// </summary>
        /// <param name="depthNumber"> Depth number </param>
        /// <returns> Count of bar number </returns>
        public int GetGetCountBarNumberByDepth(int depthNumber)
            => this.GetGridRows().Count(g => this.GetDepthColumnBarNumber(g).Equals(depthNumber));

        /// <summary>
        /// Get depth of bar number
        /// </summary>
        /// <param name="gridColumn"> Grid Column </param>
        /// <param name="title"> Title </param>
        /// <returns> Depth of bar number </returns>
        public int GetDepthColumnBarNumber(ReferenceGridColumns gridColumn, string title)
            => this.GetDepthColumnBarNumber(this.GetResultsGridRow(gridColumn, title));

        /// <summary>
        /// Get List of the depth of bar number
        /// </summary>
        /// <returns> Depth list </returns>
        public List<int> GetListDepthColumnBarNumber()
            => this.GetGridRows().Select(this.GetDepthColumnBarNumber).ToList();

        /// <summary>
        /// Get depth of bar number 
        /// </summary>
        /// <param name="index"> Row index </param>
        /// <returns> Depth of bar number </returns>
        public int GetDepthColumnBarNumberByIndex(int index) => this.GetDepthColumnBarNumber(this.GetGridRows()[index]);

        /// <summary>
        /// Get headnotes count
        /// </summary>
        /// <param name="gridColumn"> Grid Column </param>
        /// <param name="title"> Title </param>
        /// <returns> Headnotes count </returns>
        public int GetHeadNotesCount(ReferenceGridColumns gridColumn, string title)
            => this.GetHeadNotesList(this.GetResultsGridRow(gridColumn, title)).Count;

        /// <summary>
        /// Hover headnote element
        /// </summary>
        /// <param name="gridColumn"> Grid Column </param>
        /// <param name="title"> Title </param>
        /// <param name="index"> Index </param>
        public void HoverHeadNoteElement(ReferenceGridColumns gridColumn, string title, int index)
            => this.GetHeadNotesList(this.GetResultsGridRow(gridColumn, title))[index].Hover();

        /// <summary>
        /// Hover headnote element
        /// </summary>
        /// <param name="rowIndex"> Row number </param>
        /// <param name="itemIndex"> Item number </param>
        public void HoverHeadNoteElement(int rowIndex, int itemIndex)
            => this.GetHeadNotesList(this.GetGridRows()[rowIndex])[itemIndex].SeleniumHover();

        /// <summary>
        /// Get headnote text
        /// </summary>
        /// <param name="rowIndex"> Row number </param>
        /// <returns> Headnote text </returns>
        public List<string> GetHeadnotesText(int rowIndex)
            => this.GetHeadNotesList(this.GetGridRows()[rowIndex]).Select(item => item.Text).ToList();

        /// <summary>
        /// Click on the headnote
        /// </summary>
        /// <param name="rowIndex"> Row index </param>
        /// <param name="textInRow"> Text in row </param>
        /// <param name="headnoteNumber"> Number of headnote </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickHeadnote<T>(int rowIndex, string textInRow, int headnoteNumber) where T : ICreatablePageObject
        {
            IWebElement element = this.GetHeadNotesList(this.GetGridRows()[rowIndex]).Where(h => h.Text.Substring(0, 2) == textInRow).ToList()[headnoteNumber];
            element.Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the headnote element
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="gridColumn"> Grid Column </param>
        /// <param name="title"> Title </param>
        /// <param name="index"> Row number </param>
        /// <returns> New instance of the page </returns>
        public T ClickHeadNoteElement<T>(ReferenceGridColumns gridColumn, string title, int index) where T : ICreatablePageObject
        {
            this.GetHeadNotesList(this.GetResultsGridRow(gridColumn, title))[index].Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the text found in the box that appears when a head note is hovered over. 
        /// An empty string will be returned if no head note in the grid is hovered over.
        /// </summary>
        /// <returns> text </returns>
        public string GetHeadNoteHoveredText() => DriverExtensions.WaitForElement(HoveredHeadNoteLocator).Text;

        /// <summary>
        /// Verify that Hovered Head Note is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsHeadNoteHoveredComponentDisplayed() => DriverExtensions.IsDisplayed(HoveredHeadNoteLocator, 5);

        /// <summary>
        /// Click on the checkbox in the grid row.
        /// </summary>
        /// <param name="index"> Grid row index </param>
        public void ClickGridRowCheckBoxByIndex(int index) => this.ClickGridRowCheckBox(this.GetGridRows()[index]);

        /// <summary>
        ///  Checks depending on the parameters being transferred, either a random check box  of any document
        /// or a check box of a random document is selected without marking out of the plan .
        /// <param name="state"> checkBox check state </param>
        /// <param name="includeOutOfPlan"> include checkBoxes related to out of plan results </param>
        /// </summary>
        public void SelectRandomCheckBox(bool state = true, bool includeOutOfPlan = false)
        {
            var notOutOfPlanGridRows = includeOutOfPlan
                                           ? this.GetGridRows()
                                           : this.GetGridRows()
                                                 .Except(DriverExtensions.GetElements(OutOfPlanGridRowsLocator)).Where(
                                                     elem => !DriverExtensions.GetElement(elem, ResultListCheckboxLocator).Selected);
            DriverExtensions.WaitForElement(notOutOfPlanGridRows.ElementAt(new Random().Next(0, notOutOfPlanGridRows.Count() - 1)), ResultListCheckboxLocator).SetCheckbox(state);
        }

        /// <summary>
        /// Click on the checkbox in the grid row.
        /// </summary>
        public void ClickGridRowCheckBoxWithRedFlag() => this.ClickGridRowCheckBox(this.GetGridRows().Find(this.IsGridRowContainsRedFlag));

        /// <summary>
        /// Is Grid Row Contains View All By Index
        /// </summary>
        /// <param name="gridRowIndex"> Row index in the results grid </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsGridRowContainsViewAllByIndex(int gridRowIndex)
            => DriverExtensions.IsDisplayed(this.GetGridRows()[gridRowIndex], ViewAllLinkLocator);

        /// <summary>
        /// Click View All Link By Index
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="gridRowIndex">Row index in the results grid</param>
        /// <returns>The new instance of T page</returns>
        public T ClickViewAllLinkByIndex<T>(int gridRowIndex) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(this.GetGridRows()[gridRowIndex], ViewAllLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that all checkboxes are checked
        /// </summary>
        /// <returns> True if all checkboxes are checked, false otherwise </returns>
        public bool AreAllGridRowCheckBoxesChecked() => DriverExtensions.GetElements(this.ComponentLocator, ResultListCheckboxLocator).ToList().TrueForAll(ch => ch.Selected);

        /// <summary>
        /// Verify that all checkboxes are not checked
        /// </summary>
        /// <returns> True if all checkboxes are not checked, false otherwise </returns>
        public bool AreAllGridRowCheckBoxesNotChecked() => DriverExtensions.GetElements(this.ComponentLocator, ResultListCheckboxLocator).ToList().TrueForAll(ch => !ch.Selected);

        /// <summary>
        /// Verify that checkbox is checked
        /// </summary>
        /// <param name="rowIndex"> Row number </param>
        /// <returns> True if checkbox is checked, false otherwise </returns>
        public bool IsGridRowCheckBoxCheckedByIndex(int rowIndex)
            => this.IsGridRowCheckBoxChecked(this.GetGridRows()[rowIndex]);

        /// <summary>
        /// Verify that grid row contains checkbox
        /// </summary>
        /// <param name="gridRowIndex"> Row number </param>
        /// <returns> True if checkbox is displayed, false otherwise </returns>
        public bool IsGridRowHasCheckbox(int gridRowIndex) => this.GridRowHasCheckbox(this.GetGridRows()[gridRowIndex]);

        /// <summary>
        /// Get the Number in the title column indicating the number of this item in the whole collection of items.
        /// </summary>
        /// <param name="gridRowNumber"> Row in the grid </param>
        /// <returns> Item number </returns>
        public int GetGridRowItemNumber(int gridRowNumber)
        {
            By xPath = By.XPath(string.Format(RowItemNumberLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            List<IWebElement> gridRows = this.GetGridRows();
            return gridRowNumber <= gridRows.Count ? this.GetGridRowItemCount(gridRows[gridRowNumber - 1], xPath) : 0;
        }

        /// <summary>
        /// Get the Number in the title column indicating the number of this item in the whole collection of items.
        /// </summary>
        /// <param name="gridRow"> Row in the grid </param>
        /// <returns> Item number </returns>
        public int GetGridRowItemNumber(IWebElement gridRow)
        {
            By xPath = By.XPath(string.Format(RowItemNumberLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            return this.GetGridRowItemCount(gridRow, xPath);
        }

        /// <summary>
        /// Get docket list
        /// </summary>
        /// <param name="index"> Row index </param>
        /// <returns> Docket list </returns>
        public List<string> GetGridRowDocketListByIndex(int index) => this.GetGridRowDocketList(this.GetGridRows()[index]);

        /// <summary>
        /// Get docket list
        /// </summary>
        /// <param name="title"> Title </param>
        /// <param name="index"> Row index </param>
        /// <returns> Docket list </returns>
        public List<string> GetGridRowDocketListByIndexByTitle(string title, int index)
        {
            IWebElement citingRefGridItem =
                this.GetGridRows().Where(i => this.GetGridRowTitle(i).Equals(title)).ToList()[index];
            return this.GetGridRowDocketList(citingRefGridItem);
        }

        /// <summary>
        /// Get the list of Related Info Courtlines
        /// </summary>
        /// <returns>The list of Courtlines</returns>
        public List<string> GetCourtlineList() => this.GetGridRows().Select(row => this.GetGridRowCourtlineText(row)).ToList();

        /// <summary>
        /// Click on the highlighted term
        /// </summary>
        /// <param name="gridColumns"> Grid column </param>
        /// <param name="title"> Title </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickHighLightedTermFromGridRow<T>(ReferenceGridColumns gridColumns, string title) where T : ICreatablePageObject
            => this.ClickHighLightedTermFromGridRow<T>(this.GetResultsGridRow(gridColumns, title));

        /// <summary>
        /// Click on the highlighted term
        /// </summary>
        /// <param name="index"> Row index </param>
        /// <typeparam name="T">Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickHighLightedTermFromGridRowByIndex<T>(int index) where T : ICreatablePageObject
            => this.ClickHighLightedTermFromGridRow<T>(this.GetGridRows()[index]);

        /// <summary>
        /// Verify that all rows contain highlighted text
        /// </summary>
        /// <param name="highLightedText"> Highlighted text </param>
        /// <returns> True if all rows contain highlighted text, false otherwise </returns>
        public bool AreAllRowContainsHighlightedText(string highLightedText)
            => this.GetGridRows().TrueForAll(r => this.GridRowContainsHighlightedText(r, highLightedText));

        /// <summary>
        /// Verify that row contains highlighted text
        /// </summary>
        /// <param name="highLightedText"> Highlighted text </param>
        /// <returns> True if row contains highlighted text, false otherwise </returns>
        public bool IsHighlightedTextWithinRowExist(string highLightedText)
            => this.GetGridRows().Any(r => this.GridRowContainsHighlightedText(r, highLightedText));

        /// <summary>
        /// Verify that row contains highlighted text
        /// </summary>
        /// <param name="gridColumn"> Grid column </param>
        /// <param name="title"> Title </param>
        /// <param name="highLightedText"> Highlighted text </param>
        /// <returns> True if row contains highlighted text, false otherwise </returns>
        public bool IsHighlightedTextWithinRowExist(ReferenceGridColumns gridColumn, string title, string highLightedText)
            => this.GridRowContainsHighlightedText(this.GetResultsGridRow(gridColumn, title), highLightedText);

        /// <summary>
        /// Verify that PDF element is displayed
        /// </summary>
        /// <param name="rowIndex"> row index</param>
        /// <returns> True if element exist, false otherwise </returns>
        public bool IsGridRowPdfElementExist(int rowIndex)
        {
            IWebElement elementRow = this.GetGridRows()[rowIndex];
            return this.GetGridRowPdfElement(elementRow) != null;
        }

        /// <summary>
        /// Verify that all rows don't contains PDF element
        /// </summary>
        /// <returns> True if PDF elements is not displayed </returns>
        public bool IsAllGridRowPdfElementNotExist()
        {
            bool isNotExist = true;
            int count = this.GetGridRows().Count;
            for (int i = 0; i < count; i++)
            {
                isNotExist &= !this.IsGridRowPdfElementExist(i);
            }

            return isNotExist;
        }

        /// <summary>
        /// Click on the PDF element 
        /// </summary>
        /// <param name="elementIndex"> Row index </param>
        /// <param name="tabName"> Row index </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickGridRowPdfElement<T>(int elementIndex, string tabName) where T : ICreatablePageObject =>
                this.ClickAndOpenNewBrowserTab<T>(this.GetGridRowPdfElement(this.GetGridRows()[elementIndex]), tabName);

        /// <summary>
        /// Return the currently selected page from the footer pagination
        /// </summary>
        /// <returns>Integer indicating the current page in the grid.</returns>
        public int GetCurrentPageFromFooterPagination()
        {
            IWebElement webElement = DriverExtensions.WaitForElement(FooterPaginationDropdownLocator);
            IWebElement currentPageElement = DriverExtensions.GetElement(webElement, By.XPath(".//li//strong"));
            return currentPageElement.Text.ConvertCountToInt();
        }

        /// <summary>
        /// Return a list of elements for all the number links for the paging portion at the bottom of the page. 
        /// The number not returned will be the current page which will not have a long but will be a bolded number.
        /// </summary>
        /// <returns>List of elements for the paging links</returns>
        public int GetFooterPaginationLinksCount()
        {
            DriverExtensions.WaitForElement(FooterPaginationDropdownLocator);
            return DriverExtensions.GetElements(FooterPaginationLinksLocator).Count;
        }

        /// <summary>
        /// Click on the pagination number link in the bottom of the page.
        /// </summary>
        /// <param name="pageNumber">link to click</param>
        public void ClickFooterPaginationLink(int pageNumber)
        {
            DriverExtensions.WaitForElement(FooterPaginationDropdownLocator);
            DriverExtensions.WaitForElement(By.XPath(string.Format(PaginationLinkLctMask, pageNumber))).Click();
            this.WaitForPageLoad();
        }

        /// <summary>
        /// Set the items per page in the results grid.
        /// </summary>
        /// <param name="itemsPerPage" >Items per page to set. </param>
        public void SetItemsPerPage(ItemsPerPageOptions itemsPerPage)
        {
            this.ItemsPerPageDropdown.SelectByValue(((int)itemsPerPage).ToString());
            this.WaitForPageLoad();
        }

        /// <summary>
        /// Determines if the row is scrolled in view to the user in the browser window
        /// </summary>
        /// <param name="rowIndex"> Row to verify </param>
        /// <returns> True if element in view, false otherwise </returns>
        public bool IsRowInView(int rowIndex) => this.GetGridRows()[rowIndex].IsElementInView();

        /// <summary>
        /// Get the column index. 1 based index for XPATH
        /// </summary>
        /// <param name="gridColumn"> Column </param>
        /// <returns> Index </returns>
        public int GetColumnIndex(ReferenceGridColumns gridColumn)
        {
            int index = this.GetHeaderColumnIndex(By.XPath(string.Format(this.ColumnsMap[gridColumn].LocatorMask, this.gridTableXpath)));
            return index + 1;
        }

        /// <summary>
        /// Get GridItem RowIndex By Title
        /// </summary>
        /// <param name="title">title name</param>
        /// <returns>row index</returns>
        public int GetGridItemRowIndexByTitle(string title)
        {
            IList<IWebElement> elements = DriverExtensions.GetElements(TitleItemLocator).ToList();
            IList<ReferenceItem> allItems = elements
                                            .Select(
                                                element => (ReferenceItem)Activator.CreateInstance(
                                                    typeof(ReferenceItem),
                                                    element)).ToList();
            return allItems.IndexOf(allItems.First(i => i.Title.Text.Equals(title)));
            
        }

        /// <summary>
        /// Waits for the page to be loaded.
        /// </summary>
        private void WaitForPageLoad()
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementNotDisplayed(60000, MainContentLoadingSpinnerLocator);
            DriverExtensions.WaitForElementNotDisplayed(60000, FacetsLoadingSpinnerLocator);
        }

        private bool IsGridRowContainsElement(IWebElement gridRow, By elementLocator)
        {
            IWebElement icon;
            DriverExtensions.TryGetElement(gridRow, elementLocator, out icon);
            return icon != null;
        }

        private int GetGridRowItemCount(IWebElement gridRow, By itemLocator)
        {
            IWebElement e = DriverExtensions.GetElement(gridRow, itemLocator);
            return e.Text.ConvertCountToInt();
        }

        /// <summary>
        /// Click on the checkbox in the grid row.
        /// </summary>
        /// <param name="gridRow"> Grid row to click </param>
        private void ClickGridRowCheckBox(IWebElement gridRow)
        {
            IWebElement checkbox;
            DriverExtensions.TryGetElement(gridRow, ResultListCheckboxLocator, out checkbox);
            checkbox?.Click();
        }

        /// <summary>
        /// Get citation in the Title column of the given row
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <returns> Citations string </returns>
        private string GetGridRowCitationText(IWebElement gridRow)
        {
            string xPath = string.Format(CitationLctMask, this.GetColumnIndex(ReferenceGridColumns.Title));
            return DriverExtensions.GetElement(gridRow, By.XPath(xPath)).Text;
        }

        /// <summary>
        /// Return the element for the PDF link
        /// </summary>
        /// <param name="gridRow">Row in the grid</param>
        /// <returns>PDF element for the link</returns>
        private IWebElement GetGridRowPdfElement(IWebElement gridRow)
        {
            By pdfElementLocator = By.XPath($"./td[{this.GetColumnIndex(ReferenceGridColumns.Pdf)}]/a");
            IWebElement pdfElement;
            DriverExtensions.TryGetElement(gridRow, pdfElementLocator, out pdfElement);
            string href = pdfElement?.GetAttribute("href");

            By newElementLocator = By.XPath($"//td[{this.GetColumnIndex(ReferenceGridColumns.Pdf)}]/a[@href='{href}']");
            return href != null ? DriverExtensions.WaitForElement(newElementLocator) : null;
        }

        /// <summary>
        /// Get the text in the specified column of the given row
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <param name="gridColumns"> Column of the grid </param> 
        /// <returns> Text in the column </returns>
        private string GetGridRowColumnText(IWebElement gridRow, ReferenceGridColumns gridColumns)
        {
            IWebElement webElement = DriverExtensions.GetElement(gridRow);
            IWebElement e = DriverExtensions.GetElement(webElement, By.XPath($"./td[{this.GetColumnIndex(gridColumns)}]"));
            return e != null ? e.Text : string.Empty;
        }

        /// <summary>
        /// Verify that the row has the red flag image.
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <returns> True if red flag exists, false otherwise </returns>
        private bool IsGridRowContainsRedFlag(IWebElement gridRow) => this.IsGridRowContainsElement(gridRow, RedFlagIconLocator);

        private IWebElement GetRedFlagElement() => this.GetGridRows().Find(this.IsGridRowContainsRedFlag);

        /// <summary>
        /// Get the docket for the given grid row
        /// </summary>
        /// <param name="gridRow"> Grid row </param>
        /// <returns> List of docket info </returns>
        private List<string> GetGridRowDocketList(IWebElement gridRow)
        {
            By xPath = By.XPath(string.Format(RowDocketLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            IList<IWebElement> citationElements = DriverExtensions.GetElements(gridRow, xPath);
            return citationElements.Select(c => c?.Text).ToList();
        }

        /// <summary>
        /// Get the docket for the given grid row
        /// </summary>
        /// <param name="gridRow"> Grid row </param>
        /// <returns> List of docket info </returns>
        private string GetGridRowCourtlineText(IWebElement gridRow)
            => DriverExtensions.GetElement(gridRow, By.XPath(string.Format(RelatedInfoCourtLineLctMask, this.GetColumnIndex(ReferenceGridColumns.Title))))?.Text;

        /// <summary>
        /// Get the citations in the Title column of the given row
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <returns> List of citations </returns>
        private List<string> GetGridRowCitationList(IWebElement gridRow)
        {
            By xPath = By.XPath(string.Format(CitationLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));

            var results = new List<string>();
            IWebElement webElement = DriverExtensions.GetElement(gridRow);
            IWebElement citationElement = DriverExtensions.GetElement(webElement, xPath);
            string[] citationParts = citationElement.Text.Split(',');

            for (int x = 0; x < citationParts.Length; x++)
            {
                if (x % 2 == 0)
                {
                    results.Add(citationParts[x]);
                }
            }

            return results;
        }

        /// <summary>
        /// Find the depth value for the rows as defined by the number of bars displayed.
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <returns> Number of bars </returns>
        private int GetDepthColumnBarNumber(IWebElement gridRow)
        {
            IWebElement element;
            int count;
            DriverExtensions.TryGetElement(gridRow, EmptyDepthBarLocator, out element);
            if (element != null)
            {
                count = 0;
            }
            else
            {
                element = DriverExtensions.GetElement(gridRow, DepthBarLocator);
                count = element.GetAttribute("class").ConvertCountToInt();
            }

            return count;
        }

        /// <summary>
        /// Get the title in the Title column of the given row
        /// </summary>
        /// <param name="gridRow"> Row in the results grid </param>
        /// <returns> Title </returns>
        private string GetGridRowTitle(IWebElement gridRow)
        {
            By xPath = By.XPath(string.Format(GridRowTitleLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            IWebElement titleElement;
            DriverExtensions.TryGetElement(gridRow, xPath, out titleElement);
            return titleElement?.Text;
        }

        /// <summary>
        /// Find the collection of head notes in the given row.
        /// </summary>
        /// <param name="gridRow">Row to look in.</param>
        /// <returns>List of head note elements</returns>
        private List<IWebElement> GetHeadNotesList(IWebElement gridRow)
        {
            IWebElement webElement = DriverExtensions.GetElement(gridRow);
            List<string> headNotes = DriverExtensions.GetElements(webElement, HeadNotesLocator).Select(webElement2 => webElement2.GetAttribute("id")).ToList();

            return headNotes.Select(id => DriverExtensions.WaitForElement(By.XPath($"//li[@id='{id}']//a"))).ToList();
        }

        /// <summary>
        /// Verify that the checkbox in grid row is selected.
        /// </summary>
        /// <param name="gridRow"> Grid row to look at at. </param>
        /// <returns> True if checked, false otherwise </returns>
        private bool IsGridRowCheckBoxChecked(IWebElement gridRow)
        {
            bool isSelected = false;
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.GetColumnIndex(ReferenceGridColumns.SelectAll)));
            if (this.GridRowHasCheckbox(gridRow))
            {
                isSelected = DriverExtensions.GetElement(gridRow, checkboxLocator).Selected;
            }

            return isSelected;
        }

        /// <summary>
        /// Determine if the given grid row has a checkbox
        /// </summary>
        /// <param name="gridRow">Grid row to look at at.</param>
        /// <returns>True if found.</returns>
        private bool GridRowHasCheckbox(IWebElement gridRow)
        {
            IWebElement checkbox;
            By checkboxLocator = By.XPath(string.Format(CheckboxLctMask, this.GetColumnIndex(ReferenceGridColumns.SelectAll)));
            DriverExtensions.TryGetElement(gridRow, checkboxLocator, out checkbox);

            return checkbox != null;
        }

        /// <summary>
        /// Return the element for the text that is highlighted in the title column
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="gridRow"> Grid row to use when looking for highlighted term </param>
        /// <returns> New instance of the page </returns>
        private T ClickHighLightedTermFromGridRow<T>(IWebElement gridRow) where T : ICreatablePageObject
        {
            By xPath = By.XPath(string.Format(HighLightedTermLinkLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            IWebElement highLightedTerm;
            DriverExtensions.TryGetElement(gridRow, xPath, out highLightedTerm);
            highLightedTerm?.Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Verify that the text is highlighted in the grid row.
        /// </summary>
        /// <param name="gridRow"> Grid row to use when looking for highlighted term </param>
        /// <param name="highLightedText"> Text to find </param>
        /// <returns> True if text is found and is highlighted, false otherwise </returns>
        private bool GridRowContainsHighlightedText(IWebElement gridRow, string highLightedText)
        {
            By xPath = By.XPath(string.Format(HighLightedTermLctMask, this.GetColumnIndex(ReferenceGridColumns.Title)));
            IWebElement webElement = DriverExtensions.GetElement(gridRow);
            List<IWebElement> elements = DriverExtensions.GetElements(webElement, xPath).ToList();

            return elements.Any(e => e.Text.Contains(highLightedText, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// Find the grow row data based on searching for text in the given column.
        /// </summary>
        /// <param name="gridColumn"> Column to look for the provided text. </param>
        /// <param name="columnText"> Search text .</param>
        /// <returns> Element with the row data </returns>
        private IWebElement GetResultsGridRow(ReferenceGridColumns gridColumn, string columnText)
        {
            By locator = By.XPath($"{this.gridTableXpath}/tbody//tr[./td[{this.GetColumnIndex(gridColumn)} and .//*[text()[contains(.,'{columnText}')]]]]");
            return DriverExtensions.WaitForElement(locator);
        }

        /// <summary>
        /// Get the rows from the results grid.
        /// </summary>
        /// <returns> Elements list from the grid </returns>
        private List<IWebElement> GetGridRows()
        {
            string xPath = $"{this.gridTableXpath}/tbody//tr";
            By itemLocator = By.XPath(xPath);
            DriverExtensions.IsDisplayed(itemLocator, 60000);
            return DriverExtensions.GetElements(itemLocator).ToList();
        }

        /// <summary>
        /// Find the column index of the given grid header element
        /// </summary>
        /// <param name="headerElement"> Element to find </param>
        /// <returns> Index of the element. -1 if not found. </returns>
        private int GetHeaderColumnIndex(By headerElement)
        {
            IList<IWebElement> gridTableColumns = DriverExtensions.GetElements(DriverExtensions.WaitForElement(By.XPath(this.gridTableXpath)), By.TagName("th"));
            IWebElement webElement = DriverExtensions.GetElement(headerElement);
            int elementIndex = -1;

            for (int index = 0; index < gridTableColumns.Count; index++)
            {
                if (gridTableColumns[index].GetAttribute("class").Equals(webElement.GetAttribute("class"))
                        && gridTableColumns[index].GetText().Equals(webElement.GetText()))
                {
                    elementIndex = index;
                }
            }

            return elementIndex;
        }
    }
}