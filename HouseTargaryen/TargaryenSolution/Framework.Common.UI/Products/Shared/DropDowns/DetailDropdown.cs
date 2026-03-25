namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <inheritdoc />
    /// <summary>
    /// This is DetailDropdown in the toolbar
    /// </summary>
    public class DetailDropdown : BaseModuleRegressionCustomDropdown<DetailLevel>
    {
        private static readonly By DropdownOptionLocator = By.XPath(".//li[./a[contains(@title,'Detail')]]");

        private static readonly By DropdownLocator = By.XPath("//div[contains(@id, 'DetailSliderTab')]");

        private static readonly By DetailLevelIconLocator = By.XPath(".//li[@aria-checked='true']//span[@class='a11yDropdown-itemText'] | .//button[contains(@id,'DetailSliderLink')]/span");
        private By dropdownLocator;

        private EnumPropertyMapper<DetailLevel, WebElementInfo> detailLevelMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailDropdown"/> class.
        /// </summary>
        public DetailDropdown() => this.dropdownLocator = DropdownLocator;
   
        /// <summary>
        /// Initializes a new instance of the <see cref="DetailDropdown"/> class.
        /// </summary>
        /// <param name="detailDropdownLocator">Detail dropdown element</param>
        public DetailDropdown(By detailDropdownLocator) => this.dropdownLocator = detailDropdownLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="DetailDropdown"/> class.
        /// </summary>
        /// <param name="container">Container</param>
        /// <param name="detailDropdownLocator">Detail dropdown element</param>
        public DetailDropdown(By container, By detailDropdownLocator) =>
            this.dropdownLocator = new ByChained(container, detailDropdownLocator);

        /// <inheritdoc />
        /// <summary>
        /// Return Selected Option.
        /// </summary>
        public override DetailLevel SelectedOption
        {
            get
            {
                IWebElement checkedElement = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("class").Contains("checked")
                            || elem.GetAttribute("class")
                                   .Contains("selected"));
                string checkedElementText =
                    DriverExtensions.WaitForElement(checkedElement, By.TagName("a")).GetAttribute("title");

                return checkedElementText.GetEnumValueByText<DetailLevel>();
            }
        }

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<DetailLevel> OptionsFromExpandedDropdown
        {
            get
            {
                return DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                    elem => DriverExtensions
                        .GetElement(elem, By.TagName("a")).GetAttribute("title")
                        .GetEnumValueByText<DetailLevel>()).ToList();
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// The dropdown container element 
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.dropdownLocator);

        /// <summary>
        /// Detail Level Map
        /// </summary>
        protected EnumPropertyMapper<DetailLevel, WebElementInfo> DetailLevelMap =>
            this.detailLevelMap = this.detailLevelMap ?? EnumPropertyModelCache.GetMap<DetailLevel, WebElementInfo>();

        /// <inheritdoc />
        /// <summary>
        /// Verify that detailLevel option is selected
        /// </summary>
        /// <param name="detailLevel">The desired detail Level</param>
        /// <returns> True if selected, false otherwise </returns>
        public override bool IsSelected(DetailLevel detailLevel)
        {
            this.ExpandIfNotExpanded();
            return string.Equals(
                   DriverExtensions.GetElement(this.Dropdown, DetailLevelIconLocator).Text,
                   this.DetailLevelMap[detailLevel].Text,
                   StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Verifies if Detail drop down is displayed link displayed
        /// </summary>
        /// <returns>The instance of the page</returns>
        /// <returns></returns>
        public override bool IsDisplayed() => DriverExtensions.GetElement(DropdownLocator).IsDisplayed();

        /// <inheritdoc />
        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">The desired option</param>
        protected override void SelectOptionFromExpandedDropdown(DetailLevel option)
        {
            DriverExtensions.Click(
                DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.DetailLevelMap[option].LocatorString)));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.GetElement(this.Dropdown, By.TagName("ul")).GetAttribute("style").Contains("block");


        /// <summary>
        /// Click DropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            var button = DriverExtensions.WaitForElement(this.Dropdown, By.TagName("button"));
            DriverExtensions.Click(button);
            DriverExtensions.WaitForJavaScript();
        }
    }
}