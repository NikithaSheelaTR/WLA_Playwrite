namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// The document info.
    /// </summary>
    public class DocumentKeyInfo
    {
        /// <summary>
        /// Gets or sets the document guid.
        /// </summary>
        [JsonProperty("documentGuid")]
        public string DocumentGuid { get; set; }
    }
}
