
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// Design Category Facet Component
    /// </summary>
    public class DesignCategoryFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='facet_div_trademarkDesignCategoryWide']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
