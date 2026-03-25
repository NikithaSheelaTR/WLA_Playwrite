namespace Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent
{
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Raw.WestlawEdge.Items.QuestionAnswerItem;
    using Framework.Common.UI.Raw.WestlawEdge.Models.QuestionAnswer;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The question-answer result list component for Indigo
    /// </summary>
    public class EdgeSearchQuestionAnswerResultListComponent : BaseModuleRegressionComponent
    {
        private static readonly By AnswerLocator = By.XPath(".//div[contains(@class,'co_searchResults_questionAndAnswer Cases-tab')]");

        private static readonly By AnswerCheckboxLocator = By.XPath(".//input[contains(@class,'co_linkCheckbox_checkbox')]");

        private static readonly By ContainerLocator = By.XPath("//*[contains(@class,'co_pftLegalStatus')]");

        private static readonly By ContainerBodyLocator = By.XPath("//*[contains(@class,'co_pftLegalStatus')] | //*[contains(@class,'co_documentReportBodyhasPadding')]");

        private const string AnswerLinkLocator = "//*[@id='coid_website_questionAndAnswer']//div[@id='Cases-tab'][{0}]//span[@id='ellipsis-expand']/a";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Component Body locator
        /// </summary>
        protected By ComponentBodyLocator => ContainerBodyLocator;

        /// <summary>
        /// Gets answers count
        /// </summary>
        /// <returns></returns>
        public virtual int GetAnswersCount() => DriverExtensions.GetElements(this.ComponentLocator, AnswerLocator).Count;

        /// <summary>
        /// Gets body answers count
        /// </summary>
        /// <returns></returns>
        public int GetBodyAnswersCount() => DriverExtensions.GetElements(this.ComponentBodyLocator, AnswerLocator).Count;

        /// <summary>
        /// Clicks answers feedback "Yes" button by index
        /// </summary>
        /// <param name="index"></param>
        public virtual void ClickFeedbackYesButtonByIndex(int index) =>
            this.GetEdgeQuestionAnswerResultListItemByIndex(index).ClickFeedbackYesButton();

        /// <summary>
        /// Clicks answers feedback "Yes" body button by index
        /// </summary>
        /// <param name="index"></param>
        public void ClickFeedbackYesBodyButtonByIndex(int index) =>
            this.GetEdgeQuestionAnswerBodyResultListItemByIndex(index).ClickFeedbackYesButton();

        /// <summary>
        /// Hides answers associated content by index
        /// </summary>
        /// <param name="index"></param>
        public virtual void ClickHideAssociatedContentByIndex(int index) =>
            this.GetEdgeQuestionAnswerResultListItemByIndex(index).ClickHideAssociatedContent();

        /// <summary>
        /// The get question-answer result list item model by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItemModel"/>.
        /// </returns>
        public virtual EdgeQuestionAnswerResultListItemModel GetAnswerModel(int index) =>
            this.GetEdgeQuestionAnswerResultListItemByIndex(index).ToModel<EdgeQuestionAnswerResultListItemModel>();

        /// <summary>
        /// The get question-answer result list body item model by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItemModel"/>.
        /// </returns>
        public EdgeQuestionAnswerResultListItemModel GetBodyAnswerModel(int index) =>
            this.GetEdgeQuestionAnswerBodyResultListItemByIndex(index).ToModel<EdgeQuestionAnswerResultListItemModel>();

        /// <summary>
        /// Select answer by index
        /// </summary>
        /// <param name="index"></param>
        public void SelectAnswer(int index) =>
            DriverExtensions.GetElements(this.ComponentLocator, AnswerLocator, AnswerCheckboxLocator).ElementAt(index)?.Click();

        /// <summary>
        /// Select body answer by index
        /// </summary>
        /// <param name="index"></param>
        public void SelectBodyAnswer(int index) =>
            DriverExtensions.GetElements(this.ComponentBodyLocator, AnswerLocator, AnswerCheckboxLocator).ElementAt(index)?.Click();

        /// <summary>
        /// The get indigo question-answer result list item by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItem"/>.
        /// </returns>
        public EdgeQuestionAnswerResultListItem GetEdgeQuestionAnswerResultListItemByIndex(int index) =>
            new EdgeQuestionAnswerResultListItem(
                DriverExtensions.GetElements(this.ComponentLocator, AnswerLocator).ElementAt(index));

        /// <summary>
        /// The get indigo question-answer result list item by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItem"/>.
        /// </returns>
        public EdgeQuestionAnswerResultListItem GetEdgeQuestionAnswerBodyResultListItemByIndex(int index) =>
            new EdgeQuestionAnswerResultListItem(
                DriverExtensions.GetElements(this.ComponentBodyLocator, AnswerLocator).ElementAt(index));

        /// <summary>
        /// Click an answer link on page
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="answerIndex">The col number.</param>
        /// <returns>New instance of T page</returns>
        public virtual T ClickAnswerLinkByIndex<T>(int answerIndex = 0) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.XPath(string.Format(AnswerLinkLocator, answerIndex))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}