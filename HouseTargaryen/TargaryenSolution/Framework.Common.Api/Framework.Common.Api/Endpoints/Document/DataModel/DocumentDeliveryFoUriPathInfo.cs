namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    using Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo;

    /// <summary>
    /// The document delivery fo uri path info.
    /// </summary>
    [DataContract]
    public class DocumentDeliveryFoUriPathInfo
    {
        /// <summary>
        /// Gets or sets the delivery format override.
        /// </summary>
        [DataMember(Name = "DeliveryFormatOverride")]
        public string DeliveryFormatOverride { get; set; }

        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        [DataMember(Name = "DocumentGuid")]
        public string DocumentGuid { get; set; }

        /// <summary>
        /// Gets or sets the full json responce.
        /// </summary>
        public string FullJsonResponce { get; set; }

        /// <summary>
        /// Gets or sets the uri s.
        /// </summary>
        [DataMember(Name = "URIs")]
        public Uri[] UriS { get; set; }
    }
}