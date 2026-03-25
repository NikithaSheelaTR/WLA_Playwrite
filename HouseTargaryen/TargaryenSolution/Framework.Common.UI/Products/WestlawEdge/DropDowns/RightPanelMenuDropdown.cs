namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// This is Right panel menu dropdown in the toolbar
    /// </summary>
    public class RightPanelMenuDropdown : BaseModuleRegressionCustomDropdown<RightPanelOptions>
    {
        private static readonly By DropdownLocator = By.XPath(".//div[@class='a11yDropdown'] | ./preceding-sibling::*[@class='DocumentPanel-fixed']//div[@class='a11yDropdown']");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");
        private static readonly By DropdownExpanderLocator = By.XPath(".//button[contains(@class, 'a11yDropdown-button')]");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RightPanelMenuDropdown"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RightPanelMenuDropdown(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override RightPanelOptions SelectedOption =>
             DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).GetHiddenText()
                                .GetEnumValueByText<RightPanelOptions>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<RightPanelOptions, WebElementInfo> ActionsMenuMap
            => EnumPropertyModelCache.GetMap<RightPanelOptions, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Document");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<RightPanelOptions> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<RightPanelOptions>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Document")).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.ComponentLocator, DropdownLocator);

        /// <summary>
        /// Verify that action menu option is selected
        /// </summary>
        /// <param name="option">Actions menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(RightPanelOptions option) => DriverExtensions
            .GetElement(this.Dropdown, By.XPath(this.ActionsMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Action menu option </param>
        protected override void SelectOptionFromExpandedDropdown(RightPanelOptions option) =>
            DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.ActionsMenuMap[option].LocatorString)).CustomClick();

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
        /// ClickDropdown
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => this.componentLocator;
    }
}