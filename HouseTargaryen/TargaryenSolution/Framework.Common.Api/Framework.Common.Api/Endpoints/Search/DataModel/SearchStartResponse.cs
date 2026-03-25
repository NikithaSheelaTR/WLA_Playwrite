namespace Framework.Common.Api.Endpoints.Search.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// Response from Search Start endpoint
    /// </summary>
    [DataContract]
    public class SearchStartResponse
    {
        /// <summary>
        /// Gets or sets contentType
        /// </summary>
        [DataMember(Name = "contentType")]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets modifiedquery
        /// </summary>
        [DataMember(Name = "modifiedquery")]
        public string ModifiedQuery { get; set; }

        /// <summary>
        /// Gets or sets searchquid
        /// </summary>
        [DataMember(Name = "searchguid")]
        public string SearchGuid { get; set; }

        /// <summary>
        /// Gets or sets typeOfSearch
        /// </summary>
        [DataMember(Name = "typeOfSearch")]
        public string TypeOfSearch { get; set; }
    }
}
