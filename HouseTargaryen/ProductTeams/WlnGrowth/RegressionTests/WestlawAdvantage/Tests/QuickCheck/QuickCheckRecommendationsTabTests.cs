using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
using Framework.Common.UI.Products.Shared.Enums.Content;
using Framework.Common.UI.Products.Shared.Enums.Delivery;
using Framework.Common.UI.Products.Shared.Managers;
using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
using Framework.Common.UI.Raw.EnhancementTests.Utils;
using Framework.Common.UI.Utils;
using Framework.Core.CommonTypes.Constants;
using Framework.Core.Utils.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;

namespace WestlawAdvantage.Tests.QuickCheck
{
    /// <summary>
    /// Quick Check Recommendations tab tests
    /// </summary>
    [TestClass]
    public class QuickCheckRecommendationsTabTests : WestlawAdvantageQuickCheckBaseTest
    {
        /// <summary>
        /// Task 2239289
        /// Verify Jurisdiction picker and jurisdiction info for zero citations
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        [TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-QUICKCHECK-US-DOC-RAS-CUTOVER")]
        public void AdvantageQuickCheckCheckYourWorkOpposingNoCitationsTest()
        {
            const string DocName = "NoCitations";
            const string DocExtension = "docx";

            const string PartyType = "Plaintiff";
            const string JurisdictionsNoPartyMessage = "Please select document author.";
            const string JurisdictionsNoCitationsMessage = "Quick Check was unable to find citations matching to authority on Westlaw in the uploaded document. Please select up to three jurisdictions to continue the analysis.";
            const string OpposingJurisdictionsNoCitationsMessage = "Quick Check was unable to find citations matching to authority on Westlaw in the uploaded document. Please select document author and up to three jurisdictions to continue the analysis.";
            const string JurisdictionsInfoMessage = "The results are based on selected jurisdictions: Alabama, All Federal";

            string filePath = $@"{TestDocsPath}\{DocName}.{DocExtension}";

            string checkJurisdictionsInfoMessage = "Verify: Jurisdiction info message is correct";
            string checkJurisdictionsInfoMessageOnRecPage = "Verify: Jurisdictions info box message is correct";
            string checkItemIsCompletedInTray = "Verify: Report item status is changed to Completed";
            string checkJurisdictionsInfoMessageInReport = "Verify: Delivered report contains Jurisdicons info message like on the Recommendations page";

            //Opponent's view
            string checkOpposingJurisdictionsInfoMessage = "Verify: Jurisdictions info message is correct for Opposing";
            string checkOpposingJurisdictionsNoPartyInfoMessage = "Verify: Jurisdictions No Party info message is correct for Opposing";
            string checkOpposingJurisdictionsInfoMessageOnRecPage = "Verify: Jurisdictions info box message is correct for Opposing";
            string checkOpposingRecommendationDocumentDetailpartyType = "Verify: Party type is the same as was selected during uploading for Opposing";

            QuickCheckUploadPage uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage();

            JurisdictionOptionsDialog jurisdictionDialog = uploadPage.UploadFile<JurisdictionOptionsDialog>(filePath);

            SafeMethodExecutor.WaitUntil(() => jurisdictionDialog.QuickCheckNoCitationsLabel.Displayed);

            this.TestCaseVerify.AreEqual(
                checkJurisdictionsInfoMessage,
                JurisdictionsNoCitationsMessage,
                jurisdictionDialog.QuickCheckNoCitationsLabel.Text,
                "Jurisdiction selector No Citations warning message is not correct");

            jurisdictionDialog.SelectJurisdictions(true, Jurisdiction.Alabama, Jurisdiction.AllFederal);

            var uploadDialog = jurisdictionDialog.SaveButton.Click<QuickCheckFileUploadDialog>();

            var reportPage = uploadDialog.WaitUntilFileUploadAndGetReportPage();

            this.TestCaseVerify.IsTrue(
                checkJurisdictionsInfoMessageOnRecPage,
                reportPage.ReportTabsPanel.GetRecommendationsTab().JurisdictionsInfoLabel.Text.Equals(JurisdictionsInfoMessage),
                "Jurisdictions info message is not correct");

            this.TestCaseVerify.IsTrue(
                checkItemIsCompletedInTray,
                reportPage.ReportTray.Expand().First().ReportStatusLabel.Text.Equals("Complete"),
                "Report status is not changed after uploading a document");

            var deliveryDialog = reportPage.ReportTabsPanel.GetRecommendationsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<RecommendationsTab>();

            string fileNameFullReport = $"Westlaw Advantage - Full report from {DocName}{DocExtension}.pdf";

            FileUtils.WaitForFileDownload(FolderToSave, fileNameFullReport);

            string text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileNameFullReport));

            this.TestCaseVerify.IsTrue(
                checkJurisdictionsInfoMessageInReport,
                text.Contains(JurisdictionsInfoMessage),
                "Delivered document doesn't contain Jurisdictions info message");

            //Opponent's view     
            uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage(WhatYouWouldLikeToDoOptions.AnalyzeOpponents);
            jurisdictionDialog = uploadPage.UploadFile<JurisdictionOptionsDialog>(filePath);

            SafeMethodExecutor.WaitUntil(() => jurisdictionDialog.QuickCheckNoCitationsLabel.Displayed);

            this.TestCaseVerify.AreEqual(
                checkOpposingJurisdictionsInfoMessage,
                OpposingJurisdictionsNoCitationsMessage,
                jurisdictionDialog.QuickCheckNoCitationsLabel.Text,
                "Jurisdiction selector No Citations warning message is not correct for Opposing");

            jurisdictionDialog.SelectJurisdictions(true, Jurisdiction.Alabama, Jurisdiction.AllFederal).SaveButton.Click<JurisdictionOptionsDialog>();

            this.TestCaseVerify.AreEqual(
                checkOpposingJurisdictionsNoPartyInfoMessage,
                JurisdictionsNoPartyMessage,
                jurisdictionDialog.QuickCheckNoPartyLabel.Text,
                "Jurisdiction selector No Party warning message is not correct for Opposing");

            jurisdictionDialog.QuickCheckPartiesButtons.First(item => item.Text.Equals(PartyType)).Select();
            uploadDialog = jurisdictionDialog.SaveButton.Click<QuickCheckFileUploadDialog>();
            reportPage = uploadDialog.WaitUntilFileUploadAndGetReportPage();

            this.TestCaseVerify.IsTrue(
                checkOpposingJurisdictionsInfoMessageOnRecPage,
                reportPage.ReportTabsPanel.GetRecommendationsTab().JurisdictionsInfoLabel.Text.Equals(JurisdictionsInfoMessage),
                "Jurisdictions info message is not correct for Opposing");

            this.TestCaseVerify.IsTrue(
                checkOpposingRecommendationDocumentDetailpartyType,
                reportPage.ReportTabsPanel.GetRecommendationsTab().DocumentInformation.GetDetail(RecommendationDocumentDetail.MovantAtTrialLevel).Equals(PartyType),
                "Party type is not the same as was selected during uploading for Opposing");
        }
    }
}
