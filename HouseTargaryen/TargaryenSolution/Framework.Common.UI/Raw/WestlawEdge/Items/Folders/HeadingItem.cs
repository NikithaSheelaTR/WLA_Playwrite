namespace Framework.Common.UI.Raw.WestlawEdge.Items.Folders
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Each item represents item in the list of Outline's headings
    /// </summary>
    public class HeadingItem : BaseItem
    {
        private static readonly By EditButtonLocator = By.CssSelector("button.OutlineBuilder-editIcon");
        private static readonly By ItemNameLocator = By.CssSelector("div.outlineHeadingNode");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="HeadingItem"/> class. 
        /// </summary>
        /// <param name="container">Container</param>
        public HeadingItem(IWebElement container)
            : base(container)
        {
        }

        /// <summary>
        /// Edit button
        /// </summary>
        public IButton EditButton => new Button(this.Container, EditButtonLocator);

        /// <summary>
        /// Text that contains in node
        /// </summary>
        public string Text => DriverExtensions.GetElement(this.Container, ItemNameLocator).Text;
    }
}