namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Initial parameters to start image search process
	/// </summary>
	public class StartImageSearchRequest
	{
		/// <summary>
		/// The Expiry
		/// </summary>
		[JsonProperty("expiry")]
		public Expiry Expiry { get; set; }

		/// <summary>
		/// The Orchestration data
		/// </summary>
		[JsonProperty("orchestrationData")]
		public OrchestrationData OrchestrationData { get; set; }
	}
}