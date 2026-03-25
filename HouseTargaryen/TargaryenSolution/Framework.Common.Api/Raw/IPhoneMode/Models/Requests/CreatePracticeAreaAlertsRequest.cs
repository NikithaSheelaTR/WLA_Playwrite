namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// Create Practice Area Alerts Request
    /// </summary>
    [DataContract]
    public class CreatePracticeAreaAlertsRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreatePracticeAreaAlertsRequest"/> class. 
        /// </summary>
        /// <param name="practiceAreas"> practice Areas </param>
        public CreatePracticeAreaAlertsRequest(List<PracticeAreasItem> practiceAreas)
        {
            this.PracticeAreas = practiceAreas;
        }

        /// <summary>
        /// Gets or sets the practice areas.
        /// </summary>
        [DataMember(Name = "practiceAreas")]
        public List<PracticeAreasItem> PracticeAreas { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, CreatePracticeAreaAlertsRequest>(this);
        }
    }

    /// <summary>
    /// PracticeAreasItem
    /// Examples: {"practiceAreas":[{"practiceArea":"Bankruptcy","clientId":"REGRESSION","alertContent":"NEWS_AND_ANALYSIS"}]}
    /// </summary>
    [DataContract]
    public class PracticeAreasItem
    {
        /// <summary>
        /// Gets or sets the alert content.
        /// </summary>
        [DataMember(Name = "alertContent")]
        public string AlertContent { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets the practice area.
        /// </summary>
        [DataMember(Name = "practiceArea")]
        public string PracticeArea { get; set; }
    }
}