
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Design Type Facet Component
    /// </summary>
    public class DesignTypeFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='facet_div_trademarkDesignType']");
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
