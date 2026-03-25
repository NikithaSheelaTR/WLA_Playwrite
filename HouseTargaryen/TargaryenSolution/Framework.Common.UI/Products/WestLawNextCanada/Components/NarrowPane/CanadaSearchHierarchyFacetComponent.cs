namespace Framework.Common.UI.Products.WestLawNextCanada.Components.NarrowPane
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Canada Search Hierarchy Facet Component
    /// </summary>
    public class CanadaSearchHierarchyFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private static readonly By CheckedOptionLocator = By.XPath(".//input[@checked]/following-sibling::span[@class='SearchFacet-labelText']//b");

        /// <summary>
        /// Initializes a new instance of the <see cref="CanadaSearchHierarchyFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public CanadaSearchHierarchyFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Gets name of selected options
        /// </summary>
        /// <returns>selected options names</returns>
        public new List<string> GetSelectedOptions() =>
            DriverExtensions.GetElements(this.ComponentLocator, CheckedOptionLocator).Select(el => el.Text).ToList();
    }
}
