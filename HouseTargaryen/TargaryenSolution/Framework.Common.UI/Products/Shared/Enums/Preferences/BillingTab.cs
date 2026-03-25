namespace Framework.Common.UI.Products.Shared.Enums.Preferences
{
    /// <summary>
    /// This is an enum of the possible options on the user preferences billing tab
    /// </summary>
    public enum BillingTab
    {
        /// <summary>
        /// Billing For Session, Ask at Sign
        /// </summary>
        AskAtSignOnBillingRadioButton,

        /// <summary>
        /// Billing For Deliveries, Bill by Document
        /// </summary>
        BillByDocumentRadioButton,

        /// <summary>
        /// Billing For Deliveries, Bill by Line
        /// </summary>
        BillByLineRadioButton,

        /// <summary>
        /// Billing For Session, Hourly
        /// </summary>
        HourlyBillingRadioButton,

        /// <summary>
        /// Pause Session Checkbox
        /// </summary>
        PauseSessionCheckbox,

        /// <summary>
        /// Billing For Session, Transactional
        /// </summary>
        TransactionalBillingRadioButton
    }
}