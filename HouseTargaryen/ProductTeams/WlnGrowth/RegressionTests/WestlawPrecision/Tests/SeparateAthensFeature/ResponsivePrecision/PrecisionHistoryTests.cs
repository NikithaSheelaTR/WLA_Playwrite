namespace WestlawPrecision.Tests.SeparateAthensFeature.ResponsivePrecision
{
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Components.History;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.History;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrecisionHistoryTests : PrecisionResponsiveBaseTest
    {
        private const string FeatureTestCategory = "PrecisionHistory";
        private const string SearchQuery = "Variable rate electricity";

        /// <summary>
        /// Check tree controls work when browser width is less than 600px. (Bug 1722379)
        /// 1. Sign in WL Precision, run a search and click to view a document
        /// 2. View graphical history
        /// 3. Reduce browser width to 550px
        /// 4. Click user preference gear icon
        /// 5. Check: Emphasize high activity checkbox is displayed
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void TreeControlsTest()
        {
            const int BrowserWidth = 550;
            const int BrowserHeight = 1000;

            this.SwitchContentTypeAndRunSearch(ContentType.Cases, SearchQuery).ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            
            var gridItems = EdgeNavigationManager.Instance.GoToHistoryPage<EdgeCommonHistoryPage>().HistoryTabPanel
                                                 .SetActiveTab<GraphicalHistoryTabComponent>(HistoryTabs.GraphicalView).GraphicalGrid.GraphicalHistoryGridItems;

            this.SetBrowserSize(BrowserWidth, BrowserHeight);
            var dialog = gridItems.First().SettingsButton.Click<GraphicalHistoryAnchorSettingsDialog>();

            this.TestCaseVerify.IsTrue(
                "Verify user preference on graphical tree works with browser width = " + BrowserWidth,
                dialog.EmphasizeHighActivityCheckBox.Displayed,
                "User preference widget does not work");
        }
    }
}
