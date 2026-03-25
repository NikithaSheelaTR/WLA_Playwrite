namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// The relevant portion data
    /// </summary>
    public class RelevantPortionData
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the xpath.
        /// </summary>
        [JsonProperty("xpath")]
        public string Xpath { get; set; }
    }
}
