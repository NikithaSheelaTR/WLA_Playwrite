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
    /// Viewed in the last 30 days Facet Component
    /// </summary>
    public class ViewedFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("co_facetHeaderpreviouslyViewed");

        private EnumPropertyMapper<ViewedFacetCheckboxes, WebElementInfo> viewedFacetCheckboxesMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// ViewedFacetCheckboxesMap
        /// </summary>
        protected EnumPropertyMapper<ViewedFacetCheckboxes, WebElementInfo> ViewedFacetCheckboxesMap
            => this.viewedFacetCheckboxesMap = this.viewedFacetCheckboxesMap ?? EnumPropertyModelCache.GetMap<ViewedFacetCheckboxes, WebElementInfo>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="checkbox">The checkbox.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(ViewedFacetCheckboxes checkbox, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ViewedFacetCheckboxesMap[checkbox].LocatorString)), setTo);

        /// <summary>
        /// Is Checkbox Selected
        /// </summary>
        /// <param name="checkbox">The checkbox.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsCheckboxSelected(ViewedFacetCheckboxes checkbox)
            => this.IsCheckboxSelected(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.ViewedFacetCheckboxesMap[checkbox].LocatorString)));
    }
}