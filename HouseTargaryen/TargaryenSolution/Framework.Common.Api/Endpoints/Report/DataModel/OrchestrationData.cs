namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The orchestration data.
    /// </summary>
    public class OrchestrationData
    {
        /// <summary>
        /// Gets or sets the cases.
        /// </summary>
        [JsonProperty("cases")]
        public LegalIssuesContainer Cases { get; set; }

        /// <summary>
        /// Gets or sets the briefs.
        /// </summary>
        [JsonProperty("briefs")]
        public LegalIssuesContainer Briefs { get; set; }

        /// <summary>
        /// Gets or sets the secondary sources.
        /// </summary>
        [JsonProperty("secondarySources")]
        public LegalIssuesContainer SecondarySources { get; set; }

        /// <summary>
        /// Gets or sets the trial court documents.
        /// </summary>
        [JsonProperty("trialCourtDocuments")]
        public LegalIssuesContainer TrialCourtDocuments { get; set; }

        /// <summary>
        /// Gets or sets the extracted citations.
        /// </summary>
        [JsonProperty("extractedCitations")]
        public List<Recommendation> ExtractedCitations { get; set; }

        /// <summary>
        /// Gets or sets the invalid citations.
        /// </summary>
        [JsonProperty("invalidCitations")]
        public List<InvalidCitation> InvalidCitations { get; set; }

        /// <summary>
        /// Gets or sets the report data.
        /// </summary>
        [JsonProperty("reportData")]
        public ReportData ReportData { get; set; }

        /// <summary>
        /// Gets or sets the report type.
        /// </summary>
        [JsonProperty("reportType")]
        public string ReportType { get; set; }

        /// <summary>
        /// Gets or sets the fileName.
        /// </summary>
        [JsonProperty("fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the characterCountWithoutTags.
        /// </summary>
        [JsonProperty("characterCountWithoutTags")]
        public int CharacterCountWithoutTags { get; set; }

        /// <summary>
        /// Gets or sets the wordCountWithTags.
        /// </summary>
        [JsonProperty("wordCountWithTags")]
        public int WordCountWithTags { get; set; }

        /// <summary>
        /// Gets or sets the wordCountWithoutTags.
        /// </summary>
        [JsonProperty("wordCountWithoutTags")]
        public int WordCountWithoutTags { get; set; }

        /// <summary>
        /// Gets or sets the paragraphCount.
        /// </summary>
        [JsonProperty("paragraphCount")]
        public int ParagraphCount { get; set; }

    }
}
