namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// PreviouslyViewedDocument
    /// </summary>
    public class PreviouslyViewedDocument
    {
        /// <summary>
        /// last7
        /// </summary>
        [JsonProperty("last7")]
        public bool Last7 { get; set; }

        /// <summary>
        /// last14
        /// </summary>
        [JsonProperty("last14")]
        public bool Last14 { get; set; }

        /// <summary>
        /// last30
        /// </summary>
        [JsonProperty("last30")]
        public bool Last30 { get; set; }

        /// <summary>
        /// never
        /// </summary>
        [JsonProperty("never")]
        public bool Never { get; set; }
    }
}
