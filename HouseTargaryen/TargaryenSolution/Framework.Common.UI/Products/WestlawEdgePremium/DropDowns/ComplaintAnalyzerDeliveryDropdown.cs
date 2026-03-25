namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyzer Delivery dropdown
    /// </summary>
    public class ComplaintAnalyzerDeliveryDropdown : BaseModuleRegressionCustomDropdown<DeliveryOptions>
    {
        private static readonly By DropdownLocator = By.XPath(".//saf-button-v3[contains(@aria-controls,'delivery-button')]");
        private static readonly By DropdownOptionLocator = By.XPath("//saf-menu-v3[@data-testid='menu-item-download-file']//saf-text[contains(@class, 'delphi-document-file') and not(@id)]");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrayDropdown"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public ComplaintAnalyzerDeliveryDropdown(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override DeliveryOptions SelectedOption { get; }

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<DeliveryOptions, WebElementInfo> ActionsMenuMap
            => EnumPropertyModelCache.GetMap<DeliveryOptions, WebElementInfo>(
                                         string.Empty,
                                         @"Resources\EnumPropertyMaps\WestlawEdgePremium\AALP\ComplaintAnalyzer");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<DeliveryOptions> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<DeliveryOptions>(string.Empty,
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
        public override bool IsSelected(DeliveryOptions option) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(this.ActionsMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Action menu option </param>
        protected override void SelectOptionFromExpandedDropdown(DeliveryOptions option)
        {
            DriverExtensions.GetElement(By.XPath(this.ActionsMenuMap[option].LocatorString)).WaitForElementEnabled();
            DriverExtensions.WaitForElement(By.XPath(this.ActionsMenuMap[option].LocatorString)).ClickUsingMouse();
        }

        /// <summary>
        /// The is dropdown expanded.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected override bool IsDropdownExpanded() => base.IsDropdownExpanded();

        /// <summary>
        /// ClickDropdown
        /// </summary>
        protected override void ClickDropdownArrow() => this.Dropdown.Click();

        /// <summary>
        /// Get all options 
        /// </summary>
        /// <returns> Options list </returns>
        public new IEnumerable<DeliveryOptions> Options
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
