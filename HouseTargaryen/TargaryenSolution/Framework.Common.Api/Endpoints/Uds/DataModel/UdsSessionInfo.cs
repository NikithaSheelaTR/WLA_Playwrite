namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The uds session info.
    /// </summary>
    [DataContract]
    public class UdsSessionInfo
    {
        /// <summary>
        /// Gets or sets the billing method.
        /// </summary>
        [DataMember(Name = "BillingMethod")]
        public string BillingMethod { get; set; }

        /// <summary>
        /// Gets or sets the created date time.
        /// </summary>
        [DataMember(Name = "CreatedDateTime")]
        public string CreatedDateTime { get; set; }

        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [DataMember(Name = "EmailAddress")]
        public string EmailAddress { get; set; }

        /// <summary>
        /// Gets or sets the expires date time.
        /// </summary>
        [DataMember(Name = "ExpiresDateTime")]
        public string ExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the expires reason.
        /// </summary>
        [DataMember(Name = "ExpiresReason")]
        public string ExpiresReason { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [DataMember(Name = "FirstName")]
        public string FirstName { get; set; }

        /// <summary>
        ///  Gets or sets the Hto time zone offset
        /// </summary>
        [DataMember(Name = "HtoTimeZoneOffset")]
        public string HtoTimeZoneOffset { get; set; }

        /// <summary>
        /// Gets or sets the ip address.
        /// </summary>
        [DataMember(Name = "IpAddress")]
        public string IpAddress { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember(Name = "LastName")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the long token.
        /// </summary>
        [DataMember(Name = "LongToken")]
        public string LongToken { get; set; }

        /// <summary>
        /// Gets or sets the one pass product name.
        /// </summary>
        [DataMember(Name = "OnePassProductName")]
        public string OnePassProductName { get; set; }

        /// <summary>
        /// Gets or sets the one pass user name.
        /// </summary>
        [DataMember(Name = "OnePassUserName")]
        public string OnePassUserName { get; set; }

        /// <summary>
        /// Gets or sets the orphan expires date time.
        /// </summary>
        [DataMember(Name = "OrphanExpiresDateTime")]
        public string OrphanExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the payment type.
        /// </summary>
        [DataMember(Name = "PaymentType")]
        public string PaymentType { get; set; }

        /// <summary>
        /// Gets or sets the pmd data version.
        /// </summary>
        [DataMember(Name = "PmdDataVersion")]
        public int PmdDataVersion { get; set; }

        /// <summary>
        /// Gets or sets the prism auth token.
        /// </summary>
        [DataMember(Name = "PrismAuthToken")]
        public string PrismAuthToken { get; set; }

        /// <summary>
        /// Gets or sets the prism guid.
        /// </summary>
        [DataMember(Name = "PrismGuid")]
        public string PrismGuid { get; set; }

        /// <summary>
        /// Gets or sets the prism registration key.
        /// </summary>
        [DataMember(Name = "PrismRegistrationKey")]
        public string PrismRegistrationKey { get; set; }

        /// <summary>
        /// Gets or sets the product name.
        /// </summary>
        [DataMember(Name = "ProductName")]
        public string ProductName { get; set; }

        /// <summary>
        /// Gets or sets the product view.
        /// </summary>
        [DataMember(Name = "ProductView")]
        public string ProductView { get; set; }

        /// <summary>
        /// Gets or sets the seamless authentication token.
        /// </summary>
        [DataMember(Name = "SeamlessAuthenticationToken")]
        public string SeamlessAuthenticationToken { get; set; }

        /// <summary>
        /// Gets or sets the service type.
        /// </summary>
        [DataMember(Name = "ServiceType")]
        public string ServiceType { get; set; }

        /// <summary>
        /// Gets or sets the session based preferences.
        /// </summary>
        [DataMember(Name = "SessionBasedPreferences")]
        public string SessionBasedPreferences { get; set; }

        /// <summary>
        /// Gets or sets the session expires date time.
        /// </summary>
        [DataMember(Name = "SessionExpiresDateTime")]
        public string SessionExpiresDateTime { get; set; }

        /// <summary>
        /// Gets or sets the session id.
        /// </summary>
        [DataMember(Name = "SessionId")]
        public string SessionId { get; set; }

        /// <summary>
        /// Gets or sets the session source.
        /// </summary>
        [DataMember(Name = "SessionSource")]
        public string SessionSource { get; set; }

        /// <summary>
        /// Gets or sets the site.
        /// </summary>
        [DataMember(Name = "Site")]
        public string Site { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        [DataMember(Name = "Status")]
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the tier.
        /// </summary>
        [DataMember(Name = "Tier")]
        public string Tier { get; set; }

        /// <summary>
        /// Gets or sets the user category.
        /// </summary>
        [DataMember(Name = "UserCategory")]
        public string UserCategory { get; set; }

        /// <summary>
        /// Gets or sets the user classification.
        /// </summary>
        [DataMember(Name = "UserClassification")]
        public string UserClassification { get; set; }
    }
}