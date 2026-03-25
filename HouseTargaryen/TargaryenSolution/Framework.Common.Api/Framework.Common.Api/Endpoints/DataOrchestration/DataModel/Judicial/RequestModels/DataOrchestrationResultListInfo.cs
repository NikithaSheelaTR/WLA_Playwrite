namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using Newtonsoft.Json;
    
    /// <summary>
    /// Request info to data orchestration
    /// sends to get status of report
    /// </summary>
    public class DataOrchestrationResultListInfo
    {
        /// <summary>
        /// Gets expire date of report
        /// </summary>
        [JsonProperty("expiry")]
        public ExpiryReportInfo Expiry { get; set; }

        /// <summary>
        /// Gets party info
        /// </summary>
        [JsonProperty("orchestrationData")]
        public OrchestrationDataRequest OrchestrationData { get; set; }
    }
}
