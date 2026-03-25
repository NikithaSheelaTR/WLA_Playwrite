
namespace Framework.Common.UI.Products.Shared.Components.GraphDialog
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Dialogs.Facets;
    using Framework.Common.UI.Products.Shared.Items.GraphData;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Graphs Dialog's' charts component
    /// </summary>
    public class ChartComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='coid_graphicalChart']");

        private static readonly By ChartLegendLocator = By.XPath(".//*[name() = 'g' and @class = 'highcharts-legend']");
            
        private static readonly By ChartNodePointsLocator = By.XPath(".//*[name() = 'svg']//*[contains(@class,'highcharts-tracker')]/*[name()='rect' or name() = 'path']");

        private static readonly By ChartLegendItemContainerLocator = By.XPath("//*[name() = 'g' and @class = 'highcharts-legend-item']");
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets Graph data items
        /// </summary>
        public List<GraphLegendItem> GetGraphLegendItems() =>
            DriverExtensions.GetElements(ChartLegendLocator, ChartLegendItemContainerLocator)
                            .Select(elem => new GraphLegendItem(elem)).ToList();

        /// <summary>
        /// Hovers chart
        /// </summary>
        /// <returns> Pop Up dialog </returns>
        public ChartsPopUpDialog HoverChart()
        {
            DriverExtensions.GetElement(ContainerLocator, ChartNodePointsLocator).SeleniumHover();
            return  new ChartsPopUpDialog();
        }

        /// <summary>
        /// Verifies if Legend is displayed
        /// </summary>
        public bool IsLegendDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, ChartLegendLocator);
    }
}
