

namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.DropDowns;

    using OpenQA.Selenium;

    /// <summary>
    /// Owner Facet Component
    /// </summary>
    public class OwnerFacetComponent : BaseLinkFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@Id,'facet_div_') and contains(@id,'Owner')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}

