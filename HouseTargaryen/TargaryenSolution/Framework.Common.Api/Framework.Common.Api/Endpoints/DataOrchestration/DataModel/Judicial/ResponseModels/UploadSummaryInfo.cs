namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System.Collections.Generic;

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

        /// <summary>
        /// Equals
        /// </summary>
        /// <param name="obj">object that equals</param>
        /// <returns>true if equals, false otherwise</returns>
        public override bool Equals(object obj)
        {
            var result = obj as UploadSummaryInfo;

            return this.ReportName.Equals(result.ReportName) && this.Parties[0].Id.Equals(result.Parties[0].Id)
                                                             && this.Parties[0].Name.Equals(result.Parties[0].Name);
        }

        /// <summary>
        /// Get Hash Code method
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode() => this.ReportName.GetHashCode() * this.Parties.GetHashCode();

    }
}
