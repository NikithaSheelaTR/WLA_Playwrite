namespace Framework.Common.UI.Products.WestlawEdge.Components.Delivery
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Number of items component
    /// Select radiobuttons
    /// </summary>
    public class NumberOfItemsRadiobuttonComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.ClassName("co_deliveryNumber");

        private EnumPropertyMapper<NumberOfItems, WebElementInfo> numberOfItemsMap;

        /// <summary>
        /// Dropdown locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the NumberOfItems enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<NumberOfItems, WebElementInfo> NumberOfItemsMap =>
            this.numberOfItemsMap = this.numberOfItemsMap
                                    ?? EnumPropertyModelCache.GetMap<NumberOfItems, WebElementInfo>(
                                        string.Empty,
                                        @"Resources/EnumPropertyMaps/WestlawEdge/Delivery");

        /// <summary>
        /// Verify the Number of item option is displayed
        /// </summary>
        /// <param name="option"> The option to check for </param>
        /// <returns> True if the option is displayed, false otherwise </returns>
        public bool IsOptionDisplayed(NumberOfItems option) =>
            DriverExtensions.IsDisplayed(By.Id(this.NumberOfItemsMap[option].Id));

        /// <summary>
        /// Set Number of Items option </summary>
        /// <param name="option">Number of items option</param>
        /// <returns> The <see cref="TheBasicsTabComponent"/>.</returns>
        public TheBasicsTabComponent SelectRadiobutton(NumberOfItems option)
        {
            DriverExtensions.GetElement(By.Id(this.NumberOfItemsMap[option].Id)).Click();
            return new TheBasicsTabComponent();
        }

        /// <summary>
        /// Check if option is selected
        /// </summary>
        /// <param name="option">option to be checked</param>
        /// <returns>rue if the option is selected</returns>
        public bool IsRadioButtonSelected(NumberOfItems option) =>
            DriverExtensions.IsRadioButtonSelected(By.Id(this.NumberOfItemsMap[option].Id));
    }
}