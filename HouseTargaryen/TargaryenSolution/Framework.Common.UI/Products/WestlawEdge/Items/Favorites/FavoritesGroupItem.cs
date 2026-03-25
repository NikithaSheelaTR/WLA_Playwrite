namespace Framework.Common.UI.Products.WestlawEdge.Items.Favorites
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Items;
    using OpenQA.Selenium;

    /// <summary>
    /// Favorites group item
    /// </summary>
    public class FavoritesGroupItem : BaseItem
    {
        private static  readonly By GroupCheckBoxLocator = By.XPath(".//input");

        private static readonly By GroupLabelLocator = By.XPath(".//label");

        /// <summary>
        /// Initializes a new instance of the <see cref="FavoritesGroupItem"/> class. 
        /// </summary>
        /// <param name="container">
        /// </param>
        public FavoritesGroupItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Group check Box
        /// </summary>
        public ICheckBox GroupCheckBox => new CheckBox(this.Container, GroupCheckBoxLocator);

        /// <summary>
        /// Group label
        /// </summary>
        public ILabel GroupLabel => new Label(this.Container, GroupLabelLocator);
    }
}
