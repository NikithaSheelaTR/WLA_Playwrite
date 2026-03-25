namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    /// <summary>
    /// Indigo Ri Narrow Pane Component
    /// </summary>
    public class EdgeRiNarrowPaneComponent
    {
        /// <summary>
        /// Content type facet
        /// </summary>
        public EdgeContentTypesFacetComponent ContentType { get; } = new EdgeContentTypesFacetComponent();

        /// <summary>
        /// Ri Filter facet
        /// </summary>
        public EdgeRiFacetsFilterComponent RiFilter { get; } = new EdgeRiFacetsFilterComponent();
    }
}
