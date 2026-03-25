namespace Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.Facets
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Ri Notes Of Decisions Topics FacetComponent
    /// </summary>
    public class RiNotesOfDecisionsTopicsFacetComponent : EdgeBaseFacetWithAppearingDialogComponent
    {
        private static readonly By NotesOfDecisionsFacetLableLocator = By.XPath(".//span[@class = 'SearchFacet-buttonText']");

        /// <summary>
        /// Initializes a new instance of the <see cref="RiNotesOfDecisionsTopicsFacetComponent"/> class.
        /// </summary>
        /// <param name="componentLocator">
        /// The container locator.
        /// </param>
        public RiNotesOfDecisionsTopicsFacetComponent(By componentLocator) : base(componentLocator)
        {
        }

        /// <summary>
        /// Is HeadnoteTopicsFacetLabel Displayed
        /// </summary>
        /// <returns>True if displayed</returns>
        public bool IsNotesOfDecisionsFacetLabelDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, NotesOfDecisionsFacetLableLocator);
    }
}