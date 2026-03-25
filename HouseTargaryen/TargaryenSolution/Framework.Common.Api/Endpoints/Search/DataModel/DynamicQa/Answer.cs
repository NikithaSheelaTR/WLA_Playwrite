namespace Framework.Common.Api.Endpoints.Search.DataModel.DynamicQa
{
    using Newtonsoft.Json;

    /// <summary>
    /// The answer.
    /// </summary>
    public class Answer
    {
        /// <summary>
        /// Gets or sets the source document.
        /// </summary>
        [JsonProperty("sourceDocument")]
        public SourceDocument SourceDocument { get; set; }
    }
}
