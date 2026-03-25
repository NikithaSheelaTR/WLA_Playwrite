namespace Framework.Common.Api.Raw.IPhoneMode.Models.Requests
{
    using System.Runtime.Serialization.Json;

    using Framework.Core.Utils;

    /// <summary>
    /// SearchSortRequest class
    /// </summary>
    public class SearchSortRequest : IRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SearchSortRequest"/> class.
        /// </summary>
        /// <param name="guid">The unique identifier.</param>
        public SearchSortRequest(string guid)
        {
            this.Guid = guid;
        }

        /// <summary>
        /// Gets or sets the GUID.
        /// </summary>
        public string Guid { get; set; }

        /// <summary>
        /// Get Request Body
        /// </summary>
        /// <returns> Request Body </returns>
        public string GetRequestBody()
        {
            return ObjectSerializer.SerializeObject<DataContractJsonSerializer, SearchSortRequest>(this);
        }
    }
}