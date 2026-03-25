namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// AlertsToDelete models the expected body for  '/Alert/v2/alerts/deleteAlerts'
    /// </summary>
    public class DeleteAlertsRequest : IRequest
    {
        /// <summary>
        /// The alerts to delete.
        /// </summary>
        private readonly List<AlertsToDelete> alertsToDelete;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteAlertsRequest"/> class. 
        /// </summary>
        /// <param name="alertsToDelete">
        /// alerts To Delete 
        /// </param>
        public DeleteAlertsRequest(List<AlertsToDelete> alertsToDelete)
        {
            this.alertsToDelete = alertsToDelete;
        }

        /// <summary>
        /// Get Request Serializes the list of alerts 
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return
                ObjectSerializer.SerializeObject<DataContractJsonSerializer, List<AlertsToDelete>>(this.alertsToDelete);
        }
    }

    /// <summary>
    /// Alerts To Delete
    /// </summary>
    [DataContract]
    public class AlertsToDelete
    {
        /// <summary>
        /// Gets or sets the alert GUIDs.
        /// </summary>
        [DataMember(Name = "alertGuids")]
        public IList<string> AlertGuids { get; set; }

        /// <summary>
        /// Gets or sets the alert type.
        /// </summary>
        [DataMember(Name = "alertType")]
        public string AlertType { get; set; }
    }
}