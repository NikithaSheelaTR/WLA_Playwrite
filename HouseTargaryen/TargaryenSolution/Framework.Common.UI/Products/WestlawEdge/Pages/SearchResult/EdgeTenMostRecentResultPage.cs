namespace Framework.Common.UI.Products.WestlawEdge.Pages.SearchResult
{
    using Framework.Common.UI.Raw.WestlawEdge.Items.DocumentListItems;

    /// <summary>
    /// Edge ten most recent result page
    /// </summary>
    public sealed class EdgeTenMostRecentResultPage : BaseEdgeSearchResultPageWithResultList<EdgeResultListItem>
    {
        /// <summary>
        /// Get page's heading (page heading is displayed under global search bar, e.g. Find Results)
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>        
        public override string GetPageHeadingText() => this.GetBrowsePageTitle();
    }
}
