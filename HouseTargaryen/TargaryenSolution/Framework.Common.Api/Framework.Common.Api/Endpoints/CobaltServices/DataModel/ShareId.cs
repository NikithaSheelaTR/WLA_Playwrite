namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The share id.
    /// </summary>
    [DataContract]
    public class ShareId
    {
        /// <summary>
        /// Gets the created Date
        /// </summary>
        [DataMember(Name = "createdDate")]
        public string CreatedDate { get; set; }

        /// <summary>
        /// Gets the expires Date
        /// </summary>
        [DataMember(Name = "expiresDate")]
        public string ExpiresDate { get; set; }

        /// <summary>
        /// Gets the external Share State
        /// </summary>
        [DataMember(Name = "externalShareState")]
        public string ExternalShareState { get; set; }

        /// <summary>
        /// Gets the Initiator Guid
        /// </summary>
        [DataMember(Name = "initiatorGuid")]
        public string InitiatorGuid { get; set; }

        /// <summary>
        /// Gets the Recipient Email Address
        /// </summary>
        [DataMember(Name = "recipientEmailAddress")]
        public string RecipientEmailAddress { get; set; }

        /// <summary>
        /// Gets the Initiator Email Address
        /// </summary>
        [DataMember(Name = "initiatorEmailAddress")]
        public string InitiatorEmailAddress { get; set; }

        /// <summary>
        /// Gets the Sharing Token
        /// </summary>
        [DataMember(Name = "sharingToken")]
        public string SharingToken { get; set; }

        /// <summary>
        /// Gets the Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }
    }
}
