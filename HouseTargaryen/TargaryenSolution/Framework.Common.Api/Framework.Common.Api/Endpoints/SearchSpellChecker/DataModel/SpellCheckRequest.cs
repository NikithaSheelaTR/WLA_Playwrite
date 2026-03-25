namespace Framework.Common.Api.Endpoints.SearchSpellChecker.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// Request for SpellCheck endpoint
    /// </summary>
    public class SpellCheckRequest
    {
        /// <summary>
        ///  Gets or sets terms
        /// </summary>
        [JsonProperty("terms")]
        public string Terms { get; set; }

        /// <summary>
        ///  Gets or sets ignoreCheckSpecialChar
        /// </summary>
        [JsonProperty("ignoreCheckSpecialChar")]
        public bool IgnoreCheckSpecialChar { get; set; } = true;
    }
}
