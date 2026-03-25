namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The Admindecision data contract
    /// </summary>
    [DataContract]
    public class Admindecision
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
        /// highlight
        /// </summary>
        [DataMember(Name = "highlight")]
        public Highlight Highlight { get; set; }

        /// <summary>
        /// displayCitations
        /// </summary>
        [DataMember(Name = "displayCitations")]
        public List<DisplayCitation> DisplayCitations { get; set; }
    }
}