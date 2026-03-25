namespace Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer.RecommendationsV5
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The Headnote
    /// </summary>
    public class Headnote
    {
        /// <summary>
        /// Gets or sets The ChdId
        /// </summary>
        [JsonProperty("chdid")]
        public long Chdid { get; set; }

        /// <summary>
        /// Gets or sets The Text
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets The KeyNumbers
        /// </summary>
        [JsonProperty("keyNumbers")]
        public List<KeyNumber> KeyNumbers { get; set; }
    }
}