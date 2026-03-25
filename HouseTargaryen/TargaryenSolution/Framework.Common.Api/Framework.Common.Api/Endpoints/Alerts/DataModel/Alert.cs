namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The alert.
    /// </summary>
    [DataContract]
    public class Alert
    {
        /// <summary>
        /// Gets or sets the alert name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the alert description.
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the alert message.
        /// </summary>
        [DataMember(Name = "noDocsMessage")]
        public string NoDocsMessage { get; set; }

        /// <summary>
        /// Gets or sets the alert client id.
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the email recipients.
        /// </summary>
        [DataMember(Name = "emailRecipients")]
        public List<string> EmailRecipients { get; set; }

        /// <summary>
        /// Gets or sets the paused alerts.
        /// </summary>
        [DataMember(Name = "paused")]
        public bool Paused { get; set; }

        /// <summary>
        /// Gets or sets the alert type.
        /// </summary>
        [DataMember(Name = "alertType")]
        public string AlertType { get; set; }

        /// <summary>
        /// Gets or sets the firm.
        /// </summary>
        [DataMember(Name = "firmOwned")]
        public bool FirmOwned { get; set; }

        /// <summary>
        /// Gets or sets the created alert date.
        /// </summary>
        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the next update.
        /// </summary>
        [DataMember(Name = "nextUpdate")]
        public string NextUpdate { get; set; }

        /// <summary>
        /// Gets or sets the alert guid.
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the enabled deliveiries.
        /// </summary>
        [DataMember(Name = "enabledDeliveries")]
        public string EnabledDeliveries { get; set; }

        /// <summary>
        /// Gets or sets the enabled portal deliveiriy.
        /// </summary>
        [DataMember(Name = "enabledPortalDelivery")]
        public string EnabledPortalDelivery { get; set; }

        /// <summary>
        /// Gets or sets the client search data.
        /// </summary>
        [DataMember(Name = "clientDefinedSearchData")]
        public ClientDefinedSearchData ClientDefinedSearchData { get; set; }

        /// <summary>
        /// Gets or sets the mobile properties.
        /// </summary>
        [DataMember(Name = "mobileProperties")]
        public string MobileProperties { get; set; }

        /// <summary>
        /// Gets or sets the one time delivery
        /// </summary>
        [DataMember(Name = "oneTimeDelivery")]
        public bool OneTimeDelivery { get; set; }

        /// <summary>
        /// Gets or sets is private.
        /// </summary>
        [DataMember(Name = "isPrivate")]
        public bool IsPrivate { get; set; }

        /// <summary>
        /// Gets or sets the is data incomplete.
        /// </summary>
        [DataMember(Name = "isDataIncomplete")]
        public bool IsDataIncomplete { get; set; }

        /// <summary>
        /// Gets or sets the alert citation.
        /// </summary>
        [DataMember(Name = "citation")]
        public string Citation { get; set; }

        /// <summary>
        /// Gets or sets the alert group.
        /// </summary>
        [DataMember(Name = "alertGroup")]
        public string AlertGroup { get; set; }
    }
}
