namespace Framework.Common.UI.Products.WestlawEdge.Pages.CaseEvaluator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Represents Document page specific to CE with CE toolbar (return button, etc)
    /// </summary>
    public class EdgeCaseEvaluatorDocumentPage : EdgeCommonDocumentPage
    {
        private static readonly By FooterBreadcrumbReturnLocator = By.XPath(
                "//*[@id='co_docHeaderReturnTo']/a/span");
             
        /// <summary>
        /// Is Return Button Displayed
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsReturnButtonDisplayed()
                => DriverExtensions.IsDisplayed(FooterBreadcrumbReturnLocator, 5);

        /// <summary>
        /// Return to CE report page
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>report page or doc list page</returns>
        public T ClickReturn<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(FooterBreadcrumbReturnLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
