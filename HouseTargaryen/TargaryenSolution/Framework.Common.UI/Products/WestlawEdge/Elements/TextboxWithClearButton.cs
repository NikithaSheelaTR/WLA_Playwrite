namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using OpenQA.Selenium;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    /// <summary>
    /// Textbox in Search Within Result facet
    /// </summary>
    public sealed class TextboxWithClearButton : Textbox
    {
        private readonly By xClearButtonLocator;

        /// <inheritdoc />
        public TextboxWithClearButton(IWebElement currentContainer, By xClearButtonLocator = null) : base(currentContainer)
        {
            this.xClearButtonLocator = xClearButtonLocator;
        }

        /// <summary>
        /// X Clear Button
        /// </summary>
        public IButton XClearButton => new Button(this.GetContainer(), this.xClearButtonLocator);

        /// <inheritdoc />
        public override void Clear() => this.XClearButton.Click();

        /// <summary>
        /// Clear textbox
        /// </summary>
        /// <typeparam name="T">T page</typeparam>
        /// <returns>T page</returns>
        public override T Clear<T>() 
        {
            this.XClearButton.Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <inheritdoc />
        public override void SetText(string textToSet) => 
            DriverExtensions.GetElement(this.GetContainer()).SendKeys(textToSet);
    }
}
