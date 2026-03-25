namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The case data contract
    /// </summary>
    [DataContract]
    public class Case
    {
        /// <summary>
        /// guid
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// date
        /// </summary>
        [DataMember(Name = "date")]
        public string Date { get; set; }

        /// <summary>
        /// title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; }

        /// <summary>
        /// jurisdictionType
        /// </summary>
        [DataMember(Name = "jurisdictionType")]
        public string JurisdictionType { get; set; }

        /// <summary>
        /// highlight
        /// </summary>
        [DataMember(Name = "highlight")]
        public Highlight Highlight { get; set; }

        /// <summary>
        /// displayCitations
        /// </summary>
        [DataMember(Name = "displayCitations")]
        public List<DisplayCitation> DisplayCitations { get; set; }

        /// <summary>
        /// court
        /// </summary>
        [DataMember(Name = "court")]
        public string Court { get; set; }
    }
}