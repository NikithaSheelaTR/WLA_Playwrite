namespace Framework.Common.UI.Products.WestLawAnalytics.Dropdowns.Analytics
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Data Range Dropdown on the Analytics page in Westlaw Analytics
    /// </summary>
    public class AnalyticsDateRangeDropdown : BaseModuleRegressionCustomDropdown<AnalyticsDateRangeOptions>
    {
        private static readonly By ExpandedDropdownLocator =
            By.XPath("//div[@class='wa_menu alignLeft' and @style='display: block;']");

        private static readonly By DropdownButtonLocator = By.XPath("//div[@class='wa_dropDown wa_timeFrameDropDown']/button");

        private static readonly By DropdownLocator = By.XPath("//div[@class='wa_dropDown wa_timeFrameDropDown']");

        private static readonly By DropdownOption = By.XPath("//ul[@class='wa_options']/li");

        private static readonly By SaveButtonLocator = By.XPath("//button[contains(@class,'wa_saveButton blue')]");

        private static readonly By FromDateInputLocator = By.Id("wa_fromDate");

        private static readonly By ToDateInputLocator = By.Id("wa_toDate");

        private static readonly By FromDateLabelLocator = By.XPath("//label[@for='wa_fromDate']");

        private static readonly By ToDateLabelLocator = By.XPath("//label[@for='wa_toDate']");

        private EnumPropertyMapper<AnalyticsDateRangeOptions, WebElementInfo> dateRangeMap;

        /// <summary>
        /// Get selected option
        /// </summary>
        public override AnalyticsDateRangeOptions SelectedOption
            => DriverExtensions.GetText(DropdownButtonLocator).GetEnumValueByText<AnalyticsDateRangeOptions>();

        /// <summary>
        /// Enters from date.
        /// </summary>
        /// <param name="fromDate"> The from date. </param>
        /// <returns> The <see cref="AnalyticsDateRangeDropdown"/>. </returns>
        public AnalyticsDateRangeDropdown EnterFromDate(string fromDate)
        {
            DriverExtensions.GetElement(FromDateLabelLocator).Click();
            DriverExtensions.GetElement(FromDateInputLocator).SendKeys(Keys.LeftShift + Keys.Home);
            DriverExtensions.GetElement(FromDateInputLocator).SendKeys(fromDate);
            return this;
        }

        /// <summary>
        /// Enters to date.
        /// </summary>
        /// <param name="toDate"> The to date. </param>
        /// <returns> The <see cref="AnalyticsDateRangeDropdown"/>. </returns>
        public AnalyticsDateRangeDropdown EnterToDate(string toDate)
        {
            DriverExtensions.GetElement(ToDateLabelLocator).Click();
            DriverExtensions.GetElement(ToDateInputLocator).SendKeys(Keys.LeftShift + Keys.Home);
            DriverExtensions.GetElement(ToDateInputLocator).SendKeys(toDate);
            return this;
        }

        /// <summary>
        /// Clicks the 'Save' button locator.
        /// </summary>
        /// <returns> The <see cref="AnalyticsPage"/>. </returns>
        public AnalyticsPage ClickSaveButton()
        {
            DriverExtensions.WaitForElement(SaveButtonLocator).Click();
            return new AnalyticsPage();
        }

        /// <summary>
        /// Get Options From dropdown
        /// </summary>
        protected override IEnumerable<AnalyticsDateRangeOptions> OptionsFromExpandedDropdown
            =>
                DriverExtensions.GetElements(DropdownLocator, DropdownOption)
                                .Select(elem => elem.Text.GetEnumValueByText<AnalyticsDateRangeOptions>())
                                .ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Annotation Options Map
        /// </summary>
        protected EnumPropertyMapper<AnalyticsDateRangeOptions, WebElementInfo> DateRangeMap
            =>
                this.dateRangeMap =
                    this.dateRangeMap ?? EnumPropertyModelCache.GetMap<AnalyticsDateRangeOptions, WebElementInfo>();

        /// <summary>
        /// Verify that option is selected
        /// </summary>
        /// <param name="option"> Option to verify </param>
        /// <returns> True if selected, false otherwise </returns>
        public override bool IsSelected(AnalyticsDateRangeOptions option) => this.SelectedOption == option;

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option"> Option to Select </param>
        protected override void SelectOptionFromExpandedDropdown(AnalyticsDateRangeOptions option)
        {
            DriverExtensions.WaitForElement(By.XPath(this.DateRangeMap[option].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verify that dropdown is expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.IsDisplayed(ExpandedDropdownLocator);

        /// <summary>
        /// Click on the dropdown
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.WaitForElement(DropdownButtonLocator).Click();
    }
}
