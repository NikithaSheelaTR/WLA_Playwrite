namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Alerts
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Pages;

    using OpenQA.Selenium;

    /// <summary>
    /// Canada Alert History Result page
    /// </summary>
    public class CanadaAlertHistoryResultPage : BaseModuleRegressionPage
    {
        private static readonly By AlertHistoryResultPageTitleLocator = By.XPath("//*[@id='co_alerts']//h2");

        /// <summary>
        /// Alert History Result Page title label
        /// </summary>
        public ILabel AlertHistoryResultPageTitle = new Label(AlertHistoryResultPageTitleLocator);
    }
}