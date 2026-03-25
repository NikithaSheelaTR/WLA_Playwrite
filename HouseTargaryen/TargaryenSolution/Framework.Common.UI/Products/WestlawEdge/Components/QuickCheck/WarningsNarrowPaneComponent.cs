namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.Facets;

    using OpenQA.Selenium;

    /// <summary>
    /// The pane with content types and facets on the recommendations tab.
    /// </summary>
    public class WarningsNarrowPaneComponent : RecommendationsNarrowPaneComponent
    {
        private static readonly By CitationsTagsFacetLocator = By.XPath("//section[contains(@class, 'SearchFacetMultipleXBoxes-CitationTag')]");
        private static readonly By ContainerLocator = By.XPath("//div[contains(@id,'DALeftColumn')]/div");

        /// <summary>
        /// The KeyCite warnings facet component (Check your work).
        /// </summary>
        public KeyCiteWarningsFacetComponent KeyCiteWarningsFacetComponent { get; } = new KeyCiteWarningsFacetComponent();

        /// <summary>
        /// The Citation tags facet component
        /// </summary>
        public BaseSearchHierarchyFacetComponent CitationTagsFacetComponent { get; } =
            new BaseSearchHierarchyFacetComponent(CitationsTagsFacetLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}