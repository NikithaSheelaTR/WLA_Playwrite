namespace WestlawPrecision.Tests.SeparateAthensFeature.DocViewLimits
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading;

    [TestClass]
    public class CatchAllAndPracticalLawDailyLimitsTests : DocViewLimitsBaseTest
    {
        /// <summary>
        /// Test doc view limits for PL and catch all. User story: 2074601
        /// 1. Sign in WL Precision with routing:
        /// 2. IAC on: IAC-WL-PL-DOC-VIEW-LIMITS
        /// 3. FAC granted: DocViewDailyLimitPracticalLaw and DocViewDailyLimits
        /// 4. Set doc view limits: DocViewLimitPracticalLawDaily = 2, DocViewLimitCatchAllDaily = 2
        /// 5. Run global search in All State & Federal: tax fraud
        /// 6. View all Practical law results and try to view 3 documents
        /// 7. Check: Block message is displayed when PL limit reached
        /// 8. View all Cases results and try to view 3 documents
        /// 9. Check: Block message is displayed when catch all limit reached
        /// 10.Click to view the first viewed Case document
        /// 11.Check: Block message is not displayed when viewing viewed document after limit reached
        /// 12.Try to view a free PL glossary document: I2bbd23c1061a11e598db8b09b4f043e0
        /// 13.Check: Block message is not displayed when viewing free PL document after limit reached
        /// </summary>
        [TestMethod]
        [TestCategory(CurrentTestCategory)]
        [TestCategory("TransitionToSharat")]
        public void ApplyCatchAllAndPLLimitsTest()
        {
            const string SearchQuery = "tax fraud";
            const string FreePLDocGuid = "I2bbd23c1061a11e598db8b09b4f043e0";

            var homePage = this.GetHomePage<CommonSearchHomePage>();
            ResetDocViewLimitsUsage();
            homePage.Header.OpenJurisdictionDialog()
                .SelectJurisdictions(true, Jurisdiction.AllStates, Jurisdiction.AllFederal).ClickSelectButton<CommonSearchHomePage>();

            // View all Practical Law results and test the PL limit
            var searchResultPage = homePage.Header.EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery);
            searchResultPage = searchResultPage.NarrowTabPanel
                                   .SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                   .ContentType
                                   .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.PracticalLaw);

            var documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            searchResultPage = documentPage.FixedHeader.PLDocReturnToReportButton.Click<EdgeCommonSearchResultPage>();

            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(1);
            searchResultPage = documentPage.FixedHeader.PLDocReturnToReportButton.Click<EdgeCommonSearchResultPage>();

            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(2);
            this.TestCaseAssert.IsTrue(
                "Verify block message displayed when PL limit reached",
                documentPage.IsDisplayed(Dialogs.DocumentLimitReached),
                "Block message not displayed when PL limit reached");

            var limitReachedDialog = new DocumentLimitReachedDialog();
            searchResultPage = limitReachedDialog.GoBackToPreviousPageLink.Click<EdgeCommonSearchResultPage>();

            // View all Cases results and test the catch all limit
            searchResultPage = searchResultPage.NarrowTabPanel
                       .SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                       .ContentType
                       .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Cases);
            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            searchResultPage = documentPage.FixedHeader.ClickReturnToPreviousDocumentButton<EdgeCommonSearchResultPage>();

            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(1);
            searchResultPage = documentPage.FixedHeader.ClickReturnToPreviousDocumentButton<EdgeCommonSearchResultPage>();

            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(2);
            this.TestCaseAssert.IsTrue(
                "Verify block message displayed when catch all limit reached",
                documentPage.IsDisplayed(Dialogs.DocumentLimitReached),
                "Block message not displayed when catch all limit reached");

            // Test user can open viewed and free documents after limit reached
            limitReachedDialog = new DocumentLimitReachedDialog();
            searchResultPage = limitReachedDialog.GoBackToPreviousPageLink.Click<EdgeCommonSearchResultPage>();

            documentPage = searchResultPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            this.TestCaseVerify.IsFalse(
                "Verify block message not displayed viewing viewed doc after limit reached",
                documentPage.IsDisplayed(Dialogs.DocumentLimitReached),
                "Block message displayed viewing viewed doc after limit reached");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(FreePLDocGuid);
            this.TestCaseVerify.IsFalse(
                "Verify block message not displayed viewing free PL doc after limit reached",
                documentPage.IsDisplayed(Dialogs.DocumentLimitReached),
                "Block message displayed viewing free PL doc after limit reached");
        }

        private void ResetDocViewLimitsUsage()
        {
            //This resets the usage count to 0 if happens to be any
            const string WlnSite = "https://1.next.";
            const string Domain = ".westlaw.com/Document/ResetDocumentViewLimits";
            string environment = this.TestExecutionContext.TestEnvironment.Id.ToString().ToLower();

            string browsePageUrl = $"{WlnSite}{environment}{Domain}";
            BrowserPool.CurrentBrowser.GoToUrl(browsePageUrl);
            Thread.Sleep(2000);
            BrowserPool.CurrentBrowser.GoBack();
            Thread.Sleep(2000);
        }
    }
}

