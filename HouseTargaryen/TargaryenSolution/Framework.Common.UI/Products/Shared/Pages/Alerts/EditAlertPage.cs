namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Products.Shared.Components.Alerts;

    /// <summary>
    /// Edit Alert Page
    /// </summary>
    public class EditAlertPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Select Content
        /// </summary>
        public SelectContentComponent SelectContent { get; } = new SelectContentComponent();

        /// <summary>
        /// Enter Search Term
        /// </summary>
        public EnterSearchTermsComponent EnterSearchTerm { get; } = new EnterSearchTermsComponent();

    }
}
