namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Part of Image search Results Response
	/// </summary>
	public class Result
	{
		/// <summary>
		/// The Id
		/// </summary>
		[JsonProperty("id")]
		public string Id { get; set; }

		/// <summary>
		/// The Name
		/// </summary>
		[JsonProperty("name")]
		public string Name { get; set; }

		/// <summary>
		/// The Index
		/// </summary>
		[JsonProperty("index")]
		public long Index { get; set; }

		/// <summary>
		/// The Created date
		/// </summary>
		[JsonProperty("createdDate")]
		public string CreatedDate { get; set; }

		/// <summary>
		/// The orchestration data
		/// </summary>
		[JsonProperty("orchestrationData")]
		public OrchestrationData OrchestrationData { get; set; }
	}
}