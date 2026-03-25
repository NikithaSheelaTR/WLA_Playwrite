namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;


    /// <summary>
    /// The Specific Alert data model
    /// </summary>
    [DataContract]
	public class SpecificAlertDataModel
	{
        /// <summary>
        /// Comments
        /// </summary>
        [DataMember(Name = "comments")]
		public string Comments { get; set; }

        /// <summary>
        /// Created Date
        /// </summary>
        [DataMember(Name = "createdDate")]
		public string CreatedDate { get; set; }

        /// <summary>
        /// Expire Date
        /// </summary>
		[DataMember(Name = "expireDate")]
		public string ExpireDate { get; set; }

        /// <summary>
        /// End Date
        /// </summary>
		[DataMember(Name = "endDate")]
		public string EndDate { get; set; }

        /// <summary>
        /// Guid
        /// </summary>
		[DataMember(Name = "guid")]
		public string Guid { get; set; }

        /// <summary>
        /// DataRoom Alert Guid
        /// </summary>
		[DataMember(Name = "dataRoomAlertGuid")]
		public string DataRoomAlertGuid { get; set; }

        /// <summary>
        /// Last Run Date
        /// </summary>
		[DataMember(Name = "lastRunDate")]
		public string LastRunDate { get; set; }

        /// <summary>
        /// Last Ran Successfully On
        /// </summary>
		[DataMember(Name = "lastRanSuccessfullyOn")]
		public string LastRanSuccessfullyOn { get; set; }

        /// <summary>
        /// Modified Token
        /// </summary>
		[DataMember(Name = "modifiedToken")]
		public string ModifiedToken { get; set; }

        /// <summary>
        /// Locale
        /// </summary>
		[DataMember(Name = "locale")]
		public object Locale { get; set; }

        /// <summary>
        /// Name
        /// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; }

        /// <summary>
        /// Next Run Date
        /// </summary>
		[DataMember(Name = "nextRunDate")]
		public long NextRunDate { get; set; }

        /// <summary>
        /// Partner Defined Properties
        /// </summary>
		[DataMember(Name = "partnerDefinedProperties")]
		public PartnerDefinedProperties PartnerDefinedProperties { get; set; }

        /// <summary>
        /// Alert Type
        /// </summary>
		[DataMember(Name = "alertType")]
		public string AlertType { get; set; }

        /// <summary>
        /// Resume Date
        /// </summary>
		[DataMember(Name = "resumeDate")]
		public string ResumeDate { get; set; }

        /// <summary>
        /// Subscriptions
        /// </summary>
		[DataMember(Name = "subscriptions")]
		public List<Subscription> Subscriptions { get; set; }

        /// <summary>
        /// Schedule Infos
        /// </summary>
		[DataMember(Name = "scheduleInfos")]
		public List<ScheduleInfo> ScheduleInfos { get; set; }

        /// <summary>
        /// Status
        /// </summary>
		[DataMember(Name = "status")]
		public string Status { get; set; }

        /// <summary>
        /// Suspend Date
        /// </summary>
		[DataMember(Name = "suspendDate")]
		public string SuspendDate { get; set; }

        /// <summary>
        /// Time Zone
        /// </summary>
		[DataMember(Name = "timeZone")]
		public string TimeZone { get; set; }

        /// <summary>
        /// User Guid
        /// </summary>
		[DataMember(Name = "userGuid")]
		public string UserGuid { get; set; }

        /// <summary>
        /// Allow Triggers
        /// </summary>
		[DataMember(Name = "allowTriggers")]
		public bool AllowTriggers { get; set; }

        /// <summary>
        /// App Specific Metadata
        /// </summary>
		[DataMember(Name = "appSpecificMetadata")]
		public string AppSpecificMetadata { get; set; }

        /// <summary>
        /// Error Count
        /// </summary>
        [DataMember(Name = "errorCount")]
		public int ErrorCount { get; set; }

        /// <summary>
        /// Indexable Data
        /// </summary>
        [DataMember(Name = "indexableData")]
		public IndexableData IndexableData { get; set; }

        /// <summary>
        /// Product View
        /// </summary>
        [DataMember(Name = "productView")]
		public string ProductView { get; set; }
	}
}
