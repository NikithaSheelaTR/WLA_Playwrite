namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Part of Image Search Start Request
	/// </summary>
	public class Expiry
	{
		/// <summary>
		/// The Type
		/// </summary>
		[JsonProperty("type")]
		public string Type { get; set; }
	}
}