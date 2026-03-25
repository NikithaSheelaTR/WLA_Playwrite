

using OpenQA.Selenium;

namespace Framework.Common.UI.Products.Shared.Components.GraphDialog
{
    using Framework.Common.UI.Products.Shared.Enums.IpTools;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Enums;

    /// <summary>
    /// Graphs Dialog's' Type component
    /// </summary>
    public class TypeComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[@id='coid_graphType']");

        private const string ChartTypeLctMask = ".//a[@id='icon-{0}-graph']";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Select Chart type 
        /// </summary>
        public void SelectType(string chartType) =>
            DriverExtensions.GetElement(ContainerLocator, By.XPath(string.Format(ChartTypeLctMask, chartType))).Click();

        /// <summary>
        /// Verify if chart type is selected
        /// </summary>
        public bool IsSelected(string chartType) =>
            DriverExtensions.GetElement(ContainerLocator, By.XPath(string.Format(ChartTypeLctMask, chartType)))
                            .GetAttribute("class").Contains("is-active");
    }
}
