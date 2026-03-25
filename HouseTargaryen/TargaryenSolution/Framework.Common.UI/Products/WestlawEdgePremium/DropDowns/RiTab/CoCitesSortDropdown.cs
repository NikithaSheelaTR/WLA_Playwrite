namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.RiTab
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.Toolbars;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Sort dropdown on co-cites Ri tab
    /// </summary>
    public class CoCitesSortDropdown : BaseModuleRegressionCustomDropdown<CoCitesSortOptions>
    {
        private static readonly By DropdownLocator = By.XPath("//div[contains(@id, 'SortSelectorContainer')]");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");
        private static readonly By DropdownArrowLocator = By.XPath(".//span[@class = 'icon25 icon_downMenu-gray']");

        /// <summary>
        /// Return Selected Option
        /// </summary>
        public override CoCitesSortOptions SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown.FindElement( SelectedOptionLocator)).Text
                            .GetEnumValueByText<CoCitesSortOptions>(
                                string.Empty,
                                @"Resources/EnumPropertyMaps/WestlawEdgePremium/Toolbars");

        /// <summary>
        /// Sort dropdown map
        /// </summary>
        protected EnumPropertyMapper<CoCitesSortOptions, WebElementInfo> SortDropdownMap
            => EnumPropertyModelCache.GetMap<CoCitesSortOptions, WebElementInfo>(
                string.Empty,
                @"Resources/EnumPropertyMaps/WestlawEdgePremium/Toolbars");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<CoCitesSortOptions> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                elem => elem.Text.GetEnumValueByText<CoCitesSortOptions>(
                    string.Empty,
                    @"Resources/EnumPropertyMaps/WestlawEdgePremium/Toolbars")).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Verify that sort option is selected
        /// </summary>
        /// <param name="option">sort option</param>
        /// <returns>true if selected</returns>
        public override bool IsSelected(CoCitesSortOptions option)
            => DriverExtensions.GetElement(this.Dropdown, By.XPath(this.SortDropdownMap[option].LocatorString)).GetAttribute("class")
                               .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">sort option</param>
        protected override void SelectOptionFromExpandedDropdown(CoCitesSortOptions option) =>
            DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.SortDropdownMap[option].LocatorString)).Click();

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() =>
            DriverExtensions.GetElement(this.Dropdown, DropdownArrowLocator).Click();
    }
}