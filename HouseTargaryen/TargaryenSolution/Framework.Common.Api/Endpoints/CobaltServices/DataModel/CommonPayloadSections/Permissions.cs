namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.CommonPayloadSections
{
    using Newtonsoft.Json;

    /// <summary>
    /// The permissions.
    /// </summary>
    public class Permissions
    {
        /// <summary>
        /// Gets or sets a value indicating whether read.
        /// </summary>
        [JsonProperty("read")]
        public bool Read { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether update.
        /// </summary>
        [JsonProperty("update")]
        public bool Update { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether delete.
        /// </summary>
        [JsonProperty("delete")]
        public bool Delete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether add contents.
        /// </summary>
        [JsonProperty("addContents")]
        public bool AddContents { get; set; }
    }
}