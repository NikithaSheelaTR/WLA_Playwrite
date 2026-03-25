namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Annotated Documents Facet
    /// Locations
    ///     - Search results Page
    ///     - Folder Page
    ///     - History Page
    /// </summary>
    public class AnnotatedDocumentsFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_annotated");

        private static readonly By AnnotatedFacetIconLocator = By.XPath(".//*[contains(@class,'co_facet_icon_annotated')]");

        private EnumPropertyMapper<AnnotatedDocumentsFacetOptions, WebElementInfo> annotatedFacetsMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the AnnotatedDocumentsFacetOptions enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<AnnotatedDocumentsFacetOptions, WebElementInfo> AnnotatedFacetsMap
            => this.annotatedFacetsMap = this.annotatedFacetsMap ?? EnumPropertyModelCache.GetMap<AnnotatedDocumentsFacetOptions, WebElementInfo>();

        /// <summary>
        /// Click on the Annotated Facet Icon
        /// Click on the Annotated Documents Icon is equal to check Highlighted and Notes options
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// /// <returns>The new instance of T page.</returns>
        public T ClickAnnotatedFacetIcon<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), AnnotatedFacetIconLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// IsCheckboxSelected
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(AnnotatedDocumentsFacetOptions option)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.Id(this.AnnotatedFacetsMap[option].Id)));

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="option">The option.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(AnnotatedDocumentsFacetOptions option, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.Id(this.AnnotatedFacetsMap[option].Id)), setTo);
    }
}