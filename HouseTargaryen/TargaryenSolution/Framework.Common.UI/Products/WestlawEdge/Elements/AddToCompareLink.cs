namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using OpenQA.Selenium;

    /// <summary>
    /// Add to Compare link
    /// </summary>
    public sealed class AddToCompareLink : Link
    {
        /// <inheritdoc />
        public AddToCompareLink(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Is link enabled
        /// </summary>
        /// <inheritdoc />
        public override bool Enabled => !this.GetAttribute("class").Contains("co_disabled");
    }
}