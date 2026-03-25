namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements;
    using System.Collections.Generic;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// AI-Assisted Research Question and Answer item
    /// </summary>
    public class AiAssistedResearchQuestionAndAnswerItem : BaseItem
    {
        private static readonly By UserQuestionLocator = By.XPath(".//span[contains(@class, 'item-heading-content')]");
        private static readonly By HelpfulNoButtonLocator = By.XPath(".//button[@class='CS-feedback-prompt-button' and contains(text(), 'No')]");
        private static readonly By HelpfulYesButtonLocator = By.XPath(".//button[@class='CS-feedback-prompt-button' and contains(text(), 'Yes')]");
        private static readonly By QuestionAndAnswerExpandButtonLocator = By.XPath(".//button[contains(@id, 'saf-accordion__control') and @aria-expanded='false']");
        private static readonly By QuestionAndAnswerCollapseButtonLocator = By.XPath(".//button[contains(@id, 'saf-accordion__control') and @aria-expanded='true']");
        private static readonly By EmailMeButtonLocator = By.XPath(".//*[contains(@class, 'CS-email-me-button')]");
        private static readonly By EmailMeMessageLabelLocator = By.XPath(".//*[contains(@class, 'CS-email-me-container')]");
        private static readonly By JurisdictionResolverLabelLocator = By.XPath(".//*[@class='CS-ai-summary-juris-resolution']");
        private static readonly By AnswerLabelLocator = By.XPath(".//*[@class='CS-ai-summary-box-content']");
        private static readonly By JumpLinkLocator = By.XPath(".//a[contains(@href, 'qa')]");
        private static readonly By InlineTitlesLinkLocator = By.XPath(".//*[not(contains(@class, 'AAR-additionalSearchSummaries-link'))]/a[contains(@href, 'Document') and not(contains(@class, 'CS-ai-inlineCitations-cite'))]");
        private static readonly By InlineTitlesKeyCiteFlagLocator = By.XPath(".//a[contains(@href, 'RelatedInformation')]");
        private static readonly By ErrorAnswerLabelLocator = By.XPath(".//span[@id='QAErrorContent']");
        private static readonly By OutOfScopeMessageLabelLocator = By.XPath(".//p[@class='CS-ai-summary-box-content']");
        private static readonly By SkillsDetectionLabelLocator = By.XPath("//p[@class='AAR-intentResolver-link']");
        private static readonly By TipsLinkLocator = By.XPath(".//button[@class='Conversational-search-formWrapper-infoButton']");
        private static readonly By TryClaimsExplorerLinkLocator = By.XPath(".//a[@href and contains(text(), 'Claims Explorer')]");
        private static readonly By AiJurisdictionalSurveysLinkLocator = By.XPath(".//a[@href and contains(text(), 'AI Jurisdictional Surveys')]");
        private static readonly By ClaimsExplorerLinkLocator = By.XPath(".//a[@href and contains(text(), 'Claims Explorer')]");
        private static readonly By ProgressDotsLabelLocator = By.XPath(".//*[@class='AALP-progress-dots']");
        private static readonly By ConversationDateLabelLocator = By.XPath(".//*[@class='CS-metadata']");
        private static readonly By EditButtonLocator = By.XPath(".//button[contains(@class,'saf-button_secondary') and text()='Edit question']");
        private static readonly By ResubmitButtonLocator = By.XPath(".//button[contains(@class,'saf-button_secondary') and text()='Resubmit question']");
        private static readonly By TooltipLabelLocator = By.XPath(".//*[contains(@class, 'Athens-browseBox-horizontalList')]");
        private static readonly By ContinueUsingAiAssisitedResearchButtonLocator = By.XPath(".//button[contains(@class,'saf-button_secondary') and text()='Continue using AI-Assisted Research']");
        private static readonly By AlertLoadingProcessLabelLocator = By.XPath("//div[@class='AALP-progress-dots']//..//p[contains(text(),'Loading your response')]");
        private static readonly By ShowResultBtnLocator = By.XPath("//button[contains(@class,'CS-act-show-supporting-materials')]");
        private static readonly By QuestionOutOfScopeMessageLabelLocator = By.XPath("//span[@id='QAErrorContent' and contains(text(), 'Your question appears to be outside of the scope of this feature')]");
       
        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UserQuestionItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public AiAssistedResearchQuestionAndAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// Is QandA component expanded: true - yes, false - otherwise.
        /// </summary>
        public bool IsExpanded => this.Container.GetAttribute("class").Contains("current");

        /// <summary>
        /// User feedback component
        /// </summary>
        public FeedbackFormComponent FeedbackForm => new FeedbackFormComponent(this.Container);

        /// <summary>
        /// Supporting Materials component
        /// </summary>
        public SupportingMaterialsComponent SupportingMaterials => new SupportingMaterialsComponent(this.Container);

        /// <summary>
        /// 'Try Claims Explorer' link
        /// </summary>
        public ILink TryClaimsExplorerLink => new Link(this.Container, TryClaimsExplorerLinkLocator);

        /// <summary>
        /// Ai Jurisdictional Surveys link
        /// </summary>
        public ILink AiJurisdictionalSurveysLink => new Link(this.Container, AiJurisdictionalSurveysLinkLocator);

        /// <summary>
        /// Claims Explorer link
        /// </summary>
        public ILink ClaimsExplorerLink => new Link(this.Container, ClaimsExplorerLinkLocator);

        /// <summary>
        /// Conversation's date label
        /// </summary>
        public ILabel ConversationDateLabel => new Label(this.Container, ConversationDateLabelLocator);

        /// <summary>
        /// Progress dots label
        /// </summary>
        public ILabel ProgressDotsLabel => new Label(this.Container, ProgressDotsLabelLocator);

        /// <summary>
        /// Alert Loading Process label
        /// </summary>
        public ILabel AlertLoadingProcessLabel => new Label(this.Container, AlertLoadingProcessLabelLocator);

        /// <summary>
        /// Error answer label
        /// </summary>
        public ILabel ErrorAnswerLabel => new Label(this.Container, ErrorAnswerLabelLocator);

        /// <summary>
        /// Out of scope message label
        /// </summary>
        public ILabel OutOfScopeMessageLabel => new Label(this.Container, OutOfScopeMessageLabelLocator);

        /// <summary>
        /// Skills detection label
        /// </summary>
        public ILabel SkillsDetectionLabel => new Label(this.Container, SkillsDetectionLabelLocator);

        /// <summary>
        /// Tips link in out of scope message
        /// </summary>
        public ILink TipsLink => new Link(this.Container, TipsLinkLocator);

        /// <summary>
        /// Question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.Container, UserQuestionLocator);

        /// <summary>
        /// Jurisdiction resolver label
        /// </summary>
        public ILabel JurisdictionResolverLabel => new Label(this.Container, JurisdictionResolverLabelLocator);

        /// <summary>
        /// Answer label
        /// </summary>
        public ILabel AnswerLabel => new Label(this.Container, AnswerLabelLocator);

        /// <summary>
        /// 'Email me' message label
        /// </summary>
        public ILabel EmailMeMessageLabel => new Label(this.Container, EmailMeMessageLabelLocator);

        /// <summary>
        /// Tooltip label
        /// </summary>
        public ILabel TooltipLabel => new Label(this.Container, TooltipLabelLocator);

        /// <summary>
        /// QandA expand button
        /// </summary>
        public IButton ExpandButton => new Button(this.Container, QuestionAndAnswerExpandButtonLocator);

        /// <summary>
        /// QandA collapse button
        /// </summary>
        public IButton CollapseButton => new Button(this.Container, QuestionAndAnswerCollapseButtonLocator);

        /// <summary>
        /// Helpful Feedback 'Yes' button
        /// </summary>
        public IButton HelpfulYesButton => new Button(this.Container, HelpfulYesButtonLocator);

        /// <summary>
        /// Helpful Feedback 'No' button
        /// </summary>
        public IButton HelpfulNoButton => new Button(this.Container, HelpfulNoButtonLocator);

        /// <summary>
        /// 'Email me' button
        /// </summary>
        public IButton EmailMeButton => new Button(this.Container, EmailMeButtonLocator);

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.Container, EditButtonLocator);

        /// <summary>
        /// Resubmit button
        /// </summary>
        public IButton ResubmitButton => new Button(this.Container, ResubmitButtonLocator);

        /// <summary>
        /// Continue using AI-Assisted Research button
        /// </summary>
        public IButton ContinueUsingAiAssisitedResearchButton => new Button(this.Container, ContinueUsingAiAssisitedResearchButtonLocator);

        /// <summary>
        /// Jump links
        /// </summary>
        public IReadOnlyCollection<ILink> JumpLinks => new ElementsCollection<Link>(this.Container, AnswerLabelLocator, JumpLinkLocator);

        /// <summary>
        /// Inline titles links
        /// </summary>
        public IReadOnlyCollection<ILink> InlineTitlesLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerLabelLocator, InlineTitlesLinkLocator));

        /// <summary>
        /// Inline titles key cite flags links
        /// </summary>
        public IReadOnlyCollection<ILink> InlineTitlesKeyCiteFlagsLinks => new ElementsCollection<Link>(this.Container, new ByChained(AnswerLabelLocator, InlineTitlesKeyCiteFlagLocator));

        /// <summary>
        /// Show more results button
        /// </summary>
        public IButton ShowResultsButton => new Button(this.Container, ShowResultBtnLocator);

        /// <summary>
        /// Question out of scope message label
        /// </summary>
        public ILabel QuestionOutOfScopeMessageLabel => new Label(QuestionOutOfScopeMessageLabelLocator);
    }
}