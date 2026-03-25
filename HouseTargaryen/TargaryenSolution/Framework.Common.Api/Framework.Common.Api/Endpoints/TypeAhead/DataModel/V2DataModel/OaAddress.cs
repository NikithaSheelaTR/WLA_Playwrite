namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The Oa Address data contract
    /// </summary>
    [DataContract]
    public class OaAddress
    {
        /// <summary>
        /// line1
        /// </summary>
        [DataMember(Name = "line1")]
        public string Line1 { get; set; }

        /// <summary>
        /// line2
        /// </summary>
        [DataMember(Name = "line2")]
        public string Line2 { get; set; }

        /// <summary>
        /// line3
        /// </summary>
        [DataMember(Name = "line3")]
        public string Line3 { get; set; }

        /// <summary>
        /// line4
        /// </summary>
        [DataMember(Name = "line4")]
        public string Line4 { get; set; }

        /// <summary>
        /// line5
        /// </summary>
        [DataMember(Name = "line5")]
        public string Line5 { get; set; }

        /// <summary>
        /// city
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// stateProvince
        /// </summary>
        [DataMember(Name = "stateProvince")]
        public string StateProvince { get; set; }

        /// <summary>
        /// postalCode
        /// </summary>
        [DataMember(Name = "postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// country
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }
    }
}