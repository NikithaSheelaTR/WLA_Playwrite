namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NarrowPane
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.Facets;

    /// <summary>
    /// Left narrow component of Westlaw Canada
    /// </summary>
    public class LeftNarrowPaneComponent : NarrowPaneComponent
    {
        /// <summary>
        /// Citing Relationship Facet Component
        /// </summary>
        public CitingRelationshipFacetComponent CitingRelationshipFacet { get; } = new CitingRelationshipFacetComponent();

        /// <summary>
        /// Citing Proximity Facet Component
        /// </summary>
        public CitingProximityFacetComponent CitingProximityFacet { get; } = new CitingProximityFacetComponent();

        /// <summary>
        /// Abridgment Classification Facet Component
        /// </summary>
        public AbridgmentClassificationFacetComponent AbridgmentClassificationFacet { get; } = new AbridgmentClassificationFacetComponent();
        
        /// <summary>
        /// Subject Area Facet Component
        /// </summary>
        public SubjectAreaFacetComponent SubjectAreaFacet { get; } = new SubjectAreaFacetComponent();
    }
}