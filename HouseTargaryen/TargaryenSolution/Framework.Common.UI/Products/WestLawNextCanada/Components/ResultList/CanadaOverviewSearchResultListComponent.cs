namespace Framework.Common.UI.Products.WestLawNextCanada.Components.ResultList
{
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.Shared.Items;

    /// <summary>
    /// Canada Overview Search Result List
    /// </summary>
    public class CanadaOverviewSearchResultListComponent : OverviewSearchResultList
    {
        private static readonly By SearchResultsElementLocator = By.XPath("//li[starts-with(@id, 'cobalt_search_')]");
        private const string SearchResultsByContentTypeLctMask = "//li[starts-with(@id, 'cobalt_search_results_can_{0}')]";

        /// <summary>
        /// Get item by content type and index
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="index"></param>
        /// <returns>text of the document</returns>
        public new ResultListItem GetItem(ContentType contentType, int index) =>
           this.GetItems(contentType)[index];

        /// <summary>
        /// Get items by content type
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public new ItemsCollection<ResultListItem> GetItems(ContentType contentType) =>
            new ItemsCollection<ResultListItem>(SearchResultsElementLocator, By.XPath(string.Format(SearchResultsByContentTypeLctMask,this.ContentTypeMap[contentType].SearchResultsLocatorString)));
    }
}
