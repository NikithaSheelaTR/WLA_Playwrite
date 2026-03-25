namespace Framework.Common.Api.Endpoints.AlertProductService.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Subscription data model.
    /// </summary>
    [DataContract]
    public class Subscription
    {
        /// <summary>
        /// Guid
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Alert Guid
        /// </summary>
        [DataMember(Name = "alertGuid")]
        public string AlertGuid { get; set; }

        /// <summary>
        /// User Guid
        /// </summary>
        [DataMember(Name = "userGuid")]
        public string UserGuid { get; set; }

        /// <summary>
        /// Client Id
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Delivery Destination
        /// </summary>
        [DataMember(Name = "deliveryDestination")]
        public DeliveryDestination DeliveryDestination { get; set; }

        /// <summary>
        /// Status Code
        /// </summary>
        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// No Docs Message
        /// </summary>
        [DataMember(Name = "noDocsMessage")]
        public bool NoDocsMessage { get; set; }

        /// <summary>
        /// Format
        /// </summary>
        [DataMember(Name = "format")]
        public string Format { get; set; }

        /// <summary>
        /// Stylesheet Transformation Parameters
        /// </summary>
        [DataMember(Name = "stylesheetTransformationParameters")]
        public Dictionary<string, object> StylesheetTransformationParameters { get; set; }

        /// <summary>
        /// Result History
        /// </summary>
        [DataMember(Name = "resultHistory")]
        public bool ResultHistory { get; set; }

        /// <summary>
        /// Modified Token
        /// </summary>
        [DataMember(Name = "modifiedToken")]
        public string ModifiedToken { get; set; }
    }
}
