using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacetusing;

namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    /// <summary>
    /// Indigo Narrow Pane Component
    /// </summary>
    public class EdgeNarrowPaneComponent
    {
        /// <summary>
        /// Content type facet
        /// </summary>
        public EdgeContentTypesFacetComponent ContentType { get; } = new EdgeContentTypesFacetComponent();

        /// <summary>
        /// Filter Facet
        /// </summary>
        public EdgeSearchFacetsFilterComponent Filter { get; } = new EdgeSearchFacetsFilterComponent();

        ///<summary>
        /// CourtLevelFacetComponent
        /// </summary>
        public CourtLevelFacetComponent CourtLevelFacet { get; } = new CourtLevelFacetComponent();
    }
}
