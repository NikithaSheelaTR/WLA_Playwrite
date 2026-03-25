namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint Analyser Filter Facet
    /// </summary>
    public class ComplaintAnalyserFilterComponent: NewEdgeRecentFiltersFacetComponent
    {
       private new By ComponentLocator;

        /// <summary>
        /// Complaint Analyser Filter Facet
        /// </summary>
        /// 
        public ComplaintAnalyserFilterComponent(By componentLocator)
        {
            this.ComponentLocator = componentLocator;
        }

        /// <summary>
        /// Get list of Filter Check Labels
        /// </summary>
        public ComplaintAnalyzerPartyFacetComponent PartyFacet => new ComplaintAnalyzerPartyFacetComponent(this.ComponentLocator);
    }
}
