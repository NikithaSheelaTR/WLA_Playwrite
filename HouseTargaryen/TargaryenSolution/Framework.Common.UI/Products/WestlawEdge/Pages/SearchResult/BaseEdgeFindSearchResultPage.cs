namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// Find Search Result Page
    /// </summary>
    public abstract class BaseEdgeFindSearchResultPage : BaseEdgeSearchResultPage
    {
        /// <summary>
        /// Toolbar component
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();
    }
}
