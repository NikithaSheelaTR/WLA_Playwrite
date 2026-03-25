namespace Framework.Common.Api.Endpoints.Search.DataModel.FocusHighlighting
{
    using Newtonsoft.Json;

    /// <summary>
    /// Field of ResultSection 
    /// </summary>
    public class ListItem
    {
        /// <summary>
        /// Gets or sets snippets array
        /// </summary>
        [JsonProperty("snippets", NullValueHandling = NullValueHandling.Ignore)]
        public Snippet[] Snippets { get; set; }

        /// <summary>
        /// Gets or sets the synopsis
        /// </summary>
        [JsonProperty("synopsis", NullValueHandling = NullValueHandling.Ignore)]
        public string Synopsis { get; set; }
    }
}