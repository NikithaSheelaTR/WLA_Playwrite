namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Chat component
    /// </summary>
    public class ChatComponent : BaseModuleRegressionComponent
    {
        private static readonly By ChatContainerLocator = By.XPath(".//div[@class='CS-main']");
        private static readonly By ChatSummaryLabelLocator = By.XPath(".//*[@class='CS-ai-summary-box-content']");
        private static readonly By LandingPageLabelLocator = By.XPath(".//*[@class='CS-landing-page']");
        private static readonly By LandingPageContentLabelLocator = By.XPath(".//*[@class='CS-landing-page-content']");
        private static readonly By TryLinkLocator = By.XPath(".//*[@class='AAR-features-link']//a");
        private static readonly By AiJurisdictionalSurveysLinkLocator = By.XPath(".//a[@href and contains(text(), 'AI Jurisdictional Surveys')]");
        private static readonly By HowAiWorksLinkLocator = By.XPath(".//button[contains(@class,'Conversational-search-formWrapper-infoButton') and contains(text(), 'how the AI works')]");
        private static readonly By TipsBestResultLinkLocator = By.XPath(".//button[contains(@class,'Conversational-search-formWrapper-infoButton') and contains(text(), 'tips for best results')]");
        private static readonly By QuestionAndAnswerLocator = By.XPath(".//div[contains(@class, 'saf-accordion__item') and not(contains(@class, 'icon'))]");
        private static readonly By ReadOnlyMessageLabelLocator = By.XPath(".//*[@class='CS-toast-message-container']");
        private static readonly By EmailMeButtonLocator = By.XPath(".//*[contains(@class, 'CS-email-me-button')]");
        private static readonly By DisclaimerInformationLocator = By.XPath(".//p[@class='claimsExplorerDisclaimer']");
        private static readonly By AdditionalSummariesDisclaimerLabelLocator = By.XPath(".//*[@class='AAR-additionalSearchSummaries']/following-sibling::p");
        private static readonly By ContinueUsingARRButtonLocator = By.XPath(".//button[contains(@class,'saf-button saf-button_secondary') and contains(text(), 'Continue using AI-Assisted Research')]");
        private static readonly By AiDeepResearchLinkLocator = By.XPath(".//a[@href and contains(text(), 'AI Deep Research')]");

        /// <summary>
        /// How Ai works link
        /// </summary>
        public ILink HowAiWorksLink => new Link(this.ComponentLocator, HowAiWorksLinkLocator);

        /// <summary>
        /// Try 'AAR' or 'Claims' linkout
        /// </summary>
        public ILink TryLink => new Link(this.ComponentLocator, TryLinkLocator);

        /// <summary>
        /// Ai Jurisdictional Surveys link
        /// </summary>
        public ILink AiJurisdictionalSurveysLink => new Link(this.ComponentLocator, AiJurisdictionalSurveysLinkLocator);

        /// <summary>
        /// Ai Deep Research Link
        /// </summary>
        public ILink AiDeepResearchLink => new Link(this.ComponentLocator, AiDeepResearchLinkLocator);

        /// <summary>
        /// Read-only message label
        /// </summary>
        public ILabel ReadOnlyMessageLabel => new Label(this.ComponentLocator, ReadOnlyMessageLabelLocator);

        /// <summary>
        /// Landing page label
        /// </summary>
        public ILabel LandingPageLabel => new Label(this.ComponentLocator, LandingPageLabelLocator);

        /// <summary>
        /// Landing page label content
        /// </summary>
        public ILabel LandingPageContentLabel => new Label(this.ComponentLocator, LandingPageContentLabelLocator);

        /// <summary>
        /// Chat summary label
        /// </summary>
        public ILabel ChatSummaryLabel => new Label(this.ComponentLocator, ChatSummaryLabelLocator);

        /// <summary>
        /// Disclaimer Information label
        /// </summary>
        public ILabel DisclaimerInformationLabel => new Label(this.ComponentLocator, DisclaimerInformationLocator);

        /// <summary>
        /// Additional Summaries Disclaimer Label
        /// </summary>
        public ILabel AdditionalSummariesDisclaimerLabel => new Label(this.ComponentLocator, AdditionalSummariesDisclaimerLabelLocator);

        /// <summary>
        /// AI-Assisted Research Question and Answer items list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public ItemsCollection<AiAssistedResearchQuestionAndAnswerItem> AiResearchQuestionAndAnswerItems => new ItemsCollection<AiAssistedResearchQuestionAndAnswerItem>(this.ComponentLocator, QuestionAndAnswerLocator);

        /// <summary>
        /// Claims Explorer Question and Answer items list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public AiClaimsExplorerQuestionAndAnswerItem ClaimsExplorerQuestionAndAnswerItem => new AiClaimsExplorerQuestionAndAnswerItem(DriverExtensions.GetElement(this.ComponentLocator));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ChatContainerLocator;


        /// <summary>
        /// Tips for best results link
        /// </summary>
        public ILink TipsForBestResultLink => new Link(this.ComponentLocator, TipsBestResultLinkLocator);

        /// <summary>
        /// 'Email me' button
        /// </summary>
        public IButton EmailMeButton => new Button(this.ComponentLocator, EmailMeButtonLocator);

        /// <summary>
        /// Continue Using ARR Button
        /// </summary>
        public IButton ContinueUsingARRButton => new Button(this.ComponentLocator, ContinueUsingARRButtonLocator);
    }
}
