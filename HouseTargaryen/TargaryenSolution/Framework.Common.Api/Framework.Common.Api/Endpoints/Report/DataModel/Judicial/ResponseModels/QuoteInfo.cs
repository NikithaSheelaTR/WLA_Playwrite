namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using System;
    using System.Collections.Generic;

    using Framework.Core.Utils;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// QuoteInfo
    /// </summary>
    public class QuoteInfo
    {
        /// <summary>
        /// matchType
        /// </summary>
        [JsonProperty("matchType")]
        public string MatchType { get; set; }

        /// <summary>
        /// citationType
        /// </summary>
        [JsonProperty("citationType")]       
        public string CitationType { get; set; }
        
        /// <summary>
        /// currentPui
        /// </summary>
        [JsonProperty("currentPui")]
        public CurrentPuiInfo CurrentPui { get; set; }

        /// <summary>
        /// TODO add data contract
        /// </summary>
        [JsonProperty("flags")]
        public object Flags { get; set; }

        /// <summary>
        /// originalQuoteId
        /// </summary>
        [JsonProperty("originalQuoteId")]
        public string OriginalQuoteId { get; set; }

        /// <summary>
        /// originalPreQuote
        /// </summary>
        [JsonProperty("originalPreQuote")]
        public string OriginalPreQuote { get; set; }

        /// <summary>
        /// originalPostQuote
        /// </summary>
        [JsonProperty("originalPostQuote")]
        public string OriginalPostQuote { get; set; }

        /// <summary>
        /// originalQuoteLocation
        /// </summary>
        [JsonProperty("originalQuoteLocation")]
        public string OriginalQuoteLocation { get; set; }

        /// <summary>
        /// matchedQuoteItems
        /// </summary>
        [JsonProperty("matchedQuoteItems")]
        public List<object> MatchedQuoteItems { get; set; }

        /// <summary>
        /// originalQuoteItems
        /// </summary>
        [JsonProperty("originalQuoteItems")]
        public List<OriginalQuoteItemInfo> OriginalQuoteItems { get; set; }

        /// <summary>
        /// isFootnote
        /// </summary>
        [JsonProperty("isFootnote")]
        public bool IsFootnote { get; set; }

        /// <summary>
        /// isBlockQuote
        /// </summary>
        [JsonProperty("isBlockQuote")]
        public bool IsBlockQuote { get; set; }

        /// <summary>
        /// containsBrackets
        /// </summary>
        [JsonProperty("containsBrackets")]
        public bool ContainsBrackets { get; set; }

        /// <summary>
        /// containsEllipses
        /// </summary>
        [JsonProperty("containsEllipses")]
        public bool ContainsEllipses { get; set; }

        /// <summary>
        /// index
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// citedBy
        /// </summary>
        [JsonProperty("citedBy")]
        public CitedByInfo CitedBy { get; set; }

        /// <summary>
        /// guid
        /// </summary>
        [JsonProperty("guid", NullValueHandling = NullValueHandling.Ignore)]
        public string Guid { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [JsonProperty("title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        /// <summary>
        /// fileDate
        /// </summary>
        [JsonProperty("fileDate", NullValueHandling = NullValueHandling.Ignore)]
        public string FileDate { get; set; }

        /// <summary>
        /// courtLine
        /// </summary>
        [JsonProperty("courtLine", NullValueHandling = NullValueHandling.Ignore)]
        public string CourtLine { get; set; }

        /// <summary>
        /// primaryCitation
        /// </summary>
        [JsonProperty("primaryCitation", NullValueHandling = NullValueHandling.Ignore)]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// matchedPreQuote
        /// </summary>
        [JsonProperty("matchedPreQuote", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedPreQuote { get; set; }

        /// <summary>
        /// matchedPostQuote
        /// </summary>
        [JsonProperty("matchedPostQuote", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedPostQuote { get; set; }

        /// <summary>
        /// parallelCitation
        /// </summary>
        [JsonProperty("parallelCitation", NullValueHandling = NullValueHandling.Ignore)]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// matchedQuoteOffset
        /// </summary>
        [JsonProperty("matchedQuoteOffset", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedQuoteOffset { get; set; }

        /// <summary>
        /// matchedQuoteXPath
        /// </summary>
        [JsonProperty("matchedQuoteXPath", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedQuoteXPath { get; set; }

        /// <summary>
        /// matchedQuoteEndingXPath
        /// </summary>
        [JsonProperty("matchedQuoteEndingXPath", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedQuoteEndingXPath { get; set; }

        /// <summary>
        /// matchedQuoteId
        /// </summary>
        [JsonProperty("matchedQuoteId", NullValueHandling = NullValueHandling.Ignore)]
        public string MatchedQuoteId { get; set; }

        /// <summary>
        /// InPlan
        /// </summary>
        [JsonProperty("inPlan")]
        public bool InPlan { get; set; }

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
                Logger.LogInfo("No lexisCite property on the quotation");
                return string.Empty;
            }
        }
    }
}
