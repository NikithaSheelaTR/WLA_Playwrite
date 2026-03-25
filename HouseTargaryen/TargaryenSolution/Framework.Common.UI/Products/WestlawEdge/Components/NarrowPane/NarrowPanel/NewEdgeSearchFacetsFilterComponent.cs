namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    /// <summary>
    /// EdgeSearchFacetsFilterComponent for new Narrow Panel
    /// </summary>
    public class NewEdgeSearchFacetsFilterComponent : EdgeSearchFacetsFilterComponent
    {
        /// <summary>
        /// new component for Recent Filters 
        /// </summary>
        public new NewEdgeRecentFiltersFacetComponent RecentFiltersFacet { get; } = new NewEdgeRecentFiltersFacetComponent();
    }
}