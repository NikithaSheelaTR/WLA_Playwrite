namespace Framework.Common.UI.Products.WestLawNext.Pages.Dockets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.CategoryPage;
    using Framework.Common.UI.Products.Shared.Components.Docket;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Docket;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// DocketsRequestsPage
    /// </summary>
    public class DocketsRequestsPage : CommonDocketsPage
    {
        private static readonly By ClearSelectedItemsLinkLocator = By.XPath(".//*[text()='clear selected'] | .//*[@class='js-clear-selected-items']");

        private static readonly By DeletePdfDownloadsButtonLocator = By.Id("coid_deleteRecordButton_PDF");

        private static readonly By DeleteUpdatesButtonLocator = By.Id("coid_deleteRecordButton_DKT");

        private static readonly By DocketTableBodyLocator = By.XPath(".//tbody");

        private static readonly By DocketTableTitleLocator = By.XPath(".//h3[@class = 'co_docketHeader']");

        private static readonly By PdfDeliveryButtonLocator = By.Id("deliveryWidget2");

        private static readonly By SelectAlltemsLinkLocator = By.XPath(".//*[text() = 'Select all items']");

        private static readonly By SelectAlltemsCheckboxLocator = By.XPath(".//li[@class='co_navSelectAll']/input");

        private static readonly By SelectedItemsLabelLocator = By.XPath(".//li[@class='co_navItemsSelected']/span");

        private static readonly By RefreshStatusButtonLocator = By.XPath("//*[contains(text(), 'Refresh Status')]");

        private EnumPropertyMapper<DocketsRequestsTables, WebElementInfo> tablesMap;

        /// <summary>
        /// Gets the delivery dropdown.
        /// </summary>
        public DeliveryDropdown DeliveryUpdateDropdown => new DeliveryDropdown();

        /// <summary>
        /// Gets the delivery pdf dropdown.
        /// </summary>
        public DeliveryDropdown DeliveryPdfDropdown => new DeliveryDropdown(PdfDeliveryButtonLocator);

        /// <summary>
        /// The Dockets Requests Grid Component.
        /// </summary>
        public DocketsRequestsGridComponent DocketsRequestsGrid { get; } = new DocketsRequestsGridComponent();

        /// <summary>
        /// The Filter By Docket Number Dropdown.
        /// </summary>
        public FilterByDocketNumberDropdown FilterByDocketNumberDropdown { get; } = new FilterByDocketNumberDropdown();

        /// <summary>
        /// The Favorites Component.
        /// </summary>
        public FavoritesComponent Favorites { get; } = new FavoritesComponent();

        /// <summary>
        /// Docket Requests Tables mapper
        /// </summary>
        protected EnumPropertyMapper<DocketsRequestsTables, WebElementInfo> TablesMap
            =>
                this.tablesMap =
                    this.tablesMap ?? EnumPropertyModelCache.GetMap<DocketsRequestsTables, WebElementInfo>();

        /// <summary>
        /// Click Clear Selected Link
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> The <see cref="DocketsRequestsPage"/>. </returns>
        public DocketsRequestsPage ClickClearSelectedLink(DocketsRequestsTables docketTable)
        {
            DriverExtensions.GetElement(By.Id(this.TablesMap[docketTable].Id), ClearSelectedItemsLinkLocator).Click();
            return this;
        }

        /// <summary>
        /// Click Select All Items Link
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> Docket request page or Warning dialog if count more than 20 </returns>
        public T ClickSelectAllItemsLink<T>(DocketsRequestsTables docketTable) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(DriverExtensions.WaitForElement(By.Id(this.TablesMap[docketTable].Id)), SelectAlltemsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click Select All Items check-box
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <param name="setTo"> True to check, false otherwise </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> Docket request page or Warning dialog if count more than 20 </returns>
        public T SetSelectAllItemsCheckbox<T>(DocketsRequestsTables docketTable, bool setTo) where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(setTo, DriverExtensions.WaitForElement(By.Id(this.TablesMap[docketTable].Id)), SelectAlltemsCheckboxLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the Delete button for docket downloads
        /// </summary>
        /// <returns> The <see cref="DocketsRequestsPage"/>. </returns>
        public DocketsRequestsPage ClickDeletePdfDownloadsButton()
        {
            DriverExtensions.WaitForElement(DeletePdfDownloadsButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Clicks the Delete button for docket updates
        /// </summary>
        /// <returns>A new instance of this page</returns>
        public DocketsRequestsPage ClickDeleteUpdatesButton()
        {
            DriverExtensions.Click(DeleteUpdatesButtonLocator);
            DriverExtensions.WaitForJavaScript();
            return this;
        }

        /// <summary>
        /// Get docket table body Text
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns>The docket table body Text</returns>
        public string GetDocketTableBodyText(DocketsRequestsTables docketTable)
            => DriverExtensions.GetElement(By.Id(this.TablesMap[docketTable].Id), DocketTableBodyLocator).Text;

        /// <summary>
        /// Get docket table title Text
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns>The docket table title Text</returns>
        public string GetDocketTableTitleText(DocketsRequestsTables docketTable)
            => DriverExtensions.GetElement(By.Id(this.TablesMap[docketTable].Id), DocketTableTitleLocator).Text;

        /// <summary>
        /// Get PDF Download Selected Items Text
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns>Selected Items Label Text</returns>
        public string GetSelectedItemsLabelText(DocketsRequestsTables docketTable) 
            => DriverExtensions.GetElement(By.Id(this.TablesMap[docketTable].Id), SelectedItemsLabelLocator).Text;

        /// <summary>
        /// Determines if the Delete button for deleting docket downloads displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDeletePdfDownloadsButtonDisplayed() => DriverExtensions.IsDisplayed(DeletePdfDownloadsButtonLocator);

        /// <summary>
        /// Determines if the Delete button for deleting docket updates displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDeleteUpdatesButtonDisplayed() => DriverExtensions.IsDisplayed(DeleteUpdatesButtonLocator);

        /// <summary>
        /// Determines if the Delete button for deleting docket downloads enabled
        /// </summary>
        /// <returns> True if enabled, false otherwise </returns>
        public bool IsDeletePdfDownloadsButtonEnabled()
            => !DriverExtensions.GetAttribute("class", DeletePdfDownloadsButtonLocator).Contains("disabled");

        /// <summary>
        /// Determines if the Delete button for deleting docket updates enabled
        /// </summary>
        /// <returns> True if enabled, false otherwise </returns>
        public bool IsDeleteUpdatesButtonEnabled()
             => !DriverExtensions.GetAttribute("class", DeleteUpdatesButtonLocator).Contains("disabled");

        /// <summary>
        /// Determines if the docket Table displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise</returns>
        public bool IsDocketTableDisplayed(DocketsRequestsTables docketTable)
            => DriverExtensions.IsDisplayed(By.Id(this.TablesMap[docketTable].Id));

        /// <summary>
        /// Get checkbox status
        /// </summary>
        /// <param name="docketTable">The docket Table.</param>
        /// <returns>True if selected</returns>
        public bool IsSelectAllItemsCheckboxSelected(DocketsRequestsTables docketTable) =>
            DriverExtensions.IsCheckboxSelected(
                DriverExtensions.WaitForElement(By.Id(this.TablesMap[docketTable].Id)),
                SelectAlltemsCheckboxLocator);

        /// <summary>
        /// Verify that Clear Selection Link is displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsClearSelectedLinkDisplayed(DocketsRequestsTables docketTable)
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.TablesMap[docketTable].Id));
            return DriverExtensions.IsDisplayed(By.Id(this.TablesMap[docketTable].Id), ClearSelectedItemsLinkLocator);
        }

        /// <summary>
        /// Verify that 'Select all items' Link is displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSelectAllItemsLinkDisplayed(DocketsRequestsTables docketTable) => DriverExtensions.IsDisplayed(
            By.Id(this.TablesMap[docketTable].Id),
            SelectAlltemsLinkLocator);

        /// <summary>
        /// Verify that 'Select all items' checkbox is displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSelectAllItemsCheckboxDisplayed(DocketsRequestsTables docketTable) => DriverExtensions.IsDisplayed(
            By.Id(this.TablesMap[docketTable].Id),
            SelectAlltemsCheckboxLocator);

        /// <summary>
        /// Is refresh status button displayed
        /// </summary>
        /// <param name="docketTable"> The docket Table. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsRefreshStatusButtonDisplayed(DocketsRequestsTables docketTable) 
            => DriverExtensions.IsDisplayed(By.Id(this.TablesMap[docketTable].Id), RefreshStatusButtonLocator);
    }
}