namespace Framework.Common.UI.Products.Shared.DropDowns
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;
     
    /// <summary>
    /// Delivery Dropdown inside Toolbar
    /// </summary>
    public class DeliveryDropdown : BaseModuleRegressionCustomDropdown<DeliveryMethod>
    {
        private readonly By containerLocator = By.XPath("//*[contains(@class, 'deliveryWidget') and not(contains(@id, 'actionBtn'))] | //*[@class='co_deliveryWidget'] | //*[contains(@id, 'coid_browseDeliveryWidgetContainer')] | //*[@id='co_docToolbarDeliveryWidget']");
        
        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> deliveryMethodMap;

        private EnumPropertyMapper<DeliveryDropdownLocators, WebElementInfo> dropdownLocatorMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryDropdown"/> class.
        /// </summary>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        public DeliveryDropdown(string additionalInfo = "", string sourceFolder = "")
        {
            this.dropdownLocatorMap =
                EnumPropertyModelCache.GetMap<DeliveryDropdownLocators, WebElementInfo>(additionalInfo, sourceFolder);

                this.deliveryMethodMap =
                    EnumPropertyModelCache.GetMap<DeliveryMethod, WebElementInfo>(additionalInfo, sourceFolder);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryDropdown"/> class.
        /// </summary>
        /// <param name="container"> The container for research</param>
        /// <param name="additionalInfo"> The additional Info. </param>
        /// <param name="sourceFolder"> The source Folder. </param>
        public DeliveryDropdown(By container, string additionalInfo = "", string sourceFolder = "") : this(additionalInfo, sourceFolder)
        {
            this.containerLocator = container;
        }

        /// <summary>
        /// Returns Selected Option for delivery dropdown
        /// </summary>
        public override DeliveryMethod SelectedOption =>
             DriverExtensions.GetElement(this.Dropdown, By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.SelectedOption].LocatorString)).GetAttribute("title") != string.Empty 
            ? DriverExtensions.GetElement(this.Dropdown, By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.SelectedOption].LocatorString)).GetAttribute("title").GetEnumValueByText<DeliveryMethod>()
            : DriverExtensions.GetElement(this.Dropdown, By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.SelectedOption].LocatorString)).GetHiddenText().GetEnumValueByText<DeliveryMethod>();

        /// <summary>
        /// Get Options
        /// </summary>
        protected override IEnumerable<DeliveryMethod> OptionsFromExpandedDropdown =>
            DriverExtensions
                .GetElements(
                    this.Dropdown,
                    By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.Option].LocatorString)).Select(
                    elem => elem.GetAttribute("name").GetEnumValueByText<DeliveryMethod>()).ToList();

        /// <summary>
        /// Dropdown element
        /// </summary>
        protected override IWebElement Dropdown => DriverExtensions.GetElement(this.containerLocator);

        /// <summary>
        /// Verify that Delivery Dropdown Widget is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDeliveryDropdownDisplayed() 
            => DriverExtensions.IsDisplayed(this.containerLocator);

        /// <summary>
        /// Verify that Delivery Dropdown arrow is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDeliveryDropdownArrowDisplayed()
            => DriverExtensions.IsDisplayed(this.Dropdown, By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.DropdownArrow].LocatorString));

        /// <summary>
        /// Verify that Delivery Dropdown method is displayed
        /// </summary>
        /// <param name="deliveryMethod"> Delivery Method to verify </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDropdownOptionDisplayed(DeliveryMethod deliveryMethod)
            => DriverExtensions.IsDisplayed(this.Dropdown, By.XPath(this.deliveryMethodMap[deliveryMethod].LocatorString));

        /// <summary>
        /// Verify that delivery option is selected
        /// </summary>
        /// <param name="deliveryMethod"> Delivery Method to verify </param>
        /// <returns> True if selected, false otherwise </returns>
        public override bool IsSelected(DeliveryMethod deliveryMethod) => this.SelectedOption.Equals(deliveryMethod);

        /// <summary>
        /// Select Dropdown Option
        /// </summary>
        /// <param name="option"></param>
        protected override void SelectOptionFromExpandedDropdown(DeliveryMethod option)
        {
            DriverExtensions.Click(this.Dropdown, By.XPath(this.deliveryMethodMap[option].LocatorString));
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click Dropdown arrow
        /// </summary>
        protected override void ClickDropdownArrow() => DriverExtensions.Click(this.Dropdown, By.XPath(this.dropdownLocatorMap[DeliveryDropdownLocators.DropdownArrow].LocatorString));

        /// <summary>
        /// Expand dropdown
        /// </summary>
        public void DropdownExpandIfNotExpanded()
        {
            if (!this.IsDropdownExpanded())
            {
                this.ClickDropdownArrow();
            }
        }

        /// <summary>
        /// Scroll to top and check if dropdown is already expanded
        /// </summary>
        /// <returns></returns>
        protected override bool IsDropdownExpanded()
        {
            DriverExtensions.ScrollToTop();
            string dropdownClass = this.Dropdown.GetAttribute("class");
            return dropdownClass.Contains("expanded", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}