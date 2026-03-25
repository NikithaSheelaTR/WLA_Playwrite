namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using System.Windows.Forms;

    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Textbox in Search Within Result facet
    /// </summary>
    public sealed class SearchWithinTextbox : Textbox
    {
        /// <inheritdoc />
        public SearchWithinTextbox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public SearchWithinTextbox(IWebElement outerContainer, params By[] locatorBys)
            : base(outerContainer, locatorBys)
        {
        }

        /// <summary>
        /// Set the text to textbox using Ctrl + V
        /// </summary>
        /// <param name="textToSet">text</param>
        /// <inheritdoc />
        public override T SetText<T>(string textToSet)
        {
            Clipboard.SetText(textToSet);
            this.GetContainer().SendKeys(OpenQA.Selenium.Keys.Control + "v");
            Logger.LogDebug("Enter '" + textToSet + "' to  Search Within textbox.");
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
