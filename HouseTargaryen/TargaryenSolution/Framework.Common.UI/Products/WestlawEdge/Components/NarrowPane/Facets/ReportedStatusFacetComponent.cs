namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Enums.Facets;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Reported Facet 
    /// </summary>
    public class ReportedStatusFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private EnumPropertyMapper<Reported, WebElementInfo> reportedFacetsMap;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportedStatusFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public ReportedStatusFacetComponent(By componentLocator) : base(componentLocator)
        {
        }
        
        /// <summary>
        /// Gets the ReportedFacetOptions enumeration to WebElementInfo map.
        /// </summary>
        protected EnumPropertyMapper<Reported, WebElementInfo> ReportedFacetsMap
            =>
                this.reportedFacetsMap =
                this.reportedFacetsMap ?? EnumPropertyModelCache.GetMap<Reported, WebElementInfo>();

        /// <summary>
        /// Apply Judge Facet
        /// </summary>
        /// <param name="state"> state  </param>
        /// <param name="reportedOption"> The reported Option. </param>
        /// <typeparam name="T"> T  </typeparam>
        /// <returns> T page s </returns>
        public T ApplyFacet<T>(bool state, Reported reportedOption) where T : ICreatablePageObject
        {
            this.ExpandFacet();
            IEnumerable<FacetOptionItem> sas = this.GetTopLevelFacetItems();
            this.GetTopLevelFacetItems().First(item => item.Title.Equals(this.ReportedFacetsMap[reportedOption].Text)).SetCheckbox(state);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}
