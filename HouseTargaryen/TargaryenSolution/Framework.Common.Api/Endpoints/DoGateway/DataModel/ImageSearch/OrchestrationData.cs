namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

	/// <summary>
	/// Part of Image search Results Response
	/// </summary>
	public class OrchestrationData
	{
		/// <summary>
		/// The DataRoom image id
		/// </summary>
		[JsonProperty("dataRoomId")]
		public string DataRoomId { get; set; }

		/// <summary>
		/// Orchestration Result Guid
		/// </summary>
		[JsonProperty("guid")]
		[JsonIgnore]
		public string Guid { get; set; }

		/// <summary>
		/// AWS search result rank
		/// </summary>
		[JsonProperty("score")]
		[JsonIgnore]
		public string Score { get; set; }

        /// <summary>
        /// Jurisdictions list
        /// </summary>
        [JsonProperty("jurisdictions")]
        public List<string> Jurisdictions { get; set; }
	}
}