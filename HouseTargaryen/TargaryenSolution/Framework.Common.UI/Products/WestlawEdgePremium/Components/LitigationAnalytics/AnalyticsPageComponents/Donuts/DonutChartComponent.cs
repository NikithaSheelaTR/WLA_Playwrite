namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.LitigationAnalytics.AnalyticsPageComponents.Donuts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Initializes a new instance of the <see cref="DonutChartComponent"/> class.
    /// </summary>
    public class DonutChartComponent : BaseModuleRegressionComponent, ICreatablePageObject
    {
        private const string DonutPartLctMask = ".//*[@data-name = '{0}']";

        private const string BlackColor = "rgb(218, 218, 218)";

        private static readonly By PercentagesLocator = By.Id("donutTextBig");

        private static readonly By ContainerLocator = By.XPath("//la-toggle-widget//*[@id = 'la-donut-chart']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is Donut Chart part active
        /// </summary>
        public bool IsDonutChartPartActive(string nameOfPart)
            => !DriverExtensions.WaitForElement(DriverExtensions.WaitForElement(this.ComponentLocator), By.XPath(string.Format(DonutPartLctMask, nameOfPart)))
                            .GetCssValue("fill")
                            .Equals(BlackColor);

        /// <summary>
        /// Gets percentages
        /// </summary>
        /// <returns></returns>
        public string GetPercentages() => DriverExtensions.GetElement(this.ComponentLocator, PercentagesLocator).Text;
    }
}