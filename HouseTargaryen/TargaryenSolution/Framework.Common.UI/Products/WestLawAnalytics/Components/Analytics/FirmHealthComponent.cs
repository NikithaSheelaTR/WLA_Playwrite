namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Items.FirmHealthItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Summarize By Tabs Component
    /// </summary>
    public class FirmHealthComponent : BaseModuleRegressionComponent
    {
        private static readonly By GridItemLocator = By.XPath("//div[@class='wa_gridRow']");

        private static readonly By ReturnToAllButtonLocator = By.XPath("//button[@class='wa_firmHealthReturnButton']");

        private static readonly By BillingDetailsButtonLocator = By.XPath("//li[@class='wa_firmHealthBillingDetails']/button");

        private static readonly By CompareButtonLocator = By.XPath("//li[@class='wa_firmHealthCompare']/button");

        private static readonly By SummarizeTitleLocator = By.XPath("//span[@class='wa_summarizeByFilterTitle']");

        private static readonly By GridViewLocator = By.XPath("//div[@class='wa_firmHealthGridMainContent']");

        private static readonly By DescriptionLocator = By.XPath("//div[contains(@class,'wa_gridColumnDescription')]/div");

        private static readonly By BarGraphElementLocator = By.XPath("//div[@class = 'wa_gridBar']");

        private static readonly By FindTextboxLocator = By.CssSelector("#wa_firmHealthSearchBox");

        private static readonly By FindValuesLocator = By.CssSelector("li.ui-menu-item a.ui-corner-all");

        private static readonly By CheckboxLocator = By.XPath("//input[@class = 'wa_firmHealthItemCheckBox']");

        private static readonly By SelectedColumnLocator = By.XPath("//div[contains(@class,'wa_ascending') or contains(@class,'wa_descending')]");

        private static readonly By ContainerLocator = By.Id("wa_firmHealthMainContent");

        private EnumPropertyMapper<AnalyticsSummarizeTabs, WebElementInfo> analyticsSummarizeTabsMap;

        private EnumPropertyMapper<AnalyticsGridColumns, WebElementInfo> columnsMap;

        /// <summary>
        /// Delivery dropdown
        /// </summary>
        public DeliveryDropdown DeliveryDropdown => new DeliveryDropdown();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Analytics Summarize Tabs Map
        /// </summary>
        private EnumPropertyMapper<AnalyticsSummarizeTabs, WebElementInfo> AnalyticsSummarizeTabsMap
            =>
                this.analyticsSummarizeTabsMap =
                    this.analyticsSummarizeTabsMap
                    ?? EnumPropertyModelCache.GetMap<AnalyticsSummarizeTabs, WebElementInfo>();

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        private EnumPropertyMapper<AnalyticsGridColumns, WebElementInfo> ColumnsMap
            => this.columnsMap = this.columnsMap ?? EnumPropertyModelCache.GetMap<AnalyticsGridColumns, WebElementInfo>();

        /// <summary>
        /// Verify that 'Return To All' button is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsReturnToAllButtonDisplayed() => DriverExtensions.IsDisplayed(ReturnToAllButtonLocator, 5);

        /// <summary>
        /// Click on the Billing Details button
        /// </summary>
        /// <returns> The <see cref="BillingInvestigationResultsPage"/>. </returns>
        public BillingInvestigationResultsPage ClickBillingDetailsButton()
        {
            DriverExtensions.WaitForElement(BillingDetailsButtonLocator).Click();
            return new BillingInvestigationResultsPage();
        }

        /// <summary>
        /// Clicks on the Compare button at the top of the grid
        /// </summary>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage ClickCompareButton()
        {
            DriverExtensions.WaitForElement(CompareButtonLocator).Click();
            return new AnalyticsPage();
        }

        /// <summary>
        /// This method checks if the billing details button is enabled
        /// </summary>
        /// <returns>True if the billing details button is enabled, false if disabled</returns>
        public bool IsBillingDetailsButtonEnabled() => DriverExtensions.IsEnabled(BillingDetailsButtonLocator);

        /// <summary>
        /// This method checks if the compare button is enabled
        /// </summary>
        /// <returns>True if the compare button is enabled false if it is disabled</returns>
        public bool IsCompareButtonEnabled() => DriverExtensions.IsEnabled(CompareButtonLocator);

        /// <summary>
        /// Click on the summarize tab
        /// </summary>
        /// <param name="tab"> Summarize tab </param>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage ClickSummarizeTab(AnalyticsSummarizeTabs tab)
        {
            DriverExtensions.WaitForElement(By.Id(this.AnalyticsSummarizeTabsMap[tab].Id)).Click();
            DriverExtensions.WaitForJavaScript();
            return new AnalyticsPage();
        }

        /// <summary>
        /// Get title
        /// </summary>
        /// <returns> Title </returns>
        public string GetSummarizeTitle() => DriverExtensions.WaitForElement(SummarizeTitleLocator).Text;

        /// <summary>
        /// Click on the link
        /// </summary>
        /// <param name="link"> Link name to click </param>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage ClickOnTheLink(string link)
        {
            By locationLinkLocator = By.LinkText(link);

            DriverExtensions.WaitForElement(locationLinkLocator).Click();
            return new AnalyticsPage();
        }

        /// <summary>
        /// Verify that grid with results is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsResultsGridDisplayed() => DriverExtensions.IsDisplayed(GridViewLocator, 5);

        /// <summary>
        /// Get links name from the grid
        /// </summary>
        /// <returns> list of names from the grid </returns>
        public List<string> GetGridItemsLinksList() => this.GetGridItemsList().Select(u => u.Link).ToList();

        /// <summary>
        /// Get description of the item from the grid
        /// </summary>
        /// <returns> Descriptions list </returns>
        public List<string> GetDescriptionList() => DriverExtensions.GetElements(DescriptionLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Get count of items in grid
        /// </summary>
        /// <returns> Count of items </returns>
        public int GetGridItemsCount() => DriverExtensions.GetElements(GridItemLocator).Count;

        /// <summary>
        /// Verify that all bar graphs are displayed and for all items in the grid
        /// </summary>
        /// <returns> True if displayed and count of bars are equal count of the items </returns>
        public bool AreBarGraphElementsDisplayed()
        {
            List<IWebElement> barGraphList = DriverExtensions.GetElements(BarGraphElementLocator).ToList();
            return barGraphList.TrueForAll(elem => elem.Displayed) && barGraphList.Count.Equals(this.GetGridItemsCount());
        }

        /// <summary>
        /// This method search for a user using the find box, then clicks user in the results list
        /// </summary>
        /// <param name="value"> The value we want entered into the find </param>
        /// <param name="indexToSelect"> The index To Select. </param>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage FindAndSelectUser(string value, int indexToSelect)
        {
            DriverExtensions.SetTextField(value, FindTextboxLocator);
            DriverExtensions.WaitForElementDisplayed(FindValuesLocator);
            DriverExtensions.GetElements(FindValuesLocator).ToList()[indexToSelect].Click();
            return new AnalyticsPage();
        }

        /// <summary>
        ///  Select / unselect checkboxes
        /// </summary>
        /// <param name="itemsCountToSelect"> count of checkboxes to select/unselect</param>
        /// <param name="isSelect"> Select checkboxes if true, unselect otherwise </param>
        public void SetGridCheckboxes(int itemsCountToSelect, bool isSelect)
        {
            List<IWebElement> elementsList =
                DriverExtensions.GetElements(CheckboxLocator).Take(itemsCountToSelect).ToList();
            elementsList.ForEach(elem => elem.SetCheckbox(isSelect));
        }

        /// <summary>
        /// Get count of the selected checkboxes
        /// </summary>
        /// <returns> Count of the selected checkboxes </returns>
        public int GetSelectedCheckboxesCount()
            => DriverExtensions.GetElements(CheckboxLocator).Where(elem => DriverExtensions.IsCheckboxSelected(elem)).ToList().Count;

        /// <summary>
        /// Sort item by column
        /// </summary>
        /// <param name="column"> Column to sort by </param>
        /// <param name="byDescending"> If true - sort by descending, otherwise - sort by ascending </param>
        /// <returns> The <see cref="FirmHealthModel"/>. </returns>
        public List<FirmHealthModel> SortItemsByColumn(AnalyticsGridColumns column, bool byDescending)
        {
            DriverExtensions.WaitForElement(By.XPath(this.ColumnsMap[column].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();

            string attribute = DriverExtensions.WaitForElement(SelectedColumnLocator).GetAttribute("class");
            if ((attribute.Contains("wa_descending") && !byDescending)
                || (attribute.Contains("wa_ascending") && byDescending))
            {
                DriverExtensions.WaitForElement(By.XPath(this.ColumnsMap[column].LocatorString)).Click();
                DriverExtensions.WaitForJavaScript();
            }

            return this.GetGridModelsList();
        }

        private List<FirmHealthModel> GetGridModelsList() =>
            this.GetGridItemsList().Select(item => item.ToModel<FirmHealthModel>()).ToList();

        private List<FirmHealthItem> GetGridItemsList() =>
            DriverExtensions.GetElements(GridItemLocator).Select(itemContainer => new FirmHealthItem(itemContainer)).ToList();
    }
}