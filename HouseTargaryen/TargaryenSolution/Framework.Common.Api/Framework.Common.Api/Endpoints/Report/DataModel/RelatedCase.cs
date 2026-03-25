namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System;

    using Framework.Core.Utils;

    using Newtonsoft.Json;

    /// <summary>
    /// The related case.
    /// </summary>
    public class RelatedCase
    {
        /// <summary>
        /// Gets or sets flag
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets link
        /// </summary>
        [JsonProperty("link")]
        public string Link { get; set; }

        /// <summary>
        /// Gets or sets doc name
        /// </summary>
        [JsonProperty("documentName")]
        public string DocumentName { get; set; }

        /// <summary>
        /// FiledDate
        /// </summary>
        [JsonProperty("filedDate")]
        public string FiledDate { get; set; }

        /// <summary>
        /// Gets or sets primary citation
        /// </summary>
        [JsonProperty("primaryCitation")]
        public string PrimaryCitation { get; set; }

        /// <summary>
        /// Gets or sets convertedLexisCite
        /// </summary>
        [JsonProperty("convertedLexisCite", NullValueHandling = NullValueHandling.Ignore)]
        public bool ConvertedLexisCite { get; set; }

        /// <summary>
        /// Courtline
        /// </summary>
        [JsonProperty("courtline")]
        public string Courtline { get; set; }
  
        /// <summary>
        /// Gets or sets LexisCiteTitle
        /// </summary>
        [JsonProperty("lexisCiteTitle", NullValueHandling = NullValueHandling.Ignore)]
        public string LexisCiteTitle { get; set; }

        /// <summary>
        /// Verify is related case is converted lexis cite
        /// </summary>
        /// <returns></returns>
        public bool IsConvertedLexisCite()
        {
            try
            {
                return this.ConvertedLexisCite;
            }
            catch (NullReferenceException)
            {
                Logger.LogInfo("Related case is not lexis");
                return false;
            }
        }
    }
}