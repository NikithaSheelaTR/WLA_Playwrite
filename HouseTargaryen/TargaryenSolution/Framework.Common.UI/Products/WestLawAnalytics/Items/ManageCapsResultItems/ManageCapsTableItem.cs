namespace Framework.Common.UI.Products.WestLawAnalytics.Items.ManageCapsResultItems
{
    using OpenQA.Selenium;

    /// <summary>
    /// Describe items in the 'Manage Caps' table in Westlaw Analytics
    /// </summary>
    public class ManageCapsTableItem
    {
        /// <summary>
        /// Checkbox
        /// </summary>
        public IWebElement Checkbox { get; set; }

        /// <summary>
        /// Cap Name
        /// </summary>
        public string CapName { get; set; }

        /// <summary>
        /// Client ID
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Billing Groups
        /// </summary>
        public string BillingGroups { get; set; }

        /// <summary>
        /// Cap Amount
        /// </summary>
        public string CapAmount { get; set; }

        /// <summary>
        /// Cap Type
        /// </summary>
        public string CapType { get; set; }

        /// <summary>
        /// Begin Date
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
        public string EndDate { get; set; }

        /// <summary>
        /// Edit Link
        /// </summary>
        public IWebElement EditLink { get; set; }
    }
}
