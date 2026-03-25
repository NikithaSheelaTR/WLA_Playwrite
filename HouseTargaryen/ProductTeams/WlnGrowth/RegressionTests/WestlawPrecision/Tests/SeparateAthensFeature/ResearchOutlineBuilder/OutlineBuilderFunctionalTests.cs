namespace WestlawPrecision.Tests.SeparateAthensFeature.ResearchOutlineBuilder
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Dialogs.Delivery;
    using Framework.Common.UI.Products.Shared.Enums.Delivery;
    using Framework.Common.UI.Utils;
    using OpenQA.Selenium;
    using System.Linq;
    using System;
    using System.IO;

    using Framework.Common.UI.Products.Shared.Managers;
    using Framework.Common.UI.Products.WestlawEdge.Components.RightPanel.OutlineBuilder;
    using Framework.Common.UI.Products.WestlawEdgePremium.Pages;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;

    [TestClass]
    public class OutlineBuilderFunctionalTests : OutlineBuilderBaseTest
    {
        /// <summary>
        /// Test that verifies we can create new outline in Outline Builder, edit its name and delete it
        /// 1. Sign in Westlaw Precision
        /// 2. Go to Trial Court Orders document
        /// 3. Click Outlines on right panel
        /// 4. Check: Outlines right panel is displayed and title contains: My outlines
        /// 5. Click to add outline with title: Monroe City Attractive Case
        /// 6. Check: Outline title has been changed correctly
        /// 7. Click Back to list of outlines button
        /// 8. Check: Outlines list contains entity with such title
        /// 9. Delete the outline 
        /// 10.Check: No more such entity in Outlines list
        /// 11.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateChangeTitleAndDeleteOutlineTest()
        {
            const string DocGuid = "I13017ae05cee11ee8b1aa41cfacbf0e2";
            const string OutlineTitle = "Monroe City Attractive Case";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines right panel is displayed and title contains right text",
                outlinesRightPanel.IsDisplayed()
                && outlinesRightPanel.OutlinePanelHeaderLabel.Text.Contains("My outlines"),
                "Outlines panel is not displayed or doesn't contain right title");

            document = CreateNewOutline(document, OutlineTitle);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outline title has been changed correctly",
                outlinesRightPanel.CurrentOutlineTitleLabel.Text.Contains(OutlineTitle),
                "Outline title is incorrect");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with such title",
                outlinesRightPanel.ListOfOutlines.Contains(OutlineTitle),
                "No such title in the list");

            document = outlinesRightPanel.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle)
                                         .DeleteOutline<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsFalse(
                "No more such entity in Outlines list",
                outlinesRightPanel.ListOfOutlines.Any(item => item.TitleButton.Text == OutlineTitle),
                "Element with such title is still there");
        }

        /// <summary>
        /// Test that verifies we can create and save outline without name and it'll have default name in this case
        /// 1. Sign in Westlaw Precision
        /// 2. Go to Statutes document
        /// 3. Click Outlines on right panel
        /// 4. Click to add outline
        /// 5. Check: Outline title has been set to default as expected: Untitled
        /// 6. Click to edit outline name with empty space
        /// 7. Check: Outline title remains: Untitled
        /// 8. Click to edit outline name with title: Monroe City Attractive Memo
        /// 9. Check: outline title has been updated accordingly 
        /// 10.Click Back to outline list button
        /// 11.Check: Outlines list contains entity with such title
        /// 12.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateOutlineSaveEmptyTitleVerifyDefaultIsSavedTest()
        {
            const string DocGuid = "NC482C4F0575011F0B3D8D0F9B0B67B62";
            const string DefaultOutlineTitle = "Untitled";
            const string OutlineTitle = "Monroe City Attractive Memo";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            document = outlinesRightPanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outline title has been set to default as expected for initial creation",
                outlinesRightPanel.CurrentOutlineTitleLabel.Text.Contains(DefaultOutlineTitle),
                "Outline title is incorrect");

            document = EditOutline(document, string.Empty);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outline title has been set to default as expected for editing with empty name",
                outlinesRightPanel.CurrentOutlineTitleLabel.Text.Contains(DefaultOutlineTitle),
                "Outline title is incorrect");

            document = EditOutline(document, OutlineTitle);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outline title has been changed correctly",
                outlinesRightPanel.CurrentOutlineTitleLabel.Text.Contains(OutlineTitle),
                "Outline title is incorrect");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with such title",
                outlinesRightPanel.ListOfOutlines.Contains(OutlineTitle),
                "No such title in the list");
        }

        /// <summary>
        /// Test that verifies we can create outline add heading then edit and delete this heading
        /// 1. Sign in Westlaw Precision
        /// 2. Go to Regulations document
        /// 3. Click Outlines on right panel
        /// 4. Click to add outline and add heading1: Premises liability
        /// 5. Check: Heading list contains text of heading1
        /// 6. Edit heading1 to heading2: Notice of defect
        /// 7. Check: Heading list contains text of heading2
        /// 8. Edit heading2 text and leave it empty and revisit outline
        /// 9. Check: Heading list contains text of heading2 after empty update 
        /// 10.Edit heading and click Delete
        /// 11.Check: Confirm header deletion popup is shown
        /// 12.Click Delete button
        /// 13.Check: Heading with heading2 text is deleted
        /// 14.Click Back to list of outlines button
        /// 15.Check: Outlines list contains entity with title: Untitled
        /// 16.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateOutlineAddHeadingEditAndDeleteHeadingTest()
        {
            const string DocGuid = "N085B79F08B4411D98CF4E0B65F42E6DA";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document = document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;
            var outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            outlinesRightPanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>();
            document = CreateHeadingInOutline(document, "1", HeadingString1);
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with text: " + HeadingString1,
                outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString1)),
                "No such entity on the page");

            outlinesHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString1)).EditButton
                                .Click<EdgeCommonDocumentPage>();
            document = EditHeading(document, HeadingString2);
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with text: " + HeadingString2,
                outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "No such entity on the page");

            outlinesHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString2))
                                         .EditButton.Click<EdgeCommonDocumentPage>();
            document = EditHeading(document, String.Empty);
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains entity with text by leaving name empty: " + HeadingString2,
                outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "No such entity on the page");

            document = outlinesHeadingPanel.ListOfHeadings.First(item => item.Text.Contains(HeadingString2)).EditButton
                                           .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                           .OutlineInternalRightPanel.DeleteHeadingButton
                                           .Click<EdgeCommonDocumentPage>();
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Confirm header deletion popup is shown",
                outlinesHeadingPanel.ConfirmDeleteHeadingButton.Displayed,
                "No confirmation popup on the page");

            outlinesHeadingPanel.ConfirmDeleteHeadingButton.Click<EdgeCommonDocumentPage>();

            this.TestCaseVerify.IsFalse(
                "Heading with such text was deleted",
                outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2)),
                "Entity is still there on the page");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with such title",
                outlinesRightPanel.ListOfOutlines.Contains("Untitled"),
                "No such title in the list");
        }

        /// <summary>
        /// Test that verifies we can create outline and add heading level 1,2,3 and notes
        /// 1. Sign in Westlaw Precision
        /// 2. Go to Trial Court Orders document
        /// 3. Click Outlines on right panel
        /// 4. Click to add outline and click Add headings
        /// 5. Check: Heading2 and Heading3 options are shown but disabled and Note button is enabled
        /// 6. Click heading1 and add: Premises liability, then click Add again
        /// 7. Check: Heading3 options is shown but disabled
        /// 8. Click heading2 and add: Notice of defect, then click Add again
        /// 9. Check: All options are shown and enabled after updating heading2
        /// 10.Click heading3 and add: No breach of duty, then click Add again
        /// 11.Check: All options are shown and enabled after updating heading3
        /// 12.Add some Note text
        /// 13.Check: Heading list contains all previously added entities
        /// 14.Click Back to list of outlines button
        /// 15.Check: Outlines list contains entity with title: Untitled
        /// 16.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AvailabilityDifferentLevelHeadingsTest()
        {
            const string DocGuid = "Ie588b1306f2111f0a4dbff60442cb5fb";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string HeadingString3 = "No breach of duty";
            const string NoteString = "He still planned to import small quantities of snakes because he had gone"
                                      + " to a lot of expense and trouble. Appellant subsequently attempted to sell 20 snakes";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.CheckAndClosePendoDialog();
            document = document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            document = outlinesRightPanel.CreateNewOutlineButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                         .OutlineInternalRightPanel.AddHeadingOrNoteButton.Click<EdgeCommonDocumentPage>();
            var outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Heading2 and Heading3 options are shown but disabled and Note button is enabled",
                !outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & !outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & outlinesHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 1"))
                                           .Click<EdgeCommonDocumentPage>().RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingTextbox
                                           .SetText<EdgeCommonDocumentPage>(HeadingString1 + Keys.Enter).RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingOrNoteButton
                                           .Click<EdgeCommonDocumentPage>();
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsFalse(
                "Heading3 options is shown but disabled",
                outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present,
                "Heading3 option doesn't show or enabled");

            document = outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2"))
                                           .Click<EdgeCommonDocumentPage>().RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingTextbox
                                           .SetText<EdgeCommonDocumentPage>(HeadingString2 + Keys.Enter).RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingOrNoteButton
                                           .Click<EdgeCommonDocumentPage>();
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "All options are shown and enabled",
                outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & outlinesHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3"))
                                           .Click<EdgeCommonDocumentPage>().RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingTextbox
                                           .SetText<EdgeCommonDocumentPage>(HeadingString3 + Keys.Enter).RightPanel
                                           .OutlineBuilderPanel.OutlineInternalRightPanel.AddHeadingOrNoteButton
                                           .Click<EdgeCommonDocumentPage>();
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "All options are shown and enabled",
                outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 2")).Present
                & outlinesHeadingPanel.HeadingButton.First(item => item.Title.Contains("Heading 3")).Present
                & outlinesHeadingPanel.NoteButton.Present,
                "Heading2, Heading3 and Note options doesn't show or enabled");

            document = CreateNoteInOutline(document, NoteString);
            outlinesHeadingPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            this.TestCaseVerify.IsTrue(
                "Heading list contains all previously added entities",
                outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString1))
                & outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString2))
                & outlinesHeadingPanel.ListOfHeadings.Any(item => item.Text.Contains(HeadingString3)),
                "No such entities on the page");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list contains entity with such title",
                outlinesRightPanel.ListOfOutlines.Contains("Untitled"),
                "No such title in the list");
        }

        /// <summary>
        /// Test that verifies we can create several outlines and sort it by alphabet order
        /// 1. Sign in WLP and search: law 
        /// 2. Go to Cases results and click to view first document
        /// 3. Create Outline1 with title: Accountant Breach of Fiduciary
        /// 4. Create Outline2 with title: Monroe City Attractive Memo
        /// 5. Create Outline3 with title: Camp, LLC - Defendants
        /// 6. Click Back to list button and select Title A-Z under sort
        /// 7. Check: Outlines list is sorted in alphabetical order
        /// 8. Select Title Z-A under sort
        /// 9. Check: Outlines list is sorted in reverse alphabetical order
        /// 10.Click Outline3 and add some Note text
        /// 11.Click Back to list button and select Last updated under sort
        /// 12.Check: Outlines list is sorted by last updated order
        /// 13.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void CreateSeveralOutlinesAndSortItAlphabetTest()
        {
            const string DocGuid = "If9218cc289e211d9b6ea9f5a173c4523";
            const string OutlineTitle1 = "Accountant Breach of Fiduciary";
            const string OutlineTitle2 = "Monroe City Attractive Memo";
            const string OutlineTitle3 = "Camp, LLC - Defendants";
            const string NoteString = "He still planned to import small quantities of snakes because he had gone"
                                      + " to a lot of expense and trouble. Appellant subsequently attempted to sell 20 snakes";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document = document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);
            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            document = CreateNewOutline(document, OutlineTitle1).RightPanel.OutlineBuilderPanel
                                         .OutlineBuilderRightPanel.BackToListOfOutlinesButton
                                         .Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            document = CreateNewOutline(document, OutlineTitle2, false).RightPanel.OutlineBuilderPanel
                                         .OutlineBuilderRightPanel.BackToListOfOutlinesButton
                                         .Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            document = CreateNewOutline(document, OutlineTitle3, false).RightPanel.OutlineBuilderPanel
                                         .OutlineBuilderRightPanel.BackToListOfOutlinesButton
                                         .Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            document = outlinesRightPanel.SortOutlinesByDropdown.SelectOption<EdgeCommonDocumentPage>(
                OutlinesSortOrderOptions.TitleA);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list is sorted in alphabetical order",
                outlinesRightPanel.ListOfOutlines.First().TitleButton.Text == OutlineTitle1,
                "Wrong sorting order");

            document = outlinesRightPanel.SortOutlinesByDropdown.SelectOption<EdgeCommonDocumentPage>(OutlinesSortOrderOptions.TitleZ);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list is sorted in reverse alphabetical order",
                outlinesRightPanel.ListOfOutlines.First().TitleButton.Text == OutlineTitle2,
                "Wrong sorting order");

            document = outlinesRightPanel.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle3)
                                         .TitleButton.Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                         .OutlineInternalRightPanel.AddHeadingOrNoteButton
                                         .Click<EdgeCommonDocumentPage>();
            document = CreateNoteInOutline(document, NoteString)
                               .RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel.BackToListOfOutlinesButton
                               .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel
                               .SortOutlinesByDropdown
                               .SelectOption<EdgeCommonDocumentPage>(OutlinesSortOrderOptions.LastUpdated);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            this.TestCaseVerify.IsTrue(
                "Outlines list is sorted by last updated order",
                outlinesRightPanel.ListOfOutlines.First().TitleButton.Text == OutlineTitle3,
                "Wrong sorting order");

            document = outlinesRightPanel.SortOutlinesByDropdown.SelectOption<EdgeCommonDocumentPage>(
                OutlinesSortOrderOptions.TitleA);
        }

        /// <summary>
        /// Test that verifies we can upload file with current outline
        /// 1. Sign in Westlaw Precision
        /// 2. Go to Cases document: I6c98b820d1d411e8b93ad6f77bf99296
        /// 3. Create Outline1 with title: Accountant Breach
        /// 4. Add heading1 with name: Premises liability
        /// 5. Add heading2 with name: Notice of defect
        /// 6. Add heading3 with name: No breach of duty
        /// 7. Download Outline in Word format
        /// 8. Check: downloaded outline name contains: AOA v Rennert.docx
        /// 9. Go to full page mode
        /// 10.Check: List of Outlines contains just created one and it is opened
        /// 11.Download Outline in Word format
        /// 12.Check: downloaded outline name contains: Westlaw Precision - AOA v Rennert.docx
        /// 13.Go back to zero state by deleting outline
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void DownloadOutlineToFileBothRightPanelAnFullPageTest()
        {
            const string OutlineTitle = "Accountant Breach";
            const string HeadingString1 = "Premises liability";
            const string HeadingString2 = "Notice of defect";
            const string HeadingString3 = "No breach of duty";
            const string FileName = "AOA v Rennert";
            const string DocGuid = "I6c98b820d1d411e8b93ad6f77bf99296";

            DeleteFilesInFolder(FolderToSave);

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(DocGuid);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            var outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                             .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                             .OutlineBuilderRightPanel;

            document = CreateNewOutline(document, OutlineTitle);
            var internalRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineInternalRightPanel;

            CreateHeadingInOutline(document, "1", HeadingString1);
            CreateHeadingInOutline(document, "2", HeadingString2);
            document = CreateHeadingInOutline(document, "3", HeadingString3);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;

            var downloadDialog = document.Toolbar.DeliveryDropdown.SelectOption<DownloadDialog>(DeliveryMethod.Download);
            downloadDialog = downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Docx);
            var readyForDeliveryDialog = downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>();
            readyForDeliveryDialog.ClickDownloadButton<EdgeCommonDocumentPage>();

            FileUtils.WaitForFileDownload(FolderToSave, FileName + ".docx");

            this.TestCaseVerify.IsTrue("The document was downloaded",
                Directory.GetFiles(FolderToSave).Any(i => i.Contains(FileName + ".docx")),
                "Can't find that file");

            var fullPage = outlinesRightPanel.FullPageModeButton.Click<EdgeCommonDocumentPage>();
            fullPage.CheckAndClosePendoDialog();
            var fullPagePanel = fullPage.RightPanel.OutlineBuilderPanel.OutlineBuilderFullPagePanel;

            this.TestCaseVerify.AreEqual("List of Outlines contains just created one and it is opened",
                fullPagePanel.CurrentOutlineTitleLabel.Text, OutlineTitle,
                "Created Outline isn't opened");

            downloadDialog = fullPagePanel.DownloadButton.Click<DownloadDialog>();
            downloadDialog = downloadDialog.TheBasicsTab.FormatDropdown.SelectOption<DownloadDialog>(DeliveryFormat.Docx);
            readyForDeliveryDialog = downloadDialog.ClickDownloadButton<ReadyForDeliveryDialog>();
            readyForDeliveryDialog.ClickDownloadButton<EdgeCommonDocumentPage>();

            var expectedFileName = "Westlaw Precision - " + fullPagePanel.CurrentOutlineTitleLabel.Text + ".docx";
            FileUtils.WaitForFileDownload(FolderToSave, expectedFileName);

            this.TestCaseVerify.IsTrue("The document was downloaded on full page",
                Directory.GetFiles(FolderToSave).Any(i => i.Contains(expectedFileName)),
                "Can't find that file");
        }

        /// <summary>
        /// Test verifies ROB is hidden on analytical pages in reading mode (story: 1731078)
        /// 1. Sign in WLP
        /// 2. View analytical doc: I0d101bd4a5e711ebb8fca0356b4d27b0 and click Reading Mode
        /// 3. Check: ALR Digest document page hides ROB function
        /// 4. View analytical doc: I485035911b0a11daabcae0e56c9db133 and click Reading Mode
        /// 5. Check: Anno Code document page hides ROB function
        /// 6. View analytical doc: I8a5cd582727911da9cd2d0bb1ab9e23e and click Reading Mode
        /// 7. Check: Jurs document page hides ROB function
        /// 8. View analytical doc: Ifefb3e95f7e911dab04ede773c62241b and click Reading Mode
        /// 9. Check: Jury Instructions document page hides ROB function
        /// 10.View analytical doc: Id78fdef10aa611da9e62f0ceff3e4816 and click Reading Mode
        /// 11.Check: Treatise Practice Guide Commentary document page hides ROB function
        /// 12.View analytical doc: I39fd4eb2378c11e6a3b0c9b7d06be68c and click Reading Mode
        /// 13.Check: Treatise Other Commentary document page hides ROB function
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void HideFunctionOnAnalyticalPageTest()
        {
            const string AlrDigestDoc = "I0d101bd4a5e711ebb8fca0356b4d27b0";
            const string AnnoCodeDoc = "I485035911b0a11daabcae0e56c9db133";
            const string JursDoc = "I8a5cd582727911da9cd2d0bb1ab9e23e";
            const string JuryInstructionsDoc = "Ifefb3e95f7e911dab04ede773c62241b";
            const string TreatisePracticeGuideDoc = "I6f875e1e23ea11e59a3df93fc9165f32";
            const string TreatiseOtherCommentaryDoc = "I39fd4eb2378c11e6a3b0c9b7d06be68c";

            var documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(AlrDigestDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("ALR Digest document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "ALR Digest document page should hide ROB function");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(AnnoCodeDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("Anno Code document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "Anno Code document page should hide ROB function");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(JursDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("Jurs document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "Jurs document page should hide ROB function");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(JuryInstructionsDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("Jury Instructions document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "Jury Instructions document page should hide ROB function");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(TreatisePracticeGuideDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("Treatise Practice Guide Commentary document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "Treatise Practice Guide Commentary document page should hide ROB function");

            documentPage = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<PrecisionDocumentPageWithHeadnotes>(TreatiseOtherCommentaryDoc);
            documentPage = documentPage.Toolbar.ClickToolbarElement<PrecisionDocumentPageWithHeadnotes>(EdgeToolbarElements.ReadingMode);
            this.TestCaseVerify.IsFalse("Treatise Other Commentary document page hides ROB function",
                documentPage.RightPanel.IsDisplayed(),
                "Treatise Other Commentary document page should hide ROB function");
        }

        /// <summary>
        /// Test verifies we can create, rename, and delete outline on Analytical page (Story: 1731080)
        /// 1. Sign in WLP and view analytical doc: I0d101bd4a5e711ebb8fca0356b4d27b0 
        /// 2. Open the Outlines right panel
        /// 3. Check: Outlines right panel is displayed with title: My outlines
        /// 4. Create outline with user provided title
        /// 5. Check: Outline created with expected title
        /// 6. Check: Outlines list contains correct entry with expected title
        /// 7. Delete the outline
        /// 8. Check: Outline deleted successfully from Outlines list
        /// </summary>
        [TestMethod]
        [TestCategory(FeatureTestCategory)]
        [TestCategory(CurrentTestCategory)]
        public void AddROBSupportOnAnalyticalPageTest()
        {
            const string AlrDoc = "Iee4b34e350c011dab569972103a5b03e";
            const string OutlineTitle = "Add Support On Analytical Page";

            var document = EdgeNavigationManager.Instance.NavigateToDocumentDirectly<EdgeCommonDocumentPage>(AlrDoc);
            document.CheckAndClosePendoDialog();
            document.RightPanel.Toggle.ToggleState<EdgeCommonDocumentPage>(false);

            OutlineBuilderRightPanelComponent outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlinesPanelButton
                                                                           .Click<EdgeCommonDocumentPage>().RightPanel.OutlineBuilderPanel
                                                                           .OutlineBuilderRightPanel;
            this.TestCaseVerify.IsTrue(
                "Outline right panel displayed with correct title text",
                outlinesRightPanel.IsDisplayed()
                && outlinesRightPanel.OutlinePanelHeaderLabel.Text.Contains("My outlines"),
                "Outline right panel not displayed with correct title text");

            document = CreateNewOutline(document, OutlineTitle);
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;
            this.TestCaseVerify.IsTrue(
                "Outline created with expected title",
                outlinesRightPanel.CurrentOutlineTitleLabel.Text.Contains(OutlineTitle),
                "Outline not created with expected title");

            document = outlinesRightPanel.BackToListOfOutlinesButton.Click<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;
            this.TestCaseVerify.IsTrue(
                "Outlines list contains correct entry with expected title",
                outlinesRightPanel.ListOfOutlines.Contains(OutlineTitle),
                "Entry with expected title missing from Outline list");

            document = outlinesRightPanel.ListOfOutlines.First(item => item.TitleButton.Text == OutlineTitle)
                                         .DeleteOutline<EdgeCommonDocumentPage>();
            outlinesRightPanel = document.RightPanel.OutlineBuilderPanel.OutlineBuilderRightPanel;
            this.TestCaseVerify.IsFalse(
                "Outline deleted successfully from Outlines list",
                outlinesRightPanel.ListOfOutlines.Any(item => item.TitleButton.Text == OutlineTitle),
                "Deleted outline still displays in Outlines list");
        }
    }
}
