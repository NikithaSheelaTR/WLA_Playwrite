namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The oa data contract
    /// </summary>
    [DataContract]
    public class Oa
    {
        /// <summary>
        /// parentType
        /// </summary>
        [DataMember(Name = "parentType")]
        public string ParentType { get; set; }

        /// <summary>
        /// type
        /// </summary>
        [DataMember(Name = "type")]
        public string Type { get; set; }

        /// <summary>
        /// permID
        /// </summary>
        [DataMember(Name = "permID")]
        public string PermId { get; set; }

        /// <summary>
        /// name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// addresses
        /// </summary>
        [DataMember(Name = "addresses")]
        public List<OaAddress> Addresses { get; set; }

        /// <summary>
        /// executives
        /// </summary>
        [DataMember(Name = "executives", IsRequired = false)]
        public List<object> Executives { get; set; }

        /// <summary>
        /// relatedDocuments
        /// </summary>
        [DataMember(Name = "relatedDocuments", IsRequired = false)]
        public List<object> RelatedDocuments { get; set; }
    }
}