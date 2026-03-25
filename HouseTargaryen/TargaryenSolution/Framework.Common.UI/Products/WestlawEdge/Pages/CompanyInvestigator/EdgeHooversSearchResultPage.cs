namespace Framework.Common.UI.Products.WestlawEdge.Pages.CompanyInvestigator
{
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Products.WestlawEdge.Components;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;

    /// <summary>
    /// Page to search a company on Hoover AST search page
    /// </summary>
    public class EdgeHooversSearchResultPage : CategorySearchResultPage
    {
        /// <summary>
        /// New Narrow Pane Component (left side of search results page)
        /// </summary>
        public new EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// header
        /// </summary>
        public new EdgeHeaderComponent Header { get; } = new EdgeHeaderComponent();

        /// <summary>
        /// toolbar
        /// </summary>
        public new EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();
    }
}
