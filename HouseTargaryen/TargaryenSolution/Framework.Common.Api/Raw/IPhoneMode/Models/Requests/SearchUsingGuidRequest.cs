namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// The search using GUID request.
    /// </summary>
    public class SearchUsingGuidRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchUsingGuidRequest"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        /// <param name="juris">The juries.</param>
        /// <param name="query">The query.</param>
        public SearchUsingGuidRequest(string guid, string juris = "ALLFEDS", string query = "google")
        {
            this.Guid = guid;
            this.Jurisdiction = juris;
            this.Query = query;
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Gets or sets the jurisdiction.
        /// </summary>
        public string Jurisdiction { get; set; }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, SearchUsingGuidRequest>(this);
        }
    }
}