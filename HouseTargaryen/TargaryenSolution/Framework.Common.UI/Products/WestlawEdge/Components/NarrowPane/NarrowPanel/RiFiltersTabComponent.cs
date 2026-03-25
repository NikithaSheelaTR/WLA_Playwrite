namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestLawNext.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Ri Filters Tab Component
    /// </summary>
    public class RiFiltersTabComponent : BaseTabComponent
    {
        private static readonly By ContentTypeCountLocator = By.XPath(".//h3");
        private static readonly By ContainerLocator = By.Id("panel_Filters");

        /// <summary>
        /// Content Type name and results count
        /// </summary>
        public ILabel ContentTypeCount => new Label(this.ComponentLocator, ContentTypeCountLocator);

        /// <summary>
        /// Filter Facet
        /// </summary>
        public NewEdgeRiSearchFacetsFilterComponent RiFilter { get; } = new NewEdgeRiSearchFacetsFilterComponent();

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
