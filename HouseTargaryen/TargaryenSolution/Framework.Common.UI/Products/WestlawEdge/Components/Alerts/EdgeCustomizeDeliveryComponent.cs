namespace Framework.Common.UI.Products.WestlawEdge.Components.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Customize Delivery Section for Alert
    /// </summary>
    public class EdgeCustomizeDeliveryComponent : CustomizeDeliveryComponent
    {
        private static readonly By NotificationsCheckboxLocator = By.Id("enableNotificationsDelivery");

        /// <summary>
        /// Notifications checkbox
        /// </summary>
        public ICheckBox NotificationsCheckbox => new CheckBox(NotificationsCheckboxLocator);
    }
}