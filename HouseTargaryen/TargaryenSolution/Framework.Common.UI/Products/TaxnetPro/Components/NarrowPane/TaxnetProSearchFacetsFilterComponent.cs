namespace Framework.Common.UI.Products.TaxnetPro.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Elements.Judicial;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Taxnet Pro component with facets for search
    /// </summary>
    public class TaxnetProSearchFacetsFilterComponent : EdgeSearchFacetsFilterComponent
    {
        private static readonly By ApplyButtonLocator = By.Id("co_multifacet_selector_1_applyFacetFilter");

        /// <summary>
        /// Document Language Facet
        /// </summary>
        public DocumentLanguageFacetComponent DocumentLanguageFacet => new DocumentLanguageFacetComponent();

        /// <summary>
        /// Areas of Interest Facet component
        /// </summary>
        public AreasOfInterestFacetComponent AreasOfInterestFacet => new AreasOfInterestFacetComponent();

        /// <summary>
        /// Legislation Facet
        /// </summary>
        public LegislationFacetComponent LegislationFacet => new LegislationFacetComponent();

        /// <summary>
        /// Apply button
        /// </summary>
        public IButton ApplyButton => new CustomClickButton(this.ComponentLocator, ApplyButtonLocator);

        /// <summary>
        /// Click on Apply filter button if displayed
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns>Creates Page instance</returns>
        public T ClickApplyFilterButton<T>()
            where T : ICreatablePageObject
        {
            if (this.ApplyButton.Displayed)
            {
                return this.ApplyButton.Click<T>();
            }

            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}