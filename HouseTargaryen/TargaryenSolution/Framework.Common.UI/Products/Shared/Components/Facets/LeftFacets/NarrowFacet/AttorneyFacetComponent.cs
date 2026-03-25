namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using OpenQA.Selenium;

    /// <summary>
    /// Attorney Facet Component
    /// </summary>
    public class AttorneyFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_attorney");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}