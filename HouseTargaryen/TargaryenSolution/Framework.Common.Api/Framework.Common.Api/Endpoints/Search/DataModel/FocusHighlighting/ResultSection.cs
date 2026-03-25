namespace Framework.Common.Api.Endpoints.Search.DataModel.FocusHighlighting
{
    using Newtonsoft.Json;

    /// <summary>
    /// The result section (the property of SearchV1Responce)
    /// </summary>
    public class ResultSection
    {
        /// <summary>
        /// Gets or sets the header
        /// </summary>
        [JsonProperty("header")]
        public string Header { get; set; }

        /// <summary>
        /// Gets or sets the item count
        /// </summary>
        [JsonProperty("itemCount")]
        public string ItemCount { get; set; }

        /// <summary>
        /// Gets or sets the result type
        /// </summary>
        [JsonProperty("resultType")]
        public string ResultType { get; set; }

        /// <summary>
        /// Gets or sets the result type key
        /// </summary>
        [JsonProperty("resultTypeKey")]
        public string ResultTypeKey { get; set; }

        /// <summary>
        /// Gets or sets the title name
        /// </summary>
        [JsonProperty("titleName")]
        public string TitleName { get; set; }

        /// <summary>
        /// Gets or sets the view name
        /// </summary>
        [JsonProperty("viewName")]
        public string ViewName { get; set; }

        /// <summary>
        /// Gets or sets the section result link
        /// </summary>
        [JsonProperty("sectionResultLink")]
        public string SectionResultLink { get; set; }

        /// <summary>
        /// Gets or sets the single content type
        /// </summary>
        [JsonProperty("isSingleContentType")]
        public string IsSingleContentType { get; set; }

        /// <summary>
        /// Gets or sets listItems array
        /// </summary>
        [JsonProperty("listItems")]
        public ListItem[] ListItems { get; set; }

        /// <summary>
        /// Gets or sets the query concepts
        /// </summary>
        [JsonProperty("queryConcepts", NullValueHandling = NullValueHandling.Ignore)]
        public QueryConcept[] QueryConcepts { get; set; }
    }
}