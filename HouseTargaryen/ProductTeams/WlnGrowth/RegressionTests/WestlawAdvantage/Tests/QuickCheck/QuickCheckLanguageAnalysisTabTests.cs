namespace WestlawAdvantage.Tests.QuickCheck
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
	using Framework.Common.UI.Products.Shared.Enums.Delivery;
	using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.QuickCheck;
	using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.DocumentResults;
	using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
	using Framework.Common.UI.Products.WestlawEdge.Dialogs.QuickCheck;
	using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
	using Framework.Common.UI.Products.WestlawEdge.Items;
	using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
	using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
	using iTextSharp.text;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Quick Check Language Analysis tab tests
    /// </summary>
	[TestClass]
	public class QuickCheckLanguageAnalysisTabTests : WestlawAdvantageQuickCheckBaseTest
	{
		private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

		/// <summary>
		/// TASK 2197728, 2205288, 2210107, 2205279,2194896
		/// Upload a document through Check Work or Opponent paths
		/// Once completed, the user will be navigated to the Language Analysis tab
		/// Verify: If INDIGO PREMIUM F1 is on, Paraphrases will be displayed
		/// Verify: Unmatched quotes in language analysis tab is sorted properly
		/// Verify: AI disclosure messgae is displayed
		/// Verify: Check Paraphrases charecter limit and conditions
		/// </summary>
		[TestMethod]
		[TestCategory(CurrentTestCategory)]
		[TestCategory(TeamMatzekCategory)]
		[TestProperty(EnvironmentConstants.InfrastructureAccessControlsOn, "IAC-AI-QUICKCHECK-PARAPHRASE")]
		public void LanguageAnalysisTabTest()
		{
			const string FirstTestFileName = "FS59779.docx";
			const string SecondTestFileName = "BL26741.pdf";
			const string InfoMessageWithParaphrases = "Summary\r\nLanguage analysis compares paraphrases and quotations of five words or more to cases, statutes, regulations, court rules, and constitutions to corresponding content on Westlaw.\r\nParaphrases are non-quote statements citing to cases. Each paraphrase result has a potential mischaracterization associated with it.\r\n\"Matched quotations\" appear when quotations in the analyzed document are able to be matched to documents on Westlaw.\r\n\"Unmatched quotations\" appear when quotations cannot be confidently matched to documents on Westlaw or there are no corresponding documents on Westlaw (e.g., quotations to the record). A quotation may also be unmatched if its corresponding citation has an error or the quotation is to a repealed, transferred, or historical version of a statute or regulation.\r\n\"All quotations\" includes all matched quotations (with and without differences) and unmatched quotations.\r\nStatutes, constitutions, rules, and regulations\r\nAll quotations to statutes, regulations, court rules, and constitutions are compared to the most current version on Westlaw.";
			const string InfoMessageNoParaphrases = "Summary\r\nLanguage analysis compares paraphrases and quotations of five words or more to cases, statutes, regulations, court rules, and constitutions to corresponding content on Westlaw.\r\n\"Matched quotations\" appear when quotations in the analyzed document are able to be matched to documents on Westlaw.\r\n\"Unmatched quotations\" appear when quotations cannot be confidently matched to documents on Westlaw or there are no corresponding documents on Westlaw (e.g., quotations to the record). A quotation may also be unmatched if its corresponding citation has an error or the quotation is to a repealed, transferred, or historical version of a statute or regulation.\r\n\"All quotations\" includes all matched quotations (with and without differences) and unmatched quotations.\r\nStatutes, constitutions, rules, and regulations\r\nAll quotations to statutes, regulations, court rules, and constitutions are compared to the most current version on Westlaw.";
			const string TestFileName = "BL87490 3 1.docx";
			string testFilePath = $@"{TestDocsPath}\{TestFileName}";

			string checkInfoMessageWithParaphrases = "Verify: Check info message for results with paraphrases";
			string checkParaphrasesDisplayed = "Verify: Paraphrases is displayed";
			string checkParaphrasesDataDisplayed = "Verify: Paraphrases from the uploaded document is/are displayed";
			string checkParaphrasesAiDisclosure = "Verify: AI Disclosure is displayed";
			string checkInfoMessageNoParaphrases = "Verify: Check info message for results without paraphrases";
			string checkUnmatchedQuotesSortingOrder = "Verify: Unmatched quotes from the uploaded document is/are correctly sorted";
			string checkParaphrasesCharacterLimit = "Verify: Pre and Post Paraphrases Character Limit is less than 150 characters";

			var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>($@"{TestDocsPath}\{FirstTestFileName}");

			var languageAnalysisTab = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab();

			var languageAnalysisInfoDialog = languageAnalysisTab.NarrowPane.InfoIconButton.Click<QuotationAnalysisInfoDialog>();

			var infoMessage = languageAnalysisInfoDialog.InfoMessageLabel.Text;

			this.TestCaseVerify.IsTrue(
				checkInfoMessageWithParaphrases,
				infoMessage.Equals(InfoMessageWithParaphrases),
				"Info message with is incorrect");

			languageAnalysisTab = languageAnalysisInfoDialog.CloseButton.Click<LanguageQuotationAnalysisTab>();

			this.TestCaseVerify.IsTrue(
				checkParaphrasesDisplayed,
				languageAnalysisTab.NarrowPane.ParaphrasesLink.Displayed,
				"Paraphrases is NOT displayed");

			languageAnalysisTab = languageAnalysisTab.NarrowPane.ParaphrasesLink.Click<LanguageQuotationAnalysisTab>();

			this.TestCaseVerify.IsTrue(
				checkParaphrasesDataDisplayed,
				languageAnalysisTab.ResultList.All(firstItem => firstItem.ParaphrasesResultsLabels.All(secondItem => secondItem.Text.Contains("Language from the analyzed document"))),
				"Paraphrases from the uploaded document is/are NOT displayed");

			var isParaphrasesCharacterLimitValid = languageAnalysisTab.ResultList.All(item =>
			{
				var preQuoteText = item.PreQuoteParaphraseLabel.Text?.Trim();
				var postQuoteText = item.PostQuoteParaphraseLabel.Text?.Trim();

				bool isPreValid = !string.IsNullOrWhiteSpace(preQuoteText) // to check if QuoteText is empty
					 && preQuoteText.Length <= 160 // to check if the QuoteText length is less than 150 char
					 && preQuoteText.StartsWith("...") // to check if QuoteText starts with ...
					 && preQuoteText.Length > 3 // to check if there is no single ._/ special charecters
					 && char.IsLetterOrDigit(preQuoteText[3]) // to check if the 4th charecter is a letter or digit
					 && preQuoteText.Any(char.IsLetterOrDigit); // doesn't start with punctuation

				bool isPostValid = !string.IsNullOrWhiteSpace(postQuoteText)
					 && postQuoteText.Length <= 160
					 && postQuoteText.EndsWith("...")
					 && postQuoteText.Length > 3// to check if it does not contain only "..."
					 && char.IsLetterOrDigit(postQuoteText[0])
					 && postQuoteText.Any(char.IsLetterOrDigit);
				return isPreValid && isPostValid;
			});

			this.TestCaseVerify.IsTrue(
				checkParaphrasesCharacterLimit,
				isParaphrasesCharacterLimitValid,
				"Pre and Post Quote paraphrases is not displayed as expected");

			this.TestCaseVerify.IsTrue(
				checkParaphrasesAiDisclosure,
				languageAnalysisTab.ResultList.All(firstItem => firstItem.AiDisclosureLabels.All(secondItem => secondItem.Text.Contains("These summaries use generative AI and should be verified for accuracy"))),
				"AI diclosure message is not displayed");

			languageAnalysisTab = languageAnalysisTab.NarrowPane.UnmatchedQuotationsLink.Click<LanguageQuotationAnalysisTab>();

			var uiQuotes = languageAnalysisTab.ResultList;
			var uiQuotesText = languageAnalysisTab.ResultList.First().QuoteTextLabels;

			List<string> sortedQuotes = new List<string>();

			if (uiQuotes.Count() > 0)
			{
				sortedQuotes = GetSortedQuotes(uiQuotes, sortedQuotes);

				int index = 0;
				foreach (var uiQuoteText in uiQuotesText)
				{
					this.TestCaseVerify.IsTrue(
						checkUnmatchedQuotesSortingOrder,
						uiQuoteText.Text.Equals(sortedQuotes[index]),
						"Unmatched quotes from the uploaded document is/are NOT correctly sorted");
					index++;
				}
			}

			reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>($@"{TestDocsPath}\{SecondTestFileName}");

			languageAnalysisTab = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab();

			languageAnalysisInfoDialog = languageAnalysisTab.NarrowPane.InfoIconButton.Click<QuotationAnalysisInfoDialog>();

			infoMessage = languageAnalysisInfoDialog.InfoMessageLabel.Text;

			this.TestCaseVerify.IsTrue(
				checkInfoMessageNoParaphrases,
				infoMessage.Equals(InfoMessageNoParaphrases),
				"Info message without is incorrect");
		}

        /// <summary>
        /// TASK 2206337, 2218634, 2206344, 2210437
        /// Upload a document through Check Work or Opponent paths
        /// Once completed, the user will be navigated to the Language Analysis tab
        /// Select download list and include citation issues
        /// Verify: Citations are present in the download document
        /// Verify: View All list report - changed title from "Quotations (count)" to "View all (count)"
        /// </summary>
        [TestMethod]
		[TestCategory(CurrentTestCategory)]
		[TestCategory(TeamMatzekCategory)]
		public void LanguageAnalysisTabDeliveryTest()
		{
			const string TestFileName = "FS59779.docx";
			string testFilePath = $@"{TestDocsPath}\{TestFileName}";

			string checkCitationsPresent = "Verify: Citations are present in the downloaded document";
			string checkDocDelivery = "Verify: List of items 'Word' delivery file exists after download";
			string checkNoQuickCheckDelivery = "Verify: No 'Quick Check' in the delivered document";
			string checkDocDeliveryViewAllCount = "Verify: View All list report title is correct";
			string checkDocDeliveryParaphrasesCount = "Verify: Paraphrases list report title is correct and 'Key:differences' not appearing on top";
			string checkDocDeliveryParaphrasesKeyDifferencesCount = "Verify: 'Key:differences' is NOT present more than once in the doc for each paraphrases quote";
            string checkContentsDelivery = "Verify: List of items delivery shows language analysis";
            //string checkHeaderDelivery= "Verify: Header contains report generated date and language analysis";
            string checkLanguageAnalysisDeliveryText= "Verify: Language analysis delivery text is present under paraphases, quotation and view all in the downloaded document";
            
			var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>(testFilePath);

			var languageAnalysisTab = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab();

			var deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
			deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
			deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
			deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<LanguageQuotationAnalysisTab>();

            //Bug 2218641: NGDA: Delivery: Report generated date not as system date
            //var fileGeneratedDate = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow).ToString("MMMM dd, yyyy 'at' h:mm tt");

            var initialAllQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;
            var fileNameFullReport = $"Westlaw Advantage - List of {initialAllQuotationsCount} Language analysis documents from FS59779docx.pdf";

			FileUtil.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

			var reportText = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport));
			var citationLabelText = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().CitationIssuesButton.Text;

			this.TestCaseVerify.IsTrue(
				checkCitationsPresent,
				reportText.Contains(citationLabelText),
				"Citation issues is not present in the downloaded pdf");

			this.TestCaseVerify.IsTrue(
				checkDocDeliveryViewAllCount,
				reportText.Contains($"View all ({initialAllQuotationsCount})"),
				"View All list report title is not correct");

            this.TestCaseVerify.IsTrue(
              checkContentsDelivery,
              this.CleanText(reportText).Contains("Report containsLanguage analysis"),
              "Report contents not showing language analysis in downloaded pdf");

            //Bug 2218641: NGDA: Delivery: Report generated date not as system date
            //this.TestCaseVerify.IsTrue(
            //   checkHeaderDelivery,
            //   this.CleanText(reportText).Contains($"Report generated: {fileGeneratedDate} • Language analysis"),
            //   "Header does not show Language analysis in the downloaded pdf");

            languageAnalysisTab = languageAnalysisTab.NarrowPane.ParaphrasesLink.Click<LanguageQuotationAnalysisTab>();

			var initialParaphrasesQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
			deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
			deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
			deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<LanguageQuotationAnalysisTab>();

			fileNameFullReport = $"Westlaw Advantage - List of {initialParaphrasesQuotationsCount} Language analysis documents from FS59779docx.pdf";

			FileUtil.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            reportText = this.CleanText(PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport)));

			this.TestCaseVerify.IsTrue(
				checkDocDeliveryParaphrasesCount,
                reportText.Contains($"Paraphrases ({initialParaphrasesQuotationsCount})")
				&& reportText.Contains("document to the language in the cited documents on Westlaw. Indicates content generated by AI. Not legal advice. A qualified professional must verify accuracy and legalcompliance.Key:  differences"),
				"Paraphrases list report title is not correct or 'Key:differences' not appearing on top");

			int occurrences = reportText.Split(new[] { "Key:differences" }, StringSplitOptions.None).Length - 1;

			this.TestCaseVerify.IsFalse(
			   checkDocDeliveryParaphrasesKeyDifferencesCount,
			   occurrences > initialParaphrasesQuotationsCount,
			   "'Key:differences' is present more than once in the doc for each paraphrases quote.");

			languageAnalysisTab = languageAnalysisTab.NarrowPane.ViewAllLink.Click<LanguageQuotationAnalysisTab>();
			         
            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<LanguageQuotationAnalysisTab>();

            fileNameFullReport = $"Westlaw Advantage - Full report from FS59779docx.pdf";

            reportText = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport));

            var languageAnalysisDeliverytext = "Use Language analysis to compare paraphrases, quotations, and the surrounding context from the analyzed";

            this.TestCaseVerify.IsTrue(
               checkLanguageAnalysisDeliveryText,
               reportText.Contains(languageAnalysisDeliverytext),
               "Language analysis text is not present in the delivered document");

            this.TestCaseVerify.IsFalse(
               checkNoQuickCheckDelivery,
               reportText.Contains("Quotation Analysis")
               && reportText.Contains("Quick Check"),
               "Quotation analysis text is present in the delivered document");

            deliveryDialog = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Doc);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<LanguageQuotationAnalysisTab>();

			fileNameFullReport = $"Westlaw Advantage - List of {initialAllQuotationsCount} Language analysis documents from FS59779docx.doc";

			FileUtil.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

			this.TestCaseVerify.IsTrue(
				checkDocDelivery,
				File.Exists(Path.Combine(this.FolderToSave, fileNameFullReport)),
				"List of items 'Word' delivery file does not exist after download.");
		}

		/// <summary>
		/// TASK 2206462, 2206463, 2245534
		/// Upload a document through Check Work or Opponent paths
		/// Verify: Check all filter in language analysis tab
		/// Verify: Check Potential mischaracterization facet in language analysis tab
		/// Verify: Clicking on citation link navigates to the document
		/// Verify: Clicking on back button pinpoints to citation where we came from
		/// </summary>
		[TestMethod]
		[TestCategory(CurrentTestCategory)]
		[TestCategory(TeamMatzekCategory)]
		public void LanguageAnalysisTabFacetTest()
		{
			const string TestFileName = "FS59779.docx";
			const string FacetItem = "Potential mischaracterizations";

			string checkQuotationTypeOptions = "Verify: Quotation type options are correct";
			string checkNumberOfResultsForAllQuotations = "Verify: Count of quotations equals the number near 'All quotations' option";
			string checkNumberOfResultsForMatchedQuotations = "Verify: Count of quotations equals the number near 'Matched quotations' option";
			string checkNumberOfResultsForUnmatchedQuotations = "Verify: Count of quotations equals the number near 'Unmatched quotations' option";
			string checkNumberOfResultsForViewAll = "Verify: Count of all items equals the number near 'View All' option";
			string checkNumberOfResultsForParaphases = "Verify: Count of all items equals the number near 'Paraphases' option";
			string checkMischaracterizationIdenticationFacetIsDisplayed = "Verify: Mischaracterization Identification facet is displayed";
			string checkMischaracterizationIdenticationFacetWorks = "Verify: Potential mischaracterization Filter works";
			string checkPotentialMischaracterizationCount = "Verify: Potential mischaracterization count is displyed correctly";
            string checkDocumentTitleLink = "Verify: Document title link click opens document page";
            string checkDocumentName = "Verify: Clicked Document is opened correclty";
            string checkDocumentPinpointWorks = "Verify: Return to list button pinpoints document we came from";

            var expectedOptions = new List<QuotationType>
			{
				QuotationType.ViewAll,
				QuotationType.Paraphrases,
				QuotationType.MatchedQuotations,
				QuotationType.UnmatchedQuotations,
				QuotationType.AllQuotations
			};

			var reportPage = QuickCheckUiManager.UploadFile<WestlawAdvantageQuickCheckRecommendationsPage>($@"{TestDocsPath}\{TestFileName}");

			var languageAnalysisTab = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab();

			this.TestCaseVerify.IsTrue(
			   checkQuotationTypeOptions,
			   reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetQuotationTypeItems().ToList().SequenceEqual(expectedOptions),
			   "Quotations type options are NOT correct");

			int resultListQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			this.TestCaseVerify.AreEqual(
				checkNumberOfResultsForViewAll,
				resultListQuotationsCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.ViewAll),
				"Count of all items doesn't equal the number near 'View All' option");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.MatchedQuotations);

			resultListQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			this.TestCaseVerify.AreEqual(
				checkNumberOfResultsForMatchedQuotations,
				resultListQuotationsCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.MatchedQuotations),
				"Count of quotations doesn't equal the number near 'Matched quotations' option");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.UnmatchedQuotations);

			resultListQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			this.TestCaseVerify.AreEqual(
				checkNumberOfResultsForUnmatchedQuotations,
				resultListQuotationsCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.UnmatchedQuotations),
				"Count of quotations doesn't equal the number near 'Unmatched quotations' option");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.AllQuotations);

			resultListQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			this.TestCaseVerify.AreEqual(
				checkNumberOfResultsForAllQuotations,
				resultListQuotationsCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.AllQuotations),
				"Count of quotations doesn't equal the number near 'All quotations' option");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.Paraphrases);

			resultListQuotationsCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count;

			this.TestCaseVerify.AreEqual(
				checkNumberOfResultsForParaphases,
				resultListQuotationsCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.Paraphrases),
				"Count of paraphases doesn't equal the number near 'Paraphases' option");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.ViewAll);

			this.TestCaseVerify.IsTrue(
				checkMischaracterizationIdenticationFacetIsDisplayed,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.MischaracterizationIdentificationFacet.IsDisplayed()
				&& reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.MischaracterizationIdentificationFacet.IsCheckboxDisplayed(FacetItem),
				"Mischaracterization Identification facet is NOT displayed");

			this.TestCaseVerify.IsTrue(
				checkPotentialMischaracterizationCount,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Where(item => item.PotentialMischaracterizationBox.IsDisplayed()).Count().Equals(reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.MischaracterizationIdentificationFacet.GetItemCountByName(FacetItem)),
				"Potential Mischaracterization count is not displayed correctly");

			reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.MischaracterizationIdentificationFacet.ApplyFacet<WestlawAdvantageQuickCheckRecommendationsPage>(true, FacetItem);
			var facetCount = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.MischaracterizationIdentificationFacet.GetItemCountByName(FacetItem);

			this.TestCaseVerify.IsTrue(
				checkMischaracterizationIdenticationFacetWorks,
				reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.All(item => item.PotentialMischaracterizationBox.IsDisplayed())
				&& reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().ResultList.Count.Equals(facetCount)
				&& reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.ViewAll).Equals(facetCount),
				"Potential mischaracterization filter doesn't work");

            var documentName = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().CitationLink.Last().Text;
            var documentPage = reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().CitationLink.Last().Click<EdgeCommonDocumentPage>();
            
			BrowserPool.CurrentBrowser.CreateTab(DocumentTab);
			BrowserPool.CurrentBrowser.ActivateTab(DocumentTab);

            SafeMethodExecutor.WaitUntil(() => documentPage.IsDocumentLoaded());

            this.TestCaseVerify.IsTrue(
                checkDocumentTitleLink,
                documentPage.IsDocumentLoaded(),
                "Click on Document link doesn't open document page");

            this.TestCaseVerify.IsTrue(
                checkDocumentName,
                documentName.Contains(documentPage.GetDocumentTitle()),
                "Document is not opened correctly");

            reportPage = documentPage.FixedHeader.ClickReturnToListButton<WestlawAdvantageQuickCheckRecommendationsPage>();

            this.TestCaseVerify.IsTrue(
                checkDocumentPinpointWorks,
                reportPage.WestlawAdvantageReportTabsPanel.GetLanguageAnalysisTab().CitationLink.Last().IsInView,
                "Return to list button does not pinpoint document we came from");
        }

        /// <summary>
        /// Get sorted quotes from the whole result list
        /// </summary>
        /// <param name="uiQuotes"></param>
        /// <param name="sortedQuotes"></param>
        /// <returns> List of Sorted quotes </returns>
        public List<string> GetSortedQuotes(QuickCheckItemsCollection<QuotationAnalysisItem> uiQuotes, List<string> sortedQuotes)
		{
			int index = 0;
			foreach (var uiQuote in uiQuotes)
			{
				var isPreQuoteTextLinkDisplayed = uiQuote.PreQuoteTextLink.Displayed;
				var isPostQuoteTextLinkDisplayed = uiQuote.PostQuoteTextLink.Displayed;
				var quoteLabel = uiQuote.QuoteTextLabels.ElementAt(index).Text;

				if (isPreQuoteTextLinkDisplayed && isPostQuoteTextLinkDisplayed)
				{
					sortedQuotes.Add(quoteLabel);
				}
				else if (!isPreQuoteTextLinkDisplayed && isPostQuoteTextLinkDisplayed)
				{
					sortedQuotes.Add(quoteLabel);
				}
				else if (isPreQuoteTextLinkDisplayed && !isPostQuoteTextLinkDisplayed)
				{
					sortedQuotes.Add(quoteLabel);
				}
				else
				{
					sortedQuotes.Add(quoteLabel);
				}
				index++;
			}
			return sortedQuotes;
		}

        private string CleanText(string text)
        {
            text = text.Replace("\r\n", string.Empty).Replace("\r\n", string.Empty);

            return text;
        }
    }
}