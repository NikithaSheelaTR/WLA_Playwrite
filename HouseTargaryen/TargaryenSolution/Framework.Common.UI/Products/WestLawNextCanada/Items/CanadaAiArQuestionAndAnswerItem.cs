namespace Framework.Common.UI.Products.WestLawNextCanada.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch;
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// Canada Ai Assisted Research Question And Answer Item
    /// </summary>
    public class CanadaAiArQuestionAndAnswerItem : BaseItem
    {
        private static readonly By ProgressDotsLabelLocator = By.XPath(".//*[@class='AALP-progress-dots']");
        private static readonly By QuestionLocator = By.XPath(".//span[contains(@class, 'item-heading-content')]/span[3]");
        private static readonly By SummaryContentLocator = By.XPath(".//*[@class='CS-ai-summary-box-content']");
        private static readonly By AnswerLabelLocator = By.XPath(".//*[@class='CS-ai-summary-box-content']");
        private static readonly By JumpLinkLocator = By.XPath(".//a[contains(@href, 'qa')]");
        private static readonly By TooltipLabelLocator = By.XPath("//div[contains(@class,'co_message')]//h2");
        private static readonly By ErrorAnswerLabelLocator = By.XPath(".//span[@id='QAErrorContent']");
        private static readonly By CitationTitleLocator = By.XPath("//span[@class='CS-ai-inlineCitations']/a");
        private static readonly By EmailMeMessageLabelLocator = By.XPath(".//*[contains(@class, 'CS-email-me-container')]");
        private static readonly By EmailMeButtonLocator = By.XPath(".//*[contains(@class, 'CS-email-me-button')]");
        private static readonly By HelpfulYesButtonLocator = By.XPath(".//button[@class='CS-feedback-prompt-button' and contains(text(), 'Yes')]");
        private static readonly By HelpfulNoButtonLocator = By.XPath(".//button[@class='CS-feedback-prompt-button' and contains(text(), 'No')]");
        private static readonly By JurisdictionResolverLabelLocator = By.XPath(".//*[@class='CS-ai-summary-juris-resolution']");

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="containerElement"></param>
        public CanadaAiArQuestionAndAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// User feedback component
        /// </summary>
        public CanadaFeedbackFormComponent FeedbackForm => new CanadaFeedbackFormComponent(this.Container);

        /// <summary>
        /// Progress dots label
        /// </summary>
        public ILabel ProgressDotsLabel => new Label(this.Container, ProgressDotsLabelLocator);

        /// <summary>
        /// First question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.Container, QuestionLocator);

        /// <summary>
        /// Summary Content Label 
        /// </summary>
        public ILabel SummaryContentLabel => new Label(this.Container, SummaryContentLocator);

        /// <summary>
        /// Tooltip label
        /// </summary>
        public ILabel TooltipLabel => new Label(this.Container, TooltipLabelLocator);

        /// <summary>
        /// Jump links
        /// </summary>
        public IReadOnlyCollection<ILink> JumpLinks => new ElementsCollection<Link>(this.Container, AnswerLabelLocator, JumpLinkLocator);

        /// <summary>
        /// Error answer label
        /// </summary>
        public ILabel ErrorAnswerLabel => new Label(this.Container, ErrorAnswerLabelLocator);

        /// <summary>
        /// Citation Title
        /// </summary>
        public ILabel CitationTitle => new Label(this.Container, CitationTitleLocator);

        /// <summary>
        /// 'Email me' message label
        /// </summary>
        public ILabel EmailMeMessageLabel => new Label(this.Container, EmailMeMessageLabelLocator);

        /// <summary>
        /// 'Email me' button
        /// </summary>
        public IButton EmailMeButton => new Button(this.Container, EmailMeButtonLocator);

        /// <summary>
        /// Helpful Feedback 'Yes' button
        /// </summary>
        public IButton HelpfulYesButton => new Button(this.Container, HelpfulYesButtonLocator);

        /// <summary>
        /// Helpful Feedback 'No' button
        /// </summary>
        public IButton HelpfulNoButton => new Button(this.Container, HelpfulNoButtonLocator);

        /// <summary>
        /// Citation Title
        /// </summary>
        public ILabel JurisdictionResolverLabel => new Label(this.Container, JurisdictionResolverLabelLocator);
    }
}