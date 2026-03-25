namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The fo private data.
    /// </summary>
    [DataContract]
    public class FoPrivateData
    {
        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        [DataMember(Name = "DocumentGuid")]
        public string DocumentGuid { get; set; }

        /// <summary>
        /// Gets or sets the fo base request.
        /// </summary>
        [DataMember(Name = "FoBaseRequest")]
        public FoBaseRequest FoBaseRequest { get; set; }
    }
}