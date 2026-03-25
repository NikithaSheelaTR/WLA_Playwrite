namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// CountsByPartyInfo
    /// </summary>
    public class CountsByPartyInfo
    {
        /// <summary>
        /// allIdentified
        /// </summary>
        [JsonProperty("allIdentified")]
        public int AllIdentified { get; set; }

        /// <summary>
        /// verifiedDifferences
        /// </summary>
        [JsonProperty("verifiedDifferences")]
        public int VerifiedDifferences { get; set; }

        /// <summary>
        /// AllVerified
        /// </summary>
        [JsonProperty("allVerified")]
        public int AllVerified { get; set; }
    }
}
