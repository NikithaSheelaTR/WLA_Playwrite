namespace Framework.Common.UI.Products.Shared.Models.GridModels
{
    using Framework.Common.UI.Products.Shared.Enums.Foldering;

    /// <summary>
    /// The all history table item model.
    /// </summary>
    public class AllHistoryGridModel : SearchesHistoryGridModel
    {       
        /// <summary>
        /// Gets or sets the event.
        /// </summary>
        public Events Event { get; set; }
    }
}
