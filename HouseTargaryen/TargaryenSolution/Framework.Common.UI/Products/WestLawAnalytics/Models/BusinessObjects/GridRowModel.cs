namespace Framework.Common.UI.Products.WestLawAnalytics.Models.BusinessObjects
{
    using System;

    /// <summary>
    /// Grid Row model
    /// </summary>
    public class GridRowModel
    {
        /// <summary>
        /// Client Id
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Session Type
        /// </summary>
        public string SessionType { get; set; }

        /// <summary>
        /// Chargeable or not
        /// </summary>
        public bool IsChargeable { get; set; }

        /// <summary>
        /// Is edited or not
        /// </summary>
        public bool IsEdited { get; set; }

        /// <summary>
        /// Total Cost
        /// </summary>
        public decimal TotalCost { get; set; }
    }
}
