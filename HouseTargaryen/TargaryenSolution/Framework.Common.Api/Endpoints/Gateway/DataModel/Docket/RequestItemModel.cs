namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using Newtonsoft.Json;

    /// <summary>
    /// The request item model.
    /// </summary>
    public class RequestItemModel
    {
        /// <summary>
        /// Gets or sets the acquisition guid.
        /// </summary>
        [JsonProperty("acquisitionGuid")]
        public string AcquisitionGuid { get; set; }

        /// <summary>
        /// Gets or sets the type code.
        /// </summary>
        [JsonProperty("typeCode")]
        public string TypeCode { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        [JsonProperty("statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the status description.
        /// </summary>
        [JsonProperty("statusDescription")]
        public string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the type description.
        /// </summary>
        [JsonProperty("typeDescription")]
        public string TypeDescription { get; set; }

        /// <summary>
        /// Gets or sets the request payload.
        /// </summary>
        [JsonProperty("requestPayload")]
        public RequestPayloadModel RequestPayload { get; set; }

        /// <summary>
        /// Gets or sets the created by user id.
        /// </summary>
        [JsonProperty("createdByUserId")]
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// Gets or sets the create datetime.
        /// </summary>
        [JsonProperty("createDatetime")]
        public string CreateDatetime { get; set; }

        /// <summary>
        /// Gets or sets the request version.
        /// </summary>
        [JsonProperty("requestVersion")]
        public string RequestVersion { get; set; }

        /// <summary>
        /// Gets or sets the request id.
        /// </summary>
        [JsonProperty("requestId")]
        public string RequestId { get; set; }
    }
}
