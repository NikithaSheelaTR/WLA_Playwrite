namespace Framework.Common.Api.Endpoints.Report.DataModel.Judicial.RequestModels
{
    using System.Collections.Generic;

    using Newtonsoft.Json;
  
    /// <summary>
    /// Info to start check 
    /// </summary>
    public class JudicialStartCheckInfo
    {
        /// <summary>
        /// report name
        /// </summary>
        [JsonProperty("reportName")]
        public string ReportName { get; set; }

        /// <summary>
        ///parties
        /// </summary>
        [JsonProperty("parties")]
        public List<JudicialPartyInfo> Parties { get; set; }
    }
}
