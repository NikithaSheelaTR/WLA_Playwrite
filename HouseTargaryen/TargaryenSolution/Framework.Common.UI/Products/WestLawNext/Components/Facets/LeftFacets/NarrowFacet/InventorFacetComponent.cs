
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// Inventor Facet Component
    /// </summary>
    public class InventorFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_ipInventor");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
