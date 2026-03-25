namespace Framework.Common.Api.Endpoints.DataOrchestration.DataModel.Judicial.RequestModels
{
    using Framework.Common.Api.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// SortKeyInfo
    /// </summary>
    public class SortKeyInfo
    {
        /// <summary>
        /// tab
        /// </summary>
        [JsonProperty("tab")]
        public string Tab { get; private set; }

        /// <summary>
        /// clientId
        /// </summary>
        [JsonProperty("clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Set tab option
        /// </summary>
        /// <param name="option">option</param>
        public void SetOption(JudicialTabOption option)
        {
            this.Tab = option.ToString();
        }
    }
}
