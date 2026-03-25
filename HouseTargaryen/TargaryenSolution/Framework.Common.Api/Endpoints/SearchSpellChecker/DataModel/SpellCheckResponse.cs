namespace Framework.Common.Api.Endpoints.SearchSpellChecker.DataModel
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    /// <summary>
    /// Response for SpellCheck endpoint
    /// </summary>
    public class SpellCheckResponse
    {
        /// <summary>
        ///  Gets or sets elements
        /// </summary>
        [JsonProperty("elements")]
        public List<Element> Elements { get; set; }

        /// <summary>
        ///  Gets or sets HasSuggestions
        /// </summary>
        [JsonProperty("hasSuggestions")]
        public bool HasSuggestions { get; set; }
    }
}
