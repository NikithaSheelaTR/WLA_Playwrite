namespace Framework.Common.UI.Products.Shared.Elements.Labels
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The label.
    /// </summary>
    public class Label : BaseWebElement, ILabel
    {
        /// <inheritdoc />
        public Label(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public Label(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public Label(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        public string HiddenText => this.GetContainer().GetHiddenText();
    }
}