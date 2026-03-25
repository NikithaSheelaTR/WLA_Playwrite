namespace Framework.Common.Api.Endpoints.Document.DataModel.DeliveryFoUriPathInfo
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The uri.
    /// </summary>
    [DataContract]
    public class Uri
    {
        /// <summary>
        /// Gets or sets the fo private data.
        /// </summary>
        [DataMember(Name = "FOPrivateData")]
        public FoPrivateData FoPrivateData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is related docket or opinion.
        /// </summary>
        [DataMember(Name = "IsRelatedDocketOrOpinion")]
        public bool IsRelatedDocketOrOpinion { get; set; }

        /// <summary>
        /// Gets or sets the uri.
        /// </summary>
        [DataMember(Name = "URI")]
        public string Uris { get; set; }
    }
}