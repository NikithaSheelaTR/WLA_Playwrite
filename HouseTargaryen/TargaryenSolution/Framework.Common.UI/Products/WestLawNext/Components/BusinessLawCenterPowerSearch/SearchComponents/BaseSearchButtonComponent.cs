namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.SearchComponents
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base search button section.
    /// </summary>
    /// <typeparam name="TReturnPage"> Type of page returns after search button click
    /// </typeparam>
    public abstract class BaseSearchButtonComponent<TReturnPage>
        where TReturnPage : BaseModuleRegressionPage
    {
        private static readonly By ClearFieldsLinkLocator = By.XPath("//a[@class='clearFieldsButton']");

        private static readonly By SearchButtonLocator = By.Id("searchButton");

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseSearchButtonComponent{TReturnPage}"/> class.
        /// </summary>
        protected BaseSearchButtonComponent()
        {
            DriverExtensions.WaitForElementDisplayed(SearchButtonLocator);
            DriverExtensions.WaitForElementDisplayed(ClearFieldsLinkLocator);
        }

        /// <summary>
        /// The click search button.
        /// </summary>
        /// <returns> new instance of the page </returns>
        public abstract TReturnPage ClickSearchButton();

        /// <summary>
        /// The click search button.
        /// </summary>
        protected void BaseClickSearchButton()
        {
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
        }
    }
}