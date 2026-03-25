using Framework.Common.UI.Products.WestLawNext.Components;
using Framework.Common.UI.Products.WestLawNext.Components.Facets.RiPageFacets;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    /// <summary>
    /// Content Types Tab Component
    /// </summary>
    public class RiContentTypesTabComponent : BaseTabComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='panel_ContentTypes']");

        /// <summary>
        /// Content type facet
        /// </summary>
        public EdgeContentTypesFacetComponent RiContentType => new EdgeContentTypesFacetComponent();

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Content types";
    }
}