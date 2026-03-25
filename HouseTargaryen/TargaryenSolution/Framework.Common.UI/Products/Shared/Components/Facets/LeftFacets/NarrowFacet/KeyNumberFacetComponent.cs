namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using OpenQA.Selenium;

    /// <summary>
    /// KeyNumberFacetComponent
    /// </summary>
    public class KeyNumberFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_keynumber, #facet_div_customDigestKeyNumber");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}