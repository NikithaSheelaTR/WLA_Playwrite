namespace Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch
{
	using Newtonsoft.Json;

	/// <summary>
	/// Image search status progress response
	/// </summary>
	public class ImageSearchStatusResponse
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
		/// The Orchesstration
		/// </summary>
		[JsonProperty("orchestration")]
		public string Orchestration { get; set; }

		/// <summary>
		/// The Document count
		/// </summary>
		[JsonProperty("documentCount")]
		public long DocumentCount { get; set; }

		/// <summary>
		/// The Created Date
		/// </summary>
		[JsonProperty("createdDate")]
		public string CreatedDate { get; set; }

		/// <summary>
		/// The Update date
		/// </summary>
		[JsonProperty("updatedDate")]
		public string UpdatedDate { get; set; }

		/// <summary>
		/// The Expiry
		/// </summary>
		[JsonProperty("expiry")]
		public Expiry Expiry { get; set; }

		/// <summary>
		/// The State
		/// </summary>
		[JsonProperty("state")]
		public string State { get; set; }
	}
}