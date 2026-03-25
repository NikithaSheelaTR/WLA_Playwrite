namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System;

    using Newtonsoft.Json;

    /// <summary>
    /// QuotationsResults
    /// </summary>
    public class QuotationsResults
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        [JsonProperty("index")] 
        public long Index { get; set; }

        /// <summary>
        /// CreatedDate
        /// </summary>
        [JsonProperty("createdDate")]
        public DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// OrchestrationData
        /// </summary>
        [JsonProperty("orchestrationData")] 
        public JudicialQuotationsOrchestrationData OrchestrationData { get; set; }

    }
}
