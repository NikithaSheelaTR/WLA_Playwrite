namespace Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch
{
    using System;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// AI Deep Research Input component
    /// </summary>
    public class AiDeepResearchInputComponent : BaseModuleRegressionComponent
    {
        private static readonly By InputContainerLocator = By.XPath("//div[contains(@class,'Input-module__inputContainer')]");
        private static readonly By QuestionTextAreaLocator = By.XPath(".//saf-text-area-v3[@data-testid='input-textarea']");
        private static readonly By SendButtonLocator = By.XPath(".//saf-button-v3[contains(@class,'Input-module__questionButton')]");
        private static readonly By JurisdictionButtonLocator = By.XPath(".//saf-button-v3[@data-testid='jurisdictions-button']");
        private static readonly By AgentTimeDropdownLocator = By.XPath(".//saf-icon-v3[@icon-name='chevron-down']");
        private static readonly By ReportTypeConciseRadioButtonLocator = By.XPath(".//input[@data-testid='report-type-radio-concise']");
        private static readonly By ReportTypeExpandedRadioButtonLocator = By.XPath(".//input[@data-testid='report-type-radio-expanded']");
        private static readonly By ReportTypeConciseLabelLocator = By.XPath(".//label[@for='dual-agent-concise-radio']");
        private static readonly By ReportTypeExpandedLabelLocator = By.XPath(".//label[@for='dual-agent-expanded-radio']");
        private static readonly By AgentTimeLabelLocator = By.XPath(".//saf-button-v3[@id='report-type-menu-button']");
        private const string SelectAgentTime = ".//saf-menu-item-v3[@id='report-type-menu-item-X']";
        private static readonly By AgentTimeHeadingsLocator = By.XPath(".//span[contains(@class,'ReportTypeMenuItem-module__reportDropdownHeading')]");

        /// <summary>
        /// Clear the question TextArea
        /// </summary>
        public void ClearQuestionTextArea()
        {
            if (this.QuestionTextarea.GetAttribute("current-value").Length > 0)
            {
                this.QuestionTextarea.SendKeys(Keys.Control + "a");
                this.QuestionTextarea.SendKeys(Keys.Delete);
            }
        }

        /// <summary>
        /// Concise Report Type Radiobutton
        /// </summary>
        public ICheckBox ReportTypeConciseRadioButton => new CheckBox(ReportTypeConciseRadioButtonLocator);

        /// <summary>
        /// Expanded Report Type Radiobutton
        /// </summary>
        public ICheckBox ReportTypeExpandedRadioButton => new CheckBox(ReportTypeExpandedRadioButtonLocator);

        /// <summary>
        /// Concise Report Type label
        /// </summary>
        public ILabel ReportTypeConciseLabel => new Label(ReportTypeConciseLabelLocator);

        /// <summary>
        /// Expanded Report Type label
        /// </summary>
        public ILabel ReportTypeExpandedLabel => new Label(ReportTypeExpandedLabelLocator);

        /// <summary>
        /// Question text Area
        /// </summary>
        public ITextbox QuestionTextarea => new Textbox(QuestionTextAreaLocator);

        /// <summary>
        /// Jurisdiction button
        /// </summary>
        public IButton JurisdictionButton => new Button(this.ComponentLocator, JurisdictionButtonLocator);

        /// <summary>
        /// Send Button
        /// </summary>
        public IButton SendButton => new Button(ComponentLocator, SendButtonLocator);

        /// <summary>
        /// Agent time dropdown Button
        /// </summary>
        public IButton AgentTimeDropdownButton => new Button(ComponentLocator, AgentTimeDropdownLocator);

        /// <summary>
        /// Agent time label
        /// </summary>
        public ILabel AgentTimeLabel => new Label(ComponentLocator, AgentTimeLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => InputContainerLocator;

        /// <summary>
        /// select agent time from dropdown
        /// </summary>
        /// <param name="agentTime">agentTime</param>
        public void SelectAgentTimeFromDropdown(AgentTime agentTime)
        {
            string agentTimeIndex = ((int)agentTime).ToString();
            IWebElement selectAgentTime = DriverExtensions.GetElement(By.XPath(SelectAgentTime.Replace("X", agentTimeIndex)));
            selectAgentTime.Click();
        }

        /// <summary>
        /// select report type from dropdown
        /// </summary>
        /// <param name="reportType">agentTime</param>
        public void SelectReportType(ReportType reportType)
        {
            if (reportType.Equals(ReportType.Concise))
            {
                this.ReportTypeConciseRadioButton.Set(true);
            }
            else
            {
                this.ReportTypeExpandedRadioButton.Set(true);
            }
        }

        /// <summary>
        /// Is Send Button Disabled
        /// </summary>
        public bool IsSendButtonDisabled => SendButton.GetAttribute("class").Contains("disabled");

        /// <summary>
        /// Gets a list of Agent Time headings in the Agent Time dropdown
        /// </summary>
        /// <returns>List of strings</returns>
        public List<string> GetAgentTimeHeadings()
            => DriverExtensions.GetElements(AgentTimeHeadingsLocator).Select(e => e.Text).ToList();
    }
}


