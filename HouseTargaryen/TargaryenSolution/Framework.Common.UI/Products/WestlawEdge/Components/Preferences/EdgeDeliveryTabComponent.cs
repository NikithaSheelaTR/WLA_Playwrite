namespace Framework.Common.UI.Products.WestlawEdge.Components.Preferences
{
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Preferences delivery tab component
    /// </summary>
    public class EdgeDeliveryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("panel_Delivery");

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Delivery";

        private EnumPropertyMapper<EdgeDeliveryTab, WebElementInfo> deliveryTabMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the DeliveryTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<EdgeDeliveryTab, WebElementInfo> DeliveryTabMap =>
            this.deliveryTabMap = this.deliveryTabMap ?? EnumPropertyModelCache.GetMap<EdgeDeliveryTab, WebElementInfo>(
                                      string.Empty,
                                      @"Resources/EnumPropertyMaps/WestlawEdge/Preferences");

        /// <summary>
        /// Checks is checkbox displayed
        /// </summary>
        /// <param name="deliveryTabOption">deliveryTabOption</param>
        /// <returns>True if checkbox displayed</returns>
        public bool IsCheckboxSelected(EdgeDeliveryTab deliveryTabOption) =>
            DriverExtensions.WaitForElement(By.XPath(this.DeliveryTabMap[deliveryTabOption].LocatorString)).Selected;

        /// <summary>
        /// Sets the specified checkbox option on the delivery tab to the specified value.
        /// </summary>
        /// <param name="deliveryTabOption"> the option to look for </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns> The <see cref="EdgeDeliveryTabComponent"/>DeliveryTabComponent</returns>
        public EdgeDeliveryTabComponent SetCheckbox(EdgeDeliveryTab deliveryTabOption, bool setTo)
        {
            DriverExtensions.SetCheckbox(By.XPath(this.DeliveryTabMap[deliveryTabOption].LocatorString), setTo);
            return this;
        }

        /// <summary>
        /// Returns the value of the specified dropdown
        /// </summary>
        /// <param name="tabOption">the dropdown to look for</param>
        /// <returns>the selected dropdown option</returns>
        public string GetDeliveryDropdownSelectedValue(EdgeDeliveryTab tabOption)
        {
            By locator = By.XPath(this.DeliveryTabMap[tabOption].LocatorString);
            return DriverExtensions.GetSelectedDropdownOptionText(locator);
        }

        /// <summary>
        /// Sets the specified dropdown on the delivery tab to the specified value.
        /// </summary>
        /// <param name="deliveryTabOption"> the option to look for </param>
        /// <param name="option"> What to select from the dropdown. </param>
        /// <returns> The <see cref="EdgeDeliveryTabComponent"/>DeliveryTabComponent</returns>
        public EdgeDeliveryTabComponent SetDeliveryTabOptionDropdown(EdgeDeliveryTab deliveryTabOption, string option)
        {
            By locator = By.XPath(this.DeliveryTabMap[deliveryTabOption].LocatorString);
            DriverExtensions.SetDropdown(option, locator);
            return this;
        }
    }
}
