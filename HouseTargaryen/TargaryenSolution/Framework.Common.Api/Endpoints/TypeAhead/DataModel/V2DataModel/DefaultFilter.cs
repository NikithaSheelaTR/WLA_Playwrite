namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Default Filter data contract
    /// </summary>
    [DataContract]
    public class DefaultFilter
    {
        /// <summary>
        /// filterEntityType
        /// </summary>
        [DataMember(Name = "filterEntityType")]
        public string FilterEntityType { get; set; }

        /// <summary>
        /// filterValue
        /// </summary>
        [DataMember(Name = "filterValue")]
        public List<string> FilterValue { get; set; }
    }
}