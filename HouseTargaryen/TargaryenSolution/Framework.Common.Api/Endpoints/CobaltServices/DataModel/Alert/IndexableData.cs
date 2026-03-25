namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Indexable Data model
    /// </summary>
    [DataContract]
    public class IndexableData
    {
        /// <summary>
        /// Private
        /// </summary>
        [DataMember(Name = "private")]
        public List<string> Private { get; set; }

        /// <summary>
        /// Client Id
        /// </summary>
        [DataMember(Name = "clientId")]
        public List<string> ClientId { get; set; }

        /// <summary>
        /// Has New Results
        /// </summary>
        [DataMember(Name = "hasNewResults")]
        public List<object> HasNewResults { get; set; }

        /// <summary>
        /// Active
        /// </summary>
        [DataMember(Name = "active")]
        public List<string> Active { get; set; }

        /// <summary>
        /// User Guid
        /// </summary>
        [DataMember(Name = "userGuid")]
        public List<string> UserGuid { get; set; }

        /// <summary>
        /// Search Target
        /// </summary>
        [DataMember(Name = "searchTarget")]
        public List<object> SearchTarget { get; set; }

        /// <summary>
        /// Frequency
        /// </summary>
        [DataMember(Name = "frequency")]
        public List<string> Frequency { get; set; }

        /// <summary>
        /// Firm Owned
        /// </summary>
        [DataMember(Name = "firmOwned")]
        public List<string> FirmOwned { get; set; }

        /// <summary>
        /// Has Recipients
        /// </summary>
        [DataMember(Name = "hasRecipients")]
        public List<string> HasRecipients { get; set; }

        /// <summary>
        /// Recipient
        /// </summary>
        [DataMember(Name = "recipient")]
        public List<string> Recipient { get; set; }

        /// <summary>
        /// Correlation Id
        /// </summary>
        [DataMember(Name = "correlationId")]
        public List<object> CorrelationId { get; set; }

        /// <summary>
        /// Alert Group 
        /// </summary>
        [DataMember(Name = "alertGroup")]
        public List<object> AlertGroup { get; set; }

        /// <summary>
        /// Customer Storage Key
        /// </summary>
        [DataMember(Name = "customerStorageKey")]
        public List<string> CustomerStorageKey { get; set; }
    }
}
