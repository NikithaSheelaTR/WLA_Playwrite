namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

	/// <summary>
	/// Brief Alert Info Data Model
	/// </summary>
	[DataContract]
	public class BriefAlertInfoDataModel
	{
		/// <summary>
		/// Alerts
		/// </summary>
		[DataMember(Name = "alerts")]
		public List<Alert> Alerts { get; set; }

		/// <summary>
		/// Pagination
		/// </summary>
		[DataMember(Name = "pagination")]
		public Pagination Pagination { get; set; }
	}
}
