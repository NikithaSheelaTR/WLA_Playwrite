namespace Framework.Common.Api.Endpoints.Website.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Pdf batch download status model
    /// </summary>
    [DataContract]
    public class PdfBatchDownloadStatusModel
    {
        /// <summary>
        /// Request count
        /// </summary>
        [DataMember(Name = "RequestCount")]
        public int RequestCount { get; set; }

        /// <summary>
        /// Count of complete
        /// </summary>
        [DataMember(Name = "Complete")]
        public int Complete { get; set; }

        /// <summary>
        /// Count in progress
        /// </summary>
        [DataMember(Name = "InProgress")]
        public int InProgress { get; set; }

        /// <summary>
        /// Count of failed
        /// </summary>
        [DataMember(Name = "Failed")]
        public int Failed { get; set; }
    }
}
