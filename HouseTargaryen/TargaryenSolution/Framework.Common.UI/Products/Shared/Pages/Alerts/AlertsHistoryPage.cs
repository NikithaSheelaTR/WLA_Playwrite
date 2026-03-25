namespace Framework.Common.UI.Products.Shared.Pages.Alerts
{
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alerts History page
    /// </summary>
    public class AlertsHistoryPage : BaseModuleRegressionPage
    {
        private const string AlertSearchTermbyNameLctMask = "//a[text()='{0}']//ancestor::td[contains(@class,'newsletter')]//span";

        /// <summary>
        /// Get search term by alert name
        /// </summary>
        /// <param name="alertName"> Alert name </param>
        /// <returns> Search term </returns>
        public string GetSearchTermByAlertName(string alertName) =>
            DriverExtensions.GetText(By.XPath(string.Format(AlertSearchTermbyNameLctMask, alertName)));

        /// <summary>
        /// Date Facet Component
        /// </summary>
        public DateFacetComponent DateFacet { get; } = new DateFacetComponent();
    }
}
