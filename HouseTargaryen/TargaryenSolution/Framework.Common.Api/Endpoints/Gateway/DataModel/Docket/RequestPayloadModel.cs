namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using Newtonsoft.Json;

    /// <summary>
    /// The request payload model.
    /// </summary>
    public class RequestPayloadModel
    {
        /// <summary>
        /// Gets or sets the court.
        /// </summary>
        [JsonProperty("court")]
        public string Court { get; set; }

        /// <summary>
        /// Gets or sets the case number.
        /// </summary>
        [JsonProperty("caseNumber")]
        public string CaseNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the court number.
        /// </summary>
        [JsonProperty("courtNumber")]
        public string CourtNumber { get; set; }

        /// <summary>
        /// Gets or sets the parent key.
        /// </summary>
        [JsonProperty("parentKey")]
        public string ParentKey { get; set; }

        /// <summary>
        /// Gets or sets the custom payload.
        /// </summary>
        [JsonProperty("customPayload")]
        public CustomPayloadModel CustomPayload { get; set; }
    }
}