namespace Framework.Common.UI.Products.WestlawEdge.Elements.Judicial
{
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Judicial upload page textbox
    /// </summary>
    public sealed class JudicialUploadPageTextbox : Textbox
    {
        /// <inheritdoc />
        public JudicialUploadPageTextbox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <summary>
        /// Clear textbox using ctrl+a+delete
        /// </summary>
        public override void Clear() => this.GetContainer().ClearUsingButtons();

        /// <summary>
        /// Set the text to textbox
        /// </summary>
        /// <param name="text">text</param>
        public override void SetText(string text)
        {
            this.Clear();
            base.SetText(text);
        }
    }
}