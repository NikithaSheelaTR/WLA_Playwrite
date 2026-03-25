namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The persisted document
    /// </summary>
    [DataContract]
    public class PersistedDocument
    {
        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        [DataMember(Name = "Checksum")]
        public string Checksum { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        [DataMember(Name = "Guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        [DataMember(Name = "State")]
        public int State { get; set; }

        /// <summary>
        /// Gets or sets the sub content type id.
        /// </summary>
        [DataMember(Name = "SubContentTypeId")]
        public string SubContentTypeId { get; set; }
    }
}