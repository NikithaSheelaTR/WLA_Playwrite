namespace Framework.Common.UI.Products.TaxnetPro.Pages
{
    using Framework.Common.UI.Products.TaxnetPro.Components.Toolbar;
    using Framework.Common.UI.Raw.WestlawEdge.Enums.FocusHighlighting;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro3 Document page
    /// </summary>
    public class TaxnetProDocumentPage : EdgeCommonDocumentPage
    {
        private const string HighlightLctMask = "//span[contains(@class, 'co_searchTerm') and contains(.,'{0}')]";
        private static readonly By DocumentHeaderTitleLocator = By.XPath("//h1[@id='co_docHeaderTitleLine']");
        /// <summary>
        /// Taxnet Pro Tool bar component
        /// </summary>
        public TaxnetProToolbarComponent TaxnetProToolbar { get; protected set; } = new TaxnetProToolbarComponent();

        /// <summary>
        /// Get highlighted term color by term text
        /// </summary>
        /// <param name="term"> The term text </param>
        /// <returns> Term color </returns>
        public TermColors GetTermColorByText(string term) =>
            this.GetColorTypeByCode(
                DriverExtensions.GetElement(By.XPath(string.Format(HighlightLctMask, term)))
                                .GetCssValue("background-color"));
       
        /// <summary>
        /// This method returns document language attribute
        /// </summary>
        /// <returns></returns>
        public string GetDocumentLanguageAttribute() =>
            DriverExtensions.GetElement(DocumentHeaderTitleLocator).GetAttribute("lang");
    }
}