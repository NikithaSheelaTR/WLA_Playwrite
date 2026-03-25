namespace Framework.Common.UI.Products.WestlawEdge.Elements
{
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SuggestionCheckBox
    /// </summary>
    public class SuggestionCheckBox : CheckBox
    {
        private static readonly By CheckBoxLocator = By.XPath("./input");

        /// <inheritdoc />
        public SuggestionCheckBox(IWebElement currentContainer) : base(currentContainer)
        {
        }

        /// <inheritdoc />
        public override bool Selected => DriverExtensions.IsCheckboxSelected(this.GetContainer(), CheckBoxLocator);
    }
}