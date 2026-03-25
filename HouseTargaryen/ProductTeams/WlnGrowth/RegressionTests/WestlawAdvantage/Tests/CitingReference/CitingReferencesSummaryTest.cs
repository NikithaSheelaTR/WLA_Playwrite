namespace WestlawAdvantage.Tests.CitingReference
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CitingReferencesSummaryTest : WlaBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageFullRegression";
        private const string FeatureTestCategory = "CitingReferencesSummary";

        /// <summary>
        /// User Story 2164787,2180498
        /// Citing Reference doc page has Summarize button and clicking button generates summary
        /// 1. navigate to citing reference document
        /// 2. Check: Verify summarize citing reference button displayed
        /// 3. Click summarize citing reference button
        /// 4. Check: Verify citing reference summary is displayed.
        /// 5. Click on the link that is available in the summary 
        /// 6. Check: verify user is navigated to respective document page on a new tab
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CitingReferencesSummaryCommonTest()
        {
            const string DocGuid = "Ice9fb1e89c9611d993e6d35cc61aab4a";
            const string ExpectedSummaryHeading = "Citing references summary";
            const string DocumentlinkTab = "Document link Tab";

            string checkCitationReferencesButtonNotDisplayed = "Verify citing references button is not displayed for content type other than cases";
            string checkCitationReferencesButtonDisplayed = "Verify citing references button is displayed for content type cases only";
            string checkCitationReferencesHeadingDisplayed = "Verify citing references summary heading displayed";
            string checkSummaryDocumentLinkNavigation = "Verify document link in summary navigates to respective document in new tab";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            var citingReferencesPage = documentPage.RiTabs.ClickTab<EdgeCitingReferencesPage>(RiTab.CitingReferences);

            this.TestCaseVerify.IsFalse(
                checkCitationReferencesButtonNotDisplayed,
                citingReferencesPage.SummarizeCitingReferenceButton.Displayed,
                "Summarize citing references button displayed for all results");

            var citingReferencesCasesPage=citingReferencesPage.NewEdgeRiNarrowPane.SetActiveTab<RiContentTypesTabComponent>(RiNarrowTab.RiContentTypes) .RiContentType.ClickContentTypeLink<EdgeCitingReferencesPage>(ContentType.Cases);

            SafeMethodExecutor.WaitUntil(() => citingReferencesCasesPage.IsCitingReferencesGridElementDisplayed());

            this.TestCaseVerify.IsTrue(
                checkCitationReferencesButtonDisplayed,
                citingReferencesCasesPage.SummarizeCitingReferenceButton.Displayed,
                "Summarize citing references button not displayed for cases content type");

            citingReferencesCasesPage.SummarizeCitingReferenceButton.Click<EdgeCitingReferencesPage>();

            SafeMethodExecutor.WaitUntil(() => !citingReferencesCasesPage.CitingReferenceSummary.ProgressRingLabel.Displayed);

            var citingreferencesmaryHeading = citingReferencesPage.CitingReferenceSummary.SummaryHeadingLabel.Text;
            this.TestCaseVerify.IsTrue(
                checkCitationReferencesHeadingDisplayed,
                citingreferencesmaryHeading.Equals(ExpectedSummaryHeading),
                "citing references summary heading not displayed. Displayed: " + citingreferencesmaryHeading);

            var summaryContentKeyciteLinkLabel = citingReferencesCasesPage.CitingReferenceSummary.SummaryContentDocumentLinks.First().Text;

            citingReferencesCasesPage.CitingReferenceSummary.SummaryContentDocumentLinks.First().Click<EdgeCitingReferencesPage>();
            var documentTitleForBrowser = summaryContentKeyciteLinkLabel.Split(',')[0];

            BrowserPool.CurrentBrowser.CreateTab(DocumentlinkTab);
            BrowserPool.CurrentBrowser.ActivateTab(DocumentlinkTab);
            DriverExtensions.WaitForPageLoad();

            this.TestCaseVerify.IsTrue(
                checkSummaryDocumentLinkNavigation,
                BrowserPool.CurrentBrowser.Title.Contains(documentTitleForBrowser),
                "Citing reference keycite link not navigating to respective document in new tab");
        }

        /// <summary>
        /// Task 2230294, 2230231
        /// 1. Navigate to Citing references
        /// 2. Click on summarize button
        /// 3. Click summarize citing reference button
        /// 4. Click download and select pdf, word
        /// 5. Verify the citing reference summary is displayed in the downloaded document
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void CitingReferencesSummaryDeliveryTest()
        {
            const string DocGuid = "Ice9fb1e89c9611d993e6d35cc61aab4a";
            const string ExpectedSummaryHeading = "Citing references summary";

            string checkAiSummaryNotDisplayedInDocument = "Verify AI Summary option is displayed in Layout and Limits tab when Ai summary is not generated";
            string checkAiSummaryDisplayedInDocument = "AI Summary is not delivered in downloaded file";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            var citingReferencesPage = documentPage.RiTabs.ClickTab<EdgeCitingReferencesPage>(RiTab.CitingReferences);

            var citingReferencesCasesPage = citingReferencesPage.NewEdgeRiNarrowPane.SetActiveTab<RiContentTypesTabComponent>(RiNarrowTab.RiContentTypes).RiContentType.ClickContentTypeLink<EdgeCitingReferencesPage>(ContentType.Cases);

            var deliveryDialog = citingReferencesCasesPage.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            this.TestCaseVerify.IsFalse(
                checkAiSummaryNotDisplayedInDocument,
                 deliveryDialog.LayoutAndLimitsTab.AiSummaryCheckbox.Displayed,
                 "AI Summary option is displayed in Layout and Limits tab when Ai summary is not generated"
                );
            deliveryDialog.ClickCancel<EdgeCitingReferencesPage>();

            citingReferencesCasesPage.SummarizeCitingReferenceButton.Click<EdgeCitingReferencesPage>();

            SafeMethodExecutor.WaitUntil(() => !citingReferencesCasesPage.CitingReferenceSummary.ProgressRingLabel.Displayed);

            deliveryDialog = citingReferencesCasesPage.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.Documents);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);
            deliveryDialog.TheBasicsTab.DeliveryAsDropdown.SelectOption("Single merged file");
            deliveryDialog.LayoutAndLimitsTab.SetIncludeSectionOption(LayoutAndLimitsInclude.AISummary);

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<AiAssistedResearchPage>();
            var fileName = "Westlaw Advantage - 20 full text Citing References for Motor Vehicle Manufacturers Association of th.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));
            var textWithoutWhitespaces = text.Replace(" ", string.Empty).Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkAiSummaryDisplayedInDocument,
                textWithoutWhitespaces.Contains(ExpectedSummaryHeading.Replace(" ", string.Empty)),
                "AI Summary is not delivered in downloaded file"
                );
        }
    }
}