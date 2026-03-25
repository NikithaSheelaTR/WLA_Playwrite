namespace Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.TaxnetPro.Enums.Facets;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Legislation Facet component
    /// </summary>
    public class LegislationFacetComponent : EdgeBaseFacetComponent
    {
        private static readonly By ContainerLocator = By.XPath("facet_div_legislation");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<LegislationFacetType, WebElementInfo> legislationFacetType;

        /// <summary>
        /// Gets the content type enumeration to ContentTypeInfo map.
        /// </summary>
        protected EnumPropertyMapper<LegislationFacetType, WebElementInfo> LegislationType =>
            this.legislationFacetType = this.legislationFacetType
                                        ?? EnumPropertyModelCache.GetMap<LegislationFacetType, WebElementInfo>(
                                            string.Empty,
                                            @"Resources/EnumPropertyMaps/TaxnetPro");

        /// <summary>
        /// Apply a Legislation facet
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="legislationType">Legislation type to apply</param>
        /// <param name="state">state</param>
        /// <returns> a new search result page object </returns>
        public T ApplyFacet<T>(LegislationFacetType legislationType, bool state)
            where T : ICreatablePageObject
        {
            DriverExtensions.SetCheckbox(By.Id(this.LegislationType[legislationType].Id), state);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Checks if legislation facet is displayed
        /// </summary>
        /// <param name="legislationType">Legislation to check</param>
        /// <returns>true if displayed</returns>
        public bool IsLegislationFacetDisplayed(LegislationFacetType legislationType) =>
            DriverExtensions.IsDisplayed(By.Id(LegislationType[legislationType].Id));
    }
}