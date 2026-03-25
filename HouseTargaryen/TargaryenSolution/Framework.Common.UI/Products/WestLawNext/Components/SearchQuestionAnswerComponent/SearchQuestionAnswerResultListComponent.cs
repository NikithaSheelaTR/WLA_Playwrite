namespace Framework.Common.UI.Products.WestLawNext.Components.SearchQuestionAnswerComponent
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.WestLawNext.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SearchQuestionAnswerResultListComponent
    /// </summary>
    public class SearchQuestionAnswerResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By AnswerLocator = By.ClassName("co_searchResults_questionAndAnswer");
        private static readonly By AnswerCheckboxLocator = By.XPath("//input[contains(@class,'co_linkCheckbox_checkbox')]");
        private static readonly By ContainerLocator = By.Id("coid_website_questionAndAnswer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Returns a document title by index
        /// </summary>
        /// <param name="index">index</param>
        /// <returns>The List of document titles.</returns>
        public string GetDocumentTitleByIndex(int index) =>
            this.GetQuestionAnswerResultListItemByIndex(index).TitleText;

        /// <summary>
        /// Gets answers count
        /// </summary>
        /// <returns></returns>
        public int GetAnswersCount() => DriverExtensions.GetElements(AnswerLocator).Count;

        /// <summary>
        /// Hides answers associated content by index
        /// </summary>
        /// <param name="index"></param>
        public void ClickHideAssociatedContentByIndex(int index) =>
            this.GetQuestionAnswerResultListItemByIndex(index).ClickHideAssociatedContent();

        /// <summary>
        /// Clicks on the document title by index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <returns></returns>
        public T ClickTitleLinkByIndex<T>(int index)
            where T : ICreatablePageObject => this.GetQuestionAnswerResultListItemByIndex(index).ClickTitleLink<T>();

        /// <summary>
        /// Select answer by index
        /// </summary>
        /// <param name="index"></param>
        public void SelectAnswer(int index) =>
            DriverExtensions.GetElements(AnswerLocator, AnswerCheckboxLocator).ElementAt(index)?.Click();

        /// <summary>
        /// The get indigo question-answer result list item by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="QuestionAnswerResultListItem"/>.
        /// </returns>
        public QuestionAnswerResultListItem GetQuestionAnswerResultListItemByIndex(int index) =>
            new QuestionAnswerResultListItem(
                DriverExtensions.GetElements(AnswerLocator).ElementAt(index));
    }
}