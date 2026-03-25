namespace Framework.Common.UI.Products.Shared.Elements.Buttons
{
    using OpenQA.Selenium;

    /// <summary>
    /// Custom button with Enabled verification by class name.
    /// </summary>
    public sealed class CustomEdgeButton : Button
    {
        /// <inheritdoc />
        public CustomEdgeButton(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public CustomEdgeButton(params By[] locatorBys)
            : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public override bool Enabled => !this.GetAttribute("class").Contains("co_disabled");
    }
}
