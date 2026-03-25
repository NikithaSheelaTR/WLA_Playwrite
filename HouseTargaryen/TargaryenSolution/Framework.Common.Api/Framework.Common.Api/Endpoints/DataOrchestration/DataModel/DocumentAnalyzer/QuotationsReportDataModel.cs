namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Quotations Report Data Model
    /// </summary>
    public class QuotationsReportDataModel
    {
        /// <summary>
        /// Identified quotations
        /// </summary>
        [JsonProperty("identifiedQuotations")]
        public List<IdentifiedQuotations> IdentifiedQuotations { get; set; }

        /// <summary>
        /// Quotations
        /// </summary>
        [JsonProperty("quotations")]
        public List<Quotations> Quotations { get; set; }
    }
}
