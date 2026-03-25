namespace Framework.Common.Api.Endpoints.Foldering.DataModel
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The favorite search query.
    /// </summary>
    [DataContract]
    public class FavoriteSearchItem
    {
        /// <summary>
        /// Gets or sets the query id.
        /// </summary>
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        [DataMember(Name = "query")]
        public string Query { get; set; }
    }
}
