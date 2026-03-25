namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Products.Shared.Components.Alerts;

    /// <summary>
    /// Create Report Page
    /// </summary>
    public class CreateReportPage : CommomCreateAlertPage
    {
        /// <summary>
        /// Select Content
        /// </summary>
        public SelectContentComponent SelectContent { get; private set; } = new SelectContentComponent();

        /// <summary>
        /// Refine Report Component
        /// </summary>
        public RefineReportComponent RefineReport { get; private set; } = new RefineReportComponent();
    }
}
