namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using System.Linq;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Core.Utils.Execution;

    [TestClass]
    public class OutlineBuilderDragAndDropFunctionalTests : OutlineBuilderBaseTest
    {
        /// <summary>
        /// Test dragging and dropping snippet to new outline
        /// 1. Sign in Westlaw Precision 
        /// 2. View a Case document
        /// 3. Click Outlines on right panel 
        /// 4. Click Create outline
        /// 5. Highlight first paragraph of opinion section of document
        /// 6. Drag and drop highlighted text in outline
        /// 7. Check: Outline contains dropped text
        /// 8. Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void DragSnippetToNewOutlineTest()
        {
            const string OutlineTitle = "DragSnippetToNewOutline";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(DocGuid);
            document.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            // Open Outline Right Panel
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<PrecisionDocumentPageWithHeadnotes>();

            // Create new Outline
            CreateNewOutline(document, OutlineTitle);

            // Highlight Snippet
            string expectedParagraphText = document.GetDocumentSectionText(DocumentSection.OpinionBody).Split('\r', '\n').FirstOrDefault();
            document.SelectDocumentSection(DocumentSection.OpinionBody);

            // Drag Snippet to Right Panel Outline
            document.DragAndDropSelectedSnippetToOutlineBuilder();

            // Verify new outline has snippet
            string foundParagraphText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel.OutlineSnippetNodeLabel;

            this.TestCaseVerify.IsTrue("Outline contains snippet with such text",
                foundParagraphText.Contains(expectedParagraphText),
                "No such text in the Outline");
        }

        /// <summary>
        /// Test drag and drop function not available when selected text exceeds 4000 symbols
        /// 1. Sign in Westlaw Precision 
        /// 2. View a Case document
        /// 3. Click Outlines on right panel 
        /// 4. Click Create outline
        /// 5. Highlight entire document (expect to be over 4000 symbols)
        /// 6. Check: Drag and drop handle is not displayed
        /// 7. Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void UnableToDragAndDropMoreThan4000SymbolSnippetTest()
        {
            const string OutlineTitle = "Test DnD Outline";
            const string DocGuid = "I67ac4deba3aa11e3b58f910794d4f75e";

            // Select First Document
            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(DocGuid);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            // Open Outline Right Panel
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<PrecisionDocumentPageWithHeadnotes>();

            // Create new Outline
            CreateNewOutline(document, OutlineTitle);

            // Highlight Snippet
            document.SelectEntireDocumentSection(DocumentSection.OpinionBody);

            // Verify unable to drag snippet to outline
            this.TestCaseVerify.IsFalse("Should not be able to drag text to outline",
                document.IsDragAndDropHandleVisible(),
                "Drag Handle appeared for large snippet");
        }

        /// <summary>
        /// Test drag and drop citation with pinpoint location story: 1668878
        /// 1. Sign in WLP and run unique search: 490 F.3d 143 
        /// 2. Open Outline right panel and create outline: Test Pinpoint Outline
        /// 3. Scroll to citation link: Hill v. City of New York, 45 F.3d 653, 657 (2d Cir.1995) 
        /// 4. Drag and drop citation link to Outline
        /// 5. Click citation link on Outline to go to target document
        /// 6. Check: Pinpoint green arrow displayed on document
        /// 7. Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void DragAndDropPinpointTest()
        {
            const string UniqueFindQuery = "490 F.3d 143";
            const string OutlineTitle = "Test Pinpoint Outline";
            const string CitationLinkText = "45 F.3d 653, 657 (2d Cir.1995)"; //Full link is: Hill v. City of New York, 45 F.3d 653, 657 (2d Cir.1995)

            // Go to Document by running a unique search
            var document = GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<PrecisionDocumentPageWithHeadnotes>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            // Open Outline Right Panel
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<PrecisionDocumentPageWithHeadnotes>();
            CreateNewOutline(document, OutlineTitle);

            // Drag citation link to Outline on right panel
            document.DragAndDropCitationLinkToOutlineBuilder(CitationLinkText);

            // Click citation link on Outline to open new document page
            var targetDoc = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.
                                     GetOutlineCitationLink(CitationLinkText).Click<PrecisionDocumentPageWithHeadnotes>();

            // Verify green pinpoint arrow displayed
            this.TestCaseVerify.IsTrue("Green pinpoint arrow displayed on target document",
                targetDoc.IsBestPortionArrowDisplayed(),
                "Pinpoint arrow is not displayed");
        }

        /// <summary>
        /// Test pinpoint citation displayed in outline snippet. Production bug: 1841655
        /// 1. Sign in WLP and run unique search: 634 F.Supp.2d 1108 
        /// 2. Open Outline right panel and create outline: Test Pinpoint Outline
        /// 3. Scroll to Opinion section and highlight: In re Republic of Philippines 
        /// 4. Add to Outline and Save
        /// 5. Check: Pinpoint citation 1113 is displayed in the outline snippet
        /// 6. Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void OutlineSnippetPinpointCitationTest()
        {
            const string UniqueFindQuery = "634 F.Supp.2d 1108";
            const string OutlineTitle = "Test Pinpoint Outline";
            const string OpinionSectionId = "I42acd3d4c2d011eabea4f0dc9fb69570_opinionHeader_0";
            const string HighlightText = "In re Republic of Philippines";
            const string PinpointCitationText = "1113";

            // Go to Document by running a unique search
            var document = GetHomePage<EdgeHomePage>().Header.EnterSearchQueryAndClickSearch<EdgeCommonDocumentPage>(UniqueFindQuery);
            document.RightPanel.Toggle.ToggleState<PrecisionDocumentPageWithHeadnotes>(false);

            // Open Outline Right Panel
            document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();
            CreateNewOutline(document, OutlineTitle);

            document.ScrollToSection(OpinionSectionId);
            HighlightMenuDialog highlightMenu = document.SelectText(HighlightText);
            var outlinesModalPanel = highlightMenu.AddToOutlinesButton.Click<AddToOutlineDialog>();
            document = outlinesModalPanel.SaveOutlineButton.Click<PrecisionDocumentPageWithHeadnotes>();

            //The selection was successfully added to Test Pinpoint Outline 
            SafeMethodExecutor.WaitUntil(() => outlinesModalPanel.InfoBox.Text.Contains("successfully added"));
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(true);
            string outlineText = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel
                                         .OutlineSnippetNodeLabel;

            // Verify pinpoint citation is added to the outline snippet
            this.TestCaseVerify.IsTrue("Pinpoint citation is displayed in the outline snippet",
                outlineText.Contains(PinpointCitationText),
                "Pinpoint citation is not displayed. Outline text: " + outlineText);
        }
    }
}
