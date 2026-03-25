namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Copy menu Dropdown inside Toolbar
    /// </summary>
    public class CopyMenuDropdown : BaseModuleRegressionCustomDropdown<CopyMenuOption>
    {
        private static readonly By DropdownLocator = By.XPath("//*[contains(@id,'CopyMenuWidget')]//*[contains(@id,'dropdown')]");
        private static readonly By DropdownOptionLocator = By.XPath(".//li[//span[contains(.,'Copy')]]");
        private static readonly By DropdownArrowLocator = By.XPath("//button[@aria-label = 'Copy menu'] | //button[@aria-label = 'Copy Menu'] ");
        private static readonly By DropdownButtonLocator = By.XPath(".//li[contains(@class,'a11yDropdown-item_selected')]//span[@class='a11yDropdown-itemText']");

        private EnumPropertyMapper<CopyMenuOption, WebElementInfo> copyMenuMap;

        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override CopyMenuOption SelectedOption
        {
            get
            {
                this.ExpandIfNotExpanded();
                return DriverExtensions.GetElement(this.Dropdown, DropdownButtonLocator).Text
                                       .GetEnumValueByText<CopyMenuOption>();
            }
        }

        /// <summary>
        /// Copy menu map
        /// </summary>
        protected EnumPropertyMapper<CopyMenuOption, WebElementInfo> CopyMenuMap =>
            this.copyMenuMap = this.copyMenuMap
                               ?? EnumPropertyModelCache.GetMap<CopyMenuOption, WebElementInfo>();

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<CopyMenuOption> OptionsFromExpandedDropdown
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<CopyMenuOption>()).ToList();

        /// <summary>
        /// Dropdown
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(DropdownLocator);

        /// <summary>
        /// Verify that copy menu option is selected
        /// </summary>
        /// <param name="option">Copy menu option (Copy link, Copy Citation)</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(CopyMenuOption option) => DriverExtensions
            .GetElement(this.Dropdown, By.XPath(this.CopyMenuMap[option].LocatorString)).GetAttribute("aria-selected")
            .Equals("true");

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option">Copy menu option (Copy link, Copy Citation)</param>
        protected override void SelectOptionFromExpandedDropdown(CopyMenuOption option) => DriverExtensions.Click(
            DriverExtensions.WaitForElement(this.Dropdown, By.XPath(this.CopyMenuMap[option].LocatorString)));

        /// <summary>
        /// ClickDropdownArrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.GetElement(DropdownArrowLocator)
                            .Click();
    }
}