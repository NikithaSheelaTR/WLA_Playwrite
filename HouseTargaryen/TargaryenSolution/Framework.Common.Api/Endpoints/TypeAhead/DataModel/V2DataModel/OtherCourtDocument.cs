namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The other court document data contract
    /// </summary>
    [DataContract]
    public class OtherCourtDocument
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
        /// court
        /// </summary>
        [DataMember(Name = "court")]
        public string Court { get; set; }

        /// <summary>
        /// subDocTypeDisplay
        /// </summary>
        [DataMember(Name = "subDocTypeDisplay")]
        public string SubDocTypeDisplay { get; set; }

        /// <summary>
        /// highlight
        /// </summary>
        [DataMember(Name = "highlight")]
        public Highlight Highlight { get; set; }

        /// <summary>
        /// displayCitations
        /// </summary>
        [DataMember(Name = "displayCitations", IsRequired = false)]
        public List<DisplayCitation> DisplayCitations { get; set; }
    }
}