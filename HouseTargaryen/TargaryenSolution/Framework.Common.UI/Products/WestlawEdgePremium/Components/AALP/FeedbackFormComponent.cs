namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Components;

    /// <summary>
    /// Feedback form component
    /// </summary>
    public class FeedbackFormComponent : BaseModuleRegressionComponent
    {
        private static readonly By FeedbackContainerLocator = By.XPath("//*[@class='AALP-feedback-form-wrapper']");
        private static readonly By SendFeedbackButtonLocator = By.XPath(".//button[contains(@class, 'Button-primary') and text() = 'Send feedback']");
        private static readonly By CancelButtonLocator = By.XPath(".//button[contains(@class, 'Button-secondary') and text() = 'Cancel']");       
        private static readonly By TextboxLocator = By.XPath(".//textarea");

        private readonly IWebElement ContainerElement;

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="FeedbackFormComponent"/> class. 
        /// </summary>
        /// <param name="containerElement"></param>
        public FeedbackFormComponent(IWebElement containerElement)
        {
            ContainerElement = containerElement;
        }

        /// <summary>
        /// Send feedback button
        /// </summary>
        public IButton SendFeedbackButton => new Button(ContainerElement, FeedbackContainerLocator, SendFeedbackButtonLocator);

        /// <summary>
        /// Cancel button
        /// </summary>
        public IButton CancelButton => new Button(ContainerElement, FeedbackContainerLocator, CancelButtonLocator);

        /// <summary>
        /// Textbox
        /// </summary>
        public ITextbox Textbox => new Textbox(ContainerElement, FeedbackContainerLocator, TextboxLocator);

        /// <summary>
        /// Is User Feedback component displayed
        /// </summary>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(ContainerElement, FeedbackContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator { get; } = FeedbackContainerLocator;
    }
}
