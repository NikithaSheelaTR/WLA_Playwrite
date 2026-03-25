namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.CostRecoveryCaps;

    /// <summary>
    /// Cost Recovery Caps Tab Page
    /// </summary>
    public class CostRecoveryCapsPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// WLN Analytics CostRecoveryCaps Header Component
        /// </summary>
        public CostRecoveryCapsTabPanel CostRecoveryCapsTabPanel { get; set; } = new CostRecoveryCapsTabPanel();
    }
}
