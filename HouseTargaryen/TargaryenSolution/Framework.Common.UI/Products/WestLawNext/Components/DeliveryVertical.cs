namespace Framework.Common.UI.Products.WestLawNext.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents the right delivery options
    /// </summary>
    public class DeliveryVertical : BaseModuleRegressionComponent
    {
        private static readonly By DeliveryVerticalBarLocator = By.Id("co_docToolbarVerticalMenuRight");

        private EnumPropertyMapper<DeliveryMethod, WebElementInfo> deliveryMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryVertical"/> class. 
        /// Creates the RightExecutiveSummarySection
        /// </summary>
        public DeliveryVertical()
        {
            DriverExtensions.WaitForElement(DeliveryVerticalBarLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => DeliveryVerticalBarLocator;

        /// <summary>
        /// Gets the DeliveryMethod enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<DeliveryMethod, WebElementInfo> DeliveryMap
            => this.deliveryMap = this.deliveryMap ?? EnumPropertyModelCache.GetMap<DeliveryMethod, WebElementInfo>("Vertical");

        /// <summary>
        /// The click delivery.
        /// </summary>
        /// <param name="deliveryType">The delivery type.</param>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickDelivery<T>(DeliveryMethod deliveryType) where T : BaseDeliveryDialog
        {
            DriverExtensions.WaitForElementDisplayed(By.Id(this.DeliveryMap[deliveryType].Id)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}