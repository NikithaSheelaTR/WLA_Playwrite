namespace Framework.Common.UI.Products.WestlawEdge.Elements.QuickCheck
{
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search by case text box
    /// </summary>
    public sealed class SearchByCaseTextbox: Textbox
    {
        /// <inheritdoc />
        public SearchByCaseTextbox(params By[] locatorBys) : base(locatorBys)
        {
        }

        /// <inheritdoc />
        public override T SetText<T>(string textToSet)
        {
            this.Clear();
            this.GetContainer().SendKeysSlow(textToSet);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}