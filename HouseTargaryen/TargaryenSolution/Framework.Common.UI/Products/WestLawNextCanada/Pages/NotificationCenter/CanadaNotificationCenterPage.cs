namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.NotificationCenter
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.NotificationCenter;

    using OpenQA.Selenium;

    /// <summary>
    /// The notification center page.
    /// </summary>
    public class CanadaNotificationCenterPage : NotificationCenterPage
    {
        private static readonly By NotificationCenterHeaderLocator = By.ClassName("NotificationTabPanelHeader-heading");

        /// <summary>
        /// Notification Center Facet component
        /// </summary>
        public CanadaNotificationCenterFacetComponent NotificationCenterFacetComponent = new CanadaNotificationCenterFacetComponent();

        /// <summary>
        /// Notification Center header label
        /// </summary>
        public ILabel NotificationCenterHeader = new Label(NotificationCenterHeaderLocator);
    }
}
