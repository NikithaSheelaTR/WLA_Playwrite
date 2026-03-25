namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Dockets Pdfs batch info model
    /// </summary>
    [DataContract]
    public class DocketsPdfsBatchInfoModel
    {
        /// <summary>
        /// Warning block types
        /// </summary>
        [DataMember(Name = "warningBlockTypes")]
        public List<string> WarningBlockTypes { get; set; }

        /// <summary>
        /// Warning reporting names
        /// </summary>
        [DataMember(Name = "warningReportingNames")]
        public string WarningReportingNames { get; set; }

        /// <summary>
        /// Charge amount
        /// </summary>
        [DataMember(Name = "chargeAmount")]
        public string ChargeAmount { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; }

        /// <summary>
        /// Case number
        /// </summary>
        [DataMember(Name = "caseNumber")]
        public string CaseNumber { get; set; }

        /// <summary>
        /// Court
        /// </summary>
        [DataMember(Name = "court")]
        public string Court { get; set; }

        /// <summary>
        /// Court norm
        /// </summary>
        [DataMember(Name = "courtNorm")]
        public string CourtNorm { get; set; }

        /// <summary>
        /// Court number
        /// </summary>
        [DataMember(Name = "courtNumber")]
        public string CourtNumber { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Correlation Id
        /// </summary>
        [DataMember(Name = "correlationId")]
        public string CorrelationId { get; set; }

        /// <summary>
        /// Allow multipart
        /// </summary>
        [DataMember(Name = "allowMultipart")]
        public string AllowMultipart { get; set; }

        /// <summary>
        /// Is multipart
        /// </summary>
        [DataMember(Name = "isMultipart")]
        public bool IsMultipart { get; set; }

        /// <summary>
        /// From multipart
        /// </summary>
        [DataMember(Name = "fromMultipart")]
        public bool FromMultipart { get; set; }

        /// <summary>
        /// Exhibit items
        /// </summary>
        [DataMember(Name = "exhibitItems")]
        public List<object> ExhibitItems { get; set; }

        /// <summary>
        /// Entry number
        /// </summary>
        [DataMember(Name = "entryNumber")]
        public string EntryNumber { get; set; }

        /// <summary>
        /// Attachments
        /// </summary>
        [DataMember(Name = "attachments")]
        public bool Attachments { get; set; }

        /// <summary>
        /// Has exhibits
        /// </summary>
        [DataMember(Name = "hasExhibits")]
        public bool HasExhibits { get; set; }

        /// <summary>
        /// Is sealed
        /// </summary>
        [DataMember(Name = "isSealed")]
        public bool IsSealed { get; set; }

        /// <summary>
        /// Has error
        /// </summary>
        [DataMember(Name = "hasError")]
        public bool HasError { get; set; }

        /// <summary>
        /// Platform
        /// </summary>
        [DataMember(Name = "platform")]
        public string Platform { get; set; }

        /// <summary>
        /// Number
        /// </summary>
        [DataMember(Name = "number")]
        public string Number { get; set; }

        /// <summary>
        /// Index
        /// </summary>
        [DataMember(Name = "index")]
        public string Index { get; set; }

        /// <summary>
        /// Local Image Guid
        /// </summary>
        [DataMember(Name = "localImageGuid")]
        public string LocalImageGuid { get; set; }
    }
}
