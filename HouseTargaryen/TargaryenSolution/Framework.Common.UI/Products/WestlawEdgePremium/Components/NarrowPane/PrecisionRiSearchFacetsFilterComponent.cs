namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Ri Search Facets Filter Component
    /// </summary>
    public class PrecisionRiSearchFacetsFilterComponent : NewEdgeRiSearchFacetsFilterComponent
    {
        private static readonly By CitingProximityFacetLocator = By.XPath("//div[contains(@id, 'facet')][.//span[text() = 'Citing proximity']]");
        private static readonly By CitingRelationshipFacetLocator = By.XPath("//div[contains(@id, 'facet')][.//span[text() = 'Citing relationship']]");
        private static readonly By IntroductorySignalsFacetLocator = By.XPath("//div[contains(@id, 'facet')][.//span[text() = 'Introductory signals']]");
        

        /// <summary>
        /// Citing Proximity facet for the Co-cites RI tab
        /// </summary>
        public RiCitingProximityFacetComponent CitingProximityFacet =>
            new RiCitingProximityFacetComponent(CitingProximityFacetLocator);

        /// <summary>
        /// Citing Relationship facet for the Co-cites RI tab
        /// </summary>
        public BaseSearchHierarchyFacetComponent CitingRelationshipFacet =>
            new BaseSearchHierarchyFacetComponent(CitingRelationshipFacetLocator);

        /// <summary>
        /// Introductory Signals facet for the Cited With RI tab in Precision
        /// </summary>
        public RiIntroductorySignalsFacetComponent IntroductorySignalsFacet =>
            new RiIntroductorySignalsFacetComponent(IntroductorySignalsFacetLocator);
    }
}
