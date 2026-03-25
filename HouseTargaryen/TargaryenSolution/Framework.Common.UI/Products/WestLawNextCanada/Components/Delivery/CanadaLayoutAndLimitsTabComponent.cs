namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Delivery
{
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNextCanada.Enums.Delivery;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Canada Layout And Limits Tab Component
    /// </summary>
    public class CanadaLayoutAndLimitsTabComponent : LayoutAndLimitsTabComponent
    {
        private EnumPropertyMapper<LayoutAndLimitsDeliveryFullText, WebElementInfo> deliveryFullTextMap;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<LayoutAndLimitsDeliveryFullText, WebElementInfo> DeliveryFullTextMap =>
            this.deliveryFullTextMap = this.deliveryFullTextMap
                                       ?? EnumPropertyModelCache
                                           .GetMap<LayoutAndLimitsDeliveryFullText, WebElementInfo>(
                                               string.Empty,
                                               @"Resources/EnumPropertyMaps/WestlawNextCanada");

        /// <summary>
        /// Select Include option on the Layout and Limits tab
        /// </summary>
        /// <param name="expectedTabOption"> Include option on the Layout and Limits tab </param>
        public void SetDeliveryFullTextSectionOption(LayoutAndLimitsDeliveryFullText expectedTabOption) =>
            DriverExtensions.WaitForElementDisplayed(By.Id(DeliveryFullTextMap[expectedTabOption].Id)).Click();
    }
}