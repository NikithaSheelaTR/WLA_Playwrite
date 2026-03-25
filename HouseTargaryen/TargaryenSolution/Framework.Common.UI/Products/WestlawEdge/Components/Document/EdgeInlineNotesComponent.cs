namespace Framework.Common.UI.Products.WestlawEdge.Components.Document
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Inline Notes Component
    /// </summary>
    public class EdgeInlineNotesComponent : BaseModuleRegressionComponent
    {
        private const string NoteByTextAndOwnerLctMask = ".//*[@class='co_viewNote'][contains(.,'{0}')]//div[@class='co_noteHeader_createdBy']";
        private const string CollapsedNoteIconLctMask = "//div[@class='co_viewNoteText' and contains(.,'{0}')]/ancestor::span[contains(@class,'co_noteHolder')]//a[contains(@class,'co_noteMaximize')]";
        private const string ViewNoteDialogContainerLctMask = "//div[@id='co_inlineNotes' and .//div[@class='co_viewNoteText' and contains(.,'{0}')]]//div[@class='co_notesContainer']";

        private static readonly By ContainerLocator = By.XPath("//span[contains(@id,'co_noteHolder_') and .//div[@class='co_noteContainer']]");
        private static readonly By InlineNotesOnNotePaneContainerLocator = By.XPath("//*[@id = 'co_rightColumn']//div[@class='co_noteContainer']");
        private static readonly By InlineNotesLocator = By.XPath("//div[@id='co_inlineNotes']");
        private static readonly By CollapsedNoteLinkLocator = By.XPath("//span[@id = 'Headnotes_hiddenNoteCtrl']");
        private static readonly By SingleNoteIconLocator = By.XPath("//span[@id = 'Headnotes_hiddenNoteCtrl']//a[contains(@class, 'marginOpen')]//span[contains(@class, 'annotation-customOne')]");
        private static readonly By MultipleNoteIconLocator = By.XPath("//span[@id = 'Headnotes_hiddenNoteCtrl']//a[contains(@class, 'marginOpen')]//span[contains(@class, 'icon_annotationShow-customOne')]");
        private static readonly By InfoBoxContainerLocator = By.XPath("//*[@id = 'co_rightColumn']//div[@id = 'infoBoxWrapper']");

        /// <summary>
        /// The NavigationComponent for inline notes
        /// </summary>
        public EdgeInlineNotesNavigationComponent NavigationComponent { get; } = new EdgeInlineNotesNavigationComponent();

        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(InfoBoxContainerLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets list of Inline notes
        /// </summary>
        /// <returns>list of Inline notes</returns>
        public List<EdgeViewInlineNoteDialog> GetInlineNotesList() => DriverExtensions
            .GetElements(this.ComponentLocator).Select(note => new EdgeViewInlineNoteDialog(note)).ToList();

        /// <summary>
        /// Get collapsed note link text 
        /// </summary>
        /// <returns>Collapsed note control text</returns>
        public string GetCollapsedNoteLinkText() => DriverExtensions.GetText(CollapsedNoteLinkLocator).Trim();

        /// <summary>
        /// Click collapsed note link
        /// </summary>
        public void ClickCollapsedNoteLink() => DriverExtensions.WaitForElementDisplayed(CollapsedNoteLinkLocator).CustomClick();

        /// <summary>
        /// Is single note icon displayed
        /// </summary>
        /// <returns>True if the single icon is displayed, false otherwise.</returns>
        public bool IsSingleNoteIconDisplayed() => DriverExtensions.IsDisplayed(DriverExtensions.GetElement(SingleNoteIconLocator));

        /// <summary>
        /// Is multiple note icon displayed
        /// </summary>
        /// <returns>True if multiple icon is displayed, false otherwise.</returns>
        public bool IsMultipleNoteIconDisplayed() => DriverExtensions.IsDisplayed(DriverExtensions.GetElement(MultipleNoteIconLocator));

        /// <summary>
        /// Verifies that inline level note is in view.
        /// </summary>
        /// <param name="notesName">The notes Name. </param>
        /// <param name="authorName">The author Name.</param>      
        /// <returns> True if note is in view, false otherwise </returns>
        public bool IsInlineNoteInView(string notesName, string authorName)
        {
            bool isNoteInView;

            var listOfNotes = DriverExtensions.GetElements(
                                    DriverExtensions.GetElement(InlineNotesLocator),
                                    By.XPath(string.Format(NoteByTextAndOwnerLctMask, notesName)));

            // User name is not displayed on an inline not for Academic users
            if (!string.IsNullOrEmpty(authorName))
            {
                isNoteInView = listOfNotes.First( note => note.Text.Contains(authorName, StringComparison.InvariantCultureIgnoreCase))
                      .IsElementInView();
            }
            else
            {
                isNoteInView = listOfNotes.First().IsElementInView();
            }

            return isNoteInView;
        }
                                             
        /// <summary>
        /// Clicks inline note by its text
        /// </summary>
        /// <param name="noteText">note text</param>
        /// <returns><see cref="EdgeViewInlineNoteDialog"/></returns>
        public EdgeViewInlineNoteDialog ClickInliteNoteByText(string noteText)
        {
            DriverExtensions.Click(By.XPath(string.Format(CollapsedNoteIconLctMask, noteText)));
            IWebElement noteContainer = DriverExtensions.WaitForElement(By.XPath(string.Format(ViewNoteDialogContainerLctMask, noteText)));
            return new EdgeViewInlineNoteDialog(noteContainer);
        }

        /// <summary>
        /// Scroll to note
        /// </summary>
        /// <param name="inlineNote">Inline note</param>
        public void ScrollToNote(EdgeViewInlineNoteDialog inlineNote) => inlineNote.Container.ScrollToElementCenter();

        /// <summary>
        /// Are inline notes displayed on Note pane
        /// </summary>
        public bool AreInlineNotesDisplayed() => DriverExtensions.IsDisplayed(InlineNotesOnNotePaneContainerLocator);

        /// <summary>
        /// Click to inline note
        /// </summary>
        /// <param name="noteText"> text of inline note</param>
        /// <returns><see cref="EdgeViewInlineNoteDialog"/></returns>
        public EdgeViewInlineNoteDialog ClickInlineNote(string noteText)
        {
            var inlineNote = this.GetInlineNotesList().Find(note => note.NoteText.Contains(noteText));
            inlineNote.Container.Click();
            return new EdgeViewInlineNoteDialog(inlineNote.Container);
        }
    }
}