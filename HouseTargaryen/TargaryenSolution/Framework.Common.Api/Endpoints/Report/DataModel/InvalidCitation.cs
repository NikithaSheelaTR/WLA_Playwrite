namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// The invalid citation.
    /// </summary>
    public class InvalidCitation
    {
        /// <summary>
        /// Guid
        /// </summary>
        [JsonProperty("guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}
