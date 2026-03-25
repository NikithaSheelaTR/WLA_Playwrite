namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Components.TrDiscover;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using OpenQA.Selenium;

    /// <summary>
    /// Filters Tab Component
    /// </summary>
    public class FiltersTabComponent : BaseTabComponent
    {
        /// <summary>
        /// The content type count locator.
        /// </summary>
        private By ContentTypeCountLocator = By.XPath(".//h3 | .//h2");

        /// <summary>
        /// The container locator.
        /// </summary>
        private By ContainerLocator = By.XPath("//div[@id = 'co_narrowResultsBox']");

        /// <summary>
        /// Content Type name and results count
        /// </summary>
        public ILabel ContentTypeCount => new Label(this.ComponentLocator, this.ContentTypeCountLocator);

        /// <summary>
        /// Filter Facet
        /// </summary>
        public NewEdgeSearchFacetsFilterComponent Filter { get; } = new NewEdgeSearchFacetsFilterComponent();

        /// <summary>
        /// Precision Filter Facet 
        /// </summary>
        public PrecisionSearchFacetsFilterComponent PrecisionFilter { get; } = new PrecisionSearchFacetsFilterComponent();

        /// <summary>
        /// Applyed Filters Message 
        /// </summary>
        public AppliedFiltersMessageComponent AppliedFiltersMessageComponent { get; } = new AppliedFiltersMessageComponent();

        /// <summary>
        /// Tab Name
        /// </summary>
        protected override string TabName => "Filters";

        /// <summary>
        /// Component Locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}
