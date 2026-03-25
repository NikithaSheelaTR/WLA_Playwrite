namespace Framework.Common.UI.Products.WestlawEdge.Components.Judicial.Facets
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using Framework.Common.UI.Raw.WestlawEdge.Items.Facets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Party documents facet component
    /// </summary>
    public class PartyDocumentsFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private const string DocsByPartyLctMask = ".//span[contains(@class,'SearchFacet-labelText') and text()='{0}']//ancestor::div[@class='SearchFacet-listItem']//following-sibling::div/div";

        /// <summary>
        /// Initializes a new instance of the <see cref="PartyDocumentsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The component locator.
        /// </param>
        public PartyDocumentsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Gets document names for party
        /// </summary>
        /// <param name="partyName">party name</param>
        /// <returns>document names for party</returns>
        public List<FacetOptionItem> GetDocumentItemsByParty(string partyName)
        {
            this.ExpandFacet();
            return DriverExtensions
                   .GetElements(this.ComponentLocator, By.XPath(string.Format(DocsByPartyLctMask, partyName)))
                   .Select(item => new FacetOptionItem(item)).ToList();
        }

        /// <summary>
        /// Sets the checkbox
        /// </summary>
        /// <param name="partyName">party name</param>
        /// <param name="documentTitle">document title</param>
        /// <param name="state">checkbox state</param>
        public void SetCheckbox(string partyName, string documentTitle, bool state = true) => 
            this.GetDocumentItemsByParty(partyName).First(item => item.GetTooltip().Equals(documentTitle)).SetCheckbox(state);
    }
}
