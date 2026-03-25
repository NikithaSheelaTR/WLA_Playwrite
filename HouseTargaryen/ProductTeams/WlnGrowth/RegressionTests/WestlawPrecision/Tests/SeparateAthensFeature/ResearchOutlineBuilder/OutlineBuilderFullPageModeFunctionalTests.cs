namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Enums.NarrowPanel;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Managers;
    using System.Collections.Generic;
    using OpenQA.Selenium;
    using System.Linq;
    using System;
    using System.Threading;

    [TestClass]
    public class OutlineBuilderFullPageModeFunctionalTests : OutlineBuilderBaseTest
    {
        /// <summary>
        /// Test that verifies we can create new outline in Full Page mode
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Cases results and click to view first document
        /// 3. Click Outlines on right panel and click Full page
        /// 4. Check: Outline Builder full page component is opened
        /// 5. Click left panel collapse button
        /// 6. Check: left panel is collapsed
        /// 7. Click left panel expand button
        /// 8. Check: left panel is expanded
        /// 9. Click Create outline with a new name
        /// 10.Check: outline renamed to new name
        /// 11.Click Return to document link
        /// 12.Check: Full page panel is closed and Right panel is opened
        /// 13.Go back to zero state by deleting outline
        /// </summary>  
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateAndManipulateOutlineFullPageModeTest()
        {
            const string SearchQuery = "law";
            const string OutlineTitle = "New Outline";
            var searchResultsPage = this.GetHomePage<EdgeHomePage>().Header
                                        .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                                        .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                        .ContentType
                                        .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Cases);

            var document = searchResultsPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>()
                                        .RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.FullPageModeButton
                                        .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                        .OutlineBuilderFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Outline Builder full page component is opened",
                fullPagePanel.IsDisplayed(),
                "Outline Builder full page panel isn't shown");

            document = fullPagePanel.CollapseButton.Click<EdgeCommonDocumentPage>();
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.IsFalse(
                "Outline Builder full page left panel is hidden",
                fullPagePanel.IsDisplayed(),
                "Outline Builder full page left panel is still displayed");

            document = fullPagePanel.CollapseButton.Click<EdgeCommonDocumentPage>();
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Outline Builder full page left panel is opened",
                fullPagePanel.IsDisplayed(),
                "Outline Builder full page left panel isn't shown");

            document = CreateNewOutline(document, OutlineTitle);
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Outline title has been changed correctly",
                fullPagePanel.CurrentOutlineTitleLabel.Text.Contains(OutlineTitle),
                "Outline title is incorrect");

            document = fullPagePanel.ReturnToDocumentButton.Click<EdgeCommonDocumentPage>();
            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue("Full page panel is closed and Right panel is opened",
                outlinesRightPanel.IsDisplayed(),
                "Full page component still displayed");
        }

        /// <summary>
        /// Test that verifies we can create several headings in Full Page mode
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Regulations results and click to view second document
        /// 3. Click Outlines on right panel and click Full page
        /// 4. Click to add outline and add heading1: Premises liability
        /// 5. Check: outline contains heading1
        /// 6. Edit heading1 name to heading2: Notice of defect
        /// 7. Check: outline contains heading2
        /// 8. Edit heading2 name to be empty and get outline page again
        /// 9. Check: outline contains heading2 after editing with empty name 
        /// 10.Edit heading2 and delete
        /// 11.Check: Confirm header deletion popup is shown
        /// 12.Click Cancel on confirm widget
        /// 13.Check: outline contains heading2 after canceling delete
        /// 14.Edit heading2 and delete
        /// 15.Click Delete on confirm widget
        /// 16.Check: outline does not contain heading2 after deleting
        /// 17.Go back to zero state by deleting outline
        /// </summary>      
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateSeveralHeadingsInFullPageModeTest()
        {
            const string SearchQuery = "law";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            var searchResultsPage = this.GetHomePage<EdgeHomePage>().Header
                                        .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                                        .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                        .ContentType
                                        .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Regulations);

            var document = searchResultsPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(1);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var fullPage = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>();
            fullPage.CheckAndClosePendoDialog();
            var fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.FullPageModeButton
                                        .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                        .OutlineBuilderFullPagePanel;

            document = fullPagePanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();
            var fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            document = CreateHeadingInOutline(document, "1", HeadingString1);
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with such text",
                fullHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString1)),
                "No such entity on the page");

            fullHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString1)).EditButton
                            .Click<EdgeCommonDocumentPage>();
            document = EditHeading(document, HeadingString2);
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with such text",
                fullHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "No such entity on the page");

            fullHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString2)).EditButton
                            .Click<EdgeCommonDocumentPage>();
            document = EditHeading(document, String.Empty);
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Outline contains heading2 after editing with empty name",
                fullHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "No such entity on the page");

            document = fullHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString2)).EditButton
                                       .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                       .OutlineInternalFullPagePanel.DeleteHeadingButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Confirm header deletion popup is shown",
                fullHeadingPanel.ConfirmDeleteHeadingButton.Displayed,
                "No confirmation popup on the page");

            document = fullHeadingPanel.CancelDeleteHeadingButton.Click<EdgeCommonDocumentPage>().RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.CancelHeadingButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with such text",
                fullHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "No such entity on the page");

            document = fullHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString2)).EditButton
                                       .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                       .OutlineInternalFullPagePanel.DeleteHeadingButton.Click<EdgeCommonDocumentPage>()
                                       .RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel
                                       .ConfirmDeleteHeadingButton.Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsFalse(
                "Heading with such text was deleted",
                fullHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "Entity is still there on the page");
        }

        /// <summary>
        /// Test that verifies we can create outline and add heading level 1,2,3 and notes in Full Page mode
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Trial Court Orders results and click to view third document
        /// 3. Click Outlines on right panel and click Full page
        /// 4. Click to add outline 
        /// 5. Check: Heading2 and Heading3 options are shown but disabled and Outline Note button is enabled
        /// 6. Edit heading1 name to: Premises liability
        /// 7. Check: Heading3 options is shown but disabled
        /// 8. Edit heading2 name to: Notice of defect
        /// 9. Check: All options are shown and enabled after updating heading2 
        /// 10.Edit heading3 name to: No breach of duty
        /// 11.Check: All options are shown and enabled after updating heading3
        /// 12.Click to add another heading1 name: Expenses and trouble
        /// 13.Check: Heading3 options is shown but disabled after adding second Heading1
        /// 14.Click to add some Note
        /// 15.Check: Heading3 options is shown but disabled after adding note
        /// 16.Go back to zero state by deleting outline
        /// </summary>  
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AvailabilityDifferentLevelHeadingsFullPageModeTest()
        {
            const string SearchQuery = "law";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string HeadingString3 = "No breach of duty";
            const string HeadingString4 = "Expenses and trouble";
            const string NoteString = "He still planned to import small quantities of snakes because he had gone"
                                      + " to a lot of expense and trouble. Appellant subsequently attempted to sell 20 snakes";
            var searchResultsPage = this.GetHomePage<EdgeHomePage>().Header
                                        .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                                        .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                        .ContentType
                                        .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.TrialCourtOrders);

            var document = searchResultsPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(2);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
            
            var fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>()
                                        .RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.FullPageModeButton
                                        .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                        .OutlineBuilderFullPagePanel;

            document = fullPagePanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                    .OutlineInternalFullPagePanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
            var fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "Heading2 and Heading3 options are shown but disabled and Outline Note button is enabled",
                !fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & !fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & fullHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 1"))
                                        .Click<EdgeCommonDocumentPage>().RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingTextbox
                                       .SetText<EdgeCommonDocumentPage>(HeadingString1 + Keys.Enter).RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsFalse(
                "Heading3 options is shown but disabled",
                fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present,
                "Heading3 option doesn't show or enabled");

            document = fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2"))
                                        .Click<EdgeCommonDocumentPage>().RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingTextbox
                                       .SetText<EdgeCommonDocumentPage>(HeadingString2 + Keys.Enter).RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "All options are shown and enabled after updating heading2",
                fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & fullHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3"))
                                        .Click<EdgeCommonDocumentPage>().RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingTextbox
                                       .SetText<EdgeCommonDocumentPage>(HeadingString3 + Keys.Enter).RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue(
                "All options are shown and enabled after updating heading3",
                fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & fullHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 1"))
                                        .Click<EdgeCommonDocumentPage>().RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingTextbox
                                       .SetText<EdgeCommonDocumentPage>(HeadingString4 + Keys.Enter).RightPanel
                                       .OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton
                                       .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsFalse(
                "Heading3 options is shown but disabled after adding second Heading1",
                fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present,
                "Heading3 option doesn't show or enabled");

            document = CreateNoteInOutline(document, NoteString)
                       .RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton
                                                                        .Click<EdgeCommonDocumentPage>();
            fullHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsFalse(
                "Heading3 options is shown but disabled after adding note",
                fullHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present,
                "Heading3 option doesn't show or enabled");
        }

        /// <summary>
        /// Test that verifies we can align content inside the Outline in Full Page mode
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Cases results and click to view first document
        /// 3. Click Outlines on right panel and click Full page
        /// 4. Click to add heading1: Premises liability
        /// 5. Click to add outline note
        /// 6. Click to add heading2: Notice of defect
        /// 7. Click to add heading3: No breach of duty
        /// 8. Click to add outline note
        /// 9. Click Alignment and select Center option 
        /// 10.Check: Current Outline's content text is aligned to center
        /// 11.Click Alignment and select Left option
        /// 12.Check: Current Outline's content text is aligned to left
        /// 13.Click Alignment and select Hierarchy option
        /// 14.Check: Current Outline's content text is aligned by hierarchy
        /// 15.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AlignContentInsideOutlineFullPageTest()
        {
            const string SearchQuery = "law";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string HeadingString3 = "No breach of duty";
            var searchResultsPage = this.GetHomePage<EdgeHomePage>().Header
                                        .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                                        .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                        .ContentType
                                        .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Cases);

            var document = searchResultsPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            string tempText = document.GetDocumentSectionText(DocumentSection.OpinionBody)
                .Replace("\r\n", "");
            if (tempText.Length > 500)
            {
                tempText = tempText.Substring(0, 500);
            }
            string noteText1 = tempText.Substring(0, tempText.Length / 2);
            string noteText2 = tempText.Substring(tempText.Length / 2, noteText1.Length - 1);

            var fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>()
                                        .RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.FullPageModeButton
                                        .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                        .OutlineBuilderFullPagePanel;

            document = fullPagePanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();

            CreateHeadingInOutline(document, "1", HeadingString1).RightPanel.OutlineBuilderPanel
                .OutlineInternalFullPagePanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
            CreateNoteInOutline(document, noteText1);
            CreateHeadingInOutline(document, "2", HeadingString2);
            CreateHeadingInOutline(document, "3", HeadingString3)
                .RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
            document = CreateNoteInOutline(document, noteText2)
                       .RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel.AlignTextDropdown
                       .SelectOption<EdgeCommonDocumentPage>(OutlinesAlignOrderOptions.Center);
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.AreEqual<string>("Current Outline's content text is aligned to center",
                fullPagePanel.ReturnOutlineViewportTextAlignment(), "centered",
                "Incorrect alignment");

            document = fullPagePanel.AlignTextDropdown.SelectOption<EdgeCommonDocumentPage>(OutlinesAlignOrderOptions.Left);
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.AreEqual<string>("Current Outline's content text is aligned to left",
                fullPagePanel.ReturnOutlineViewportTextAlignment(), "left",
                "Incorrect alignment");

            document = fullPagePanel.AlignTextDropdown.SelectOption<EdgeCommonDocumentPage>(OutlinesAlignOrderOptions.Hierarchy);
            fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.AreEqual<string>("Current Outline's content text is aligned by hierarchy",
                fullPagePanel.ReturnOutlineViewportTextAlignment(), "hierarchy",
                "Incorrect alignment");

            fullPagePanel.AlignTextDropdown.SelectOption<EdgeCommonDocumentPage>(OutlinesAlignOrderOptions.Left);
        }

        /// <summary>
        /// Test that verifies we can add and remove auto-numbering inside the Outline in Full Page mode
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Regulations results and click to view first document
        /// 3. Click Outlines on right panel and click Full page
        /// 4. Click Create outline
        /// 5. Add Level 1, 2, and 3 Headings and Outline note
        /// 6. Check: Auto numbering is applied by default
        /// 7. Click Numbering button(to turn off)
        /// 8. Check: Auto numbering is not applied
        /// 9. Click Numbering button again(to turn on)
        /// 10.Check: Auto numbering is applied again
        /// 11.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AddAndRemoveHeadingAutoNumberingFullPageTest()
        {
            const string SearchQuery = "law";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string HeadingString3 = "No breach of duty";
            const string NoteString = "He still planned to import small quantities of snakes because he had gone"
                                      + " to a lot of expense and trouble. Appellant subsequently attempted to sell 20 snakes";
            var searchResultsPage = this.GetHomePage<EdgeHomePage>().Header
                                        .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                                        .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                                        .ContentType
                                        .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Regulations);

            var document = searchResultsPage.ResultList.ClickOnSearchResultDocumentByIndex<EdgeCommonDocumentPage>(0);
            document.CheckAndClosePendoDialog();
            document = document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
            document = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton.Click<EdgeCommonDocumentPage>()
                               .RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.FullPageModeButton
                               .Click<EdgeCommonDocumentPage>();
            document.CheckAndClosePendoDialog();
            var fullPagePanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;
            document = fullPagePanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();

            CreateHeadingInOutline(document, "1", HeadingString1);
            CreateHeadingInOutline(document, "2", HeadingString2);
            CreateHeadingInOutline(document, "3", HeadingString3).RightPanel.OutlineBuilderPanel
                .OutlineInternalFullPagePanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
            document = CreateNoteInOutline(document, NoteString);
            var fullPageInternal = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue("Auto-numbers are applied to Outline's headings",
                fullPageInternal.IsAutonumberingApplied(),
                "No auto-numbers found");

            document = fullPagePanel.NumberingButton.Click<EdgeCommonDocumentPage>();
            fullPageInternal = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsFalse("Auto-numbers are not applied to Outline's headings",
                fullPageInternal.IsAutonumberingApplied(),
                "Auto-numbers are still found");

            document = fullPagePanel.NumberingButton.Click<EdgeCommonDocumentPage>();
            fullPageInternal = document.RightPanel.OutlineBuilderPanel.OutlineInternalFullPagePanel;

            this.TestCaseVerify.IsTrue("Auto-numbers are applied to Outline's headings",
                fullPageInternal.IsAutonumberingApplied(),
                "No auto-numbers found");
        }

        /// <summary>
        /// Test that verifies we can add 500 nodes inside the Outline using Right panel and Add to Citation dialog
        /// * This test is NOT part of the scheduled regression run. It's executed on demand.
        /// </summary>
        [TestMethod]
        [TestCategory("OutlineBuilder_onDemand")]
        public void Add500NodesToOutlineTest()
        {
            const string SearchQuery = "law";
            const string OutlineTitle = "Complex Outline";
            const string NoteString = "He still planned to import small quantities of snakes because he had gone"
                                      + " to a lot of expense and trouble. Appellant subsequently attempted to sell 20 snakes"
                                      + " but he didn't succeed and forced to start from the beginning ...";
            const string TextToSelect = " United States citizens and domestic organizations seeking to provide support for lawful activities";
            List<string> ListOfNodes = new List<string>() { "Premises liability", "Notice of defect", "No breach of duty" };

            this.GetHomePage<EdgeHomePage>().Header
                .EnterSearchQueryAndClickSearch<EdgeCommonSearchResultPage>(SearchQuery)
                .NarrowTabPanel.SetActiveTab<ContentTypesTabComponent>(NarrowTab.ContentTypes)
                .ContentType
                .ClickContentTypeLink<EdgeCommonSearchResultPage>(ContentType.Cases);

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>
                ("Ib0037d817d3d11df8e45a3b5a338fda3").RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineInternalRightPanel;

            document = CreateNewOutline(document, OutlineTitle);

            AddToOutlineDialog outlinesModalPanel;
            HighlightMenuDialog highlightMenu;
            OutlineTabComponent addToOutline;
            bool switcher = true;
            int nodeCounter = 0;

            while (nodeCounter < 90)
            {
                for (int i = 1; i <= 3; i++)
                {
                    CreateHeadingInOutline(document, i.ToString(), ListOfNodes[i - 1]);
                }

                if (switcher)
                {
                    highlightMenu = document.SelectText(TextToSelect);
                    Thread.Sleep(500);
                    outlinesModalPanel = highlightMenu
                        .AddToOutlinesButton.Click<AddToOutlineDialog>();
                }
                else
                {
                    highlightMenu = document.SelectDocumentSection(DocumentSection.OpinionBody, 0);
                    Thread.Sleep(500);
                    outlinesModalPanel = highlightMenu
                        .AddToOutlinesButton.Click<AddToOutlineDialog>();
                }

                switcher = !switcher;
                addToOutline = outlinesModalPanel.AddToOutlinePanelComponent.SetActiveTab<OutlineTabComponent>(
                        AddToOutlineTabs.Outline);

                while (addToOutline.MoveBlockOfTextDownButton.Enabled)
                {
                    addToOutline.MoveBlockOfTextDownButton.Click<EdgeCommonDocumentPage>();
                }

                document = outlinesModalPanel.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
                outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;
                outlinesRightPanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
                CreateNoteInOutline(document, NoteString);

                nodeCounter += 3;
                this.TestCaseVerify.IsTrue("Outline contains expected number of nodes",
                    outlinesRightPanel.ListOfHeadings.Count == nodeCounter,
                    "Seems new nodes didn't add");
            }
        }
    }
}


