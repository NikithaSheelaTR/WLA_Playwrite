namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Pdfs submission status model
    /// </summary>
    [DataContract]
    public class PdfsSubmitionStatusModel
    {
        /// <summary>
        /// Response string
        /// </summary>
        [DataMember(Name = "ResponseData")]
        public string ResponseData { get; set; }

        /// <summary>
        /// Message
        /// </summary>
        [DataMember(Name = "Message")]
        public string Message { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        [DataMember(Name = "Status")]
        public string Status { get; set; }

        /// <summary>
        /// Status code
        /// </summary>
        [DataMember(Name = "StatusCode")]
        public int StatusCode { get; set; }
    }
}
