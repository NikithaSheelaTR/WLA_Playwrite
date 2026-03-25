namespace WestlawPrecision.Tests.Aalp
{
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Core.Utils.Extensions;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.Document;
    using Framework.Core.Utils.Execution;
    using Framework.Common.UI.Products.Shared.Dialogs.Dockets;
    using System.Threading;
    using Framework.Common.UI.Products.Shared.Pages.Browse;
    using Framework.Common.UI.Products.WestlawEdge.Components.BrowseWidget;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdgePremium.Enums;
    using System.Linq;

    /// <summary>
    /// Docket Analyzer tests
    /// </summary>
    [TestClass]
    public class AalpDocketAnalyzerTests : AalpBaseTest
    {
        protected const string FeatureTestCategory = "DocketAnalyzer";

        /// <summary>
        /// Test Docket doc page has Summarize button and clicking button generates summary with disclaimer 
        /// (Stories:2110313 2117063 2110312 Test Cases:2119580 2120168 2120156, 2149470).
        /// 1. Sign in WL Precision with Docket Analyzer access
        /// 2. Access docket document: I7E38FC2C9AC711E3A341EA44E5E1F25F
        /// 3. Check: Verify Summarize docket button displayed on docket page
        /// 4. Click Summarize docket button
        /// 5. Check: Verify disclaimer displayed on summary
        /// 6. Check: Verify summary content generated 
        /// 7. Check: Verify bottom message displayed on summary
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void AiDocketAnalyzerSmokeTest()
        {
            const string DocGuid = "I7E38FC2C9AC711E3A341EA44E5E1F25F";
            const string ExpectedDisclaimer = "The below response is AI-generated and may contain errors. It should be verified for accuracy.";
            const string ExpectedContent = "Jurisdiction and Governing Law";
            
            var docketsDocumentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeDocketsDocumentPage>(DocGuid);
            PrintSessionIdFromPageSource();
            this.TestCaseVerify.IsTrue(
                "Verify Summarize docket button displayed on docket page",
                docketsDocumentPage.Toolbar.SummarizeDocketButton.Displayed,
                "Summarize docket button not displayed on docket page");

            docketsDocumentPage = docketsDocumentPage.Toolbar.SummarizeDocketButton.Click<EdgeDocketsDocumentPage>();
            
            Thread.Sleep(5000);
            
            SafeMethodExecutor.WaitUntil(() => !docketsDocumentPage.DocketSummary.ProgressRingLabel.Displayed, timeoutFromSec: 300);
            
            var displayedDisclaimer = docketsDocumentPage.DocketSummary.DisclaimerLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify disclaimer displayed on summary",
                displayedDisclaimer.Contains(ExpectedDisclaimer),
                "Expected disclaimer not displayed on summary. Displayed: " + displayedDisclaimer); 

            var displayedContent = docketsDocumentPage.DocketSummary.SummaryContentLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify summary content generated",
                displayedContent.Contains(ExpectedContent),
                "Expected summary content not generated. Displayed: " + displayedContent);
        }

        /// <summary>
        /// Test Docket doc page update button 
        /// (Stories:2124670 Test Cases:2133744).
        /// 1. Sign in WL Precision with Docket Analyzer access
        /// 2. Access docket document: I38B03571ACAA11E89A6EFC60AF1B5D9C
        /// 3. Check: Verify UPDATE button displayed
        /// 4. Click on the UPDATE button
        /// 5. Wait till the Update dialog closes, and UPDATE button disappears in the docket landing page
        /// 6. Check: Verify Summarize docket button displayed on docket page
        /// 7. Click Summarize docket button
        /// 8. Check: Verify summary content generated
        /// 9.Click on Expand/collapse button
        /// 10.Check: Verify summary box is collapsed
        /// 11.Click on Expand/collapse button
        /// 12.Check: Verify summary box is Expanded
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        public void AiDocketAnalyzerDocketUpdateTest()
        {
            const string DocGuid = "I3E20983FBCA911DB9F1FBB4812379D8B";
            const string ExpectedContent1 = "Jurisdiction and Governing Law";
            const string ExpectedContent2 = "Case Type";

            var docketsDocumentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeDocketsDocumentPage>(DocGuid);
            PrintSessionIdFromPageSource();
            this.TestCaseVerify.IsTrue(
                "Verify docket update button displayed on docket page",
                docketsDocumentPage.DocketUpdateButton.Displayed,
                "docket update button not displayed on docket page");

            var docketUpdateDialog= docketsDocumentPage.DocketUpdateButton.Click<SingleDocketUpdateRequestsDialog>();
            SafeMethodExecutor.WaitUntil(() => !docketUpdateDialog.UpdateProcessingMessageLabel.Displayed, timeoutFromSec: 300);
            this.TestCaseVerify.IsTrue(
                "Verify Summarize docket button displayed on docket page after update",
                docketsDocumentPage.Toolbar.SummarizeDocketButton.Displayed,
                "Summarize docket button not displayed on docket page after update");

            docketsDocumentPage = docketsDocumentPage.Toolbar.SummarizeDocketButton.Click<EdgeDocketsDocumentPage>();
            SafeMethodExecutor.WaitUntil(() => !docketsDocumentPage.DocketSummary.ProgressRingLabel.Displayed, timeoutFromSec: 300);
                        
            var displayedContent = docketsDocumentPage.DocketSummary.SummaryContentLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify summary content generated after clicking on summarize button after docket update",
                displayedContent.Contains(ExpectedContent1) & displayedContent.Contains(ExpectedContent2),
                "Expected summary content not generated after clicking on summarize button after docket update. Displayed: " + displayedContent);

            bool isSummaryExpanded = docketsDocumentPage.DocketSummary.ClickExpandCollapseButton();
            this.TestCaseVerify.IsFalse("Summary Content should be collapsed and not visible",
                isSummaryExpanded,
                "Summary Content is not collapsed and visible");

            isSummaryExpanded = docketsDocumentPage.DocketSummary.ClickExpandCollapseButton();
            this.TestCaseVerify.IsTrue(
                "Summary content should be expanded and visible",
                 isSummaryExpanded,
                "Expected summary content is not expanded and not visible");
        }

        /// <summary>
        /// Test Docket gateway has Summarize button and clicking on button generates summary
        /// (Stories:2124672 Task: 2134628, 2133188).
        /// 1. Sign in WL Precision with Docket gateway access
        /// 2. Navigate to Content ->Dockets page
        /// 3. Scroll to bottom and click on any juridiction link under "Excluded" Search Separately" section.
        /// 4. enter the required details (Name/Docket/BusinessName) and click on search.
        /// 5. Wait untill results are displayed
        /// 6. select first docket item in the result
        /// 7. Check: Verify Summarize docket button displayed on docket gateway page
        /// 8. Click Summarize docket button
        /// 9. Check: Verify clicking Summarize button works with no error
        /// 10. Check: Verify summary content generated 
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(TeamDahlCategory)]
        [TestProperty("DocketType", "Gateway")]
        public void AiDocketGatewaySummarizeTest()
        {
            const string ExpectedContent = "Jurisdiction and Governing Law";            
            const string Jurisdiction = "Cuyahoga County (OH)";

            var content = this.GetHomePage<PrecisionHomePage>().BrowseTabPanel.SetActiveTab<ContentTypesTabPanel>(PrecisionBrowseTab.ContentTypes);
            var docketPage = content.ClickBrowseCategory<DocketsCategoryPage>(ContentTypeEdge.Dockets);
            
            var searchDocketPage = docketPage.SearchSeparatelylinks.Where(x => x.Text == Jurisdiction).Last().Click<AdvancedSearchDocketsPage>();

            searchDocketPage.SelectBussinessNameRadioButton.Select();
            searchDocketPage.EnterBussinessNameTextBox.SendKeys("Purdue");

            searchDocketPage.SearchButton.Click<EdgeDocketsDocumentPage>();
            SafeMethodExecutor.WaitUntil(() => !searchDocketPage.ProgressRingLabel.Displayed);
            var docketsDocumentPage = searchDocketPage.ResultItemsTitleLinks.First().Click<EdgeDocketsDocumentPage>();

            this.TestCaseVerify.IsTrue(
                "Verify summarize docket button displayed on docket gateway page",
                docketsDocumentPage.Toolbar.SummarizeDocketButton.Displayed,
                "Summarize docket button not displayed on docket gateway page");

            docketsDocumentPage = docketsDocumentPage.Toolbar.SummarizeDocketButton.Click<EdgeDocketsDocumentPage>();
            Thread.Sleep(1000);
            SafeMethodExecutor.WaitUntil(() => !docketsDocumentPage.DocketSummary.ProgressRingLabel.Displayed, timeoutFromSec: 300);

            var displayedError = docketsDocumentPage.DocketSummary.ErrorLabel.Text;

            this.TestCaseVerify.IsFalse(
                "Verify clicking Summarize button works with no error for docket gateway",
                docketsDocumentPage.DocketSummary.ErrorLabel.Displayed,
                "Clicking summarize button does not work for docket gateway. Error: " + docketsDocumentPage.DocketSummary.ErrorLabel.Text);

            var displayedContent = docketsDocumentPage.DocketSummary.SummaryContentLabel.Text;

            this.TestCaseVerify.IsTrue(
                "Verify summary content generated for docket gateway",
                displayedContent.Contains(ExpectedContent),
                "Expected summary content not generated for docket gateway. Displayed: " + displayedContent);
        }

        protected override void InitializeRoutingPageSettings()
        {           
            if ((this.TestContext.Properties["DocketType"] != null) &&
            (this.TestContext.Properties["DocketType"].Equals("Gateway")))
            {
                this.Settings.AppendValues(
                EnvironmentConstants.GatewayLiveExternal,
                SettingUpdateOption.Append,
                true);
            }
        }
    }
}
