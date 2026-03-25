namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using OpenQA.Selenium;

    /// <summary>
    /// Describe Law Firm Facet Component
    /// </summary>
    public class LawFirmFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_lawfirm");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}