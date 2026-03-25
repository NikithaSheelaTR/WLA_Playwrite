namespace Framework.Common.UI.Products.WestLawNextCanada.Components.Facets
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using OpenQA.Selenium;

    /// <summary>
    /// Citing Relationship Left narrow facet component
    /// </summary>
    public class CitingRelationshipFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.CssSelector("#facet_div_cocitecitingrelationship");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}