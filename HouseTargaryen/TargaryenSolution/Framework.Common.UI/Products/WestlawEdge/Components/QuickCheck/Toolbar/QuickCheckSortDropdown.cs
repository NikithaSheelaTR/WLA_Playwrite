namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The doc analyzer Quotations Sort dropdown.
    /// </summary>
    public class QuickCheckSortDropdown : BaseModuleRegressionCustomDropdown<QuickCheckSortOptions>
    {
        private static readonly By DropdownOptionLocator = By.XPath(".//li");
        private static readonly By DropdownExpanderLocator = By.XPath("./button");       

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckSortDropdown"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckSortDropdown(IWebElement container)
        {
            this.Container = container;
        }    

        /// <summary>
        /// Select option
        /// </summary>
        /// <typeparam name="TPage"> Page Type </typeparam>
        /// <param name="option"> Option to select </param>
        /// <returns> New instance of the page </returns>
        public new TPage SelectOption<TPage>(QuickCheckSortOptions option) where TPage : ICreatablePageObject
        {
            this.ExpandIfNotExpanded();
            this.SelectOptionFromExpandedDropdown(option);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForAnimation();
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        public override QuickCheckSortOptions SelectedOption
        {
            get
            {
                this.ExpandIfNotExpanded();
                string checkedElementText = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Contains("true")).Text;
                return checkedElementText.GetEnumValueByText<QuickCheckSortOptions>(
                    "",
                    @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");
            }
        }

        /// <summary>
        /// The options from expanded dropdown.
        /// </summary>
        protected override IEnumerable<QuickCheckSortOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                elem => elem.Text.GetEnumValueByText<QuickCheckSortOptions>(
                    "",
                    @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck")).ToList();

        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.Container;

        /// <summary>
        /// Sort Quotations Map
        /// </summary>
        private EnumPropertyMapper<QuickCheckSortOptions, WebElementInfo> SortMap =>
            EnumPropertyModelCache.GetMap<QuickCheckSortOptions, WebElementInfo>("", @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

        /// <summary>
        /// Gets the container.
        /// </summary>
        private IWebElement Container { get; }

        /// <summary>
        /// Dropdown attribute
        /// </summary>
        public string GetAttribute(string attribute) => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).GetAttribute(attribute);

        /// <summary>
        /// The is selected.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsSelected(QuickCheckSortOptions option)
        {          
            return DriverExtensions.GetElement(this.Container, By.XPath(this.SortMap[option].LocatorString))
                                   .GetAttribute("aria-checked").Equals("true");
        }

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(QuickCheckSortOptions option)
        {
            DriverExtensions.WaitForElement(By.XPath(this.SortMap[option].LocatorString)).CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded() => IsSortDropdownStateAsExpected("aria-expanded", "true", false);

        /// <summary>
        /// custom click on dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).CustomClick();  

        /// <summary>
        /// Verify Sort quotations dropdown state
        /// </summary>
        /// <returns> True if Sort quotations dropdown is as expected, false otherwise </returns>
        private bool IsSortDropdownStateAsExpected(string attribute, string dropDownState, bool state) =>
            DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator)
            .GetAttribute(attribute)?.Contains(dropDownState, StringComparison.InvariantCultureIgnoreCase) ?? state;
    }
}