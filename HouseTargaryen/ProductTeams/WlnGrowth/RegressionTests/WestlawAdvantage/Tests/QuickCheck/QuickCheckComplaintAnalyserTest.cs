namespace WestlawAdvantage.Tests.QuickCheck
{
    using System.Linq;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP.ComplaintAnalyzer;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Header;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;

    /// <summary>
    /// Complaint analyzer from quick check test
    /// </summary>
    [TestClass]
    public class QuickCheckComplaintAnalyzerTest : WestlawAdvantageQuickCheckBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

        ///<summary>
        ///Navigate to Complaint Analyzer File Upload Page,
        ///Verify: Clicking Back to start will navigate back to LDA home page
        ///</summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void QuickCheckComplaintAnalyzerCommonTest()
        {
            string checkComplaintAnalyserTileDisplayed = "Verify: Complaint analyser tile is displayed in LDA landing page";
            string checkBackToStartButtonNavigation = "Verify: Clicking on Back to start button navigates to LDA page";

            var landingPage = QuickCheckUiManager.GoToLandingPage();

            this.TestCaseVerify.IsTrue(
                checkComplaintAnalyserTileDisplayed,
                landingPage.ComplaintAnalyserTile.IsDisplayed(),
                "Complaint analyser tile is not displayed");

            var complaintAnalyserPage = QuickCheckUiManager.GoToComplaintAnalyserPage();

            landingPage = complaintAnalyserPage.BackToStartLink.Click<QuickCheckLandingPage>();

            this.TestCaseVerify.IsTrue(
                checkBackToStartButtonNavigation,
                landingPage.ComplaintAnalyserTile.IsDisplayed(),
                "Back to start button did not navigate to LDA landing page");
        }

        ///<summary>
        ///Task 2253487
        ///Navigate to Complaint Analyzer File Upload Page,
        ///Upload a document for analysis
        ///Verify: History shows the uploaded document
        ///</summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void QuickCheckComplaintAnalyzerHistoryTest()
        {
            string checkHistoryTabShowsComplaintDocument = "Verify: Complaint analyser document is visible on history header";
            string checkHistoryPageShowsHistoryDocument = "Verify: Complaint analyser document is visible on history page";
            //string checkSignOffPage= "Verify: Complaint analyser document is visible on sign off page";
            string checkClientIdPage= "Verify: Complaint analyser document is visible on client id page";

            var complaintAnalyserPage = QuickCheckUiManager.GoToComplaintAnalyserPage();
            var documentName = "2023 WL 9108365.docx";
            var documentPath = $@"{TestDocsPath}\{documentName}";

            var uploadADocumentTab = complaintAnalyserPage.ComplaintAnalyzerTabPanel.SetActiveTab<UploadADocumentTab>(ComplaintAnalyzerTabs.UploadADocument);

            // Upload A Document
            uploadADocumentTab = uploadADocumentTab.UploadFile(documentPath);

            SafeMethodExecutor.WaitUntil(() => uploadADocumentTab.SafeAnalysisButton.Enabled);

            var complaintAnalyzerSkillLandingPage = uploadADocumentTab.SafeAnalysisButton.Click<ComplaintAnalyzerSkillLandingPage>();

            SafeMethodExecutor.WaitUntil(() => complaintAnalyzerSkillLandingPage.ProgressBarLabelText.Displayed);
            SafeMethodExecutor.WaitUntil(() => !complaintAnalyzerSkillLandingPage.ProgressBarLabelText.Displayed);

            // History tab
            var recentHistoryDialog = complaintAnalyserPage.Header.ClickHeaderTab<EdgeRecentHistoryDialog>(EdgeHeaderTabs.History);

            this.TestCaseVerify.IsTrue(
                checkHistoryTabShowsComplaintDocument,
                recentHistoryDialog.GetRecentSearchesItems().First().HistoryItemLink.Text.Equals(documentName)
                && recentHistoryDialog.GetRecentSearchesItems().First().MetaData.Replace(" ", string.Empty).Contains("LitigationDocumentAnalyzerReportComplaint"),
                "History event is NOT displayed on the History tab");

            var historyPage = recentHistoryDialog.ViewAllHistoryLink.Click<EdgeCommonHistoryPage>();

            this.TestCaseVerify.IsTrue(
                checkHistoryPageShowsHistoryDocument,
                historyPage.HistoryTable.GetGridItems().First().Title.Contains(documentName)
                && historyPage.HistoryTable.GetGridItems().First().Summary.Replace(" ", string.Empty).Equals("LitigationDocumentAnalyzerReportComplaint"),
                "Description is NOT correct on History page");

            //Sign off page
            var signOffPage = historyPage.Header.ClickHeaderTab<EdgeProfileSettingsDialog>(EdgeHeaderTabs.PreferencesAndSignOut).ClickSignOff();
            //var lastEventTitle = signOffPage.SignOffSessionDetailsComponent.GetSessionItems().First().GetDescriptionText().Replace("\r\n", string.Empty).Replace(" ", string.Empty);

            //this.TestCaseVerify.IsTrue(
            //    checkSignOffPage,
            //    lastEventTitle.Contains(documentName),
            //    "History event is NOT displayed on the Sign off page");

            //Client ID page
            var clientIdPage = this.SignOnBack();

            SafeMethodExecutor.WaitUntil(() => clientIdPage.RecentResearchPane.IsDisplayed());
            var lastEventTitle = clientIdPage.RecentResearchPane.RecentResearchList.First().Text.Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkClientIdPage,
                lastEventTitle.Contains(documentName),
                "History event is not displayed on the Client Id page");
        }
    }
}