namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgeFilingsPage
    /// </summary>
    public class EdgeFilingsPage : FilingsPage
    {
        private static readonly By FacetPaneElementLocator = By.Id("co_website_searchFacets");

        /// <summary>
        /// New Ri Narrow Pane affects only the following Ri tabs in Edge:
        /// Citing References,
        /// Professional References, 
        /// Court Docs,
        /// Medical References, 
        /// Filings, 
        /// References Cites
        /// </summary>
        public NewEdgeRiNarrowTabPanel NewEdgeRiNarrowPane { get; } = new NewEdgeRiNarrowTabPanel();

        /// <summary>
        /// Is Facet Pane Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public new bool IsFacetPaneDisplayed() => DriverExtensions.IsDisplayed(FacetPaneElementLocator);
    }
}
