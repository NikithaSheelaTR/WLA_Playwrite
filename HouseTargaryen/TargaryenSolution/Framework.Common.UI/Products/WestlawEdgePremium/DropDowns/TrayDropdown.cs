namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;

    /// <inheritdoc />
    /// <summary>
    /// Precision Filters Sort dropdown
    /// </summary>
    public class TrayDropdown : BaseModuleRegressionCustomDropdown<TrayOptions>
    {
        private static readonly By DropdownLocator = By.XPath(".//saf-button[@id='button-menu-trigger']");
        private static readonly By DropdownAreaLocator = By.XPath("./following-sibling::*//saf-menu");
        private static readonly By DropdownOptionLocator = By.XPath(".//saf-menu-item[contains(@class, 'coCounselMenuItem')]//span");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayDropdown"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public TrayDropdown(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override TrayOptions SelectedOption { get; }

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<TrayOptions, WebElementInfo> ActionsMenuMap
            => EnumPropertyModelCache.GetMap<TrayOptions, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdgePremium/CoCounsel");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<TrayOptions> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.ComponentLocator, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<TrayOptions>(string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdgePremium/CoCounsel")).Distinct().ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.ComponentLocator, DropdownLocator);

        /// <summary>
        /// Verify that action menu option is selected
        /// </summary>
        /// <param name="option">Actions menu option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(TrayOptions option) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(this.ActionsMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Action menu option </param>
        protected override void SelectOptionFromExpandedDropdown(TrayOptions option) 
        {
            SafeMethodExecutor.WaitUntil(() => this.IsDropdownExpanded());
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), By.XPath(this.ActionsMenuMap[option].LocatorString)).CustomClick();
            SafeMethodExecutor.WaitUntil(() => !this.IsDropdownExpanded());
        }

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded() 
        {
            var isExpanded = DriverExtensions.WaitForElement(this.Dropdown, DropdownAreaLocator).GetAttribute("hidden");
            return isExpanded == null;
        }
        /// <summary>
        /// ClickDropdown
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();

        /// <summary>
        /// Get all options 
        /// </summary>
        /// <returns> Options list </returns>
        public new IEnumerable<TrayOptions> Options
        {
            get
            {
                this.ExpandIfNotExpanded();
                SafeMethodExecutor.WaitUntil(() => this.OptionsFromExpandedDropdown.Count() > 0);
                return this.OptionsFromExpandedDropdown;
            }
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected By ComponentLocator => this.componentLocator;
    }
}