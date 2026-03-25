namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    /// <summary>
    /// AI-Assisted Research tab panel
    /// </summary>
    public class AiAssistedResearchTabPanel : BaseBrowseTabPanelComponent
    {
        private static readonly By ContainerLocator = By.XPath("//*[@id='panel1']");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//span[@id='jurisdictionIdInner_AIAssistantHomePageId']");
        private static readonly By LearnMoreLinkLocator = By.XPath(".//*[@class='Conversational-search-formWrapper-infoButton' and contains(text(), 'Read')]");
        private static readonly By TryClaimsExplorerLinkLocator = By.XPath(".//a[@href and contains(text(), 'Claims Explorer')]");
        private static readonly By AiJurisdictionalSurveysLinkLocator = By.XPath(".//a[@href and contains(text(), 'AI Jurisdictional Surveys')]");
        private static readonly By HowAiWorksButtonLocator = By.XPath(".//*[@class='Conversational-search-formWrapper-infoButton' and contains(text(), 'AI works')]");
        private static readonly By SubmitButtonLocator = By.XPath(".//*[contains(@class, 'saf-button_primary')]");
        private static readonly By TipsForBestResultsButtonLocator = By.XPath(".//*[@class='Conversational-search-formWrapper-infoButton' and contains(text(), 'tips')]");
        private static readonly By QuestionTextboxLocator = By.XPath(".//*[@id='saf-text-area1']");
        private static readonly By QueryLimitWarningLabelLocator = By.XPath(".//*[@id='CS-chat-character-limit-warning']//div[@class='saf-alert__content']");
        private static readonly By AARTabTitleLocator = By.XPath("//*[@id='Athens-conversation-search-container']/div/div[1]/h2 | //*[@id='Athens-conversation-search-container']/div/div[1]/div/h2");
        private static readonly By AARTabInfoLocator = By.XPath("//div[@class='Conversational-search-wrapper']");
        private static readonly By WelcomeMessageLocator = By.XPath("//h2[@class='Conversational-search-heading']");

        /// <summary>
        /// Recent questions dropdown
        /// </summary>
        public RecentQuestionsDropdown RecentQuestionsDropdown => new RecentQuestionsDropdown();

        /// <summary>
        /// Submit button
        /// </summary>
        public IButton SubmitButton => new Button(this.ComponentLocator, SubmitButtonLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Learn more link
        /// </summary>
        public ILink LearnMoreLink => new Link(this.ComponentLocator, LearnMoreLinkLocator);

        /// <summary>
        /// 'Try Claims Explorer' link
        /// </summary>
        public ILink TryClaimsExplorerLink => new Link(this.ComponentLocator, TryClaimsExplorerLinkLocator);

        /// <summary>
        /// Ai Jurisdictional Surveys link
        /// </summary>
        public ILink AiJurisdictionalSurveysLink => new Link(this.ComponentLocator, AiJurisdictionalSurveysLinkLocator);

        /// <summary>
        /// How AI works button
        /// </summary>
        public IButton HowAiWorksButton => new Button(this.ComponentLocator, HowAiWorksButtonLocator);

        /// <summary>
        /// Tips for best results button
        /// </summary>
        public IButton TipsForBestResultsButton => new Button(this.ComponentLocator, TipsForBestResultsButtonLocator);

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox QuestionTextbox => new Textbox(this.ComponentLocator, QuestionTextboxLocator);

        /// <summary>
        /// Query limit warning label
        /// </summary>
        public ILabel QueryLimitWarningLabel => new Label(ContainerLocator, QueryLimitWarningLabelLocator);

        /// <summary>
        /// Welcome Message label
        /// </summary>
        public ILabel WelcomeMessageLabel => new Label(ContainerLocator, WelcomeMessageLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "AI-Assisted Research";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the heading of the AAR tab
        /// </summary>
        /// <returns>Heading of the custom tab</returns>
        public string GetAARHeadingText()
            => DriverExtensions.GetElement(AARTabTitleLocator).GetText();

        /// <summary>
        /// Gets the text of the AAR tab
        /// </summary>
        /// <returns>Info Text of the AAR tab</returns>
        public string GetCustomTabInfoText()
            => DriverExtensions.GetElement(AARTabInfoLocator).GetText();
    }
}
