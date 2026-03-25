namespace Framework.Common.UI.Products.Shared.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.Toolbars;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Includes methods to work with notes and highlights
    /// </summary>
    public static class EdgeAnnotationManager
    {
        private static EdgeCommonDocumentPage DocumentPage { get; set; } = new EdgeCommonDocumentPage();

        /// <summary>
        /// Clicks 'Add Note' on toolbar, sets specified note text, click Save
        /// </summary>
        /// <param name="note">
        /// Text to set in the note text box 
        /// </param>
        public static void AddDocLevelNote(string note)
        {
            var editNoteDialog = DocumentPage.Toolbar.AnnotationsDropdown.SelectOption<EditNoteDialog>(EdgeAnnotationsOption.AddDocumentLevelNote);
            editNoteDialog.TypeNote(note);
            editNoteDialog.ClickSave<EdgeCommonDocumentPage>();
        }

        /// <summary>
        /// Selects Paragraph and adds yellow inline note to the document
        /// </summary>
        /// <param name="docSection">doc section</param>
        /// <param name="paragraphIndex">paragraph index</param>
        /// <param name="note">note text</param>
        /// <param name="color"></param>
        public static void AddInlineNote(DocumentSection docSection, int paragraphIndex, string note, HighlightColor color = HighlightColor.Yellow)
            => DocumentPage.SelectParagraph(docSection, paragraphIndex).AddNote<EdgeCommonDocumentPage>(note, color);

        /// <summary>
        /// Add note to highlight
        /// </summary>
        /// <param name="highlightedText"> highlighted text </param>
        public static void AddNoteToHighlightedText(string highlightedText)
        {
            ManageHighlightDialog highlightMenu = DocumentPage.ClickHighlightByText(highlightedText);
            EditNoteDialog editNote = highlightMenu.ClickAddNoteLink();
            editNote.AddTextToNote<EdgeCommonDocumentPage>("Making a test note at " + DateTime.Now);
        }

        /// <summary>
        /// Deletes inline note from document page with or without Note pane.
        /// If Note pane exists, collapsed icon is not displayed.
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <param name="isCollapsedIconDisplayed"> True - if collapsed icon displayed </param>
        /// <returns><see cref="EditNoteDialog"/></returns>
        public static EditNoteDialog DeleteInlineNote(string noteText, string ownerName, bool isCollapsedIconDisplayed) =>
            EdgeAnnotationManager.DeleteInlineNoteByText(noteText, ownerName, isCollapsedIconDisplayed);

        /// <summary>
        /// Deletes all Doc level notes and Inline notes that located on Note pane.
        /// Note pane appears only for Statutes, Cases, TCOs and Regulations documents.
        /// </summary>
        public static void DeleteAllNotes()
        {
            EdgeAnnotationManager.DeleteDocLevelNotes();
            EdgeAnnotationManager.DeleteInlineNotes(false);
        }

        /// <summary>
        /// Deletes all Inline and Doc level notes from doc page that does not have Note pane.
        /// </summary>
        public static void DeleteAllNotesFromDocWithoutNotePane()
        {
            EdgeAnnotationManager.DeleteDocLevelNotes();
            EdgeAnnotationManager.DeleteInlineNotes(true);
        }

        /// <summary>
        /// Deletes all highlights
        /// </summary>
        public static void DeleteAllHighlights()
            => DocumentPage.GetHighlightedElements().ForEach(EdgeAnnotationManager.DeleteHighlightByElement);

        /// <summary>
        /// Delete highlight by text
        /// </summary>
        /// <param name="text"> Highlighted text </param>
        public static void DeleteHighlightByText(string text)
        {
            IWebElement highlightedElement = DocumentPage.GetHighlightedElementByText(text);
            EdgeAnnotationManager.DeleteHighlightByElement(highlightedElement);
        }

        /// <summary>
        /// Delete doc level note by its index
        /// </summary>
        /// <param name="index">note index</param>
        /// <returns><see cref="EditNoteDialog"/></returns>
        public static EditNoteDialog DeleteDocLevelNoteByIndex(int index) =>
            DocumentPage.GetDocLevelNotesList().ElementAt(index).ClickNote().ClickDelete<EditNoteDialog>();

        /// <summary>
        /// Get text of highlight by highlight index
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> Highlight's index </returns>
        public static string GetHighlightTextByIndex(int index)
            => DocumentPage.GetHighlightedElementByIndex(index)?.Text;

        /// <summary>
        /// The add yellow highlighting.
        /// </summary>
        /// <param name="docSection">
        /// The doc section.
        /// </param>
        /// <param name="color"></param>
        public static void AddHighlighting(DocumentSection docSection, HighlightColor color = HighlightColor.Yellow) =>
            DocumentPage.SelectDocumentSection(docSection).AddHighlight<EdgeCommonDocumentPage>(color);

        /// <summary>
        /// The add yellow highlighting.
        /// </summary>
        /// <param name="docSection"> The doc section. </param>
        /// <param name="index"> The index. </param>
        /// <param name="color"></param>
        public static void AddHighlighting(DocumentSection docSection, int index, HighlightColor color = HighlightColor.Yellow) =>
            DocumentPage.SelectParagraph(docSection, index).AddHighlight<EdgeCommonDocumentPage>(color);

        /// <summary>
        /// Verify that Inline note is displayed
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <returns> True if note with name and owner is displayed, false otherwise </returns>
        public static bool IsInlineNoteDisplayed(string noteText, string ownerName)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();
            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);

                if (inlineNote.NoteText.Contains(noteText) &&  inlineNote.Owner.IndexOf(ownerName, StringComparison.InvariantCultureIgnoreCase) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Verify that document level note is displayed
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <returns> True if note with name and owner is displayed, false otherwise </returns>
        public static bool IsDocLevelNoteDisplayed(string noteText, string ownerName)
            => DocumentPage.GetDocLevelNotesList().Any(note => note.NoteText.Contains(noteText) && note.Owner.Contains(ownerName));
        
        /// <summary>
        /// Verify that highlight i displayed
        /// </summary>
        /// <param name="index"> Highlight index </param>
        /// <returns> True if displayed, false otherwise </returns>
        public static bool IsHighlightDisplayed(int index)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByIndex(index);
            return highlight != null && highlight.Displayed && !highlight.GetAttribute("class").Contains("co_hideHighlight");
        }

        /// <summary>
        /// Verifies that Highlight is in view.
        /// </summary>
        /// <param name="text"> Text of highlight </param>
        /// <returns> True if Highlight is in view, false otherwise </returns>
        public static bool IsHighlightInView(string text)
            => DocumentPage.GetHighlightedElementByText(text).IsElementInView();

        /// <summary>
        /// Verify that highlight is shared
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> True if shared, false otherwise </returns>
        public static bool IsHighlightShared(int index)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByIndex(index);
            return highlight != null && highlight.Displayed && highlight.GetAttribute("class").Contains("co_hlShared");
        }

        /// <summary>
        /// Verify that shared highlight displayed for reviewer
        /// </summary>
        /// <param name="highlightNumber"> Highlight number </param>
        /// <param name="ownerName"> Owner </param>
        /// <returns> True if displayed, false otherwise </returns>
        public static bool IsSharedHighlightDisplayedByIndex(int highlightNumber, string ownerName)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByIndex(highlightNumber);
            return highlight != null && EdgeAnnotationManager.IsSharedHighlightDisplayed(highlight, ownerName);
        }

        /// <summary>
        /// Verify that snippet is highlighted
        /// </summary>
        /// <param name="text"> Snippet's text </param>
        /// <returns> True if highlighted, false otherwise </returns>
        public static bool IsSnippetTextHighlighted(string text)
            => DocumentPage.GetHighlightedSnippets().Any(elem => elem.GetText().Contains(text));

        /// <summary>
        /// This method clicks on the inline note with specified text to make it editable.
        /// </summary>
        /// <param name="noteText"> text of the note to edit </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public static EditNoteDialog EditOwnerInlineNoteByNoteText(string noteText)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();
            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);

                if (inlineNote.NoteText.Contains(noteText))
                {
                    return inlineNote.EditButton.Click<EditNoteDialog>();
                }
            }

            return new EditNoteDialog();
        }

        /// <summary>
        /// Clicks on the doc level note with specified text to make it editable
        /// </summary>
        /// <param name="noteText"> Text of the note to edit </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public static EditNoteDialog EditOwnerDocLevelNoteByNoteText(string noteText)
        {
            DocumentPage.ScrollPageToTop();
            return DocumentPage.GetDocLevelNotesList().First(note => note.NoteText.Contains(noteText)).EditButton.Click<EditNoteDialog>();
        }

        /// <summary>
        /// Get edit button name
        /// </summary>
        /// <returns> Edit button name </returns>
        public static string GetEditButtonNameByNoteText(string noteText) => DocumentPage.GetDocLevelNotesList().First(note => note.NoteText.Contains(noteText)).EditButton.Text;

        /// <summary>
        /// Gets count of shared Inline notes by its owner
        /// </summary>
        /// <param name="owner">owner name</param>
        /// <returns>count of shared inline notes</returns>
        public static int GetCountOfSharedInlineNotes(string owner)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();

            int sharedInlineCount = 0;
            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);
                if (inlineNote.IsSharedIconDisplayed())
                {
                    sharedInlineCount++;
                }
            }

            return sharedInlineCount;
        }

        /// <summary>
        /// Get count Of Shared doc level note icons 
        /// </summary>
        /// <param name="owner"> Note's author name </param>
        /// <returns> Count of shared doc level notes </returns>
        public static int GetCountOfSharedDocLevelNotes(string owner) =>
            DocumentPage.GetDocLevelNotesList()
                .Count(n => n.IsSharedIconDisplayed() && n.Owner.Contains(owner));

        /// <summary>
        /// Gets count of shared Inline and Doc level notes
        /// </summary>
        /// <param name="owner">owner name</param>
        /// <returns>count of shared Inline and Doc level notes</returns>
        public static int GetCountOfSharedNotes(string owner)
            => EdgeAnnotationManager.GetCountOfSharedDocLevelNotes(owner)
                + EdgeAnnotationManager.GetCountOfSharedInlineNotes(owner);

        /// <summary>
        /// Checks if shared inline note is editable.
        /// !!! NOTE. Is an inline note is shared there is no 'Edit' button displayed.
        /// Thus, if 'Edit' button is NOT displayed - the note is not editable
        /// </summary>
        /// <param name="noteText">text note</param>
        /// <param name="owner">owner</param>
        /// <returns>true if inline note is editable</returns>
        public static bool IsSharedInlineNoteEditable(string noteText, string owner)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();

            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);

                if (inlineNote.NoteText.Contains(noteText) && inlineNote.Owner.Contains(owner) && inlineNote.IsSharedIconDisplayed())
                {
                    return inlineNote.EditButton.Displayed;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if shared doc level note is editable.
        /// </summary>
        /// <param name="noteIndex">Note index</param>
        /// <returns>True if doc level note is editable</returns>
        public static bool IsSharedDocLevelNoteEditable(int noteIndex = 0) => DocumentPage.GetDocLevelNotesList()[noteIndex].ClickNote().IsNoteInputDisplayed();

        /// <summary>
        /// Verifies that doc level note is in view.
        /// </summary>
        /// <param name="noteText"> The note text </param>
        /// <param name="owner"> The owner name </param>
        /// <returns> True if note is in view, false otherwise </returns>
        public static bool IsDocLevelNoteInView(string noteText, string owner) =>
            DocumentPage.GetDocLevelNotesList()
                        .First(note => note.NoteText.Contains(noteText) && note.Owner.Contains(owner)).Container
                        .IsElementInView();

        /// <summary>
        /// Deletes all notes that are created by highlighting text
        /// </summary>
        public static void DeleteAllHighlightNotes()
        {
            var inlineNotes = DocumentPage.GetInlineNotesList();
            foreach (var note in inlineNotes)
            {
                if (DocumentPage.IsMaximizeNoteIconDisplayed())
                {
                    DocumentPage.ClickMaximizeNoteIcon();
                }
                note.ClickOnNoteText().ClickDelete<EditNoteDialog>().ClickCloseLink<EdgeCommonDocumentPage>();
            }
        }

        private static void DeleteHighlightByElement(IWebElement highlightedElement)
            => DocumentPage = EdgeAnnotationManager.ClickOnHighlightedElement(highlightedElement).ClickDeleteButton<EdgeCommonDocumentPage>();

        private static ManageHighlightDialog ClickOnHighlightedElement(IWebElement highlightedElement)
        {
            highlightedElement.ScrollToElementCenter();
            highlightedElement.Hover();
            highlightedElement.CustomClick();
            return new ManageHighlightDialog();
        }

        private static void DeleteDocLevelNotes()
        {
            List<EdgeViewDocLevelNoteDialog> docNotes = DocumentPage.GetDocLevelNotesList();
            foreach (EdgeViewDocLevelNoteDialog docNote in docNotes)
            {
                EditNoteDialog noteDialog = docNote.ClickNote();
                DocumentPage = noteDialog.ClickDelete<EditNoteDialog>().ClickCloseLink<EdgeCommonDocumentPage>();
            }
        }

        private static EditNoteDialog DeleteInlineNoteByText(string noteText, string ownerName, bool isCollapsedIconDisplayed)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();

            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);
                if (isCollapsedIconDisplayed)
                {
                    DocumentPage.ClickMaximizeNoteIcon();
                }

                if (inlineNote.NoteText.Contains(noteText) && inlineNote.Owner.Contains(ownerName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return inlineNote.EditButton.Click<EditNoteDialog>().ClickDelete<EditNoteDialog>();
                }
            }

            return new EditNoteDialog();
        }

        private static void DeleteInlineNotes(bool isCollapsedIconDisplayed)
        {
            List<EdgeViewInlineNoteDialog> inlineNotes = DocumentPage.InlineNotes.GetInlineNotesList();

            foreach (EdgeViewInlineNoteDialog inlineNote in inlineNotes)
            {
                DocumentPage.InlineNotes.ScrollToNote(inlineNote);
                if (isCollapsedIconDisplayed)
                {
                    DocumentPage.ClickMaximizeNoteIcon();
                }

                DocumentPage = inlineNote.EditButton.Click<EditNoteDialog>().ClickDelete<EditNoteDialog>()
                          .ClickCloseLink<EdgeCommonDocumentPage>();
            }
        }

        private static bool IsSharedHighlightDisplayed(IWebElement highlight, string ownerName)
        {
            bool isDisplayed = false;
            if (highlight.Displayed)
            {
                highlight.CustomClick();
                DriverExtensions.WaitForJavaScript();
                isDisplayed = DocumentPage.IsSharedHighlightsOwnerNameDisplayed(ownerName);
            }

            return isDisplayed;
        }
    }
}