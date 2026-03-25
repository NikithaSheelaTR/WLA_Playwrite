namespace Framework.Common.UI.Products.WestlawEdge.DropDowns
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.DropDowns;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;

    /// <summary>
    /// Delivery dropdown
    /// </summary>
    public class CompareTextDeliveryDropdown : BaseModuleRegressionCustomDropdown<DeliveryMethod>
    {
        private static readonly By DropdownLocator = By.XPath(".//div[@class = 'co_snippetCompare_delivery']/div[contains(@class, 'co_dropDownMenu')] | .//div[@class = 'co_snippetCompare_delivery']");
        private static readonly By DropdownArrowLocator = By.XPath(".//span[@class ='icon25 icon_down_blue_arrow'] | .//span[@class ='icon25 icon_downMenu-gray'] | .//button[@type='button']");
        private static readonly By SelectedOptionLocator = By.XPath(".//a[(@aria-selected ='true')]");
        private static readonly By DropdownOptionLocator = By.XPath(".//li");
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="componentLocator"></param>
        public CompareTextDeliveryDropdown(By componentLocator) =>
            this.Dropdown = DriverExtensions.GetElement(componentLocator, DropdownLocator);
        
        /// <summary>
        /// Return Selected Option
        /// </summary>
        /// <returns> Selected option</returns>
        public override DeliveryMethod SelectedOption =>
            DriverExtensions.GetElement(this.Dropdown, SelectedOptionLocator).Text
                                .GetEnumValueByText<DeliveryMethod>();

        /// <summary>
        /// Delivery menu map
        /// </summary>
        protected EnumPropertyMapper<DeliveryMethod, WebElementInfo> DeliveryMap
            => EnumPropertyModelCache.GetMap<DeliveryMethod, WebElementInfo>();

        /// <summary>
        /// Gets Options
        /// </summary>
        protected override IEnumerable<DeliveryMethod> OptionsFromExpandedDropdown 
            => DriverExtensions.GetElements(this.Dropdown, DropdownOptionLocator)
                               .Select(elem => elem.Text.GetEnumValueByText<DeliveryMethod>()).ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown { get; }

        /// <summary>
        /// Verify that delivery option is selected
        /// </summary>
        /// <param name="option">Delivery option</param>
        /// <returns>True- if selected</returns>
        public override bool IsSelected(DeliveryMethod option) =>
            DriverExtensions
            .GetElement(this.Dropdown, By.XPath(this.DeliveryMap[option].LocatorString)).GetAttribute("aria-selected")
            .Equals("true");

        /// <summary>
        /// Select dropdown option
        /// </summary>
        /// <param name="option">Delivery menu option </param>
        protected override void SelectOptionFromExpandedDropdown(DeliveryMethod option) =>
            DriverExtensions.Click(this.Dropdown, By.XPath(this.DeliveryMap[option].LocatorString));

        /// <summary>
        /// Click on the dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() =>
            DriverExtensions.GetElement(Dropdown, DropdownArrowLocator).Click();
    }
}