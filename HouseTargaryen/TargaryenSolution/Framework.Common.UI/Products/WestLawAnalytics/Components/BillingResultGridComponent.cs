namespace Framework.Common.UI.Products.WestLawAnalytics.Components
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawAnalytics.Dialogs;
    using Framework.Common.UI.Products.WestLawAnalytics.Items.BillingResultGridItems;
    using Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.BillingInvestigation;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents central table on BillingResultPage
    /// </summary>
    public class BillingResultGridComponent : BaseModuleRegressionComponent
    {
        private static readonly By RowLocator = By.ClassName("wa_gridRow");

        private static readonly By TransactionGridLocator = By.CssSelector("div.co_billingInvestigationTransactionGrid");

        private static readonly By ChargeableSortingLocator = By.XPath("//div[@data-sort-column='Chargeable']/a");

        private static readonly By HourlyTransactionalLabelLocator = By.CssSelector("div.co_transactionColumnDescription div");

        private static readonly By ContainerLocator = By.Id("co_billingInvestigationSessionGrid");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Get list of Client IDs
        /// </summary>
        /// <returns>List of client IDs</returns>
        public List<string> GetListOfClientIds() => DriverExtensions
            .GetElements(RowLocator).Select(item => new GridRowItem(item.GetAttribute("Id")).ClientId).ToList();

        /// <summary>
        /// Get User Name of GridRowItem by index
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>User name</returns>
        public string GetUserNameOfGridRowItemByIndex(int itemIndex = 0) => this.GetGridRowItemByIndex(itemIndex).User;

        /// <summary>
        /// This method checks to see if the first transaction info grid is displayed.
        /// </summary>
        /// <returns>Returns true if the transaction grid is displayed.</returns>
        public bool IsTransactionGridDisplayed() => DriverExtensions.IsDisplayed(TransactionGridLocator, 5);

        /// <summary>
        /// Is GridRowItem chargeable
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>true - if it is chargeable, false - otherwise</returns>
        public bool IsGridRowItemChargeable(int itemIndex = 0) => this.GetGridRowItemByIndex(itemIndex).IsChargeable;

        /// <summary>
        /// Performs sorting billing results by charge
        /// </summary>
        /// <param name="chargeable">Sorting by chargeable if true</param>
        /// <returns><see cref="BillingInvestigationResultsPage"/></returns>
        public BillingInvestigationResultsPage SortByCharge(bool chargeable = true)
        {
            DriverExtensions.WaitForElement(ChargeableSortingLocator).Click();
            DriverExtensions.WaitForJavaScript();
            if (chargeable)
            {
                DriverExtensions.WaitForElement(ChargeableSortingLocator).Click();
            }

            return new BillingInvestigationResultsPage();
        }

        /// <summary>
        /// Expand GridRowItem by index
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>The GridRowDetailItem </returns>
        public GridRowDetailItem ExpandGridRowItemByIndex(int itemIndex = 0) => this.GetGridRowItemByIndex(itemIndex).ClickExpandButton();

        /// <summary>
        /// Expand GridRowItem by index and get DetailGridRowModel
        /// </summary>
        /// <param name="itemIndex"> Index of the item to expand </param>
        /// <returns> The <see cref="GridRowDetailItem"/> </returns>
        public GridRowDetailItemModel ExpandGridRowItemByIndexAndGetDetailGridRowModel(int itemIndex = 0) => this.ExpandGridRowItemByIndex(itemIndex).ToModel<GridRowDetailItemModel>();

        /// <summary>
        /// Collapse GridRowItem By Index
        /// </summary>
        /// <param name="itemIndex"> Item to collapse </param>
        public void CollapseGridRowItemByIndex(int itemIndex = 0) => this.GetGridRowItemByIndex(itemIndex).ClickCollapseButton();

        /// <summary>
        /// Get Detail Model
        /// </summary>
        /// <param name="index">Index of item</param>
        /// <returns> The <see cref="GridRowDetailItemModel"/>.</returns>
        public GridRowDetailItemModel GetDetailModelByIndex(int index = 0) =>
            this.GetGridRowItemByIndex(index).DetailItem.ToModel<GridRowDetailItemModel>();

        /// <summary>
        ///     Returns true if the description column within an expanded session does not contain the labels Hourly or
        ///     transactional
        /// </summary>
        /// <returns>True if the Hourly or Transactional labels do not appear on expanded sessions</returns>
        public bool VerifyHourlyTransactionLabelNotDisplayed()
            =>
                !DriverExtensions.GetText(HourlyTransactionalLabelLocator).Contains("Hourly")
                || !DriverExtensions.GetText(HourlyTransactionalLabelLocator).Contains("transactionional");

        /// <summary>
        /// Clicks on 'Edit' button
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>The <see cref="EditBillingInformationDialog"/>.</returns>
        public EditBillingInformationDialog ClickEditButtonOnGridRowItem(int itemIndex = 0) =>
            this.GetGridRowItemByIndex(itemIndex).DetailItem.ClickEditButton();

        /// <summary>
        /// Verify that edit button is displayed
        /// </summary>
        /// <param name="itemIndex">Item of index</param>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public bool IsEditButtonOnGridRowItemDisplayed(int itemIndex = 0) =>
            this.GetGridRowItemByIndex(itemIndex).DetailItem.IsEditButtonDisplayed();

        /// <summary>
        /// Get GridRowModel by index
        /// </summary>
        /// <param name="itemIndex">Index of item</param>
        /// <returns>The <see cref="GridRowModel"/>.</returns>
        public GridRowModel GetGridRowModelByIndex(int itemIndex = 0) => this.GetGridRowItemByIndex(itemIndex).ToModel<GridRowModel>();

        private GridRowItem GetGridRowItemByIndex(int itemIndex = 0) => new GridRowItem(
            DriverExtensions.GetElements(RowLocator).ElementAt(itemIndex).GetAttribute("Id"));
    }
}