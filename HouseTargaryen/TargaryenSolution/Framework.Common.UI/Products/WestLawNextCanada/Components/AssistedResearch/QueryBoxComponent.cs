namespace Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Query box component
    /// </summary>
    public class QueryBoxComponent : BaseModuleRegressionComponent
    {
        private static readonly By QueryBoxContainerLocator = By.XPath("//div[@class='CS-chat'] | //div[contains(@class, 'AALP-question-limit-wrapper')]");
        private static readonly By TitleLabelLocator = By.XPath(".//*[@class='CS-chat-label']");
        private static readonly By QuestionTextboxLocator = By.XPath(".//*[@id='questionId']");
        private static readonly By SendQuestionButtonLocator = By.XPath(".//button[@class='CS-chat-button']");
        private static readonly By QuestionLimitLabelLocator = By.XPath(".//div[@class='AALP-question-limit']/p");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => QueryBoxContainerLocator;

        /// <summary>
        /// Textbox title label
        /// </summary>
        public ILabel TitleLabel => new Label(this.ComponentLocator, TitleLabelLocator);

        /// <summary>
        /// Question textbox
        /// </summary>
        public ITextbox QuestionTextbox => new Textbox(this.ComponentLocator, QuestionTextboxLocator);

        /// <summary>
        /// Send question button
        /// </summary>
        public IButton SendQuestionButton => new Button(this.ComponentLocator, SendQuestionButtonLocator);

        /// <summary>
        /// Questions limit label
        /// </summary>
        public ILabel QuestionLimitLabel => new Label(this.ComponentLocator, QuestionLimitLabelLocator);
    }
}
