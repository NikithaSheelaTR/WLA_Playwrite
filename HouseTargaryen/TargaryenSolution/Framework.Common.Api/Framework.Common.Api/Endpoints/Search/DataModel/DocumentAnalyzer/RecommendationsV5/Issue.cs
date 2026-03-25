namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The issue
    /// </summary>
    public class Issue
    {
        /// <summary>
        /// Gets or sets the order
        /// </summary>
        [JsonProperty("order")]
        public long Order { get; set; }

        /// <summary>
        /// Gets or sets the offset
        /// </summary>
        [JsonProperty("offset")]
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the issueId
        /// </summary>
        [JsonProperty("issueId")]
        public string IssueId { get; set; }

        /// <summary>
        /// Gets or sets the issueTitle
        /// </summary>
        [JsonProperty("issueTitle")]
        public string IssueTitle { get; set; }

        /// <summary>
        /// Gets or sets the uniqueCitationsCount
        /// </summary>
        [JsonProperty("uniqueCitationsCount")]
        public long UniqueCitationsCount { get; set; }

        /// <summary>
        /// Gets or sets the recommendations Guids
        /// </summary>
        [JsonProperty("recommendationGuids", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> RecommendationGuids { get; set; }

        /// <summary>
        /// Gets or sets recommendations
        /// </summary>
        [JsonProperty("recommendations", NullValueHandling = NullValueHandling.Ignore)]
        public List<SearchRecommendation> Recommendations { get; set; }
    }
}