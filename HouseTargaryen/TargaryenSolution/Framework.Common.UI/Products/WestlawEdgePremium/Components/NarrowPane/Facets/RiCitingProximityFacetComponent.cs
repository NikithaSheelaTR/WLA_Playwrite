namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Citing proximity facet
    /// </summary>
    public class RiCitingProximityFacetComponent : BaseSearchHierarchyFacetComponent
    {
        private static readonly By MoreInfoButtonLocator = By.XPath(".//*[@aria-label = 'Citing Proximity More info']");

        /// <summary>
        /// Initializes a new instance of the <see cref="RiCitingProximityFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RiCitingProximityFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// More info button
        /// </summary>
        public IButton MoreInfoButton => new Button(this.ComponentLocator, MoreInfoButtonLocator);
    }
}
