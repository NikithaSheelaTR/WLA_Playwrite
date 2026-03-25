namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult.ContentTypeSearchResultPages
{
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;

    /// <summary>
    /// Edge custom digest search result page
    /// </summary>
    public sealed class EdgeCustomDigestSearchResultPage : BaseEdgeCategorySearchResultPage<CustomDigetResultListItem>
    {
        /// <summary>
        /// Key Number Hierarchy Block
        /// </summary>
        public CustomDigestHierarchyComponent CustomDigestHierarchyBlock { get; } = new CustomDigestHierarchyComponent();
    }
}
