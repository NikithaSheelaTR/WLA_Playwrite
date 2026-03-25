namespace WestlawAdvantage.Tests.CoCounsel
{
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Components.AALP.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums.CoCounsel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using WestlawAdvantage.Tests.QuickCheck;
    using Framework.Core.Utils.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Utils.Browser;
    using System.IO;
    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;

    /// <summary>
    /// Westlaw Advantage CoCounsel Chat Assistant tests
    /// </summary>
    [TestClass]
    public class AdvantageCoCounselChatAssistantTests : WestlawAdvantageQuickCheckBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

        /// <summary>
        /// Task 2206323
        /// Verify: All cards contains "Quotation" and "Paraphrases"
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void AdvantageCoCounselQuickCheckCommonTest()
        {
            const string Question = "I want to check for quotations in a brief for accuracy?";
            const string DocumentName = "FS59779.docx";

            string checkPotentialMischaracterizationResultsCards = "Verify: Potential mischaracterization cards display 'Quotation' and 'Paraphrases' data";
            string checkDeliveryIcon = "Verify: Delivery Icon is visible for the skill";
            string checkIdentifyingMischaracterizationsInReport = "Verify: 'Identifying Mischaracterizations' is present in the downloaded report";

            var coCounselChatAssistantDialog = new CoCounselChatAssistantDialog();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.MaximizeButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.QuestionTextbox.Displayed);

            coCounselChatAssistantDialog.Chat.QuestionTextbox.SendKeys(Question);
            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.SubmitButton.Click<CoCounselChatAssistantDialog>();

            SafeMethodExecutor.WaitUntil(() => coCounselChatAssistantDialog.Chat.UploadDocumentButton.Displayed);

            coCounselChatAssistantDialog.Chat.SelectUploadPath(QuickCheckOptions.CheckYourWork);

            var coCounselQuickCheckUploadDocumentDialog = coCounselChatAssistantDialog.Chat.UploadDocumentButton.Click<CoCounselQuickCheckUploadDocumentDialog>();

            coCounselQuickCheckUploadDocumentDialog = coCounselQuickCheckUploadDocumentDialog.UploadFile<CoCounselQuickCheckUploadDocumentDialog>(Path.Combine(TestDocsPath, DocumentName));

            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.UploadFileTextbox.Enabled);
            SafeMethodExecutor.WaitUntil(() => coCounselQuickCheckUploadDocumentDialog.DoneButton.Enabled);

            coCounselChatAssistantDialog = coCounselQuickCheckUploadDocumentDialog.DoneButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog = coCounselChatAssistantDialog.Chat.StartAiAssistedResearchButton.Click<CoCounselChatAssistantDialog>();

            coCounselChatAssistantDialog.Chat.QuickCheckProgressSpinnerLabel.WaitDisplayed(3000);

            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.QuickCheckProgressSpinnerLabel.Displayed);
            SafeMethodExecutor.WaitUntil(() => !coCounselChatAssistantDialog.Chat.QuickCheckProgressLinesLabel.Displayed);

            this.TestCaseVerify.IsTrue(
                checkPotentialMischaracterizationResultsCards,
                coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.All(item => item.PotentialMischaracterizationCard.CardTitleLabel.Text.Equals("Potential mischaracterization\r\nQuotation"))
                || coCounselChatAssistantDialog.Chat.CoCounselQuickCheckAnswerItems.All(item => item.PotentialMischaracterizationCard.CardTitleLabel.Text.Equals("Potential mischaracterization\r\nParaphrase")),
                "Potential mischaracterization cards don't display 'Quotation' and 'Paraphrases' data");

            this.TestCaseVerify.IsTrue(
                checkDeliveryIcon,
                coCounselChatAssistantDialog.Chat.DownloadButton.Displayed,
                "Download button is not displayed");

            coCounselChatAssistantDialog.Chat.DownloadButton.Click();

            string fileNameFullReport = $"Westlaw Advantage - Westlaw - My Citation.pdf";
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport));

            this.TestCaseVerify.IsTrue(
                checkIdentifyingMischaracterizationsInReport,
                text.Contains("Identifying Mischaracterizations"),
                "Downloaded report doesn't contain 'Identifying Mischaracterizations'");
        }
    }
}
