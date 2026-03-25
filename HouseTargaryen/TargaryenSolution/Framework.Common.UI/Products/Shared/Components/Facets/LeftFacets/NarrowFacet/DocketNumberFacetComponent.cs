namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using OpenQA.Selenium;

    /// <summary>
    /// DocketFacetComponent
    /// </summary>
    public class DocketNumberFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_docket");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}