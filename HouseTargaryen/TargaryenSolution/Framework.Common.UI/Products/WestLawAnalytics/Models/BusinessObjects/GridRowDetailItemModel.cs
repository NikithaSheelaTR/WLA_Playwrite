namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    using System;

    /// <summary>
    /// Detail Grid Row Model
    /// </summary>
    public class GridRowDetailItemModel
    {
        /// <summary>
        /// Practice Area
        /// </summary>
        public string PracticeArea { get; set; }

        /// <summary>
        /// Reason Code
        /// </summary>
        public string ReasonCode { get; set; }

        /// <summary>
        /// Research Description
        /// </summary>
        public string ResearchDescription { get; set; }

        /// <summary>
        /// Client Matter
        /// </summary>
        public string ClientMatter { get; set; }

        /// <summary>
        /// Product
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// Chargeable
        /// </summary>
        public string Chargeable { get; set; }

        /// <summary>
        /// Last IsEdited date
        /// </summary>
        public DateTime LastEditedDate { get; set; }

        /// <summary>
        /// Last edited by
        /// </summary>
        public string LastEditedBy { get; set; }
    }
}
