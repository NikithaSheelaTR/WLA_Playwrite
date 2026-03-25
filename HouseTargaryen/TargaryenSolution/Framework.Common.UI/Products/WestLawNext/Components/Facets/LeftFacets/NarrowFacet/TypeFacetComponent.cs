namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.Ui.Products.WestlawNext.Enums.Facets;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// TypeFacetComponent
    /// </summary>
    public class TypeFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_Type");

        private EnumPropertyMapper<TypeFacet, WebElementInfo> typeFacetMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the TypeFacet enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<TypeFacet, WebElementInfo> TypeFacetMap
            => this.typeFacetMap = this.typeFacetMap ?? EnumPropertyModelCache.GetMap<TypeFacet, WebElementInfo>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="typeFacet">typeFacet to apply</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(TypeFacet typeFacet, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.TypeFacetMap[typeFacet].LocatorString)), setTo);

        /// <summary>
        /// Get count from reported checkbox
        /// </summary>
        /// <param name="typeFacet">The typeFacet.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetCheckboxCount(TypeFacet typeFacet)
            => this.GetCheckboxCount(this.TypeFacetMap[typeFacet].LocatorMask);
    }
}