namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using Newtonsoft.Json;

    /// <summary>
    /// Quotations count summary data model
    /// </summary>
    public class QuotationsCountSummary
    {
        /// <summary>
        /// All identified
        /// </summary>
        [JsonProperty("allIdentified")]
        public string AllIdentified { get; set; }

        /// <summary>
        /// All verified
        /// </summary>
        [JsonProperty("allVerified")]
        public string AllVerified { get; set; }

        /// <summary>
        /// Verified differences
        /// </summary>
        [JsonProperty("verifiedDifferences")]
        public string VerifiedDifferences { get; set; }

        /// <summary>
        /// Record
        /// </summary>
        [JsonProperty("record")]
        public string Record { get; set; }

        /// <summary>
        /// Unverified
        /// </summary>
        [JsonProperty("unverified")]
        public string Unverified { get; set; }
    }
}
