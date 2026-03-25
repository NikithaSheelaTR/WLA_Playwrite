namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// GroupedAlertsRequest
    /// </summary>
    [DataContract]
    public class GroupedAlertsRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupedAlertsRequest"/> class. 
        /// </summary>
        public GroupedAlertsRequest()
        {
            this.TrackingGroup = new List<TrackingGroupItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GroupedAlertsRequest"/> class. 
        /// </summary>
        /// <param name="trackingItem">
        /// The tracking Item. 
        /// </param>
        public GroupedAlertsRequest(TrackingGroupItem trackingItem)
        {
            this.TrackingGroup = new List<TrackingGroupItem> { trackingItem };
        }

        /// <summary>
        /// trackingGroup
        /// </summary>
        [DataMember(Name = "trackingGroup")]
        public List<TrackingGroupItem> TrackingGroup { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, GroupedAlertsRequest>(this);
        }

        /// <summary>
        /// filterItems
        /// </summary>
        public class FilterItem
        {
            /// <summary>
            /// Gets or sets the search term.
            /// </summary>
            public string SearchTerm { get; set; }

            /// <summary>
            /// Gets or sets the TOC GUID.
            /// </summary>
            public string TocGuid { get; set; }

            /// <summary>
            /// Gets or sets the tracking name.
            /// </summary>
            public string TrackingName { get; set; }
        }

        /// <summary>
        /// TrackingGroupItem
        /// </summary>
        [DataContract]
        public class TrackingGroupItem
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="TrackingGroupItem"/> class. 
            /// </summary>
            /// <param name="name"> name </param>
            /// <param name="filterItem"> filter Item </param>
            public TrackingGroupItem(string name, FilterItem filterItem)
            {
                this.Name = name;
                this.FilterParameters = new List<FilterItem> { filterItem };
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TrackingGroupItem"/> class. 
            /// </summary>
            /// <param name="name"> name </param>
            public TrackingGroupItem(string name)
            {
                this.Name = name;
                this.FilterParameters = new List<FilterItem>();
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="TrackingGroupItem"/> class. 
            /// </summary>
            /// <param name="filterItem"> filter Item </param>
            public TrackingGroupItem(FilterItem filterItem)
            {
                this.FilterParameters = new List<FilterItem> { filterItem };
            }

            /// <summary>
            /// Gets or sets the filter parameters.
            /// </summary>
            [DataMember(Name = "filterParameters")]
            public List<FilterItem> FilterParameters { get; set; }

            /// <summary>
            /// Gets or sets the name.
            /// </summary>
            [DataMember(Name = "name")]
            public string Name { get; set; }
        }
    }
}