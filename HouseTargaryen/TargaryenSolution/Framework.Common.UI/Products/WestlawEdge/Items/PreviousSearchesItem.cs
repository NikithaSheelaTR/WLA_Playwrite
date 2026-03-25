namespace Framework.Common.UI.Products.WestlawEdge.Items
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Previous searches item
    /// </summary>
    public class PreviousSearchesItem : BaseItem
    {
        private const string TooltipLctMask = "//div[@class = 'a11yTooltip-content' and @id = '{0}']";

        private static readonly By TermLocator = By.XPath(".//*[contains(@class,'searchTermItem co_inline')]");
        private static readonly By DeleteButtonLocator = By.XPath(".//button[text() = 'Delete']");
        private static readonly By UndoDeleteButtonLocator = By.XPath(".//button[text() = 'Undo delete']");
        private static readonly By CrossedOutTermLocator = By.XPath(".//del[contains(@class,'searchTermItem co_inline')]");

        /// <summary>
        /// Constructor
        /// Initializes a new instance of the <see cref="PreviousSearchesItem"/> class. 
        /// </summary>
        /// <param name="container"></param>
        public PreviousSearchesItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Previous Search Item Label
        /// </summary>
        public ILabel PreviousSearchItemLabel => new Label(this.Container);

        /// <summary>
        /// Term
        /// </summary>
        public ILabel Term => new Label(this.Container, TermLocator);

        /// <summary>
        /// Crossed out label
        /// </summary>
        public ILabel CrossedOutTerm => new Label(this.Container, CrossedOutTermLocator);
               
        /// <summary>
        /// Tooltip for Delete button
        /// </summary>
        public ILabel DeleteTooltip => new Label(By.XPath(string.Format(TooltipLctMask, DeleteButton.GetAttribute("aria-describedby"))));

        /// <summary>
        /// Tooltip for Undo delete button
        /// </summary>
        public ILabel UndoDeleteTooltip => new Label(By.XPath(string.Format(TooltipLctMask, UndoDeleteButton.GetAttribute("aria-describedby"))));

        /// <summary>
        /// Delete button
        /// </summary>
        public IButton DeleteButton => new Button(this.Container, DeleteButtonLocator);

        /// <summary>
        /// Undo delete button
        /// </summary>
        public IButton UndoDeleteButton => new Button(this.Container, UndoDeleteButtonLocator);
    }
}
