namespace Framework.Common.UI.Raw.WestlawEdge.Pages.RelatedInfo
{
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.Document.RI;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Indigo Tab Page
    /// </summary>
    public class EdgeTabPage : TabPage
    {
        private static readonly By TabContainerLocator = By.Id("co_contentColumn");

        private static readonly string RelatedInfoGridLocator = "//table[contains(@class,'co_detailsTable')]";

        /// <summary> Indigo Document Fixed Header Component </summary>
        public new EdgeDocumentFixedHeaderComponent FixedHeader { get; } = new EdgeDocumentFixedHeaderComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public new EdgeLegacyResultListComponent ResultList => new EdgeLegacyResultListComponent(DriverExtensions.GetElement(TabContainerLocator));

        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();

        /// <summary>
        /// Gets the header.
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// New Ri Narrow Pane
        /// </summary>
        public EdgeRiNarrowPaneComponent RiNarrowPane { get; } = new EdgeRiNarrowPaneComponent();

        /// <summary>
        /// New Ri Narrow Pane affects only the following Ri tabs in Edge:
        /// Citing References,
        /// Professional References, 
        /// Court Docs,
        /// Medical References, 
        /// Filings, 
        /// References Cites,
        /// Cited With
        /// </summary>
        public NewEdgeRiNarrowTabPanel NewEdgeRiNarrowPane { get; } = new NewEdgeRiNarrowTabPanel();

        /// <summary>
        /// Reference grid
        /// </summary>
        public EdgeReferenceGridComponent ReferenceGrid { get; } = new EdgeReferenceGridComponent(RelatedInfoGridLocator);
    }
}
