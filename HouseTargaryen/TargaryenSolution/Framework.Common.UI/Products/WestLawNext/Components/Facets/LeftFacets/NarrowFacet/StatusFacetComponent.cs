namespace Framework.Common.UI.Products.WestLawNext.Components.Facets.LeftFacets.NarrowFacet
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestLawNext.Enums.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// StatusFacetComponent
    /// </summary>
    public class StatusFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_legislationStatus");

        private EnumPropertyMapper<Status, WebElementInfo> statusMap;

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Gets the jurisdiction enumeration to JurisdictionInfo map.
        /// </summary>
        protected EnumPropertyMapper<Status, WebElementInfo> StatusMap
            => this.statusMap = this.statusMap ?? EnumPropertyModelCache.GetMap<Status, WebElementInfo>();

        /// <summary>
        /// Apply the specified checkbox
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="status">The status.</param>
        /// <param name="setTo">setTo</param>
        /// <returns>The new instance of T page.</returns>
        public T SetCheckbox<T>(Status status, bool setTo = true) where T : ICreatablePageObject
            => this.SetCheckbox<T>(DriverExtensions.GetElement(this.ComponentLocator, By.XPath(this.StatusMap[status].LocatorString)), setTo);
    }
}