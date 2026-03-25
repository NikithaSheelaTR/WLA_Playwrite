namespace Framework.Common.UI.Raw.WestlawEdge.Pages.IpTools
{
    using Framework.Common.UI.Products.Shared.Components.IpTools;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo;

    /// <summary>
    /// Indigo References Cited Result Page
    /// </summary>
    public class EdgeReferencesCitedResultPage : CitingReferencesPage
    {
        /// <summary>
        /// Grid Component
        /// </summary>
        public ReferenceCitedGridComponent GridComponent { get; } = new ReferenceCitedGridComponent();

        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public new EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        ///  Gets or sets The toolbar across the top
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; set; } = new EdgeToolbarComponent();
    }
}