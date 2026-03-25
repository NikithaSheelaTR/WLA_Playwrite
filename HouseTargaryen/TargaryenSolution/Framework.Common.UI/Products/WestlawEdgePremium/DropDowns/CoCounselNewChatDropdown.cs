namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Execution;
    using OpenQA.Selenium;

    /// <summary>
    /// CoCounsel New Chat Dropdown
    /// </summary>
    public class CoCounselNewChatDropdown : BaseModuleRegressionCustomDropdown<NewChatOptions>
    {
        private static readonly By DropdownLocator = By.XPath(".//button[@data-testid='new-folder-or-chat-button']");
        private static readonly By DropdownOptionLocator = By.XPath(".//li[@data-testid='new-chat-new-menu-item']//div");

        private readonly By componentLocator;

        /// <summary>
        /// Initializes a new instance of the <see cref="NewChatOptions"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public CoCounselNewChatDropdown(By componentLocator)
        {
            this.componentLocator = componentLocator;
        }

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override NewChatOptions SelectedOption { get; }

        /// <summary>
        /// Actions menu map
        /// </summary>
        protected EnumPropertyMapper<NewChatOptions, WebElementInfo> ActionsMenuMap
            => EnumPropertyModelCache.GetMap<NewChatOptions, WebElementInfo>(
                                         string.Empty,
                                         @"Resources/EnumPropertyMaps/WestlawEdgePremium/CoCounsel");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<NewChatOptions> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<NewChatOptions>(string.Empty,
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
        public override bool IsSelected(NewChatOptions option) => DriverExtensions
            .GetElement(this.ComponentLocator, By.XPath(this.ActionsMenuMap[option].LocatorString)).GetAttribute("class")
            .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Action menu option </param>
        protected override void SelectOptionFromExpandedDropdown(NewChatOptions option)
        {
            DriverExtensions.GetElement(By.XPath(this.ActionsMenuMap[option].LocatorString)).WaitForElementEnabled();
            DriverExtensions.WaitForElement(By.XPath(this.ActionsMenuMap[option].LocatorString)).ClickUsingMouse();
        }

        /// <summary>
        /// Get all options 
        /// </summary>
        /// <returns> Options list </returns>
        public new IEnumerable<NewChatOptions> Options
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
