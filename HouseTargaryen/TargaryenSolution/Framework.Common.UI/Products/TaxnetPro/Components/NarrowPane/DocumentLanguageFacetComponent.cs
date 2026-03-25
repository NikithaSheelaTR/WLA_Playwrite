namespace Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;
    using Framework.Core.Utils.Extensions;
    using OpenQA.Selenium;

    /// <summary>
    /// Document Language Facet Component
    /// </summary>
    public class DocumentLanguageFacetComponent : EdgeBaseFacetComponent
    {
        private const string DocumentLanguageCountLctMask = "//div[@id='facet_div_Language']//li[.//*[contains(text(),'{0}')]]/span";

        private static readonly By ContainerLocator = By.XPath("//div[@id='facet_div_language']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<DocumentLanguageType, WebElementInfo> documentLanguageType;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<DocumentLanguageType, WebElementInfo> DocumentLanguageType =>
            this.documentLanguageType = this.documentLanguageType ?? EnumPropertyModelCache.GetMap<DocumentLanguageType, WebElementInfo>(
                                            string.Empty,
                                            @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Apply a Document Language facet
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="docLanguageType"> Document Language type to apply </param>
        /// <param name="state">state</param>
        /// <returns> a new search result page object </returns>
        public T ApplyFacet<T>(DocumentLanguageType docLanguageType, bool state)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(By.XPath(this.DocumentLanguageType[docLanguageType].LocatorString), state);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if document language is displayed
        /// </summary>
        /// <param name="documentLanguage">Document language to check</param>
        /// <returns>true if displayed</returns>
        public bool IsDocumentLanguageFacetDisplayed(DocumentLanguageType documentLanguage) =>
            DriverExtensions.IsDisplayed(By.XPath(DocumentLanguageType[documentLanguage].LocatorString));

        /// <summary>
        /// Get the number of results for a given language
        /// </summary>
        /// <param name="documentLanguage">Document language to check</param>
        /// <returns> Number of results </returns>
        public int GetDocumentLanguageCount(DocumentLanguageType documentLanguage)
            => this.GetDocumentLanguageCount(this.DocumentLanguageType[documentLanguage].Text);

        /// <summary>
        /// Get the number of results for a given language
        /// </summary>
        /// <param name="documentLanguage">Document language to check</param>
        /// <returns> Number of results </returns>
        public int GetDocumentLanguageCount(string documentLanguage)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(DocumentLanguageCountLctMask, documentLanguage)))
                .Text.ConvertCountToInt();
    }
}