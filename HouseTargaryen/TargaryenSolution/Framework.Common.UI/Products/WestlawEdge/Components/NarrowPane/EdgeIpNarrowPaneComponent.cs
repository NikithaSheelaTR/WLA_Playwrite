

using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;

namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane
{
    using Framework.Common.UI.Products.Shared.Components;

    using OpenQA.Selenium;

    /// <summary>
    /// Edge Ip Narrow Pane Component
    /// </summary>
    public class EdgeIpNarrowPaneComponent: BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='co_narrowResultsBox']");

        /// <summary>
        /// Edge Ip Narrow Pane Component
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        ///Ip Filter Facet
        /// </summary>
        public EdgeIpSearchFacetsFilterComponent Filter { get; } = new EdgeIpSearchFacetsFilterComponent();
    }
}
