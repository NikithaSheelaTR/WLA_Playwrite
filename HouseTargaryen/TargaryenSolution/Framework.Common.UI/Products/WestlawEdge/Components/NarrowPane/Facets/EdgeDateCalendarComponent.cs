namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The Indigo date facet calendar component.
    /// </summary>
    public class EdgeDateCalendarComponent : BaseModuleRegressionComponent
    {
        private const string TableDayItemLctMask = ".//*[contains(@class,'ui-state-default') and text()={0}]";

        private static readonly By ContainerLocator = By.Id("ui-datepicker-div");
        private static readonly By MonthDropDownLocator = By.XPath(".//*[@class='ui-datepicker-month']");
        private static readonly By YearDropDownLocator = By.XPath(".//*[@class='ui-datepicker-year']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// set Date using calendar
        /// </summary>
        public void SetDateInCalendar(string firstValue)
        {
            this.ApplyYearDropDownByYear(DateTime.Parse(firstValue).ToString("yyyy"));
            this.ApplyMonthDropDownByMonthShortName(DateTime.Parse(firstValue).ToString("MMM"));
            this.ClickOnTableDay(DateTime.Parse(firstValue).Day);
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// Get all options from Year DropDown.
        /// </summary>
        public List<string> GetYearDropDownOptions()
            => DriverExtensions.GetDropdownOptionElements(YearDropDownLocator)
            .Select(option => option.Text)
                               .ToList();

        /// <summary>
        /// The apply month drop down by month short name.
        /// </summary>
        /// <param name="shortMonthName">
        /// The short month name.
        /// </param>
        private void ApplyMonthDropDownByMonthShortName(string shortMonthName)
            => DriverExtensions.GetElement(this.ComponentLocator, MonthDropDownLocator).SetDropdown(shortMonthName);

        /// <summary>
        /// The apply year drop down by year.
        /// </summary>
        /// <param name="year">
        /// The year.
        /// </param>
        private void ApplyYearDropDownByYear(string year)
            => DriverExtensions.GetElement(this.ComponentLocator, YearDropDownLocator).SetDropdown(year);

        /// <summary>
        /// The click on table day.
        /// </summary>
        /// <param name="dayNumber">
        /// The day number.
        /// </param>
        private void ClickOnTableDay(int dayNumber)
            =>
            DriverExtensions.GetElement(this.ComponentLocator, SafeXpath.BySafeXpath(TableDayItemLctMask, dayNumber))
                            .Click();

    }
}
