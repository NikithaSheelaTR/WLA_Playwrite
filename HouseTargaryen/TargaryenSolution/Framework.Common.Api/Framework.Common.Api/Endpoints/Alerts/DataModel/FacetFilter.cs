using Newtonsoft.Json;
using System.Collections.Generic;

namespace Framework.Common.Api.Endpoints.Alerts.DataModel
{
    /// <summary>
    /// Facet Filter
    /// </summary>
    public class FacetFilter
    {
        /// <summary>
        /// Start
        /// </summary>
        [JsonProperty("start")]
        public int Start { get; set; }

        /// <summary>
        /// End
        /// </summary>
        [JsonProperty("end")]
        public int End { get; set; }

        /// <summary>
        /// Facet Criteria
        /// </summary>
        [JsonProperty("facetCriteria")]
        public List<object> FacetCriteria { get; set; }

        /// <summary>
        /// Sort
        /// </summary>
        [JsonProperty("sort")]
        public Sort Sort { get; set; }

        /// <summary>
        /// Alert type
        /// </summary>
        [JsonProperty("alertType")]
        public string AlertType { get; set; }
    }
    
    /// <summary>
    /// Stort class
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// Direction
        /// </summary>
        [JsonProperty("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// Field Name
        /// </summary>
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }
    }
}
