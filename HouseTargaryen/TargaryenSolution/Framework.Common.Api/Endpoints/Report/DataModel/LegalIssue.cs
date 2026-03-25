namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The legal issue.
    /// </summary>
    public class LegalIssue
    {
        /// <summary>
        /// Gets or sets the order.
        /// </summary>
        [JsonProperty("order")]
        public long Order { get; set; }

        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        [JsonProperty("offset")]
        public long Offset { get; set; }

        /// <summary>
        /// Gets or sets the issue id.
        /// </summary>
        [JsonProperty("issueId")]
        public string IssueId { get; set; }

        /// <summary>
        /// Gets or sets the issue title.
        /// </summary>
        [JsonProperty("issueTitle")]
        public string IssueTitle { get; set; }

        /// <summary>
        /// Gets or sets the unique citations count.
        /// </summary>
        [JsonProperty("uniqueCitationsCount")]
        public long UniqueCitationsCount { get; set; }

        /// <summary>
        /// Gets or sets the Associated citations
        /// </summary>
        [JsonProperty("associatedCitations", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> AssociatedCitations { get; set; }

        /// <summary>
        /// Gets or sets the recommendations.
        /// </summary>
        [JsonProperty("recommendations")]
        public List<Recommendation> Recommendations { get; set; }

        /// <summary>
        /// Gets or sets the additional cases count.
        /// </summary>
        [JsonProperty("additionalCasesCount")]
        public int AdditionalCasesCount { get; set; }
    }
}