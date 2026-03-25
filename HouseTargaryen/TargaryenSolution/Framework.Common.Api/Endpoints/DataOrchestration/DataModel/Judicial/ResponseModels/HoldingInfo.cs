namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// HoldingInfo
    /// </summary>
    public class HoldingInfo
    {
        /// <summary>
        /// HoldingText
        /// </summary>
        [JsonProperty("holdingText")]
        public string HoldingText { get; set; }

        /// <summary>
        /// HoldingLink
        /// </summary>
        [JsonProperty("holdingLink")]
        public string HoldingLink { get; set; }

        /// <summary>
        /// Outcome
        /// </summary>
        [JsonProperty("outcome")]
        public bool Outcome { get; set; }
    }
}
