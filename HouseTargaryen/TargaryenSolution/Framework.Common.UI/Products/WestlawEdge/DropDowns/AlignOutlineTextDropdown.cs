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
    /// Align Outline's text content menu dropdown
    /// </summary>
    public class AlignOutlineTextDropdown : BaseModuleRegressionCustomDropdown<OutlinesAlignOrderOptions>
    {
        private static readonly By DropdownOptionLocator = By.CssSelector("li[role='menuitemradio'][aria-setsize='3']");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]");
        private static readonly By DropdownExpanderLocator = By.XPath(".//button[@id='OutlineBuilderAlignmentButton']/span[contains(@class,'downMenu')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="AlignOutlineTextDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public AlignOutlineTextDropdown(IWebElement container) => this.container = container;

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override OutlinesAlignOrderOptions SelectedOption =>
            DriverExtensions.GetElement(this.container, SelectedOptionLocator).Text
                            .GetEnumValueByText<OutlinesAlignOrderOptions>();

        /// <summary>
        /// Verify that action menu option is selected
        /// </summary>
        /// <param name="option">Outlines align menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(OutlinesAlignOrderOptions option) =>
            DriverExtensions.GetElement(this.container,
                By.XPath(this.AlignMenuMap[option].LocatorString)).GetAttribute("class").Contains("item_selected");

        /// <summary>
        /// Actions alignment map
        /// </summary>
        protected EnumPropertyMapper<OutlinesAlignOrderOptions, WebElementInfo> AlignMenuMap
            => EnumPropertyModelCache.GetMap<OutlinesAlignOrderOptions, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<OutlinesAlignOrderOptions> OptionsFromExpandedDropdown =>
             DriverExtensions.GetElements(this.container, DropdownOptionLocator)
            .Select(x => this.AlignMenuMap.First(y => y.Value.Text.Equals(x.Text)).Key).ToList();

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Outlines sort menu option</param>
        protected override void SelectOptionFromExpandedDropdown(OutlinesAlignOrderOptions option) =>
            DriverExtensions.WaitForElement(this.container, By.XPath(this.AlignMenuMap[option].LocatorString)).CustomClick();

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded() => DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator)
            .GetAttribute("aria-expanded")?.Contains("true", StringComparison.InvariantCultureIgnoreCase) ?? false;

        /// <summary>
        /// Click Dropdown Arrow
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.CustomClick();

        /// <summary>
        /// The dropdown.
        /// </summary>
        protected override IWebElement Dropdown => this.container;

        private readonly IWebElement container;
    }
}
