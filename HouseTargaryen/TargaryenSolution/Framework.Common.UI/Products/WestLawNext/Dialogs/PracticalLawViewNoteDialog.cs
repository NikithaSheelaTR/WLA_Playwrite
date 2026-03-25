namespace Framework.Common.UI.Products.WestLawNext.Dialogs
{
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.WestLawNext.Pages.Document;

    using OpenQA.Selenium;

    /// <summary>
    /// The class contains elements and methods pertaining to a Note on PL page
    /// </summary>
    public class PracticalLawViewNoteDialog : ViewNoteDialog
    {
        private static readonly By MinimizeButton = By.XPath(".//a[@class = 'co_noteMinimize co_widget_collapseIcon']");
        private static readonly By EditButtonLocator = By.XPath(".//a[@class = 'co_noteEdit']");

        /// <summary>
        /// Initializes a new instance of the <see cref="PracticalLawViewNoteDialog"/> class.
        /// </summary>
        /// <param name="container">container </param>
        public PracticalLawViewNoteDialog(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Clicks on inline note edit button
        /// </summary>
        /// <returns><see cref="EditNoteDialog"/></returns>
        public PracticalLawEditNoteDialog ClickEditButton()
            => this.ClickElement<PracticalLawEditNoteDialog>(this.Container, EditButtonLocator);

        /// <summary>
        /// Click minimize Button
        /// </summary>
        /// <returns>PL doc page</returns>
        public PracticalLawDocumentPage ClickMinimizeButton() => this.ClickElement<PracticalLawDocumentPage>(this.Container, MinimizeButton);
    }
}
