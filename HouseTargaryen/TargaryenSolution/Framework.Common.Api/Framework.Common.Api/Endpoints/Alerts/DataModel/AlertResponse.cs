namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The alert response.
    /// </summary>
    [DataContract]
    public class AlertResponse
    {
        /// <summary>
        /// Gets or sets the alerts
        /// </summary>
        [DataMember(Name = "alerts")]
        public List<Alert> Alerts { get; set; }

        /// <summary>
        /// Gets or sets the alert reports
        /// </summary>
        [DataMember(Name = "reports")]
        public object Reports { get; set; }

        /// <summary>
        /// Gets or sets the alert total count
        /// </summary>
        [DataMember(Name = "totalCount")]
        public long TotalCount { get; set; }
    }
}
