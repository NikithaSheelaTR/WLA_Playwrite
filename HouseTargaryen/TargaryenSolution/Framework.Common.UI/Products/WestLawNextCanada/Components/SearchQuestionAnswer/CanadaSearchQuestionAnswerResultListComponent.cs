namespace Framework.Common.UI.Products.WestLawNextCanada.Components.SearchQuestionAnswer
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent;
    using Framework.Common.UI.Raw.WestlawEdge.Items.QuestionAnswerItem;
    using Framework.Common.UI.Raw.WestlawEdge.Models.QuestionAnswer;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The question-answer result list component for Canada
    /// </summary>
    public class CanadaSearchQuestionAnswerResultListComponent : EdgeSearchQuestionAnswerResultListComponent
    {
        private const string DocumentLinkTextLocatorLctMask = "//label[@for='cobalt_answerDoument_checkbox_0_{0}']";

        private static readonly By AnswerLinkTextLocator = By.XPath(".//div[contains(@class, 'co_searchResults_questionAndAnswer')]//div[@class = 'co_goToQuote']//a");

        private static readonly By AnswerLocator = By.XPath(".//div[contains(@class,'co_searchResults_questionAndAnswer')]");

        private static readonly By ContainerLocator = By.XPath("//*[contains(@id, 'coid_website_questionAndAnswer')]");

        private static readonly By DocumentCitationLinkLocator = By.XPath("//a[@class='co_statutoryCitation_metaLink']");

        private static readonly By ShowAssociatedContentLinkLocator = By.XPath(".//a[@class='co_statutoryCitation_toggleButton']/span[text()='Show associated content']");

        private static readonly By GiveFeedbackLinkLocator = By.Id("qa_feedback");

        private static readonly By SelectedItemsLabelLocator = By.XPath(".//li[@class='co_navItemsSelected']/span");

        /// <summary>
        /// The give feedback link
        /// </summary>
        public ILink GiveFeedbackLink => new Link(this.ComponentLocator, GiveFeedbackLinkLocator);

        /// <summary>
        /// Document Citation Links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentCitationLinks => new ElementsCollection<Link>(this.ComponentLocator, DocumentCitationLinkLocator);

        /// <summary>
        /// Document Citation Show Links
        /// </summary>
        public IReadOnlyCollection<ILink> DocumentShowCitationLinks => new ElementsCollection<Link>(this.ComponentLocator, ShowAssociatedContentLinkLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets answers count
        /// </summary>
        /// <returns> The answers count</returns>
        public override int GetAnswersCount() => DriverExtensions.GetElements(this.ComponentLocator, AnswerLocator).Count;

        /// <summary>
        /// Click on document citation link on page
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="answerIndex">The col number.</param>
        /// <returns>New instance of T page</returns>
        public T ClickOnDocumentCitationLinkByIndex<T>(int answerIndex = 0) where T : ICreatablePageObject
        {
            DriverExtensions.GetElements(DocumentCitationLinkLocator).ElementAt(answerIndex).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks answers feedback "Yes" button by index
        /// </summary>
        /// <param name="index"> The index </param>
        public override void ClickFeedbackYesButtonByIndex(int index) =>
            this.GetCanadaQuestionAnswerResultListItemByIndex(index).ClickFeedbackYesButton();

        /// <summary>
        /// Clicks answers feedback "No" button by index
        /// </summary>
        /// <param name="index"> The index </param>
        public void ClickFeedbackNoButtonByIndex(int index) =>
            this.GetCanadaQuestionAnswerResultListItemByIndex(index).FeedbackNoButton.Click();

        /// <summary>
        /// Hides answers associated content by index
        /// </summary>
        /// <param name="index"> The index </param>
        public override void ClickHideAssociatedContentByIndex(int index) =>
            this.GetCanadaQuestionAnswerResultListItemByIndex(index).ClickHideAssociatedContent();

        /// <summary>
        /// Hides answers associated content by index
        /// </summary>
        /// <param name="index"> The index </param>
        public void ClickShowAssociatedContentByIndex(int index) =>
            this.GetCanadaQuestionAnswerResultListItemByIndex(index).ShowAssociatedContent.Click();

        /// <summary>
        /// Get the answer link text
        /// </summary>
        /// <param name="answerIndex"> The answer index</param>
        /// <returns> The answer link text </returns>
        public string GetAnswerLinkText(int answerIndex = 0) => DriverExtensions.GetElements(this.ComponentLocator, AnswerLinkTextLocator).ElementAt(answerIndex).Text;

        /// <summary>
        /// Click on answer link on page
        /// </summary>
        /// <typeparam name="T">page</typeparam>
        /// <param name="answerIndex">The col number.</param>
        /// <returns>New instance of T page</returns>
        public override T ClickAnswerLinkByIndex<T>(int answerIndex = 0)
        {
            DriverExtensions.GetElements(this.ComponentLocator, AnswerLinkTextLocator).ElementAt(answerIndex).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get the answer document title
        /// </summary>
        /// <param name="answerIndex"> The answer index</param>
        /// <returns> The answer document title </returns>
        public string GetAnswerDocumentTitle(int answerIndex = 0) => DriverExtensions.GetImmediateText(By.XPath(string.Format(DocumentLinkTextLocatorLctMask, answerIndex)));

        /// <summary>
        /// The get question-answer result list item model by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItemModel"/>.
        /// </returns>
        public override EdgeQuestionAnswerResultListItemModel GetAnswerModel(int index) =>
            this.GetCanadaQuestionAnswerResultListItemByIndex(index).ToModel<EdgeQuestionAnswerResultListItemModel>();

        /// <summary>
        /// The get canada question-answer result list item by index.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="EdgeQuestionAnswerResultListItem"/>.
        /// </returns>
        public EdgeQuestionAnswerResultListItem GetCanadaQuestionAnswerResultListItemByIndex(int index) =>
            new EdgeQuestionAnswerResultListItem(DriverExtensions.GetElements(this.ComponentLocator, AnswerLocator).ElementAt(index));

        /// <summary>
        /// Gets the number of items selected
        /// </summary>
        /// <returns>No. of items selected</returns>
        public int GetSelectedItemsCount()
            => DriverExtensions.GetElement(ContainerLocator, SelectedItemsLabelLocator).Text.ConvertCountToInt();
    }
}
