namespace Framework.Common.UI.Products.Shared.Elements.Labels.IpLabel
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The Global Ip label.
    /// </summary>
    public sealed class IpLabel : Label
    {
        private readonly string labelName;

        /// <inheritdoc />
        public IpLabel(string labelName, IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
            this.labelName = labelName;
        }

        /// <inheritdoc />
        public override string Text => this.GetContainer(elementWaitTimeout: 500)?.GetText()?.Replace(labelName, string.Empty) ?? string.Empty;
    }
}
