namespace Framework.Common.UI.Products.WestLawNextMobile.Models
{
    using Framework.Common.UI.Products.Shared.Enums.Content;

    /// <summary>
    /// Represents a search result
    /// </summary>
    public class SearchResultModel
    {
        /// <summary>
        /// Content type
        /// </summary>
        public ContentType ContentType { get; set; }

        /// <summary>
        /// GUID of the search result
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Title as it appears on the page
        /// </summary>
        public string Title { get; set; }
    }
}