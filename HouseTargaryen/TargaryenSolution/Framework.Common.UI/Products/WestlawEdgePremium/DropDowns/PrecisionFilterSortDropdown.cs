namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// Precision Filters Sort dropdown
    /// </summary>
    public sealed class PrecisionFilterSortDropdown : BaseModuleRegressionCustomDropdown<PrecisionFilterSortOptions>
    {
        private static readonly By DropdownOptionLocator = By.XPath("//div[contains(@class, 'Tab-panel--show')]//li[contains(@class, 'a11yDropdown-item')]");
        private static readonly By DropdownExpanderLocator = By.XPath("//div[contains(@class, 'Tab-panel--show')]//button");

        private EnumPropertyMapper<PrecisionFilterSortOptions, WebElementInfo> sortOptionsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrecisionFilterSortDropdown"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public PrecisionFilterSortDropdown(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        public override PrecisionFilterSortOptions SelectedOption
        {
            get
            {
                this.ExpandIfNotExpanded();
                IWebElement checkedElement = DriverExtensions.GetElements(DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Contains("true"));
                string checkedElementText = checkedElement.Text;

                return checkedElementText.GetEnumValueByText<PrecisionFilterSortOptions>("", @"Resources\EnumPropertyMaps\WestlawEdgePremium");
            }
        }

        /// <summary>
        /// The options from expanded dropdown.
        /// </summary>
        protected override IEnumerable<PrecisionFilterSortOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                            .Select(elem => elem.Text.GetEnumValueByText<PrecisionFilterSortOptions>()).ToList();

        /// <inheritdoc />
        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.Container;

        /// <summary>
        /// Sort Options Map
        /// </summary>
        private EnumPropertyMapper<PrecisionFilterSortOptions, WebElementInfo> SortOptionsMap =>
            this.sortOptionsMap = this.sortOptionsMap ?? EnumPropertyModelCache.GetMap<PrecisionFilterSortOptions, WebElementInfo>(
                                      "",
                                      @"Resources\EnumPropertyMaps\WestlawEdgePremium");

        /// <summary>
        /// Gets the container.
        /// </summary>
        private IWebElement Container { get; }

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsSelected(PrecisionFilterSortOptions option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.GetElement(this.Container, By.XPath(this.SortOptionsMap[option].LocatorString))
                                   .GetAttribute("aria-checked").Equals("true");
        }

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(PrecisionFilterSortOptions option)
        {
            var dropdownOption = DriverExtensions.WaitForElement(this.Dropdown, By.XPath( this.SortOptionsMap[option].LocatorString));
            dropdownOption.CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// custom click on dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).Click();

        /// <summary>
        /// Verifies if dropdown is expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.GetElement(this.Dropdown, By.TagName("ul")).GetAttribute("class").Equals("a11yDropdown-menu");
    }
}