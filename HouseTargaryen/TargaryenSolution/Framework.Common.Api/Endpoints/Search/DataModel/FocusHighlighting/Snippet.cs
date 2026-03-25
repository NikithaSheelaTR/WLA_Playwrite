namespace Framework.Common.Api.Endpoints.Search.DataModel.FocusHighlighting
{
    using Newtonsoft.Json;

    /// <summary>
    /// The field of listitem 
    /// </summary>
    public class Snippet
    {
        /// <summary>
        /// Gets or sets the document offset.
        /// </summary>
        [JsonProperty("documentOffset")]
        public string DocumentOffset { get; set; }

        /// <summary>
        /// Gets or sets the snippet fragment
        /// </summary>
        [JsonProperty("snippetFragment")]
        public string SnippetFragment { get; set; }

        /// <summary>
        /// Gets or sets the snippet link
        /// </summary>
        [JsonProperty("snippetLink")]
        public string SnippetLink { get; set; }
    }
}
