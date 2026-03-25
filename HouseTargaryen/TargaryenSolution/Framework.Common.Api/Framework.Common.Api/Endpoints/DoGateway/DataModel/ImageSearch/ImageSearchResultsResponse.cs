namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// Image search results response
	/// </summary>
	public class ImageSearchResultsResponse
	{
		/// <summary>
		/// The Results
		/// </summary>
		[JsonProperty("results")]
		public List<Result> Results { get; set; }
	}
}
