namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Toolbar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
    
    /// <inheritdoc />
    /// <summary>
    /// The doc analyzer detail level dropdown.
    /// </summary>
    public sealed class QuickCheckDetailLevelDropdown : BaseModuleRegressionCustomDropdown<DetailLevel>
    {
        private static readonly By DropdownOptionLocator = By.XPath("//li[./span[contains(.,'detail') or contains(.,'Detail')]]");

        private static readonly By DropdownExpanderLocator = By.XPath("./button");

        /// <summary>
        /// Initializes a new instance of the <see cref="QuickCheckDetailLevelDropdown"/> class.
        /// </summary>
        /// <param name="container">
        /// The container.
        /// </param>
        public QuickCheckDetailLevelDropdown(IWebElement container)
        {
            this.Container = container;
        }

        /// <summary>
        /// Gets the selected option.
        /// </summary>
        public override DetailLevel SelectedOption
        {
            get
            {
                IWebElement checkedElement = DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).FirstOrDefault(
                    elem => elem.GetAttribute("aria-checked").Contains("true"));
                string checkedElementText = checkedElement.Text;

                return checkedElementText.GetEnumValueByText<DetailLevel>();
            }
        }

        /// <summary>
        /// The options from expanded dropdown.
        /// </summary>
        protected override IEnumerable<DetailLevel> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                            .Select(elem => elem.Text.GetEnumValueByText<DetailLevel>()).ToList();

        /// <inheritdoc />
        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.Container;

        /// <summary>
        /// Detail Level Map
        /// </summary>
        private EnumPropertyMapper<DetailLevel, WebElementInfo> DetailLevelMap =>
           EnumPropertyModelCache.GetMap<DetailLevel, WebElementInfo>("", @"Resources\EnumPropertyMaps\WestlawEdge\QuickCheck");

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
        public override bool IsSelected(DetailLevel option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.GetElement(this.Container, By.XPath(this.DetailLevelMap[option].LocatorString))
                                   .GetAttribute("aria-checked").Equals("true");
        }

        /// <summary>
        /// The select option from expanded dropdown.
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        protected override void SelectOptionFromExpandedDropdown(DetailLevel option)
        {
            var dropdownOption = DriverExtensions.WaitForElement(By.XPath(this.DetailLevelMap[option].LocatorString));
            dropdownOption.CustomClick();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded()
        {
            string dropdownArea = DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).GetAttribute("aria-expanded");
            return dropdownArea?.Contains("true", StringComparison.InvariantCultureIgnoreCase) ?? false;
        }

        /// <summary>
        /// custom click on dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).CustomClick();
    }
}