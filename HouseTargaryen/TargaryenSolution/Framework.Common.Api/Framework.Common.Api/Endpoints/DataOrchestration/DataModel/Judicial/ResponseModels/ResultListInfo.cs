namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.ResponseModels
{
    using Newtonsoft.Json;

    /// <summary>
    /// Judicial Result list info
    /// </summary>
    public class ResultListInfo
    {
        /// <summary>
        /// Result List GUID
        /// </summary>
        [JsonProperty("resultListGUID")]      
        public string ResultListGuid { get; set; }
    }
}
