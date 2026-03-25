namespace Framework.Common.UI.Products.WestlawEdgePremium.Components.NarrowPane
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets;
    using OpenQA.Selenium;

    /// <summary>
    /// Precision Search Within Facet
    /// </summary>
    public class PrecisionSearchWithinFacetComponent : EdgeSearchWithinFacetComponent
    {
        private static readonly By AthesnSearchWithinContainerLocator = By.XPath("//*[contains(@class, 'SearchFacetSearchWithin-keyword')]");
        private static readonly By DocumentsRadioButtonLocator = By.XPath(".//input[contains(@id, 'documentsSearch')]");
        private static readonly By MaterialFactsRadioButtonLocator = By.XPath(".//input[contains(@id, 'materialFactsSearch')]");

        /// <summary>
        /// Documents radiobutton
        /// </summary>
        public IRadiobutton DocumentsRadiobutton => new Radiobutton(this.ComponentLocator, DocumentsRadioButtonLocator);

        /// <summary>
        ///  Material facts radiobutton
        /// </summary>
        public IRadiobutton MaterialFactsRadiobutton => new Radiobutton(this.ComponentLocator, MaterialFactsRadioButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => AthesnSearchWithinContainerLocator;
    }
}
