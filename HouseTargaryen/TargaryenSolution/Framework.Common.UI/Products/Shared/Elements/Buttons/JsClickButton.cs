namespace Framework.Common.UI.Products.Shared.Elements.Buttons
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils;
    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The button with JS click.
    /// </summary>
    public sealed class JsClickButton : Button
    {
       /// <summary>
       /// Creta
       /// </summary>
       /// <param name="locatorBys"></param>
        public JsClickButton(params By[] locatorBys) : base(locatorBys)
        {
        }
        
        /// <summary>
        /// JS click
        /// </summary>
        /// <param name="outerContainer"></param>
        /// <param name="locatorBys"></param>
        public JsClickButton(IWebElement outerContainer, params By[] locatorBys) : base(outerContainer, locatorBys)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// JS click.
        /// </summary>
        public override void Click()
        {
            string buttonName = this.Text;
            this.GetContainer().JavascriptClick();
            Logger.LogDebug("Click button:" + buttonName);
        }
    }
}