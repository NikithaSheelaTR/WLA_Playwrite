namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.Utils;

    using Newtonsoft.Json;

    /// <summary>
    /// ExtractedCitationInfo
    /// </summary>
    public class ExtractedCitationInfo
    {
        /// <summary>
        /// guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// fileDate
        /// </summary>
        [JsonProperty("fileDate")]
        public string FileDate { get; set; }

        /// <summary>
        /// contentType
        /// </summary>
        [JsonProperty("contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// citation
        /// </summary>
        [JsonProperty("citation")]
        public string Citation { get; set; }

        /// <summary>
        /// CodeSetName
        /// </summary>
        [JsonProperty("codeSetName")]
        public string CodeSetName { get; set; }

        /// <summary>
        /// TitleDescription
        /// </summary>
        [JsonProperty("titleDescription")]
        public string TitleDescription { get; set; }

        /// <summary>
        /// fileDateFormatted
        /// </summary>
        [JsonProperty("fileDateFormatted")]
        public string FileDateFormatted { get; set; }

        /// <summary>
        /// jurisdiction
        /// </summary>
        [JsonProperty("jurisdiction")]
        public JurisdictionInfo Jurisdiction { get; set; }

        /// <summary>
        /// flags
        /// </summary>
        [JsonProperty("flags")]
        public List<object> Flags { get; set; }

        /// <summary>
        /// InPlan
        /// </summary>
        [JsonProperty("inPlan")]
        public bool InPlan { get; set; }

        /// <summary>
        /// citingParties
        /// </summary>
        [JsonProperty("citingParties")]
        public List<string> CitingParties { get; set; }

        /// <summary>
        /// citedBy
        /// </summary>
        [JsonProperty("citedBy")]
        public List<CitedByInfo> CitedBy { get; set; }

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
        /// originalOrder
        /// </summary>
        [JsonProperty("originalOrder")]
        public int OriginalOrder { get; set; }

        /// <summary>
        /// sortOrder
        /// </summary>
        [JsonProperty("sortOrder")]
        public int SortOrder { get; set; }

        /// <summary>
        /// offsetSortOrder
        /// </summary>
        [JsonProperty("offsetSortOrder")]
        public int OffsetSortOrder { get; set; }

        /// <summary>
        /// toaSortOrder
        /// </summary>
        [JsonProperty("toaSortOrder")]
        public int ToaSortOrder { get; set; }

        /// <summary>
        /// currentPui
        /// </summary>
        [JsonProperty("currentPui")]
        public CurrentPuiInfo CurrentPui { get; set; }

        /// <summary>
        /// badges
        /// </summary>
        [JsonProperty("badges")]
        public List<object> Badges { get; set; }

        /// <summary>
        /// courtLine
        /// </summary>
        [JsonProperty("courtLine")]
        public string CourtLine { get; set; }

        /// <summary>
        /// parallelCitation
        /// </summary>
        [JsonProperty("parallelCitation")]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// primaryCitation
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// convertedLexisCite
        /// </summary>
        [JsonProperty("convertedLexisCite")]
        public bool ConvertedLexisCite { get; set; }

        /// <summary>
        /// lexisCiteTitle
        /// </summary>
        [JsonProperty("lexisCiteTitle")]
        public string LexisCiteTitle { get; set; }

        /// <summary>
        /// return lexis title
        /// </summary>
        /// <returns>lexis title</returns>
        public string GetLexisCiteTitle()
        {
            try
            {
                return this.LexisCiteTitle;
            }
            catch (NullReferenceException)
            {
                Logger.LogInfo("No lexisCite property on the item");
                return string.Empty;
            }
        }
    }
}
