namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RiReferencedInNodFacetComponent
    /// </summary>
    public class RiReferencedInNodFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string ReferencedInNodOptionLctMask = "//span[contains(@class, 'SearchFacet-labelText') and contains(text(),'{0}')]";
       
        /// <summary>
        /// Initializes a new instance of the <see cref="RiReferencedInNodFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RiReferencedInNodFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// IsOptionDisplayedByName
        /// </summary>
        /// <param name="option">
        /// The option.
        /// </param>
        /// <returns>
        /// Count
        /// </returns>
        public bool IsOptionDisplayedByName(string option)
        {
            this.ExpandFacet();
            return DriverExtensions.IsDisplayed(By.XPath(string.Format(ReferencedInNodOptionLctMask, option)), 5);
        } 
    }
}