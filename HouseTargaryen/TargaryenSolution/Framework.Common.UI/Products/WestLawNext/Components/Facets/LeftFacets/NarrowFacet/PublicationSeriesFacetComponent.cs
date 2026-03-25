namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// PublicationSeriesFacetComponent
    /// </summary>
    public class PublicationSeriesFacetComponent : MoreLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_MetaDataBrandFacet");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}