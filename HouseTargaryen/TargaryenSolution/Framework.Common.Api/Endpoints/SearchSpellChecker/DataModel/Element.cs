namespace Framework.Common.Api.Endpoints.SearchSpellChecker.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Element
    /// </summary>
    public class Element
    {
        /// <summary>
        ///  Gets or sets IsSuggestionOffered
        /// </summary>
        [JsonProperty("IsSuggestionOffered")]
        public bool IsSuggestionOffered { get; set; }

        /// <summary>
        ///  Gets or sets NewValue
        /// </summary>
        [JsonProperty("newValue")]
        public string NewValue { get; set; }

        /// <summary>
        ///  Gets or sets OriginalValue
        /// </summary>
        [JsonProperty("originalValue")]
        public string OriginalValue { get; set; }
    }
}
