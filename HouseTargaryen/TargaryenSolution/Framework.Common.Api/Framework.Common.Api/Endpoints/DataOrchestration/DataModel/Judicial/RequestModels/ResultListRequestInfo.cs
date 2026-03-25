namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// ResultListRequestInfo
    /// </summary>
    public class ResultListRequestInfo
    {
        /// <summary>
        /// FilterInfo
        /// </summary>
        [JsonProperty("filter")]
        public FilterInfo Filter { get; set; }

        /// <summary>
        /// SortKey
        /// </summary>
        [JsonProperty("sortKey")]
        public SortKeyInfo SortKey { get; set; }
    }
}
