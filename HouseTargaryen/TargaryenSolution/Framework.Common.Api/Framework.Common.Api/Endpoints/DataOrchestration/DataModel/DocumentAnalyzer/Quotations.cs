namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Quotations Data Model
    /// </summary>
    public class Quotations
    {
        /// <summary>
        /// Citation type
        /// </summary>
        [JsonProperty("citationType")]
        public string CitationType { get; set; }

        /// <summary>
        /// Converted lexis cite
        /// </summary>
        [JsonProperty("convertedLexisCite")]
        public string ConvertedLexisCite { get; set; }

        /// <summary>
        /// Lexis cite title
        /// </summary>
        [JsonProperty("lexisCiteTitle")]
        public string LexisCiteTitle { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Is block quote
        /// </summary>
        [JsonProperty("isBlockQuote")]
        public string IsBlockQuote { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// File Data
        /// </summary>
        [JsonProperty("fileDate")]
        public string FileDate { get; set; }

        /// <summary>
        /// Court Line
        /// </summary>
        [JsonProperty("courtLine")]
        public string CourtLine { get; set; }

        /// <summary>
        /// Primary Citations
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// Parallel Citation
        /// </summary>
        [JsonProperty("parallelCitation")]
        public string ParallelCitation { get; set; }

        /// <summary>
        /// Original Quote Id
        /// </summary>
        [JsonProperty("originalQuoteId")]
        public string OriginalQuoteId { get; set; }

        /// <summary>
        /// Matched Quote Offset
        /// </summary>
        [JsonProperty("matchedQuoteOffset")]
        public int MatchedQuoteOffset { get; set; }

        /// <summary>
        /// Matched Pre Quote
        /// </summary>
        [JsonProperty("matchedPreQuote")]
        public string MatchedPreQuote { get; set; }

        /// <summary>
        /// Matched Post Quote
        /// </summary>
        [JsonProperty("matchedPostQuote")]
        public string MatchedPostQuote { get; set; }

        /// <summary>
        /// Matched Quote Truncated
        /// </summary>
        [JsonProperty("matchedQuoteTruncated")]
        public string MatchedQuoteTruncated { get; set; }

        /// <summary>
        /// Original Quote Truncated
        /// </summary>
        [JsonProperty("originalQuoteTruncated")]
        public string OriginalQuoteTruncated { get; set; }

        /// <summary>
        /// Original Quote Location
        /// </summary>
        [JsonProperty("originalQuoteLocation")]
        public string OriginalQuoteLocation { get; set; }

        /// <summary>
        /// Original Quote Sub Title
        /// </summary>
        [JsonProperty("originalQuoteSubTitle")]
        public string OriginalQuoteSubTitle { get; set; }

        /// <summary>
        /// Matched Quote Items
        /// </summary>
        [JsonProperty("matchedQuoteItems")]
        public List<QuoteItems> MatchedQuoteItems { get; set; }

        /// <summary>
        /// Original Quote Items
        /// </summary>
        [JsonProperty("originalQuoteItems")]
        public List<QuoteItems> OriginalQuoteItems { get; set; }
        
        /// <summary>
        /// Origination Pre Quote Items
        /// </summary>
        [JsonProperty("originalPreQuoteItems")]
        public List<OriginalQuoteItems> OriginalPreQuoteItems { get; set; }

        /// <summary>
        /// Original Post Quote Items
        /// </summary>
        [JsonProperty("originalPostQuoteItems")]
        public List<OriginalQuoteItems> OriginalPostQuoteItems { get; set; }

        /// <summary>
        /// The get hash code.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return this.Equals((Quotations)obj);
        }

        /// <summary>
        /// The equals.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        protected bool Equals(Quotations item)
        {
            return string.Equals(this.Guid, item.Guid) && string.Equals(this.Title, item.Title)
                                                       && string.Equals(this.FileDate, item.FileDate)
                                                       && string.Equals(this.CourtLine, item.CourtLine)
                                                       && string.Equals(this.PrimaryCitation, item.PrimaryCitation)
                                                       && string.Equals(this.OriginalQuoteId, item.OriginalQuoteId)
                                                       && string.Equals(this.MatchedQuoteOffset.ToString(), item.MatchedQuoteOffset.ToString())
                                                       && string.Equals(this.MatchedPreQuote, item.MatchedPreQuote)
                                                       && string.Equals(this.MatchedPostQuote, item.MatchedPostQuote)
                                                       && string.Equals(this.OriginalQuoteLocation, item.OriginalQuoteLocation)
                                                       && string.Equals(this.OriginalQuoteSubTitle, item.OriginalQuoteSubTitle)
                                                       && string.Equals(this.ParallelCitation, item.ParallelCitation)
                                                       && this.OriginalPostQuoteItems.SequenceEqual(item.OriginalPostQuoteItems)
                                                       && this.OriginalPreQuoteItems.SequenceEqual(item.OriginalPreQuoteItems)
                                                       && this.OriginalQuoteItems.SequenceEqual(item.OriginalQuoteItems)
                                                       && this.MatchedQuoteItems.SequenceEqual(item.MatchedQuoteItems);

        }
    }
}
