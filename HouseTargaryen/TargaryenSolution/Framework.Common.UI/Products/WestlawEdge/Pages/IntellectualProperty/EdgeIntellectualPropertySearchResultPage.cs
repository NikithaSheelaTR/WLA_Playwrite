namespace Framework.Common.UI.Products.WestlawEdge.Pages.IntellectualProperty
{
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;

    /// <summary>
    /// Common Search Result Page for Intellectual Property content
    /// </summary>
    /// <typeparam name="TItem">ResultListItem</typeparam>
    public class EdgeIntellectualPropertySearchResultPage<TItem> : IntellectualPropertySearchResultPage<TItem>
        where TItem : ResultListItem
    {
        /// <summary>
        /// Edge Ip Narrow pane component
        /// </summary>
        public new  EdgeIpNarrowPaneComponent IpNarrowPane { get; } = new EdgeIpNarrowPaneComponent();
    }
}
