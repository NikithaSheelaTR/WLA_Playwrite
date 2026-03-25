namespace Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestLawNextCanada.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Chat component
    /// </summary>
    public class ChatComponent : BaseModuleRegressionComponent
    {
        private static readonly By ChatContainerLocator = By.XPath(".//div[@class='CS-main']");
        private static readonly By QuestionAndAnswerLocator = By.XPath(".//div[contains(@class, 'saf-accordion__item') and not(contains(@class, 'icon'))]");
        private static readonly By LandingPageLabelLocator = By.XPath(".//*[@class='CS-landing-page']");
        private static readonly By TipsBestResultLinkLocator = By.XPath(".//button[contains(@class,'Conversational-search-formWrapper-infoButton') and contains(text(), 'tips for best results')]");
        private static readonly By ShowResultBtnLocator = By.XPath("//button[contains(@class,'CS-act-show-supporting-materials')]");
        private static readonly By SupportingMaterialsLocator = By.XPath("//ol[contains(@class,'CS-supporting-materials-list-box')]/li");
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ChatContainerLocator;

        /// <summary>
        /// AI-Assisted Research Question and Answer items list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public ItemsCollection<CanadaAiArQuestionAndAnswerItem> AiResearchQuestionAndAnswerItems =>
            new ItemsCollection<CanadaAiArQuestionAndAnswerItem>(this.ComponentLocator, QuestionAndAnswerLocator);

        /// <summary>
        /// Supporting Materials item list
        /// </summary>
        /// <returns>List of user questions and answers</returns>
        public ItemsCollection<SupportingMaterialsItem> SupportingMaterialsItems =>
            new ItemsCollection<SupportingMaterialsItem>(this.ComponentLocator, SupportingMaterialsLocator);

        /// <summary>
        /// Landing page label
        /// </summary>
        public ILabel LandingPageLabel => new Label(this.ComponentLocator, LandingPageLabelLocator);

        /// <summary>
        /// Tips for best results link
        /// </summary>
        public ILink TipsForBestResultLink => new Link(this.ComponentLocator, TipsBestResultLinkLocator);

        /// <summary>
        /// Show more results button
        /// </summary>
        public IButton ShowResultsButton => new Button(this.ComponentLocator, ShowResultBtnLocator);
    }
}