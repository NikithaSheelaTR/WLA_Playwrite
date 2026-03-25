namespace Framework.Common.UI.Products.WestLawNext.Pages.News
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages;

    /// <summary>
    /// News Page
    /// </summary>
    public class NewsPage : BaseModuleRegressionPage
    {
        /// <summary>
        /// WestlawNextHeaderComponent
        /// </summary>
        public WestlawNextHeaderComponent Header { get; private set; } = new WestlawNextHeaderComponent();
    }
}