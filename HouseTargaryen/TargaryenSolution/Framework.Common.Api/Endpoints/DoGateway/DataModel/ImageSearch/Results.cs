namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Part of Image Search Results Request
	/// </summary>
	public class Results
	{
		/// <summary>
		/// The Index
		/// </summary>
		[JsonProperty("index")]
		public string Index { get; set; }
	}
}