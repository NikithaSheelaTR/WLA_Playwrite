namespace Framework.Common.UI.Products.WestLawNext.Models.HistoryItem
{
    /// <summary>
    /// Negative Treatment Item Model for WLN Related Info
    /// </summary>
    public class NegativeTreatmentItemModel : HistoryItemModel
    {
        /// <summary>
        /// Badge
        /// </summary>
        public string Badge { get; set; }

        /// <summary>
        /// CheckBox
        /// </summary>
        public bool CheckBox { get; set; }

        /// <summary>
        /// CourtLine
        /// </summary>
        public string CourtLine { get; set; }
    }
}