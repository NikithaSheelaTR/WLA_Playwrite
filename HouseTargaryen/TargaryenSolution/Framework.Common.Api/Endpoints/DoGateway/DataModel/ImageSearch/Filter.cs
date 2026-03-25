namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using System.Collections.Generic;
	using Newtonsoft.Json;

	/// <summary>
	/// Part of Image Search Results Request
	/// </summary>
	public class Filter
	{
		/// <summary>
		/// The List of Results
		/// </summary>
		[JsonProperty("results")]
		public List<Results> Results { get; set; }
	}
}