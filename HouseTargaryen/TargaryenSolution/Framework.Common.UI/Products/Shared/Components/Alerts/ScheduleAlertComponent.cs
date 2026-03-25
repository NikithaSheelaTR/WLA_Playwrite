namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.UI;

    /// <summary>
    /// Schedule Section for Alert
    /// </summary>
    public class ScheduleAlertComponent : BaseAlertComponent
    {
        private const string HourSelectDropdownLctMask = "//select[@id='hourDropDown']/option[text()='{0}']";

        private static readonly By AlertAmPmScheduleCheckboxesLocator = By.CssSelector(".co_showState>.co_hourInput");

        private static readonly By AlertAmScheduleContainerLocator = By.Id("alertAMContainer");

        private static readonly By AlertPmScheduleContainerLocator = By.Id("alertPMContainer");

        private static readonly By AmExecutionTimeLocator =
            By.XPath("//label[@class='co_checked'][contains(@id,'amExecutionTimeLabel')]");

        private static readonly By HourSelectDropDownLocator = By.Id("hourDropDown");

        private static readonly By PmExecutionTimeLocator =
            By.XPath("//label[@class='co_checked'][contains(@id,'pmExecutionTimeLabel')]");

        private static readonly By ReportPausedLabelLocator = By.XPath("//h3[text()='This Report is Paused']");

        private static readonly By SaveAlertButtonLocator = By.Id("co_button_saveAlert");

        private static readonly By SchedulePauseButtonLocator = By.Id("co_pause");

        private static readonly By TimeZoneCentralTimeLabelLocator = By.CssSelector("#alertAtTheseTimesContainer>legend>p");

        private static readonly By TimeZoneSelectDropDownLocator = By.Id("co_formSelect_timeZones");

        private static readonly By WeekDayDropdownLocator = By.Id("weekdayDropDown");

        private static readonly By AlertEvenNoResultsCheckboxLocator = By.XPath("//input[@id='co_noResultsAlert']");

        private static readonly By ContainerLocator = By.Id("scheduleSection");

        private static readonly By AlertScheduleFrequencyDropdownLocator = By.CssSelector("#frequencySelect");

        private static readonly By ScheduleAlertHeaderLabelLocator = By.XPath(".//h2[@id='scheduleBellowHeader']/strong");

        private static readonly By AlertAtThisTimeLabelLocator = By.XPath(".//*[@id='alertAtTheseTimesContainer']//label");

        private static readonly By EndDateLabelLocator = By.XPath(".//ul[@id='scheduleLeftColumn']//label");
        
        /// <summary>
        /// This is the instance of the AlertsScheduleFrequencyDropdown we will use in the Schedule Alert Component
        /// </summary>
        public IDropdown<AlertsScheduleFrequencyOptions> AlertsScheduleFrequency { get; } =
            new Dropdown<AlertsScheduleFrequencyOptions>(AlertScheduleFrequencyDropdownLocator);

        /// <summary>
        /// Time to select
        /// </summary>
        /// <returns></returns>
        public SheduleAlertTimeBoxesComponent AlertTimeBoxes { get; } = new SheduleAlertTimeBoxesComponent();

        /// <summary>
        ///  Schedule alert header label
        /// </summary>
        public ILabel ScheduleAlertHeaderLabel => new Label(this.ComponentLocator, ScheduleAlertHeaderLabelLocator);

        /// <summary>
        ///  Alert at this time label
        /// </summary>
        public ILabel AlertAtThisTimeLabel => new Label(this.ComponentLocator, AlertAtThisTimeLabelLocator);

        /// <summary>
        ///  End date label
        /// </summary>
        public ILabel EndDateLabel => new Label(this.ComponentLocator, EndDateLabelLocator);

        /// <summary>
        /// Save alert button
        /// </summary>
        public IButton SaveAlertButton => new Button(this.ComponentLocator, SaveAlertButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the save alert button
        /// </summary>
        /// <typeparam name="T">page object</typeparam>
        /// <param name="waitForValidation">specify wait or not alert validation</param>
        /// <returns>new page object</returns>
        public T ClickSaveAlertButton<T>(bool waitForValidation = true) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SaveAlertButtonLocator).Click();

            if (waitForValidation)
            {
                this.WaitForValidation();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the schedule pause button
        /// </summary>
        /// <returns> The <see cref="ScheduleAlertComponent"/>. </returns>
        public ScheduleAlertComponent ClickSchedulePauseButton()
        {
            DriverExtensions.Click(SchedulePauseButtonLocator);
            return this;
        }

        /// <summary>
        /// Get Am Pm Schedule Checkboxes Count
        /// </summary>
        /// <returns>Count of checkboxes</returns>
        public int GetAmPmScheduleCheckboxesCount() => DriverExtensions.GetElements(AlertAmPmScheduleCheckboxesLocator).Count;

        /// <summary>
        /// GetTimeZoneCentralTimeLabelText (only for Daily and Weekdays)
        /// </summary>
        /// <returns>Label Text or empty string if element is not displayed</returns>
        public string GetTimeZoneCentralTimeLabelText()
            => DriverExtensions.IsDisplayed(TimeZoneCentralTimeLabelLocator)
            ? DriverExtensions.GetText(TimeZoneCentralTimeLabelLocator)
            : string.Empty;

        /// <summary>
        /// Get TimeZone DropDown selected option text
        /// </summary>
        /// <returns>TimeZone DropDown selected option text</returns>
        public string GetTimeZoneDropDownText() => DriverExtensions.GetSelectedDropdownOptionText(TimeZoneSelectDropDownLocator);

        /// <summary>
        /// Verify that Time Zone dropdown is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTimeZoneDropDownDisplayed() => DriverExtensions.IsDisplayed(TimeZoneSelectDropDownLocator);

        /// <summary>
        /// Check if Am Schedule Container Displayed
        /// </summary>
        /// <returns>True if if Am Schedule Container is displayed, false otherwise</returns>
        public bool IsAmScheduleContainerDisplayed() => DriverExtensions.IsDisplayed(AlertAmScheduleContainerLocator);

        /// <summary>
        /// Verify alert checked execution time is as expected
        /// </summary>
        /// <param name="time"> Checked alert execution time </param>
        /// <returns> True if alert execution time is as specified, false otherwise </returns>
        public bool IsExecutionTimeCorrect(List<int> time)
        {
            List<int> timeElementsList =
                DriverExtensions.GetElements(AmExecutionTimeLocator).Select(element => int.Parse(element.Text)).ToList();

            List<int> pmElementsList =
                DriverExtensions.GetElements(PmExecutionTimeLocator).Select(element => int.Parse(element.Text) + 12).ToList();

            timeElementsList.AddRange(pmElementsList);

            return timeElementsList.SequenceEqual(time);
        }

        /// <summary>
        /// Check if Pm Schedule Container Displayed
        /// </summary>
        /// <returns>True if Pm Schedule Container is displayed, false otherwise</returns>
        public bool IsPmScheduleContainerDisplayed() => DriverExtensions.IsDisplayed(AlertPmScheduleContainerLocator);

        /// <summary>
        /// Verify Alert is paused
        /// </summary>
        /// <returns>true if Alert is paused, false otherwise</returns>
        public bool IsReportPaused() => DriverExtensions.IsDisplayed(ReportPausedLabelLocator, 5);

        /// <summary>
        /// Selects the Hour Dropdown option based on the value given
        /// </summary>
        /// <param name="value"> Hour value </param>
        /// <returns> The <see cref="ScheduleAlertComponent"/>. </returns>
        public ScheduleAlertComponent SetHourDropDownByValue(string value)
        {
            // Expand dropdown
            DriverExtensions.WaitForElement(HourSelectDropDownLocator).Click();

            // Select option
            DriverExtensions.WaitForElement(By.XPath(string.Format(HourSelectDropdownLctMask, value))).Click();

            // Collapse dropdown
            DriverExtensions.WaitForElement(HourSelectDropDownLocator).Click();

            return this;
        }

        /// <summary>
        /// Select WeekDay option
        /// </summary>
        /// <param name="weekDay"> Day of week </param>
        /// <returns> The <see cref="ScheduleAlertComponent"/>. </returns>
        public ScheduleAlertComponent SelectWeekDayOption(string weekDay)
        {
            new SelectElement(DriverExtensions.WaitForElement(WeekDayDropdownLocator)).SelectByText(weekDay);
            return new ScheduleAlertComponent();
        }

        /// <summary>
        /// Set 'Alert Even No results' checkbox
        /// </summary>
        /// <param name="isSet"> True to check, false - to uncheck </param>
        /// <returns> The <see cref="ScheduleAlertComponent"/>. </returns>
        public ScheduleAlertComponent SetAlertEvenNoResultsCheckbox(bool isSet = true)
        {
            DriverExtensions.SetCheckbox(AlertEvenNoResultsCheckboxLocator, isSet);
            return new ScheduleAlertComponent();
        }
    }
}