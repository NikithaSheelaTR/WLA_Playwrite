namespace Framework.Common.UI.Products.TaxnetPro.Components.Delivery
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Delivery From Component in Basics tab
    /// </summary>
    public class DeliveryFromComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.Id("co_delivery_deliverFromContainer");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<DeliveryFrom, WebElementInfo> deliveryFromMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<DeliveryFrom, WebElementInfo> DeliveryFromMapper =>
            this.deliveryFromMap = this.deliveryFromMap
                                   ?? EnumPropertyModelCache
                                       .GetMap<DeliveryFrom, WebElementInfo>(
                                           string.Empty,
                                           @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Select Delivery From : Current Document/Result List
        /// </summary>
        /// <param name="option">Delivery from Option</param>
        /// <returns>The <see cref="TaxnetProDeliveryBasicTabComponent"/>.</returns>
        public TaxnetProDeliveryBasicTabComponent SelectOption(DeliveryFrom option)
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.DeliveryFromMapper[option].Id)).Click();
            return new TaxnetProDeliveryBasicTabComponent();
        }
    }
}
