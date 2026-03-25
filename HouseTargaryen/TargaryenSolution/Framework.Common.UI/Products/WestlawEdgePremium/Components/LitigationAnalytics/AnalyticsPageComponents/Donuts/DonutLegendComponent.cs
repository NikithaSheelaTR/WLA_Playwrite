namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Donuts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using System.Linq;

    /// <summary>
    /// The DonutLegendComponent class
    /// </summary>
    public class DonutLegendComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private static readonly By ContainerLocator = By.XPath("//la-toggle-widget//la-base-donut-legend");

        private static readonly By DonutLegendTableLocator = By.XPath(".//*[@class ='la-DonutLegend']");

        private static readonly By DonutLegendRowLocator = By.XPath("./*[@class ='la-DonutLegend-row']");

        private static readonly By DonutLegendRowText = By.XPath(".//span[@class = 'la-DonutLegend-label']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Hovers on Row by Index
        /// </summary>
        /// <param name="index"></param>
        public void HoverOnRowByIndex(int index)
            => DriverExtensions.GetElements(this.ComponentLocator, DonutLegendTableLocator, DonutLegendRowLocator)
                                .ElementAt(index)
                                .SeleniumHover();

        /// <summary>
        /// Gets row text by index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetRowTextByIndex(int index)
            => DriverExtensions.GetElements(this.ComponentLocator, DonutLegendTableLocator, DonutLegendRowText)
                            .ElementAt(index)
                            .Text;
    }
}