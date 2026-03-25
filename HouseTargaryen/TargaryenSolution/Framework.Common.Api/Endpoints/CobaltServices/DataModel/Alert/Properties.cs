namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Properties data model
    /// </summary>
    [DataContract]
    public class Properties
    {
        /// <summary>
        /// Email Alerts Out Of Plan History
        /// </summary>
        [DataMember(Name = "email_alertsOutOfPlanHistory")]
        public string EmailAlertsOutOfPlanHistory { get; set; }

        /// <summary>
        /// Result Format
        /// </summary>
        [DataMember(Name = "ResultFormat")]
        public string ResultFormat { get; set; }

        /// <summary>
        /// Delivery Info Json
        /// </summary>
        [DataMember(Name = "deliveryInfoJson")]
        public string DeliveryInfoJson { get; set; }
    }
}
