namespace Framework.Common.UI.Products.GovernmentWeblinks.Pages.Standard
{
    using Framework.Common.UI.Products.GovernmentWeblinks.Components;
    using Framework.Common.UI.Products.GovernmentWeblinks.Pages.Search;

    /// <summary>
    /// Standard search results page for weblinks product
    /// </summary>
    public class StandardSearchResultsPage : SearchResultsPage
    {
        /// <summary>
        /// Header component
        /// </summary>
        public new WeblinksStandardHeaderComponent Header { get; } = new WeblinksStandardHeaderComponent();
    }
}
