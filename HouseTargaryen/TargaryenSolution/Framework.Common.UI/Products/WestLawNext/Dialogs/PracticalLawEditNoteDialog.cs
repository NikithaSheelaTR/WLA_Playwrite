namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The class contains elements and methods pertaining to a Note on PL page
    /// </summary>
    public class PracticalLawEditNoteDialog : EditNoteDialog
    {
        private static readonly By NoteInputLocator =
            By.XPath("//div[contains(@class, 'co_richEditor co_noteArea')]");

        private static readonly By TrashButtonLocator = By.XPath("//div[@class = 'co_dropdownBoxContentRight']//a[@class = 'co_noteDelete']");

        /// <summary>
        /// Type text into note textbox
        /// </summary>
        /// <param name="noteText">Note text</param>
        public override void TypeNote(string noteText) => DriverExtensions.WaitForElementDisplayed(NoteInputLocator).SendKeysSlow(noteText);

        /// <summary>
        /// Enter text in text area and click 'Save'
        /// </summary>
        /// <param name="note">Text to set in the note text box</param>
        /// <returns>The new instance of T page</returns>
        public PracticalLawDocumentPage AddTextToNote(string note) 
        {
            this.TypeNote(note);
            DriverExtensions.WaitForJavaScript();
            return base.ClickSave<PracticalLawDocumentPage>();
        }

        /// <summary>
        /// Remove Note
        /// </summary>
        /// <returns>PL doc page</returns>
        public PracticalLawDocumentPage RemoveNote() => this.ClickElement<PracticalLawDocumentPage>(TrashButtonLocator);
    }
}
