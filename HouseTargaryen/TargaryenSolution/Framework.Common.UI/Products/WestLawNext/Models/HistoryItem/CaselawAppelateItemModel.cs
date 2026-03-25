namespace Framework.Common.UI.Products.WestLawNext.Models.HistoryItem
{
    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// Caselaw Appelate Item Model  for WLN Related Info
    /// </summary>
    public class CaselawAppelateItemModel : HistoryItemModel
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
        /// Is Key Cited
        /// </summary>
        public bool KeyCited { get; set; }

        /// <summary>
        /// Letter Icon
        /// </summary>
        public bool LetterIcon { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        public int Rank { get; set; }
    }
}