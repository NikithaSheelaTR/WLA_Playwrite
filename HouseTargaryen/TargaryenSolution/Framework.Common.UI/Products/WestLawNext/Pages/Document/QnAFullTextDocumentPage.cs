namespace Framework.Common.UI.Products.WestLawNext.Pages.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchQuestionAnswerComponent;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Question And Answer Full Text Document Page.
    /// </summary>
    public class QnAFullTextDocumentPage : BaseModuleRegressionPage
    {
        private static readonly By QuestionLocator = By.XPath("//h2[@class='co_searchResults_question']");
        private static readonly By BackToResultsLinkLocator = By.LinkText("Back to Search");
        private static readonly By JurisdictionForStaticQaLocator = By.XPath("//span[contains(@class,'Results_jurisdiction')]");
        private static readonly By ExpandCollapseAnswersLocator = By.Id("qa_maximizeMinimize");
        private static readonly By PrintButtonLocator = By.Id("deliveryLinkRowPrint");
        private static readonly By EmailButtonLocator = By.Id("deliveryLinkRowEmail");
        private static readonly By DownloadButtonLocator = By.Id("deliveryLinkRowDownload");

        /// <summary>
        /// Initializes a new instance of the <see cref="QnAFullTextDocumentPage"/> class. 
        /// </summary>
        public QnAFullTextDocumentPage()
        {
            DriverExtensions.WaitForElement(QuestionLocator);
        }

        /// <summary>
        /// The Question and Answer Result list component
        /// </summary>
        /// <returns>QnA reult list<see cref="SearchQuestionAnswerResultListComponent"/>.</returns>
        public SearchQuestionAnswerResultListComponent QnAResultList => new SearchQuestionAnswerResultListComponent();

        /// <summary>
        /// Returns the question text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetQuestion() => DriverExtensions.WaitForElement(QuestionLocator).Text;

        /// <summary>
        /// Check if Back to result link is displayed
        /// </summary>
        /// <returns>True if the back to search results link is displayed, otherwise - false</returns>
        public bool IsBackToSearchResultsLinkDisplayed() => DriverExtensions.IsDisplayed(BackToResultsLinkLocator);

        /// <summary>
        /// Clicks the Back to Search Results Page
        /// </summary>
        /// <returns>The <see cref="CategorySearchResultPage"/>.</returns>
        public CategorySearchResultPage ClickBackToSearchResultsLink()
        {
            DriverExtensions.WaitForElement(BackToResultsLinkLocator).Click();
            return new CategorySearchResultPage();
        }

        /// <summary>
        /// Returns jurisdiction for easy qa
        /// </summary>
        /// <returns>Jurisdiction</returns>
        public string GetJurisdictionForStaticQaText() => DriverExtensions.GetText(JurisdictionForStaticQaLocator);

        /// <summary>
        /// Expands to see answer from other jurisdiction
        /// </summary>
        public void ExpandAnswerFromOtherJurisdiction() => DriverExtensions.Click(ExpandCollapseAnswersLocator);

        #region Toolbar options
        /// <summary>
        /// Clicks Print button
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <returns>
        /// A new instance of an object of type T
        /// </returns>
        public T ClickPrintButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PrintButtonLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks Delivery button
        /// </summary>
        /// <typeparam name="T"> T
        /// </typeparam>
        /// <returns>
        /// A new instance of an object of type T
        /// </returns>
        public T ClickDownloadButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DownloadButtonLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks Email button
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <returns>
        /// A new instance of an object of type T
        /// </returns>
        public T ClickEmailButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(EmailButtonLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }
        #endregion Toolbar options
    }
}