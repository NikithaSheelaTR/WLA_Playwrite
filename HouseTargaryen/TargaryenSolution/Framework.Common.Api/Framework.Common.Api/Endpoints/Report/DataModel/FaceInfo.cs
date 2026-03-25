namespace Framework.Common.Api.Endpoints.Report.DataModel
{
    using Newtonsoft.Json;

    /// <summary>
    /// The jurisdiction.
    /// </summary>
    public class FaceInfo
    {
        /// <summary>
        /// Gets or sets the Id.
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the Path.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}