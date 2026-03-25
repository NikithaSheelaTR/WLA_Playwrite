namespace Framework.Common.UI.Products.Patron.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page for sponsor accounts which have all account slots currently in use
    /// </summary>
    public class UnableToAccessWestlawPage : BaseModuleRegressionPage
    {
        private static readonly By RetryButtonLocator = By.Id("coid_button_retry");
        

        /// <summary>
        /// Click on the retry button.
        /// </summary>
        public void ClickRetry() => DriverExtensions.WaitForElement(RetryButtonLocator).Click();

        /// <summary>
        /// Verify that page is displayed
        /// </summary>
        /// <returns> True if page is displayed, false otherwise </returns>
        public bool IsPageDisplayed() => DriverExtensions.IsDisplayed(RetryButtonLocator, 5);
    }
}