namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;
    using Newtonsoft.Json;

    /// <summary>
    /// Rerun report request
    /// </summary>
    public class RerunReportRequest
    {
        /// <summary>
        /// Rerun report request parameters
        /// </summary>
        [JsonProperty("requestParams")]
        public RerunReportRequestParams RequestParams { get; set; } = new RerunReportRequestParams();
    }
}
