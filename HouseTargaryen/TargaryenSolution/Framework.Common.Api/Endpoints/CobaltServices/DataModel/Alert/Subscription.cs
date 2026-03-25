namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel.Alert
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Subscription data model
    /// </summary>
    [DataContract]
    public class Subscription
    {
        /// <summary>
        /// User Entered Client Id
        /// </summary>
        [DataMember(Name = "userEnteredClientId")]
        public string UserEnteredClientId { get; set; }

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
        /// Subscription Name
        /// </summary>
        [DataMember(Name = "subscriptionName")]
        public string SubscriptionName { get; set; }

        /// <summary>
        /// Destination
        /// </summary>
        [DataMember(Name = "destination")]
        public Destination Destination { get; set; }
    }
}
