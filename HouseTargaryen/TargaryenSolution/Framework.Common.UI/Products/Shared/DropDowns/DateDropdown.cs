namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// DateDropdown
    /// </summary>
    public class DateDropdown : BaseModuleRegressionCustomDropdown<DateRangeOptions>
    {
        private static readonly By DropdownArrowLocator = By.XPath(".//a[contains(@class,'co_dropdownArrow')]");
        private static readonly By DropdownLocator = By.XPath("//div[@class='co_facet_sad_footerContainer']");
        private static readonly By DateFacetOptionLocator = By.XPath(".//*[contains(@id,'co_dateWidget')]/a[@href]");
        private static readonly By SelectedOptionLocator = By.XPath(".//*[contains(@id,'dateWidget') and contains(@id,'listItem')]/span");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="DateDropdown"/> class. 
        /// </summary>
        /// <param name="container"> container</param>
        public DateDropdown(By container)
        {
            this.Container = DriverExtensions.WaitForElement(container);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateDropdown"/> class. 
        /// </summary>
        /// <param name="container"> container</param>
        public DateDropdown(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Dropdown (list of options)
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// ToDo should be deleted  and used  Dropdown except it
        /// </summary>
        private IWebElement Container { get; set; }

        private EnumPropertyMapper<DateRangeOptions, DateRangeOptionsModel> dateRangeOptionsMap;

        /// <summary>
        /// Gets the date options type enumeration to DateRangeOptionsModel map.
        /// </summary>
        protected EnumPropertyMapper<DateRangeOptions, DateRangeOptionsModel> DateRangeOptionsMap =>
            this.dateRangeOptionsMap = this.dateRangeOptionsMap
                                       ?? EnumPropertyModelCache.GetMap<DateRangeOptions, DateRangeOptionsModel>();



        /// <summary>
        /// Returns Selected Option
        /// </summary>
        /// <returns> Selected Date dropdown option</returns>
        public override DateRangeOptions SelectedOption {
            get
            {
                return DriverExtensions.GetElement(this.Container,SelectedOptionLocator).Text
                                .GetEnumValueByPropertyModel<DateRangeOptions, DateRangeOptionsModel>(
                                    option => option.Text);
            }
        }

        /// <summary>
        /// Get all options from expanded dropdown
        /// </summary>
        protected override IEnumerable<DateRangeOptions> OptionsFromExpandedDropdown
        {
            get
            {
                List<DateRangeOptions> options = DriverExtensions.GetElements(this.Dropdown, DateFacetOptionLocator)
                                                                 .Select(e => e.Text.GetEnumValueByText<DateRangeOptions>()).ToList();
                options.Add(DriverExtensions.GetElement(this.Dropdown, By.XPath(this.DateRangeOptionsMap[DateRangeOptions.All].LocatorString))
                                            .Text.GetEnumValueByText<DateRangeOptions>());
                return options;
            }
        }

        /// <summary>
        /// Get text from selected option
        /// </summary>
        /// <returns> Text of option </returns>
        public string GetSelectedOptionText() => DriverExtensions.GetElement(this.Container, SelectedOptionLocator).Text;

        /// <summary>
        /// Verify that date dropdown option is selected
        /// </summary>
        /// <param name="option"> date dropdown option </param>
        /// <returns> True if option is selected, false otherwise </returns>
        public override bool IsSelected(DateRangeOptions option) => option.Equals(this.SelectedOption);
        
        /// <summary>
        /// Select and fill option
        /// </summary>
        /// <param name="option"> option </param>
        /// <param name="firstDate"> the first date </param>
        /// <param name="secondDate"> the second date </param>
        /// <param name="isDropDown"> if true, selects option from dropdown; otherwise, selects from expanded dropdown </param>
        /// <returns> New instance of the page</returns>
        public T SelectAndFillOption<T>(DateRangeOptions option, string firstDate = null, string secondDate = null,bool isDropDown = true) where T: ICreatablePageObject
        {
            if (isDropDown)
            {
                this.SelectOption(option);
            }
            else {
                SelectOptionFromExpandedDropdown(option);
            }
            if (firstDate != null)
            {
                if (secondDate != null)
                {
                    DriverExtensions.SetTextField(secondDate, this.Container, By.XPath(this.dateRangeOptionsMap[option].SecondFieldLocatorString));
                }

                DriverExtensions.SetTextField(firstDate, this.Container, By.XPath(this.dateRangeOptionsMap[option].FirstFieldLocatorString));
                DriverExtensions.GetElement(By.XPath(this.dateRangeOptionsMap[option].SubmitButtonLocatorString)).Click();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the count for a specific date option
        /// </summary>
        /// <param name="option"> date range to get the count for</param>
        /// <returns> The <see cref="int"/>. </returns>
        public int GetDateRangeCount(DateRangeOptions option)
        {
            this.ClickDropdownArrow();
            int count = DriverExtensions.WaitForElement(this.Container, By.XPath(this.DateRangeOptionsMap[option].OptionCountLocator)).Text.ConvertCountToInt();
            this.ClickDropdownArrow();
            return count;
        }

        /// <summary>
        /// Verify that selected option is displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsSelectedOptionDisplayed() => DriverExtensions.IsDisplayed(this.Container, SelectedOptionLocator);

        /// <summary>
        /// Verify that date dropdown arrow is displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsDropdownArrowDisplayed() => DriverExtensions.IsDisplayed(this.Container, DropdownArrowLocator);

        /// <summary>
        /// Verify if first date option text field is present
        /// </summary>
        /// <param name="option"> date option </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsOptionFirstTextFieldPresent(DateRangeOptions option)
            => DriverExtensions.IsElementPresent(this.Container, By.XPath(this.DateRangeOptionsMap[option].FirstFieldLocatorString));

        /// <summary>
        /// Verify if second date option text field is present
        /// </summary>
        /// <param name="option"> date option </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsOptionSecondTextFieldPresent(DateRangeOptions option)
            => DriverExtensions.IsElementPresent(this.Container, By.XPath(this.DateRangeOptionsMap[option].SecondFieldLocatorString));

        /// <summary>
        /// Verify if error for first text field is present
        /// </summary>
        /// <param name="option"> date option </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsErrorForFirstTextFieldPresent(DateRangeOptions option)
            => DriverExtensions.IsElementPresent(this.Container, By.XPath(this.DateRangeOptionsMap[option].FirstErrorLocatorString));

        /// <summary>
        /// Verify if error for second text field is present
        /// </summary>
        /// <param name="option"> date option </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsErrorForSecondTextFieldPresent(DateRangeOptions option)
            => DriverExtensions.IsElementPresent(this.Container, By.XPath(this.DateRangeOptionsMap[option].SecondErrorLocatorString));

        /// <summary>
        /// Click dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.WaitForElement(this.Container, DropdownArrowLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Select option from expanded dropdown
        /// </summary>
        /// <param name="option"> option </param>
        protected override void SelectOptionFromExpandedDropdown(DateRangeOptions option)
        {
            DriverExtensions.WaitForElement(this.Container, By.XPath(this.DateRangeOptionsMap[option].LocatorString)).Click();
            DriverExtensions.WaitForJavaScript();
        }
        

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownClass = DriverExtensions.GetElement(this.Container).GetAttribute("class");
            return dropdownClass.Contains("expanded", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}