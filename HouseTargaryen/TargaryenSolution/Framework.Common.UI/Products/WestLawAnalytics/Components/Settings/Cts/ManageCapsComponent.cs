namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Items.ManageCapsResultItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Manage Caps Component
    /// </summary>
    public class ManageCapsComponent : BaseModuleRegressionComponent
    {
        private const string RowElementsLctMask = "//tbody[@id='wa_manageCapsTableBody']/tr[{0}]/td[{1}]";

        private const string CheckboxItemLctMask = "//tbody[@id='wa_manageCapsTableBody']/tr[{0}]/td[@class='wa_manageCapsTable_checkbox']/input";

        private static readonly By DeleteButtonLocator = By.Id("wa_manageCapsDeleteCapsButton");

        private static readonly By ManageCapsTableHeaderLocator = By.XPath("//table[@class='wa_manageCapsTable']//th");

        private static readonly By EditLinkLocator = By.Id("wa_editBtn");

        private static readonly By SelectedColumnLocator = By.XPath("//*[contains(@class,'wa_ascending') or contains(@class,'wa_descending')]");

        private static readonly By ManageCapsItemLocator = By.XPath("//tbody[@id='wa_manageCapsTableBody']/tr/td[1]");

        private static readonly By InlineDeleteMessageLocator = By.XPath("//div[@id='wa_manageCapsMessageAreaPlaceholder']//div[@class='co_infoBox_message']");

        private static readonly By TabNameTextLocator = By.XPath("//div[@id='wa_manageCaps']/h2");

        private static readonly By ReturnToCtsSearchResultLinkLocator = By.Id("wa_return");

        private static readonly By ContainerLocator = By.ClassName("accountSetup_SubAccountList");

        /// <summary>
        /// Manage Caps columns Map
        /// </summary>
        private EnumPropertyMapper<ManageCapsColumns, WebElementInfo> manageCapsColumnsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the Manage Caps columns enumeration
        /// </summary>
        private EnumPropertyMapper<ManageCapsColumns, WebElementInfo> ManageCapsColumnsMap
            =>
                this.manageCapsColumnsMap =
                    this.manageCapsColumnsMap
                    ?? EnumPropertyModelCache.GetMap<ManageCapsColumns, WebElementInfo>();

        /// <summary>
        /// Gets Manage Caps Table Column Titles.
        /// </summary>
        /// <returns> Titles of table columns </returns>
        public List<string> GetManageCapsTableColumnTitles()
            => DriverExtensions.GetElements(ManageCapsTableHeaderLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Verifies that the delete button is displayed.
        /// </summary>
        /// <returns> True if the delete button is displayed </returns>
        public bool IsDeleteButtonDisplayed() => DriverExtensions.IsDisplayed(DeleteButtonLocator);

        /// <summary>
        /// Clicks create new session cap button.
        /// </summary>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage ClickDeleteButton()
        {
            DriverExtensions.Click(DeleteButtonLocator);
            return new CtsPage();
        }

        /// <summary>
        /// Gets Tab Name text
        /// </summary>
        /// <returns> Tab Name text </returns>
        public string GetTabNameText() => DriverExtensions.GetText(TabNameTextLocator);

        /// <summary>
        /// Gets Name of the column that is sorted by default
        /// </summary>
        /// <returns> Name of the column that is sorted by default </returns>
        public string GetColumnSortedByDefault() => DriverExtensions.GetText(SelectedColumnLocator);

        /// <summary>
        /// Gets the inline delete message
        /// </summary>
        /// <returns> Inline delete message </returns>
        public string GetInlineDeleteMessage()
        {
            DriverExtensions.WaitForElementDisplayed(InlineDeleteMessageLocator);
            return DriverExtensions.GetText(InlineDeleteMessageLocator);
        }

        /// <summary>
        /// Verifies that the Return To Cts Search Result Link is displayed.
        /// </summary>
        /// <returns> True if the Return To Cts Search Result Link is displayed </returns>
        public bool IsReturnToCtsSearchResultLinkDisplayed() => DriverExtensions.IsDisplayed(ReturnToCtsSearchResultLinkLocator);

        /// <summary>
        /// Clicks the Return To Cts Search Result Link.
        /// </summary>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage ClickReturnToCtsSearchResultLin()
        {
            DriverExtensions.Click(ReturnToCtsSearchResultLinkLocator);
            return new CtsPage();
        }

        /// <summary>
        /// Verifies that the Edit link is displayed
        /// </summary>
        /// <returns> True if the Edit link is displayed </returns>
        public bool IsEditLinkDisplayed() => DriverExtensions.IsDisplayed(EditLinkLocator);

        /// <summary>
        /// Sort item by column
        /// </summary>
        /// <param name="column"> Column to sort by </param>
        /// <param name="defaultSorting"> If true - sort by descending, otherwise - sort by ascending </param>
        public void SortItemsByColumn(ManageCapsColumns column, bool defaultSorting = false)
        {
            DriverExtensions.WaitForElement(By.XPath(this.ManageCapsColumnsMap[column].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();

            string attribute = DriverExtensions.WaitForElement(SelectedColumnLocator).GetAttribute("class");
            if ((attribute.Contains("wa_descending") && !defaultSorting)
                || (attribute.Contains("wa_ascending") && defaultSorting))
            {
                DriverExtensions.WaitForElement(By.XPath(this.ManageCapsColumnsMap[column].LocatorString)).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Deletes a cap by Cap Name
        /// </summary>
        /// <param name="capName"> Cap Name </param>
        /// <returns> The <see cref="CtsPage"/>. </returns>
        public CtsPage DeleteCapByName(string capName)
        {
            var createdCapsList = this.GetManageCapItems().Where(item => item.CapName.Equals(capName)).ToList();
            createdCapsList.ForEach(item => item.Checkbox.SetCheckbox(true));
            return this.ClickDeleteButton();
        }

        /// <summary>
        /// Verifies that Only Selected Billing Group Displayed
        /// </summary>
        /// <param name="selectedBillingGroup"> The billing group </param>
        /// <returns> True if Only Selected Billing Group Displayed </returns>
        public bool IsOnlySelectedBillingGroupDisplayed(string selectedBillingGroup)
            => this.GetManageCapItems().TrueForAll(item => item.BillingGroups.Contains(selectedBillingGroup));

        /// <summary>
        /// Verifies that the deleted cap isn't present.
        /// </summary>
        /// <param name="capName"> The cap name. </param>
        /// <returns> The <see cref="bool"/>. True if the deleted cap is present.</returns>
        public bool IsDeletedCapPresent(string capName)
            => !this.GetManageCapItems().TrueForAll(item => !item.CapName.Equals(capName));

        /// <summary>
        /// Gets list of ManageCapsModal
        /// </summary>
        /// <returns> List of ManageCapsModal </returns>
        private List<ManageCapsTableItem> GetManageCapItems()
        {
            var resultList = new List<ManageCapsTableItem>();
            int numberOfRow = 1;

            foreach (IWebElement item in DriverExtensions.GetElements(ManageCapsItemLocator))
            {
                int numberOfFirstColumn = 2;

                var model = new ManageCapsTableItem
                {
                    Checkbox = DriverExtensions.GetElement(By.XPath(string.Format(CheckboxItemLctMask, numberOfRow))),
                    CapName = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, numberOfFirstColumn))).Text,
                    ClientId = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    BillingGroups = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    CapAmount = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    CapType = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    BeginDate = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    EndDate = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text
                };
                numberOfRow++;
                resultList.Add(model);
            }

            return resultList;
        }
    }
}
