namespace Framework.Common.UI.Products.WestlawEdge.Components.QuickCheck
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Items;

    using OpenQA.Selenium;

    /// <summary>
    /// Item on the doc heading dialog
    /// </summary>
    public sealed class DocumentHeadingItem : BaseItem
    {
        private static readonly By ItemCheckboxLocator = By.XPath(".//input");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentHeadingItem"/> class. 
        /// Doc heading item
        /// </summary>
        /// <param name="container"> The container </param>
        public DocumentHeadingItem(IWebElement container) : base(container)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, ItemCheckboxLocator);
    }
}