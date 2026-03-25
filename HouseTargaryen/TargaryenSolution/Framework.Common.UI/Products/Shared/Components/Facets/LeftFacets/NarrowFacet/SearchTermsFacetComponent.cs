namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    using OpenQA.Selenium;

    /// <summary>
    /// Search terms facet component
    /// </summary>
    public class SearchTermsFacetComponent : BaseFacetComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_searchterm");
        private static readonly By SearchTermsFilterLabelLocator = By.XPath("//*[@id='coid_graphicalHistory_searchTerms_header']//h4");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///  Search terms filter label on the filter panel on graphical history page
        /// </summary>
        public ILabel SearchTermsFilterLabel => new Label(SearchTermsFilterLabelLocator);
    }
}
