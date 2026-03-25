namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Citing Proximity Facet Component in Narrow Pane of Canada
    /// </summary>
    public class CitingProximityFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_cociteproximity");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}