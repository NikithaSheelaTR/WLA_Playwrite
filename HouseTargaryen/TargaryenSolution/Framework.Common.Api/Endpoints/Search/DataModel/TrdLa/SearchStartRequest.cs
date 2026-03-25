namespace Framework.Common.Api.Endpoints.Search.DataModel.TrdLa
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Request from Search Start endpoint
    /// </summary>
    /// 
    [DataContract]
    public class SearchStartRequest
    {
        /// <summary>
        /// Gets or sets DefaultSortTypes
        /// </summary>
        [DataMember(Name = "defaultSortTypes")]
        public DefaultSortTypes DefaultSortTypes { get; set; }

        /// <summary>
        ///  Gets or sets query
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }

        /// <summary>
        /// Gets or sets algorithmInfo
        /// </summary>
        [DataMember(Name = "algorithmInfo")]
        public string AlgorithmInfo { get; set; }
    }
}