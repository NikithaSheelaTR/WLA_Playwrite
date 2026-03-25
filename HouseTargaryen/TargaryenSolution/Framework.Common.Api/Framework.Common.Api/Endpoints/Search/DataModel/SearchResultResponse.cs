namespace Framework.Common.Api.Endpoints.Search.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Response from Search Result endpoint
    /// </summary>
    [DataContract]
    public class SearchResultResponse
    {
        /// <summary>
        /// Gets or sets quid
        /// </summary>
        [DataMember(Name = "guid")]
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets multiCite
        /// </summary>
        [DataMember(Name = "multiCite")]
        public bool MultiCite { get; set; }

        /// <summary>
        /// Gets or sets statusCode
        /// </summary>
        [DataMember(Name = "statusCode")]
        public string StatusCode { get; set; }

        /// <summary>
        /// Gets or sets typeOfSearch
        /// </summary>
        [DataMember(Name = "typeOfSearch")]
        public string TypeOfSearch { get; set; }
    }
}
