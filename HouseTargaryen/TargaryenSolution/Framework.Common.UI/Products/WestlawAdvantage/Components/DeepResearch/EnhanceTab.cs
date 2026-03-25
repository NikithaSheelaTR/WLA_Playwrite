namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.HomePage.Browse;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Products.Shared.Elements;
    using System.Collections.Generic;

    /// <summary>
    /// Enhance Tab
    /// </summary>
    public class EnhanceTab : BaseBrowseTabPanelComponent
    {
        private static readonly By TabContainerLocator = By.XPath("//div[@data-testid='report-answer-panel-enhancetab']");
        private static readonly By ClearResponseButtonLocator= By.XPath("//saf-button-v3[@data-testid='clear-button']");
        private static readonly By GenerateNewReportButtonLocator = By.XPath(".//saf-button-v3[@data-testid='refine-report-button']");
        private static readonly By ClarifyingBottomTextLocator = By.XPath("//p[contains(@class, 'clarifyingTextBottom')]");
        private static readonly By AnswerTextAreaLocator = By.XPath(".//saf-text-area-v3[contains(@id, 'question-text-field')]");
        private const string InputAreaScript = "return(arguments[0].shadowRoot.querySelector('textarea'));";

        /// <summary>
        ///  Clear Response button
        /// </summary>
        public IButton ClearResponseButton => new Button(ComponentLocator, ClearResponseButtonLocator);

        /// <summary>
        ///  Generate new report button
        /// </summary>
        public IButton GenerateNewReportButton => new Button(ComponentLocator, GenerateNewReportButtonLocator);

        /// <summary>
        ///  Answer Text area 
        /// </summary>
        public IReadOnlyCollection<ITextbox> AnswerTextArea => new ElementsCollection<Textbox>(ComponentLocator, AnswerTextAreaLocator);

        /// <summary>
        ///  Clarifying bottom text label
        /// </summary>
        public ILabel ClarifyingBottomText => new Label(ComponentLocator, ClarifyingBottomTextLocator);

        /// <summary>
        /// Tab name
        /// </summary>
        protected override string TabName => "Enhance";

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => TabContainerLocator;

        /// <summary>
        /// Enter the answer to the first clarifying question
        /// </summary>
        /// <param> name="answer">
        /// answer
        /// </param>
        public void EnterAnswerToFirstClarifyingQuestion(string answer)
        {
            IWebElement AnswerTextArea = DriverExtensions.GetElement(ComponentLocator, AnswerTextAreaLocator);
            IWebElement TextArea = (IWebElement)DriverExtensions.ExecuteScript(InputAreaScript, AnswerTextArea);
            TextArea.SendKeys(answer);
        }
    }
}


