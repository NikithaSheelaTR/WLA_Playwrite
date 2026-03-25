namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Deep Research feedback component
    /// </summary>
    public class AiDeepResearchFeedbackComponent : BaseModuleRegressionComponent
    {
        private readonly By FeedbackComponentLocator;

        /// <summary>
        /// New constructor to accept custom locator
        /// </summary>
        public AiDeepResearchFeedbackComponent(By componentLocator)
        {
            FeedbackComponentLocator = componentLocator;
        }

        private static readonly By ThumbsUpButtonLocator = By.XPath(".//saf-button-v3[@data-testid='thumbs-up-button']");
        private static readonly By ThumbsDownButtonLocator = By.XPath(".//saf-button-v3[@data-testid='thumbs-down-button']");
        private static readonly By FeedbackLocator = By.XPath(".//saf-text-area-v3[@data-testid='feedback-textarea']");
        private const string FeedbackTextAreaScript = "return(arguments[0].shadowRoot.querySelector('textarea'));";
        private static readonly By SubmitFeedbackButtonLocator = By.XPath(".//saf-button-v3[@data-testid='submit-button']");
        
        /// <summary>
        /// Thumbs up Button
        /// </summary>
        public IButton ThumbsUpButton => new Button(ComponentLocator, ThumbsUpButtonLocator);

        /// <summary>
        /// Thumbs down Button
        /// </summary>
        public IButton ThumbsDownButton => new Button(ComponentLocator, ThumbsDownButtonLocator);

        /// <summary>
        /// Submit feedback Button
        /// </summary>
        public IButton SubmitFeedbackButton => new Button(ComponentLocator, SubmitFeedbackButtonLocator);

        /// <summary>
        /// Enter feedback in the feedback TextArea
        /// </summary>
        /// <param name="feedback">feedback</param>   
        public void EnterFeedback(string feedback)
        {
            IWebElement FeedbackElement = DriverExtensions.GetElement(this.ComponentLocator, FeedbackLocator);
            IWebElement FeedbackTextArea = (IWebElement)DriverExtensions.ExecuteScript(FeedbackTextAreaScript, FeedbackElement);
            FeedbackTextArea.SendKeys(feedback);
        }
        
        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => FeedbackComponentLocator;
    }
}


