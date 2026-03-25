namespace Framework.Common.UI.Products.WestLawAnalytics.Dialogs
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The monthly date picker dialog.
    /// </summary>
    public class MonthDatepickerDialog : BaseModuleRegressionDialog
    {
        private static readonly By DoneButtonLocator = By.XPath("//button[contains(@class,'ui-datepicker-close')]");

        private static readonly By PreviousArrowButtonLocator = By.XPath("//a[contains(@class,'ui-datepicker-prev')]");

        private static readonly By NextArrowButtonLocator = By.XPath("//a[contains(@class,'ui-datepicker-next')]");

        private static readonly By SelectMonthDatepickerDropdownLocator = By.ClassName("ui-datepicker-month");

        private static readonly By SelectYearDatepickerDropdownLocator = By.ClassName("ui-datepicker-year");
        
        /// <summary>
        /// Gets the select month dropdown.
        /// </summary>
        public IDropdown<SelectMonthDatepickerOptions> SelectMonthDropdown { get; } = new Dropdown<SelectMonthDatepickerOptions>(SelectMonthDatepickerDropdownLocator);

        /// <summary>
        /// Gets the select year dropdown.
        /// </summary>
        public IDropdown<SelectYearDatepickerOptions> SelectYearDropdown { get; } = new Dropdown<SelectYearDatepickerOptions>(SelectYearDatepickerDropdownLocator);

        /// <summary>
        /// Clicks done button.
        /// </summary>
        /// <returns> The <see cref="CostRecoveryCapsPage"/>. </returns>
        public CostRecoveryCapsPage ClickDoneButton() => this.ClickElement<CostRecoveryCapsPage>(DoneButtonLocator);

        /// <summary>
        /// Clicks previous arrow button.
        /// </summary>
        /// <returns> The <see cref="MonthDatepickerDialog"/>. </returns>
        public MonthDatepickerDialog ClickPreviousArrowButton() => this.ClickElement<MonthDatepickerDialog>(PreviousArrowButtonLocator);

        /// <summary>
        /// Clicks next arrow button.
        /// </summary>
        /// <returns> The <see cref="MonthDatepickerDialog"/>. </returns>
        public MonthDatepickerDialog ClickNextArrowButton() => this.ClickElement<MonthDatepickerDialog>(NextArrowButtonLocator);

        /// <summary>
        /// Verifies that done button is displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if done button is displayed. </returns>
        public bool IsDoneButtonDisplayed() => DriverExtensions.IsDisplayed(DoneButtonLocator);

        /// <summary>
        /// Verifies that previous arrow button displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if previous arrow button is displayed. </returns>
        public bool IsPreviousArrowButtonDisplayed() => DriverExtensions.IsDisplayed(PreviousArrowButtonLocator);

        /// <summary>
        /// Verifies that next arrow button displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if next arrow button is displayed. </returns>
        public bool IsNextArrowButtonDisplayed() => DriverExtensions.IsDisplayed(NextArrowButtonLocator);
    }
}
