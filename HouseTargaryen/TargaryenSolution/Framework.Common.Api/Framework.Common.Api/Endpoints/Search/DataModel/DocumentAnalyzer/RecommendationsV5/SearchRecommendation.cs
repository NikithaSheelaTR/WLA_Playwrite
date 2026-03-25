namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The Search Recommendation
    /// </summary>
    public class SearchRecommendation
    {
        /// <summary>
        /// Gets or sets Guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets CaseCitationCount
        /// </summary>
        [JsonProperty("caseCitationCount")]
        public long CaseCitationCount { get; set; }

        /// <summary>
        /// Gets or sets CourtLevel
        /// </summary>
        [JsonProperty("courtLevel")]
        public long CourtLevel { get; set; }

        /// <summary>
        /// Gets or sets Headnotes
        /// </summary>
        [JsonProperty("headnotes")]
        public List<Headnote> Headnotes { get; set; }

        /// <summary>
        /// Gets or sets RelevantPortions
        /// </summary>
        [JsonProperty("relevantPortions")]
        public List<RelevantPortion> RelevantPortions { get; set; }

        /// <summary>
        /// Gets or sets Details
        /// </summary>
        [JsonProperty("details")]
        public Details Details { get; set; }
    }
}