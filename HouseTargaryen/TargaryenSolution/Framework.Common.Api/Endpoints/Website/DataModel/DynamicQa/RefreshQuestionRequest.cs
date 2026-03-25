namespace Framework.Common.Api.Endpoints.Website.DataModel.DynamicQa
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The refresh question request.
    /// </summary>
    [DataContract]
    public class RefreshQuestionRequest
    {
        /// <summary>
        /// Query
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }
    }
}
