namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// Result info
    /// </summary>
    public class ResultInfo
    {
        /// <summary>
        /// Index
        /// </summary>
        [JsonProperty("index")]
        public string Index { get; set; }
    }
}
