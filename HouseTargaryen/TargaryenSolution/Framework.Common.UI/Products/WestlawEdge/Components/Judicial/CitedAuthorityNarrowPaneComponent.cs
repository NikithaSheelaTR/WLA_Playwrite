namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial
{
    using Framework.Common.UI.Products.WestlawEdge.Components.Judicial.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial cited authority narrow pane
    /// </summary>
    public sealed class CitedAuthorityNarrowPaneComponent : RecommendationsNarrowPaneComponent
    {
        private static readonly By CitedByFacetComponentLocator = By.ClassName("SearchFacetMultipleXBoxes-CitedByParty");

        private static readonly By PartyDocumentsFacetComponentLocator = By.ClassName("SearchFacetMultipleXBoxes-PartyDocuments");

        /// <summary>
        /// Cited by Facet Component
        /// </summary>
        public BaseSearchHierarchyFacetComponent CitedByFacetComponent => new BaseSearchHierarchyFacetComponent(CitedByFacetComponentLocator);

        /// <summary>
        /// Party documents component
        /// </summary>
        public PartyDocumentsFacetComponent PartyDocumentsFacetComponent => new PartyDocumentsFacetComponent(PartyDocumentsFacetComponentLocator);
    }
}
