namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// OrchestrationMetaInfo
    /// </summary>
    public class OrchestrationMetaInfo
    {
        /// <summary>
        /// status
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        /// <summary>
        /// documentIds
        /// </summary>
        [JsonProperty("documentIds")]
        public List<string> DocumentIds { get; set; }

        /// <summary>
        /// clientId
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }
    }
}
