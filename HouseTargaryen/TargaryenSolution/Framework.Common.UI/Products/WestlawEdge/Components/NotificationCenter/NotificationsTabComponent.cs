namespace Framework.Common.UI.Products.WestlawEdge.Components.NotificationCenter
{
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.NotificationCenter;

    using OpenQA.Selenium;

    /// <summary>
    /// Notifications Tab Component
    /// </summary>
    public class NotificationsTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.Id("notificationDetailsMainContent");

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => NotificationCenterTabs.Notifications.ToString();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}