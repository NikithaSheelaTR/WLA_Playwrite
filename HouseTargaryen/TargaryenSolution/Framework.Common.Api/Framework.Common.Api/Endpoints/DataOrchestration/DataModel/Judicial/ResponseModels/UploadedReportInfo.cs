namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Uploaded report info
    /// </summary>
    public class UploadedReportInfo
    {
        /// <summary>
        /// Judicial id
        /// </summary>
        [JsonProperty("judicialId")]
        public string JudicialId { get; set; }

        /// <summary>
        /// Overall Status
        /// </summary>
        [JsonProperty("overallStatus")]
        public string OverallStatus { get; set; }

        /// <summary>
        /// List of documents uploaded to report
        /// </summary>
        [JsonProperty("documents")]
        public List<UploadingDocumentInfo> Documents { get; set; }
    }
}
