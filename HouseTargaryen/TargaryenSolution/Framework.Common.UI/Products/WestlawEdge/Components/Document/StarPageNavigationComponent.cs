namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// GoTo component
    /// </summary>
    public class StarPageNavigationComponent : BaseModuleRegressionComponent
    {
        private static readonly By SearchInputLocator = By.Id("co_document_starPage_starPageNavInput");
        private static readonly By GoToStarPageButtonLocator = By.Id("co_document_starPage_starPageNavGo");
        private static readonly By ErrorContainerLocator = By.Id("co_document_starPage_starPageNavError");
        private static readonly By ErrorMessageTextLocator = By.XPath(".//div[@class='co_infoBox_message']");
        private static readonly By ContainerLocator = By.Id("co_starPage");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Navigates to the star page withing the document
        /// </summary>
        /// <param name="query"></param>
        public void NavigateToStarPage(string query)
        {
            DriverExtensions.WaitForElement(SearchInputLocator).SendKeys(query);
            DriverExtensions.WaitForElement(GoToStarPageButtonLocator).Click();
        }

        /// <summary>
        /// Navigates to the star page withing the document
        /// </summary>
        /// <param name="query"></param>
        public void NavigateToStarPageUsingEnterButton(string query)
        {
            DriverExtensions.WaitForElement(SearchInputLocator).SendKeys(query);
            DriverExtensions.PressKey(Keys.Enter);
        }

        /// <summary>
        /// Gets error message if displayed
        /// </summary>
        /// <returns>Error message text, if error message is displayed, otherwise returns empty string </returns>
        public string GetErrorMessage() => DriverExtensions.IsDisplayed(ErrorContainerLocator, 5)
                                             ? DriverExtensions.GetElement(ErrorContainerLocator, ErrorMessageTextLocator).Text
                                             : string.Empty;
    }
}
