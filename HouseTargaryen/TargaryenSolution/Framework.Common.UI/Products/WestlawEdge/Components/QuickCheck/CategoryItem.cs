namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Category item
    /// </summary>
    public class CategoryItem: BaseItem
    {
        private static readonly By LabelLocator = By.XPath(".//label");
        private static readonly By ButtonLocator = By.XPath(".//input");

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryItem"/> class. 
        /// </summary>
        /// <param name="container">container</param>
        public CategoryItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public IButton Button => new Button(this.Container, ButtonLocator);

        /// <summary>
        /// Label
        /// </summary>
        public ILabel Label => new Label(this.Container, LabelLocator);
    }
}
