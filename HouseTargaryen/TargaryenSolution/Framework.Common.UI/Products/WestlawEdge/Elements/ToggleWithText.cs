namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Products.Shared.Elements.Toggles;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Toggle with text
    /// </summary>
    public class ToggleWithText : Toggle
    {
        private readonly string toggleText;

        /// <summary>
        /// Initializes a new instance of the <see cref="ToggleWithText"/> class.
        /// </summary>
        /// <param name="currentContainer"></param>
        /// <param name="toggleLocator"></param>
        /// <param name="toggleText"></param>
        public ToggleWithText(IWebElement currentContainer, By toggleLocator, string toggleText = "")
            : base(currentContainer, toggleLocator)
        {
            this.toggleText = toggleText;
        }

        /// <inheritdoc />
        public override bool State =>
                 DriverExtensions.GetElement(this.GetContainer()).Text.Equals(toggleText);
    }
}
