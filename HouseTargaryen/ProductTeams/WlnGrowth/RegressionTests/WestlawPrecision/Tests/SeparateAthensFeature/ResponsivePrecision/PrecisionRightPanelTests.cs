namespace WestlawPrecision.Tests.SeparateAthensFeature.ResponsivePrecision
{
    using Framework.Common.UI.Products.WestlawEdgePremium.Dialogs;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Core.Utils.Execution;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PrecisionRightPanelTests : PrecisionResponsiveBaseTest
    {
        private const string FeatureTestCategory = "PrecisionRightPanel";

        /// <summary>
        /// Responsive Design - Panel selection kebab opens slide in menu (1921852)
        /// 1. Sign in WL Precision with responsive IACs 
        /// 2. Open case document: 13 F3d 888
        /// 3. Reduce browser width to 550px
        /// 4. Click Notes panel button
        /// 5. Check: Notes panel opens
        /// 6. Close notes panel and click Outlines panel button
        /// 7. Check: Outlines panel opens
        /// 8. Close Outlines panel and click Quick Check panel button
        /// 9. Check: Quick Check panel opens
        /// 10. Open 3-dot menu and Choose Outlines panel
        /// 11. Check: Outlines panel opens
        /// 12. Open 3-dot menu and Choose Notes panel
        /// 13. Check: Notes panel opens
        /// 14. Open 3-dot menu and Choose Quick Check panel
        /// 15. Check: Quick Check panel opens
        /// 16. Close Quick Check panel
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void RightPanel3DotMenuTest()
        {
            const string Query = "13 F3d 888";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;
            var rightPanel = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(Query, true).RightPanel;
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            rightPanel.NotesPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.NotesPanel.IsDisplayed());
            this.TestCaseVerify.IsTrue(
                "Notes panel opens on page with browser width = " + BrowserWidth,
                rightPanel.NotesPanel.IsDisplayed(),
                "Notes panel does not open on page");
            rightPanel.NotesPanel.CloseButton.Click();

            rightPanel.OutlinesPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.IsDisplayed());
            this.TestCaseVerify.IsTrue(
                "Outlines panel opens on page with browser width = " + BrowserWidth,
                rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.IsDisplayed()
                && rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlinePanelHeaderLabel.Displayed,
                "Outlines panel does not open on page");
            rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlineResponsiveCloseButton.Click();

            rightPanel.QuickCheckPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.QuickCheckPanel.IsDisplayed());
            this.TestCaseVerify.IsTrue(
                 "Quick Check panel opens on page with browser width = " + BrowserWidth,
                 rightPanel.QuickCheckPanel.IsDisplayed(),
                 "Quick Check does not open on page");
            var toolsMenu = rightPanel.QuickCheckPanel.HeadingToolsMenuButton.Click<PrecisionRightPanelToolsDialog>();

            toolsMenu.OutlinesPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlineResponsiveCloseButton.Displayed);
            this.TestCaseVerify.IsTrue(
                "Outlines panel opens on Tools manu with browser width = " + BrowserWidth,
                rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.IsDisplayed()
                && rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlinePanelHeaderLabel.Displayed,
                "Outlines does not open on tools menu");
            rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlineResponsiveHeadingToolsMenuButton.Click();

            toolsMenu.NotesPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.NotesPanel.IsDisplayed());
            this.TestCaseVerify.IsTrue(
                "Notespanel opens on Tools menu with browser width = " + BrowserWidth,
                 rightPanel.NotesPanel.IsDisplayed(),
                 "Notes does not open on Tools menu");
            rightPanel.NotesPanel.HeadingToolsMenuButton.Click();
            toolsMenu.QuickCheckPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.QuickCheckPanel.IsDisplayed());
            this.TestCaseVerify.IsTrue(
                "Quick Check panel opens on Tools menu with browser width = " + BrowserWidth,
                 rightPanel.QuickCheckPanel.IsDisplayed(),
                 "Quick Check does not open on tools menu");
            rightPanel.QuickCheckPanel.CloseButton.Click();
        }

        /// <summary>
        /// Responsive Design - Outline toolbar 'Full page' item exists in panel slide menu (1795882)
        /// 1. Sign in WL Precision with responsive IACs 
        /// 2. Open case document: 327 F.Supp.3d 606
        /// 3. Reduce browser width to 1024px
        /// 4. Click Outlines in 3-feature panel's open widget
        /// 5. Delete all outlines if any
        /// 6. Click 3-dot menu
        /// 7. Check: Outline toolbar 'Full page' item option is shown in slide menu
        /// 8. Click Full page
        /// 9. Check: Outline builder full page opened
        /// 10. Click Return to document
        /// 11. Add an outline
        /// 12. Click 3-dot menu
        /// 13. Check: Outline toolbar 'Full page' item option is shown in slide menu
        /// 14. Click Full page
        /// 15. Check: Outline builder full page opened
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void OutlineToolbarFullPageItemTest()
        {
            const string Query = "327 F.Supp.3d 606";
            const string OutlineTitle = "Test Outline";
            const int BrowserWidth = 1024;
            const int BrowserHeight = 1000;
            var document = this.GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(Query, true);
            var rightPanel = document.RightPanel;
            this.SetBrowserSize(BrowserWidth, BrowserHeight);

            rightPanel.OutlinesPanelButton.Click();
            SafeMethodExecutor.WaitUntil(() => rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.IsDisplayed());

            document = this.DeleteAllOutlines(document);
            var toolsMenu = rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlineResponsiveHeadingToolsMenuButton.Click<PrecisionRightPanelToolsDialog>();
            this.TestCaseVerify.IsTrue(
                "Outlines Full page item exists in the right panel without outlines with browser width = " + BrowserWidth,
                toolsMenu.FullPageButton.Displayed,
                "Outlines Full page item does not open within the right panel without outlines");

            var fullPagePanel = toolsMenu.FullPageButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;
            this.TestCaseVerify.IsTrue(
                "Outline Builder full page component is opened",
                fullPagePanel.IsDisplayed(),
                "Outline Builder full page panel isn't shown");

            rightPanel = fullPagePanel.ReturnToDocumentButton.Click<EdgeCommonDocumentPage>().RightPanel;
            rightPanel.OutlinesPanelButton.Click();
            document = this.AddOutline(document, OutlineTitle);

            toolsMenu = rightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.OutlineResponsiveHeadingToolsMenuButton.Click<PrecisionRightPanelToolsDialog>();
            this.TestCaseVerify.IsTrue(
                "Outlines Full page item exists in the right panel with outlines with browser width = " + BrowserWidth,
                toolsMenu.FullPageButton.Displayed,
                "Outlines Full page item does not open within the right panel with outlines");

            fullPagePanel = toolsMenu.FullPageButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;
            this.TestCaseVerify.IsTrue(
                "Outline Builder full page component is opened",
                fullPagePanel.IsDisplayed(),
                "Outline Builder full page panel isn't shown");

            fullPagePanel.ReturnToDocumentButton.Click<EdgeCommonDocumentPage>();
            rightPanel.OutlinesPanelButton.Click();
            this.DeleteAllOutlines(document);
        }
    }
}
