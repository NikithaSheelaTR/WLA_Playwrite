namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Annotated Documents Facet Component
    /// </summary>
    public class AnnotatedDocumentsFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string AnnotatedDocumentsFacetOptionLctMask = "//span[@class='SearchFacet-labelText' and text()='{0}']";

        private EnumPropertyMapper<AnnotatedDocumentsFacetOptions, WebElementInfo> anatFacetsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="AnnotatedDocumentsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public AnnotatedDocumentsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Gets the AnnotatedDocumentsFacetOptions enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AnnotatedDocumentsFacetOptions, WebElementInfo> AnatFacetsMap => this.anatFacetsMap = this.anatFacetsMap ?? EnumPropertyModelCache.GetMap<AnnotatedDocumentsFacetOptions, WebElementInfo>();
        
        /// <summary>
        /// Set Annotated facet option Multiple filter mode
        /// </summary>
        /// <param name="option"> Annotated Facet Option </param>
        /// <param name="optionState"> Option state </param>
        /// <returns> The <see cref="AnnotatedDocumentsFacetComponent"/>. </returns>
        public AnnotatedDocumentsFacetComponent SetAnnotatedOptionMultipleFilterMode(
            AnnotatedDocumentsFacetOptions option,
            bool optionState)
        {
            this.SetAnnotatedOption(option, optionState);
            return this;
        }

        /// <summary>
        /// Set Annotated facet option Single filter mode
        /// </summary>
        /// <typeparam name="T"> Type of the page </typeparam>
        /// <param name="option"> Annotated Facet Option </param>
        /// <param name="optionState"> Option state </param>
        /// <returns> New Page with filter applied </returns>
        public T SetAnnotatedOptionSingleFilterMode<T>(AnnotatedDocumentsFacetOptions option, bool optionState)
            where T : ICreatablePageObject
        {
            this.SetAnnotatedOption(option, optionState);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Build locator using part of element id
        /// Required common method with the same options for core utilities
        /// </summary>
        /// <param name="option">option</param>
        /// <returns>string</returns>
        private string GetLocatorByOptionType(AnnotatedDocumentsFacetOptions option) =>
            string.Format(AnnotatedDocumentsFacetOptionLctMask, this.AnatFacetsMap[option].Text);

        /// <summary>
        /// Set state for annotation facet check box
        /// </summary>
        /// <param name="option"> Annotated Facet Option </param>
        /// <param name="optionState"> Option state </param>
        private void SetAnnotatedOption(AnnotatedDocumentsFacetOptions option, bool optionState)
        {
            this.ExpandFacet();
            DriverExtensions.SetCheckbox(optionState, By.XPath(this.GetLocatorByOptionType(option)));
            DriverExtensions.WaitForJavaScript();
        }
    }
}
