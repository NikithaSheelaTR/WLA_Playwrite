namespace Framework.Common.UI.Products.WestLawNextCanada
{
    using Framework.Common.UI.Interfaces.Elements;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Items;

    /// <summary>
    /// Trillium Result Features
    /// </summary>
    public class TrilliumResultListItem : BaseItem
    {
        private static readonly By CheckBoxLocator = By.XPath(".//input[@type='checkbox']");

        /// <summary>
        /// Initializes a new instance of the <see cref="TrilliumResultListItem"/> class. 
        /// </summary>
        /// <param name="containerElement">The container Element.</param>
        public TrilliumResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Checkbox
        /// </summary>
        public ICheckBox Checkbox => new CheckBox(this.Container, CheckBoxLocator);
    }
}
