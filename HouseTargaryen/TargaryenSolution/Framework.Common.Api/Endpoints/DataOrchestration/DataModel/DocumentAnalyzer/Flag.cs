namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System;
    using Newtonsoft.Json;

    /// <summary>
    /// flag
    /// </summary>
    public class Flag
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the flag code.
        /// </summary>
        [JsonProperty("flagCode")]
        public string FlagCode { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the link.
        /// </summary>
        [JsonProperty("link")]
        public Uri Link { get; set; }

        /// <summary>
        /// Gets or sets the metadata.
        /// </summary>
        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }
    }
}
