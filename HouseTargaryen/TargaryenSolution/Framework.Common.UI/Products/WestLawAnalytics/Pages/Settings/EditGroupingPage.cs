namespace Framework.Common.UI.Products.WestLawAnalytics.Pages.Settings
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Components.Settings.Cts;

    /// <summary>
    /// Edit grouping page
    /// </summary>
    public class EditGroupingPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// Edit Grouping Component
        /// </summary>
        public EditGroupingComponent EditGroupingComponent { get; } = new EditGroupingComponent();
    }
}
