
namespace Framework.Common.UI.Raw.WestlawEdge.Pages.LegalAnalaytics
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Analytics Profiler Page
    /// </summary>
    public class EdgeAnalyticsProfilerPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ChatLocator = By.Id("la-donut-chart");

        /// <summary>
        /// Is Chart Desplayed
        /// </summary>
        /// <returns> true if present </returns>
        public bool IsChartDisplayed() => DriverExtensions.IsDisplayed(ChatLocator);

    }
}
