namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// RiHeadnoteTopicsFacetComponent
    /// </summary>
    public class RiHeadnoteTopicsFacetComponent : EdgeBaseFacetWithAppearingDialogComponent
    {
        private static readonly By HeadnoteFacetLableLocator = By.XPath(".//span[@class = 'SearchFacet-buttonText']");

        /// <summary>
        /// Initializes a new instance of the <see cref="RiHeadnoteTopicsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RiHeadnoteTopicsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }
        
        /// <summary>
        /// Is HeadnoteTopicsFacetLabel Displayed
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsHeadnoteTopicsFacetLabelDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, HeadnoteFacetLableLocator);
    }
}