namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacetusing
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;  
    using OpenQA.Selenium;
    using System.Collections.Generic;

    /// <summary>
    /// CourtLevel Facet Component
    /// </summary>
    public class CourtLevelFacetComponent : BaseFacetCheckboxComponent
    {
        private static readonly By ContainerLocator = By.Id("facet_div_wlncCourtLevel");
        private static readonly By ListOfChildLabelsLocator = By.XPath(".//label");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Child Labels List of the facet
        /// </summary>
        /// <returns>The list of labels name</returns>
        public IReadOnlyCollection<ILabel> ChildLabelsList => new ElementsCollection<Label>(this.ComponentLocator, ListOfChildLabelsLocator);
    }
}
