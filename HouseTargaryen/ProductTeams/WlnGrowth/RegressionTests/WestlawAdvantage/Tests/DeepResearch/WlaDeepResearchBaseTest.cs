namespace WestlawAdvantage.Tests.DeepResearch
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;

    /// <summary>
    /// AI Deep Research Base Test class
    /// </summary>
    [TestClass]
    [DeploymentItem("TestData/DeepResearch")]
    public class WlaDeepResearchBaseTest : WlaBaseTest
    {
        protected const string AiDeepResearchName = "AI Deep Research";
        protected const string AiDeepResearchTab = "Westlaw AI Deep Research";

        /// <summary>
        /// Generates AI Deep Research report
        /// </summary>
        /// <param name="query">Question to ask</param>
        /// <param name="agentTime">Type of report to generate: 3, 7, or 10 min</param>
        /// <param name="jurisdictions">Semicolon-delimited jurisdictions</param>
        protected AiDeepResearchPage GenerateReport(string query, AgentTime agentTime, string jurisdictions)
        {           
            var homePage = this.GetHomePage<AdvantageHomePage>();
            AiDeepResearchPage deepResearchPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchName).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 10);

            deepResearchPage.WelcomeComponent.InputComponent.AgentTimeDropdownButton.Click();
            deepResearchPage.WelcomeComponent.InputComponent.SelectAgentTimeFromDropdown(agentTime);

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(jurisdictions.Split(';'));
            jurisdictionDialog.SaveButton.Click();

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(query);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            return deepResearchPage;
        }

        /// <summary>
        /// Generates AI Deep Research report without selecting agent time. WARNING: THIS METHOD WILL REPLACE GenerateReport METHOD
        /// </summary>
        /// <param name="query">Question to ask</param>
        /// <param name="agentTime">Type of report to generate: Concise or Expanded</param>
        /// <param name="jurisdictions">Semicolon-delimited jurisdictions</param>
        protected AiDeepResearchPage GenerateReportWithoutAgentTime(string query, string jurisdictions, ReportType reportType = ReportType.Concise)
        {
            var homePage = this.GetHomePage<AdvantageHomePage>();
            AiDeepResearchPage deepResearchPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchName).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 10);

            deepResearchPage.WelcomeComponent.InputComponent.SelectReportType(reportType);

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(jurisdictions.Split(';'));
            jurisdictionDialog.SaveButton.Click();

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(query);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            return deepResearchPage;
        }

        /// <summary>
        /// Perform ui postcondition routines.
        /// </summary>
        protected override void PerformUiPostconditionRoutines()
        {
            var homePage = this.GetHomePage<AdvantageHomePage>();
            homePage.WestlawAdvantageLogoLink.Click();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);
            const string jurisdictions= "All Federal;All States";
            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(jurisdictions.Split(';'));
            jurisdictionDialog.SaveButton.Click();
        }
    }
}

