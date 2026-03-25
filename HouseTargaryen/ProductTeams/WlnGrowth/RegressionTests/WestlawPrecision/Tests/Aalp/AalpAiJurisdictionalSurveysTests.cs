namespace WestlawPrecision.Tests.Aalp
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using System.Windows.Forms;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.HomePage.HomePageTabs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// AI Jurisdictional Surveys tests
    /// </summary>
    [TestClass]
    public class AalpAiJurisdictionalSurveysTests : AalpBaseTest
    {
        private const string FeatureTestCategoryAiJurisdictionalSurveys = "AiJurisdictionalSurveys";
        private const string AjsTestFolder = "AjsTestFolder";

        /// <summary>
        /// Test Jurisdictional Surveys page displays correctly (Story 1893501 1998052).
        /// Email me: Story 1900008 Test Case 2003211 Delivery: Story 1900007 Test Case 1941099
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        /// 2. Navigate to landing page via CoCounsel dialog
        /// 3. Check: Verify Jurisdictional Surveys home page displayed
        /// 4. Enter question: "What's the minimum wage?"
        /// 5. Select jurisdictions: California, Texas, Content type: Statutes, then click Create button
        /// 6. Check: Verify disclaimer message displayed on result page
        /// 7. Click Email me when survery is ready button
        /// 8. Check: Verify email success label displayed after clicking Email me button
        /// 9. Check: Verify result displayed for selected jurisdiction
        /// 10.Check: Verify disclaimer message displayed on result page
        /// 11.Click Delivery dropdown and select Download option
        /// 12.Check: Verify delivery confirmation displayed
        /// 13.Click New Survey button
        /// 14.Check: Verify clicking New Survey button takes to survey home page
        /// 15.Click Select All checkbox to select all jurisdictions
        /// 16.Check: Verify checking Select All doesn't cause alert message to display
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        [TestCategory(SmokeTestCategory)]
        [TestCategory(TeamSahniSmokeTestCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-SHOW-EMAIL-ME-IMMEDIATELY")]
        [TestCategory("JurisdictionalSurveysSmokeTest")]
        public void JurisdictionalSurveysSmokeTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string Jurisdiction1 = "CA-CS", Jurisdiction2 = "TX-CS", JurisSelectAll = "Select All";
            const string ExpectedLandingPageLabel = "Survey search criteria";
            const string ExpectedEmailSuccessLabel = "You'll receive an email at";

            var surveysPage = NavigateToLandingPage();
            string displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Jurisdictional Surveys home page displayed",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                $"Jurisdictional Surveys page not displayed correctly. Expected: {ExpectedLandingPageLabel} Displayed: {displayedPageDescription}");

            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(Jurisdiction1, Jurisdiction2);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();

            SafeMethodExecutor.WaitUntil(() => surveysPage.EmailMeButton.Displayed);
            surveysPage.EmailMeButton.Click();
            
            this.TestCaseVerify.IsTrue(
                "Verify email success label displayed after clicking Email me button",
                surveysPage.EmailSuccessLabel.Text.Contains(ExpectedEmailSuccessLabel),
                "Email success label not displayed after clicking Email me button.");

            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
            this.TestCaseVerify.IsTrue(
                "Verify result displayed for selected jurisdictions",
                "California".Equals(surveysPage.SurveyResult.ResultItems.First().JurisdictionNameLabel.Text),
                "Result is not displayed for the selected jurisdiction.");         

            var downloadDialog = surveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            this.TestCaseVerify.IsTrue(
                "Verify delivery confirmation displayed",
                downloadDialog.DownloadAndWaitForConfirmation<AiJurisdictionalSurveysPage>(),
                "Delivery confirmation not displayed");

            surveysPage.Toolbar.NewSurveyButton.Click();
            SafeMethodExecutor.ExecuteUntil(() => surveysPage.PageDescription.Displayed);

            displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify clicking New Survey button takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Clicking New Survey button did not take to survey home page.");

            surveysPage.Jurisdictions.SelectJurisdiction(JurisSelectAll);
            this.TestCaseVerify.IsFalse(
                "Verify checking Select All does not cause alert message to display",
                surveysPage.Jurisdictions.SelectAllAlertLabel.Displayed,
                "Checking Select All caused alert message to display.");
        }

        /// <summary>
        /// Test Jurisdictional Surveys Status widget (Story 2031707 Test Case 2033775).
        /// 1. Sign in WL Precision with FAC: AIResearchFiftyStates
        ///    IAC-AI-RESEARCH-FIFTY-STATES and IAC-FIFTY-STATES-IN-PROGRESS-MODAL
        /// 2. Navigate to landing page via CoCounsel dialog
        /// 3. Select 9 jurisdictions: Alabama, Alaska, Arizona, Arkansas, California, Colorado, Connecticut, D.C. and Delaware
        /// 4. Enter question: "What's the minimum wage?" and click Create Survey button
        /// 5. Check: Verify status button text updated with survey being processed
        /// 6. Check: Verify new survey button enabled after submitting first question
        /// 7. Click Survey Status button to open the widget
        /// 8. Check: Verify status contains survey question link
        /// 9. Check: Verify 7 jurisdiction names displayed with 'and 2 more' at the end
        /// 10.Click question link from status
        /// 11.Check: Verify clicking question link from status takes to results page successfully
        /// </summary>
        [Ignore]
        //[TestMethod]// Disabled due to feature not being available yet
        //[TestCategory(CurrentTestCategory)]
        //[TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysStatusTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisAl = "AL-CS", JurisAk = "AK-CS", JurisAz = "AZ-CS", JurisAr = "AR-CS", JurisCa = "CA-CS", JurisCo = "CO-CS", JurisCt = "CT-CS", JurisDc = "DC-CS", JurisDe = "DE-CS";
            const string ExpectedJurisLabel = "Alabama, Alaska, Arizona, Arkansas, California, Colorado, Connecticut and 2 more";
            
            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisAl, JurisDc, JurisAk, JurisAz, JurisAr, JurisCa, JurisCo, JurisCt, JurisDe);
            SafeMethodExecutor.WaitUntil(() => surveysPage.IsCreateSurveyButtonDisabled, timeoutFromSec: 10);
            surveysPage.CreateSurveyButtonTop.ScrollToElement();
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => surveysPage.Toolbar.SurveyStatusButton.Enabled);

            this.TestCaseVerify.IsFalse(
                "Verify status button text updated with survey being processed",
                surveysPage.Toolbar.SurveyStatusButton.Text.Contains("Survey Status (0)"),
                "Status button text updated with survey being processed.");

            // Two concurrent surveys are allowed, so Create button should be enabled
            this.TestCaseVerify.IsTrue(
                "Verify new survey button enabled after submitting first question",
                surveysPage.Toolbar.NewSurveyButton.Enabled,
                "New survey button not enabled after submitting first question.");

            surveysPage = BrowserPool.CurrentBrowser.Refresh<AiJurisdictionalSurveysPage>();
            surveysPage.Toolbar.SurveyStatusButton.Click();
            var surveyStatus = surveysPage.Toolbar.SurveyStatus;

            this.TestCaseVerify.IsTrue(
                "Verify status contains survey question link",
                surveyStatus.QuestionLink.Text.Contains(SurveyQuestion),
                "Status does not contain survey question link.");

            string jurisLabel = surveyStatus.JurisdictionLabel.Text;
            
            this.TestCaseVerify.IsTrue(
                "Verify 7 jurisdiction names displayed with 'and 2 more' at the end",
                jurisLabel.Contains(ExpectedJurisLabel),
                "Jurisdiction display not correct. Expected: " + ExpectedJurisLabel + " Displayed: " + jurisLabel);

            surveysPage = surveyStatus.QuestionLink.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
            this.TestCaseVerify.IsTrue(
                "Verify clicking question link from status takes to results page successfully",
                "Alabama".Equals(surveysPage.SurveyResult.ResultItems.First().JurisdictionNameLabel.Text),
                "Clicking question link from status does not take to results page successfully.");
        }

        /// <summary>
        /// Test Jurisdictional Surveys History (Story 1997857 Test Case 2036936).
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        /// 2. Navigate to landing page via CoCounsel dialog
        /// 3. Select a jurisdiction: California
        /// 4. Enter question: "What's the minimum wage?" and click Create Survey button
        /// 5. Click History button to view all history
        /// 6. Check: Verify Jurisdictional Surveys entry displayed in History
        /// 7. Click title from History to view survey results
        /// 8. Check: Verify clicking title from History takes to Jurisdictional Surveys result page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysHistoryTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCa = "CA-CS";
            
            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCa);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
            var timeStampLabelInitial = surveysPage.SurveyResult.TimeStampLabel.Text;

            // Wait extra time plus refresh a few times for the entry to appear in history
            Thread.Sleep(10000);//It takes some time for the entry to appear in history
            var recentHistoryDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
            Thread.Sleep(5000);
            historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
            Thread.Sleep(5000);
            historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.GetGridItems().First().IsTextLinkDisplayed(SurveyQuestion), timeoutFromSec: 15);

            this.TestCaseVerify.IsTrue(
                "Verify Jurisdictional Surveys entry displayed in History",
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SurveyQuestion),
                "Jurisdictional Surveys entry not displayed in History");
            
            surveysPage = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
            var timeStampLabelHistory = surveysPage.SurveyResult.TimeStampLabel.Text;
            var questionDisplayed = surveysPage.SurveyResult.QuestionLabel.Text;
            
            this.TestCaseVerify.IsTrue(
                "Verify clicking title from History takes to Jurisdictional Surveys result page",
                questionDisplayed.Equals(SurveyQuestion)
                && timeStampLabelInitial.Equals(timeStampLabelHistory),
                $"Clicking title from History did not take to Jurisdictional Surveys result page. " +
                $"Question expected: {SurveyQuestion} Question displayed: {questionDisplayed} " +
                $"Timestamp expected: {timeStampLabelInitial} Timestamp displayed: {timeStampLabelHistory}");
        }

        /// <summary>
        /// Test Jurisdictional Surveys access points on home page (Story 1900723 Test Case 1911786).
        /// Access from CoCounsel dialog, Tools flyout and Key Features panel opens in new tab.
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        ///    setting CategoryPageCollectionSet to: w_cb_catpagesqa_cs
        /// 2. Click Tools button and select AI Jurisdictional Surveys from flyout
        /// 3. Check: Verify Tools flyout link takes to survey home page
        /// 4. Close new tab and navigate to home page 
        /// 5. Click AI Jurisdictional Surveys Key Features panel
        /// 6. Check: Verify key feature link takes to survey home page 
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysHomePageAccessTest()
        {
            const string SurveyLabel = "AI Jurisdictional Surveys";
            const string ExpectedLandingPageLabel = "Survey search criteria";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var surveysPage = homePage.Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>().ToolLinks.First(link => link.Text.Contains(SurveyLabel)).Click<AiJurisdictionalSurveysPage>();
            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            string displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify Tools flyout link takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Tools flyout link not takes to survey home page.");

            BrowserPool.CurrentBrowser.CloseTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            surveysPage = BrowserPool.CurrentBrowser.Refresh<AiJurisdictionalSurveysPage>();
            surveysPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(SurveyLabel).Click<AiJurisdictionalSurveysPage>();
            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify key feature link takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Key feature link not takes to survey home page.");
        }

        /// <summary>
        /// Test Jurisdictional Surveys access points on AAR (Storis 2074012 and 2069130).
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        ///    setting CategoryPageCollectionSet to: w_cb_catpagesqa_cs
        /// 2. Click AI-Assisted Research browse tab on home page
        /// 3. Check: Verify AI Jurisdictional Surveys link displayed on AAR browse tab on home page  
        /// 4. Click AI-Assisted Research from CoCounsel dialog to go to AAR landing page
        /// 5. Check: Verify AI Jurisdictional Surveys link displayed on AAR landing page
        /// 6. Set jurisdiction to All State & Federal and enter question and submit: 
        ///    Can churches and other religious organizations be held liable for creating or allowing a hostile work environment or are those claims precluded by the ministerial exception?
        /// 7. Check: Verify AI Jurisdictional Surveys link displayed on AAR result page
        /// 8. Click AI Jurisdictional Surveys link from AAR result page
        /// 9. Check: Verify clicking Surveys link on AAR result page takes to survey home page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysAarAccessTest()
        {
            const string Question = "Can churches and other religious organizations be held liable for creating or allowing a hostile work environment or are those claims precluded by the ministerial exception?";
            const string ExpectedLandingPageLabel = "Survey search criteria";
            const string AssistedResearchLabel = "AI-Assisted Research";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            var aiAssistedResearchTab = homePage.BrowseTabPanel.SetActiveTab<AiAssistedResearchTabPanel>(PrecisionBrowseTab.AiAssistedResearch);

            this.TestCaseVerify.IsTrue(
                "Verify AI Jurisdictional Surveys link displayed on AAR browse tab on home page",
                aiAssistedResearchTab.AiJurisdictionalSurveysLink.Displayed,
                "AI Jurisdictional Surveys link NOT displayed on AAR tab on home page.");

            var aiAssistantPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(AssistedResearchLabel).Click<AiAssistedResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiAssistedResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiAssistedResearchTab);
            SafeMethodExecutor.WaitUntil(() => aiAssistantPage.Toolbar.JurisdictionButton.Displayed);
            aiAssistantPage = aiAssistantPage.Toolbar.JurisdictionButton.Click<JurisdictionOptionsDialog>().SelectDefaultJurisdiction().SaveButton.Click<AiAssistedResearchPage>();

            this.TestCaseVerify.IsTrue(
                "Verify AI Jurisdictional Surveys link displayed on AAR landing page",
                aiAssistantPage.Chat.AiJurisdictionalSurveysLink.Displayed,
                "AI Jurisdictional Surveys link NOT displayed on AAR tab on home page.");

            aiAssistantPage.QueryBox.QuestionTextbox.SetText(Question);
            aiAssistantPage = aiAssistantPage.QueryBox.SendQuestionButton.Click<AiAssistedResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().ProgressDotsLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify AI Jurisdictional Surveys link displayed on AAR result page",
                aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AiJurisdictionalSurveysLink.Displayed,
                "AI Jurisdictional Surveys link NOT displayed on AAR result page.");

            var surveysPage = aiAssistantPage.Chat.AiResearchQuestionAndAnswerItems.First().AiJurisdictionalSurveysLink.Click<AiJurisdictionalSurveysPage>();

            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);
            SafeMethodExecutor.ExecuteUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            var displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify clicking Surveys link on AAR result page takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Clicking Surveys link on AAR result page did not take to survey home page.");
        }

        /// <summary>
        /// Test Jurisdictional Surveys access points on category pages (Story 1912184 Test Case 2001932).
        /// Access covered in this test opens Jurisdictional Surveys page in the same tab.
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        ///    setting CategoryPageCollectionSet to: w_cb_catpagesqa_cs
        /// 2. Go to Home-Content types tab-Statutes & Court Rules
        /// 3. Click AI Jurisdictional Surveys link
        /// 4. Check: Verify link on Statutes page takes to survey home page
        /// 5. Go to Home-Content types tab-Regulations
        /// 6. Click AI Jurisdictional Surveys link 
        /// 7. Check: Verify link on Regulations page is not displayed //takes to survey home page
        /// 8. Go to Home-Content types tab-Secondary Sources
        /// 9. Check: Verify AI Jurisdictional Surveys link not displayed on Secondary Sources page
        /// 10.Go to Home-Content types tab-Statutes & Court Rules-Jurisdictional Statutory Surveys
        /// 11.Click Access Now link 
        /// 12.Check: Verify link on Statutory page takes to survey home page
        /// 13.Go to Home-Content types tab-Regulations-Jurisdictional Regulatory Surveys
        /// 14.Click Access Now link 
        /// 15.Check: Verify link on Statutory page takes to survey home page
        /// 16.Go to Home-Tools tab
        /// 17.Click AI Jurisdictional Surveys link
        /// 18.Check: Verify link on Tools tab opens AJS in new browser tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysCategoryPageAccessTest()
        {
            const string SurveyLabel = "AI Jurisdictional Surveys";
            const string AccessNowLabel = "Access Now";
            const string ExpectedLandingPageLabel = "Survey search criteria"; 
            const string StatutesPageUri = @"/Browse/Home/StatutesCourtRules?transitionType=Default&contextData=(sc.Default)";
            const string RegulationsPageUri = @"/Browse/Home/Regulations?transitionType=Default&contextData=(sc.Default)";
            const string SecondarySourcesPageUri = @"/Browse/Home/SecondarySources?transitionType=Default&contextData=(sc.Default)";
            const string StatutoryPageUri = @"/Browse/Home/SecondarySources/50StateSurveys/50StateStatutorySurveys?transitionType=Default&contextData=(sc.Default)";
            const string RegulatoryPageUri = @"/Browse/Home/SecondarySources/50StateSurveys/50StateRegulatorySurveys?transitionType=Default&contextData=(sc.Default)";

            //Home-Content types tab-Statutes & Court Rules
            var browsePage = NavigateToBrowsePage(StatutesPageUri);
            var surveysPage = browsePage.ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyLabel);                
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            string displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify link on Statutes page takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Link on Statutes page not takes to survey home page.");

            //Home-Content types tab-Regulations
            browsePage = NavigateToBrowsePage(RegulationsPageUri);
           
            this.TestCaseVerify.IsTrue(
                "Verify AI Jurisdictional Surveys link displayed on Regulations page",
                browsePage.IsTextLinkDisplayed(SurveyLabel),
                "AI Jurisdictional Surveys link not displayed on Regulations page.");

            //Home-Content types tab-Secondary Sources
            browsePage = NavigateToBrowsePage(SecondarySourcesPageUri);

            this.TestCaseVerify.IsFalse(
                "Verify AI Jurisdictional Surveys link not displayed on Secondary Sources page",
                browsePage.IsTextLinkDisplayed(SurveyLabel),
                "AI Jurisdictional Surveys link should not display on Secondary Sources page.");

            //Home-Content types tab-Statutes & Court Rules-Jurisdictional Statutory Surveys
            browsePage = NavigateToBrowsePage(StatutoryPageUri);
            surveysPage = browsePage.ClickLinkByText<AiJurisdictionalSurveysPage>(AccessNowLabel);
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify link on Statutory page takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Link on Statutory page not takes to survey home page.");

            //Home-Content types tab-Regulations-Jurisdictional Regulatory Surveys
            browsePage = NavigateToBrowsePage(RegulatoryPageUri);
            surveysPage = browsePage.ClickLinkByText<AiJurisdictionalSurveysPage>(AccessNowLabel);
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify link on Regulatory page takes to survey home page",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Link on Regulatory page not takes to survey home page.");

            //Home-Tools tab. AJS should open in new browser tab
            var homePage = surveysPage.Header.ClickLogo<PrecisionHomePage>();
            var tooltab = homePage.BrowseTabPanel.SetActiveTab<PrecisionToolsTabPanel>(PrecisionBrowseTab.Tools);
            surveysPage = tooltab.ToolsItems.First(item => item.HeaderLink.Text.Contains(SurveyLabel)).HeaderLink.Click<AiJurisdictionalSurveysPage>();
            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);
            SafeMethodExecutor.WaitUntil(() => surveysPage.PageDescription.Displayed, timeoutFromSec: 10);
            surveysPage.ClosePendoMessage();

            displayedPageDescription = surveysPage.PageDescription.Text;
            this.TestCaseVerify.IsTrue(
                "Verify link on Tools tab opens AJS in new browser tab",
                displayedPageDescription.Equals(ExpectedLandingPageLabel),
                "Link on Tools tab not takes to survey home page.");
        }

        /// <summary>
        /// Test verifies block message displayed when usage limit is met. (Story 1903330 TestCase 1912244)
        /// 1. Sign in WL Precision with IAC-AI-RESEARCH-FIFTY-STATES and FAC: AIResearchFiftyStates
        ///    Set daily limit to 1
        /// 2. Navigate to landing page via CoCounsel dialog
        /// 3. Enter question: "What's the minimum wage?"
        /// 4. Select jurisdiction: California, Content type: Statutes, then click Create button
        /// 5. Click New Survey button
        /// 6. If there is error on page, retry creating survey
        /// 7. Check: Verify usage limit message displayed as expected
        /// </summary>
        [Ignore]
        //[TestMethod] // Disabled temporarily due to flakiness
        //[TestCategory(CurrentTestCategory)]
        //[TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        [TestProperty("JurisdictionalSurveysDailyLimit", "1")]
        public void JurisdictionalSurveysUsageLimitTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string Jurisdiction = "CA-CS";
            const string ExpectedUsageLimitMessage = "You have exceeded the daily limit for creating surveys. Please try again tomorrow.";
            const string PageErrorMessage = "Sorry, something went wrong.";

            var surveysPage = NavigateToLandingPage();
            var displayedUsageLimitMessage = surveysPage.UsageLimitLabel.Text;

            if (string.IsNullOrEmpty(displayedUsageLimitMessage))
            {
                surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
                surveysPage.Jurisdictions.SelectJurisdiction(Jurisdiction);
                SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
                Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
                surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
                SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

                // Add checking for failure, then retry
                var pageError = surveysPage.PageErrorLabel.Text;

                surveysPage.Toolbar.NewSurveyButton.Click();
                SafeMethodExecutor.ExecuteUntil(() => surveysPage.PageDescription.Displayed);
                if(pageError.Contains(PageErrorMessage))
                {
                    surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
                    surveysPage.Jurisdictions.SelectJurisdiction(Jurisdiction);
                    SafeMethodExecutor.WaitUntil(() => surveysPage.IsCreateSurveyButtonDisabled, timeoutFromSec: 10);
                    surveysPage.CreateSurveyButtonTop.ScrollToElement();
                    surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
                    SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
                }
                surveysPage.Toolbar.NewSurveyButton.Click();
                SafeMethodExecutor.ExecuteUntil(() => surveysPage.PageDescription.Displayed);
                displayedUsageLimitMessage = surveysPage.UsageLimitLabel.Text;
            }

            this.TestCaseVerify.IsTrue(
                "Verify usage limit message displayed as expected",
                ExpectedUsageLimitMessage.Equals(displayedUsageLimitMessage),
                $"Usage limit message not displayed as expected. Displayed: {displayedUsageLimitMessage}");
        }

        /// <summary>
        /// Description: Verify Ajs copy link delivery functionality .
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Open the AI Jurisdictional Surveys link.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 4. Click on the Copy link Icon button in the toolbar and save link.
        /// 5.Log off and log in back.
        /// 6.Open a new tab using the copied link.
        /// 7. Download document and veryfy the content
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysCopyLinkDeliveryTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCa = "CA-CS";

            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCa);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10);
            var copiedLink = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();

            var homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var surveysPageCopiedlinkpage = this.GetHomePage<PrecisionHomePage>()
                   .OpenNewTabUsingJavascript<AiJurisdictionalSurveysPage>("AI Jurisdictional Surveys | Westlaw Precision", copiedLink);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed,1000);
                        
            var jurisdictionSummaries =  surveysPageCopiedlinkpage.SurveyResult.ResultItems
                .Select(item =>this.CleanTextForCompare(item.JurisdictionResponceLabel.Text));
                       
            var downloadDialog = surveysPageCopiedlinkpage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - AI Jurisdictional Survey - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                "Verify survey result displayed for selected jurisdiction",
                jurisdictionSummaries.All(summary => text.Contains(summary)),
                $"Survey result not displayed for the selected jurisdiction.");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysCopyLinkTest()
        {
            const string SurveyQuestion = "What are the implications for running a red light?";
            const string AllJurisdiction = "Select All";
            string checkCopiedLinkBrowserTabTitle = "Verify: Browse tab title is correct in the copied link";
            string checkCopiedLinkPageHeading = "Verify: page heading is correct in the copied link";
            
            var surveysPage = NavigateToLandingPage();

            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(AllJurisdiction);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000);

            var jurisdictionNames = surveysPage.ContentType.JurisdictionContentTypes;
            var jurisdictionLabels = surveysPage.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction filters are comparing with Jurisdictions",
                jurisdictionNames.SequenceEqual(jurisdictionLabels),
               $"Jurisdiction filters are not comparing with Jurisdictions");

            var originalBrowserTitle = BrowserPool.CurrentBrowser.Title;
            var originalPageHeading = surveysPage.PageHeaderLabel.Text;

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, timeoutFromSec: 10);
            var copiedLink = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();

            var homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var surveysPageCopiedlinkpage = this.GetHomePage<PrecisionHomePage>()
                   .OpenNewTabUsingJavascript<AiJurisdictionalSurveysPage>("AI Jurisdictional Surveys | Westlaw Precision", copiedLink);

            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);
    
            this.TestCaseVerify.IsTrue(
               checkCopiedLinkBrowserTabTitle,
               BrowserPool.CurrentBrowser.Title.Equals(originalBrowserTitle),
               "Browser tab title is not correct after opening copied link");

            this.TestCaseVerify.IsTrue(
               checkCopiedLinkPageHeading,
               surveysPageCopiedlinkpage.PageHeaderLabel.Text.Equals(originalPageHeading),
               "Heading is not correct after opening copied link");

            var jurisdictionFilters = surveysPageCopiedlinkpage.ContentType.JurisdictionContentTypes;
            var jurisdictionSummaries = surveysPageCopiedlinkpage.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();
           
            this.TestCaseVerify.IsTrue(
               "Verify jurisdiction filters are comparing with Jurisdictionn filters from Copied Link",
                jurisdictionSummaries.SequenceEqual(jurisdictionLabels),
               $"Jurisdiction filters are not comparing with filters from Copied Link.");

            this.TestCaseVerify.IsTrue(
               "Verify jurisdictions are comparing with Jurisdictions from Copied Link",
                jurisdictionFilters.SequenceEqual(jurisdictionNames),
               $"Jurisdictions are not comparing with Jurisdictionns from Copied Link");
        }

        // <summary>
        /// Description: Verify AJS copy link functionality with History.
        /// 1. Navigate to the Westlaw Precision homepage.
        /// 2. Access the AI Jurisdictional Surveys link.
        /// 3. Enter a question regarding family leave in the question textbox.
        /// 5. Submit the question and wait for the AI response.
        /// 6. Save the question and answer to a folder.
        /// 7. Open History page
        /// 8. Select and click last question
        /// 9. Click on the Copy link Icon button in the toolbar.
        /// 10.Log off and log in back.
        /// 11.Open a new tab using the copied link from history page and verify that the question and answer are displayed correctly.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysCopylinkHistoryTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCa = "CA-CS";

            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCa);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            var jurisdictionNamesInitial = surveysPage.ContentType.JurisdictionContentTypes;
            var jurisdictionLabelsInitial = surveysPage.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();
            var questionInitial = surveysPage.SurveyResult.QuestionLabel.Text;

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, 3000);
            var copiedLink = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();
            var homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var surveysPageCopiedLink = this.GetHomePage<PrecisionHomePage>()
                .OpenNewTabUsingJavascript<AiJurisdictionalSurveysPage>("AI Jurisdictional Surveys | Westlaw Precision", copiedLink);
            SafeMethodExecutor.WaitUntil(() => !surveysPageCopiedLink.ProgressLabel.Displayed, timeoutFromSec: 5);

            var questionCopied = surveysPageCopiedLink.SurveyResult.QuestionLabel.Text;
            var jurisdictionNamesCopied = surveysPageCopiedLink.ContentType.JurisdictionContentTypes;
            var jurisdictionLabelsCopied = surveysPageCopiedLink.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();

            this.TestCaseVerify.AreEqual(
                "Verify the Question is the same as the query asked (Copied Link)",
                questionInitial,
                questionCopied,
                "Question is not the same as the query asked (Copied Link)");

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction filters are comparing with Jurisdictions from Copied Link",
                jurisdictionNamesInitial.SequenceEqual(jurisdictionNamesCopied),
                "Jurisdiction filters are not matching with Copied Link");

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction labels are comparing with Jurisdictions from Copied Link",
                jurisdictionLabelsInitial.SequenceEqual(jurisdictionLabelsCopied),
                "Jurisdiction labels are not matching with Copied Link");

            var recentHistoryDialog = surveysPageCopiedLink.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
       
            historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.GetGridItems().First().IsTextLinkDisplayed(SurveyQuestion), timeoutFromSec: 15);

            this.TestCaseVerify.IsTrue(
                "Verify Jurisdictional Surveys entry displayed in History",
                historyPage.HistoryTable.GetGridItems().First().Title.Equals(SurveyQuestion),
                "Jurisdictional Surveys entry not displayed in History");

            var surveysPageFromHistory = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPageFromHistory.ProgressLabel.Displayed);

            var questionDisplayed = surveysPageFromHistory.SurveyResult.QuestionLabel.Text;
            var jurisdictionNamesHistory = surveysPageFromHistory.ContentType.JurisdictionContentTypes;
            var jurisdictionLabelsHistory = surveysPageFromHistory.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction filters are comparing with Jurisdictions from History",
                jurisdictionNamesInitial.SequenceEqual(jurisdictionNamesHistory),
                "Jurisdiction filters are not matching with History");

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction labels are comparing with Jurisdictions from History",
                jurisdictionLabelsInitial.SequenceEqual(jurisdictionLabelsHistory),
                "Jurisdiction labels are not matching");
        }

        [TestMethod] 
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionaFolderingDeliveryTest()
        {
            const string SurveyQuestion = "What are the implications for running a red light?";
            const string JurisCa = "CA-CS";

            PrepareTestFolder();

            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCa);

            Thread.Sleep(1000);
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000);

            var jurisdictionNamesInitial = surveysPage.ContentType.JurisdictionContentTypes;
            var jurisdictionLabelsInitial = surveysPage.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();
            var questionInitial = surveysPage.SurveyResult.QuestionLabel.Text;

            var saveToFolderDialog = surveysPage.Toolbar.SaveToFolderButton.Click<SaveToFolderDialog>();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(AjsTestFolder);
            saveToFolderDialog.ClickSaveButton<AiJurisdictionalSurveysPage>();

            var recentFolderDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            var folderPage = recentFolderDialog.ClickFolderByName(AjsTestFolder).ClickViewThisFolderButton();
            surveysPage = folderPage.FolderGrid.ClickGridItemByName<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed, 3000);

            var jurisdictionSummaries = surveysPage.SurveyResult.ResultItems
               .Select(item => this.CleanTextForCompare(item.JurisdictionResponceLabel.Text));

            var downloadDialog = surveysPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Pdf);
            downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = $"Westlaw Precision - AI Jurisdictional Survey - {DateTime.Now.ToString(DeliveryDateFormat)}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = this.CleanTextForCompare(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName)));

            this.TestCaseVerify.IsTrue(
                 "Verify survey result displayed for selected jurisdiction",
                 jurisdictionSummaries.All(summary => text.Contains(summary)),
                 $"Survey result not displayed for the selected jurisdiction.");

            surveysPage.Toolbar.ResearchCopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => surveysPage.CopiedLinkSuccessLabel.Displayed, 5000);
            var copiedLink = Clipboard.GetText();

            this.DefaultSignOnManager.SignOff();
            var homePage = this.SignOnBack().ClickContinueButton<PrecisionHomePage>();
            BrowserPool.CurrentBrowser.CreateTab("Westlaw Precision");
            BrowserPool.CurrentBrowser.ActivateTab("Westlaw Precision");

            var surveysPageCopiedLink = this.GetHomePage<PrecisionHomePage>()
                .OpenNewTabUsingJavascript<AiJurisdictionalSurveysPage>("AI Jurisdictional Surveys | Westlaw Precision", copiedLink);
            SafeMethodExecutor.WaitUntil(() => !surveysPageCopiedLink.ProgressLabel.Displayed, timeoutFromSec: 5);

            var questionCopied = surveysPageCopiedLink.SurveyResult.QuestionLabel.Text;
            var jurisdictionNamesCopied = surveysPageCopiedLink.ContentType.JurisdictionContentTypes;
            var jurisdictionLabelsCopied = surveysPageCopiedLink.SurveyResult.ResultItems.Select(item => item.JurisdictionNameLabel.Text).ToList();

            this.TestCaseVerify.AreEqual(
                "Verify the Question is the same as the query asked (Copied Link)",
                questionInitial,
                questionCopied,
                "Question is not the same as the query asked (Copied Link)");

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction filters are comparing with Jurisdictions from Copied Link",
                jurisdictionNamesInitial.SequenceEqual(jurisdictionNamesCopied),
                "Jurisdiction filters are not matching with Copied Link");

            this.TestCaseVerify.IsTrue(
                "Verify jurisdiction labels are comparing with Jurisdictions from Copied Link",
                jurisdictionLabelsInitial.SequenceEqual(jurisdictionLabelsCopied),
                "Jurisdiction labels are not matching with Copied Link");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryAiJurisdictionalSurveys)]
        public void JurisdictionalSurveysEmailMeButtonNotShownHistoryTest()
        {
            const string SurveyQuestion = "What's the minimum wage?";
            const string JurisCa = "CA-CS";
            
            var surveysPage = NavigateToLandingPage();
            surveysPage.QueryBox.EnterQuestion(SurveyQuestion);
            surveysPage.Jurisdictions.SelectJurisdiction(JurisCa);
            SafeMethodExecutor.WaitUntil(() => surveysPage.CreateSurveyButtonTop.Displayed);
            Thread.Sleep(1000);//Adding a short sleep to avoid intermittent issues with button click
            surveysPage = surveysPage.CreateSurveyButtonTop.Click<AiJurisdictionalSurveysPage>();
            SafeMethodExecutor.WaitUntil(() => !surveysPage.ProgressLabel.Displayed);

            var recentHistoryDialog = surveysPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);
            var historyPage = recentHistoryDialog.ViewAllLink.Click<EdgeCommonHistoryPage>();
            historyPage = BrowserPool.CurrentBrowser.Refresh<EdgeCommonHistoryPage>();
            SafeMethodExecutor.WaitUntil(() => historyPage.HistoryTable.GetGridItems().First().IsTextLinkDisplayed(SurveyQuestion), timeoutFromSec: 15);

            var surveysPageFromHistory = historyPage.HistoryTable.GetGridItems().First().ClickLinkByText<AiJurisdictionalSurveysPage>(SurveyQuestion);
            SafeMethodExecutor.WaitUntil(() => !surveysPageFromHistory.ProgressLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "'Email me' button should NOT be displayed on survey result page from History",
                surveysPageFromHistory.EmailMeButton == null || !surveysPageFromHistory.EmailMeButton.Displayed,
                "'Email me' button IS displayed on survey result page from History, but it should NOT be.");
        }

        private AiJurisdictionalSurveysPage NavigateToLandingPage()
        {
            const string JurisdictionalLabel = "AI Jurisdictional Surveys";

            var homePage = this.GetHomePage<PrecisionHomePage>();
            AiJurisdictionalSurveysPage jurisdictionalSurveysPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(JurisdictionalLabel).Click<AiJurisdictionalSurveysPage>();
            
            BrowserPool.CurrentBrowser.CreateTab(JurisdictionalSurveysTab);
            BrowserPool.CurrentBrowser.ActivateTab(JurisdictionalSurveysTab);

            Thread.Sleep(2000);
            jurisdictionalSurveysPage.ClosePendoMessage();
            SafeMethodExecutor.ExecuteUntil(() => jurisdictionalSurveysPage.PageDescription.Displayed, timeoutFromSec: 10);

            return jurisdictionalSurveysPage;
        }

        private EdgeCommonBrowsePage NavigateToBrowsePage(string pageUri)
        {
            // Example pageUri passed from caller to go to Regulations browse page
            // const string RegulationsPageUri = @"/Browse/Home/Regulations?transitionType=Default&contextData=(sc.Default)";
            const string WlnSite = "https://1.next.";
            const string Domain = ".westlaw.com/";
            string environment = this.TestExecutionContext.TestEnvironment.Id.ToString();
            string browsePageUrl = $"{WlnSite}{environment}{Domain}{pageUri}";
            return BrowserPool.CurrentBrowser.GoToUrl<EdgeCommonBrowsePage>(browsePageUrl);
        }

        protected override void InitializeRoutingPageSettings()
        {
            if (this.TestContext.Properties["JurisdictionalSurveysDailyLimit"] != null)
                this.Settings.AppendValues(
                    EnvironmentConstants.AIJurisdictionalSurveysDailyLimit,
                    SettingUpdateOption.Append,
                    this.TestContext.Properties["JurisdictionalSurveysDailyLimit"]);
        }

        private string CleanTextForCompare(string text)
        {
            text = Regex.Replace(text, @"(^|\r?\n)\d+\.", string.Empty);
            text = text.Replace(" ", string.Empty).Replace(")", string.Empty).Replace("\r\n", string.Empty);
            text = Regex.Replace(text, @"GovernmentWorks\.\b.?", string.Empty).Replace($"WestlawAIJurisdictionalSurveysresults•Surveygenerated:{DateTime.Now:MMMMd,yyyy}WestlawPrecision.©{DateTime.Now:yyyy}ThomsonReuters.NoclaimtooriginalU.S.", string.Empty).Replace("•", string.Empty);

            return text;
        }

        private void PrepareTestFolder()
        {
            // If folder does not exitst, create it. If it exists, delete all contents.
            var researchOrganizerPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            if (!researchOrganizerPage.LeftFolder.FolderTree.IsFolderExist(AjsTestFolder))
                researchOrganizerPage.CreateNewFolder(AjsTestFolder);
            else
            {
                researchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(AjsTestFolder);
                researchOrganizerPage.ClearFolderGrid();
            }
            researchOrganizerPage.Header.ClickLogo<PrecisionHomePage>();
        }
    }
}
