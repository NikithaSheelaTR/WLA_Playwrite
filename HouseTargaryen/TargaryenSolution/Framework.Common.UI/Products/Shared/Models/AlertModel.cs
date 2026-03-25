namespace Framework.Common.UI.Products.Shared.Models
{
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Enums.Content;

    /// <summary>
    /// Alert Model
    /// </summary>
    public class AlertModel
    {
        /// <summary>
        /// Alert Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Alert Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Content Category to add
        /// </summary>
        public string ContentCategoryToAdd { get; set; }

        /// <summary>
        /// Jurisdiction
        /// </summary>
        public Jurisdiction[] Jurisdictions { get; set; }

        /// <summary>
        /// Content Category to select
        /// </summary>
        public string ContentCategoryToSelect { get; set; }

        /// <summary>
        /// Text to search
        /// </summary>
        public string SearchText { get; set; }

        /// <summary>
        /// Recipient email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Client Id for alert
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Docket Number for Docket Track alerts
        /// </summary>
        public string DocketNumber { get; set; }

        /// <summary>
        /// Alerts Content Tab
        /// </summary>
        public AlertsContentTab ContentTab { get; set; }

        /// <summary>
        /// Options in 'Enter Search Terms' section for docket track alert
        /// If false - 'Do not limit results by search terms'; if true - Limit results by search terms
        /// </summary>
        public bool LimitResultsBySearchTerms { get; set; }

        /// <summary>
        /// Alert Format
        /// </summary>
        public string Format { get; set; }

        /// <summary>
        /// Schedule alert Detail Level
        /// </summary>
        public string DetailLevel { get; set; }

        /// <summary>
        /// Citation
        /// </summary>
        public string Citation { get; set; }

        /// <summary>
        /// Group Name for Alert
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// Updated Group Name for Alert
        /// </summary>
        public string UpdatedGroupName { get; set; }
    }
}
