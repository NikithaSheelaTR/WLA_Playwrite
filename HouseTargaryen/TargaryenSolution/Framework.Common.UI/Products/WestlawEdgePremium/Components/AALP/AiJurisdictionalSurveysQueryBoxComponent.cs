namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// AI Jurisdictional Surveys Query box component
    /// </summary>
    public class AiJurisdictionalSurveysQueryBoxComponent : BaseModuleRegressionComponent
    {
        private static readonly By QueryBoxContainerLocator = By.XPath(".//div[contains(@class,'__questionCard-')]");
        private static readonly By QuestionInputAreaLocator = By.XPath("//saf-text-area[@id='fiftyStateQuestionInput']");

        /// <summary>
        /// Enter question in the input area
        /// </summary>
        /// <param name="question">question</param>
        public void EnterQuestion(string question)
        {
            IWebElement questionInputArea = DriverExtensions.GetElement(ComponentLocator, QuestionInputAreaLocator);
            questionInputArea.SendKeys(question);
        }

        /// <summary>
        /// Enter question in the input area
        /// </summary>
        /// <param name="question">question</param>
        public void EnterQuestionSelectJuris(string question)
        {
            // This is a workaround for the issue where the input area is not being recognized (Greg's idea)
            string enterQuestionScript = $"document.querySelector('[id=\"fiftyStateQuestionInput\"]').value = \"{question}\";";
            DriverExtensions.ExecuteScript(enterQuestionScript);

            string fireEventQuestionScript = "document.querySelector('[id=\"fiftyStateQuestionInput\"]').dispatchEvent(new Event('input'), {bubbles: true});";
            string fireEventJuriScript = "document.querySelector('[current-value=\"AL-CS\"]').changeHandler();";
            DriverExtensions.ExecuteScript(fireEventQuestionScript);
            DriverExtensions.ExecuteScript(fireEventJuriScript);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => QueryBoxContainerLocator;
    }
}

