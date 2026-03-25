namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.DocumentAnalyzer
{
    using System;
    using Newtonsoft.Json;
    
    /// <summary>
    /// Count
    /// </summary>
    public class Count
    {
        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [JsonProperty("categoryId")]
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category url.
        /// </summary>
        [JsonProperty("categoryUrl")]
        public Uri CategoryUrl { get; set; }

        /// <summary>
        /// Gets or sets the count count.
        /// </summary>
        [JsonProperty("count")]
        public long CountCount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        [JsonProperty("displayName")]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is selected.
        /// </summary>
        [JsonProperty("isSelected")]
        public bool IsSelected { get; set; }

        /// <summary>
        /// Gets or sets the unique id.
        /// </summary>
        [JsonProperty("uniqueId")]
        public string UniqueId { get; set; }
    }
}
