namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.RequestModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
   
    /// <summary>
    /// party info
    /// </summary>
    public class JudicialPartyInfo
    {
        /// <summary>
        /// Party name
        /// </summary>
        [JsonProperty("name") ]
        public string Name { get; set; }

        /// <summary>
        /// Files to be analyzed
        /// </summary>
        [JsonProperty("files")]
        public List<JudicialUploadedFileInfo> Files { get; set; }
    }
}
