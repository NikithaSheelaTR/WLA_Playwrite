namespace Framework.Common.UI.Products.WestlawEdge.Elements.Judicial
{
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// JudicialCheckBox
    /// </summary>
    public class JudicialCheckBox : CheckBox
    {
        /// <inheritdoc />
        public JudicialCheckBox(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public override string Text => this.GetParentElement()?.GetText() ?? string.Empty;
    }
}