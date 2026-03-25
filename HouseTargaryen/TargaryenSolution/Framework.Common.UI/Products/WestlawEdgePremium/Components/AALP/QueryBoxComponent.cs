namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawEdgePremium.DropDowns.AALP;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Query box component
    /// </summary>
    public class QueryBoxComponent : BaseModuleRegressionComponent
    {        
        private static readonly By JurisdictionLabelLocator = By.XPath(".//span[@class='AALP-settings-jurisdiction-label']");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//button[@class='Jurisdiction-selector-button'] | .//div[@class='Jurisdiction-selector-wrapper']//button[contains(@class, 'Conversational-search')]");
      //  private static readonly By JurisdictionInfoBoxMessageLocator = By.XPath(".//*[@id='saf-tooltip']");
        private static readonly By JurisdictionInfoIconButtonLocator = By.XPath(".//*[contains(@class, 'CS-jurisdiction-info')]");
        private static readonly By QueryBoxContainerLocator = By.XPath("//div[@class='CS-chat'] | //div[contains(@class, 'AALP-question-limit-wrapper')]");
        private static readonly By QuestionLimitLabelLocator = By.XPath(".//div[@class='AALP-question-limit']/p");
        private static readonly By QuestionTextboxLocator = By.XPath(".//*[@id='questionId']");
        private static readonly By RemainingQuestionsLabelLocator = By.XPath(".//p[@class='CS-chat-question-limit-warning']");
        private static readonly By SendQuestionButtonLocator = By.XPath(".//button[@class='CS-chat-button']");
        private static readonly By NewResearchButtonLocator = By.XPath(".//button[@class='co_primaryBtn AALP-start-new-button']");
        private static readonly By TitleLabelLocator = By.XPath(".//*[@class='CS-chat-label']");
        private static readonly By QueryLimitWarningLabelLocator = By.XPath(".//*[@id='CS-chat-character-limit-warning']//div[@class='saf-alert__content']");
        private static readonly By ConcurentSearchesLimitInfoboxLocator = By.XPath(".//*[contains(@class, 'saf-alert_warning')]");
        private static readonly By ChatCharLimitWarningLabelLocator = By.XPath("//*[@id='CS-chat-character-limit-warning']/div/div");
        private static readonly By StartNewResearchButtonLocator = By.XPath("//*[@class='CS-chat-wrapper']/div/button");
        private static readonly By JurisdictionInfoBoxMessageLocator = By.XPath(".//*[contains(@id,'popover-panel') and not(contains(@class, 'co_hideState'))]");

        /// <summary>
        /// Recent questions dropdown
        /// </summary>
        public RecentQuestionsDropdown RecentQuestionsDropdown => new RecentQuestionsDropdown();      

        /// <summary>
        /// New research button
        /// </summary>
        public IButton NewResearchButton => new Button(this.ComponentLocator, NewResearchButtonLocator);

        /// <summary>
        /// Send question button
        /// </summary>
        public IButton SendQuestionButton => new Button(this.ComponentLocator, SendQuestionButtonLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox QuestionTextbox => new Textbox(this.ComponentLocator, QuestionTextboxLocator);

        /// <summary>
        /// Textbox title label
        /// </summary>
        public ILabel TitleLabel => new Label(this.ComponentLocator, TitleLabelLocator);

        /// <summary>
        /// Questions limit label
        /// </summary>
        public ILabel QuestionLimitLabel => new Label(this.ComponentLocator, QuestionLimitLabelLocator);

        /// <summary>
        /// Remaining questions warning label
        /// </summary>
        public ILabel RemainingQuestionsLabel => new Label(this.ComponentLocator, RemainingQuestionsLabelLocator);

        /// <summary>
        /// Query limit warning label
        /// </summary>
        public ILabel QueryLimitWarningLabel => new Label(this.ComponentLocator, QueryLimitWarningLabelLocator);

        /// <summary>
        /// Jurisdiction label
        /// </summary>
        public ILabel JurisdictionLabel => new Label(this.ComponentLocator, JurisdictionLabelLocator);

        /// <summary>
        /// Jurisdiction Info icon button
        /// </summary>
        public IButton JurisdictionInfoIconButton => new Button(this.ComponentLocator, JurisdictionInfoIconButtonLocator);

        /// <summary>
        ///  Jurisdiction info icon infobox
        /// </summary>
        public IInfoBox JurisdictionInfoBox => new InfoBox(DriverExtensions.GetElement(JurisdictionInfoBoxMessageLocator));

        /// <summary>
        /// Concurrent searches limit infobox
        /// </summary>
        public IInfoBox ConcurrentSearchesLimitInfobox => new InfoBox(DriverExtensions.GetElement(this.ComponentLocator, ConcurentSearchesLimitInfoboxLocator));

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => QueryBoxContainerLocator;

        /// <summary>
        /// Chat Character limit label
        /// </summary>
        public ILabel CharacterLimitLabel => new Label(this.ComponentLocator, ChatCharLimitWarningLabelLocator);

        /// <summary>
        /// Start New Research button
        /// </summary>
        public IButton StartNewResearchButton => new Button(this.ComponentLocator, StartNewResearchButtonLocator);       
    
    }
}
