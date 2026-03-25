namespace Framework.Common.UI.Products.Shared.Components.BreadCrumb
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Alert BreadCrumb Component
    /// </summary>
    public class AlertBreadCrumbComponent : BaseModuleRegressionComponent
    {
        private static readonly By ContainerLocator = By.XPath("//div[contains(@class,'co_breadcrumbs')]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Is breadcrumb trail displayed
        /// </summary>
        /// <returns>True - if it is displayed, false - otherwise</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Get breadcrumb trail text
        /// </summary>
        /// <returns>Text of breadcrumb trail</returns>
        public string GetBreadcrumbTrailText() => DriverExtensions.WaitForElement(this.ComponentLocator).GetText().Replace("\r\n", " ");
    }
}
