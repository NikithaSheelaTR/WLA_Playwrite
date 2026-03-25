namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;

    /// <summary>
    /// View level dialog for doc level notes
    /// </summary>
    public class EdgeViewDocLevelNoteDialog : ViewNoteDialog
    {
        private static readonly By EditButtonLocator = By.XPath(".//div[@class='co_viewNote']//button[@class='co_linkBtn co_noteEditButton']");
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeViewDocLevelNoteDialog"/> class.
        /// </summary>
        /// <param name="container"> container </param>
        public EdgeViewDocLevelNoteDialog(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.Container, EditButtonLocator);    
    }
}