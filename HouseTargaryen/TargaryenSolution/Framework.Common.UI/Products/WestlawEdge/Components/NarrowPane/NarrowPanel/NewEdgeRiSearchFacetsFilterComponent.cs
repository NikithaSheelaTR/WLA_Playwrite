namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    /// <summary>
    /// New Edge Ri Search Facets Filter Component
    /// </summary>
    public class NewEdgeRiSearchFacetsFilterComponent : EdgeRiFacetsFilterComponent
    {
        /// <summary>
        /// new component for Recent Filters 
        /// </summary>
        public NewEdgeRecentFiltersFacetComponent RecentFiltersFacet { get; } = new NewEdgeRecentFiltersFacetComponent();
    }
}
