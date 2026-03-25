namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels;

    using Newtonsoft.Json;

    /// <summary>
    /// UploadSummaryInfo
    /// </summary>
    public class UploadSummaryInfo
    {
        /// <summary>
        /// reportName
        /// </summary>
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        /// <summary>
        /// parties
        /// </summary>
        [JsonProperty("parties")]
        public List<PartyInfo> Parties { get; set; }

        /// <summary>
        /// ocrConverted
        /// </summary>
        [JsonProperty("ocrConverted")]
        public bool OcrConverted { get; set; }
    }
}
