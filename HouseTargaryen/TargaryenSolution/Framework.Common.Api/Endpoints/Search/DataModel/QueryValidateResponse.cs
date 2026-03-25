namespace Framework.Common.Api.Endpoints.Search.DataModel
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Response from Search Validate endpoint
    /// </summary>
    [DataContract]
    public class QueryValidateResponse
    {
        /// <summary>
        /// Gets or sets isValid
        /// </summary>
        [DataMember(Name = "isValid")]
        public bool IsValid { get; set; }

        /// <summary>
        /// Gets list of invalid query errors
        /// </summary>
        [DataMember(Name = "errors")]
        public List<InvalidQueryError> Errors { get; set; }
    }
}
