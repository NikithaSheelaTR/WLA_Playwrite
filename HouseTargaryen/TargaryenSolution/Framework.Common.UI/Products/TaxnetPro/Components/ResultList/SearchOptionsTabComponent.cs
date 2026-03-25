namespace Framework.Common.UI.Products.TaxnetPro.Components.ResultList
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Component represents search tab options components, such as All Content and Advanced Search tabs
    /// </summary>
    public class SearchOptionsTabComponent : BaseModuleRegressionComponent
    {
        private static readonly By SearchOptionsTabComponentLocator = By.XPath("//ul[@aria-label='Search Options']");

        private static readonly By AllContentTabLocator = By.XPath("//span[text()='All Content']");

        private static readonly By AdvancedSearchTabLocator = By.XPath("//span[text()='Advanced Search']");


        /// <summary>
        /// Click All Content Tab button
        /// </summary>
        public void ClickAllContentTab()
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(AllContentTabLocator));
        }

        /// <summary>
        /// Click Advanced Search Tab button
        /// </summary>
        public void ClickAdvancedSearchTab()
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(AdvancedSearchTabLocator));
        }

        /// <summary>
        /// Comopnent Locator
        /// </summary>
        protected override By ComponentLocator => SearchOptionsTabComponentLocator;
    }
}
