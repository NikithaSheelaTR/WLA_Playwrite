namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The work product token response.
    /// </summary>
    [DataContract]
    public class WorkProductTokenResponse
    {
        /// <summary>
        /// Gets or sets the access controls map.
        /// </summary>
        [DataMember(Name = "AccessControlsMap")]
        public AccessControlsMap AccessControlsMap { get; set; }

        /// <summary>
        /// Gets or sets the dns entries.
        /// </summary>
        [DataMember(Name = "DnsEntries")]
        public DnsEntries DnsEntries { get; set; }

        /// <summary>
        /// Gets or sets the expiration date.
        /// </summary>
        [DataMember(Name = "ExpirationDate")]
        public string ExpirationDate { get; set; }

        /// <summary>
        /// Gets or sets the site hash id.
        /// </summary>
        [DataMember(Name = "SiteHashId")]
        public string SiteHashId { get; set; }

        /// <summary>
        /// Gets or sets the token holder user id.
        /// </summary>
        [DataMember(Name = "TokenHolderUserId")]
        public string TokenHolderUserId { get; set; }

        /// <summary>
        /// Gets or sets the work product token.
        /// </summary>
        [DataMember(Name = "WorkProductToken")]
        public string WorkProductToken { get; set; }
    }
}