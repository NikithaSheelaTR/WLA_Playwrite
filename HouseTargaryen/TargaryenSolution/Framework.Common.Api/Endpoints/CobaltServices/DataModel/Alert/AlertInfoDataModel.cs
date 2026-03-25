namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Alert Info Data Model
    /// </summary>
    [DataContract]
    public class AlertInfoDataModel
	{
		/// <summary>
		/// Name
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

		/// <summary>
		/// Owned By
		/// </summary>
		[DataMember(Name = "ownedBy")]
		public string OwnedBy { get; set; }

		/// <summary>
		/// Owned By Last Name
		/// </summary>
		[DataMember(Name = "ownedByLastName")]
		public string OwnedByLastName { get; set; }

		/// <summary>
		/// Frequency
		/// </summary>
		[DataMember(Name = "frequency")]
		public string Frequency { get; set; }

		/// <summary>
		/// Visibility
		/// </summary>
		[DataMember(Name = "visibility")]
		public string Visibility { get; set; }

		/// <summary>
		/// Time Zone
		/// </summary>
		[DataMember(Name = "timeZone")]
		public string TimeZone { get; set; }

		/// <summary>
		/// Partner Defined Properties
		/// </summary>
		[DataMember(Name = "partnerDefinedProperties")]
		public PartnerDefinedProperties PartnerDefinedProperties { get; set; }

		/// <summary>
		/// Client Defined Search Data
		/// </summary>
		[DataMember(Name = "clientDefinedSearchData")]
		public ClientDefinedSearchData ClientDefinedSearchData { get; set; }

		/// <summary>
		/// Indexable Data
		/// </summary>
		[DataMember(Name = "indexableData")]
		public IndexableData IndexableData { get; set; }

		/// <summary>
		/// Schedules
		/// </summary>
		[DataMember(Name = "schedules")]
		public List<Schedule> Schedules { get; set; }

		/// <summary>
		/// Allow Triggers
		/// </summary>
		[DataMember(Name = "allowTriggers")]
		public bool AllowTriggers { get; set; }

		/// <summary>
		/// Id
		/// </summary>
		[DataMember(Name = "id")]
		public string Id { get; set; }

		/// <summary>
		/// Prism Entry Guid
		/// </summary>
		[DataMember(Name = "prismEntryGuid")]
		public string PrismEntryGuid { get; set; }

		/// <summary>
		/// Owned By Prism Guid
		/// </summary>
		[DataMember(Name = "ownedByPrismGuid")]
		public string OwnedByPrismGuid { get; set; }

		/// <summary>
		/// Owner Summary
		/// </summary>
		[DataMember(Name = "ownerSummary")]
		public string OwnerSummary { get; set; }

		/// <summary>
		/// Last Ran Successfully On
		/// </summary>
		[DataMember(Name = "lastRanSuccessfullyOn")]
		public string LastRanSuccessfullyOn { get; set; }

		/// <summary>
		/// Is Active
		/// </summary>
		[DataMember(Name = "isActive")]
		public bool IsActive { get; set; }

		/// <summary>
		/// Error Count
		/// </summary>
		[DataMember(Name = "errorCount")]
		public int ErrorCount { get; set; }

		/// <summary>
		/// Created By
		/// </summary>
		[DataMember(Name = "createdBy")]
		public string CreatedBy { get; set; }

		/// <summary>
		/// Created Date
		/// </summary>
		[DataMember(Name = "createdDate")]
		public string CreatedDate { get; set; }

		/// <summary>
		/// Modified By
		/// </summary>
		[DataMember(Name = "modifiedBy")]
		public string ModifiedBy { get; set; }

		/// <summary>
		/// Modified Date
		/// </summary>
		[DataMember(Name = "modifiedDate")]
		public string ModifiedDate { get; set; }

		/// <summary>
		/// Resource Status
		/// </summary>
		[DataMember(Name = "resourceStatus")]
		public string ResourceStatus { get; set; }

		/// <summary>
		/// Subscriptions
		/// </summary>
		[DataMember(Name = "subscriptions")]
		public List<Subscription> Subscriptions { get; set; }

		/// <summary>
		/// Modified Token
		/// </summary>
		[DataMember(Name = "modifiedToken")]
		public string ModifiedToken { get; set; }

		/// <summary>
		/// Alert Type
		/// </summary>
		[DataMember(Name = "alertType")]
		public string AlertType { get; set; }

		/// <summary>
		/// Product Name
		/// </summary>
		[DataMember(Name = "productName")]
		public string ProductName { get; set; }
	}
}
