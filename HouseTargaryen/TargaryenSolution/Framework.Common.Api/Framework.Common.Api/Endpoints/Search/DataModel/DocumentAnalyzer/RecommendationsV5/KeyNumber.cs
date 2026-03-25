namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using Newtonsoft.Json;

    /// <summary>
    /// The KeyNumber
    /// </summary>
    public class KeyNumber
    {
        /// <summary>
        /// Gets or sets  Path
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets keyNumber
        /// </summary>
        [JsonProperty("keyNumber")]
        public string TheKeyNumber { get; set; }
    }
}