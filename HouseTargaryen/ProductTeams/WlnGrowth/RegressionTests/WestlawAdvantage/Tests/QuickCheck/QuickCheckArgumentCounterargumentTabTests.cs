namespace WestlawAdvantage.Tests.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawAdvantage.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawAdvantage.Enums.LitigationDocumentAnalyzer;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Quick Check Warnings tab tests
    /// </summary>
    [TestClass]
    public class QuickCheckArgumentCounterargumentTabTests : WestlawAdvantageQuickCheckBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

        /// <summary>
        /// TASK 2183016, 2193369, 2193372, 2189433, 2218470
        /// Upload a document through Check Work or Opponent paths
        /// Once completed, the user will be navigated to the Argument Summary tab
        /// Verify: Left pane navigation links
        /// Verify: Arguments tab is displayed with Arguments , counter arguments and counter argument support
        /// Verify: Clicking on the icons navigates to the corresponding Document
        /// Verify: Back button to navigate back to the previous page exist, it is scrolled to the position where you clicked on document link.
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void ArgumentCounterArgumentsTabCommonTest()
        {
            const string TestFileName = "Great Lakes International Trading - 3 - Defendant's Opposition to Cross-Motion.pdf";
            const string IssueTitle = "My Issue";
            const string LitigationDocumentAnalyzerTabNameOfPage = "Litigation Document Analyzer Opponent Results | Westlaw Advantage";

        string testFilePath = $@"{TestDocsPath}\{TestFileName}";

            string filePath = $@"{TestDocsPath}\PlainText.txt";
            string plainTextToEnter = File.ReadAllText(filePath, Encoding.Default);

            string checkLitigationDocumentAnalyzerTabDisplayed = "Verify: Litigation Document Analyzer tab name is displayed properly";
            string checkLeftColumnNavigation = "Verify: Left column navigation works as expected";
            string checkCounterArgumentsTabDisplayed = "Verify: Counter argument tab is displayed";
            string checkCounterArgumentsCounterArgumentsSummaryDisplayed = "Verify: Argument, counter argument and/or Counterargument support sections are displayed under all result items";
            string checkDocumentPageInSameTab = "Verify: Document page is opened in the same tab";
            string checkReturntoArgumentsAndCounterargumentsTab = "Verify: Return button is redirecting to Arguments and Counterarguments Tab link clicked before";
            string checkZeroStateArgumentsAndCounterargumentsMessage = "Verify: Zero state message in Argument and counter argument tab is correct";

            var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>(testFilePath, WhatYouWouldLikeToDoOptions.AnalyzeOpponents);

            this.TestCaseVerify.IsTrue(
                checkLitigationDocumentAnalyzerTabDisplayed,
                BrowserPool.CurrentBrowser.Title.Equals(LitigationDocumentAnalyzerTabNameOfPage, StringComparison.InvariantCultureIgnoreCase),
                "Litigation Document Analyzer tab name is displayed incorrectly");

            this.TestCaseVerify.IsTrue(
                checkCounterArgumentsTabDisplayed,
                reportPage.WestlawAdvantageReportTabsPanel.IsDisplayed(QuickCheckReportTabs.ArgumentsAndCounterArguments),
                "Argument and counter argument tab is not displayed");

            var argumentsAndCounterargumentsTab = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab();

            argumentsAndCounterargumentsTab.NarrowPane.ExpandNavigationLink(ArgumentsCounterargumentsNavigationType.Arguments);

            var titleLinkItem = argumentsAndCounterargumentsTab.NarrowPane.ArgumentsTitlesLinks.ElementAt(3);
            var titleValue = titleLinkItem.Text;

            argumentsAndCounterargumentsTab = titleLinkItem.Click<ArgumentCounterargumentTab>();
            SafeMethodExecutor.WaitUntil(() => argumentsAndCounterargumentsTab.ArgumentResultList.ElementAt(3).TitleLabel.IsInView);

            this.TestCaseVerify.IsTrue(
                checkLeftColumnNavigation,
                argumentsAndCounterargumentsTab.ArgumentResultList.ElementAt(3).TitleLabel.IsInView
                && argumentsAndCounterargumentsTab.ArgumentResultList.ElementAt(3).TitleLabel.Text.Contains(titleValue),
                "Left column navigation doesn't work as expected");

            this.TestCaseVerify.IsTrue(
                checkCounterArgumentsCounterArgumentsSummaryDisplayed,
                argumentsAndCounterargumentsTab.ArgumentResultList.All(firstItem => firstItem.ArgumentsCounterArgumentsAndSummaryLabels.First().Text.Contains("Argument")
                && firstItem.ArgumentsCounterArgumentsAndSummaryLabels.ElementAt(1).Text.Contains("Counterargument")
                && firstItem.ArgumentsCounterArgumentsAndSummaryLabels.Last().Text.Contains("Counterargument support")),
                "Argument, counter argument and/or Counterargument support sections are NOT displayed under all result items");

            var documentPage = argumentsAndCounterargumentsTab.ArgumentResultList.Last().DocumentTitleLinks.Last().Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue(
                checkDocumentPageInSameTab,
                documentPage.IsDocumentLoaded(),
                "Document page is NOT opened in the same tab");

            var citedWithPage = new CitedWithPage();

            argumentsAndCounterargumentsTab = documentPage.FixedHeader.ClickReturnToListButton<ArgumentCounterargumentTab>();

            this.TestCaseVerify.IsTrue(
                checkReturntoArgumentsAndCounterargumentsTab,
                argumentsAndCounterargumentsTab.ArgumentResultList.Last().DocumentTitleLinks.Last().IsInView,
                "Return button is NOT redirecting to Arguments and Counterarguments Tab link clicked before");

            reportPage = QuickCheckUiManager.EnterTextWithCitedAuthority<WestlawAdvantageQuickCheckRecommendationsPage>(IssueTitle, plainTextToEnter);

            argumentsAndCounterargumentsTab = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab();

            this.TestCaseVerify.IsTrue(
                checkZeroStateArgumentsAndCounterargumentsMessage,
                argumentsAndCounterargumentsTab.ZeroStateArgumentsMessageLabel.Text.Equals("No arguments found\r\n\r\nLitigation Document Analyzer was unable to find any arguments in the uploaded document. Please view the other tabs in this report for other helpful information about the document.\r\nYou can also try uploading your document again from the Litigation Document Analyzer home page."),
                "Zero state message in Argument and counter argument tab is NOT correct");
        }

        /// <summary>
        /// TASK 2197817, 2218470
        /// Verify Display KeyCite Flags are displayed
        /// Verify Clicking in KeyciteFlag navigates to Negative Treatement pages
        /// Verify Clciking on return button in Negative Treatement page navigates back to Arguments Tab and points to last clicked KeyCite Flag
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void ArgumentCounterArgumentsKeyCiteFlagTest()
        {
            const string TestFileName = "Great Lakes International Trading - 3 - Defendant's Opposition to Cross-Motion.pdf";
            const string LitigationDocumentAnalyzerTabNameOfPage = "Litigation Document Analyzer Your Work Results | Westlaw Advantage";

            string testFilePath = $@"{TestDocsPath}\{TestFileName}";

            string checkLitigationDocumentAnalyzerTabDisplayed = "Verify: Litigation Document Analyzer tab name is displayed properly";
            string keyCiteFlagsAreDisplayed = "Verify: KeyCite flags are displayed";
            string checkNegativeTreatmentPage = "Verify: KeyCite flag brings to the Negative Treatment page";
            string checkYellowKeyCiteFlag = "Verify: KC flag on the Argument page and KC flag and on the Negative Treatment are the same";
            string checkPinpointWorks = "Verify: Return To List button brings user to third KeyCite Flag Argument item";

            var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>(testFilePath);

            this.TestCaseVerify.IsTrue(
                checkLitigationDocumentAnalyzerTabDisplayed,
                BrowserPool.CurrentBrowser.Title.Equals(LitigationDocumentAnalyzerTabNameOfPage, StringComparison.InvariantCultureIgnoreCase),
                "Litigation Document Analyzer tab name is displayed incorrectly");

            this.TestCaseVerify.IsTrue(
                keyCiteFlagsAreDisplayed,
                reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().ArgumentResultList.Any(item => !item.Equals(KeyCiteFlag.NoFlag)),
                "Key cite flags are NOT displayed");

            var pinpointedArgumentGuid = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab()
                                                       .ArgumentResultList.First(item => item.KeyCiteFlag.Equals(KeyCiteFlag.Yellow))
                                                       .KeyCiteTitleLabel.Text;

            var negativeTreatmentPage = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab()
                                                  .ArgumentResultList.First(item => item.KeyCiteFlag.Equals(KeyCiteFlag.Yellow))
                                                  .ClickKeyCiteFlag<EdgeNegativeTreatmentPage>();

            this.TestCaseVerify.IsTrue(
                checkNegativeTreatmentPage,
                negativeTreatmentPage.IsNegativeTreatmentPage(),
                "KeyCite flag link doesn't work");

            this.TestCaseVerify.AreEqual(
                checkYellowKeyCiteFlag,
                KeyCiteFlag.Yellow,
                negativeTreatmentPage.FixedHeader.GetKeyCiteFlag(),
                "KC flag on the Arguments page and KC flag on the Negative Treatment are different");

            reportPage = negativeTreatmentPage.FixedHeader.ClickReturnToListButton<WestlawAdvantageQuickCheckRecommendationsPage>();

            SafeMethodExecutor.WaitUntil(() => reportPage.WestlawAdvantageReportTabsPanel.IsDisplayed(QuickCheckReportTabs.ArgumentsAndCounterArguments));

            this.TestCaseVerify.IsTrue(
                checkPinpointWorks,
                reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().ArgumentResultList.First(item => item.KeyCiteFlag.Equals(KeyCiteFlag.Yellow)
                    && item.KeyCiteTitleLabel.Text.Equals(pinpointedArgumentGuid)).IsInView(),
               "Entered Argument is NOT in view");
        }

        /// <summary>
        /// NGDA: Counterarguments: (Report) Support PDF Delivery - Full report, Verify AI Discloure Message in  Argument And Counterargument Tab
        /// Counterarguments: (Report) Support PDF Delivery - List view
        /// 2205901, 2210123, 2210164, 2256021, Bug 2200053
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void ArgumentCounterArgumentsDeliveryTest()
        {
            const string TestFileName = "Great Lakes International Trading - 3 - Defendant's Opposition to Cross-Motion.pdf";
            const string DocumentSummaryMessage = "Summaries use generative AI and should be verified for accuracy.";
            const string AiDisclosureMessage = "Generated by AI. Not legal advice. A qualified professional must verify accuracy and legal compliance.";

            string deliveredDocumentSignatureText = $"Westlaw Advantage. © {DateTime.Now.Year} Thomson Reuters. No claim to original U.S. Government Works.";
            string testFilePath = $@"{TestDocsPath}\{TestFileName}";
            List<string> expectedDeliveryOptionsList = new List<string> { "List of items", "Documents", "Full report" };
            List<string> expectedDeliveryDataList = new List<string> { "Argument", "Counter argument", "Counter argument support" };

            string checkDocumentSummaryIsDisplayed = "Verify: Document summary is displayed in the Arguments and Counterarguments tab";
            string checkArgumentsAndCounterargumentCheckboxDisplayed = "Verify: Argument and Counterargument checkbox is displayed in full report Download";
            string checkArgumentsAndCounterargumentCheckboxChecked = "Verify: Argument and Counterargument checkbox is Checked by default in full report Download";
            string checkArgumentsAndCounterargumentTabDataInReportPdf = "Verify: Argument and Counterargument Tab Data is present in the downloaded full report file PDF";
            string checkArgumentsAndCounterargumentTabDataInReportWord = "Verify: Argument and Counterargument Tab Data is present in the downloaded full report file Word";
            string checkAiDisclosureMessageIsDisplayed = "Verify: AI Disclosure is displayed in the Arguments and Counterarguments tab";
            string checkAiDisclosureMessageIsDisplayedInReport = "Verify: AI Disclosure message is displayed in the downloaded full report";
            string checkOnlyPDFFormatIsDisplayed = "Verify: Only PDF format is displayed in the delivery dialogue from Argument and counterargument tab";
            string checkArgumentsAndCounterargumentTabDataInListReportPDF = "Verify: Argument and Counterargument Tab Data is present in the downloaded list of view report file PDF";
            string checkArgumentsAndCounterargumentTabDataInListReportWord = "Verify: Argument and Counterargument Tab Data is present in the downloaded list of view report file Word";
            string checkArgumentsAndCounterargumentTabDataInListReportRTF = "Verify: Argument and Counterargument RTF delivery works";
            string checkDeliverOptions = "Verify: Delivery options contain List of items, Full report";

            var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>(testFilePath);
            var argumentsAndCounterargumentsTab = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab();

            argumentsAndCounterargumentsTab = argumentsAndCounterargumentsTab.DocumentSummary.DocumentSummaryExpandLink.Click<ArgumentCounterargumentTab>();

            string documentSummaryData = argumentsAndCounterargumentsTab.DocumentSummary.DocumentSummaryContentLabel.Text.Replace(Environment.NewLine, " ");

            var argumentHeaders = argumentsAndCounterargumentsTab.ArgumentHeaderLabels.ToList();
            var argumentSubHeaders = argumentsAndCounterargumentsTab.ArgumentSubHeaderLabels.Select(item => item.Text).ToList();

            this.TestCaseVerify.IsTrue(
                checkDocumentSummaryIsDisplayed,
                argumentsAndCounterargumentsTab.DocumentSummary.DocumentSummaryLabel.Displayed
                && argumentsAndCounterargumentsTab.DocumentSummary.DocumentSummaryDisclaimerLabel.Text.Equals(DocumentSummaryMessage),
                "Document summary is not displayed under Argument and counter argument tab");

            this.TestCaseVerify.IsTrue(
                checkAiDisclosureMessageIsDisplayed,
                argumentHeaders.Count.Equals(argumentSubHeaders.Count)
                && argumentSubHeaders.All(item => item.Equals(AiDisclosureMessage)),
                "AI disclosure message is not displayed under Argument and counter argument tab");

            var itemsCount = argumentHeaders.Sum(item => item.Text.ConvertCountToInt());

            var deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsTrue(
                checkDeliverOptions,
                expectedDeliveryOptionsList.SequenceEqual(deliveryDialog.WhatToDeliver.GetAvailableWhatToDeliverLabelList()),
                "Delivery options are not correct for Argument and Counterargument delivery");

            this.TestCaseVerify.IsTrue(
                checkOnlyPDFFormatIsDisplayed,
                deliveryDialog.TheBasicsTab.FormatDropdown.SelectedOption.Equals(DeliveryFormat.Pdf),
                "PDF format is not Displayed in the delivery dialogue from Argument and counterargument tab");
            
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            string fileNameFullReport = itemsCount < 10 ? $"Westlaw Advantage - List of {itemsCount} Arguments and counterarguments documents from Great Lakes Internationa.pdf" : $"Westlaw Advantage - List of {itemsCount} Arguments and counterarguments documents from Great Lakes Internation.pdf";
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            var deliveredDocument = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport)).Replace(Environment.NewLine, " ").Replace(deliveredDocumentSignatureText, string.Empty);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInListReportPDF,
                expectedDeliveryDataList.All(item => deliveredDocument.Contains(item))
                && argumentSubHeaders.All(item => deliveredDocument.Contains(item)),
                "Argument, counter argument and/or Counterargument support sections are NOT displayed under delivered list of items PDF");
            
            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Doc);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            fileNameFullReport = itemsCount < 10 ? $"Westlaw Advantage - List of {itemsCount} Arguments and counterarguments documents from Great Lakes Internationa.doc" : $"Westlaw Advantage - List of {itemsCount} Arguments and counterarguments documents from Great Lakes Internation.doc"; ;
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            deliveredDocument = FileUtils.GetAllLinesFromFile(Path.Combine(this.FolderToSave, fileNameFullReport)).Aggregate<string>((line, nextLine) => line + nextLine).Replace(Environment.NewLine, " ").Replace(deliveredDocumentSignatureText, string.Empty);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInListReportWord,
                expectedDeliveryDataList.All(item => deliveredDocument.Contains(item))
                && argumentSubHeaders.All(item => deliveredDocument.Contains(item)),
                "Argument, counter argument and/or Counterargument support sections are NOT displayed under delivered list of items Word");

            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Rtf);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);            

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInListReportRTF,
                deliveryDialog.DownloadAndWaitForConfirmation<QuickCheckRecommendationsPage>(),
                "Argument and Counterargument RTF delivery DOESN'T work");
            
            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetRecommendationsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentCheckboxDisplayed,
                deliveryDialog.LayoutAndLimitsTab.IsIncludeSectionOptionDisplayed(LayoutAndLimitsInclude.ArgumentsAndCounterArguments),
                "In full report delivery Arguments And CounterArguments option is not displayed");

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentCheckboxChecked,
                deliveryDialog.LayoutAndLimitsTab.IsIncludeSectionOptionSelected(LayoutAndLimitsInclude.ArgumentsAndCounterArguments),
                "In full report delivery Arguments And CounterArguments option is not selected by default");

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            fileNameFullReport = "Westlaw Advantage - Full report from Great Lakes International Trading - 3 - Defendants Opposition.pdf";
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            deliveredDocument = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport)).Replace(Environment.NewLine, " ").Replace(deliveredDocumentSignatureText, string.Empty);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInReportPdf,
                deliveredDocument.Contains(documentSummaryData)
                && deliveredDocument.Contains(DocumentSummaryMessage)
                && expectedDeliveryDataList.All(item => deliveredDocument.Contains(item))
                && argumentSubHeaders.All(item => deliveredDocument.Contains(item)),
                "Delivered full report from Recommendation Tab doesn't contain Argument Tab document summary");

            //Verify AI Disclosure message is present in delivered document
            this.TestCaseVerify.IsTrue(
                checkAiDisclosureMessageIsDisplayedInReport,
                Regex.Matches(deliveredDocument, Regex.Escape(AiDisclosureMessage)).Count.Equals(argumentSubHeaders.Count),
                "Delivered full report from Recommendation Tab doesn't contain AI Disclosure message");
            
            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Doc);

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            fileNameFullReport = "Westlaw Advantage - Full report from Great Lakes International Trading - 3 - Defendants Opposition.doc";
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            deliveredDocument = FileUtils.GetAllLinesFromFile(Path.Combine(this.FolderToSave, fileNameFullReport)).Aggregate<string>((line, nextLine) => line + nextLine).Replace(Environment.NewLine, " ").Replace(deliveredDocumentSignatureText, string.Empty);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInReportWord,
                expectedDeliveryDataList.All(item => deliveredDocument.Contains(item))
                && argumentSubHeaders.All(item => deliveredDocument.Contains(item)),
                "Delivered full report from Recommendation Tab doesn't contain Argument Tab document summary Word");
            
            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            fileNameFullReport = "Westlaw Advantage - Full report from Great Lakes International Trading - 3 - Defendants Opposition (1).pdf";
            FileUtils.WaitForFileDownload(this.FolderToSave, fileNameFullReport);
            deliveredDocument = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport)).Replace(Environment.NewLine, " ").Replace(deliveredDocumentSignatureText, string.Empty);

            this.TestCaseVerify.IsTrue(
                checkArgumentsAndCounterargumentTabDataInReportPdf,
                deliveredDocument.Contains(documentSummaryData)
                && deliveredDocument.Contains(DocumentSummaryMessage)
                && expectedDeliveryDataList.All(item => deliveredDocument.Contains(item))
                && argumentSubHeaders.All(item => deliveredDocument.Contains(item)),
                "Delivered full report from Language Analysis tab doesn't contain Argument Tab document summary");
        }

        /// <summary>
        /// Task 2210653
        /// Upload document in LDA
        /// Select jurisdiction and federal
        /// Validate Argument and counter arguments are displayed
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void ArgumentCounterArgumentsNoSegmentTest()
        {
            const string TestFileName = "NoSegmentFile-Plantiff.docx";

            string testFilePath = $@"{TestDocsPath}\{TestFileName}";

            string checkArgumentsCounterArgumentsDisplayed = "Verify: Arguments and counter arguments are displayed when file with no segment is analysed";

            QuickCheckUploadPage uploadPage = QuickCheckUiManager.GoToDocAnalyzerUploadPage();

            JurisdictionOptionsDialog jurisdictionDialog = uploadPage.UploadFile<JurisdictionOptionsDialog>(testFilePath);

            SafeMethodExecutor.WaitUntil(() => jurisdictionDialog.QuickCheckNoCitationsLabel.Displayed);

            jurisdictionDialog.SelectJurisdictions(true, Jurisdiction.Alabama);

            var uploadDialog = jurisdictionDialog.SaveButton.Click<QuickCheckFileUploadDialog>();

            var reportPage = uploadDialog.WestlawAdvantageWaitUntilFileUploadAndGetReportPage();

            var argumentsAndCounterargumentsTab = reportPage.WestlawAdvantageReportTabsPanel.GetArgumentsAndCounterargumentsTab();

            this.TestCaseVerify.IsTrue(
                checkArgumentsCounterArgumentsDisplayed,
                argumentsAndCounterargumentsTab.DocumentSummary.DocumentSummaryLabel.Displayed,
                "Document summary is not displayed under Argument and counter argument tab");
        }
    }
}