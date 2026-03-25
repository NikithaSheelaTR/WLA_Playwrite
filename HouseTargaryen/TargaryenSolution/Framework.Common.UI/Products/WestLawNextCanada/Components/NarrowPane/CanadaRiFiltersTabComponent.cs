namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Ri Filters Tab Component
    /// </summary>
    public class CanadaRiFiltersTabComponent : RiFiltersTabComponent
    {
        private static readonly By RiAbridgmentClassificationFacetLocator = By.XPath("//div[@class = 'co_divider co_entry_facet' and .//*[contains(@id, 'AbridgmentTopicsHeader')]]");

        /// <summary>
        /// Abridgement Classification Facet component
        /// </summary>
        public CanadaSearchHierarchyFacetComponent AbridgementClassificationFacet =>
            new CanadaSearchHierarchyFacetComponent(RiAbridgmentClassificationFacetLocator);
    }
}
