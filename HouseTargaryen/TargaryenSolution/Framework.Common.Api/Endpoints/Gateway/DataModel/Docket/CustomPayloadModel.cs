namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using Newtonsoft.Json;

    /// <summary>
    /// The custom payload model.
    /// </summary>
    public class CustomPayloadModel
    {
        /// <summary>
        /// Gets or sets the correlation id.
        /// </summary>
        [JsonProperty("correlationId")]
        public object CorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the parent correlation id.
        /// </summary>
        [JsonProperty("parentCorrelationId")]
        public object ParentCorrelationId { get; set; }

        /// <summary>
        /// Gets or sets the parent title.
        /// </summary>
        [JsonProperty("parentTitle")]
        public object ParentTitle { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is multipart.
        /// </summary>
        [JsonProperty("isMultipart")]
        public bool IsMultipart { get; set; }

        /// <summary>
        /// Gets or sets the multipart pdf payload.
        /// </summary>
        [JsonProperty("multipartPdfPayload")]
        public object MultipartPdfPayload { get; set; }

        /// <summary>
        /// Gets or sets the entry number.
        /// </summary>
        [JsonProperty("entryNumber")]
        public string EntryNumber { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is sealed.
        /// </summary>
        [JsonProperty("isSealed")]
        public bool IsSealed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has error.
        /// </summary>
        [JsonProperty("HasError")]
        public bool HasError { get; set; }

        /// <summary>
        /// Gets or sets the court norm.
        /// </summary>
        [JsonProperty("courtNorm")]
        public string CourtNorm { get; set; }

        /// <summary>
        /// Gets or sets the novus image guid.
        /// </summary>
        [JsonProperty("novusImageGuid")]
        public string NovusImageGuid { get; set; }

        /// <summary>
        /// Gets or sets the part number.
        /// </summary>
        [JsonProperty("partNumber")]
        public string PartNumber { get; set; }

        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }
    }
}