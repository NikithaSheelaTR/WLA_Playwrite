namespace Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The case data contract
    /// </summary>
    [DataContract]
   public class LegalAnalytics
    {
        /// <summary>
        /// guid
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// snippet
        /// </summary>
        [DataMember(Name = "snippet")]
        public string Snippet { get; set; }
    }
}
