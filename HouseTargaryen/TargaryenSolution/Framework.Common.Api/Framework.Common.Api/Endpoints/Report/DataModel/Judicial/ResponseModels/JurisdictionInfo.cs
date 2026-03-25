namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// JurisdictionInfo
    /// </summary>
    public class JurisdictionInfo
    {
        /// <summary>
        /// id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// path
        /// </summary>
        [JsonProperty("path")]
        public string Path { get; set; }
    }
}
