namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// TabInfoRequest
    /// </summary>
    public class JudicialTabResponse
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// orchestration
        /// </summary>
        [JsonProperty("orchestration")]
        public string Orchestration { get; set; }

        /// <summary>
        /// documentCount
        /// </summary>
        [JsonProperty("documentCount")]
        public int DocumentCount { get; set; }

        /// <summary>
        /// createdDate
        /// </summary>
        [JsonProperty("createdDate")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// updatedDate
        /// </summary>
        [JsonProperty("updatedDate")]
        public DateTime UpdatedDate { get; set; }

        /// <summary>
        /// expiry
        /// </summary>
        [JsonProperty("expiry")]
        public ExpiryInfo Expiry { get; set; }

        /// <summary>
        /// state
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// orchestrationMeta
        /// </summary>
        [JsonProperty("orchestrationMeta")]
        public OrchestrationMetaInfo OrchestrationMeta { get; set; }

        /// <summary>
        /// results
        /// </summary>
        [JsonProperty("results")]
        public List<ReportResultInfo> Results { get; set; }
    }
}
