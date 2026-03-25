namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

   /// <summary>
   /// Judicial file status info
   /// </summary>
    public class FileStatusInfo
    {
        /// <summary>
        /// judicial id
        /// </summary>
        [JsonProperty("judicialId")]
        public string JudicialId { get; set; }

        /// <summary>
        /// OverallStatus
        /// </summary>
        [JsonProperty("overallStatus")]
        public string OverallStatus { get; set; }

        /// <summary>
        /// Documents
        /// </summary>
        [JsonProperty("documents")]
        public List<JudicialDocumentInfo> Documents { get; set; }
    }
}
