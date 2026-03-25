namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Image search results request
	/// </summary>
	public class ImageSearchResultsRequest
	{
		/// <summary>
		/// The Filter
		/// </summary>
		[JsonProperty("filter")]
		public Filter Filter { get; set; }
	}
}
