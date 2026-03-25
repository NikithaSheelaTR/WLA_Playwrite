namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using OpenQA.Selenium;

    /// <summary>
    /// Checkbox with 'partially selected' possibility
    /// </summary>
    public sealed class IndeterminateCheckBox : CheckBox, IIndeterminateCheckBox
    {
        /// <inheritdoc />
        public IndeterminateCheckBox(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Is checkbox partially selected
        /// </summary>
        public bool PartiallySelected => this.GetAttribute("indeterminate").Equals("true");
    }
}
