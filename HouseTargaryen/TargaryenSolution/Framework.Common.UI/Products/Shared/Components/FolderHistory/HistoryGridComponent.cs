namespace Framework.Common.UI.Products.Shared.Components.FolderHistory
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.Shared.Items.FolderHistory;
    using Framework.Common.UI.Products.Shared.Models.GridModels;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// History Table Component
    /// History Page Component
    /// </summary>
    public class HistoryGridComponent : BaseGridComponent
    {
        private const string DocumentLinkLctMask = "//div[@class='co_keyCite_treatment']/a[contains(text(),{0})]";

        private static readonly By DateColumnHeaderLocator = By.CssSelector(".co_detailsTable>thead .co_detailsTable_date");

        private static readonly By HistoryTableItemLocator = By.XPath("//tbody//tr[contains(@id, 'datatable-row') and .//td[not(contains(@class, 'co_detailsTable_subHeader'))]]");

        private static readonly By ContainerLocator = By.Id("co_researchOrg_detailsContainer");

        private static readonly By InfoIconLocator = By.XPath("//*[contains(@class,'itemTooltip')]//span[contains(@class,'help')]");

        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Clicks a search history event by the search history event name.
        /// </summary>
        /// <typeparam name="T">
        /// Page instance
        /// </typeparam>
        /// <param name="eventName">
        /// Event name
        /// </param>
        /// <returns>
        /// Page instance.
        /// </returns>
        public override T ClickGridItemByName<T>(string eventName)
        {
            DriverExtensions.WaitForElement(By.PartialLinkText(eventName)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// Return true if the hover message is displayed
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(InfoIconLocator);
            return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");
        }

        /// <summary>
        /// Click link by description
        /// </summary>
        /// <typeparam name="T">page to return</typeparam>
        /// <param name="description">description</param>
        /// <returns>new page</returns>
        public T ClickLinkByDescription<T>(string description)
            where T : ICreatablePageObject
        {
            DriverExtensions.Click(SafeXpath.BySafeXpath(DocumentLinkLctMask, description));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public List<AllHistoryGridModel> GetAllHistoryGridModels() =>
            DriverExtensions.GetElements(HistoryTableItemLocator).Select(item => new AllHistoryTableItem(item).ToModel<AllHistoryGridModel>()).ToList();

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public List<AllHistoryTableItem> GetGridItems() =>
            DriverExtensions.GetElements(HistoryTableItemLocator).Select(item => new AllHistoryTableItem(item)).ToList();

        /// <summary>
        /// Get object from the grid containing title and details
        /// </summary>
        /// <param name="index"> Raw index </param>
        /// <returns> The <see cref="AllHistoryGridModel"/>. </returns>
        public AllHistoryGridModel GetHistoryGridModelByIndex(int index)
            => new AllHistoryTableItem(DriverExtensions.GetElements(HistoryTableItemLocator).ElementAt(index)).ToModel<AllHistoryGridModel>();

        /// <summary>
        /// get a detailed list of the objects in the grid containing title and details
        /// </summary>
        /// <returns>list of grid objects</returns>
        public List<SearchesHistoryGridModel> GetSearchesGridModels() =>
            DriverExtensions.GetElements(HistoryTableItemLocator).Select(item => new SearchesHistoryTableItem(item).ToModel<SearchesHistoryGridModel>()).ToList();

        /// <summary>
        /// Checks if a history event exists
        /// </summary>
        /// <param name="row">Row number</param>
        /// <param name="model">All History Item model</param>
        /// <param name="timeDifference">Date time range which satisfies event time
        /// timeDifference value is measured in seconds</param>
        /// <returns>True if exists</returns>
        public bool IsHistoryEventPresent(int row, AllHistoryGridModel model, int timeDifference = 121)
        {
            AllHistoryGridModel modelToCheck = this.GetHistoryGridModelByIndex(row);
            return this.IsItemDisplayed(model.Title)
                && modelToCheck.Title == model.Title
                && modelToCheck.Date.IsInRange(model.Date, timeDifference)
                && modelToCheck.ClientId == model.ClientId
                && modelToCheck.Event == model.Event;
        }

        /// <summary>
        /// Sort History table by Date
        /// Table sorted by date as descending by default
        /// </summary>
        /// <param name="order">true (descending) or false (ascending)</param>
        /// <returns>new History Page</returns>
        public CommonHistoryPage SortByDate(SortOrder order)
        {
            string sortAttributeValue = DriverExtensions.GetElement(DateColumnHeaderLocator).GetAttribute("class");

            if (order == SortOrder.Descending && !sortAttributeValue.Contains("descending")
                || order == SortOrder.Ascending && !sortAttributeValue.Contains("ascending"))
            {
                DriverExtensions.GetElement(DateColumnHeaderLocator).Click();
            }

            return new CommonHistoryPage();
        }
    }
}