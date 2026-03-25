namespace WestlawAdvantage.Tests.DeepResearch
{
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.HomePage;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Foldering;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Windows.Forms;
    using WestlawPrecision.Utilities;

    /// <summary>
    /// AI Deep Research tests (aka Guided Research)
    /// </summary>
    [TestClass]
    public class WlaDeepResearchTests : WlaDeepResearchBaseTest
    {
        private const string FeatureTestCategoryDeepResearch = "AiDeepResearch";
        private const string FeatureLongRunningTestCategoryDeepResearch = "AiDeepResearchLongRunning";
        private const string DeepResearchCoCounsel = "DeepResearchCoCounsel";

        /// <summary>
        /// Test AI Deep Research access points.
        /// User story: 2156413 2207726 Task: 2168148 2210756
        /// 1. Sign in WL Advantage 
        /// 2. Click AI Deep Research tab on home page
        /// 3. Check: Verify welcome message is displayed on home page AI Deep Research tab
        /// 4. Click AI Deep Research link from Tools flyout to open landing page in new tab
        /// 5. Check: Verify start new message is displayed on landing page from tools flyout
        /// 6. Close new tab, focus on home page tab and close Tools flyout dialog
        /// 7. Click AI Deep Research card under Key Features to open landing page in new tab
        /// 8. Check: Verify start new message is displayed on landing page from key features
        /// 9. Check: Verify Question button displayed on landing page
        /// 10.Click Claims Explorer card under Key Features to open Claims Explorer page in new tab
        /// 11.Click AI Deep Research link on Claims Explorer page to open landing page in new tab
        /// 12.Check: Verify start new message is displayed on landing page from Claims Explorer page
        /// 13.Check: Verify Deep Research message box is displayed on F1 home page
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchAccessPointsTest()
        {
            const string WelcomeMessage = "Welcome to Westlaw";
            const string StartNewMessage = "Start new Westlaw";
            const string AiClaimsExplorerTab = "Claims Explorer";

            // Home page: AI Deep Research tab
            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);
            string headerMessageDisplayed = deepResearchTabPanel.WelcomeComponent.WelcomeHeaderLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify welcome message is displayed on home page AI Deep Research tab ",
                headerMessageDisplayed.Contains(WelcomeMessage) && headerMessageDisplayed.Contains(AiDeepResearchName),
                "Welcome message is not displayed on home page AI Deep Research tab");

            // Home page: Tools flyout - AI Deep Research link
            PrecisionToolsDialog toolsDialog = homePage.Header.ExpandToolsFlyoutButton.Click<PrecisionToolsDialog>();
            AiDeepResearchPage deepResearchPage = toolsDialog.ToolLinks.First(link => link.Text.Contains(AiDeepResearchName)).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);
            headerMessageDisplayed = deepResearchTabPanel.WelcomeComponent.WelcomeHeaderLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify start new message is displayed on landing page from tools flyout",
                headerMessageDisplayed.Contains(StartNewMessage) && headerMessageDisplayed.Contains(AiDeepResearchName),
                "Start new message is not displayed on landing page from tools flyout");

            BrowserPool.CurrentBrowser.CloseTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);
            toolsDialog.CloseButton.Click();

            // Home page: Key features - AI Deep Research card
            deepResearchPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchName).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);
            headerMessageDisplayed = deepResearchTabPanel.WelcomeComponent.WelcomeHeaderLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify start new message is displayed on landing page from key features",
                headerMessageDisplayed.Contains(StartNewMessage) && headerMessageDisplayed.Contains(AiDeepResearchName),
                "Start new message is not displayed on landing page from key features");

            this.TestCaseVerify.IsTrue(
                "Verify Question button displayed on landing page",
                deepResearchPage.WelcomeComponent.InputComponent.SendButton.Displayed,
                "Question button not displayed on page");

            BrowserPool.CurrentBrowser.CloseTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            // Home page: Key features - Claim Explorer card
            var claimsExplorerPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiClaimsExplorerTab).Click<AiClaimsExplorerPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiClaimsExplorerTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiClaimsExplorerTab);

            SafeMethodExecutor.WaitUntil(() => claimsExplorerPage.Chat.AiDeepResearchLink.Displayed, 5);

            deepResearchPage = claimsExplorerPage.Chat.AiDeepResearchLink.Click<AiDeepResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);
            headerMessageDisplayed = deepResearchTabPanel.WelcomeComponent.WelcomeHeaderLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify start new message is displayed on landing page from Claims Explorer page",
                headerMessageDisplayed.Contains(StartNewMessage) && headerMessageDisplayed.Contains(AiDeepResearchName),
                "Start new message is not displayed on landing page from Claims Explorer page");

            //F1 Deep Research new home page
            var F1homePage = BrowserPool.CurrentBrowser.GoToPath<AdvantageNewHomePage>(F1HomePageLink);
            var aideepresearchPanel = F1homePage.SearchTabPanel.SetActiveTab<AIDeepResearchTabPanel>(AdvantageSearchTabs.AIDeepResearch);
            this.TestCaseVerify.IsTrue(
                 "Verify Deep Research message box is displayed on F1 home page",
                 aideepresearchPanel.QuestionTextArea.Displayed,
                 "Deep Research message box is not displayed on F1 home page");
        }

        /// <summary>
        /// Test AI Deep Research widgets: Juris selector, Email me, How DR works, Tips, and Agent time.
        /// User story: 2191178,2201352,2249903 Task: 2197360,2196538,2261448
        /// 1. Sign in WL Advantage 
        /// 2. Click AI Deep Research tab on home page
        /// 3. Check: Verify jurisdiction names displayed
        /// 4. Click jurisdiction button to open Jurisdiction options dialog
        /// 5. Check: Verify Jurisdiction options dialog opened
        /// 6. Check: Verify Report type has concise and expanded options
        /// 7. Check: Verify Email me when report is ready button displayed
        /// 8. Click on Email me when report is ready button and check: Verify Email me when report is ready is checked
        /// 8.Click How DR works button and check: Verify clicking how DR works opens dialog
        /// 9.Check: Verify clicking how DR works opens dialog with expected content
        /// 10.Click Tips for best results button and check: Verify clicking tips for best results opens dialog
        /// 11.Check: Verify clicking tips for best results opens dialog with expected content
        /// 12. Submit a question
        /// 13. Check: Verify report is launched in a separate tab with Email me checked
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchWidgetsTest()
        {
            const string HowDrWorksPartialContent = "Use it to accelerate thorough research. Don't use it as a replacement for thorough research.";
            const string AICourtRules = "Visit the AI Court Rules page to review the court rules, orders that update court rules";
            const string TipsPartialContent = "Choose the right AI Deep Research report type for your legal research needs";
            const string Query = "Is habitual felony offender sentence invalid if underlying convictions imposed without jurisdiction or vacated?";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);

            this.TestCaseVerify.IsFalse(
                "Verify jurisdiction names displayed",
                string.IsNullOrEmpty(deepResearchTabPanel.WelcomeComponent.InputComponent.JurisdictionButton.Text),
                "Jurisdiction names not displayed");

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            this.TestCaseVerify.IsTrue(
                "Verify Jurisdiction options dialog opened",
                jurisdictionDialog.ClearAllButton.Displayed,
                "Jurisdiction options dialog not opened");
            jurisdictionDialog.CancelButton.Click();
            
            this.TestCaseVerify.IsTrue(
                "Verify Report type has Concise and Expanded options",
                deepResearchTabPanel.WelcomeComponent.InputComponent.ReportTypeConciseLabel.Text.Equals("Concise") && deepResearchTabPanel.WelcomeComponent.InputComponent.ReportTypeExpandedLabel.Text.Equals("Expanded"),
                "Report type doesn't has Concise and Expanded options");

            deepResearchTabPanel.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);

            this.TestCaseVerify.IsTrue(
                "Verify 'Email me when report is ready' button displayed",
                deepResearchTabPanel.WelcomeComponent.InputFooterComponent.EmailMeButton.Displayed,
                "Email me when report is ready button not displayed");

            if (!deepResearchTabPanel.WelcomeComponent.InputFooterComponent.EmailMeCheckedLabel.Displayed)
                deepResearchTabPanel.WelcomeComponent.InputFooterComponent.EmailMeButton.Click();

            this.TestCaseVerify.IsTrue(
                "Verify 'Email me when report is ready' is checked",
                deepResearchTabPanel.WelcomeComponent.InputFooterComponent.EmailMeCheckedLabel.Displayed,
                "Email me when report is ready is not checked");

            var howDeepResearchWorksDialog = deepResearchTabPanel.WelcomeComponent.InputFooterComponent.HowDeepResearchWorksButton.Click<HowDeepResearchWorksDialog>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking how DR works opens dialog",
                howDeepResearchWorksDialog.TipsContent.Text.Contains(HowDrWorksPartialContent) && howDeepResearchWorksDialog.TipsContent.Text.Contains(AICourtRules),
                "Clicking how DR works does not open dialog");

            howDeepResearchWorksDialog.CloseButton.Click();

            var tipsForBestResultsDialog = deepResearchTabPanel.WelcomeComponent.InputFooterComponent.TipsForBestResultsButton.Click<TipsForBestResultsDialog>();
            this.TestCaseVerify.IsTrue(
                "Verify clicking tips for best results opens dialog",
                tipsForBestResultsDialog.TipsContent.Text.Contains(TipsPartialContent),
                "Clicking tips for best results does not open dialog");

            tipsForBestResultsDialog.CloseButton.Click();

            deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Query);
            var deepResearchPage = deepResearchTabPanel.WelcomeComponent.InputComponent.SendButton.Click<AiDeepResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                "Verify report is launched in a separate tab with Email me checked",
                deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed,
                "Report is not launched in a separate tab with Email me checked");
        }
       
        /// <summary>
        /// Test AI Deep Research able to verify DR report in history.
        /// User story: 2160173 Task: 2175434
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate concise report
        /// 4. Get the report date and time
        /// 5. Convert IST date and time to CST date and time
        /// 6. Refresh the page
        /// 7. Click on History menu from top and view all history
        /// 8. Check: Verify Deep research entry displayed in History
        /// 9. Check: Verify Deep research event displayed in History
        /// 10.Check: Verify history conversation displayed as per report date and time in full history
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchHistoryTest()
        {
            const string Query = "Is habitual felony offender sentence invalid if underlying convictions imposed without jurisdiction or vacated?";
            const string Jurisdictions = "All Federal";
            const string AIDeepResearchHistoryEventLabel = "AI Deep Research";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            ReportTab reportTab = new ReportTab();
            this.TestCaseVerify.IsTrue("Verify research report tab displayed",
                reportTab.RightColumnComponent.SummarySectionHeadingLabel.Text.Equals("Summary"),
                "Research report tab not displayed");

            string ReportDateTime = reportTab.LeftColumnComponent.ReportTimeLabel.Text;

            DateTime istDateTime = DateTime.ParseExact(ReportDateTime, "MMM dd, h:mm tt", CultureInfo.InvariantCulture);
            string formattedReportDate = istDateTime.ToString("MM/dd/yyyy h:mm tt", CultureInfo.InvariantCulture);

            DriverExtensions.RefreshPage();
            Thread.Sleep(5000);

            var historyPage = EdgeNavigationManager.Instance.GoToHistoryPage<EdgeCommonHistoryPage>();
            this.TestCaseVerify.IsTrue(
               "Verify Deep research entry displayed in History",
               historyPage.HistoryTable.GetGridItems().First().Title.Equals(Query),
               "Deep research entry not displayed in History");
            
            string historyEvent= historyPage.HistoryTable.GetFirstHistoryEvent();
            this.TestCaseVerify.IsTrue(
               "Verify Deep research event displayed in History",
               historyEvent.Equals(AIDeepResearchHistoryEventLabel),
               "Deep research event not displayed in History");

            string historyDate = historyPage.HistoryTable.GetGridItems().First().Date.ToString("MM/dd/yyyy h:mm tt");
            this.TestCaseVerify.IsTrue("Verify history conversation displayed as per report date and time in full history",
                historyDate.Equals(formattedReportDate),
                $"History conversation not displayed as per report date and time in full history Expected:{formattedReportDate}  Actual:{historyDate}");
        }

        /// <summary>
        /// Test AI Deep Research able to verify follow up and feedback.
        /// User story: 2216986 Task: 2216990
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate 3 min report
        /// 4. Check: verify research report tab displayed
        /// 5. Enter follow up question 
        /// 6. click Send button
        /// 7. Wait until follow up summary generated 
        /// 8. Check: Verify follow up summary displayed in left column
        /// 9. Check: Verify thumbs up button displayed in left column
        /// 10.Check: Verify thumbs down button displayed in left column
        /// 11. Click thumbs down button
        /// 12. Enter feedback in text area and submit
        /// 13. Check: Verify thumbs down button displayed in left column
        /// 14. Check: Verify feedback submitted and thumbs down button is disabled
        /// 15. Check: Verify thumbs up button not displayed in left column
        /// 16. Change Client ID and ask a follow-up question
        /// 17. Check: Verify follow-up block message displayed after client id change
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchFollowUpFeedbackTest()
        {
            const string Query = "Is habitual felony offender sentence invalid if underlying convictions imposed without jurisdiction or vacated?";
            const string Jurisdictions = "All Federal";
            const string FollowUpQuery = "Can a news article be used as a learned treatise at trial?";
            const string FollowUpMessage = "Ask follow-up question to clarify or expand on information in your report. Follow-ups won't generate new report versions.";
            const string Feedback = "Regression Test - feedback";
            const string FollowupBlockMessage = "Open that client to continue working on this report, or select Start new research to create a new report for the current client.";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            this.TestCaseVerify.IsTrue("Verify Report tab is displayed",
                deepResearchPage.ResultComponent.DeepResearchResultTabPanel.IsDisplayed(DeepResearchResultTabs.Report),
                "Report tab not displayed");

            var followUpTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<FollowUpTab>(DeepResearchResultTabs.FollowUp);
            Thread.Sleep(2000);
            this.TestCaseVerify.IsTrue("Verify Follow up message displayed in follow up tab",
                followUpTab.FollowUpMessageLabel.Text.Equals(FollowUpMessage),
                "Follow up message not displayed in follow up tab");

            followUpTab.EnterFollowUpQuestion(FollowUpQuery);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !followUpTab.FollowUpProgressBarLabel.Displayed);

            this.TestCaseVerify.IsTrue("Verify Follow up answer message displayed in follow up tab",
                followUpTab.FollowUpAnswerLabel.Displayed,
                "Follow up answer message not displayed in follow up tab");

            deepResearchPage.ScrollPageToBottom();
            this.TestCaseVerify.IsTrue("Verify thumbs up button displayed in left column",
                followUpTab.Feedback.ThumbsUpButton.Displayed,
                "Thumbs up button not displayed in left column");
            this.TestCaseVerify.IsTrue("Verify thumbs down button displayed in left column",
                followUpTab.Feedback.ThumbsDownButton.Displayed,
                "Thumbs down button not displayed in left column");

            followUpTab.Feedback.ThumbsDownButton.Click();
            followUpTab.Feedback.EnterFeedback(Feedback);
            deepResearchPage.ScrollPageToBottom();
            followUpTab.Feedback.SubmitFeedbackButton.Click();

            this.TestCaseVerify.IsTrue("Verify thumbs down button displayed in left column",
                followUpTab.Feedback.ThumbsDownButton.Displayed,
                "Thumbs down button not displayed in left column");
            this.TestCaseVerify.IsTrue("Verify feedback submitted and thumbs down button is disabled",
                followUpTab.Feedback.ThumbsDownButton.GetAttribute("class").Equals("disabled"),
                "Feedback not submitted and thumbs down button not disabled");
            this.TestCaseVerify.IsFalse("Verify thumbs up button not displayed in left column",
               followUpTab.Feedback.ThumbsUpButton.Displayed,
                "Thumbs up button displayed in left column");

            // Change client id. Follow-up should be blocked.
            var changeIdDialog = deepResearchPage.Header.OpenChangeClientIdDialog();
            deepResearchPage = changeIdDialog.EnterClientIdAndHitContinue<AiDeepResearchPage>("NewClientId");
            Thread.Sleep(3000);
            followUpTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<FollowUpTab>(DeepResearchResultTabs.FollowUp);

            followUpTab.EnterFollowUpQuestion(FollowUpQuery);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => followUpTab.AlertMessageLabel.Displayed, 100);
            
            this.TestCaseVerify.IsTrue("Verify follow-up block message displayed after client id change",
                followUpTab.AlertMessageLabel.Text.Contains(FollowupBlockMessage),
                "Follow-up block message not displayed after client id change");
        }

        /// <summary>
        /// Test AI Deep Research feedback on report page and block follow-up after client id change.
        /// User story: 2216986 Task: 2216990
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate concise report
        /// 4. Check: Verify thumbs up and down buttons displayed in right column
        /// 5. Click thumbs up button and submit feedback 
        /// 6. Check: Verify thumbs up button displayed in right column
        /// 7. Check: Verify thumbs up button is disabled after feedback submitted 
        /// 8. Check: Verify thumbs down button not displayed in right column
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchReportFeedbackTest()
        {
            const string Query = "Is habitual felony offender sentence invalid if underlying convictions imposed without jurisdiction or vacated?";
            const string Jurisdictions = "All Federal";
            const string Feedback = "Regression Test - feedback";
            
            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            deepResearchPage.ScrollPageToBottom();
            Thread.Sleep(2000);
            ReportTab reportTab = new ReportTab();
            this.TestCaseVerify.IsTrue("Verify thumbs up and down buttons displayed in right column",
                reportTab.RightColumnComponent.Feedback.ThumbsUpButton.Displayed &&
                reportTab.RightColumnComponent.Feedback.ThumbsDownButton.Displayed,
                "Thumbs up and down buttons not displayed in right column");

            reportTab.RightColumnComponent.Feedback.ThumbsUpButton.Click();

            reportTab.RightColumnComponent.Feedback.EnterFeedback(Feedback);
            deepResearchPage.ScrollPageToBottom();
            reportTab.RightColumnComponent.Feedback.SubmitFeedbackButton.Click();
            Thread.Sleep(2000);

            this.TestCaseVerify.IsTrue("Verify thumbs up button displayed in right column",
                reportTab.RightColumnComponent.Feedback.ThumbsUpButton.Displayed,
                "Thumbs up button not displayed in left column");

            this.TestCaseVerify.IsTrue("Verify thumbs up button is disabled after feedback submitted",
                reportTab.RightColumnComponent.Feedback.ThumbsUpButton.GetAttribute("class").Equals("disabled"),
                "Thumbs up button not disabled after feedback submitted");

            this.TestCaseVerify.IsFalse("Verify thumbs down button not displayed in right column",
                reportTab.RightColumnComponent.Feedback.ThumbsDownButton.Displayed,
                "Thumbs down button displayed in right column after thumbs up feedback submitted");
        }

        /// <summary>
        /// Test AI Deep Research items such as research steps,contents,citation and keycite links.
        /// User story: 2160173 2178687 Task: 2175434 2208131
        /// 1. Sign in WL Advantage and view AI Deep Reseearch tab          
        /// 2. Select report type as concise
        /// 3. Set jurisdiction to Minnesota
        /// 4. Enter question and click Send button: Can there be a failure to hire discrimination...
        /// 5. Check: Verify user selected jurisdiction displayed on report
        /// 9. Check: Verify Research contents header displayed
        /// 10.Click last item in Research contents
        /// 11.Check: Verify clicking last TOC item jumps to section on right panel
        /// 12.Click the last citation link in Research report tab
        /// 13.Click citation title link in Citation dialog
        /// 14.Check: Verify clicking citation link opens document in new tab
        /// 15.Close document tab and focus on Deep Research tab
        /// 16.Refresh Deep Research tab
        /// 17.Click the last KeyCite flag link in Research report tab
        /// 18.Click KeyCite flag link in Citation dialog
        /// 19.Check: Verify clicking KeyCite flag opens Negative Treatment or History tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchReportItemsTest()
        {
            const string DocumentTab = "Document tab";
            const string Query = "Can a trial court extend the deadline to file an opposition to a motion for summary judgment without all parties consent?";
            const string Jurisdictions = "Minnesota";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            ReportTab reportTab = new ReportTab();
            string displayedJurisdictions = reportTab.LeftColumnComponent.JurisdictionLabel.Text;
            this.TestCaseVerify.IsTrue("Verify user selected jurisdiction displayed on report",
                displayedJurisdictions.Contains(Jurisdictions),
                $"User selected jurisdiction not displayed on report. Displayed: {displayedJurisdictions} Expected: {Jurisdictions}");

            this.TestCaseVerify.IsTrue("Verify summary is displayed in report tab",
                reportTab.RightColumnComponent.SummarySectionHeadingLabel.Text.Equals("Summary"),
                "Summary is not displayed in report tab");

            string reportContentHeader = reportTab.LeftColumnComponent.ReportContentHeader.Text;
            this.TestCaseVerify.IsTrue("Verify Report Contents header displayed",
                reportContentHeader.Equals("Report contents"),
                "Report Contents header not displayed");

            string lastTocTitle = reportTab.LeftColumnComponent.ReportContentsList.Last().Text;
            reportTab.LeftColumnComponent.TocTitleLink(lastTocTitle).Last().Click();
            Thread.Sleep(2000);
            
            this.TestCaseVerify.IsTrue("Verify clicking last TOC item jumps to section on right panel",
                reportTab.RightColumnComponent.IsSectionScrolledIntoView(lastTocTitle),
                "Clicking last TOC item does not jump to section on right panel");

            CitationDialog citationDialog = reportTab.RightColumnComponent.CitationLinks.Last().Click<CitationDialog>();
            Thread.Sleep(1000);
            var documentPage = citationDialog.CitationTitleLink.Last().Click<EdgeCommonDocumentPage>();
            BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);
            SafeMethodExecutor.WaitUntil(() => documentPage.IsDocumentLoaded(), 10);

            this.TestCaseVerify.IsTrue("Verify clicking citation link opens document in new tab",
                documentPage.RiTabs.GetSelectedTab().Equals(RiTab.Document),
                "Clicking citation link does not open document in new tab");

            BrowserPool.CurrentBrowser.CloseTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);

            deepResearchPage = BrowserPool.CurrentBrowser.Refresh<AiDeepResearchPage>();
            SafeMethodExecutor.WaitUntil(() => reportTab.RightColumnComponent.SummarySectionHeadingLabel.Displayed, 10);
            
            citationDialog = reportTab.RightColumnComponent.KeyCiteFlagLinks.First().Click<CitationDialog>();
            Thread.Sleep(1000);
            var negativeTreatmentPage = citationDialog.KeyCiteFlagLink.First().Click<EdgeNegativeTreatmentPage>();
            BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);
            SafeMethodExecutor.WaitUntil(() => negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History), 10);

            this.TestCaseVerify.IsTrue("Verify clicking KeyCite flag opens Negative Treatment or History tab",
                negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
                "Clicking KeyCite flag does not open Negative Treatment or History tab");
        }

        /// <summary>
        /// Test AI Deep Research limiting up to 3 concurrent searches.
        /// User story: 2184580 Task: 2206314
        /// 1. Sign in WL Advantage and open 4 landing page tabs        
        /// 2. Submit a question on each tab 
        /// 3. Check: Verify limit warning message displayed for more than 3 concurrent searches
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchConcurrentSearchesLimitTest()
        {
            const string Question = "How to show waiver of arbitration rights?";
            const string Jurisdiction = "All Federal";
            const string WarningMessage = "You can submit this question once one of your 3 current questions has finished processing.";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchName).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);

            var deepResearchPageUrl = BrowserPool.CurrentBrowser.Url;

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Question);
            
            deepResearchPage.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);
            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(Jurisdiction);
            jurisdictionDialog.SaveButton.Click();
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiDeepResearchTab}Second", deepResearchPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiDeepResearchTab}Second");
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Question);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiDeepResearchTab}Third", deepResearchPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiDeepResearchTab}Third");
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Question);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiDeepResearchTab}Fourth", deepResearchPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiDeepResearchTab}Fourth");
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 5);

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Question);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.AlertMessageLabel.Displayed, 5);

            this.TestCaseVerify.AreEqual("Verify limit warning message displayed for more than 3 concurrent searches",
                WarningMessage,
                deepResearchPage.ResultComponent.SingleColumnComponent.AlertMessageLabel.Text,
                "Limit concurrent searches warning message is NOT displayed for more than 3 concurrent searches");

            // Go back to previous search and wait until report generated
            BrowserTabManager.Instance.SetTabActive($"{AiDeepResearchTab}Third");
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
        }

        /// <summary>
        /// Test AI Deep Research able to verify simple response and forced report.
        /// User story: 2221364 2199511 Task: 2221368 2210769
        /// 1. Sign in WL Advantage          
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Generate concise report for simple question
        /// 4. Check: verify report tab not displayed
        /// 5. Check: verify download report button displayed
        /// 6. Check: verify asking follow-up is disabled
        /// 7. Click first citation link in single column result
        /// 8. Check: Verify clicking citation link opens document in same tab
        /// 9. Navigate back to Deep Research report page
        /// 10.Check: Verify Force Report link is present
        /// 11.Click Force Report link
        /// 12.Wait until report generated
        /// 13.Check: verify research report tab displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchSimpleQuestionForcedReportTest()
        {
            const string Query = "What is a trade secret?";
            const string Jurisdiction = "California";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);

            deepResearchTabPanel.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);
            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(Jurisdiction);
            jurisdictionDialog.SaveButton.Click();

            deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Query);
            var deepResearchPage = deepResearchTabPanel.WelcomeComponent.InputComponent.SendButton.Click<AiDeepResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            this.TestCaseVerify.IsFalse("Verify Report tab not displayed",
                deepResearchPage.ResultComponent.DeepResearchResultTabPanel.IsDisplayed(DeepResearchResultTabs.Report),
                "Report tab displayed");

            this.TestCaseVerify.IsTrue("Verify download report button displayed",
                deepResearchPage.ResultComponent.SingleColumnComponent.DownloadReportButton.Displayed,
                "Download report button not displayed");

            var documentPage = deepResearchPage.ResultComponent.SingleColumnComponent.CitationLinks.First().Click<EdgeCommonDocumentPage>();
            SafeMethodExecutor.WaitUntil(() => documentPage.IsDocumentLoaded(), 10);

            this.TestCaseVerify.IsTrue("Verify clicking citation link opens document in same tab",
                documentPage.RiTabs.GetSelectedTab().Equals(RiTab.Document),
                "Clicking citation link does not open document in same tab");
            BrowserPool.CurrentBrowser.GoBack();

            this.TestCaseVerify.IsTrue("Verify Force Report link is present",
                deepResearchPage.ResultComponent.SingleColumnComponent.GenerateFullReportButton.Displayed,
                "Force Report link is not present");
            deepResearchPage.ResultComponent.SingleColumnComponent.GenerateFullReportButton.Click();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed, 300);
            ReportTab reportTab = new ReportTab();
            this.TestCaseVerify.IsTrue("Verify summary is displayed in report tab",
                reportTab.RightColumnComponent.SummarySectionHeadingLabel.Text.Equals("Summary"),
                "Summary is not displayed in report tab");
        }

        /// <summary>
        /// Test AI Deep Research able to verify out of scope message
        /// User story: 2221364 2200610, 2260906 Task: 2221368 2209353, 2261451
        /// 1. Sign in WL Advantage          
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Generate concise report for out of scope question
        /// 4. Check: verify research report tab not displayed
        /// 5. Check: verify out of scope message displayed
        /// 6. Click Start new research button and enter question with less than min length (7)
        /// 7. Check: Verify Send button disabled entering less than minimum query length
        /// 8. Click Start new research button and enter question with more than max length (2000)
        /// 9. Check: Verify input error displayed when query is over max 2000
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchOutOfScopeMinMaxQueryMessageTest()
        {
            const string OutOfScopeQuery = "What is your name?";
            const string MinLengthQuery = "What's"; // 6 characters, minimum is 7
            string MaxLengthQuery = File.ReadAllText(Environment.CurrentDirectory + @"\MaxInput2000.txt");
            const string Jurisdictions = "Minnesota";
            const string OutOfScopeMessage = "Your question is outside the scope of this feature.";
            const string InputErrorMessage = "Questions must be 2,000 characters or less. Any text after that limit has been removed.";
            
            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(OutOfScopeQuery, Jurisdictions);

            this.TestCaseVerify.IsFalse("Verify Report tab not displayed",
                deepResearchPage.ResultComponent.DeepResearchResultTabPanel.IsDisplayed(DeepResearchResultTabs.Report),
                "Report tab displayed");

            string outOfScopeText = deepResearchPage.ResultComponent.SingleColumnComponent.OutOfScopeMessageLabel.Text;
            this.TestCaseVerify.IsTrue("Verify out of scope message displayed",
                outOfScopeText.Contains(OutOfScopeMessage),
                "Out of scope message not displayed");

            deepResearchPage = deepResearchPage.DeepResearchHeader.NewResearchButton.Click<AiDeepResearchPage>();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 2);
            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(MinLengthQuery);
            Thread.Sleep(1000);

            this.TestCaseVerify.IsTrue("Verify Send button disabled entering less than minimum query length",
                deepResearchPage.WelcomeComponent.InputComponent.SendButton.Displayed,
                "Send button not disabled entering less than minimum query length");

            deepResearchPage.WelcomeComponent.InputComponent.ClearQuestionTextArea();
            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(MaxLengthQuery);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.WelcomeComponent.InputValidationErrorLabel.Displayed, 2);

            string displayedError = deepResearchPage.WelcomeComponent.InputValidationErrorLabel.Text;
            this.TestCaseVerify.IsTrue("Verify input error displayed when query is over max 2000",
                displayedError.Contains(InputErrorMessage),
                $"Input error not displayed when query is over max 2000. Displayed: {displayedError} Expected: {InputErrorMessage}");
        }
       
        /// <summary>
        /// Test AI Deep Research Report type: Expanded report.
        /// User story: 2160173 Task: 2175434
        /// 1. Sign in WL Advantage          
        /// 2. Submit to run an expanded report
        /// 3. Check: Verify expanded report generated successfully
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureLongRunningTestCategoryDeepResearch)]
        public void DeepResearchExpandedReportTest()
        {
            const string Jurisdictions = "All Federal";
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions, ReportType.Expanded);

            this.TestCaseVerify.IsTrue(
                "Verify expanded report generated successfully",
                deepResearchPage.ResultComponent.DeepResearchResultTabPanel.IsDisplayed(DeepResearchResultTabs.Report),
                "Expanded report not generated successfully");
        }

        /// <summary>
        /// Test AI Deep Research running a deeeper report.
        /// User story: 2160173, 2239413 Task: 2175434, 2240410
        /// 1. Sign in WL Advantage          
        /// 2. Run a non-simple question with concise report
        /// 3. Check: Verify expanded report option displayed
        /// 4. Click Run an expanded report button
        /// 5. Check: Verify expanded report tab opened in new tab
        /// 5. Check: Verify expanded report has started 
        /// 6. Wait for report to complete
        /// 7. Check: Verify expanded report has completed with expanded report type
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureLongRunningTestCategoryDeepResearch)]
        public void DeepResearchRunAnExpandedReportTest()
        {
            const string Jurisdictions = "All Federal";
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";
            const string ExpandedReportLabel = "Expanded report";
            const string ExpandedReportTab = "Expanded Report Tab";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            ReportTab reportTab = new ReportTab();
            deepResearchPage.ScrollPageToBottom();
            
            this.TestCaseVerify.IsTrue(
                "Verify expanded option displayed",
                reportTab.RightColumnComponent.ExpandedReportButton.Text.Equals("Run an Expanded report"),
                "Expanded option not displayed");

            deepResearchPage = reportTab.RightColumnComponent.ExpandedReportButton.Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(ExpandedReportTab);
            BrowserPool.CurrentBrowser.ActivateTab(ExpandedReportTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed, 30);

            this.TestCaseVerify.IsTrue(
                "Verify Expanded report generation has started",
                deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed,
                "Expanded report generation has not started");

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            string reportTypeDisplayed = reportTab.LeftColumnComponent.ReportTypeLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify expanded report has completed with expanded report type",
                reportTypeDisplayed.Contains(ExpandedReportLabel),
                $"Expanded report has not completed with expanded report type. Displayed: {reportTypeDisplayed}");
        }

        /// <summary>
        /// Test AI Deep Research running a search in CoCounsel.
        /// User story: 2200720 Task: 2211594
        /// 1. Sign in WL Advantage and open CoCounsel chat assistant dialog        
        /// 2. Enter a query and submit
        /// 3. Select Westlaw AI Deep Research radio button if present and click Start button
        /// 4. Check: Verify Verify Deep Research response returned
        /// 5. Check: Verify View all cited sources button displayed 
        /// 6. Click View all cited sources button
        /// 7. Check: Verify supporting sources displayed
        /// 8. Click the first KeyCite flag link from supporting source list
        /// 9. Check: Verify clicking KeyCite flag opens Negative Treatment or History tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureLongRunningTestCategoryDeepResearch)]
        [TestCategory(DeepResearchCoCounsel)]
        public void DeepResearchCoCounselTest()
        {
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";

            this.GetHomePage<AdvantageHomePage>();
            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();
            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Query);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();
            Thread.Sleep(9000);
            if (coCounselChatAssistantDialog.Chat.AiDeepResearchRadioButton.Displayed)
            {
                coCounselChatAssistantDialog.Chat.AiDeepResearchRadioButton.Select();
                coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiDeepResearchButton.Click<CoCounselChatAssistantDialog>();
                Thread.Sleep(9000);
            }
            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.DRProgressBarLabel.Displayed);
            coCounselChatAssistantDialog.Chat.ViewAllDRCitedSourcesButton.ScrollToElement();

            this.TestCaseVerify.IsTrue(
                "Verify Deep Research response returned",
                coCounselChatAssistantDialog.Chat.CitationLinks.Count > 0,
                "Deep Research response not returned");

            this.TestCaseVerify.IsTrue(
                "Verify View all cited sources button displayed",
                coCounselChatAssistantDialog.Chat.ViewAllDRCitedSourcesButton.Displayed,
                "View all cited sources button not displayed");

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.ViewAllDRCitedSourcesButton.Click<CoCounselChatAssistantDialog>();
            Thread.Sleep(2000);

            this.TestCaseVerify.IsTrue(
                "Verify supporting sources displayed",
                coCounselChatAssistantDialog.Chat.DRSourcesLabel.Displayed,
                "Supporting sources not displayed");

            var negativeTreatmentPage = coCounselChatAssistantDialog.Chat.SourcesFlagLinks.First().Click<EdgeNegativeTreatmentPage>();
            Thread.Sleep(2000);

            this.TestCaseVerify.IsTrue("Verify clicking KeyCite flag opens Negative Treatment or History tab",
                negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.NegativeTreatment)
                || negativeTreatmentPage.RiTabs.GetSelectedTab().Equals(RiTab.History),
                "Clicking KeyCite flag does not open Negative Treatment or History tab");
        }

        /// <summary>
        /// Test AI Deep Research honors the 5 follow-up questions limit and displays a message.
        /// User story: 2216986 Task: 2216990
        /// 1. Sign in WL Advantage and generate a Deep Research report         
        /// 2. Ask 2 follow up questions
        /// 3. Answer CQ to generate 2 new report version
        /// 4. Ask follow up question
        /// 5. Click on Verify Report button
        /// 6. Check: verify limit message displayed for the 6th followup/Ehance/Verify
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearch5FollowupEnhanceVerifyLimitTest()
        {
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";
            const string Jurisdictions = "Minnesota";
            const string FollowUpQuery1 = "Can the fraudulent omission be established independently of the contractual obligations between the manufacturer and my client?";
            const string FollowUpQuery2 = "Did the manufacturer's fraudulent omission expose my client to a risk of harm beyond what was reasonably contemplated when entering the contract?";
            const string FollowUpQuery3 = "Can my employer discipline me for wearing a union button?";
            const string FollowupEnhanceVerifyLimitMessage = "You have reached the limit of 5 updates to this report.";
            const string Answer = "Yes";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            this.TestCaseVerify.IsTrue("Verify Report tab is displayed",
                deepResearchPage.ResultComponent.DeepResearchResultTabPanel.IsDisplayed(DeepResearchResultTabs.Report),
                "Report tab not displayed");

            var followUpTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<FollowUpTab>(DeepResearchResultTabs.FollowUp);
            Thread.Sleep(2000);

            followUpTab.EnterFollowUpQuestion(FollowUpQuery1);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !followUpTab.FollowUpProgressBarLabel.Displayed);
           
            followUpTab.EnterFollowUpQuestion(FollowUpQuery2);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !followUpTab.FollowUpProgressBarLabel.Displayed);
            
            var enhanceTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<EnhanceTab>(DeepResearchResultTabs.Enhance);

            enhanceTab.EnterAnswerToFirstClarifyingQuestion(Answer);
            enhanceTab.GenerateNewReportButton.ScrollToElement();
            var deepResearchSecondVersionPage = enhanceTab.GenerateNewReportButton.Click<AiDeepResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !deepResearchSecondVersionPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            var reportTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<ReportTab>(DeepResearchResultTabs.Report);
            this.TestCaseVerify.IsTrue("Verify summary is displayed in report tab",
                reportTab.RightColumnComponent.SummarySectionHeadingLabel.Text.Equals("Summary"),
                "Summary is not displayed in report tab");
            
            enhanceTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<EnhanceTab>(DeepResearchResultTabs.Enhance);

            enhanceTab.EnterAnswerToFirstClarifyingQuestion(Answer);
            enhanceTab.GenerateNewReportButton.ScrollToElement();
            var deepResearchThirdVersionPage = enhanceTab.GenerateNewReportButton.Click<AiDeepResearchPage>();
            SafeMethodExecutor.WaitUntil(() => !deepResearchThirdVersionPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            
            followUpTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<FollowUpTab>(DeepResearchResultTabs.FollowUp);
            Thread.Sleep(2000);

            followUpTab.EnterFollowUpQuestion(FollowUpQuery3);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !followUpTab.FollowUpProgressBarLabel.Displayed);
            
            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();
            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);
            
            this.TestCaseVerify.IsTrue("Verify block message displayed asking the 6th follow-up/enhance/Verify question",
                deepResearchPage.ResultComponent.SingleColumnComponent.AlertMessageLabel.Text.Contains(FollowupEnhanceVerifyLimitMessage),
                "Block message not displayed asking the 6th follow-up/enhance/Verify question");
        }

        /// <summary>
        /// Test AI Deep Research honors daily limit for Initial query and displays a message.
        /// User story: 2216986 Task: 2216990
        /// 1. Sign in WL Advantage and set daily limit to 1 on routing page    
        /// 2. Ask 1 question, start new and ask a second question
        /// 3. Check: verify daily limit message displayed when asking the second question
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestProperty("DailyLimitTest","Yes")]
        public void DeepResearchDailyLimitTest()
        {
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";
            const string Jurisdiction = "Minnesota";
            const string DailyLimitMessage = "You've reached the daily limit of queries in AI Deep Research. This limit resets every night at 12:00 a.m. Central time.";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchPage = homePage.FeaturesIncludedPanel.GetWidgetLinkByTitle(AiDeepResearchName).Click<AiDeepResearchPage>();
            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 10);
            deepResearchPage.UsageDebug.BackDateExpiryButton.Click();

            var deepResearchPageUrl = BrowserPool.CurrentBrowser.Url;
            
            deepResearchPage.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(Jurisdiction);
            jurisdictionDialog.SaveButton.Click();

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Query);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            BrowserTabManager.Instance.OpenUrlInNewTab($"{AiDeepResearchTab} Second", deepResearchPageUrl);
            BrowserTabManager.Instance.SetTabActive($"{AiDeepResearchTab} Second");
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 10);

            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Query);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SingleColumnComponent.AlertMessageLabel.Displayed, 10);

            this.TestCaseVerify.IsTrue("Verify daily limit message displayed",
                deepResearchPage.ResultComponent.SingleColumnComponent.AlertMessageLabel.Text.Contains(DailyLimitMessage),
                "Daily limit message not displayed");

            // Set date back to get out of limit block
            deepResearchPage.UsageDebug.BackDateExpiryButton.Click();
        }

        /// <summary>
        /// Test AI Deep Research accessible from Browse content in responsive mode and report behavior.
        /// User story: 2206028 2206014 Task: 2231140 2210589
        /// 1. Sign in WL Advantage 
        /// 2. Reduce browser width to 600px width to enable responsive view
        /// 3. Click AI Deep Research link from Browse Content section
        /// 4. Check: Verify clicking DR link under browse content takes to Deep Research landing page
        /// 5. Maximize browser window and generate a 3 min report
        /// 6.Reduce browser width to 612px
        /// 7 Check: Verify tabs dropdown displayed with at 612px width
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestProperty("ResponsiveTest", "Yes")]
        public void DeepResearchResponsiveTest()
        {
            const string Question = "When can insurers be bound by a completed policy application?";
            const string Jurisdiction = "All Federal";
            const string WelcomeMessage = "Start new Westlaw";
            const int BrowserHeight = 1000;

            var homePage = this.GetHomePage<AdvantageHomePage>();
            BrowserPool.CurrentBrowser.SetWindowSize(600, BrowserHeight);//600px width to display AI Deep Research link

            AiDeepResearchPage deepResearchPage = homePage.BrowseContentLink(AiDeepResearchName).Click<AiDeepResearchPage>();
            string headerMessageDisplayed = deepResearchPage.WelcomeComponent.WelcomeHeaderLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify clicking DR link under browse content takes to Deep Research landing page",
                headerMessageDisplayed.Contains(WelcomeMessage) && headerMessageDisplayed.Contains(AiDeepResearchName),
                "Clicking DR link under browse content does not take to Deep Research landing page");

            BrowserPool.CurrentBrowser.Maximize();
            Thread.Sleep(2000);
            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Question);

            deepResearchPage.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchPage.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(Jurisdiction);
            jurisdictionDialog.SaveButton.Click();
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
             
            ReportTab reportTab = new ReportTab();
            
            BrowserPool.CurrentBrowser.SetWindowSize(612, BrowserHeight);//612px width to change tabs to dropdown           
            this.TestCaseVerify.IsTrue(
                "Verify tabs dropdown displayed with at 612px width",
                reportTab.RightColumnComponent.IsTabsDropDownDisplayed(),
                "Tabs dropdown not displayed with at 612px width");

            BrowserPool.CurrentBrowser.Maximize();
        }

        protected override void InitializeRoutingPageSettings()
        {
            base.InitializeRoutingPageSettings();
            
            if ((this.TestContext.Properties["DailyLimitTest"] != null) &&
                (this.TestContext.Properties["DailyLimitTest"].Equals("Yes")))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.AIGuidedResearchDailyLimit,
                    SettingUpdateOption.Append, "1");

                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-AI-ASSISTANT-USAGE-LIMITS-DEBUG",
                    "IAC-ENABLE-CARI-SESSION-ROUTING");
            }

            if ((this.TestContext.Properties["ResponsiveTest"] != null) &&
                (this.TestContext.Properties["ResponsiveTest"].Equals("Yes")))
            {
                this.Settings.AppendValues(
                    EnvironmentConstants.InfrastructureAccessControlsOn,
                    SettingUpdateOption.Append,
                    "IAC-WESTLAW-RESPONSIVE-DOCUMENT",
                    "IAC-ADVANTAGE-RESPONSIVE-DEEPRESEARCH");
            }
        }

        /// <summary>
        /// Test AI Deep Research able to verify jurisdiction resolution message
        /// User story: 2221364 Task: 2221368
        /// 1. Sign in WL Advantage          
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Enter query that does not match selected jurisdiction and generate concise report
        /// 4. Check: Verify jurisdiction resolution message displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchJurisdictionResolutionMessageTest()
        {
            const string Query = "Is reliance a necessary element for a fraud by nondisclosure claim in Texas?";
            const string Jurisdictions = "California";
            const string JurisdictionResolutionMessage1 = "Based on your question, this response is focused on";
            const string JurisdictionResolutionMessage2 = "not the original selection of";

            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);

            deepResearchTabPanel.WelcomeComponent.InputComponent.SelectReportType(ReportType.Concise);

            DeepResearchJurisdictionDialog jurisdictionDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.JurisdictionButton.Click<DeepResearchJurisdictionDialog>();
            jurisdictionDialog.SelectJurisdiction(Jurisdictions);
            jurisdictionDialog.SaveButton.Click();

            deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(Query);
            var deepResearchPage = deepResearchTabPanel.WelcomeComponent.InputComponent.SendButton.Click<AiDeepResearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(AiDeepResearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(AiDeepResearchTab);
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var researchStepsTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<ResearchStepsTab>(DeepResearchResultTabs.ResearchSteps);

            string jurisdictionResolutionText = researchStepsTab.JurisdictionResolutionMessageLabel.Text;
            this.TestCaseVerify.IsTrue("Verify Jurisdiction Resolution message displayed",
                jurisdictionResolutionText.Contains(JurisdictionResolutionMessage1) && jurisdictionResolutionText.Contains(JurisdictionResolutionMessage2) && jurisdictionResolutionText.Contains(Jurisdictions),
                "Jurisdiction Resolution message not displayed. Actual message displayed as:" + jurisdictionResolutionText);
        }

        /// <summary>
        /// Test AI Deep Research delivery
        /// User story: 2198795, Task: 2205915
        /// 1. Sign in to WL Advantage
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Navigate to AI Deep Research page in a new tab
        /// 4. Set agent time to ~3 min
        /// 5. Set jurisdiction to All Federal
        /// 6. Enter question and click Send button: "Can an attorney be sanctioned for submitting frivolous Freedom of Information Act requests?"
        /// 7. Check: Verify download report button displayed
        /// 8. Click on the download report button
        /// 9. Check: Verify downloaded report contains search query
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchDeliveryTest()
        {
            const string Query = "Can an attorney be sanctioned for submitting frivolous Freedom of Information Act requests?";
            const string Jurisdictions = "All Federal";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            this.TestCaseVerify.IsTrue("Verify download report button displayed",
                deepResearchPage.ResultComponent.ToolBar.DownloadReportButton.Displayed, 
                "Download report button is not displayed");

            deepResearchPage.ResultComponent.ToolBar.DownloadReportButton.Click();

            var fileName = $"{Query}.docx".Replace("?", "");
            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);
            var text = DocxTextExtractor.ExtractTextFromWord(Path.Combine(this.FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                "Verify delivery contains search query",
                text.Contains(Query),
                "Delivery does not contain search query");
        }

        /// <summary>
        /// Test AI Deep Research Cases on Both Sides section and Tab
        /// User story: 2226121,2226123, 2228026 Task: 2238576, 2238585, 2235107
        /// 1. Sign in to WL Advantage
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Generate 10 mins report
        /// 4. Scroll to Cases on Both Sides section
        /// 5. Check: Verify COBS Title displayed
        /// 6. Check: Verify COBS Table displayed
        /// 7. Check: Verify Show more button displayed on COBS section
        /// 8. Click Show more button
        /// 9. Check: Verify it navigates to COBS Tabs and Cases on Both Sides title is displayed on COBS tab
        /// 10. Check: Verify COBS filter case in favor is displayed
        /// 11. Check: Verify COBS filter case in against is displayed
        /// 12. Click COBS filter case in favor
        /// 13. Check: Verify only one column is displayed
        /// 14. Check: Verify COBS filter case in favor expands to fill both columns
        /// 15. click cross button to clear filter
        /// 16. Check: Verify two columns are displayed
        /// 17. Check: Verify COBS filter case in favor is displayed
        /// 18. Check: Verify COBS filter case in against is displayed
        /// </summary>
        // NOTE: Set IAC (IAC-AI-GUIDED-RESEARCH-MORE-CASES) values until they are turned on by default
        // In Jenkins script (for scheduled runs)
        // In Resources.LocalTestConfig.xml (to run locally):
        //<add key = "IACS_ON" value= "IAC-AI-GUIDED-RESEARCH-MORE-CASES" />
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchCasesOnBothSidesTest()
        {
            const string Query = "Should motions to dismiss be resolved before class certifications in class action lawsuits?";
            const string Jurisdictions = "All Federal";
            const string COBSTitle = "Cases on both sides";
            const string ShowMoreLabel = "Show more";
            
            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions, ReportType.Expanded);

            deepResearchPage.ResultComponent.RightColumnComponent.ScrollToCOBSTitle();
            TestCaseVerify.IsTrue("Verify Cases on Both Sides Title is displayed",
                deepResearchPage.ResultComponent.RightColumnComponent.COBSTitleLabel.Displayed,
                "Cases on Both Sides Title is not displayed");
            TestCaseVerify.IsTrue("Verify Cases on Both Sides Table is displayed",
                deepResearchPage.ResultComponent.RightColumnComponent.COBSTableLabel.Displayed,
                "Cases on Both Sides Table is not displayed");
            TestCaseVerify.IsTrue("Verify Show more button displayed on COBS section",
                deepResearchPage.ResultComponent.RightColumnComponent.ShowMoreButton.Text.Equals(ShowMoreLabel),
                "Show more button not displayed on COBS section");
            deepResearchPage.ResultComponent.RightColumnComponent.ShowMoreButton.Click();

            CasesOnBothSidesTab COBSTab= new CasesOnBothSidesTab();
            TestCaseVerify.IsTrue("Verify show more button navigates to COBS Tabs and COBS title is displayed under COBS tab",
                COBSTab.COBSTitleLabel.Text.Equals(COBSTitle),
                "Cases on Both Sides title is not displayed on COBS Tab");          

            TestCaseVerify.IsTrue("Verify COBS filter cases in favor displayed",
                COBSTab.CasesInFavorButton.Displayed,
                "COBS filter cases in favor not displayed");
            TestCaseVerify.IsTrue("Verify COBS filter cases in against displayed",
                COBSTab.CasesAgainstButton.Displayed,
                "COBS filter cases in against is not displayed");

            COBSTab.CasesInFavorButton.Click();
            TestCaseVerify.IsTrue("Verify only one column is displayed",
                COBSTab.COBSColumnsLabel.Count.Equals(1),
                "One column not displayed");
            TestCaseVerify.IsTrue("Verify COBS filter cases in favor expands to fill both columns",
                COBSTab.COBSColumnsLabel.First().Displayed,
                "COBS filter cases in favor doesn't expand to fill both columns");

            COBSTab.ClickXButton();
            TestCaseVerify.IsTrue("Verify two columns are displayed",
                COBSTab.COBSColumnsLabel.Count.Equals(2),
                "Two columns are not displayed");
            TestCaseVerify.IsTrue("Verify COBS filter cases in favor displayed",
                COBSTab.COBSColumnsLabel.First().Displayed,
                "COBS filter cases in favor not displayed");
            TestCaseVerify.IsTrue("Verify COBS filter cases against displayed",
                COBSTab.COBSColumnsLabel.Last().Displayed,
                "COBS filter cases against not displayed");
        }

        /// <summary>
        /// Test AI Deep Research clarifying questions enhance tab
        /// User story: 2192353, 2193898, 2193903, 2238204, 2260705 Task: 2197777, 2237261, 2238442, 2239252, 2261445
        /// 1. Sign in to WL Advantage
        /// 2. Click Deep AI Research card under Key Features on the home page
        /// 3. Generate 3 mins report
        /// 4. click on Enhance tab
        /// 5. Check: Verify clarifying question bottom text not displayed
        /// 6. Check: Verify clear response button is disabled
        /// 7. Check: Verify generate new report button is disabled
        /// 8. Check: Verify answer text area is displayed
        /// 9. Enter answer to first clarifying question
        /// 10. Check: Verify answer is displayed in text area
        /// 11. Click clear response button
        /// 12. Check: Verify answer text area is cleared
        /// 13. Enter answer to first clarifying question
        /// 14. Click generate new report button
        /// 15. Check: Verify the query is truncated to 19 characters and displayed in the left column
        /// 16. Wait for report to complete
        /// 17. Check: Verify clear response button is disabled
        /// 18. Check: Verify generate new report button is disabled
        /// 19. Click V2 report button
        /// 20. Check: Verify research report content displayed
        /// 21. Check: Verify report research steps displayed
        /// 22. Check: Verify report research content displayed
        /// 23. Click last TOC item
        /// 24. Check: Verify clicking last TOC item jumps to correct section on right column
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchEnhanceTest()
        {
            const string Query = "Can you object to a demand for production on the grounds that documents are equally available to the propounding party?";
            const string Jurisdictions = "California";
            const string Answer = "Yes";
            
            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            var enhanceTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<EnhanceTab>(DeepResearchResultTabs.Enhance);
            
            TestCaseVerify.IsTrue("Verify generate new report button is disabled",
                enhanceTab.GenerateNewReportButton.GetAttribute("class").Contains("disabled"),
                "Generate new report button is not disabled");
            TestCaseVerify.IsTrue("Verify text box is displayed",
                enhanceTab.AnswerTextArea.First().Displayed,
                "Text box is not displayed");

            enhanceTab.EnterAnswerToFirstClarifyingQuestion(Answer);
            TestCaseVerify.IsTrue("Verify answer is displayed",
                enhanceTab.AnswerTextArea.First().GetAttribute("current-value").Equals(Answer),
            "Answer is not displayed");

            enhanceTab.GenerateNewReportButton.ScrollToElement();
            var deepResearchSecondVersionPage = enhanceTab.GenerateNewReportButton.Click<AiDeepResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !deepResearchSecondVersionPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var selectedVersion = deepResearchSecondVersionPage.DeepResearchHeader.ReportVersionsMenu.SelectedOption;
            TestCaseVerify.IsTrue("Verify Report version displayed as Version2",
                  selectedVersion.Equals(ReportVersionsOption.SecondVersion),
                  "Report version not displayed as Version2");

            ReportTab reportTab = new ReportTab();
            this.TestCaseVerify.IsTrue("Verify summary is displayed in report tab",
                reportTab.RightColumnComponent.SummarySectionHeadingLabel.Text.Equals("Summary"),
                "Summary is not displayed in report tab");
            
            string reportContentHeader = reportTab.LeftColumnComponent.ReportContentHeader.Text;
            this.TestCaseVerify.IsTrue("Verify Report Contents header displayed",
                reportContentHeader.Equals("Report contents"),
                "Report Contents header not displayed");
            var V2ReportSummary= reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;

            deepResearchSecondVersionPage.DeepResearchHeader.ReportVersionsMenu.SelectOption(ReportVersionsOption.FirstVersion);
            var V1ReportSummary = reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;
            
            TestCaseVerify.AreNotSame("Verify summary is different between report version 1 and version 2",
                V1ReportSummary, V2ReportSummary,
                "Summary is same between report version 1 and version 2");
        }

        /// <summary>
        /// Test AI Deep Research recent and saved questions.
        /// User story: 2199122 Task: 2201766
        /// 1. Sign in WL Advantage and go to Deep Research tab on home page
        /// 2. Click in the question text area to open recent questions dialog
        /// 3. Check: Verify Recent questions tab displayed on popup dialog
        /// 4. Check: Verify Recent questions list contains no duplicates
        /// 5. Click a recent question from the list
        /// 6. Check: Verify selecting a recent question populates input box with the selection
        /// 7. Clear question text area and the recent questions dialog opens
        /// 8. Check: Verify Saved questions tab displayed on popup dialog
        /// 9. Click save button for a recent question
        /// 10.Check: Verify recent question is saved successfully
        /// 11.Click remove button for the saved question
        /// 12.Check: Verify saved question is removed successfully
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        public void DeepResearchRecentAndSavedQuestionsTest()
        {
            var homePage = this.GetHomePage<AdvantageHomePage>();
            var deepResearchTabPanel = homePage.TabPanel.SetActiveTab<DeepResearchTabPanel>(AdvantageBrowseTab.DeepAIResearch);
            RecentQuestionsDialog recentQuestionsDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.Click<RecentQuestionsDialog>();
            SafeMethodExecutor.WaitUntil(() => !recentQuestionsDialog.ProgressBarLabel.Displayed, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
                "Verify Recent questions tab displayed on popup dialog",
                recentQuestionsDialog.RecentQuestionsTab.Displayed,
                "Recent questions tab not displayed on popup dialog");

            List<string> questionList = recentQuestionsDialog.RecentQuestionButtons.Select(button => button.Text).ToList();
            // avoid questions with single quote as it causes issue when matching text in input box
            string recentQuestion = questionList.FirstOrDefault(s => !s.Contains("'"));
            bool hasNoDuplicates = questionList.Count == new HashSet<string>(questionList).Count;
            this.TestCaseVerify.IsTrue(
                "Verify Recent questions list contains no duplicates",
                hasNoDuplicates,
                "Recent questions list contains duplicates");
            
            recentQuestionsDialog.RecentQuestionLinkWithText(recentQuestion).Click();
            var displayedQuestion = deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.GetAttribute("current-value");
            this.TestCaseVerify.IsTrue(
                "Verify selecting a recent question populates input box with the selection",
                recentQuestion.Contains(displayedQuestion),
                $"Selecting a recent question does not populate input box. Selected question:{recentQuestion} | Displayed question:{displayedQuestion}");

            deepResearchTabPanel.WelcomeComponent.InputComponent.ClearQuestionTextArea();
            recentQuestionsDialog = deepResearchTabPanel.WelcomeComponent.InputComponent.QuestionTextarea.Click<RecentQuestionsDialog>();
            SafeMethodExecutor.WaitUntil(() => !recentQuestionsDialog.ProgressBarLabel.Displayed, timeoutFromSec: 10);

            this.TestCaseVerify.IsTrue(
                "Verify Saved questions tab displayed on popup dialog",
                recentQuestionsDialog.SavedQuestionsTab.Displayed,
                "Saved questions tab not displayed on popup dialog");

            recentQuestionsDialog.SaveQuestion(recentQuestion);
            Thread.Sleep(1000); // wait for the save to process
            this.TestCaseVerify.IsTrue(
                "Verify recent question is saved successfully",
                recentQuestionsDialog.IsQuestionSaved(recentQuestion),
                "Recent question is not saved");

            recentQuestionsDialog.RemoveSavedQuestion(recentQuestion);
            Thread.Sleep(1000); // wait for the remove to process
            this.TestCaseVerify.IsFalse(
                "Verify saved question is removed successfully",
                recentQuestionsDialog.IsQuestionSaved(recentQuestion),
                "Saved question is not removed");
        }
               
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(TeamSahniCategory)]
        public void DeepResearchCopyLinkTest()
        {            
            const string Jurisdictions = "All Federal";
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";
            const string SuccessfulCopyLinkMessageText = "Link copied to clipboard successfully.";

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            ReportTab reportTab = new ReportTab();
            var summaryBeforeCopyLink = reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;

            deepResearchPage.ResultComponent.ToolBar.CopyLinkButton.Click();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.ToolBar.SuccessfulStatusMessage.Displayed);
            var copiedLink = Clipboard.GetText();

            this.TestCaseVerify.AreEqual("", SuccessfulCopyLinkMessageText, deepResearchPage.ResultComponent.ToolBar.CopiedLinkSuccessLabel.Text);
            var copiedLinkPage = BrowserPool.CurrentBrowser.GoToUrl<AiDeepResearchPage>(copiedLink);
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var summaryAfterCopyLink = reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;

            this.TestCaseVerify.AreEqual(
                "Verify Summary is the same as the originator's Deep Research report",
                summaryBeforeCopyLink, summaryAfterCopyLink,
                "Summary is not same as the originator's Deep Research report");

            this.TestCaseVerify.IsFalse(
                "Verify Copy link is NOT available",
                deepResearchPage.ResultComponent.ToolBar.CopyLinkButton.Displayed,
                "Copy link is available");

            this.TestCaseVerify.IsFalse(
                "Verify Folder is NOT available",
                deepResearchPage.ResultComponent.ToolBar.SaveToFolderButton.Displayed,
                "Folder is available");

            this.TestCaseVerify.IsTrue(
                "Verify Delivery option is available",
                deepResearchPage.ResultComponent.ToolBar.DownloadReportButton.Displayed,
                "Delivery option is not available");

            // Copy link delivery verification
            copiedLinkPage.ResultComponent.ToolBar.DownloadReportButton.Click();
            var fileName = $"A manufacturer concealed a defect in the product it sold my client and my client suffered economic l.docx";
            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);
            var text = DocxTextExtractor.ExtractTextFromWord(Path.Combine(this.FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                "Verify copied link delivery contains search query",
                text.Contains(Query),
                "Copied link delivery does not contain search query");
        }

        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryDeepResearch)]
        [TestCategory(TeamSahniCategory)]
        public void DeepResearchSaveToFolderTest()
        {
            const string Jurisdictions = "All Federal";
            const string Query = "A manufacturer concealed a defect in the product it sold my client, and my client suffered economic losses from the fraudulent omission. Does the economic loss rule bar my client's fraud claim?";
            const string DeepResearchTitleInFolder = "A manufacturer concealed a defect in the product it sold my client, and ... my client's fraud claim?";
            PrepareTestFolder();

            AiDeepResearchPage deepResearchPage = GenerateReportWithoutAgentTime(Query, Jurisdictions);
            ReportTab reportTab = new ReportTab();
            var summaryBeforeSaveToFolder = reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;
            var saveToFolderDialog = deepResearchPage.ResultComponent.ToolBar.SaveToFolderButton.Click<EdgeSaveToFolderDialog>();
            saveToFolderDialog.FolderTreeComponent.SelectFolderByName(AiDeepResearchName);
            saveToFolderDialog.ClickSaveButton<AiDeepResearchPage>();

            var recentFolderDialog = deepResearchPage.Header.ClickHeaderTab<EdgeRecentFoldersDialog>(EdgeHeaderTabs.Folders);
            var folderPage = recentFolderDialog.ClickFolderByName(AiDeepResearchName).ClickViewThisFolderButton();
            deepResearchPage = folderPage.FolderGrid.ClickGridItemByName<AiDeepResearchPage>(DeepResearchTitleInFolder);
            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);
            var summaryAfterSaveToFolder = reportTab.RightColumnComponent.ResearchReportContentsLabel.First().Text;

            this.TestCaseVerify.AreEqual(
               "Verify Deep Research results on general page and from folder same",
               summaryBeforeSaveToFolder,
               summaryAfterSaveToFolder,
               "Deep Research results on general page and from folder not same");
        }

        private void PrepareTestFolder()
        {
            // If folder does not exitst, create it. If it exists, delete all contents.
            var researchOrganizerPage = EdgeNavigationManager.Instance.GoToFoldersPage<EdgeResearchOrganizerPage>();
            if (!researchOrganizerPage.LeftFolder.FolderTree.IsFolderExist(AiDeepResearchName))
                researchOrganizerPage.CreateNewFolder(AiDeepResearchName);
            else
            {
                researchOrganizerPage.LeftFolder.FolderTree.SelectFolderByName(AiDeepResearchName);
                researchOrganizerPage.ClearFolderGrid();
            }
            researchOrganizerPage.Header.ClickLogo<PrecisionHomePage>();
        }
    }
}