namespace Framework.Common.UI.Products.Shared.Models.GridModels
{
    /// <summary>
    /// It is related to the SearchesRecentHistoryItem
    /// </summary>
    public class SearchesRecentHistoryModel
    {
        /// <summary>
        /// Search query text
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Is Info icon (Queries over 600 characters...) displayed
        /// </summary>
        public bool IsInfoIconDisplayed { get; set; }

        /// <summary>
        /// Is hover message displayed when user hover over info icon
        /// </summary>
        public bool IsHoverMessageDisplayedIfInfoIconExists { get; set; }
    }
}