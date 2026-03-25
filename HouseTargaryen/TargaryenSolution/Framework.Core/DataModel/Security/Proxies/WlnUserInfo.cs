namespace Framework.Core.DataModel.Security.Proxies
{
    /// <summary>
    /// The WLN user info.
    /// </summary>
    public class WlnUserInfo : OnePassUserInfo
    {
        /// <summary>
        /// Billing type
        /// </summary>
        public string BillingType { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the matter id.
        /// </summary>
        public string MatterId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether retry client id selection on failure.
        /// </summary>
        public bool RetryClientIdSelectionOnFailure { get; set; }
        
        /// <summary>
        /// Gets or sets a values of User Reg Key, that we should select
        /// </summary>
        public string CurrentRegKey { get; set; }
    }
}