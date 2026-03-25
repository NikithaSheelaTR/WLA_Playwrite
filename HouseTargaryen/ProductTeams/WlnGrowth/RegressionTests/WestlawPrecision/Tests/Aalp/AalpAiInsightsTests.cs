namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using System.Linq;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Interfaces.Elements;

    /// <summary>
    /// AALP AI Insights tests
    /// </summary>
    [TestClass]
    public class AalpAiInsightsTests : AalpBaseTest
    {
        private const string FeatureTestCategoryInsight = "AiInsights";

        /// <summary>
        /// Check availability of AI Insights/KeyCite Summary feature
        /// 1. Sign in WL Precision and search in All State & Federal: 212 A.3d 1135
        /// 2. Collapse the right panel
        /// 3. Check: Verify AI Summary button displayed on right panel
        /// 4. Click AI Summary button
        /// 5. Check: Verify Zero state message displayed on AI Summary panel
        /// 6. Check: Verify Summarize link displayed as expected
        /// </summary>
        [TestMethod]
        //[TestCategory(SmokeTestCategory)]
        //[TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryInsight)]
        //[TestCategory(TeamDahlCategory)]
        // Taking out from regression due to feature design
        public void DocumentAiSummarySmokeTest()
        {
            const string DocQuery = "212 A.3d 1135";
            const string ExpectedZeroStateMessage1 = "Summary not available";
            const string ExpectedZeroStateMessage2 = "Click on Summarize button to generate summary";
            const string ExpectedSummaryLinkText = "Generate AI Insights";

            string checkAiSummaryButtonDisplayed = "Verify AI Summary button displayed on right panel";
            string checkAiSummaryZeroStateMessageDisplayed = "Verify Zero state message displayed on AI Summary panel";
            string checkExpectedSummaryLinkDisplayed = "Verify Summarize link displayed as expected";

            var documentPage = GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<PrecisionDocumentPageWithHeadnotes>(DocQuery);
            documentPage.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            this.TestCaseVerify.IsTrue(
                checkAiSummaryButtonDisplayed,
                documentPage.RightPanel.AiSummaryPanelButton.Displayed,
                "AI Summary button is not displayed on right panel");

            // Open AI Summary Right Panel and check zero state message
            documentPage.RightPanel.AiSummaryPanelButton.Click<PrecisionDocumentPageWithHeadnotes>();
            string zeroStateMessage = documentPage.RightPanel.AiSummaryPanel.ZeroStateLabel.Text;
            this.TestCaseVerify.IsTrue(
                checkAiSummaryZeroStateMessageDisplayed,
                zeroStateMessage.Contains(ExpectedZeroStateMessage1) && zeroStateMessage.Contains(ExpectedZeroStateMessage2),
                "Expected zero state message not displayed. Displayed = " + zeroStateMessage);

            // Check link text
            string firstLinkText = documentPage.WestHeadnotes.HeadnotesSummarizeButtons.ElementAtOrDefault(0).Text;
            this.TestCaseVerify.IsTrue(
                checkExpectedSummaryLinkDisplayed,
                firstLinkText.Equals(ExpectedSummaryLinkText),
                "Summarize link displayed: " + firstLinkText + " Expected: " + ExpectedSummaryLinkText);
        }

        /// <summary>
        /// Verify AI Insights zero state, summary generation and sorting
        /// Test case: 1860899  User Story 1837041: AALP: Keycite Clicking Button
        /// 1. Sign in WL Precision and search in All State & Federal: 212 A.3d 1135
        /// 2. Collapse the right panel
        /// 3. Check: Verify AI Summary button displayed on right panel
        /// 4. Click AI Summary button
        /// 5. Check: Verify Zero state message displayed on AI Summary panel
        /// 6. Click Summarize button on second headnote
        /// 7. Check: Verify AI Summary title is correctly displayed for headnote 2
        /// 8. Click Summarize button on first headnote
        /// 9. Check: Verify titles are sorted by displaying headnote 1 title in first position
        /// </summary>
        [TestMethod]
        //[TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategoryInsight)]
        //[TestCategory(TeamDahlCategory)]
        [TestCategory("DocumentAiSummaryTest")]
        // Taking out from regression due to feature design
        public void DocumentAiSummaryTest()
        {
            const string DocQuery = "212 A.3d 1135";
            const string ExpectedZeroStateMessage1 = "Summary not available";
            const string ExpectedZeroStateMessage2 = "Click on Summarize button to generate summary";
            const string ExpectedSummaryTitle2 = "AI Summary for headnote 2";
            const string ExpectedSummaryTitle1 = "AI Summary for headnote 1";

            string checkAiSummaryButtonDisplayed = "Verify AI Summary button displayed on right panel";
            string checkAiSummaryZeroStateMessageDisplayed = "Verify Zero state message displayed on AI Summary panel";
            string checkAiSummaryTitleIsCorrect = "Verify AI Summary title is correctly displayed";
            string checkAiSummaryTitleIsSorted = "Verify AI Summary title is sorted";

            var documentPage = GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<PrecisionDocumentPageWithHeadnotes>(DocQuery);
            documentPage.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            this.TestCaseVerify.IsTrue(
                checkAiSummaryButtonDisplayed,
                documentPage.RightPanel.AiSummaryPanelButton.Displayed,
                "AI Summary button is not displayed on right panel");

            // Open AI Summary Right Panel
            documentPage.RightPanel.AiSummaryPanelButton.Click<PrecisionDocumentPageWithHeadnotes>();
            string zeroStateMessage = documentPage.RightPanel.AiSummaryPanel.ZeroStateLabel.Text;
            this.TestCaseVerify.IsTrue(
                checkAiSummaryZeroStateMessageDisplayed,
                zeroStateMessage.Contains(ExpectedZeroStateMessage1) && zeroStateMessage.Contains(ExpectedZeroStateMessage2),
                "Expected zero state message not displayed. Displayed = " + zeroStateMessage) ;

            // Click Summarize on 2nd headnote
            documentPage = documentPage.WestHeadnotes.HeadnotesSummarizeButtons.ElementAtOrDefault(1).Click<PrecisionDocumentPageWithHeadnotes>();
            string secondDisplayedTitle = documentPage.RightPanel.AiSummaryPanel.ListOfAiSummaries.First().CurrentAiSummaryTitleLabel.Text;
            this.TestCaseVerify.IsTrue(
                checkAiSummaryTitleIsCorrect,
                secondDisplayedTitle.Contains(ExpectedSummaryTitle2),
                "Expected title to display" + ExpectedSummaryTitle2 + " Displayed = " + secondDisplayedTitle);

            // Click Summarize button on 1st headnote
            ILink summaryLink = documentPage.WestHeadnotes.HeadnotesSummarizeButtons.ElementAtOrDefault(0);
            summaryLink.WaitEnabled(6000);

            documentPage = documentPage.WestHeadnotes.HeadnotesSummarizeButtons.ElementAtOrDefault(0).Click<PrecisionDocumentPageWithHeadnotes>();
            string firstDisplayedTitle = documentPage.RightPanel.AiSummaryPanel.ListOfAiSummaries.First().CurrentAiSummaryTitleLabel.Text;

            this.TestCaseVerify.IsTrue(
                checkAiSummaryTitleIsSorted,
                firstDisplayedTitle.Contains(ExpectedSummaryTitle1),
                "Expected title to display" + ExpectedSummaryTitle1 + " Displayed = " + firstDisplayedTitle);
        }

        protected override void InitializeRoutingPageSettings()
        {
            this.Settings.AppendValues(
                EnvironmentConstants.InfrastructureAccessControlsOn,
                SettingUpdateOption.Append,
                "IAC-KEYCITE-AI-SUMMARIES",
                "IAC-EDGE-DOC-PANEL-HTML-REFACTOR");
        }
    }
}
