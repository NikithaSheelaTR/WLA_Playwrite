namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Alert data model
    /// </summary>
    [DataContract]
    public class Alert
    {
        /// <summary>
        /// Id
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Prism entry guid
        /// </summary>
        [DataMember(Name = "prismEntryGuid")]
        public string PrismEntryGuid { get; set; }

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
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Resource Status
        /// </summary>
        [DataMember(Name = "resourceStatus")]
        public string ResourceStatus { get; set; }

        /// <summary>
        /// Visibility
        /// </summary>
        [DataMember(Name = "visibility")]
        public string Visibility { get; set; }

        /// <summary>
        /// Alert Type
        /// </summary>
        [DataMember(Name = "alertType")]
        public string AlertType { get; set; }

        /// <summary>
        /// User Entered Client Id
        /// </summary>
        [DataMember(Name = "userEnteredClientId")]
        public string UserEnteredClientId { get; set; }

        /// <summary>
        /// Frequency
        /// </summary>
        [DataMember(Name = "frequency")]
        public string Frequency { get; set; }

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
        /// Product Name
        /// </summary>
        [DataMember(Name = "productName")]
        public string ProductName { get; set; }
    }
}
