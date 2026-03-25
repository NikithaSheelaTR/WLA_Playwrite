namespace Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.RiTab
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Per page dropdown on co-cites pages
    /// </summary>
    public class CoCitesPerPageDropdown : BaseModuleRegressionCustomDropdown<ResultItemsPerPage>
    {
        private static readonly By DropdownLocator = By.XPath(".//div[@class = 'ToolbarSection-item'][.//ul[@aria-label= 'Items per page']]");
        private static readonly By SelectedOptionLocator = By.XPath(".//li[contains(@class,'item_selected')]/span");
        private static readonly By DropdownOptionLocator = By.XPath(".//li/span");
        private static readonly By DropdownArrowLocator = By.XPath(".//span[@class = 'icon25 icon_downMenu-gray']");

        /// <summary>
        /// Initializes a new instance of the <see cref="CoCitesPerPageDropdown"/> class.
        /// </summary>
        /// <param name="container">FooterToolbarComponent</param>
        public CoCitesPerPageDropdown(By container) 
            => this.Dropdown = DriverExtensions.GetElement(container, DropdownLocator);

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override ResultItemsPerPage SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).GetAttribute("innerText")
                            .GetEnumValueByText<ResultItemsPerPage>(
                                "CoCitesTab",
                                @"Resources/EnumPropertyMaps/WestlawEdgePremium/Footer");

        /// <summary>
        /// Items per page dropdown map
        /// </summary>
        protected EnumPropertyMapper<ResultItemsPerPage, WebElementInfo> ItemsPerPageDropdownMap
            => EnumPropertyModelCache.GetMap<ResultItemsPerPage, WebElementInfo>(
                "CoCitesTab",
                @"Resources/EnumPropertyMaps/WestlawEdgePremium/Footer");

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<ResultItemsPerPage> OptionsFromExpandedDropdown =>
            DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator).Select(
                elem => elem.Text.GetEnumValueByText<ResultItemsPerPage>(
                    "CoCitesTab",
                    @"Resources/EnumPropertyMaps/WestlawEdgePremium/Footer")).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown { get; }

        /// <summary>
        /// Verify that result items per page option is selected
        /// </summary>
        /// <param name="option">result items per page option</param>
        /// <returns>true if selected</returns>
        public override bool IsSelected(ResultItemsPerPage option)
            => DriverExtensions.GetElement(this.Dropdown, By.XPath(this.ItemsPerPageDropdownMap[option].LocatorString)).GetAttribute("class")
                               .Contains("item_selected");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">result items per page option</param>
        protected override void SelectOptionFromExpandedDropdown(ResultItemsPerPage option)
            => DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.ItemsPerPageDropdownMap[option].LocatorString)).Click();
        
        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() =>
            DriverExtensions.GetElement(this.Dropdown, DropdownArrowLocator).CustomClick();
    }
}