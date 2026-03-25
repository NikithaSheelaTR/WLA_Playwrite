namespace Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NotificationCenter;

    using OpenQA.Selenium;

    /// <summary>
    /// Alerts Tab Component
    /// </summary>
    public class AlertsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("co_notification_alerts_tab");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => NotificationCenterTabs.Alerts.ToString();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}