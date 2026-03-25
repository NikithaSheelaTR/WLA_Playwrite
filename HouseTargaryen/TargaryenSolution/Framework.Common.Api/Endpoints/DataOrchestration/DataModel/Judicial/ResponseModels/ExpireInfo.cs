namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Expire info
    /// </summary>
    public class ExpiryInfo
    {
        /// <summary>
        /// Expire
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }
    }
}
