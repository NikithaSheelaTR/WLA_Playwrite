namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// Statute Title Facet 
    /// </summary>
    public class StatuteTitleFacetComponent : BaseFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_title");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}