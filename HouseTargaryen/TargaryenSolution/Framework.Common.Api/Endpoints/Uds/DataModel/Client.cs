namespace Framework.Common.Api.Endpoints.Uds.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The client.
    /// </summary>
    [DataContract]
    public class Client
    {
        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client status modified by.
        /// </summary>
        [DataMember(Name = "clientStatusModifiedBy")]
        public ClientStatusModifiedBy ClientStatusModifiedBy { get; set; }

        /// <summary>
        /// Gets or sets the client status modified date.
        /// </summary>
        [DataMember(Name = "clientStatusModifiedDate")]
        public string ClientStatusModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        [DataMember(Name = "isActive")]
        public bool IsActive { get; set; }
    }
}