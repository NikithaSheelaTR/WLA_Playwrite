namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Structured Report
    /// </summary>
    public class StructuredReport
    {
        /// <summary>
        /// Heading id
        /// </summary>
        [JsonProperty("headingId")]
        public string HeadingId { get; set; }

        /// <summary>
        /// Is filtered
        /// </summary>
        [JsonProperty("isFiltered")]
        public bool IsFiltered { get; set; }

        /// <summary>
        /// Recommendations Id
        /// </summary>
        [JsonProperty("recommendationIds")]
        public List<string> RecommendationIds { get; set; }
    }
}
