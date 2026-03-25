namespace Framework.Common.Api.Endpoints.CobaltServices.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The dataroom user property.
    /// </summary>
    [DataContract]
    public class DataroomUserProperty
    {
        /// <summary>
        /// Correlation Name
        /// </summary>
        [DataMember(Name = "correlationName")]
        public string CorrelationName { get; set; }

        /// <summary>
        /// Created By
        /// </summary>
        [DataMember(Name = "createdBy")]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Property Name
        /// </summary>
        [DataMember(Name = "propertyName")]
        public string PropertyName { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// Value
        /// </summary>
        [DataMember(Name = "value")]
        public string Value { get; set; }
    }
}
