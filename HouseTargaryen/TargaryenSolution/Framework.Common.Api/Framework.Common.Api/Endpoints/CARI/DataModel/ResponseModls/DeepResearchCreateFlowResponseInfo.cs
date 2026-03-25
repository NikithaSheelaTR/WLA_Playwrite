namespace Framework.Common.Api.Endpoints.CARI.DataModel.ResponseModls
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// DeepResearchCreateFlowResponseInfo
    /// </summary>
    public class DeepResearchCreateFlowResponseInfo
    {
        /// <summary>
        /// Flow id
        /// </summary>
        [JsonProperty("flowId")]
        public string FlowId { get; set; }
    }
}
