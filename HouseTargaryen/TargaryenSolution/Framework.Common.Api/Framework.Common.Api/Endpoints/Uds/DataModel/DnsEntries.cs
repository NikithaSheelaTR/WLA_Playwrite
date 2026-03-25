namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The dns entries.
    /// </summary>
    [DataContract]
    public class DnsEntries
    {
        /// <summary>
        /// Gets or sets the external.
        /// </summary>
        [DataMember(Name = "external")]
        public string External { get; set; }

        /// <summary>
        /// Gets or sets the external bulk.
        /// </summary>
        [DataMember(Name = "externalBulk")]
        public string ExternalBulk { get; set; }

        /// <summary>
        /// Gets or sets the internal.
        /// </summary>
        [DataMember(Name = "internal")]
        public string Internal { get; set; }

        /// <summary>
        /// Gets or sets the internal bulk.
        /// </summary>
        [DataMember(Name = "internalBulk")]
        public string InternalBulk { get; set; }
    }
}