namespace Framework.Common.Api.Endpoints.Website.DataModel.DynamicQa
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The refresh question response.
    /// </summary>
    [DataContract]
    public class RefreshQuestionResponse
    {
        /// <summary>
        /// Query
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        [DataMember(Name = "statusCode")]
        public int StatusCode { get; set; }
    }
}
