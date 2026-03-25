namespace Framework.Common.Api.Endpoints.Search.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Invalid query error
    /// </summary>
    [DataContract]
    public class InvalidQueryError
    {
        /// <summary>
        /// Error code
        /// </summary>
        [DataMember(Name = "errorCode")]
        public string ErrorCode { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
