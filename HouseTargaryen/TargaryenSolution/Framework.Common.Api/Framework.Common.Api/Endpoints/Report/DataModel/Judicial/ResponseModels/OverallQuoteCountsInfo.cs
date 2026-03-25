namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// OverallQuoteCountsInfo
    /// </summary>
    public class OverallQuoteCountsInfo
    {
        /// <summary>
        /// VerifiedDifferences
        /// </summary>
        [JsonProperty("verifiedDifferences")]
        public long VerifiedDifferences { get; set; }

        /// <summary>
        /// AllIdentified
        /// </summary>
        [JsonProperty("allIdentified")]
        public long AllIdentified { get; set; }

        /// <summary>
        /// AllVerified
        /// </summary>
        [JsonProperty("allVerified")]
        public long AllVerified { get; set; }
    }
}
