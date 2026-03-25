namespace WestlawPrecision.Tests.Aalp
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Core.Utils.Execution;
    using System.Linq;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Negative Treatment AI Summary tests
    /// </summary>
    [TestClass]
    public class AalpNegativeTreatmentTests : AalpBaseTest
    {
        protected const string FeatureTestCategory = "NegativeTreatmentAISummary";

        /// <summary>
        /// Negative treatment doc page has Summarize button and clicking button generates summary 
        /// (Stories:2124666, 2122255, 2138784 Tasks: 2138874, 2139805, 2145760).
        /// 1. Sign in WL Precision 
        /// 2. navigate to negative treatment document and click on negative treatment Tab
        /// 3. Check: Verify summarize negative treatment button displayed
        /// 4. Click summarize negative treatment button
        /// 5. Check: Verify negative treatment summary displayed. Document title available in summary.
        /// 6. Click on the link that is available in the summary 
        /// 7. Check: verify user is navigated to respective document page on a new tab
        /// 8. click on the flag in negative summary
        /// 9. Check: Verify user is navigated to the Negative Treatment tab for the document on a new tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void AiNegativeTreatmentSummaryTest()
        {
            const string DocGuid = "Icea138849c9611d993e6d35cc61aab4a";
            const string ExpectedSummaryHeading = "Negative treatment summary";
            const string DocumentlinkTab = "Document link Tab";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            var docTitle= documentPage.GetOutOfPlanBrowsePageTitle();
            var negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify Summarize negative treatment button displayed",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize negative treatment button not displayed");

            negativeTreatmentPage.SummarizeNegativeTreatmentButton.Click<EdgeNegativeTreatmentPage>();

            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);
            
            var negativeTreatmentSummaryHeading = negativeTreatmentPage.NegativeTreatmentSummary.SummaryHeadingLabel.Text;
            this.TestCaseVerify.IsTrue(
                "Verify negative treatment summary heading displayed",
                negativeTreatmentSummaryHeading.Equals(ExpectedSummaryHeading),
                "negative treatment summary heading not displayed. Displayed: " + negativeTreatmentSummaryHeading);

            var negativeTreatmentSummaryParagraph = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.First().Text;
            this.TestCaseVerify.IsTrue(
                "Verify summary content generated",
                negativeTreatmentSummaryParagraph.Contains(docTitle),
                "Expected summary content not generated.");

            var summaryContentKeyciteLinkLabel = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentDocumentLinks.First().Text;

            negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentDocumentLinks.First().Click<EdgeNegativeTreatmentPage>();
            BrowserPool.CurrentBrowser.CreateTab(DocumentlinkTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentlinkTab);
            DriverExtensions.WaitForPageLoad();
            this.TestCaseVerify.IsTrue("Verify document link in summary navigates to respective document in new tab",
                BrowserPool.CurrentBrowser.Title.Contains(summaryContentKeyciteLinkLabel.Split(',')[0]),
                "Negative summary keycite link not navigating to respective document in new tab");
            BrowserPool.CurrentBrowser.CloseTab(DocumentlinkTab);
            BrowserPool.CurrentBrowser.ActivateTab(HomePageTab);

            negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentKeyciteFlagLinks.First().Click<EdgeNegativeTreatmentPage>();
            var keyciteFlagDocumentTitle = "Negative Treatment - " + summaryContentKeyciteLinkLabel.Split(',')[0];
            BrowserPool.CurrentBrowser.CreateTab(keyciteFlagDocumentTitle);
            BrowserPool.CurrentBrowser.ActivateTab(keyciteFlagDocumentTitle);
            DriverExtensions.WaitForPageLoad();
            this.TestCaseVerify.IsTrue("Selecting Keycite flag in summary navigates to the Negative Treatment tab in a new browser Tab",
                BrowserPool.CurrentBrowser.Title.Contains(keyciteFlagDocumentTitle),
                "NT Summary keycite flag not navigating to negative treatment tab for selected document in new Tab");
        }

        /// <summary>
        /// Negative treatment doc page has Summarize button after refresh page and clicking button generates summary 
        /// (Stories:2149189 Tasks: 2149188, 2149194).
        /// 1. Sign in WL Precision 
        /// 2. navigate to negative treatment document and click on negative treatment Tab
        /// 3. Check: Verify Summarize negative treatment button displayed
        /// 4. Click Summarize negative treatment button
        /// 5. Check: Verify negative treatment summary displayed.
        /// 6. Click on Expand/collapse button
        /// 7. Check: Verify summary box is collapsed
        /// 8. Click on Expand/collapse button
        /// 9. Check: Verify summary box is Expanded
        /// 10.Refresh the page
        /// 11.Check: Verify Summarize negative treatment button displayed
        /// 12.click on the docuemnt Tab and then click on Negative Treatment Tab
        /// 13.Check: Verify Summarize negative treatment button displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void AiNegativeTreatmentSummaryButtonTest()
        {
            const string DocGuid = "I25c375707cf011e593fdee0612c55709";
            
            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            var docTitle = documentPage.GetOutOfPlanBrowsePageTitle();
            var negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify summarize negative treatment button displayed",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize negative treatment button not displayed");

            negativeTreatmentPage.SummarizeNegativeTreatmentButton.Click<EdgeNegativeTreatmentPage>();

            if(!negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed)
                negativeTreatmentPage.ClickNegativeTreatmentButton();
            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);
            
            var negativeTreatmentSummaryParagraph = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.First().Text;
            this.TestCaseVerify.IsTrue(
                "Verify summary content generated",
                negativeTreatmentSummaryParagraph.Contains(docTitle),
                "Expected summary content not generated.");

            bool isSummaryExpanded = negativeTreatmentPage.NegativeTreatmentSummary.ClickExpandCollapseButton();
            this.TestCaseVerify.IsFalse("Summary content should be collapsed and not visible",
                isSummaryExpanded,
                "Summary content is not collapsed and visible");

            isSummaryExpanded = negativeTreatmentPage.NegativeTreatmentSummary.ClickExpandCollapseButton();
            this.TestCaseVerify.IsTrue(
                "summary content should be expanded and visible",
                isSummaryExpanded,
                "Expected summary content is not expanded and not visible");

            DriverExtensions.RefreshPage();
            this.TestCaseVerify.IsTrue(
                 "Verify summarize negative treatment button displayed",
                 negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                 "Summarize negative treatment button not displayed");

            negativeTreatmentPage.SummarizeNegativeTreatmentButton.Click<EdgeNegativeTreatmentPage>();

            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);
            negativeTreatmentSummaryParagraph = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.First().Text;
            this.TestCaseVerify.IsTrue(
                "Verify summary content generated",
                negativeTreatmentSummaryParagraph.Contains(docTitle),
                "Expected summary content not generated.");

            documentPage = documentPage.RiTabs.ClickTab<EdgeCommonDocumentPage>(RiTab.Document);
            negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);
            this.TestCaseVerify.IsTrue(
                 "Verify summarize negative treatment button displayed",
                 negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                 "Summarize negative treatment button not displayed");
        }

        /// <summary>
        /// Check Negative treatment summary availablity on NY, CA, and WA Official Reports documents 
        /// (Stories:2138793 Tasks: 2149642).
        /// 1. Sign in WL Precision with IAC-AI-NEGATIVE-TREATMENT-NONNRS
        /// 2. View CA reports document and click Negative Treatment Tab
        /// 3. Check: Verify summarize button displayed on CA Reports document
        /// 4. View WA reports document and click Negative Treatment Tab
        /// 5. Check: Verify summarize button displayed on WA Reports document
        /// 6. View NY reports document and click Negative Treatment Tab
        /// 7. Check: Verify summarize button displayed on NY Reports document
        /// 8. Click on the summarize button
        /// 9. Check: Verify summary generated on NY Reports document
        /// 10.Go to Document tab, switch to NRS version and go to Negative Treatment tab
        /// 11.Check: Verify summarize button displayed on NY NRS version
        /// 12.Check: Verify summary not displayed switching from non-NRS to NRS version
        /// 13.Click on the summarize button
        /// 14.Check: Verify summary generated on NY NRS version
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void AiNegativeTreatmentReportsDocumentsTest()
        {
            const string NyReportsDocGuid = "I06326ad1d9ac11d9a489ee624f1f6e1a"; // New York Reports version
            const string CaReportsDocGuid = "I69d55bf1fb0c11d9b386b232635db992"; // California Reports version
            const string WaReportsDocGuid = "I04c4c55af55511d983e7e9deff98dc6f"; // Washington Reports version
            const string NRSLinkText = "View National Reporter System version";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(CaReportsDocGuid);
            var negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify summarize button displayed on CA Reports document",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize button not displayed on CA Reports document");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(WaReportsDocGuid);
            negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify summarize button displayed on WA Reports document",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize button not displayed on WA Reports document");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(NyReportsDocGuid);
            var nyDocTitle = documentPage.GetDocumentTitle();
            negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify summarize button displayed on NY Reports document",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize button not displayed on NY Reports document");

            // Generate summary on NY non-NRS version
            negativeTreatmentPage.ClickNegativeTreatmentButton();
            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);

            var negativeTreatmentSummaryParagraph = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.First().Text;
            this.TestCaseVerify.IsTrue(
                "Verify summary generated on NY Reports document",
                negativeTreatmentSummaryParagraph.Contains(nyDocTitle),
                "Summary not generated on NY Reports document");

            // Go to Document tab, switch to NRS version and go to Negative Treatment tab
            documentPage = negativeTreatmentPage.RiTabs.ClickTab<EdgeCommonDocumentPage>(RiTab.Document);
            documentPage = documentPage.ClickLinkByText<EdgeCommonDocumentPage>(NRSLinkText);
            negativeTreatmentPage = documentPage.RiTabs.ClickTab<EdgeNegativeTreatmentPage>(RiTab.NegativeTreatment);

            this.TestCaseVerify.IsTrue(
                "Verify summarize button displayed on NY NRS version",
                negativeTreatmentPage.SummarizeNegativeTreatmentButton.Displayed,
                "Summarize button not displayed on NY NRS version");

            this.TestCaseVerify.IsTrue(
                "Verify summary not displayed switching from non-NRS to NRS version",
                negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.Count == 0,
                "Summary displayed switching from non-NRS to NRS version");

            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);
            negativeTreatmentPage.ClickNegativeTreatmentButton();
            SafeMethodExecutor.WaitUntil(() => !negativeTreatmentPage.NegativeTreatmentSummary.ProgressRingLabel.Displayed, timeoutFromSec: 400);

            negativeTreatmentSummaryParagraph = negativeTreatmentPage.NegativeTreatmentSummary.SummaryContentParagraphLabels.First().Text;
            this.TestCaseVerify.IsTrue(
                "Verify summary generated on NY NRS version",
                negativeTreatmentSummaryParagraph.Contains(nyDocTitle),
                "Summary not generated on NY NRS version");
        }       
    }
}
