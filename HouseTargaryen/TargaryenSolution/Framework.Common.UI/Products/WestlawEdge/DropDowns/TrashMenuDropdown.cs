namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Trash menu dropdown
    /// </summary>
    public class TrashMenuDropdown : BaseModuleRegressionCustomDropdown<TrashMenuOption>
    {
        private static readonly By DropdownLocator = By.XPath("//div[@class = 'co_trash_enhanced_container a11yDropdown']");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override TrashMenuOption SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).Text
                                .GetEnumValueByText<TrashMenuOption>();

        /// <summary>
        /// Trash menu map
        /// </summary>
        protected EnumPropertyMapper<TrashMenuOption, WebElementInfo> TrashMenuMap
            => EnumPropertyModelCache.GetMap<TrashMenuOption, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<TrashMenuOption> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<TrashMenuOption>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Folders")).ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.WaitForElement(DropdownLocator);

        /// <summary>
        /// Verify that trash menu option is selected
        /// </summary>
        /// <param name="option">Trash menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(TrashMenuOption option) =>
            DriverExtensions.GetElement(this.Dropdown, By.XPath(this.TrashMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Verify that trash menu option is enabled
        /// </summary>
        /// <param name="option">Trash menu option</param>
        /// <returns>True- if enabled</returns>
        public override bool IsEnabled(TrashMenuOption option)
        {
            this.ExpandIfNotExpanded();
            return DriverExtensions.GetElement(this.Dropdown, By.XPath(this.TrashMenuMap[option].LocatorString)).GetAttribute("aria-disabled")
            .Equals("false");
        }

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Trash menu option</param>
        protected override void SelectOptionFromExpandedDropdown(TrashMenuOption option) =>
            DriverExtensions.Click(this.Dropdown, By.XPath(this.TrashMenuMap[option].LocatorString));
    }
}