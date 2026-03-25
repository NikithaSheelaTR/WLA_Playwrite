namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using OpenQA.Selenium;

    /// <summary>
    /// Parallel Search Filter Facet
    /// </summary>
    public class ParallelSearchFilterFacetComponent : NewEdgeRecentFiltersFacetComponent
    {
        private static readonly By FilterContainerLocator = By.XPath("//saf-faceted-filter[@filter-title='Filters']");
       
        /// <summary>
        ///  Date Facet component
        /// </summary>
        public ParallelSearchDateFacetComponent DateFacet { get; } = new ParallelSearchDateFacetComponent();

        /// <summary>
        ///  Search within results facet component
        /// </summary>
        public ParallelSearchSearchWithinResultsFacetComponent SearchWithinResultsFacet { get; } = new ParallelSearchSearchWithinResultsFacetComponent();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FilterContainerLocator;

        /// <summary>
        /// Jurisdiction Facet component
        /// </summary>
        public ParallelSearchJurisdictionFacetComponent JurisdictionFacet { get; } = new ParallelSearchJurisdictionFacetComponent();
    }
}
