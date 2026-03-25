
namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Tsdr Status Facet Component
    /// </summary>
    public class TsdrStatusFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@id,'facet_div_') and contains(@id,'Status')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
