namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Claims Explorer Question and Answer item
    /// </summary>
    public class AiClaimsExplorerQuestionAndAnswerItem : BaseItem
    {
        private static readonly By UserQuestionLocator = By.XPath(".//span[contains(@class, 'CE-heading-content')]");
        private static readonly By DateLabelLocator = By.XPath(".//span[contains(@class, 'CS-metadata')]");
        private static readonly By ErrorAnswerLabelLocator = By.XPath(".//div[@class='CE-content-panel']//div");
        private static readonly By SummaryLabelLocator = By.XPath(".//p[@class='CS-ai-summary-box-content']");
        private static readonly By ProgressDotsLabelLocator = By.XPath(".//*[@class='AALP-progress-dots']");
        private static readonly By EditButtonLocator = By.XPath(".//button[contains(@class,'saf-button_secondary') and text()='Edit question']");
        private static readonly By ResubmitButtonLocator = By.XPath(".//button[contains(@class,'saf-button_secondary') and text()='Resubmit question']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UserQuestionItem"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public AiClaimsExplorerQuestionAndAnswerItem(IWebElement containerElement) : base(containerElement)
        {
        }

        /// <summary>
        /// AI-Suggested Claims answer tab panel
        /// </summary>
        public AiClaimsExplorerAnswerTabPanel AnswerTabPanel => new AiClaimsExplorerAnswerTabPanel(this.Container);

        /// <summary>
        /// Metadata label
        /// </summary>
        public ILabel DateLabel => new Label(this.Container, DateLabelLocator);

        /// <summary>
        /// Progress dots label
        /// </summary>
        public ILabel ProgressDotsLabel => new Label(this.Container, ProgressDotsLabelLocator);

        /// <summary>
        /// Error answer label
        /// </summary>
        public ILabel ErrorAnswerLabel => new Label(this.Container, ErrorAnswerLabelLocator);

        /// <summary>
        /// Summary label
        /// </summary>
        public ILabel SummaryLabel => new Label(this.Container, SummaryLabelLocator);

        /// <summary>
        /// Question label
        /// </summary>
        public ILabel QuestionLabel => new Label(this.Container, UserQuestionLocator);

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.Container, EditButtonLocator);

        /// <summary>
        /// Resubmit button
        /// </summary>
        public IButton ResubmitButton => new Button(this.Container, ResubmitButtonLocator);
    }
}