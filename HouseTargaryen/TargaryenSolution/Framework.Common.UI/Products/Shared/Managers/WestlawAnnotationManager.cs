namespace Framework.Common.UI.Products.Shared.Managers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Document;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Models.Annotations;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Includes methods to work with notes and highlights
    /// </summary>
    public static class WestlawAnnotationManager
    {
        private static CommonDocumentPage DocumentPage { get; set; } = new CommonDocumentPage();

        /// <summary>
        /// Create Doc Header Note
        /// </summary>
        /// <param name="noteText">
        /// text to set in the note text box 
        /// </param>
        /// <param name="authorName">
        /// The author Name.
        /// </param>
        /// <returns>
        /// New Note Item Model
        /// </returns>
        public static NoteItemModel AddDocLevelNote(string noteText, string authorName = null)
        {
            var editNoteDialog = new CommonDocumentPage().Toolbar.AnnotationsDropdown.SelectOption<EditNoteDialog>(AnnotationsOption.AddNote);
            editNoteDialog.TypeNote(noteText);
            editNoteDialog.ClickSave();

            return new NoteItemModel() { FirstName = authorName, NoteText = noteText };
        }

        /// <summary>
        /// Selects Paragraph and adds yellow inline note to the document
        /// </summary>
        /// <typeparam name="T">page instance <see cref="ICommonDocumentPage"/></typeparam>
        /// <param name="docSection">doc section</param>
        /// <param name="paragraphIndex">paragraph index</param>
        /// <param name="note">note text</param>
        /// <param name="color"></param>
        /// <returns>page instance</returns>
        public static T AddInlineNote<T>(DocumentSection docSection, int paragraphIndex, string note, HighlightColor color = HighlightColor.Yellow) where T : ICommonDocumentPage
            => DocumentPage.SelectParagraph(docSection, paragraphIndex).AddNote<T>(note,color);

        /// <summary>
        /// Delete all notes in the document
        /// </summary>
        public static void DeleteAllNotes()
        {
            List<ViewNoteDialog> notesList = DocumentPage.GetNotesList();
            foreach (ViewNoteDialog note in notesList)
            {
                EditNoteDialog editNoteDialog = note.ClickNote();
                if (editNoteDialog.IsDeleteButtonDisplayed())
                {
                    editNoteDialog.ClickDelete<EditNoteDialog>().ClickCloseLink<CommonDocumentPage>();
                }
            }
        }

        /// <summary>
        /// Delete note by index
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public static EditNoteDialog DeleteNoteByIndex(int index)
            => DocumentPage.GetNoteByIndex(index).ClickNote().ClickDelete<EditNoteDialog>();

        /// <summary>
        /// TODO: change the type of return value
        /// Delete a note by NoteItemModel
        /// </summary>
        /// <param name="noteItem"> The note Item. </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public static EditNoteDialog DeleteNote(NoteItemModel noteItem)
            => DocumentPage.GetNote(noteItem).ClickNote().ClickDelete<EditNoteDialog>();

        /// <summary>
        /// Delete all highlights in the document
        /// </summary>
        public static void DeleteAllHighlights()
            => DocumentPage.GetHighlightedElements().ForEach(WestlawAnnotationManager.DeleteHighlightByElement);

        /// <summary>
        /// Delete highlight by text
        /// </summary>
        /// <param name="text"> Highlighted text </param>
        public static void DeleteHighlightByText(string text)
        {
            IWebElement highlightedElement = DocumentPage.GetHighlightedElementByText(text);
            WestlawAnnotationManager.DeleteHighlightByElement(highlightedElement);
        }

        /// <summary>
        /// Delete highlight
        /// </summary>
        /// <param name="highlightItem"> The highlight Item. </param>
        /// <returns> The <see cref="CommonDocumentPage"/>. </returns>
        public static CommonDocumentPage DeleteHighlight(HighlightItemModel highlightItem)
            => WestlawAnnotationManager.ClickHighlight(highlightItem).ClickDeleteButton<CommonDocumentPage>();

        /// <summary>
        /// Add a note to the highlight
        /// </summary>
        /// <param name="textToAdd"> Text for adding  </param> 
        /// <param name="noteText"> The note Text. </param>
        /// <returns> New Note Item Model </returns>
        public static NoteItemModel AddNoteToHighlightedText(string textToAdd, string noteText = null)
        {
            IWebElement highlightedElement = DocumentPage.GetHighlightedElementByText(textToAdd);
            ManageHighlightDialog highlightDialog = WestlawAnnotationManager.ClickOnHighlightedElement(highlightedElement);
            EditNoteDialog editNoteDialog = highlightDialog.ClickAddNoteLink();

            return editNoteDialog.AddTextToNote(noteText ?? "Making a test note at " + DateTime.Now);
        }

        /// <summary>
        /// Add a note to the highlight
        /// </summary>
        /// <param name="highlightItemModel"> The highlight Item Model. </param>
        /// <param name="noteText"> The note Text. </param>
        /// <returns> New Note Item Model </returns>
        public static NoteItemModel AddNoteToHighlightedText(HighlightItemModel highlightItemModel, string noteText = null)
        {
            ManageHighlightDialog highlightDialog = WestlawAnnotationManager.ClickHighlight(highlightItemModel);
            EditNoteDialog editNoteDialog = highlightDialog.ClickAddNoteLink();

            return editNoteDialog.AddTextToNote(noteText ?? "Making a test note at " + DateTime.Now);
        }

        /// <summary>
        /// Verify that snippet is highlighted
        /// </summary>
        /// <param name="text"> Snippet's text </param>
        /// <returns> True if highlighted, false otherwise </returns>
        public static bool IsSnippetTextHighlighted(string text)
            => DocumentPage.GetHighlightedSnippets().Any(elem => elem.GetText().Contains(text));

        /// <summary>
        /// Verify that note is editable
        /// </summary>
        /// <param name="noteIndex"> Note text </param>
        /// <returns> True if editable, false otherwise </returns>
        public static bool IsNoteEditable(int noteIndex = 0)
            => DocumentPage.GetNoteByIndex(noteIndex).ClickNote().IsNoteInputDisplayed();

        /// <summary>
        /// Verify that note is displayed
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <returns> True if not with name and owner is displayed, false otherwise </returns>
        public static bool IsNoteDisplayed(string noteText, string ownerName)
        {
            List<ViewNoteDialog> allNotes = DocumentPage.GetNotesList();
            return WestlawAnnotationManager.IsNoteDisplayed(noteText, ownerName, allNotes);
        }

        /// <summary>
        /// Verify that note is displayed
        /// </summary>
        /// <param name="noteItemModel"> The note Item Model. </param>
        /// <param name="authorName">The author Name.</param>
        /// <returns> true if note is displayed, false otherwise </returns>
        public static bool IsNoteDisplayed(NoteItemModel noteItemModel, string authorName = null)
        {
            List<ViewNoteDialog> allNotes = DocumentPage.GetNotesList();
            return WestlawAnnotationManager.IsNoteDisplayed(noteItemModel.NoteText, authorName ?? noteItemModel.FirstName, allNotes);
        }

        /// <summary>
        /// Verifies if a note is in the hide state(collapsed in icon)
        /// </summary>
        /// <returns>True if a note is collapsed, false otherwise</returns>
        public static bool IsCollapsedNoteIconDisplayed(NoteItemModel item) => 
           DriverExtensions.IsDisplayed(DocumentPage.GetCollapsedNoteIcon(item));

        /// <summary>
        /// Expand a collapsed note
        /// </summary>
        public static void ExpandNote(NoteItemModel item) => DocumentPage.GetCollapsedNoteIcon(item).Click();

        /// <summary>
        /// Verify that Inline note is displayed
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <returns> True if not with name and owner is displayed, false otherwise </returns>
        public static bool IsInlineNoteDisplayed(string noteText, string ownerName)
        {
            List<ViewNoteDialog> allNotes = DocumentPage.GetInlineNotesList();
            return WestlawAnnotationManager.IsNoteDisplayed(noteText, ownerName, allNotes);
        }

        /// <summary>
        /// Verify that document level note is displayed
        /// </summary>
        /// <param name="noteText"> Note's text </param>
        /// <param name="ownerName"> Note's owner </param>
        /// <returns> True if not with name and owner is displayed, false otherwise </returns>
        public static bool IsDocLevelNoteDisplayed(string noteText, string ownerName)
        {
            List<ViewNoteDialog> allNotes = DocumentPage.GetDocLevelNotesList();
            return WestlawAnnotationManager.IsNoteDisplayed(noteText, ownerName, allNotes);
        }

        /// <summary>
        /// Edit note by NoteItemModel
        /// </summary>
        /// <param name="noteItemModel"> The note Item Model. </param>
        /// <returns> The <see cref="EditNoteDialog"/>. </returns>
        public static EditNoteDialog EditNote(NoteItemModel noteItemModel)
            => DocumentPage.GetNotesList().FirstOrDefault(note => note.NoteText.Contains(noteItemModel.NoteText))?.ClickNote();

        /// <summary>
        /// Get text of highlight by highlight index
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> Highlight's index </returns>
        public static string GetHighlighTextByIndex(int index)
            => DocumentPage.GetHighlightedElementByIndex(index)?.Text;

        /// <summary>
        /// Verify that highlight i displayed
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> True if displayed, false otherwise </returns>
        public static bool IsHighlightDisplayed(int index)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByIndex(index);
            return highlight != null && highlight.Displayed && !highlight.GetAttribute("class").Contains("co_hideHighlight");
        }

        /// <summary>
        /// Get all shared notes
        /// </summary>
        /// <param name="owner"> The owner. </param>
        /// <returns> List of shared notes  </returns>
        public static List<ViewNoteDialog> GetSharedNotesListByOwner(string owner)
            => DocumentPage.GetNotesList().Where(note => note.IsSharedIconDisplayed() && note.Owner.Contains(owner, StringComparison.InvariantCultureIgnoreCase)).ToList();

        /// <summary>
        /// Get all shared notes
        /// </summary>
        /// <param name="noteItemModel"> The note Item Model. </param>
        /// <returns> List of shared notes  </returns>
        public static List<ViewNoteDialog> GetSharedNotesList(NoteItemModel noteItemModel)
            => DocumentPage.GetNotesList().Where(note => note.IsSharedIconDisplayed() && note.Owner.Contains(noteItemModel.FirstName, StringComparison.InvariantCultureIgnoreCase)).ToList();

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
            return highlight != null && WestlawAnnotationManager.IsSharedHighlightDisplayed(highlight, ownerName);
        }

        /// <summary>
        /// Verify that shared highlight displayed for reviewer
        /// </summary>
        /// <param name="highlightItem"> The highlight Item. </param>
        /// <returns> True if displayed, false otherwise </returns>
        public static bool IsSharedHighlightDisplayed(HighlightItemModel highlightItem)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByText(highlightItem.HighlightedText);
            return highlight != null && WestlawAnnotationManager.IsSharedHighlightDisplayed(highlight, highlightItem.UserInfo);
        }

        /// <summary>
        /// Verify that shared highlight displayed for reviewer
        /// </summary>
        /// <param name="text"> Highlight text </param>
        /// <param name="ownerName"> Owner </param>
        /// <returns> True if displayed, false otherwise </returns>
        public static bool IsSharedHighlightDisplayedByText(string text, string ownerName)
        {
            IWebElement highlight = DocumentPage.GetHighlightedElementByText(text);
            return highlight != null && WestlawAnnotationManager.IsSharedHighlightDisplayed(highlight, ownerName);
        }

        /// <summary>
        /// Is text highlighted
        /// </summary>
        /// <param name="text">Text which is expected to be higlighted</param>
        /// <returns>True if text is highlighted, false otherwise</returns>
        public static bool IsTextHighlighted(string text) => DriverExtensions.IsDisplayed(DocumentPage.GetHighlightedElementByText(text));

        /// <summary>
        /// Is highlight displayed
        /// </summary>
        /// <param name="text">Text which is expected to be higlighted</param>
        /// <returns>True if a highlight is displayed, false otherwise</returns>
        public static bool IsHighlightDisplayed(string text) => !DriverExtensions.GetElement(DocumentPage.GetHighlightedElementByText(text)).GetAttribute("class").Contains("co_hideHighlight");

        /// <summary>
        /// Click highlight by HighlightItemModel
        /// </summary>
        /// <param name="highlightItem"> The highlight Item. </param>
        /// <returns> New Instance of Manage Highlight Dialog </returns>
        public static ManageHighlightDialog ClickHighlight(HighlightItemModel highlightItem)
            => WestlawAnnotationManager.ClickHighlightByText(highlightItem.HighlightedText);

        /// <summary>
        /// Highlight a specific document section
        /// </summary>
        /// <param name="docSection">DocSection</param>
        /// <param name="authorName">Author name</param>
        /// <param name="color"></param>
        /// <returns> The <see cref="HighlightItemModel"/>. </returns>
        public static HighlightItemModel HighlightDocumentSection(DocumentSection docSection, string authorName = null, HighlightColor color = HighlightColor.Yellow)
        {
            string highlightedText =
                DocumentPage.SelectSnippetAndGetSelectedText(
                    DriverExtensions.GetElement(By.CssSelector(DocumentPage.DocSectionsMap[docSection].LocatorString)));
            new HighlightMenuDialog().AddHighlight<CommonDocumentPage>(color);

            return new HighlightItemModel() { HighlightedText = highlightedText, UserInfo = authorName };
        }

        /// <summary>
        /// Highlight Paragraph
        /// </summary>
        /// <param name="docSection">DocSection</param>
        /// <param name="paragraphIndex">The index of a paragraph</param>
        /// <param name="authorName"> The author Name. </param>
        /// <param name="color"></param>
        /// <returns> The <see cref="HighlightItemModel"/>. </returns>
        public static HighlightItemModel HighlightParagraph(DocumentSection docSection, int paragraphIndex, string authorName = null, HighlightColor color = HighlightColor.Yellow)
        {
            By selector = By.CssSelector(DocumentPage.DocSectionsMap[docSection].LocatorString + " .co_paragraphText");
            string highlightedText = DocumentPage.SelectSnippetAndGetSelectedText(DriverExtensions.GetElements(selector).ElementAt(paragraphIndex));
            new HighlightMenuDialog().AddHighlight<CommonDocumentPage>(color);

            return new HighlightItemModel() { HighlightedText = highlightedText, UserInfo = authorName };
        }

        private static ManageHighlightDialog ClickHighlightByText(string text)
        {
            DocumentPage.GetHighlightedElements().FirstOrDefault(elem => elem.Text.Equals(text.Trim())).CustomClick();
            return new ManageHighlightDialog();
        }

        private static bool IsSharedHighlightDisplayed(IWebElement highlight, string ownerName)
        {
            bool isDisplayed = false;
            if (highlight.Displayed)
            {
                highlight.ScrollToElement();
                highlight.Click();
                DriverExtensions.WaitForJavaScript();
                isDisplayed = DocumentPage.IsSharedHighlightsOwnerNameDisplayed(ownerName);
            }

            return isDisplayed;
        }

        private static void DeleteHighlightByElement(IWebElement highlightedElement)
            => WestlawAnnotationManager.ClickOnHighlightedElement(highlightedElement).ClickDeleteButton<CommonDocumentPage>();

        private static ManageHighlightDialog ClickOnHighlightedElement(IWebElement highlightedElement)
        {
            highlightedElement.ScrollToElement();
            highlightedElement?.Click();
            return new ManageHighlightDialog();
        }

        private static bool IsNoteDisplayed(string noteText, string ownerName, List<ViewNoteDialog> notesList)
            => notesList.Any(elem => elem.NoteText.Contains(noteText)
                                     && elem.Owner.Contains(ownerName, StringComparison.InvariantCultureIgnoreCase));
    }
}
