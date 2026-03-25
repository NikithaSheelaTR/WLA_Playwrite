namespace Framework.Common.UI.Products.WestLawAnalytics.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analytics page
    /// </summary>
    public class AnalyticsPage : BasePage
    {
        private static readonly By GraphLocator = By.Id("wa_firmHealthRaphael");
        private static readonly By PaginationDropdownLocator = By.XPath("//select[@class='wa_paginationOptions']");

        /// <summary>
        /// TabPanel
        /// </summary>
        public AnalyticsPageTabPanel TabPanel { get; set; } = new AnalyticsPageTabPanel();

        /// <summary>
        /// Summarize By Tabs Component
        /// </summary>
        public FirmHealthComponent FirmHealth { get; } = new FirmHealthComponent();

        /// <summary>
        /// Pagination Dropdown
        /// </summary>
        public IDropdown<AnalyticsFooterPaginationOption> PaginationDropdown { get; } = new Dropdown<AnalyticsFooterPaginationOption>(PaginationDropdownLocator);

        /// <summary>
        /// This method waits for the page refresh to start
        /// </summary>
        /// <returns>true if Page Refresh Started, false otherwise</returns>
        public bool IsPageStartedToRefresh()
        {
            DriverExtensions.WaitForElementNotDisplayed(GraphLocator);
            return !DriverExtensions.IsDisplayed(GraphLocator);
        }
    }
}
