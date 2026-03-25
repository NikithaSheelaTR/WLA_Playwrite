namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Docket batch info model; response data of api call '/V1/Docket/Update/Batch'; request data of api call '/V1/Docket/Update'
    /// </summary>
    [DataContract]
    public class DocketBatchInfoModel
    {
        /// <summary>
        /// Additional parameters
        /// </summary>
        [DataMember(Name = "additionalParms")]
        public AdditionalParms AdditionalParms { get; set; }

        /// <summary>
        /// can update
        /// </summary>
        [DataMember(Name = "canUpdate")]
        public bool CanUpdate { get; set; }

        /// <summary>
        /// Case number
        /// </summary>
        [DataMember(Name = "caseNumber")]
        public string CaseNumber { get; set; }

        /// <summary>
        /// Case title
        /// </summary>
        [DataMember(Name = "caseTitle")]
        public string CaseTitle { get; set; }

        /// <summary>
        /// Docket Guid
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Sign on
        /// </summary>
        [DataMember(Name = "signon")]
        public string Signon { get; set; }

        /// <summary>
        /// Slow court
        /// </summary>
        [DataMember(Name = "slowCourt")]
        public bool SlowCourt { get; set; }

        /// <summary>
        /// Warning block types
        /// </summary>
        [DataMember(Name = "warningBlockTypes")]
        public object WarningBlockTypes { get; set; }

        /// <summary>
        /// Update warning block types
        /// </summary>
        [DataMember(Name = "updateWarningBlockTypes")]
        public object UpdateWarningBlockTypes { get; set; }

        /// <summary>
        /// List of page source keys
        /// </summary>
        [DataMember(Name = "listPageSourceKey")]
        public string ListPageSourceKey { get; set; }

        /// <summary>
        /// search id
        /// </summary>
        [DataMember(Name = "searchId")]
        public string SearchId { get; set; }

        /// <summary>
        /// Charge amount
        /// </summary>
        [DataMember(Name = "chargeAmount")]
        public string ChargeAmount { get; set; }

        /// <summary>
        /// Context data
        /// </summary>
        [DataMember(Name = "contextData")]
        public string contextData { get; set; }
    }
}
