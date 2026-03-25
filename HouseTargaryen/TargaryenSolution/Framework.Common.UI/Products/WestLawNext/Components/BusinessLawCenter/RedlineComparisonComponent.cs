namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenter
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RedlineComparisonComponent on the BLC Category page
    /// </summary>
    public class RedlineComparisonComponent : BaseModuleRegressionComponent
    {
        private static readonly By LaunchToolButtonLocator = By.Id("coid_redlineView_launchButton");

        private static readonly By ContainerLocator = By.XPath("//div[.//input[@id='coid_redlineView_launchButton'] and ./h3[contains(text(),'Redline Comparison')]]");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click Launch Tool button
        /// </summary>
        /// <typeparam name="T"> age type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickLaunchToolButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(LaunchToolButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}