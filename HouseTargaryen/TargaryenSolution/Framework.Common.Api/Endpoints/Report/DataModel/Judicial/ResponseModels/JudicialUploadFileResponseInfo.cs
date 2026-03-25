namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Net;

    using Newtonsoft.Json;

    /// <summary>
    /// Judicial file upload info
    /// </summary>
    public class JudicialUploadFileResponseInfo
    {
        /// <summary>
        /// Judicial report id
        /// </summary>
        [JsonProperty("reportId")]
        public string ReportId { get; set; }

        /// <summary>
        /// HTTP status code of the response
        /// </summary>
        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }
    }
}
