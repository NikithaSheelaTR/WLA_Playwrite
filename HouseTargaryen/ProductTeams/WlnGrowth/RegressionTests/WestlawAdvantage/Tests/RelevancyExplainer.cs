namespace WestlawAdvantage.Tests
{
    using System;
    using System.IO;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.RI;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.TaxnetPro.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages.AALP;
    using Framework.Common.UI.Products.WestLawNext.Enums.Delivery;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages.RelatedInformation.CitingReferences;
    using Framework.Common.UI.Raw.EnhancementTests.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Execution;
    using Framework.Core.Utils.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using WestlawPrecision.Tests.Aalp;

    [TestClass]
    public class RelevancyExplainer : WlaBaseTest
    {
        private new const string CurrentTestCategory = "WestlawAdvantageFullRegression";
        private const string FeatureTestCategory = "RelevancyExplainer";

        /// <summary>
        /// Relevancy Explainer Delivery Test
        /// 1. Navigate to Cases search results page
        /// 2. Click on Relevancy Explainer button
        /// 3. Verify AI Relevancy Overview option is not displayed in Layout and Limits tab when relevancy explainer is not generated
        /// 4. Generate relevancy explainer summary
        /// 5. Click download and select list of items with AI Relevancy Overview
        /// 6. Verify the relevancy explainer summary is displayed in the downloaded document
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        public void RelevancyExplainerDeliveryTest()
        {
            const string SearchQuery = "motor";
            const string ExpectedRelevancyHeading = "Relevancy";

            string checkAiRelevancyOverviewNotDisplayedInDocument = "Verify AI Relevancy Overview option is not displayed in Layout and Limits tab when relevancy explainer is not generated";
            string checkAiRelevancyOverviewDisplayedInDocument = "AI Relevancy Overview is not delivered in downloaded file";

            var homePage = this.GetHomePage<EdgeHomePage>();
            var resultPage = homePage.Header.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery);

            SafeMethodExecutor.WaitUntil(() => resultPage.GetAllLinks().Count > 0);

            // Open delivery dialog before generating relevancy explainer
            var deliveryDialog = resultPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);

            this.TestCaseVerify.IsFalse(
                checkAiRelevancyOverviewNotDisplayedInDocument,
                deliveryDialog.LayoutAndLimitsTab.AiRelevancyOverviewCheckbox.Displayed,
                "AI Relevancy Overview option is displayed in Layout and Limits tab when relevancy explainer is not generated"
            );
            deliveryDialog.ClickCancel<EdgeCommonSearchResultPage>();

            // Click on Relevancy Explainer button to generate summary
            resultPage.RelevancyExplainerButtons().First().Click<EdgeCommonSearchResultPage>();

            SafeMethodExecutor.WaitUntil(() => resultPage.RelevancyExplainerSummary.Displayed);

            // Open delivery dialog after generating relevancy explainer
            deliveryDialog = resultPage.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            deliveryDialog.TheBasicsTab.WhatToDeliver.SelectOption(WhatToDeliver.ListOfItems);
            deliveryDialog.TheBasicsTab.FormatDropdown.SelectOption(DeliveryFormat.Pdf);

            this.TestCaseVerify.IsTrue(
                checkAiRelevancyOverviewNotDisplayedInDocument,
                deliveryDialog.LayoutAndLimitsTab.AiRelevancyOverviewCheckbox.Displayed,
                "AI Relevancy Overview option is not displayed in Layout and Limits tab when relevancy explainer is not generated"
            );

            deliveryDialog.LayoutAndLimitsTab.AiRelevancyOverviewCheckbox.Set(true);

            deliveryDialog.ClickDownloadButton<ReadyForDeliveryDialog>().ClickDownloadButton<EdgeCommonSearchResultPage>();
            
            var fileName = $"Westlaw Advantage - List of 20 results for {SearchQuery}.pdf";
            FileUtil.WaitForFileDownload(FolderToSave, fileName);
            var text = PdfTextExtractor.ExtractTextFromPdf(Path.Combine(FolderToSave, fileName));
            var textWithoutWhitespaces = text.Replace(" ", string.Empty).Replace("\r\n", string.Empty);

            this.TestCaseVerify.IsTrue(
                checkAiRelevancyOverviewDisplayedInDocument,
                textWithoutWhitespaces.Contains(ExpectedRelevancyHeading.Replace(" ", string.Empty)),
                "AI Relevancy Overview is not delivered in downloaded file"
            );
        }
    }
}
