namespace Framework.Common.UI.Products.WestLawNext.Models.HistoryItem
{
    using Framework.Core.CommonTypes.Enums;

    /// <summary>
    /// History Item Model for WLN Related Info
    /// </summary>
    public class HistoryItemModel
    {
        /// <summary>
        /// Date
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// Docket Number
        /// </summary>
        public string DocketNumber { get; set; }

        /// <summary>
        /// Flag
        /// </summary>
        public bool Flag { get; set; }

        /// <summary>
        /// Parallel Citation
        /// </summary>
        public string ParallelCitation { get; set; }

        /// <summary>
        /// Primary Citation
        /// </summary>
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// Primary Citation
        /// </summary>
        public KeyCiteFlag KeyCiteFlag { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
    }
}