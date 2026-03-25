namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Items.ManageCapsResultItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Manage Caps Tab Component
    /// </summary>
    public class ManageCapsTabComponent : BaseAnalyticsTabComponent
    {
        private const string RowElementsLctMask = "//tbody[@id='wa_manageCapsTableBody']/tr[descendant::td][{0}]/td[{1}]";

        private const string CheckboxItemLctMask = "//tbody[@id='wa_manageCapsTableBody']/tr[descendant::td][{0}]/td[@class='wa_manageCapsTable_checkbox']/input";

        private const string EditLinkLctMask = "//tr[descendant::td][{0}]//td[@class='wa_manageCapsTable_edit']";

        private static readonly By CreateNewMonthlyCapButtonLocator = By.Id("wa_manageCapsCreateNewMonthlyCapButton");

        private static readonly By CreateNewSessionCapButtonLocator = By.Id("wa_manageCapsCreateNewSessionCapButton");

        private static readonly By DeleteButtonLocator = By.Id("wa_manageCapsDeleteCapsButton");

        private static readonly By ManageCapsTableHeaderLocator = By.XPath("//table[@class='wa_manageCapsTable']//th");

        private static readonly By EditButtonLocator = By.Id("wa_editBtn");

        private static readonly By SelectedColumnLocator = By.XPath("//*[contains(@class,'wa_ascending') or contains(@class,'wa_descending')]");

        private static readonly By ManageCapsItemLocator = By.XPath("//tbody[@id='wa_manageCapsTableBody']/tr/td[1]");

        private static readonly By InlineDeleteMessageLocator = By.XPath("//div[@id='wa_manageCapsMessageAreaPlaceholder']//div[@class='co_infoBox_message']");

        private static readonly By TabNameTextLocator = By.XPath("//div[@id='wa_manageCaps']/h2");

        private static readonly By InformationTextLocator = By.ClassName("co_formInline");

        private static readonly By CloseInlineMessageButtonLocator = By.ClassName("co_infoBox_closeButton");

        private static readonly By ContainerLocator = By.Id("wa_manageCaps");

        /// <summary>
        /// TabName
        /// </summary>
        protected override string TabName => "Manage Caps";

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

        #region Clicks

        /// <summary>
        /// Clicks close inline message button
        /// </summary>
        public void ClickCloseInlineMessageButton() => DriverExtensions.WaitForElement(CloseInlineMessageButtonLocator).Click();

        /// <summary>
        /// Clicks create new monthly cap button.
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickCreateNewMonthlyCapButton()
        {
            DriverExtensions.Click(CreateNewMonthlyCapButtonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Clicks create new session cap button.
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickCreateNewSessionCapButton()
        {
            DriverExtensions.Click(CreateNewSessionCapButtonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Clicks delete cap button.
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickDeleteButton()
        {
            DriverExtensions.Click(DeleteButtonLocator);
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Clicks edit link.
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickEditLink()
        {
            DriverExtensions.Click(EditButtonLocator);
            return new CostRecoveryCapsPage();
        }

        #endregion

        #region Displaying

        /// <summary>
        /// Verifies that the cap is displayed.
        /// </summary>
        /// <param name="capName">The caps name</param>
        /// <returns></returns>
        public bool IsCapDisplayed(string capName) => this.GetManageCapItems().Exists(cap => cap.CapName.Equals(capName));

        /// <summary>
        /// Verifies that the create new monthly cap button is displayed.
        /// </summary>
        /// <returns> True if the create new monthly cap button is displayed </returns>
        public bool IsCreateNewMonthlyCapButtonDisplayed() => DriverExtensions.IsDisplayed(CreateNewMonthlyCapButtonLocator);

        /// <summary>
        /// Verifies that the create new session cap button is displayed.
        /// </summary>
        /// <returns> True if the create new session cap button is displayed </returns>
        public bool IsCreateNewSessionCapButtonDisplayed() => DriverExtensions.IsDisplayed(CreateNewSessionCapButtonLocator);

        /// <summary>
        /// Verifies that the delete button is displayed.
        /// </summary>
        /// <returns> True if the delete button is displayed </returns>
        public bool IsDeleteButtonDisplayed() => DriverExtensions.IsDisplayed(DeleteButtonLocator);

        /// <summary>
        /// Verifies that the edit button is displayed.
        /// </summary>
        /// <returns> True if the delete button is displayed </returns>
        public bool IsEditButtonDisplayed() => DriverExtensions.IsDisplayed(EditButtonLocator);

        /// <summary>
        /// Verifies that Delete Inline Message is Displayed
        /// </summary>
        /// <returns> True If Delete Inline Message is Displayed </returns>
        public bool IsDeleteInlineMessageDisplayed() => DriverExtensions.IsDisplayed(InlineDeleteMessageLocator);

        #endregion

        #region Getters

        /// <summary>
        /// Gets Name of the column that is sorted by default
        /// </summary>
        /// <returns> Name of the column that is sorted by default </returns>
        public string GetColumnSortedByDefault() => DriverExtensions.GetText(SelectedColumnLocator);

        /// <summary>
        /// Gets Information text
        /// </summary>
        /// <returns> Information text </returns>
        public string GetInformationText() => DriverExtensions.GetText(InformationTextLocator);

        /// <summary>
        /// Gets the inline delete message
        /// </summary>
        /// <returns> Inline delete message </returns>
        public string GetInlineDeleteMessage() => DriverExtensions.GetText(InlineDeleteMessageLocator);

        /// <summary>
        /// Gets Manage Caps Table Column Titles.
        /// </summary>
        /// <returns> Titles of table columns </returns>
        public List<string> GetManageCapsTableColumnTitles()
            => DriverExtensions.GetElements(ManageCapsTableHeaderLocator).Select(elem => elem.Text).ToList();

        /// <summary>
        /// Gets Tab Name text
        /// </summary>
        /// <returns> Tab Name text </returns>
        public string GetTabNameText() => DriverExtensions.GetText(TabNameTextLocator);

        /// <summary>
        /// Get added Billing Group
        /// </summary>
        /// <param name="capName"></param>
        /// <returns>string</returns>
        public string GetAddedBillingGroupByName(string capName) =>
            this.GetManageCapItems().First(el => el.CapName.Equals(capName)).BillingGroups;

        #endregion

        /// <summary>
        /// Deletes a cap by Cap Name
        /// </summary>
        /// <param name="capName"> Cap Name </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage DeleteCapByName(string capName)
        {
            var createdCapsList = this.GetManageCapItems().Where(item => item.CapName.Equals(capName)).ToList();

            createdCapsList.ForEach(item => item.Checkbox.SetCheckbox(true));

            return this.ClickDeleteButton();
        }

        /// <summary>
        /// Edits a cap by Cap Name
        /// </summary>
        /// <param name="capName"> Cap Name </param>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage EditCapByName(string capName)
        {
            this.GetManageCapItems().FirstOrDefault(item => item.CapName.Equals(capName))?.EditLink.Click();
            return new CostRecoveryCapsPage();
        }

        /// <summary>
        /// Sort item by column
        /// </summary>
        /// <param name="column"> Column to sort by </param>
        /// <param name="byDescending"> If true - sort by descending, otherwise - sort by ascending </param>
        /// <returns> The <see cref="ManageCapsTableItem"/>. </returns>
        public List<ManageCapsTableItem> SortItemsByColumn(ManageCapsColumns column, bool byDescending)
        {
            DriverExtensions.WaitForElement(By.XPath(this.ManageCapsColumnsMap[column].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();

            string attribute = DriverExtensions.WaitForElement(SelectedColumnLocator).GetAttribute("class");
            if ((attribute.Contains("wa_descending") && !byDescending)
                || (attribute.Contains("wa_ascending") && byDescending))
            {
                DriverExtensions.WaitForElement(By.XPath(this.ManageCapsColumnsMap[column].LocatorString)).Click();
                DriverExtensions.WaitForJavaScript();
            }

            return this.GetManageCapItems();
        }

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
                    EndDate = DriverExtensions.WaitForElement(item, By.XPath(string.Format(RowElementsLctMask, numberOfRow, ++numberOfFirstColumn))).Text,
                    EditLink = DriverExtensions.GetElement(By.XPath(string.Format(EditLinkLctMask, numberOfRow)))
                };
                numberOfRow++;
                resultList.Add(model);

            }

            return resultList;
        }
    }
}
