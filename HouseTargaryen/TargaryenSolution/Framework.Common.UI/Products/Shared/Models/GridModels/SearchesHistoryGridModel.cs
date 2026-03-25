namespace Framework.Common.UI.Products.Shared.Models.GridModels
{
    /// <summary>
    /// The searches history table item model.
    /// </summary>
    public class SearchesHistoryGridModel : GridObjectModel
    {
        /// <summary>
        /// Gets or sets the entry content.
        /// </summary>
        public string EntryContent { get; set; }

        /// <summary>
        /// Gets or sets the entry date.
        /// </summary>
        public string EntryDate { get; set; }

        /// <summary>
        /// Gets or sets the entry key number.
        /// </summary>
        public string KeyNumber { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction text
        /// </summary>
        public string Jurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the practice area type.
        /// </summary>
        public string PracticeArea { get; set; }

        /// <summary>
        /// Gets or sets the Proceedings Content
        /// </summary>
        public string ProceedingsContent { get; set; }

        /// <summary>
        /// Gets or sets the search query
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Gets or sets the search result number.
        /// </summary>
        public int SearchResultNumber { get; set; }

        /// <summary>
        /// Gets or sets the search type.
        /// </summary>
        public string SearchType { get; set; }

        /// <summary>
        /// Is Title (Search Query) enabled
        /// </summary>
        public bool IsSearchQueryEnabled { get; set; }

        /// <summary>
        /// Is Info icon (Queries over 600 characters...) displayed
        /// </summary>
        public bool IsInfoIconDisplayed { get; set; }
    }
}
