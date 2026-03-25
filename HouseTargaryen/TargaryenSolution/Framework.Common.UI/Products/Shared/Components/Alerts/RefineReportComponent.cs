namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using OpenQA.Selenium;

    /// <summary>
    /// Refine Report Component
    /// </summary>
    public class RefineReportComponent : BaseAlertComponent
    {
        private static readonly By ContainerLocator = By.Id("refineSection");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;
    }
}