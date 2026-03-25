using Framework.Common.UI.Interfaces.Elements;
using Framework.Common.UI.Products.Shared.Components;
using Framework.Common.UI.Products.Shared.Elements.Buttons;
using Framework.Common.UI.Products.Shared.Elements.Labels;
using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
using OpenQA.Selenium;

namespace Framework.Common.UI.Products.CoCounsel.Components
{
    /// <summary>
    /// Responce component item
    /// </summary>
    public class QuestionAndAnswerComponent : BaseModuleRegressionComponent
    {
        private readonly By AskAQuestionToCreateSurveyTextBoxHostLocator = By.XPath(".//saf-chat/saf-message-box//saf-text-area[@data-testid ='query-textarea']");
        private readonly By SubmitButtonLocator = By.XPath("//saf-button[@data-testid = 'submit-button']");
        private string AskAQuestionToCreateSurveyTextBoxLocatorString = "textarea";

        /// <summary>
        /// ComponentLocator
        /// </summary>
        protected override By ComponentLocator => By.XPath("//saf-chat/saf-message-box");

       
        /// <summary>
        /// Info Message
        /// </summary>
        public ILabel InfoMessageLabel => new Label((IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('span.message'));",
            DriverExtensions.GetElement(AskAQuestionToCreateSurveyTextBoxHostLocator)));

        /// <summary>
        /// SubmitButton
        /// </summary>
        public IButton SubmitButton => new Button(SubmitButtonLocator);

        /// <summary>
        /// Clear Submit Question TextBox
        /// /// </summary>
        public void ClearSubmitQuestionTextBox()
        {
            var SubmitQuestionTextBoxElement = (IWebElement)DriverExtensions.ExecuteScript($"return(arguments[0].shadowRoot.querySelector('{AskAQuestionToCreateSurveyTextBoxLocatorString}'));",
            DriverExtensions.GetElement(AskAQuestionToCreateSurveyTextBoxHostLocator));
            SubmitQuestionTextBoxElement.SendKeys(Keys.Control + "A" + Keys.Backspace);
        }
    }
}
