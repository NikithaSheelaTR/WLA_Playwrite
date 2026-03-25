namespace Framework.Common.Api.Endpoints.Gateway.DataModel.Docket
{
    using Newtonsoft.Json;

    /// <summary>
    /// The update payload model.
    /// </summary>
    public class UpdatePayloadModel
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
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the novus guid.
        /// </summary>
        [JsonProperty("novusGuid")]
        public string NovusGuid { get; set; }
    }
}