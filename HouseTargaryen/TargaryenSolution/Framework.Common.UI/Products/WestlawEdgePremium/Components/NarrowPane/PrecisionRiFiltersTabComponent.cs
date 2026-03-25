namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;

    /// <summary>
    /// Precision Ri Filters Tab Component
    /// </summary>
    public class PrecisionRiFiltersTabComponent : RiFiltersTabComponent
    {
        /// <summary>
        /// Filter Facet
        /// </summary>
        public new PrecisionRiSearchFacetsFilterComponent RiFilter { get; } = new PrecisionRiSearchFacetsFilterComponent();
    }
}
