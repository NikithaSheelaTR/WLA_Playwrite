namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer;
    using Framework.Common.Api.Endpoints.Report.DataModel;
    using Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5;
    using Framework.Core.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// RecommendationInfo
    /// </summary>
    public class RecommendationInfo
    {
        /// <summary>
        /// recGuid
        /// </summary>
        [JsonProperty("recGuid")]
        public string RecGuid { get; set; }

        /// <summary>
        /// docGuid
        /// </summary>
        [JsonProperty("docGuid")]
        public string DocGuid { get; set; }

        /// <summary>
        /// contentType
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// inPlan
        /// </summary>
        [JsonProperty("inPlan")]
        public bool InPlan { get; set; }

        /// <summary>
        /// fileDate
        /// </summary>
        [JsonProperty("fileDate")]
        public string FileDate { get; set; }

        /// <summary>
        /// fileDateFormatted
        /// </summary>
        [JsonProperty("fileDateFormatted")]
        public string FileDateFormatted { get; set; }

        /// <summary>
        /// courtline
        /// </summary>
        [JsonProperty("courtline")]
        public string Courtline { get; set; }

        /// <summary>
        /// primaryCitation
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// parallelCitation
        /// </summary>
        [JsonProperty("parallelCitation")]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// jurisdiction
        /// </summary>
        [JsonProperty("jurisdiction")]
        public JurisdictionInfo Jurisdiction { get; set; }

        /// <summary>
        /// badges
        /// </summary>
        [JsonProperty("badges")]
        public List<string> Badges { get; set; }

        /// <summary>
        /// inFolder
        /// </summary>
        [JsonProperty("inFolder")]
        public bool InFolder { get; set; }

        /// <summary>
        /// hasAnnotations
        /// </summary>
        [JsonProperty("hasAnnotations")]
        public bool HasAnnotations { get; set; }

        /// <summary>
        /// previouslyViewed
        /// </summary>
        [JsonProperty("previouslyViewed")]
        public PreviouslyViewedFacet PreviouslyViewed { get; set; }

        /// <summary>
        /// hasHighlights
        /// </summary>
        [JsonProperty("hasHighlights")]
        public bool HasHighlights { get; set; }

        /// <summary>
        /// hasTextNotes
        /// </summary>
        [JsonProperty("hasTextNotes")]
        public bool HasTextNotes { get; set; }

        /// <summary>
        /// relevantPortions
        /// </summary>
        [JsonProperty("relevantPortions")]
        public List<RelevantPortion> RelevantPortions { get; set; }

        /// <summary>
        /// relatedCases
        /// </summary>
        [JsonProperty("relatedCases")]
        public List<RelatedCase> RelatedCases { get; set; }

        /// <summary>
        /// synopsis
        /// </summary>
        [JsonProperty("synopsis")]
        public string Synopsis { get; set; }

        /// <summary>
        /// SynopsisDetails
        /// </summary>
        [JsonProperty("synopsisDetails")]
        public SynopsisDetailsInfo SynopsisDetails { get; set; }

        /// <summary>
        /// currentPui
        /// </summary>
        [JsonProperty("currentPui")]
        public CurrentPuiInfo CurrentPui { get; set; }

        /// <summary>
        /// focus
        /// </summary>
        [JsonProperty("focus")]
        public List<string> Focus { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// flags
        /// </summary>
        [JsonProperty("flags")]
        public List<Flag> Flags { get; set; }

        /// <summary>
        /// outcome
        /// </summary>
        [JsonProperty("outcome")]
        public string Outcome { get; set; }


        /// <summary>
        /// Return related cases
        /// </summary>
        /// <returns></returns>
        public List<RelatedCase> GetRelatedCases()
        {
            try
            {
                return this.RelatedCases;
            }
            catch(NullReferenceException)
            {
                Logger.LogInfo("No related cases for current document");
                return new List<RelatedCase>();
            }
        }
    }
}
