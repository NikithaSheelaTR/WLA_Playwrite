namespace Framework.Common.UI.Raw.WestlawEdge.Items.QuestionAnswerItem
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The question-answer result list item class for Indigo
    /// </summary>
    public class EdgeQuestionAnswerResultListItem : BaseItem
    {
        private static readonly By YesButtonLocator = By.ClassName("co_feedbackYes");
        private static readonly By AssociatedContentHideButtonLocator = By.XPath(".//a[@class='co_statutoryCitation_toggleButton']/span[text()='Hide associated content']");
        private static readonly By AnswerContentContainerLocator = By.XPath(".//*[@id='answerText']");
        private static readonly By FeedbackLocator = By.XPath(".//div[@class='yesNoFeedbackWidget']");
        private static readonly By NoButtonLocator = By.ClassName("co_feedbackNo");
        private static readonly By FeedbackQuestionLocator = By.XPath(".//div[contains(@class,'co_yesNoFeedbackDiv')]");
        private static readonly By ThankYouForFeedbackLocator = By.Id("co_idFeedbackThankyou");
        private static readonly By AssociatedContentShowButtonLocator = By.XPath(".//a[@class='co_statutoryCitation_toggleButton']/span[text()='Show associated content']");

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeQuestionAnswerResultListItem"/> class. 
        /// Question-Answer result list constructor
        /// </summary>
        /// <param name="container">The container.</param>
        public EdgeQuestionAnswerResultListItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// 'Hide associated content' button
        /// </summary>
        public IButton ShowAssociatedContent => new Button(this.Container, AssociatedContentShowButtonLocator);

        /// <summary>
        /// Feedback "No" button
        /// </summary>
        public IButton FeedbackNoButton => new Button(this.Container, NoButtonLocator);

        /// <summary>
        /// Verify that associated content container is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAssociatedContentDisplayed() => DriverExtensions.IsDisplayed(this.Container, AnswerContentContainerLocator);

        /// <summary>
        /// Verify that "Yes" button for feedback is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFeedbackYesButtonDisplayed() => DriverExtensions.IsDisplayed(this.Container, FeedbackLocator, YesButtonLocator);

        /// <summary>
        /// Verify that "No" button for feedback is displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFeedbackNoButtonDisplayed() => DriverExtensions.IsDisplayed(this.Container, FeedbackLocator, NoButtonLocator);

        /// <summary>
        /// Verify that Was this answer helpful? is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsFeedbackQuestionDisplayed()
        {
            IWebElement element = DriverExtensions.SafeGetElement(this.Container, FeedbackQuestionLocator);
            return element != null && element.GetText().Equals("Helpful?");
        }

        /// <summary>
        /// Verify that Thank you for your feedback is displayed
        /// </summary>
        /// <returns></returns>
        public bool IsFeedbackThankYouDisplayed()
        {
            IWebElement element = DriverExtensions.SafeGetElement(this.Container, ThankYouForFeedbackLocator);
            return element != null && element.GetText().Equals("Thank you.");
        }

        /// <summary>
        /// Click feedback "Yes" button
        /// </summary>
        public void ClickFeedbackYesButton() => DriverExtensions.Click(this.Container, YesButtonLocator);

        /// <summary>
        /// Click 'Hide associated content' button
        /// </summary>
        public void ClickHideAssociatedContent() =>
            DriverExtensions.WaitForElement(this.Container, AssociatedContentHideButtonLocator).Click();
    }
}