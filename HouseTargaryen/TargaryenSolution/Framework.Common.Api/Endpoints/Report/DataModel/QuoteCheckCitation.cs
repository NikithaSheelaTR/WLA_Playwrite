namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// QuoteCheck Citation
    /// </summary>
    public class QuoteCheckCitation
    {
        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Primary Citation
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// Parallel Citation
        /// </summary>
        [JsonProperty("parallelCitation")]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Quote Text
        /// </summary>
        [JsonProperty("quoteText")]
        public string QuoteText { get; set; }

        /// <summary>
        /// Snap Snippet Score
        /// </summary>
        [JsonProperty("snapSnippetScore")]
        public string SnapSnippetScore { get; set; }
    }
}
