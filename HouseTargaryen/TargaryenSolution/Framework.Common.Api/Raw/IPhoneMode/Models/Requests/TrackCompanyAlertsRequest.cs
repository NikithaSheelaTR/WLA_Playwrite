namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// Track Company Alerts Request
    /// </summary>
    [DataContract]
    public class TrackCompanyAlertsRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TrackCompanyAlertsRequest"/> class. 
        /// </summary>
        public TrackCompanyAlertsRequest()
        {
            this.ClientId = "Test";
            this.Companies = new List<Company>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TrackCompanyAlertsRequest"/> class. 
        /// </summary>
        /// <param name="companies">  The companies. </param>
        public TrackCompanyAlertsRequest(List<Company> companies)
        {
            this.ClientId = "Test";
            this.Companies = companies;
        }

        /// <summary>
        /// ClientId
        /// </summary>
        [DataMember(Name = "clientId")]
        public string ClientId { get; set; }

        /// <summary>
        /// Companies
        /// </summary>
        [DataMember(Name = "companies")]
        public List<Company> Companies { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, TrackCompanyAlertsRequest>(this);
        }
    }

    /// <summary>
    /// Company class and its properties
    /// </summary>
    [DataContract]
    public class Company
    {
        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the search term.
        /// </summary>
        [DataMember(Name = "searchTerm")]
        public string SearchTerm { get; set; }

        /// <summary>
        /// Gets or sets the ticker symbol.
        /// </summary>
        [DataMember(Name = "tickerSymbol")]
        public string TickerSymbol { get; set; }

        /// <summary>
        /// Gets or sets the TOC GUID.
        /// </summary>
        [DataMember(Name = "tocGuid")]
        public string TocGuid { get; set; }

        /// <summary>
        /// Gets or sets the topic code.
        /// </summary>
        [DataMember(Name = "topicCode")]
        public string TopicCode { get; set; }
    }
}