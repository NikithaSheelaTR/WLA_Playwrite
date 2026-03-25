namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// The pagination.
    /// </summary>
    public class Pagination
    {
        /// <summary>
        /// Gets or sets the from.
        /// </summary>
        [JsonProperty("from")]
        public long From { get; set; }

        /// <summary>
        /// Gets or sets the to.
        /// </summary>
        [JsonProperty("to")]
        public long To { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has more.
        /// </summary>
        [JsonProperty("hasMore")]
        public bool HasMore { get; set; }
    }
}