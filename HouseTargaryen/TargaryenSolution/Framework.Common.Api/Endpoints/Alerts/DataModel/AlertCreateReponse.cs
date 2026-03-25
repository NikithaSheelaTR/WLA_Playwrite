using System.Runtime.Serialization;

namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    /// <summary>
    /// Datacontract for alert creation response
    /// </summary>
    [DataContract]
    public class AlertCreateReponse
    {
        /// <summary>
        /// The guid of created alert
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// The name of created alert
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Is newsletter updated
        /// </summary>
        [DataMember(Name = "newsLettersUpdated")]
        public bool NewsLettersUpdated { get; set; }
    }
}
