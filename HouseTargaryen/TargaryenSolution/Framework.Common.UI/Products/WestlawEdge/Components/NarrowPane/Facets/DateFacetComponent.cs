namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Date Facet Component
    /// </summary>
    public sealed class DateFacetComponent : EdgeBaseFacetComponent
    {
        private const string SpecificDateContainerLctMask = "//header[contains(@id, 'SearchFacetRange') and .//span[text() = '{0}']]";

        private static readonly By DateAllOptionLocator =
            By.ClassName("SearchFacetOneClick");

        private static readonly By AppliedOptionLocator = By.XPath("//*[contains(@class,'SearchFacet-outputText')]");

        private static readonly By DateFacetLocator = By.XPath(".//*[@class='SearchFacet-header']//*[contains(text(),'Date')]");

        private static readonly By DateFacetActiveOptionLocator = By.XPath(
            "//div[@class='SearchFacet-selectListItem is-active' or @class='SearchFacet-selectListItem SearchFacet-RowReverse is-active']//following-sibling::span");

        private static readonly By DateFacetActiveOptionTextFieldLocator =
            By.XPath("//span[@class='SearchFacet-outputTextValue']");

        private static readonly By SelectEndDateButtonLocator = By.XPath(
            "//input[contains(@id,'SearchFacetRange-') and contains(@id,'End')] /following-sibling::*");

        private static readonly By CalendarIconLocator = By.XPath("//input[contains(@id,'SearchFacetRange-')]//following-sibling::*");

        private static readonly By CalendarLocator = By.Id("ui-datepicker-div");

        private static readonly By DateFacetItemLocator = By.XPath(
            "//div[contains(@class, 'SearchFacet-selectListItem')]//following-sibling::span[not(contains(@class, 'SearchFacet-outputText'))]");

        private static readonly By DoneButtonLocator = By.XPath("//button[contains(@class,'SearchFacet-buttonApply')] | //div[contains(@class, 'la-DateFilter')]//button[@class='co_primaryBtn'] | //*[@class='co_facet_sad_dateSelect']//*[@value='Go']");        

        private static readonly By ErrorMessageLocator = By.ClassName("SearchFacet-errorText");

        private static readonly By EditLinkLocator = By.XPath("//button[text()='Edit']");

        private static readonly By RemoveLinkLocator = By.XPath("//*[contains(@class,'button') and text()='Remove'] | //*[contains(@class,'Button') and text()='Remove']");

        private static readonly By FacetItemLocator = By.XPath("//*[@role='listitem']");

        private static readonly By OptionsWithCountLocator =
            By.XPath("//*[@class='SearchFacet-outputTextValue']/ancestor::label//span[contains(text(),'Last')]");

        private static readonly By ContainerLocator = By.XPath("//*[contains(@id, 'ateHeader')] | //ta-date-facet-container | //div[@id='facet_div_Date'] | //div[@id='facet_div_date'] | //div[@id='facet_div_enhancedDate']");

        private static readonly By SpinnerLocator = By.ClassName("co_loading");

        private readonly By containerLocator;

        private EnumPropertyMapper<EdgeDateRangeOptions, DateRangeOptionsModel> dateRangeOptionsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFacetComponent"/> class. 
        /// </summary>
        /// <param name="additionalInfo">
        /// The additional Info parameter is empty by default and set to 'Ri' for Date facet on Related info tab. 
        /// It is needed to map correctly Ri date options, as date facets on search and related info use different locators. 
        /// </param>
        public DateFacetComponent(string additionalInfo = "")
        {
            this.dateRangeOptionsMap = EnumPropertyModelCache.GetMap<EdgeDateRangeOptions, DateRangeOptionsModel>(
                additionalInfo, @"Resources/EnumPropertyMaps/WestlawEdge");
            this.containerLocator = ContainerLocator;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateFacetComponent"/> class. 
        /// </summary>
        public DateFacetComponent(string specificDate, string additionalInfo = "")
        {
            this.dateRangeOptionsMap = EnumPropertyModelCache.GetMap<EdgeDateRangeOptions, DateRangeOptionsModel>(
                additionalInfo, @"Resources/EnumPropertyMaps/WestlawEdge");
            this.containerLocator = By.XPath(string.Format(SpecificDateContainerLctMask, specificDate));
        }

        /// <summary>
        /// Calendar
        /// </summary>
        public EdgeDateCalendarComponent EdgeStartDateCalendarComponent => this.OpenCalendar(CalendarIconLocator);

        /// <summary>
        /// Calendar
        /// </summary>
        public EdgeDateCalendarComponent EdgeEndDateCalendarComponent => this.OpenCalendar(
            SelectEndDateButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => this.containerLocator;

        /// <summary>
        /// Click on Edit link after the date was applied
        /// </summary>
        public void ClickEditLink() => DriverExtensions.GetElement(EditLinkLocator).Click();

        /// <summary>
        /// Is Edit link displayed
        /// </summary>
        /// <returns>True if Edit link is displayed, false if it not displayed</returns>
        public bool IsEditLinkDisplayed() => DriverExtensions.IsDisplayed(EditLinkLocator);

        /// <summary>
        /// Click on Remove link after the date was applied
        /// </summary>
        public void ClickRemoveLink()
        {
            DriverExtensions.GetElement(RemoveLinkLocator).Click();
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Is Remove link displayed
        /// </summary>
        /// <returns>True if Remove link is displayed, false if it not displayed</returns>
        public bool IsRemoveLinkDisplayed() => DriverExtensions.IsDisplayed(RemoveLinkLocator);

        /// <summary>
        /// The get applied option.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetAppliedOption() => DriverExtensions.IsElementPresent(AppliedOptionLocator) ? 
            DriverExtensions.WaitForElement(AppliedOptionLocator).Text : string.Empty;

        /// <summary>
        /// Verify is option clickable
        /// </summary>
        /// <param name="option">option name</param>
        /// <returns>true if clickable, false otherwise</returns>
        public bool IsOptionEnabled(EdgeDateRangeOptions option)
        {
            this.ExpandFacet();
            return DriverExtensions.IsEnabled(By.XPath(this.dateRangeOptionsMap[option].LocatorString));

        }

        /// <summary>
        /// Gets a list of all the date dropdown options
        /// </summary>
        /// <returns>A list of date facet options</returns>
        public List<EdgeDateRangeOptions> GetDateFacetOptions()
        {
            this.ExpandFacet();
            List<string> stringResults = DriverExtensions.GetElements(DateFacetItemLocator)
                                                         .Select(e => e.Text.ToLower())
                                                         .ToList();
            List<EdgeDateRangeOptions> finalResults = stringResults
                .Select(
                    text => text.GetEnumValueByText<EdgeDateRangeOptions>(
                        string.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge"))
                .ToList();

            return finalResults;
        }

        /// <summary>
        /// Gets a list of options that has count
        /// </summary>
        /// <returns>list of date facet options</returns>
        public List<EdgeDateRangeOptions> GetOptionsWithCount()
        {
            this.ExpandFacet();
            List<string> stringResults = DriverExtensions
                .GetElements(OptionsWithCountLocator).Select(e => e.Text.ToLower())
                .Select(el => el.Replace("Select", string.Empty)).ToList();
            List<EdgeDateRangeOptions> finalResults = stringResults
                .Select(
                    text => text.GetEnumValueByText<EdgeDateRangeOptions>(
                        string.Empty,
                        @"Resources/EnumPropertyMaps/WestlawEdge"))
                .ToList();

            return finalResults;
        }

        /// <summary>
        /// Gets the count for a specific date range
        /// </summary>
        /// <param name="dateRange">What date range to get the count for</param>
        /// <returns>The count</returns>
        public int GetDateRangeCount(EdgeDateRangeOptions dateRange)
        {
            this.ExpandFacet();
            int count = this.GetCount(By.XPath(this.dateRangeOptionsMap[dateRange].OptionCountLocatorString));
            DriverExtensions.GetElement(DateFacetLocator).Click();

            return count;
        }

        /// <summary>
        /// Get Date facet active option
        /// </summary>
        /// <returns>option text</returns>
        public string GetDateFacetActiveOption()
        {
            DriverExtensions.ScrollTo(DateFacetLocator);
            this.ExpandFacet();
            return DriverExtensions.IsElementPresent(DateFacetActiveOptionLocator)
                ? DriverExtensions.GetElement(DateFacetActiveOptionLocator).Text
                : DriverExtensions.GetElement(DateFacetActiveOptionTextFieldLocator).Text;
        }

        /// <summary>
        /// Applies a date range facet
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="dateRange"> Date range to apply </param>
        /// <param name="firstValue"> the value to input in the first field (if any) </param>
        /// <param name="secondValue"> the value to input in the second field (if any) </param>
        /// <param name="isCalendar"> use calendar for run date facet filter
        /// if isCalendar = false  - value enter in search input
        /// if isCalendar = true  - value is entered using calendar </param>
        /// <returns> A new search result page </returns>
        public T ApplyFacet<T>(
            EdgeDateRangeOptions dateRange,
            string firstValue = null,
            string secondValue = null,
            bool isCalendar = false)
            where T : ICreatablePageObject
        {
            this.ExpandFacet();
            By locator = By.XPath(string.Format(this.dateRangeOptionsMap[dateRange].LocatorString));
            DriverExtensions.JavascriptClick(locator);
            DriverExtensions.WaitForJavaScript();

            if (!isCalendar)
            {
                this.SendKeysToDateInput(dateRange, firstValue, secondValue);
            }
            else
            {
                this.EdgeStartDateCalendarComponent.SetDateInCalendar(firstValue);
                if (secondValue != null)
                {
                    this.EdgeEndDateCalendarComponent.SetDateInCalendar(secondValue);
                }
            }

            if (DriverExtensions.IsDisplayed(DoneButtonLocator, 5))
            {
                DriverExtensions.JavascriptClick(DoneButtonLocator);
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get all facet items and all their child items through the tree view. 
        /// </summary>
        /// <returns>List of jurisdiction items</returns>
        private List<FacetOptionItem> GetAllFacetItems() => DriverExtensions
            .GetElements(DateAllOptionLocator, FacetItemLocator).Select(el => new FacetOptionItem(el)).ToList();

        /// <summary>
        /// Is Done Button Displayed
        /// </summary>
        /// <returns>true if present</returns>
        public bool IsDoneButtonDisplayed() => DriverExtensions.IsDisplayed(DoneButtonLocator, 5);

        /// <summary>
        /// Gets error message text 
        /// </summary>
        /// <returns> The <see cref="string"/>. Error message text </returns>
        public string GetErrorMessageText() => DriverExtensions.WaitForElement(ErrorMessageLocator).Text;

        /// <summary>
        /// Verifies that the date facet options are displayed.
        /// </summary>
        /// <returns> The <see cref="bool"/>. True if date dropdown is displayed. </returns>
        public bool AreAllDateFacetOptionsDisplayed() => DriverExtensions.IsDisplayed(DateAllOptionLocator);

        /// <summary>
        /// Expand/Collapse Date Facet
        /// </summary>
        public void ClickDateFacet()
        {
            if (!DriverExtensions.IsDisplayed(DateAllOptionLocator, 5))
            {
                DriverExtensions.Click(this.ComponentLocator);
            }
        }

        private EdgeDateCalendarComponent OpenCalendar(By by)
        {
            if (DriverExtensions.GetElement(CalendarLocator).GetCssValue("display").Equals("none"))
            {
                DriverExtensions.Click(DriverExtensions.GetElement(by));
            }

            return new EdgeDateCalendarComponent();
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <param name="elementLocator">locator</param>
        /// <returns>int</returns>
        private int GetCount(By elementLocator) => DriverExtensions
            .WaitForElement(elementLocator)
            .Text.ConvertCountToInt();

        /// <summary>
        /// Setts text field
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="locator">Locator</param>
        private void SetValue(string value, By locator)
        {
            if (value != null)
            {
                DriverExtensions.JavascriptClick(locator);
                DriverExtensions.SetTextField(value, locator);
            }
        }

        private void SendKeysToDateInput(EdgeDateRangeOptions dateRange, string firstValue, string secondValue)
        {
            this.SetValue(secondValue, By.XPath(this.dateRangeOptionsMap[dateRange].SecondFieldLocatorString));
            this.SetValue(firstValue, By.XPath(this.dateRangeOptionsMap[dateRange].FirstFieldLocatorString));
        }
    }
}