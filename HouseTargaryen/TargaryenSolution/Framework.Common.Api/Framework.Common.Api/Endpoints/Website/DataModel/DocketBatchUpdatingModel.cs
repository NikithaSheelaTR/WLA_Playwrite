namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Docket batch updating model
    /// </summary>
    [DataContract]
    public class DocketBatchUpdatingModel
    {
        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// requests Ids
        /// </summary>
        [DataMember(Name = "UrmIds")]
        public List<string> UrmIds { get; set; }

        /// <summary>
        /// Documents Guids
        /// </summary>
        [DataMember(Name = "DocumentGuids")]
        public List<string> DocumentGuids { get; set; }
    }
}
