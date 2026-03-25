namespace Framework.Common.UI.Products.WestLawNext.Models.HistoryItem
{
    /// <summary>
    /// Other History Item Model for WLN Related Info
    /// </summary>
    public class OtherHistoryItemModel : HistoryItemModel
    {
        /// <summary>
        /// CheckBox
        /// </summary>
        public bool CheckBox { get; set; }

        /// <summary>
        /// CourtLine
        /// </summary>
        public string CourtLine { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        public int Rank { get; set; }
    }
}