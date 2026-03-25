namespace WestlawAdvantage.Tests.DeepResearch
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Dialogs.Foldering;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvantage.Dialogs.DeepResearch;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.DeepResearch;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Folders;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WestlawPrecision.Utilities;

    /// <summary>
    /// AI Deep Research Verification tests
    /// </summary>
    [TestClass]
    public class WlaDeepResearchVerificationTests : WlaDeepResearchBaseTest
    {
        private const string DeepResearchVerification = "DeepResearchVerification";

        /// <summary>
        /// Test AI Deep Research able to verify DR report in history.
        /// Task: 2279040,2278999, 2279243, 2283042
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate 'Concise' report
        /// 4. Click skill nudge button
        /// 5. Verify: 'Verify' buttons displayed in both 'Report' and 'Verify' tabs
        /// 6. Verify: Verify result table is displayed
        /// 7. Verify: Verify result header column names
        /// 8. Verify: Assertion column data has all of the rows with texts and some rows with links
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(DeepResearchVerification)]
        public void DeepResearchVerificationCommonTest()
        {
            const string Query = "My client is an accountant who is expecting actions by people claiming he made misrepresentations to them regarding an investment for his gain. What kind of claim can be expected?";
            const string Jurisdictions = "Minnesota;All Federal";

            string checkVerifyButtonReportTab = "Verify: 'Verify' button is displayed on the 'Report' tab";
            string checkVerifyReportButtonVerifyTab = "Verify: 'Verify report' button is displayed on the 'Verify' tab";
            string checkVerifyReportLoadingMessage = "Verify: 'Verify report' loading message is displayed on clicking 'Verify report' button";
            string checkVerifyReportTableHeaderNames = "Verify: 'Assertion, Verify, Explore more' Header column names are displayed";
            string checkVerifyReportAssertionColumnData = "Verify: Assertion column data has all of rows with texts and some rows with links";
            string checkFilterByButtons = "Verify: 'Total Assertions' and 'Potential Issues' buttons are not displayed on the 'Verify' tab without IAC";

            var deepResearchPage = this.GenerateReportWithoutAgentTime(Query, Jurisdictions);

            SafeMethodExecutor.WaitUntil(() => deepResearchPage.ResultComponent.SkillNudgeButton.Displayed);

            deepResearchPage = deepResearchPage.ResultComponent.SkillNudgeButton.Click<AiDeepResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var reportTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<ReportTab>(DeepResearchResultTabs.Report);

            this.TestCaseVerify.IsTrue(
                checkVerifyButtonReportTab,
                reportTab.LeftColumnComponent.VerifyButton.Displayed,
                "'Verify' button is NOT displayed on the 'Report' tab");

            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);

            this.TestCaseVerify.IsTrue(
                checkVerifyReportButtonVerifyTab,
                verifyTab.VerifyReportButton.Displayed,
                "'Verify report' button is NOT displayed on the 'Verify' tab");

            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            this.TestCaseVerify.IsTrue(
                checkVerifyReportLoadingMessage,
                verifyTab.VerifyProgressBarLabel.Displayed
                && verifyTab.VerifyProgressBarLabel.Text.Contains("Analyzing legal assertions..."),
                "'Verify report' loading message is NOT displayed on clicking 'Verify report' button");

            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkVerifyReportTableHeaderNames,
                verifyTab.VerifyResultGridItems.Count > 0
                && verifyTab.VerifyResultGridItems.First().AssertionColumn.AssertionLabel.Text.Contains("Assertion")
                && verifyTab.VerifyResultGridItems.First().VerifyColumn.VerifyLabel.Text.Contains("Verify")
                && verifyTab.VerifyResultGridItems.First().ExploreMoreColumn.ExploreMoreLabel.Text.Contains("Explore more"),
                "Verify: 'Assertion, Verify, Explore more' Header column names are not displayed");

            this.TestCaseVerify.IsTrue(
                checkVerifyReportAssertionColumnData,
                verifyTab.VerifyResultGridItems.All(item => item.AssertionColumn.AssertionTextLabel.Displayed)
                && verifyTab.VerifyResultGridItems.Any(item => item.AssertionColumn.AssertionLinkLabel.Displayed),
                "Assertion column data does NOT have all the rows with texts and some rows with links");

            this.TestCaseVerify.IsFalse(
                checkFilterByButtons,
                verifyTab.TotalAssertionButton.Displayed
                && verifyTab.PotentialIssuesButton.Displayed,
                "'Total Assertions' and 'Potential Issues' buttons are displayed on the 'Verify' tab without turning on IAC");
        }

        /// <summary>
        /// Test AI Deep Research able to verify DR Explore more functionality
        /// Task: 2278994,2278995, 2279041, 2278997
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate 'Concise' report
        /// 4. Click 'Citing References' Explore more link
        /// 5. Verify: Page is opened in a new tab with citing references results
        /// 6. Verify: In Explore More column, if we click on the Precision Research link, then it opens the Case document in new tab.
        /// 7. Verify: In Explore More column, if we click on the Key Number link, then User is redirected to the new browser tab. Page's title contains 
        /// 8. Click 'Parallel Search' explore more link
        /// 9. Verify: Page is opened in a new tab with parallel search results
        /// 10. Open the Parallel Search direct link from home page and search for the same query
        /// 11. Verify: Both Parallel search results are identical
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(DeepResearchVerification)]
        public void DeepResearchVerificationExploreMoreTest()
        {
            const string Query = "Is a quota for drugs prescribed a factor for knowingly prescribing controlled substances without a legitimate medical purpose?";
            const string Jurisdictions = "All Federal;All States";
            const string CitingReferencesTab = "CitingReferencesTab";
            const string KeyNumbersTab = "CitingReferencesTab";
            const string DeepResearchverifyTab = "DeepResearchverifyTab";
            const string DocumentTab = "DocumentTab";
            const string ParallelSearchTab = "ParallelSearchTab";
            const string ParallelSearchLinkLabel = "Parallel Search";

            string checkExploreMoreCitingReferencesLink = "Verify: 'Explore More' 'Citing References' link is opened in a new browser tab on a 'Citing References' document tab";
            string checkExploreMoreKeyNumbersLink = "Verify: 'Explore More' 'Key Numbers' link is opened in a new browser tab";
            string checkExploreMorePresionResearchLink = "'Explore More' 'Precision Research' link is opened in a new browser tab";
            string checkExploreMoreParallelSearchLink = "Verify: 'Explore More' 'Parallel Search' link is opened in a new browser tab on a 'Parallel Search' result page and search results are displayed";
            string CheckParalleSearchCount = "Verify: Count from parallel search link from Verify tab matches with the direct parallel search result count";

            var deepResearchPage = this.GenerateReportWithoutAgentTime(Query, Jurisdictions);

            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            var citingReferencesPage = verifyTab.VerifyResultGridItems.First(item => item.ExploreMoreColumn.CitingReferencesLinks.Any()).ExploreMoreColumn.CitingReferencesLinks.First().Click<CitingReferencesPage>();
            BrowserPool.CurrentBrowser.CreateTab(CitingReferencesTab);
            BrowserPool.CurrentBrowser.ActivateTab(CitingReferencesTab);

            SafeMethodExecutor.WaitUntil(() => citingReferencesPage.LoadingSpinnerLabel.Displayed, timeoutFromSec: 120);

            this.TestCaseVerify.IsTrue(
                checkExploreMoreCitingReferencesLink,
                citingReferencesPage.RiTabs.GetSelectedTab().Equals(RiTab.CitingReferences),
                "'Explore More' 'Citing References' link is NOT opened in a new browser tab on a 'Citing References' document tab");

            BrowserPool.CurrentBrowser.CloseTab(CitingReferencesTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepResearchverifyTab);

            var keyNumberLinkText = verifyTab.VerifyResultGridItems.First(item => item.ExploreMoreColumn.KeyNumbersLinks.Any()).ExploreMoreColumn.KeyNumbersLinks.First().Text.Split('>').Last().Split(new[] { "\r\n", "\n" }, StringSplitOptions.None).First().Replace(" ", "").ToLowerInvariant();
            var keyNumbersPage = verifyTab.VerifyResultGridItems.First(item => item.ExploreMoreColumn.KeyNumbersLinks.Any()).ExploreMoreColumn.KeyNumbersLinks.First().Click<EdgeCommonBrowsePage>();
            
            BrowserPool.CurrentBrowser.CreateTab(KeyNumbersTab);
            BrowserPool.CurrentBrowser.ActivateTab(KeyNumbersTab);

            SafeMethodExecutor.WaitUntil(() => keyNumbersPage.LoadingSpinnerLabel.Displayed, timeoutFromSec: 60);
            SafeMethodExecutor.WaitUntil(() => !keyNumbersPage.LoadingSpinnerLabel.Displayed, timeoutFromSec: 60);

            this.TestCaseVerify.IsTrue(
                checkExploreMoreKeyNumbersLink,
                keyNumbersPage.GetBrowsePageTitle().Replace(" ", "").ToLowerInvariant().Contains(keyNumberLinkText),
                "'Explore More' 'Key Numbers' link is NOT opened in a new browser tab");

            BrowserPool.CurrentBrowser.CloseTab(KeyNumbersTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepResearchverifyTab);

            var resultPage = verifyTab.VerifyResultGridItems.First(item => item.ExploreMoreColumn.PresicionResearchLinks.Any()).ExploreMoreColumn.PresicionResearchLinks.First().Click<CommonBrowsePage>();

            BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);

            SafeMethodExecutor.WaitUntil(() => resultPage.LoadingSpinnerLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !resultPage.LoadingSpinnerLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkExploreMorePresionResearchLink,
                BrowserPool.CurrentBrowser.Title.Contains("Westlaw Advantage")
                && resultPage.GetCustomTabText().Contains("Cases"),
                "'Explore More' 'Precision Research' link is NOT opened in a new browser tab - document page");

            BrowserPool.CurrentBrowser.CloseTab(DocumentTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepResearchverifyTab);

            var parallelSearchPage = verifyTab.VerifyResultGridItems.First(item => item.ExploreMoreColumn.ParallelSearchLinks.Any()).ExploreMoreColumn.ParallelSearchLinks.First().Click<ParallelSearchPage>();

            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            SafeMethodExecutor.WaitUntil(() => parallelSearchPage.ProgressRingLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCountFromVerifyParallelSearchLink = parallelSearchPage.Results.CasesItems.Count;
            var query = parallelSearchPage.QueryBox.SearchQueryLabel.GetAttribute("current-value");

            this.TestCaseVerify.IsTrue(
                checkExploreMoreParallelSearchLink,
                parallelSearchPage.Results.ResultCountLabel.Text.Contains($"Cases ({resultCountFromVerifyParallelSearchLink})"),
                $"Search results are not displayed. Result count: { resultCountFromVerifyParallelSearchLink}");

            BrowserPool.CurrentBrowser.CloseTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(DeepResearchverifyTab);

            var homePage = this.GetHomePage<AdvantageHomePage>();
            homePage.WestlawAdvantageLogoLink.Click();

            parallelSearchPage = this.GetHomePage<PrecisionHomePage>().FeaturesIncludedPanel.GetWidgetLinkByTitle(ParallelSearchLinkLabel).Click<ParallelSearchPage>();
            
            BrowserPool.CurrentBrowser.CreateTab(ParallelSearchTab);
            BrowserPool.CurrentBrowser.ActivateTab(ParallelSearchTab);

            parallelSearchPage.QueryBox.EnterSearchQuery(query);
            parallelSearchPage.QueryBox.SubmitSearchQuery();
            SafeMethodExecutor.WaitUntil(() => !parallelSearchPage.ProgressRingLabel.Displayed);

            var resultCountParallelSearch = parallelSearchPage.Results.CasesItems.Count;

            this.TestCaseVerify.AreEqual(
                CheckParalleSearchCount,
                resultCountParallelSearch,
                resultCountFromVerifyParallelSearchLink,
                "Search results count from 'Explore more' parallel search and direct parallel search link from home page are different");
        }

        /// <summary>
        /// Task 2279044, 2279038
        /// Verify: Total Assertion and Potential issues are displayed with "IAC-AI-GUIDED-RESEARCH-VAV-SHOW-ISSUES"
        /// Verify: Applying Filter works properly
        /// 1. Sign in WL Advantage          
        /// 2. Click AI Deep Research card under Key Features on the home page
        /// 3. Generate 'Concise' report
        /// 4. Verify: Total Assertion and Potential issues are displayed with "IAC-AI-GUIDED-RESEARCH-VAV-SHOW-ISSUES"
        /// 5. Verify: Applying Filter works properly
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(DeepResearchVerification)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-GUIDED-RESEARCH-VAV-SHOW-ISSUES")]
        public void DeepResearchVerificationPotentialIssuesFiltersTest()
        {
            const string Query = "Can you use extrinsic evidence in a 12(b)(7) motion to dismiss?";
            const string Jurisdictions = "All Federal;All States";

            string checkFilterByButtons = "Verify: 'Total Assertions' and 'Potential Issues' buttons are displayed on the 'Verify' tab with IAC";
            string checkVerifyPotentialIssuesFilter = "Verify: 'Potential Issues' filter button filters the data from Verify result grid";
            string checkVerifyTotalAssertionsFilter = "Verify: 'Total Assertions' filter button filters the data from Verify result grid";

            var deepResearchPage = this.GenerateReportWithoutAgentTime(Query, Jurisdictions);
            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkFilterByButtons,
                verifyTab.TotalAssertionButton.Displayed
                && verifyTab.PotentialIssuesButton.Displayed,
                "'Total Assertions' and 'Potential Issues' buttons are NOT displayed on the 'Verify' tab with IAC");

            verifyTab = verifyTab.PotentialIssuesButton.Click<VerifyTab>();
            int potentialIssuesCountFromTable = verifyTab.VerifyResultGridItems.Count();
            int potentialIssuesCountFromFilter = Convert.ToInt16(verifyTab.PotentialIssuesButton.Text.Split(' ')[0]);

            this.TestCaseVerify.AreEqual(
                checkVerifyPotentialIssuesFilter,
                potentialIssuesCountFromTable,
                potentialIssuesCountFromFilter,
                "Potential Issue Filter count is not correct");

            verifyTab = verifyTab.TotalAssertionButton.Click<VerifyTab>();
            int TotalAssertionsCountFromTable = verifyTab.VerifyResultGridItems.Count();
            int TotalAssertionsCountFromFilter = Convert.ToInt16(verifyTab.TotalAssertionButton.Text.Split(' ')[0]);

            this.TestCaseVerify.AreEqual(
                checkVerifyTotalAssertionsFilter,
                TotalAssertionsCountFromTable,
                TotalAssertionsCountFromFilter,
                "Total Assertions Filter count is not correct");
        }

        /// <summary>
        /// Task 2278282, 2279043, 2308968, 2282969
        /// Verify: Report is completed after answering enhancement question and generating new report
        /// 1. Ask a question to generate DR report
        /// 2. Click 'Verify' tab and answer the clarifying question to enhance the report
        /// 3. Click on 'Generate new report' button and verify the new report is generated and completed without any loading issue.
        /// 4. Verify: In Version 2 report 'Verify Report' button is displayed
        /// 5. Switch to Version 1 report
        /// 6. Verify: 'Verify' tab is retained as peviously generated
        /// 7. Switch to Version 2 report and answer the clarifying question to enhance the report.
        /// 8. Click on 'Generate new report' button and verify the new report is generated and completed without any loading issue.
        /// 9. Switch to Version 2 report
        /// 10. Verify: 'Go to latest version of report' button is displayed on the 'Verify' tab of Version 2 report.
        /// 11. Click on new research button
        /// 12. Go to DR and ask the following query:  "What is fraud?" against any jurisdiction (make sure 3-minute type is selected).
        /// 13. Click "generate a full report". Verify Version 2 result.
        /// 14. Verify: While verification is in progress switch to Version 1 and back, there shouldn't be any error
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(DeepResearchVerification)]
        public void DeepResearchVerificationEnhancementsTest()
        {
            const string Query = "What is the deadline for filing a continuation application?";
            const string EnhancementQuery = "Yes";
            const string Jurisdictions = "All Federal";
            const string SecondConversationQuery = "What is fraud?";

            string checkReportIsCompleted = "Verify: Report is completed after answering enhancement question";
            string checkVerifyReportDisplayed = "Verify: 'Verify report' button is displayed on the 'Verify' tab of Version2 DR Report";
            string checkVerifyReportNotDisplayed = "Verify: 'Verify report' button is NOT displayed on the 'Verify' tab of Version1 DR Report";
            string checkVerifyReportTableHeaderNames = "Verify: 'Assertion, Verify, Explore more' Header column names are displayed";
            string checkGoToLatestVersionReportDisplayed = "Verify: 'Go to latest version of report' button is displayed on the 'Verify' tab of Version2 DR Report";
            string checkVerifyReportDataWhenSwitchedVersion = "Verify: Verify report data is displayed without Something went wrong error";

            var deepResearchPage = this.GenerateReportWithoutAgentTime(Query, Jurisdictions);
            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab.VerifyReportButton.WaitDisplayed(2000);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            var enhanceTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<EnhanceTab>(DeepResearchResultTabs.Enhance);
            enhanceTab.EnterAnswerToFirstClarifyingQuestion(EnhancementQuery);

            enhanceTab.GenerateNewReportButton.ScrollToElement();
            var deepResearchReportSecondPage = enhanceTab.GenerateNewReportButton.Click<AiDeepResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !deepResearchReportSecondPage.ResultComponent.LeftColumnComponent.ProgressBarLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !deepResearchReportSecondPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var reportTab = deepResearchReportSecondPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<ReportTab>(DeepResearchResultTabs.Report);

            this.TestCaseVerify.IsTrue(
                checkReportIsCompleted,
                reportTab.LeftColumnComponent.VerifyButton.Displayed,
                "Report is NOT completed after answering enhancement question");

            var secondVersionVerifyTab = deepResearchReportSecondPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);

            this.TestCaseVerify.IsTrue(
                checkVerifyReportDisplayed,
                secondVersionVerifyTab.VerifyReportButton.Displayed,
                "'Verify report' button is NOT displayed on the 'Verify' tab of Version 2");

            var firstVersionReportTab = deepResearchReportSecondPage.DeepResearchHeader.ReportVersionsMenu.SelectOption<ReportTab>(ReportVersionsOption.FirstVersion);

            var firstVersionVerifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);

            this.TestCaseVerify.IsFalse(
                checkVerifyReportNotDisplayed,
                firstVersionVerifyTab.VerifyReportButton.Displayed,
                "'Verify report' button is displayed on the 'Verify' tab of Version1 DR Report");

            this.TestCaseVerify.IsTrue(
                checkVerifyReportTableHeaderNames,
                firstVersionVerifyTab.VerifyResultGridItems.Count > 0
                && firstVersionVerifyTab.VerifyResultGridItems.First().AssertionColumn.AssertionLabel.Text.Contains("Assertion")
                && firstVersionVerifyTab.VerifyResultGridItems.First().VerifyColumn.VerifyLabel.Text.Contains("Verify")
                && firstVersionVerifyTab.VerifyResultGridItems.First().ExploreMoreColumn.ExploreMoreLabel.Text.Contains("Explore more"),
                "Verify: 'Assertion, Verify, Explore more' Header column names are not displayed");

            var secondVersionReportTab = deepResearchReportSecondPage.DeepResearchHeader.ReportVersionsMenu.SelectOption<ReportTab>(ReportVersionsOption.SecondVersion);
            var secondVersionEnhanceTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<EnhanceTab>(DeepResearchResultTabs.Enhance);
            secondVersionEnhanceTab.EnterAnswerToFirstClarifyingQuestion(EnhancementQuery);

            secondVersionEnhanceTab.GenerateNewReportButton.ScrollToElement();
            var deepResearchReportThirdPage = secondVersionEnhanceTab.GenerateNewReportButton.Click<AiDeepResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !deepResearchReportThirdPage.ResultComponent.LeftColumnComponent.ProgressBarLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !deepResearchReportThirdPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            var thirdVersionReport = deepResearchReportThirdPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<ReportTab>(DeepResearchResultTabs.Report);
            secondVersionReportTab = deepResearchReportSecondPage.DeepResearchHeader.ReportVersionsMenu.SelectOption<ReportTab>(ReportVersionsOption.SecondVersion);
            var secondVersionenVerifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);

            this.TestCaseVerify.IsTrue(
                checkGoToLatestVersionReportDisplayed,
                secondVersionenVerifyTab.GoToLatestVersionReportButton.Displayed,
                "'Go to latest version of report' button is NOT displayed on the 'Verify' tab of Version2 DR Report");

            deepResearchPage = deepResearchPage.DeepResearchHeader.NewResearchButton.Click<AiDeepResearchPage>();
            SafeMethodExecutor.WaitUntil(() => deepResearchPage.DeepResearchHeader.HeadingLabel.Displayed, 2);
            deepResearchPage.WelcomeComponent.InputComponent.QuestionTextarea.SendKeys(SecondConversationQuery);
            deepResearchPage.WelcomeComponent.InputComponent.SendButton.Click();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            deepResearchPage = deepResearchPage.ResultComponent.GenerateAFullReportButton.Click<AiDeepResearchPage>();

            SafeMethodExecutor.WaitUntil(() => !deepResearchPage.ResultComponent.SingleColumnComponent.ProgressBarLabel.Displayed);

            verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            reportTab = deepResearchPage.DeepResearchHeader.ReportVersionsMenu.SelectOption<ReportTab>(ReportVersionsOption.FirstVersion);
            verifyTab = deepResearchPage.DeepResearchHeader.ReportVersionsMenu.SelectOption<VerifyTab>(ReportVersionsOption.SecondVersion);

            SafeMethodExecutor.WaitUntil(() => verifyTab.VerifyProgressBarLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkVerifyReportDataWhenSwitchedVersion,
                verifyTab.VerifyResultGridItems.Count > 0,
                "Verify report data is NOT displayed, Something went wrong error occured");
        }

        /// <summary>
        /// Task 2279307
        /// Verify: 'Verification' tab in the delivered report
        /// 1. Ask a question to generate DR report
        /// 2. Click on 'Download' button
        /// 3. Verify: Downloaded report contains the 'Verification' tab with the search query in the content.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(DeepResearchVerification)]
        public void DeepResearchVerificationDeliveryTest()
        {
            const string Query = "Can an attorney be sanctioned for submitting frivolous Freedom of Information Act requests?";
            const string Jurisdictions = "All Federal;All States";

            string checkVerificationTabInDelivery = "Verify: 'Verification' tab in the delivery is as in UI";

            AiDeepResearchPage deepResearchPage = this.GenerateReportWithoutAgentTime(Query, Jurisdictions);
            var verifyTab = deepResearchPage.ResultComponent.DeepResearchResultTabPanel.SetActiveTab<VerifyTab>(DeepResearchResultTabs.Verify);
            verifyTab.VerifyReportButton.WaitDisplayed(2000);
            verifyTab = verifyTab.VerifyReportButton.Click<VerifyTab>();

            SafeMethodExecutor.WaitUntil(() => !verifyTab.VerifyProgressBarLabel.Displayed);

            var deliveryProgressDialog = deepResearchPage.ResultComponent.ToolBar.DownloadReportButton.Click<DeliveryProgressDialog>();

            SafeMethodExecutor.WaitUntil(() => !deliveryProgressDialog.IsDisplayed());

            var fileName = $"{Query}.docx".Replace("?", string.Empty);
            FileUtil.WaitForFileDownload(this.FolderToSave, fileName);
            var text = DocxTextExtractor.ExtractTextFromWord(Path.Combine(this.FolderToSave, fileName));

            this.TestCaseVerify.IsTrue(
                checkVerificationTabInDelivery,
                text.Contains(verifyTab.VerifyResultGridItems.First().VerifyColumn.VerifyLabel.Text)
                && text.Contains(verifyTab.VerifyResultGridItems.First().AssertionColumn.AssertionLabel.Text)
                && text.Contains(verifyTab.VerifyResultGridItems.First().ExploreMoreColumn.ExploreMoreLabel.Text)
                && text.Contains(verifyTab.VerifyResultGridItems.Last().VerifyColumn.VerifyLabel.Text)
                && text.Contains(verifyTab.VerifyResultGridItems.Last().AssertionColumn.AssertionLabel.Text)
                && text.Contains(verifyTab.VerifyResultGridItems.Last().ExploreMoreColumn.ExploreMoreLabel.Text)
                && text.Contains("Deep Research Verification Results")
                && text.Contains("Use this information to help you verify the report and determine if additional law is important for your question.")
                && text.Contains("Generated by AI. Not legal advice. A qualified professional must verify accuracy and legal compliance."),
                "'Verification' tab in the delivery is NOT as in UI");
        }
    }
}