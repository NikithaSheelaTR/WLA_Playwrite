namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Sort Outlines menu dropdown
    /// </summary>
    public class SortOutlinesDropdown : BaseModuleRegressionCustomDropdown<OutlinesSortOrderOptions>
    {
        private static readonly By DropdownOptionLocator = By.XPath(".//li[contains(@id, 'outlinebuilder_sort_panel')]/span/span");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]");
        private static readonly By DropdownExpanderLocator = By.CssSelector("button#coid_outlineBuilder_sort");

        private readonly IWebElement container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SortOutlinesDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public SortOutlinesDropdown(IWebElement container)
        {
            this.container = container;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override OutlinesSortOrderOptions SelectedOption =>
            DriverExtensions.GetElement(this.container, SelectedOptionLocator).Text
                            .GetEnumValueByText<OutlinesSortOrderOptions>();

        /// <summary>
        /// Verify that action menu option is selected
        /// </summary>
        /// <param name="option">Outlines sort menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(OutlinesSortOrderOptions option) =>
            DriverExtensions.GetElement(this.container,
                By.XPath(this.OutlinesMenuMap[option].LocatorString)).GetAttribute("class").Contains("item_selected");

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<OutlinesSortOrderOptions, WebElementInfo> OutlinesMenuMap
            => EnumPropertyModelCache.GetMap<OutlinesSortOrderOptions, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<OutlinesSortOrderOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.container, DropdownOptionLocator).Select(
                elem => elem.Text.GetEnumValueByText<OutlinesSortOrderOptions>(
                    string.Empty, @"Resources/EnumPropertyMaps/WestlawEdge/Folders")).ToList();        

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Outlines sort menu option</param>
        protected override void SelectOptionFromExpandedDropdown(OutlinesSortOrderOptions option) =>
            DriverExtensions.WaitForElement(this.container, By.XPath(this.OutlinesMenuMap[option].LocatorString)).CustomClick();

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator)
            .GetAttribute("aria-expanded")?.Contains("true", StringComparison.InvariantCultureIgnoreCase) ?? false;

        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.container;

        /// <summary>
        /// Click Dropdown Arrow
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.CustomClick();        
    }
}
