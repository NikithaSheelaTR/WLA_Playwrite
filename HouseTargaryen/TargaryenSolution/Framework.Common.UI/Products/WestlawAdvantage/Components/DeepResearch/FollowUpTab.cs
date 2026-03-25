namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Follow up Tab
    /// </summary>
    public class FollowUpTab : BaseBrowseTabPanelComponent
    {
        private static readonly By FollowUpFeedbackContainerLocator = By.XPath("//div[contains(@class, 'FollowUpAnswerMessage-module__feedback')]");
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-followuptab']");
        private static readonly By FollowUpProgressBarLabelLocator = By.XPath(".//saf-progress-v3[@data-testid='follow-up-progress-bar']");
        private static readonly By FollowUpMessageLabelLocator = By.XPath(".//div[@data-testid='follow-up-welcome-message']");
        private static readonly By FollowUpQuestionTextareaLocator = By.XPath(".//saf-text-area-v3[@data-testid='input-textarea']");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('textarea[part=control]'));";
        private static readonly By FollowUpAnswerLabelLocator = By.XPath(".//div[contains(@class, 'followUpAnswerMessage')]");
        private static readonly By AlertMessageLabelLocator = By.XPath(".//saf-alert-v3[@data-testid='next-action-error-alert']/p");

        /// <summary>
        /// Get the feedback component
        /// </summary>
        public AiDeepResearchFeedbackComponent Feedback { get; } = new AiDeepResearchFeedbackComponent(FollowUpFeedbackContainerLocator);

        /// <summary>
        /// Follow up Message label
        /// </summary>
        public ILabel FollowUpMessageLabel => new Label(this.ComponentLocator, FollowUpMessageLabelLocator);

        /// <summary>
        /// Follow up progress bar label
        /// </summary>
        public ILabel FollowUpProgressBarLabel => new Label(this.ComponentLocator, FollowUpProgressBarLabelLocator);

        /// <summary>
        /// Follow up Answer Label
        /// </summary>
        public ILabel FollowUpAnswerLabel => new Label(this.ComponentLocator, FollowUpAnswerLabelLocator);

        /// <summary>
        /// Alert Message Label
        /// </summary>
        public ILabel AlertMessageLabel => new Label(this.ComponentLocator, AlertMessageLabelLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Follow up";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;

        /// <summary>
        /// Enter question in the follow up TextArea
        /// </summary>
        /// <param name="followUpQuestion">followup question</param>
        public void EnterFollowUpQuestion(string followUpQuestion)
        {
            IWebElement followupQuestionTextArea = DriverExtensions.GetElement(ComponentLocator, FollowUpQuestionTextareaLocator);
            IWebElement inputArea = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, followupQuestionTextArea);
            inputArea.SendKeys(followUpQuestion);
        }
    }
}


