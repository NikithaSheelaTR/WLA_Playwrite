namespace Framework.Common.UI.Products.WestLawAnalytics.Components.QuickView
{
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Analytics report item
    /// </summary>
    public class AnalyticsReportComponent : BaseModuleRegressionComponent
    {
        private static readonly By NoUsageMessageLocator = By.Id("ConfirmationMessage1_lMessage");

        private static readonly By ViewFullprintableHtmlReportButtonLocator = By.XPath("//*[contains(@href, 'javascript:popUp')]");

        private static readonly By ContainerLocator = By.Id("Table1");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Checks whether Report is present
        /// </summary>
        /// <returns><see cref="bool"/></returns>
        public bool IsReportPresent()
        {
            bool isThereReport = DriverExtensions.GetElements(ViewFullprintableHtmlReportButtonLocator).Any()
                                 && DriverExtensions.GetElements(ViewFullprintableHtmlReportButtonLocator)
                                                    .First()
                                                    .Displayed;
            bool isNoReport = DriverExtensions.GetElements(NoUsageMessageLocator).Any()
                   && DriverExtensions.GetElements(NoUsageMessageLocator).First().Displayed;

            return isThereReport || isNoReport;
        }
    }
}