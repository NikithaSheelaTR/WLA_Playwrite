namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using Newtonsoft.Json;

    /// <summary>
    /// The relevant portion
    /// </summary>
    public class RelevantPortion
    {
        /// <summary>
        /// Gets or sets Id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets Source
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets Text
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets xPath
        /// </summary>
        [JsonProperty("xpath")]
        public string Xpath { get; set; }
    }
}