namespace WestlawAdvantage.Tests.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawAdvantage.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck.ReportTabs;
    using Framework.Common.UI.Products.WestlawEdge.Enums.QuickCheck;
    using Framework.Common.UI.Products.WestlawEdge.Pages.QuickCheck;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Judicial Language Analysis tab tests
    /// </summary>
    [TestClass]
    public class JudicialLanguageAnalysisTabTests : WestlawAdvantageQuickCheckBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageQuickCheck";

        /// <summary>
        /// Task 2210134, 2210035, 2218470
        /// Upload documents by parties
        /// Verify: 'Paraphrases' are displayed for both parties
        /// Verify: 'Paraphrases' filters works properly
        /// Verify: Mischaracterization Identification facet is displayed and works properly
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void JudicialLanguageAnalysisTabCommonTest()
        {
            const string Document1 = "Insurance Litigation - Party 1 - Northern 1 - Motion for Summary Judgment.docx";
            const string Document2 = "Insurance Litigation - Party 2 - Roberts 1 -  Opposition to MSJ and Cross Motion.docx";
            const string FacetItem = "Potential mischaracterizations";
            const string LitigationDocumentAnalyzerTabNameOfPage = "Litigation Document Analyzer Judicial Results | Westlaw Advantage";

            var assignment = new Dictionary<string, JudicialParties>
            {
                { Document1, JudicialParties.FirstParty },
                { Document2, JudicialParties.SecondParty }
            };

            string checkLitigationDocumentAnalyzerTabDisplayed = "Verify: Litigation Document Analyzer tab name is displayed properly";
            string checkParaphrasesFirstParty = "Verify: 'Paraphases' option exists for the 1st party";
            string checkParaphrasesSecondParty = "Verify: 'Paraphases' option exists for the 2nd party";
            string checkNumberOfResultsForParaphrases = "Verify: Count of all items equals the number near 'Paraphrases' option";
            string checkMischaracterizationIdenticationFacetIsDisplayed = "Verify: Mischaracterization Identification facet is displayed";
            string checkMischaracterizationIdenticationFacetWorks = "Verify: Potential mischaracterization Filter works";
            string checkPotentialMischaracterizationCount = "Verify: Potential mischaracterization count is displyed correctly";

            var reportPage = QuickCheckUiManager.AssignDocumentsToPartiesAndGetReportInAdvantage(TestDocsPath, assignment);

            this.TestCaseVerify.IsTrue(
                checkLitigationDocumentAnalyzerTabDisplayed,
                BrowserPool.CurrentBrowser.Title.Equals(LitigationDocumentAnalyzerTabNameOfPage, StringComparison.InvariantCultureIgnoreCase),
                "Litigation Document Analyzer tab name is displayed incorrectly");

            var judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetFirstParty();

            reportPage.ReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.Paraphrases);

            var resultListQuotationsCount = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.Count;

            this.TestCaseVerify.AreEqual(
                checkParaphrasesFirstParty,
                resultListQuotationsCount,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.Paraphrases),
                "'Paraphases' option doesn't exist for the 1st party");

            judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetSecondParty();

            reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.Paraphrases);

            resultListQuotationsCount = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.Count;

            this.TestCaseVerify.AreEqual(
                checkParaphrasesSecondParty,
                resultListQuotationsCount,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.Paraphrases),
                "'Paraphases' option doesn't exist for the 2nd party");

            judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetFirstParty();
            reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.Paraphrases);

            resultListQuotationsCount = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.Count;

            this.TestCaseVerify.AreEqual(
                checkNumberOfResultsForParaphrases,
                resultListQuotationsCount,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.Paraphrases),
                "Count of paraphrases doesn't equal the number near 'Paraphrases' option");

            reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.ViewAll);

            this.TestCaseVerify.IsTrue(
                checkMischaracterizationIdenticationFacetIsDisplayed,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.MischaracterizationIdentificationFacet.IsDisplayed()
                && reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.MischaracterizationIdentificationFacet.IsCheckboxDisplayed(FacetItem),
                "Mischaracterization Identification facet is NOT displayed");

            this.TestCaseVerify.IsTrue(
                checkPotentialMischaracterizationCount,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.Where(item => item.PotentialMischaracterizationBox.IsDisplayed()).Count().Equals(reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.MischaracterizationIdentificationFacet.GetItemCountByName(FacetItem)),
                "Potential Mischaracterization count is not displayed correctly");

            reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.MischaracterizationIdentificationFacet.ApplyFacet<WestlawAdvantageQuickCheckRecommendationsPage>(true, FacetItem);
            var facetCount = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.MischaracterizationIdentificationFacet.GetItemCountByName(FacetItem);

            this.TestCaseVerify.IsTrue(
                checkMischaracterizationIdenticationFacetWorks,
                reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.All(item => item.PotentialMischaracterizationBox.IsDisplayed())
                && reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().ResultList.Count.Equals(facetCount)
                && reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().NarrowPane.QuotationType.GetNumberOfResultsForQuotationType(QuotationType.ViewAll).Equals(facetCount),
                "Potential mischaracterization filter doesn't work");
        }

        /// <summary>
        /// Task 2218634, 2212224, 2236536
        /// Upload documents by parties
        /// Verify: Delivery works as expected
        /// Verify: Argument and Counterargument data is not displayed in full report Download
        /// Verify: Paraphrases are displayed in full report and List of View 
        /// Verify: Citation issues are displayed in full report and List of View 
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(TeamMatzekCategory)]
        public void JudicialLanguageAnalysisTabDeliveryTest()
        {
            const string Document1 = "Insurance Litigation - Party 1 - Northern 1 - Motion for Summary Judgment.docx";
            const string Document2 = "Insurance Litigation - Party 2 - Roberts 1 -  Opposition to MSJ and Cross Motion.docx";

            var assignment = new Dictionary<string, JudicialParties>
            {
                { Document1, JudicialParties.FirstParty },
                { Document2, JudicialParties.SecondParty }
            };

            string checkNoQuickCheckDelivery = "Verify: No 'Quick Check' in the delivered document";
            string checkArgumentsAndCounterargumentCheckboxIsNotDisplayed = "Verify: Argument and Counterargument checkbox is not displayed in full report Download";
            string checkArgumentsAndCounterargumentDataIsNotDisplayed = "Verify: Argument and Counterargument data is not displayed in full report Download";
            //string checkParaphrasesInFullReportDelivery = "Verify: Paraphrases are in the delivered full report document";
            //string checkParaphrasesInListOfItemsDelivery = "Verify: Paraphrases are in the delivered List of items report document";
            string checkCitationsInFullReportDelivery = "Verify: Citation issues are in the delivered full report document";
            string checkCitationsInListOfItemsDelivery = "Verify: Citation issues are in the delivered List of items report document";

            var reportPage = QuickCheckUiManager.AssignDocumentsToPartiesAndGetReportInAdvantage(TestDocsPath, assignment);

            var judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetFirstParty();

            //Full report delivery
            var deliveryDialog = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.DocAnalyzerFullReport);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsFalse(
                checkArgumentsAndCounterargumentCheckboxIsNotDisplayed,
                deliveryDialog.LayoutAndLimitsTab.IsIncludeSectionOptionDisplayed(LayoutAndLimitsInclude.ArgumentsAndCounterArguments),
                "In Judicial full report delivery Arguments And CounterArguments option is displayed");

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<LanguageQuotationAnalysisTab>();

            var fileNameFullReport = $"Westlaw Advantage - Full judicial report from Party 1 v Party 2.pdf";

            FileUtil.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            var reportText = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport));

            this.TestCaseVerify.IsFalse(
                checkNoQuickCheckDelivery,
                reportText.Contains("Quick Check"),
                "'Quick Check' exists in the delivered document");

            this.TestCaseVerify.IsFalse(
                checkArgumentsAndCounterargumentDataIsNotDisplayed,
                reportText.Contains("Arguments and counterarguments"),
                "'Arguments and counterarguments' exists in the delivered document");

            //Bug 2217483: NGDA: Paraphrases are not displayed
            //this.TestCaseVerify.IsTrue(
            //    checkParaphrasesInFullReportDelivery,
            //    reportText.Contains("Paraphrases"),
            //    "'Paraphrases' are not present in the delivered document");

            this.TestCaseVerify.IsTrue(
                checkCitationsInFullReportDelivery,
                reportText.Contains("Citation issues") &&
                reportText.Contains("Party 1\r\nDocument 1: Insurance Litigation - Party 1 - Northern 1 - Motion for Summary Judgment.docx\r\nNo citation issues were found in this uploaded document"),
                "'Citation issues' are not present in the delivered document");

            judicialLanguageAnalysisTab = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().PartySwitcher.GetSecondParty();
            reportPage.ReportTabsPanel.GetLanguageAnalysisTab().NarrowPane.QuotationType.ClickQuotationTypeLink(QuotationType.ViewAll);

            //List of items delivery
            deliveryDialog = reportPage.ReportTabsPanel.GetJudicialLanguageAnalysisTabTab().Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<QuickCheckRecommendationsPage>();

            fileNameFullReport = $"Westlaw Advantage - List of 30 Quotations judicial documents from Party 1 v Party 2.pdf";
            FileUtil.WaitForFileDownload(this.FolderToSave, fileNameFullReport);

            reportText = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(this.FolderToSave, fileNameFullReport));

            //Bug 2217483: NGDA: Paraphrases are not displayed
            //this.TestCaseVerify.IsTrue(
            //    checkParaphrasesInListOfItemsDelivery,
            //    reportText.Contains("Paraphrases"),
            //    "'Paraphrases' are not present in the delivered document");

            this.TestCaseVerify.IsTrue(
                checkCitationsInListOfItemsDelivery,
                reportText.Contains("Citation issues") &&
                reportText.Contains("Party 2\r\nDocument 1: Insurance Litigation - Party 2 - Roberts 1 - Opposition to MSJ and Cross Motion.docx\r\nCitation from uploaded document Citation on Westlaw\r\n1. 20 S.W.3d 601 (Miss. 2009) No citation found"),
                "'Citation issues' are not present in the delivered document");
        }
    }
}
