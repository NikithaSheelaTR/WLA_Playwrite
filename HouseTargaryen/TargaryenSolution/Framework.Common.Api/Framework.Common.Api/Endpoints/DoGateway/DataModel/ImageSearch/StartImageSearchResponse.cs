namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Response to the initiation of image search
	/// </summary>
	public class StartImageSearchResponse
	{
		/// <summary>
		/// The Result List GUID
		/// </summary>
		[JsonProperty("resultListGUID")]
		public string ResultListGUID { get; set; }
	}
}
