namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The LpaAddress data contract
    /// </summary>
    [DataContract]
    public class LpaAddress
    {
        /// <summary>
        /// street
        /// </summary>
        [DataMember(Name = "street")]
        public List<string> Street { get; set; }

        /// <summary>
        /// city
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; }

        /// <summary>
        /// state
        /// </summary>
        [DataMember(Name = "state")]
        public string State { get; set; }

        /// <summary>
        /// cityStateZip
        /// </summary>
        [DataMember(Name = "cityStateZip")]
        public string CityStateZip { get; set; }

        /// <summary>
        /// county
        /// </summary>
        [DataMember(Name = "county")]
        public string County { get; set; }

        /// <summary>
        /// country
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; }
    }
}