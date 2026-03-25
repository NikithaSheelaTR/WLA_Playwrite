namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;

    using Newtonsoft.Json;

    /// <summary>
    /// The recommendation.
    /// </summary>
    public class Recommendation
    {
        /// <summary>
        /// Gets or sets the badges.
        /// </summary>
        [JsonProperty("badges", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Badges { get; set; }

        /// <summary>
        /// Gets or sets the citation count.
        /// </summary>
        [JsonProperty("citationCount")]
        public int CitationCount { get; set; }

        /// <summary>
        /// Gets or sets the citing refs count.
        /// </summary>
        [JsonProperty("citingRefsCount")]
        public CitingRefsCountData CitingRefsCount { get; set; }

        /// <summary>
        /// Gets or sets the content type.
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the converted lexis cite.
        /// </summary>
        [JsonProperty("convertedLexisCite")]
        public string ConvertedLexisCite { get; set; }

        /// <summary>
        /// Gets or sets the court line.
        /// </summary>
        [JsonProperty("courtLine")]
        public string CourtLine { get; set; }

        /// <summary>
        /// Gets or sets the date display.
        /// </summary>
        [JsonProperty("dateDisplay")]
        public string DateDisplay { get; set; }

        /// <summary>
        /// Gets or sets the case info data.
        /// </summary>
        [JsonProperty("caseInfo")]
        public DetailsInfo CaseInfo { get; set; }

        /// <summary>
        /// Gets or sets the file date.
        /// </summary>
        [JsonProperty("fileDate")]
        public string FileDate { get; set; }

        /// <summary>
        /// Gets or sets the file date formatted.
        /// </summary>
        [JsonProperty("fileDateFormatted")]
        public string FileDateFormatted { get; set; }

        /// <summary>
        /// Gets or sets the focus.
        /// </summary>
        [JsonProperty("focus")]
        public List<string> Focus { get; set; }

        /// <summary>
        /// Gets or sets the guid.
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction.
        /// </summary>
        [JsonProperty("jurisdiction")]
        public FaceInfo Jurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("label")]
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets the lexis cite title.
        /// </summary>
        [JsonProperty("lexisCiteTitle")]
        public string LexisCiteTitle { get; set; }

        /// <summary>
        /// Get or set most negative citations.
        /// </summary>
        [JsonProperty("mostNegativeCitation", NullValueHandling = NullValueHandling.Ignore)]
        public MostNegativeCitation MostNegativeCitation { get; set; }

        /// <summary>
        /// Get or set negative treatment count.
        /// </summary>
        [JsonProperty("negativeTreatmentCount", NullValueHandling = NullValueHandling.Ignore)]
        public NegativeOrDistinguishedByTreatmentCount NegativeTreatmentCount { get; set; }

        /// <summary>
        /// Get or set original order.
        /// </summary>
        [JsonProperty("originalOrder")]
        public int OriginalOrder { get; set; }

        /// <summary>
        /// Gets or sets the outcome.
        /// </summary>
        [JsonProperty("outcome")]
        public string Outcome { get; set; }

        /// <summary>
        /// Gets or sets the parallel citation.
        /// </summary>
        [JsonProperty("parallelCitation")]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// Gets or sets the practice areas.
        /// </summary>
        [JsonProperty("practiceAreas")]
        public List<FaceInfo> PracticeAreas { get; set; }

        /// <summary>
        /// Gets or sets the primary citation.
        /// </summary>
        [JsonProperty("primaryCitation", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// Gets or sets the publication name.
        /// </summary>
        [JsonProperty("publicationName")]
        public string PublicationName { get; set; }

        /// <summary>
        /// Get or set negative rank.
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the reported status.
        /// </summary>
        [JsonProperty("reportedStatus")]
        public string ReportedStatus { get; set; }

        /// <summary>
        /// Gets or sets the relevant portion data.
        /// </summary>
        [JsonProperty("relevantPortions", NullValueHandling = NullValueHandling.Ignore)]
        public List<RelevantPortionData> RelevantPortionData { get; set; }

        /// <summary>
        /// Gets or sets the list of related cases.
        /// </summary>
        [JsonProperty("relatedCases", NullValueHandling = NullValueHandling.Ignore)]
        public List<RelatedCase> RelatedCases { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets the tags.
        /// </summary>
        [JsonProperty("tags", NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Get or set the threshold score.
        /// </summary>
        [JsonProperty("thresholdScore")]
        public double ThresholdScore { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
        
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [JsonProperty("depthOfDiscussion")]
        public string DepthOfDiscussion { get; set; }
    }
}