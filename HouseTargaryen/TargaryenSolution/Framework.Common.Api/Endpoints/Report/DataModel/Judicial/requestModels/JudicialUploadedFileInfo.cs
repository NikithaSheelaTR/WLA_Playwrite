
namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.RequestModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// file info
    /// </summary>
    public class JudicialUploadedFileInfo
    {
        /// <summary>
        /// File name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Report id
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
