namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Browse
{
    using Framework.Common.UI.Products.WestLawNextCanada.Components;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Lareference;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Toolbar;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using OpenQA.Selenium;

    /// <summary>
    /// Canadian Abridgment Digests Category Page
    /// </summary>
    public class CanadaCommonBrowsePage : EdgeCommonBrowsePage
    {
        private static readonly By ToolbarContainerLocator = By.ClassName("La-reference-header-buttons");

        /// <summary>
        /// Header
        /// </summary>
        public new CanadaEdgeHeaderComponent Header { get; } = new CanadaEdgeHeaderComponent();

        /// <summary>
        /// Laref Banner component
        /// </summary>
        public LarefBannerComponent LarefBanner { get; } = new LarefBannerComponent();

        /// <summary>
        /// Toolbar component
        /// </summary>
        public CanadaToolbarComponent Toolbar { get; set; } = new CanadaToolbarComponent(ToolbarContainerLocator);
    }
}