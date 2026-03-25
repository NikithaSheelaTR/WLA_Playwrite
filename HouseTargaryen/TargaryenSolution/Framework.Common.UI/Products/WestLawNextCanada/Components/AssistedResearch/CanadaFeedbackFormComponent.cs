namespace Framework.Common.UI.Products.WestLawNextCanada.Components.AssistedResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Canada Feedback form component
    /// </summary>
    public class CanadaFeedbackFormComponent : BaseModuleRegressionComponent
    {
        private static readonly By FeedbackContainerLocator = By.XPath("//*[@class='AALP-feedback-form-wrapper']");
        private static readonly By FeedbackTitleLocator = By.ClassName("AALP-feedback-heading");
        private static readonly By FeedbackTextBoxLabelLocator = By.XPath("//div[@class='AALP-feedback-text-input']/label");
        private static readonly By FeedbackTextBoxLocator = By.XPath("//div[@class='AALP-feedback-text-input']/textarea");
        private static readonly By FeedbackSendButtonLocator = By.XPath(".//button[contains(@class, 'Button-primary') and text() = 'Send feedback']");

        private readonly IWebElement ContainerElement;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="CanadaFeedbackFormComponent"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public CanadaFeedbackFormComponent(IWebElement containerElement)
        {
            ContainerElement = containerElement;
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; } = FeedbackContainerLocator;

        /// <summary>
        /// Feedback title
        /// </summary>
        public ILabel FeedbackTitle => new Label(ContainerElement, FeedbackTitleLocator);

        /// <summary>
        /// Feedback text box label
        /// </summary>
        public ILabel FeedbackTextBoxLabel => new Label(ContainerElement, FeedbackTextBoxLabelLocator);

        /// <summary>
        /// Feedback Text Box
        /// </summary>
        public ITextbox FeedbackTextBox => new Textbox(ContainerElement, FeedbackTextBoxLocator);

        /// <summary>
        /// Send feedback button
        /// </summary>
        public IButton SendFeedbackButton => new Button(ContainerElement, FeedbackContainerLocator, FeedbackSendButtonLocator);
    }
}
