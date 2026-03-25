namespace Framework.Common.UI.Products.WestLawNext.Pages.CaseEvaluator
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// represents Document page specific to CE with CE toolbar (return button, etc)
    /// </summary>
    public class CaseEvaluatorDocumentPage : CommonDocumentPage
    {
        private static readonly By DisplayOptionsLocator = By.Id("co_docToolbarDisplayOptions");

        private static readonly By ReturnButtonLocator = By.XPath("//div[@id='co_documentFooterBreadcrumb']/a");

        /// <summary>
        /// Initializes a new instance of the <see cref="CaseEvaluatorDocumentPage"/> class. 
        /// constructor
        /// </summary>
        public CaseEvaluatorDocumentPage()
        {
            DriverExtensions.WaitForElement(DisplayOptionsLocator);
        }

        /// <summary>
        /// return to CE report page
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>report page or doc list page</returns>
        public T ClickReturn<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ReturnButtonLocator).JavascriptClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// is case evaluator document page
        /// </summary>
        /// <returns>true or false</returns>
        public bool IsCaseEvalDocPageDisplayed()
            => DriverExtensions.IsDisplayed(DisplayOptionsLocator, 5) && DriverExtensions.IsDisplayed(ReturnButtonLocator, 5);
    }
}