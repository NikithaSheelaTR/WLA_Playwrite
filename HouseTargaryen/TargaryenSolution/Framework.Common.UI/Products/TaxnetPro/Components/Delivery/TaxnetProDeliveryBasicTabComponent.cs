namespace Framework.Common.UI.Products.TaxnetPro.Components.Delivery
{
    using Framework.Common.UI.Products.Shared.Components.Delivery.DeliveryTabs;

    /// <summary>
    /// Taxnet Pro3 Delivery Basics tab component
    /// </summary>
    public class TaxnetProDeliveryBasicTabComponent : TheBasicsTabComponent
    {
        /// <summary>
        /// Delivery From Component
        /// </summary>
        public DeliveryFromComponent DeliveryFrom { get; } = new DeliveryFromComponent();
    }
}