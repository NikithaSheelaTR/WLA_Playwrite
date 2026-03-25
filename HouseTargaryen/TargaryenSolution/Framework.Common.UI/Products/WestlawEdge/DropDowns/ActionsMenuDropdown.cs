namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Actions menu dropdown
    /// </summary>
    public class ActionsMenuDropdown : BaseModuleRegressionCustomDropdown<ActionsMenuOption>
    {
        private static readonly By DropdownLocator = By.XPath(".//div[contains(@*,'actionsMenu')]");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");
        private static readonly By DropdownExpanderLocator = By.XPath("./button");
        private readonly IWebElement container;

        /// <summary>
        /// Initializes a new instance of the <see cref="ActionsMenuDropdown"/> class.
        /// </summary>
        /// <param name="container">Container element</param>
        public ActionsMenuDropdown(IWebElement container) => this.container = container;
        
        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override ActionsMenuOption SelectedOption =>
             DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).Text
                                .GetEnumValueByText<ActionsMenuOption>();

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<ActionsMenuOption, WebElementInfo> ActionsMenuMap
            => EnumPropertyModelCache.GetMap<ActionsMenuOption, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Folders");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<ActionsMenuOption> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<ActionsMenuOption>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdge/Folders")).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.container, DropdownLocator);

        /// <summary>
        /// Verify that action menu option is selected
        /// </summary>
        /// <param name="option">Actions menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(ActionsMenuOption option) => DriverExtensions
            .GetElement(this.Dropdown, By.XPath(this.ActionsMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Action menu option </param>
        protected override void SelectOptionFromExpandedDropdown(ActionsMenuOption option) =>
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
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow()
        {
            DriverExtensions.GetElement(this.Dropdown, DropdownExpanderLocator).Click();
        }
    }
}
