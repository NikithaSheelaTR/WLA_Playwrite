namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Typeahead V2 Request data contract
    /// </summary>
    [DataContract]
    public class TypeaheadV2Request
    {
        /// <summary>
        /// Term
        /// </summary>
        [DataMember(Name="term")]
        public string Term { get; set; }

        /// <summary>
        /// ContentTypes
        /// </summary>
        [DataMember(Name = "contentTypes")]
        public List<string> ContentTypes { get; set; }

        /// <summary>
        /// Jurisdictions
        /// </summary>
        [DataMember(Name = "jurisdictions")]
        public List<string> Jurisdictions { get; set; }

        /// <summary>
        /// BoostSubType
        /// </summary>
        [DataMember(Name = "boostSubType")]
        public string BoostSubType { get; set; }

        /// <summary>
        /// TrdRefineEnabled
        /// </summary>
        [DataMember(Name = "trdRefineEnabled")]
        public bool TrdRefineEnabled { get; set; }
    }
}