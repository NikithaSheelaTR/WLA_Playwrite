namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// QuoteCountsByParty
    /// </summary>
    public class QuoteCountsByParty
    {
        /// <summary>
        /// FirstParty
        /// </summary>
        [JsonProperty("1")]
        public CountsByPartyInfo FirstParty { get; set; }

        /// <summary>
        /// SecondParty
        /// </summary>
        [JsonProperty("2")]
        public CountsByPartyInfo SecondParty { get; set; }
    }
}
