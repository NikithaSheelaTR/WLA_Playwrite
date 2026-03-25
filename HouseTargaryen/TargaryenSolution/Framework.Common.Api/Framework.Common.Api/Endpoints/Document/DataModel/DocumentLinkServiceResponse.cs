namespace Framework.Common.Api.Endpoints.Document.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The document link service response.
    /// </summary>
    [DataContract]
    public class DocumentLinkServiceResponse
    {
        /// <summary>
        /// Gets or sets the entity name.
        /// </summary>
        [DataMember(Name = "entityName")]
        public string EntityName { get; set; }

        /// <summary>
        /// Gets or sets the fi.
        /// </summary>
        [DataMember(Name = "fi")]
        public string Fi { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the guids.
        /// </summary>
        [DataMember(Name = "guids")]
        public string[] Guids { get; set; }

        /// <summary>
        /// Gets or sets the link out url.
        /// </summary>
        [DataMember(Name = "linkOutUrl")]
        public string LinkOutUrl { get; set; }

        /// <summary>
        /// Gets or sets the pub num.
        /// </summary>
        [DataMember(Name = "pubNum")]
        public string PubNum { get; set; }

        /// <summary>
        /// Gets or sets the pub numbers.
        /// </summary>
        [DataMember(Name = "pubNumbers")]
        public string[] PubNumbers { get; set; }

        /// <summary>
        /// Gets or sets the response code.
        /// </summary>
        [DataMember(Name = "responseCode")]
        public string ResponseCode { get; set; }

        /// <summary>
        /// Gets or sets the response text.
        /// </summary>
        [DataMember(Name = "responseText")]
        public string ResponseText { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether s med lit stub document.
        /// </summary>
        [DataMember(Name = "sMedLitStubDocument")]
        public bool SMedLitStubDocument { get; set; }
    }
}