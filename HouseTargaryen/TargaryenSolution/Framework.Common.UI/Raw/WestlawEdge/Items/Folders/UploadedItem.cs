namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Products.WestlawEdge.Components.Folders;

    using OpenQA.Selenium;

    /// <summary>
    /// Uploaded item
    /// </summary>
    public class UploadedItem : BaseItem
    {
        private static readonly By RemoveButtonLocator = By.XPath(".//button[@class = 'FileListItemRemove co_linkBlue']");
        private static readonly By ItemNameLocator = By.XPath(".//button[@class = 'FileListItemRemove co_linkBlue']");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="UploadedItem"/> class. 
        /// </summary>
        /// <param name="container">Container</param>
        public UploadedItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Add note to item component 
        /// </summary>
        public EdgeAddNoteToItemComponent AddNoteToItem => new EdgeAddNoteToItemComponent(this.Container);

        /// <summary>
        /// Remove button
        /// </summary>
        public IButton RemoveButton => new Button(this.Container, RemoveButtonLocator);

        /// <summary>
        /// Doc title
        /// </summary>
        public ILabel DocTitle => new Label(this.Container, ItemNameLocator);
    }
}