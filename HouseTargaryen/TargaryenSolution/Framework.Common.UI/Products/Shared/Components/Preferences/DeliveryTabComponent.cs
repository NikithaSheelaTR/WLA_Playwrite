namespace Framework.Common.UI.Products.Shared.Components.Preferences
{
    using System.Collections.Generic;

    using Framework.Common.UI.Products.Shared.Enums.Preferences;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Delivery Tab Component
    /// </summary>
    public class DeliveryTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("coid_userSettingsTabPanel6");

        private EnumPropertyMapper<DeliveryTab, WebElementInfo> deliveryTabMap;

        /// <summary>
        /// The tab name.
        /// </summary>
        protected override string TabName => "Delivery";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the DeliveryTab enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<DeliveryTab, WebElementInfo> DeliveryTabMap
            => this.deliveryTabMap = this.deliveryTabMap ?? EnumPropertyModelCache.GetMap<DeliveryTab, WebElementInfo>();

        /// <summary>
        /// Returns true if the specified element on the delivery tab is displayed
        /// </summary>
        /// <param name="deliveryTabOption">the option to look for</param>
        /// <returns>If the option is visible</returns>
        public bool DeliveryTabOptionExists(DeliveryTab deliveryTabOption)
        {
            By locator = By.Id(this.DeliveryTabMap[deliveryTabOption].Id);
            return DriverExtensions.IsDisplayed(locator);
        }

        /// <summary>
        /// Returns the value of the specified dropdown
        /// </summary>
        /// <param name="tabOption">the dropdown to look for</param>
        /// <returns>the selected dropdown option</returns>
        public string GetDeliveryDropdownSelectedValue(DeliveryTab tabOption)
        {
            By locator = By.Id(this.DeliveryTabMap[tabOption].Id);
            return DriverExtensions.GetSelectedDropdownOptionText(locator);
        }

        /// <summary>
        /// Returns the values of the specified dropdown
        /// </summary>
        /// <param name="deliveryTabOption">the dropdown to look for</param>
        /// <returns>the list of dropdown options</returns>
        public IList<string> GetDeliveryDropdownValues(DeliveryTab deliveryTabOption)
        {
            By locator = By.Id(this.DeliveryTabMap[deliveryTabOption].Id);
            return DriverExtensions.GetDropdownOptionTexts(locator);
        }

        /// <summary>
        /// Returns true if the specified element on the delivery tab is selected (checked for checkboxes)
        /// </summary>
        /// <param name="deliveryTabOption">the option to look for</param>
        /// <returns>true if selected, false otherwise</returns>
        public bool IsDeliveryTabOptionSelected(DeliveryTab deliveryTabOption)
        {
            By locator = By.Id(this.DeliveryTabMap[deliveryTabOption].Id);
            return DriverExtensions.GetElement(locator).Selected;
        }

        /// <summary>
        /// Sets the specified checkbox option on the delivery tab to the specified value.
        /// </summary>
        /// <param name="deliveryTabOption"> the option to look for </param>
        /// <param name="setTo"> What to set the checkbox to. True for checked, false for unchecked. </param>
        /// <returns> The <see cref="DeliveryTabComponent"/>DeliveryTabComponent</returns>
        public DeliveryTabComponent SetDeliveryTabOptionCheckbox(DeliveryTab deliveryTabOption, bool setTo)
        {
            By locator = By.Id(this.DeliveryTabMap[deliveryTabOption].Id);
            DriverExtensions.SetCheckbox(setTo, locator);
            return this;
        }

        /// <summary>
        /// Sets the specified dropdown on the delivery tab to the specified value.
        /// </summary>
        /// <param name="deliveryTabOption"> the option to look for </param>
        /// <param name="option"> What to select from the dropdown. </param>
        /// <returns> The <see cref="DeliveryTabComponent"/>DeliveryTabComponent</returns>
        public DeliveryTabComponent SetDeliveryTabOptionDropdown(DeliveryTab deliveryTabOption, string option)
        {
            By locator = By.Id(this.DeliveryTabMap[deliveryTabOption].Id);
            DriverExtensions.SetDropdown(option, locator);
            return this;
        }
    }
}