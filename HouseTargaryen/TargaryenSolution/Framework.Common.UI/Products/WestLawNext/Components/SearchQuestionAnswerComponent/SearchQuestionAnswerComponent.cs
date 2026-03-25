namespace Framework.Common.UI.Products.WestLawNext.Components.SearchQuestionAnswerComponent
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The teaser component.
    /// </summary>
    public class SearchQuestionAnswerComponent : BaseModuleRegressionComponent
    {
        private static readonly By CollapseButtonLocator = By.XPath("//div[@id='coid_website_questionAndAnswerTypeA']//a[@class='co_widget_collapseIcon']");

        private static readonly By ExpandButtonLocator = By.XPath("//div[@id='coid_website_questionAndAnswerTypeA']//a[@class='co_widget_expandIcon']");

        private static readonly By JurisLocator = By.ClassName("co_searchResults_jurisdiction");

        private static readonly By QuestionLocator = By.XPath("//div[@id='coid_website_questionAndAnswerTypeA']//h2");

        private static readonly By ShowMoreDetailsLinkLocator = By.XPath("//div[@class='co_qaDetailsWrapper Tab-viewAll']/a");

        private static readonly By ContainerLocator = By.XPath("//div[contains(@class ,'co_genericBox co_searchResults_summary co_searchResults_summaryQA co_search_detailLevel_2 ')]");

        private static readonly By InnerDocumentsLocator = By.XPath("//div[@class='co_document co_pftLegalStatus']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The Question and Answer Result list component
        /// </summary>

        public SearchQuestionAnswerResultListComponent QnAResultList = new SearchQuestionAnswerResultListComponent();

        /// <summary>
        /// Returns question text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetQuestion() => DriverExtensions.WaitForElement(QuestionLocator).Text;

        /// <summary>
        /// Returns the Applicable jurisdiction for the provided answer
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetAnswersJurisdiction() => DriverExtensions.WaitForElement(JurisLocator).Text.Trim();

        /// <summary>
        /// Clicks the View More Details Link, Returns a QnAFullTextDocumentPage 
        /// </summary>
        /// <returns>New instance of QaFullTextDocumentPage</returns>
        public QnAFullTextDocumentPage ClickShowMoreLink()
            => this.ClickElementAndWait<QnAFullTextDocumentPage>(ShowMoreDetailsLinkLocator);

        /// <summary>
        /// Collapse the QnAComponent box
        /// </summary>
        /// <returns>The <see cref="CategorySearchResultPage"/>.</returns>
        public CategorySearchResultPage CollapseQnAComponent()
            => this.ClickElementAndWait<CategorySearchResultPage>(CollapseButtonLocator);

        /// <summary>
        /// Expand Question and Answers Component
        /// </summary>
        public void ExpandQnAComponent() => DriverExtensions.WaitForElement(ExpandButtonLocator).Click();
    
        /// <summary>
        /// Verify that Question and Answer component is expanded
        /// </summary>
        /// <returns> True if expanded, false otherwise </returns>
        public bool IsExpanded() => DriverExtensions.IsDisplayed(InnerDocumentsLocator, 5);

        /// <summary>
        /// Verify that Question and Answer component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Verify that Question and Answers Component is collapsed
        /// </summary>
        /// <returns>True if collapsed, false otherwise</returns>
        public bool IsQnAComponentCollapsed() => DriverExtensions.GetAttribute("class", ContainerLocator).Contains("collapsed");

        /// <summary>
        /// Verify that Question and Answers Component is expanded
        /// </summary>
        /// <returns>True if expanded, false otherwise</returns>
        public bool IsQnAComponentExpanded() => DriverExtensions.GetAttribute("class",ContainerLocator).Contains("expanded");

        /// <summary>
        /// Verify that Show button is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsShowButtonDisplayed() => DriverExtensions.IsDisplayed(ShowMoreDetailsLinkLocator);

        /// <summary>
        /// Gets Title of Show button
        /// </summary>
        /// <returns></returns>
        public string GetShowButtonTitle() => DriverExtensions.GetText(ShowMoreDetailsLinkLocator);

        /// <summary>
        /// Click on element by locator and wait
        /// </summary>
        /// <typeparam name="T">page Type</typeparam>
        /// <param name="locatorBy">locator</param>
        /// <returns>New instance of T page</returns>
        private T ClickElementAndWait<T>(By locatorBy) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(locatorBy).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}