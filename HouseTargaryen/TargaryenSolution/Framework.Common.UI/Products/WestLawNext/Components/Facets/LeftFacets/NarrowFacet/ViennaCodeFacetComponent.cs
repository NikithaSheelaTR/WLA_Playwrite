
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Vienna code Facet Component
    /// </summary>
    public class ViennaCodeFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='facet_div_trademarkViennaCode']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
