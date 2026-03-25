namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;

    /// <summary>
    /// The identified issue object
    /// </summary>
    public class IdentifiedIssue
    {
        /// <summary>
        /// Gets or sets the order
        /// </summary>
        [JsonProperty("order")]
        public long Order { get; set; }

        /// <summary>
        /// Gets or sets the Original Title
        /// </summary>
        [JsonProperty("originalTitle")]
        public string OriginalTitle { get; set; }

        /// <summary>
        /// Gets or sets the Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}