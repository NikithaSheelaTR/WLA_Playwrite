namespace Framework.Common.UI.Products.WestLawAnalytics.Components.Analytics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestLawAnalytics.Dropdowns.Analytics;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for InPlanVsOutOfPlanComponent and ChargeableToClientVsNotChargeableToClientComponent 
    /// that contains common for both classes methods
    /// </summary>
    public abstract class BaseAnalyticsTabComponent : BaseTabComponent
    {
        private static readonly By PageTitleLocator = By.XPath("//div[@id='wa_firmHealthTitle']");

        private static readonly By GraphLocator = By.Id("wa_firmHealthRaphael");

        private static readonly By TimeFrameLabelLocator = By.XPath("//div[@id='wa_firmHealthTimeFrameValue']");

        private static readonly By TotalTitleLocator = By.XPath(".//div[@class='wa_firmHealthTotalTitle']");

        private static readonly By TotalValueLocator = By.XPath(".//div[@class='wa_firmHealthTotalAggregate']");

        private static readonly By TotalCellLocator = By.ClassName("wa_firmHealthTotalCell");

        private static readonly By GraphByDropdownLocator =
            By.XPath("//select[@id='wa_firmHealthLineChartTimeFrameDropDownOptions']");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAnalyticsTabComponent"/> class. 
        /// </summary>
        protected BaseAnalyticsTabComponent() 
        {
            this.DateRangeDropdown = new AnalyticsDateRangeDropdown();
        }

        /// <summary>
        /// Analytics DateRange Dropdown
        /// </summary>
        public AnalyticsDateRangeDropdown DateRangeDropdown { get; }

        /// <summary>
        /// Graph By Dropdown
        /// </summary>
        public IDropdown<GraphByOptions> GraphByDropdown { get; } = new GraphByDropdown(GraphByDropdownLocator);

        /// <summary>
        /// Page title
        /// </summary>
        public override string Title => DriverExtensions.WaitForElement(PageTitleLocator).Text;

        /// <summary>
        /// Verifies that the time frame label correctly matches with the selected time frame
        /// </summary>
        /// <param name="range">The time frame we would like to verify</param>
        /// <returns>True if the time frame label is correct</returns>
        public bool IsCurrentDateCorrect(AnalyticsDateRangeOptions range)
        {
            bool check;
            switch (range)
            {
                case AnalyticsDateRangeOptions.LastMonth:
                    check = this.IsDateCorrect(this.GetFirstDayOfDate(1), this.GetLastDayOfDate(1), AnalyticsDateRangeOptions.LastMonth);
                    break;
                case AnalyticsDateRangeOptions.Last3Months:
                    check = this.IsDateCorrect(this.GetFirstDayOfDate(3), this.GetLastDayOfDate(3), AnalyticsDateRangeOptions.Last3Months);
                    break;
                case AnalyticsDateRangeOptions.Last6Months:
                    check = this.IsDateCorrect(this.GetFirstDayOfDate(6), this.GetLastDayOfDate(6), AnalyticsDateRangeOptions.Last6Months);
                    break;
                default:
                    check = false;
                    break;
            }

            return check;
        }

        /// <summary>
        /// Gets the Timeframe Label text as a string
        /// </summary>
        /// <returns>The timeframe label as a string</returns>
        public string GetTimeFrameLabelText() => DriverExtensions.GetText(TimeFrameLabelLocator);

        /// <summary>
        /// Verify that graph is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsGraphDisplayed() => DriverExtensions.IsDisplayed(GraphLocator, 5);

        /// <summary>
        /// Get total values
        /// </summary>
        /// <returns> Dictionary of total values </returns>
        protected Dictionary<string, string> GetTotalValues()
            => DriverExtensions.GetElements(TotalCellLocator).ToDictionary(
                                    elem => DriverExtensions.GetElement(elem, TotalTitleLocator).Text,
                                    elem => DriverExtensions.GetElement(elem, TotalValueLocator).Text);

        private bool IsDateCorrect(string firstDay, string lastDay, AnalyticsDateRangeOptions dateRange)
        {
            return DriverExtensions.GetText(TimeFrameLabelLocator).Contains(firstDay)
                            && DriverExtensions.GetText(TimeFrameLabelLocator).Contains(lastDay)
                            && this.DateRangeDropdown.SelectedOption == dateRange;
        }

        private string GetLastDayOfDate(int monthCount)
        {
            DateTime currentDate = DateTime.Today;
            DateTime lastMonthFirstDay = currentDate.AddDays(-(currentDate.Day - 1)).AddMonths(-monthCount);
            DateTime lastMonthLastDay;

            if (monthCount != 1)
            {
                lastMonthLastDay = currentDate.AddMonths(-1);
                lastMonthLastDay =
                       lastMonthLastDay.AddDays(
                           DateTime.DaysInMonth(lastMonthLastDay.Year, lastMonthLastDay.Month) - lastMonthLastDay.Day);
            }
            else
            {
                lastMonthLastDay =
                    lastMonthFirstDay.AddDays(DateTime.DaysInMonth(lastMonthFirstDay.Year, lastMonthFirstDay.Month) - 1);
            }

            string lastDay = lastMonthLastDay.GetDateTimeFormats()[8];
            lastDay = lastDay.Remove(3, lastDay.IndexOf(' ') - 3);
            return lastDay;
        }

        private string GetFirstDayOfDate(int monthCount)
        {
            DateTime currentDate = DateTime.Today;
            DateTime lastMonthFirstDay = currentDate.AddDays(-(currentDate.Day - 1)).AddMonths(-monthCount);

            string firstDay = lastMonthFirstDay.GetDateTimeFormats()[8];
            firstDay = firstDay.Remove(3, firstDay.IndexOf(' ') - 3);
          
            return firstDay;
        }
    }
}
