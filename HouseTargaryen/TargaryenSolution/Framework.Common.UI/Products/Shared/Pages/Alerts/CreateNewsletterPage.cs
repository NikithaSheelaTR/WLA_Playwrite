namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Products.Shared.Components.Alerts;

    /// <summary>
    /// Create Newsletter Page
    /// </summary>
    public class CreateNewsletterPage : CommomCreateAlertPage
    {
        /// <summary>
        /// Selects alerts component (when Newsletter creates)
        /// </summary>
        public SelectAlertsComponent SelectAlerts { get; private set; } = new SelectAlertsComponent();
    }

    /// <summary>
    /// Create KeyCiteAlert Page
    /// </summary>
    public class KeyCiteAlert : CommomCreateAlertPage
    {
        /// <summary>
        /// Selects alerts component (when KeyCite creates)
        /// </summary>
        public SelectAlertsComponent SelectAlerts { get; private set; } = new SelectAlertsComponent();
    }
}
