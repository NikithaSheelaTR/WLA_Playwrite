namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// OriginalQuoteItemInfo
    /// </summary>
    public class OriginalQuoteItemInfo
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// content
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// originalContent
        /// </summary>
        [JsonProperty("originalContent", NullValueHandling = NullValueHandling.Ignore)]
        public string OriginalContent { get; set; }
    }
}
