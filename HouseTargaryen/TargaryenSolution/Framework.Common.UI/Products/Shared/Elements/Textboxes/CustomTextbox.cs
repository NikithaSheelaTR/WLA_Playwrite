namespace Framework.Common.UI.Products.Shared.Elements.Textboxes
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The custom textbox implementation for Outline Builder. Inherit from standart Textbox class
    /// </summary>
    public class CustomTextbox : Textbox
    {
        /// <inheritdoc />
        public CustomTextbox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public override void SetText(string textToSet)
        {
            var titleLabel = DriverExtensions.GetElement(this.CurrentElementLocator);
            titleLabel.SendKeys(Keys.Control + "a" + Keys.Backspace);
            titleLabel.SendKeys(textToSet);
        }

        /// <inheritdoc />
        public override void Clear() =>
            this.GetContainer().SendKeys(Keys.Control + "a" + Keys.Delete);
    }
}
