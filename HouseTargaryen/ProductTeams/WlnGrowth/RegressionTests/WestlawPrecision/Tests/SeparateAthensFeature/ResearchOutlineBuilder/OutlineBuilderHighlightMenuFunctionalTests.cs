namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.OutlineBuilder;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.WestlawEdge.Enums;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Products.WestlawEdge.Components.Preferences;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.HomePage;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Preferences;

    [TestClass]
    public class OutlineBuilderHighlightMenuFunctionalTests : OutlineBuilderBaseTest
    {
        /// <summary>
        /// Test that verifies we can create new outline in main document Outlines modal using toolbar
        /// 1. Sign in WLP 
        /// 2. View document: If9218cc289e211d9b6ea9f5a173c4523
        /// 3. Click Copy menu and select: Add citation to outline
        /// 4. Click Create outline button
        /// 5. Check: Outline movable text contains content as expected
        /// 6. Click Save and click Copy menu and select: Add citation to outline
        /// 7. Check: Outlines list contains entity with title Untitled for initial creation
        /// 8. Click outline title
        /// 9. Check: Outline is opened and has title as expected: Add to Untitled
        /// 10.Click Back to existing outlines
        /// 11.Check: Outlines list contains entity with title Untitled after clicking Back
        /// 12.Delete outline
        /// 13.Check: Outlines list doesn't contain entity with title: Untitled
        /// 14.Go back to zero state by deleting outline
        /// </summary>      
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateOutlineFromMainDocumentToolbarTest()
        {
            const string OutlineMovableText = "Hamdi v. Rumsfeld, 337 F.3d 335 (4th Cir. 2003)";
            const string OutlineModalTitle = "Add to Untitled";
            const string OutlineTitle = "Untitled";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesModalPanel =
                document.Toolbar.CopyMenu.SelectOption<AddToOutlineDialog>(CopyMenuOption.AddCitationToOutline);
            var addToOutlines = outlinesModalPanel.CreateNewOutlineButton.Click<AddToUntitledDialog>();

            this.TestCaseVerify.IsTrue(
                "Outline movable text contains content as expected",
                addToOutlines.AddToOutlinePanelComponent.SetActiveTab<OutlineTabComponent>(AddToOutlineTabs.Outline)
                .OutlineMovableText.Text.Contains(OutlineMovableText)
                & addToOutlines.DialogModalTitle.Text.Equals(OutlineModalTitle),
                "Outline movable text is incorrect");

            document = addToOutlines.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            outlinesModalPanel =
                document.Toolbar.CopyMenu.SelectOption<AddToOutlineDialog>(CopyMenuOption.AddCitationToOutline);

            outlinesModalPanel.WaitForListOfOutlines();
            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with title Untitled for initial creation",
                outlinesModalPanel.ListOfOutlines.Contains(OutlineTitle),
                "No such title in the list");

            addToOutlines = outlinesModalPanel.ClickOnOutlineByTitle(OutlineTitle);

            this.TestCaseVerify.IsTrue(
                "Outline is opened and has title as expected",
                addToOutlines.DialogModalTitle.Displayed
                & addToOutlines.DialogModalTitle.Text.Equals(OutlineModalTitle),
                "Outline isn't opened or title is incorrect");

            outlinesModalPanel = addToOutlines.AddToOutlinePanelComponent
                                              .SetActiveTab<OutlineTabComponent>(AddToOutlineTabs.Outline)
                                              .BackToListOfOutlinesButton.Click<AddToOutlineDialog>();

            outlinesModalPanel.WaitForListOfOutlines();
            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with such title",
                outlinesModalPanel.ListOfOutlines.Contains(OutlineTitle),
                "No such title in the list");

            outlinesModalPanel = outlinesModalPanel.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle)
                                                   .DeleteOutline<AddToOutlineDialog>();

            this.TestCaseVerify.IsFalse(
                "Outlines list doesn't contain entity with such title",
                outlinesModalPanel.ListOfOutlines.Contains(OutlineTitle),
                "Outline with such title is still in the list");

            outlinesModalPanel.OutlineModalCancelButton.Click<EdgeCommonDocumentPage>();
        }

        /// <summary>
        /// Test that verifies we can create Outline by selecting paragraph in document and using context menu
        /// 1. Sign in WLP and search: law
        /// 2. Go to Cases results and view second document
        /// 3. Select the Opinion section and click Add to Outline
        /// 4. Click Create outline button
        /// 5. Check: Outline movable text contains content as expected         
        /// 6. Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void SelectDocumentSectionAddToOutlineTest()
        {
            const string OutlineModalTitle = "Add to Untitled";
            const string OpinionSectionId = "Ifdc66d91d92211efa892ca24996b411e_opinionHeader";
            const string OpinionText = "State Homestead Exemptions and Bankruptcy Law: Is It Time for Congress To Close the Loophole? 7 Rutgers Bus. L.J. 143, 149–165 (2010)";
            const string DocGuid = "I67ac4deba3aa11e3b58f910794d4f75e";

            var preferencesDialog = this.GetHomePage<PrecisionHomePage>().Header.OpenProfileSettingsDialog().ClickWestlawPreferences<EdgePreferencesDialog>();
            var copyTabComponent = preferencesDialog.TabPanel.SetActiveTab<EdgeCopyWithReferenceTabComponent>(EdgePreferencesDialogTabs.Citations);
            copyTabComponent.SetCheckbox(EdgeCopyWithReferenceTab.AddQuotationsAroundCopiedText, false);
            preferencesDialog.SaveButton.Click<EdgeHomePage>();

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.ScrollToSection(OpinionSectionId);

            var outlinesModalPanel = document.SelectText(OpinionText).AddToOutlinesButton.Click<AddToOutlineDialog>();
            var addToOutlines = outlinesModalPanel.CreateNewOutlineButton.Click<AddToUntitledDialog>();
            string movableText = addToOutlines.AddToOutlinePanelComponent
                .SetActiveTab<OutlineTabComponent>(AddToOutlineTabs.Outline).OutlineMovableText.Text;

            this.TestCaseVerify.IsTrue(
                "Outline movable text contains content as expected",
                addToOutlines.DialogModalTitle.Text.Equals(OutlineModalTitle) &
                OpinionText.Contains(movableText),
                "Outline movable text is incorrect. Expected: " + OpinionText + " Displayed: " + movableText);            
        }

        /// <summary>
        /// Test that verifies we can create Outline, then select text in main document and add it to existing Outline
        /// 1. Sign in WLP and search: law
        /// 2. Go to Cases results and view second document
        /// 3. Create Outline with title: Accountant Breach of Fiduciary Duty
        /// 4. Select portion of opinion section and add it to the outline
        /// 5. Check: Outline title text contains content as expected
        /// 6. Click Save button         
        /// 7. Check: Outlines list contains entity with right title and doesn't contain untitled
        /// 8. Click outline title to view the outline
        /// 9. Check: Outline contains snippet with expected text
        /// 10.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AddSelectedTextToAlreadyExistingOutlineTest()
        {
            const string OutlineTitle = "Accountant Breach of Fiduciary Duty";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            document = CreateNewOutline(document, OutlineTitle);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            string expectedParagraphText = document.GetDocumentSectionText(DocumentSection.OpinionBody).Replace(
                "\r\n", "").Substring(20, 60);
            var outlinesModalPanel = document.SelectDocumentSection(DocumentSection.OpinionBody).AddToOutlinesButton
                                             .Click<AddToOutlineDialog>();

            this.TestCaseVerify.IsTrue(
                "Outline title text contains content as expected",
                outlinesModalPanel.DialogModalTitle.Text.Equals("Add to " + OutlineTitle),
                "Outline content text is incorrect");

            document = outlinesModalPanel.SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            
            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with right title and doesn't contain untitled",
                outlinesRightPanel.ListOfOutlines.Contains(OutlineTitle) &
                !outlinesRightPanel.ListOfOutlines.Contains("Untitled"),
                "No such title in the list");

            document = outlinesRightPanel.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle)
                .TitleButton.Click<EdgeCommonDocumentPage>();
            var outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue("Outline contains snippet with such text",
                outlinesHeadingPanel.OutlineSnippetNodeLabel.Contains(expectedParagraphText),
                "No such text in the Outline");
        }

        /// <summary>
        /// Test that verifies we can move selected text when adding it to existing Outline
        /// 1. Sign in WLP and search: law
        /// 2. Go to Cases results and view second document
        /// 3. Create Outline with title: Accountant Breach of Fiduciary Duty
        /// 4. Add heading1: Premises liability
        /// 5. Add heading2: Notice of defect
        /// 6. Select opinion section and add to outline
        /// 7. Check: Selected text can be moved down, not up
        /// 8. Click move down button
        /// 9. Check: Selected text can be moved up and down
        /// 10.Click move down button
        /// 11.Check: Selected text can be moved up, not down
        /// 12.Click move up button and Save button
        /// 13.Check: outline list just contains the one created 
        /// 14.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void VerifyMoveAddedTextBetweenTheHeadingsTest()
        {
            const string OutlineTitle = "Accountant Breach of Fiduciary Duty";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            CreateNewOutline(document, OutlineTitle);
            CreateHeadingInOutline(document, "1", HeadingString1);
            document = CreateHeadingInOutline(document, "2", HeadingString2);

            var outlinesModalPanel = document.SelectDocumentSection(DocumentSection.OpinionBody)
                .AddToOutlinesButton.Click<AddToOutlineDialog>();

            var addToOutline = outlinesModalPanel.AddToOutlinePanelComponent.SetActiveTab<OutlineTabComponent>(
                    AddToOutlineTabs.Outline);

            this.TestCaseVerify.IsTrue("Selected text can be moved down, not up",
                addToOutline.MoveBlockOfTextDownButton.Enabled & !addToOutline.MoveBlockOfTextUpButton.Enabled,
                "Button Down is not enabled or enabled both");

            addToOutline.MoveBlockOfTextDownButton.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue("Selected text can be moved up and down",
                addToOutline.MoveBlockOfTextDownButton.Enabled & addToOutline.MoveBlockOfTextUpButton.Enabled,
                "Up and Down buttons are not enabled");

            addToOutline.MoveBlockOfTextDownButton.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue("Selected text can be moved up, not down",
                addToOutline.MoveBlockOfTextUpButton.Enabled & !addToOutline.MoveBlockOfTextDownButton.Enabled,
                "Button Up is not enabled or enabled both");

            addToOutline.MoveBlockOfTextUpButton.Click<AddToOutlineDialog>().SaveOutlineButton.Click<EdgeCommonDocumentPage>();
            
            document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsTrue("List of Outlines contains just created one",
                outlinesRightPanel.ListOfOutlines.Contains(OutlineTitle),
                "No such Outline in the list");
        }

        /// <summary>
        /// Test that verifies we limited to enter only 4000 characters to Outline's note
        /// 1. Sign in WLP and search: law
        /// 2. Go to Cases results and view first document
        /// 3. Create Outline with title: Accountant Breach of Fiduciary Duty
        /// 4. Add note 97 times: Lorem ipsum dolor sit amet eli
        /// 5. Check: Edit Note text area shows warning message
        /// 6. Click Back to list, then Copy menu, Add citation to outline
        /// 7. Click outline title and go to Add note tab
        /// 8. Add note 97 times: Lorem ipsum dolor sit amet eli
        /// 9. Check: Error message is displayed after sending more than 4000 symbols        
        /// 10.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]       
        public void UnableToEnterMoreThan4000SymbolsToNoteTest()
        {
            const string OutlineTitle = "Accountant Breach of Fiduciary Duty";
            const string LoremIpsum = "Lorem ipsum dolor sit amet eli";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;
            var noteString = string.Join(" ", Enumerable.Repeat(LoremIpsum, 97));
            var i = noteString.Length;

            document = CreateNewOutline(document, OutlineTitle).RightPanel.OutlineBuilderPanel
                                                                         .OutlineInternalRightPanel.AddHeadingOrNoteButton
                                                                         .Click<EdgeCommonDocumentPage>();
            var outlinesHeadingsPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;
            document = outlinesHeadingsPanel.NoteButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                .OutlineInternalRightPanel.AddHeadingTextbox.SetText<EdgeCommonDocumentPage>(noteString);

            this.TestCaseVerify.IsTrue("Edit Note text area shows warning message",
                outlinesHeadingsPanel.IsErrorMessageDisplayed(), "No such warning in the text area");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();

            var citationDialog =
                document.Toolbar.CopyMenu.SelectOption<AddToOutlineDialog>(CopyMenuOption.AddCitationToOutline);
            citationDialog.WaitForListOfOutlines();
            citationDialog.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle).TitleButton
                          .Click<AddToOutlineDialog>();

            var addOutlineTab = citationDialog.AddToOutlinePanelComponent
                .SetActiveTab<AddNoteTabComponent>(AddToOutlineTabs.AddNote);

            this.TestCaseVerify.IsTrue("Error message is displayed after sending more than 4000 symbols",
                addOutlineTab.IsErrorMessageDisplayed(noteString),
               "Error message isn't displayed after sending more than 4000 symbols");                       
        }

        /// <summary>
        /// Test that verifies we can create several Outlines and sort it from modal panel
        /// 1. Sign in WLP and search: law
        /// 2. Go to Cases results and view first document
        /// 3. Create Outline1 with title: Accountant Breach of Fiduciary Duty
        /// 4. Create Outline2 with title: Monroe City Attractive Nuisance Mem
        /// 5. Create Outline3 with title: Camp, LLC - Defendants' Reply Brief
        /// 6. Click Copy menu, Add citation to outline, and Back to list
        /// 7. Check: Outlines list is sorted in alphabetical order
        /// 8. Sort list by Title Z-A
        /// 9. Check: Outlines list is sorted in reverse alphabetical order
        /// 10.Add note to Outline3: default note
        /// 11.Click Copy menu, Add citation to outline, and Back to list
        /// 12.Sort list by Last updated
        /// 13.Check: Outlines list is sorted with recently updated on top
        /// 14.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateAndSortOutlinesFromModalPanelTest()
        {
            const string NoteString = "default note";
            const string OutlineTitle1 = "Accountant Breach of Fiduciary Duty";
            const string OutlineTitle2 = "Monroe City Attractive Nuisance Mem";
            const string OutlineTitle3 = "Camp, LLC - Defendants' Reply Brief";
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            CreateNewOutline(document, OutlineTitle1).RightPanel.OutlineBuilderPanel
                              .OutlineBuilderRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            CreateNewOutline(document, OutlineTitle2, false).RightPanel.OutlineBuilderPanel
                .OutlineBuilderRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            document = CreateNewOutline(document, OutlineTitle3, false);

            var citationDialog =
                document.Toolbar.CopyMenu.SelectOption<AddToOutlineDialog>(CopyMenuOption.AddCitationToOutline);
            var addToOutline =
                citationDialog.AddToOutlinePanelComponent.SetActiveTab<OutlineTabComponent>(AddToOutlineTabs.Outline);

            addToOutline.BackToListOfOutlinesButton.Click<AddToOutlineDialog>().SortOutlinesByDropdown
                        .SelectOption<AddToOutlineDialog>(OutlinesSortOrderOptions.TitleA);

            this.TestCaseVerify.IsTrue(
               "Outlines list is sorted in alphabetical order",
               citationDialog.ListOfOutlines.First().TitleButton.Text == OutlineTitle1,
               "Wrong sorting order");

            citationDialog =
                citationDialog.SortOutlinesByDropdown.SelectOption<AddToOutlineDialog>(OutlinesSortOrderOptions.TitleZ);

            this.TestCaseVerify.IsTrue(
              "Outlines list is sorted in reverse alphabetical order",
              citationDialog.ListOfOutlines.First().TitleButton.Text == OutlineTitle2,
              "Wrong sorting order");

            document = citationDialog.ClickOnOutlineByTitle(OutlineTitle3).AddToOutlinePanelComponent
                                     .SetActiveTab<AddNoteTabComponent>(AddToOutlineTabs.AddNote).NoteTextbox
                                     .SetText<AddToUntitledDialog>(NoteString).SaveOutlineButton
                                     .Click<EdgeCommonDocumentPage>();
            var untitledDialog =
                document.Toolbar.CopyMenu.SelectOption<AddToUntitledDialog>(CopyMenuOption.AddCitationToOutline);

            untitledDialog.AddToOutlinePanelComponent.SetActiveTab<AddNoteTabComponent>(AddToOutlineTabs.AddNote);
            untitledDialog.AddToOutlinePanelComponent.SetActiveTab<OutlineTabComponent>(AddToOutlineTabs.Outline)
                          .BackToListOfOutlinesButton.Click<AddToOutlineDialog>().SortOutlinesByDropdown
                          .SelectOption<AddToOutlineDialog>(OutlinesSortOrderOptions.LastUpdated);

            this.TestCaseVerify.IsTrue(
              "Outlines list is sorted with recently updated on top",
              citationDialog.ListOfOutlines.First().TitleButton.Text == OutlineTitle3,
              "Wrong sorting order");

            citationDialog.OutlineModalCancelButton.Click<EdgeCommonDocumentPage>();
        }
    }
}
