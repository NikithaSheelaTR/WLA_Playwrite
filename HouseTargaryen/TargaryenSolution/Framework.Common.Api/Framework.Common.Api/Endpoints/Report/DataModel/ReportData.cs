namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Report data
    /// </summary>
    public class ReportData
    {
        /// <summary>
        /// Gets or sets the report info.
        /// </summary>
        [JsonProperty("reportInfo")]
        public DetailsInfo ReportInfo { get; set; }
    }
}
