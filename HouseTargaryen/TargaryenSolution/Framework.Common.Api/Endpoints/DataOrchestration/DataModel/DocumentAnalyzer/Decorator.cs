namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;
    /// <summary>
    /// Decorator
    /// </summary>
    public class Decorator
    {
        /// <summary>
        /// Get or set guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Get or set converted lexis cite
        /// </summary>
        [JsonProperty("convertedLexisCite")]
        public string ConvertedLexisCite { get; set; }

        /// <summary>
        /// Get or set offset sort order
        /// </summary>
        [JsonProperty("offsetSortOrder")]
        public string OffsetSortOrder { get; set; }

        /// <summary>
        /// Get or set sort order
        /// </summary>
        [JsonProperty("sortOrder")]
        public string SortOrder { get; set; }

        /// <summary>
        /// Get or set toa sort order
        /// </summary>
        [JsonProperty("toaSortOrder")]
        public string ToaSortOrder { get; set; }

        /// <summary>
        /// Get or set distinguished by count
        /// </summary>
        [JsonProperty("distinguishedByTreatmentCount")]
        public NegativeOrDistinguishedByTreatmentCount DistinguishedByTreatmentCount { get; set; }

        /// <summary>
        /// Get or set negative treatment count
        /// </summary>
        [JsonProperty("negativeTreatmentCount")]
        public NegativeOrDistinguishedByTreatmentCount NegativeTreatmentCount { get; set; }

        /// <summary>
        /// Get or set most negative citations
        /// </summary>
        [JsonProperty("mostNegativeCitation")]
        public MostNegativeCitation MostNegativeCitation { get; set; }

        /// <summary>
        /// Get or set verified
        /// </summary>
        [JsonProperty("verified")]
        public string Verified { get; set; }
    }
}