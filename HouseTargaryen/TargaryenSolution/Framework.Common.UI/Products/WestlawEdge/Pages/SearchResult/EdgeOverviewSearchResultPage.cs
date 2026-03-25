namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// The edge overview search result page.
    /// </summary>
    public sealed class EdgeOverviewSearchResultPage : BaseEdgeSearchResultPage
    {
        /// <summary>
        /// The result list.
        /// </summary>
        public IOverviewSearchResultList ResultList => new OverviewSearchResultList();

        /// <summary>
        /// Gets the Toolbar component
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// Gets the content type facet
        /// </summary>
        public EdgeContentTypesFacetComponent ContentType { get; } = new EdgeContentTypesFacetComponent();
    }
}
