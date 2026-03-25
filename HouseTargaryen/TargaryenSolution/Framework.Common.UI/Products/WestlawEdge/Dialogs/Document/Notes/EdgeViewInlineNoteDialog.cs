namespace Framework.Common.UI.Products.WestlawEdge.Dialogs.Document.Notes
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Document.Notes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;

    using OpenQA.Selenium;


    /// <summary>
    /// View level dialog for inline notes
    /// </summary>
    public class EdgeViewInlineNoteDialog : ViewNoteDialog
    {
        private static readonly By EditButtonLocator 
            = By.XPath(".//div[@class='co_viewNote']//button[@class='co_noteEditButton']");

        private static readonly By CloseButtonLocator 
            = By.XPath(".//div[@class='co_viewNote']//button[@class='co_noteCloseButton']");

        /// <summary>
        /// Initializes a new instance of the <see cref="EdgeViewInlineNoteDialog"/> class.
        /// </summary>
        /// <param name="container">container </param>
        public EdgeViewInlineNoteDialog(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.Container, EditButtonLocator);

        /// <summary>
        /// Close button
        /// </summary>
        public IButton CloseButton => new Button(this.Container, CloseButtonLocator);        
    }
}