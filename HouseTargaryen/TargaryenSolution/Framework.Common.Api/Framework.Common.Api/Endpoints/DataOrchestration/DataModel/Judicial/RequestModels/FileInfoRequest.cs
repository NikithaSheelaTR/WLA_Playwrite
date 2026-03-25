namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// File info
    /// </summary>
    public class FileInfoRequest
    {
        /// <summary>
        /// file name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// file if
        /// /Report/BriefAnalyzer/v1/Judicial/Upload/File return this id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
