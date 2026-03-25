namespace Framework.Common.UI.Products.WestlawAdvCanada.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
    using OpenQA.Selenium;

    /// <summary>
    /// Westlaw Advantage Canada Deep Research Result Component
    /// </summary>
    public class DeepResearchLeftResultComponent : AiDeepResearchLeftColumnComponent
    {
        private static readonly By QuestionLabelLocator = By.XPath("//saf-message-box-v3[contains(@id,'message') and contains(@id,'Question')]//div[contains(@class, 'Message-module__messageContent')]/div");
        private static readonly By ResearchContentsLabelLocator = By.XPath("//saf-disclosure-v3[@summary='Research contents' or @summary='Contenu de la recherche']");
        private static readonly By ResearchStepsLabelLocator = By.XPath("//saf-disclosure-v3[@summary='Research steps' or @summary='Étapes de la recherche']");
        private static readonly By ResearchTipsLabelLocator = By.XPath("//div[@data-testid='tips-container']");
        private static readonly By FollowUpQuestionTextBoxLocator = By.Id("deep-research-input");

        /// <summary>
        /// Question Label
        /// </summary>
        public ILabel QuestionLabel = new Label(QuestionLabelLocator);

        /// <summary>
        /// Research contents label in the report
        /// </summary>
        public ILabel ResearchContentsLabel = new Label(ResearchContentsLabelLocator);

        /// <summary>
        /// Research steps label in the report
        /// </summary>
        public ILabel ResearchStepsLabel = new Label(ResearchStepsLabelLocator);

        /// <summary>
        /// Research tips label in the report
        /// </summary>
        public ILabel ResearchTipsLabel = new Label(ResearchTipsLabelLocator);

        /// <summary>
        /// Follow up question text box
        /// </summary>
        public ITextbox FollowUpQuestionTextbox = new Textbox(FollowUpQuestionTextBoxLocator);
    }
}