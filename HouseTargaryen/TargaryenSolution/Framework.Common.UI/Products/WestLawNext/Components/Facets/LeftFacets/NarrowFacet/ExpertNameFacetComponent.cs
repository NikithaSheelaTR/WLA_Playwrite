namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;

    using OpenQA.Selenium;

    /// <summary>
    /// ExpertNameFacetComponent
    /// </summary>
    public class ExpertNameFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_expertName");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}