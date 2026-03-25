namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// NotificationsRequest
    /// </summary>
    public class NotificationsRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsRequest"/> class. 
        /// </summary>
        /// <param name="startdate"> The start date.  </param>
        /// <param name="offset"> The offset.  </param>
        /// <param name="limit"> The limit.  </param>
        public NotificationsRequest(string startdate = "2016-03-18T05:00:00.000Z", int offset = 1, int limit = 100)
        {
            this.Offset = offset;
            this.Limit = limit;

            this.Body = new NotificationsRequestBody
                            {
                                StartDate = startdate,
                                ExcludeAlertableNamesToFilter = false,
                                ExcludeTypeFilter = false,
                                AlertableNamesToFilter =
                                    new List<string> { "AlertEngine.AlertHasResults" }
                            };
        }

        /// <summary>
        /// Gets or sets Notifications Request Body
        /// </summary>
        public NotificationsRequestBody Body { get; set; }

        /// <summary>
        /// Gets or sets limit
        /// </summary>
        public int Limit { get; set; }

        /// <summary>
        /// Gets or sets offset
        /// </summary>    
        public int Offset { get; set; }

        /// <summary>
        /// Grouped Alerts Request
        /// </summary>
        /// <returns> The Request Body. </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, NotificationsRequestBody>(this.Body);
        }

        /// <summary>
        /// NotificationsRequestBody
        /// </summary>
        [DataContract]
        public class NotificationsRequestBody
        {
            /// <summary>
            /// Gets or sets alert able Names To Filter
            /// </summary>
            [DataMember(Name = "alertableNamesToFilter")]
            public List<string> AlertableNamesToFilter { get; set; }

            /// <summary>
            /// Gets or sets exclude Alert able Names To Filter
            /// </summary>
            [DataMember(Name = "excludeAlertableNamesToFilter")]
            public bool ExcludeAlertableNamesToFilter { get; set; }

            /// <summary>
            /// Gets or sets exclude Type Filter
            /// </summary>
            [DataMember(Name = "excludeTypeFilter")]
            public bool ExcludeTypeFilter { get; set; }

            /// <summary>
            /// Gets or sets start Date
            /// </summary>
            [DataMember(Name = "startDate")]
            public string StartDate { get; set; }
        }
    }
}