namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Legend  Component
    /// </summary>
    public class LegendComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@class= 'la-Chart-key']");
        private static readonly By LegendColorLocator = By.XPath(".//*[@class= 'la-Chart-keyChit']");

        /// <summary>
        /// Legend  Component
        /// </summary>
        public LegendComponent()
        {

        }

        /// <summary>
        /// Legend color item
        /// </summary>
        public List<string> LegendColorsList =>
            DriverExtensions.GetElements(DriverExtensions.GetElement(this.ComponentLocator), LegendColorLocator)
                            .ToList()
            .Select(item => item.GetAttribute("style").Substring(18, 17)).ToList();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}