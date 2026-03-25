namespace Framework.Common.UI.Products.Shared.Components.FolderHistory
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums.Foldering;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.WestLawNext.Pages.CompanyInvestigator;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Grid Component (Foldering and History Pages)
    /// </summary>
    public abstract class BaseGridComponent : BaseModuleRegressionComponent
    {
        private const string ColumnHeaderLctMask = ".//th[contains(@class,'{0}')]";        

        private const string GridRowLctMask = "//div[@id='co_researchOrg_detailsTable']//tr[.//td[@class='{0}']]";

        private const string ItemLinkLctMask = "//tbody//tr/td[@class='co_detailsTable_content' or 'TableCell--Description']//a[contains(.,{0})]";

        private const string ItemLctMask = "//tbody//tr/td[@class='co_detailsTable_content'or 'TableCell--Description' and .//a[contains(.,{0})]]";

        private const string ItemByGuidContainerLctMask = "//div[@class='co_keyCite_treatment' and ./a[contains(@href, '{0}')]]";

        private const string ItemCheckboxLctMask =
            ".//input[contains(@id,'cobalt_foldering_ro_select_checkbox_') and (@title=\"{0}\")] | .//input[@type='checkbox'][following-sibling::label[contains(text(), '{0}')]]";

        private const string SubHeadingLctMask =
            "//tbody//tr[@class='cobalt_ro_documentRow' and .//a/following-sibling::div[contains(.,{0})]]";

        private static readonly By CheckboxLocator = By.XPath(".//td[contains(@class,'co_detailsTable_select') or contains(@class,'TableCell--Select')]/input");

        private static readonly By CitationsLocator = By.XPath("./div[@class = 'cobalt_ro_documentDescription']/span");

        private static readonly By EmptyGridLocator = By.XPath("//tr[@class='empty']/td");

        private static readonly By ItemsLocator = By.XPath("//tbody//tr[contains(@class, 'cobalt_ro') or contains(@id, 'datatable-row')]");

        private static readonly By SelectAllCheckboxLocator = By.XPath("//*[not(contains(@class,'co_hideState'))]/input[@id='cobalt_foldering_select_all_checkbox'] | //input[@id='cobalt_foldering_ro_details_select_all']");

        private static readonly By TableContainerByLocator = By.Id("co_researchOrg_detailsTable");

        private static readonly By TableGridCheckboxesLocator =
            By.CssSelector("input[id*='cobalt_foldering_ro_select_checkbox_']");

        private static readonly By TableGridTitleLinksLocator = By.XPath("//tbody//tr/td[contains(@class,'co_detailsTable_content') or contains(@class,'TableCell--Description')]//a[not(contains(@class, 'Flag'))]");

        private static readonly By TableLocator = By.XPath("//table[contains(@class,'co_detailsTable') or contains(@class,'Table a11yTableSortable')]");

        private static readonly By EventLocator = By.XPath("//td[@class = 'co_detailsTable_event']/span");

        private EnumPropertyMapper<DocumentIcon, WebElementInfo> documentsIconMap;

        private EnumPropertyMapper<FolderPageGridColumns, WebElementInfo> folderGridColumnsMap;
        
        /// <summary>
        /// Footer Toolbar
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();

        /// <summary>
        /// Gets the FolderPageGridColumns enumeration to WebElementInfo map.
        /// </summary>
        protected virtual EnumPropertyMapper<FolderPageGridColumns, WebElementInfo> FolderGridColumnsMap
            =>
                this.folderGridColumnsMap =
                    this.folderGridColumnsMap ?? EnumPropertyModelCache.GetMap<FolderPageGridColumns, WebElementInfo>();

        /// <summary>
        /// Gets the Documents Icon Map
        /// </summary>
        private EnumPropertyMapper<DocumentIcon, WebElementInfo> DocumentsIconMap
            =>
                this.documentsIconMap =
                    this.documentsIconMap ?? EnumPropertyModelCache.GetMap<DocumentIcon, WebElementInfo>();

        /// <summary>
        /// Clicks on specific column header
        /// </summary>
        /// <param name="gridColumn">Table's column</param>
        public virtual void ClickGridColumnHeader(FolderPageGridColumns gridColumn)
        {
            DriverExtensions.WaitForElement(
                                DriverExtensions.WaitForElement(TableLocator), By.XPath(string.Format(ColumnHeaderLctMask, this.FolderGridColumnsMap[gridColumn].LocatorString)))
                            .Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on the first item on the FolderGrid
        /// </summary>
        /// <param name="itemNumberToClick">
        /// specific grid item to open. Default is first item, index of the first item in zero
        /// </param>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// Page Instance
        /// </returns>
        public T ClickGridItemByIndex<T>(int itemNumberToClick = 0) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(TableContainerByLocator, TableGridTitleLinksLocator).ElementAt(itemNumberToClick).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on the first item on the FolderGrid
        /// </summary>
        /// <param name="itemNumberToClick">
        /// specific grid item to open. Default is first item, index of the first item in zero
        /// </param>
        /// <param name="documentType">
        /// The document Type.
        /// </param>
        /// <returns>
        /// The page instance
        /// </returns>
        public CommonReportDocumentPage ClickGridItemByIndex(int itemNumberToClick, Type documentType)
        {
            DriverExtensions.GetElements(TableContainerByLocator, TableGridTitleLinksLocator).ElementAt(itemNumberToClick).Click();
            return (CommonReportDocumentPage)Activator.CreateInstance(documentType);
        }

        /// <summary>
        /// Click Grid Item by Name
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <returns>
        /// Page Instance
        /// </returns>
        public virtual T ClickGridItemByName<T>(string itemName) where T : ICreatablePageObject
        {
            IWebElement tableElement = DriverExtensions.WaitForElement(TableContainerByLocator);
            By itemLinkLocator = SafeXpath.BySafeXpath(ItemLinkLctMask, itemName);

            DriverExtensions.WaitForElement(tableElement, itemLinkLocator);
            DriverExtensions.GetElements(tableElement, itemLinkLocator).First(x => x.Text.Contains(itemName)).Click();

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click the "Select All" checkbox
        /// Is used by IE tests
        /// </summary>
        /// <param name="state">True by default</param>
        public void ClickSelectAllCheckBox(bool state = true)
        {
            if (!state && DriverExtensions.IsCheckboxSelected(SelectAllCheckboxLocator))
            {
                DriverExtensions.WaitForElement(SelectAllCheckboxLocator).Click();
            }

            if (state && !DriverExtensions.IsCheckboxSelected(SelectAllCheckboxLocator))
            {
                DriverExtensions.WaitForElement(SelectAllCheckboxLocator).Click();
            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary> Clicks Icon of the grid </summary>
        /// <param name="itemName"> The Item name </param>
        /// <param name="icon"> The type of the icon </param>
        /// <typeparam name="T"> BaseModuleRegressionDialog </typeparam>
        /// <returns> Icon dialog </returns>
        public T ClickGridIcon<T>(string itemName, DocumentIcon icon) where T : ICreatablePageObject
        {
            IWebElement container = DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ItemLctMask, itemName));
            DriverExtensions.Click(DriverExtensions.GetElement(container, By.XPath(this.DocumentsIconMap[icon].LocatorString)));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        ///  ToDo This logic need for Indigo. In the future should be removed
        /// checks if a specific item, indicated by the it's name, has the keycite flags
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <param name="icon">
        /// The document icon
        /// </param>
        /// <returns>
        /// true if the grid has the icon
        /// </returns>
        public bool DoesGridItemHaveIcon(string itemName, DocumentIcon icon)
            =>
                DriverExtensions.IsElementPresent(
                    DriverExtensions.GetElement(
                        SafeXpath.BySafeXpath(ItemLctMask, itemName),
                        By.XPath(this.DocumentsIconMap[icon].LocatorString)))
                && DriverExtensions.WaitForElement(
                    DriverExtensions.GetElement(SafeXpath.BySafeXpath(ItemLctMask, itemName)),
                    By.XPath(
                        string.Format(
                            this.DocumentsIconMap[icon].LocatorMask,
                            this.DocumentsIconMap[icon].LocatorString))).IsDisplayed();

        /// <summary>
        /// Citations list
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The list of citations.
        /// </returns>
        public IList<string> GetItemCitationsByGuid(string guid) =>
            DriverExtensions.GetElements(this.ItemByGuidConteiner(guid), CitationsLocator).Select(c => c.Text).ToList();

        /// <summary>
        /// Gets Title Link
        /// </summary>
        /// <param name="guid">
        /// The guid.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetItemTitleByGuid(string guid) => DriverExtensions.GetElement(this.ItemByGuidConteiner(guid), By.XPath("./a")).Text;

        /// <summary>
        /// Checks to see if grid is empty
        /// </summary>
        /// <returns>returns true if grid is empty</returns>
        public bool IsGridEmpty() => DriverExtensions.IsDisplayed(EmptyGridLocator) && DriverExtensions.GetText(EmptyGridLocator).Contains("No records found");

        /// <summary>
        /// Verify that grid row is not null
        /// </summary>
        /// <param name="gridColumns"> Grid columns </param>
        /// <param name="columnText"> Column text </param>
        /// <returns> True if not null, false otherwise </returns>
        public bool IsGridRowNotNull(FolderPageGridColumns gridColumns, string columnText) => this.GetResultsGridRow(gridColumns, columnText) != null;

        /// <summary>
        /// Check if the title of a folder or folder item is in the Folder Grid 
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        /// <returns>
        /// true if the grid has the grid with given item name.
        /// </returns>
        public virtual bool IsItemDisplayed(string itemName)
            => DriverExtensions.GetElements(ItemsLocator).Select(item => new BaseGridItem(item).ToModel<GridObjectModel>().Title).AsEnumerable().Any(item => item.StartsWith(itemName));

        /// <summary>
        /// Selects the number of the items on the grid
        /// </summary>
        /// <param name="lastItemNumber">specific grid item to open.</param>
        /// <param name="firstItemNumber">
        /// first item to click. Zero by default
        /// </param>
        /// <param name="scrollToElement">
        /// Scroll to element
        /// </param>
        public void SelectGridItemsRange(int lastItemNumber, int firstItemNumber = 0, bool scrollToElement = false)
        {
            for (int i = firstItemNumber; i < lastItemNumber; i++)
            {
                this.SelectItemByIndex(i, scrollToElementBeforeSelecting: scrollToElement);
            }
        }

        /// <summary>
        /// Checks the grid item by index
        /// </summary>
        /// <param name="itemNumberToClick">
        /// The item Number To Click.
        /// </param>
        /// <param name="state">
        /// The state of checkbox
        /// </param>
        /// <param name="scrollToElementBeforeSelecting">
        /// Scroll to element before selecting
        /// </param>
        public void SelectItemByIndex(int itemNumberToClick = 0, bool state = true, bool scrollToElementBeforeSelecting = false)
        {
            DriverExtensions.WaitForElement(TableContainerByLocator);
            DriverExtensions.WaitForJavaScript();
            var checkBoxElement = DriverExtensions.GetElements(TableContainerByLocator, TableGridCheckboxesLocator).ElementAt(itemNumberToClick);
            if (scrollToElementBeforeSelecting)
            {
                checkBoxElement.ScrollToElementCenter();
            }

            checkBoxElement.SetCheckboxUsingClick(state);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// Selects the item by name
        /// </summary>
        /// <param name="itemName">
        /// The Item name
        /// </param>
        public void SelectItemByName(string itemName)
        {
            IWebElement tableElement = DriverExtensions.WaitForElementDisplayed(TableContainerByLocator);
            DriverExtensions.Hover(By.XPath(string.Format(ItemCheckboxLctMask, itemName)));
            DriverExtensions.WaitForElement(tableElement, By.XPath(string.Format(ItemCheckboxLctMask, itemName))).Click();
        }

        /// <summary>
        /// The select item.
        /// </summary>
        /// <param name="subHeading">
        /// The Subtitle.
        /// </param>
        public void SelectItemBySubHeading(string subHeading)
        {
            IWebElement item = DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SubHeadingLctMask, subHeading));
            item.ScrollToElementCenter();
            DriverExtensions.Click(DriverExtensions.WaitForElement(item, CheckboxLocator));           
        }

        /// <summary>
        /// Find the grow row data based on searching for text in the given column.
        /// </summary>
        /// <param name="gridColumn">Column to look for the provided text.</param>
        /// <param name="columnText">Search text.</param>
        /// <returns>Element with the row data</returns>
        protected virtual IWebElement GetResultsGridRow(FolderPageGridColumns gridColumn, string columnText)
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

        private IWebElement ItemByGuidConteiner(string guid) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(ItemByGuidContainerLctMask, guid)));

        /// <summary>
        /// Get the history event from the first row in the grid.
        /// </summary>
        /// <returns>First history event text</returns>
        public string GetFirstHistoryEvent()
        {
            IReadOnlyCollection<IWebElement> historyEvents = DriverExtensions.GetElements(EventLocator);
            return historyEvents.First().Text;
        }
    }
}