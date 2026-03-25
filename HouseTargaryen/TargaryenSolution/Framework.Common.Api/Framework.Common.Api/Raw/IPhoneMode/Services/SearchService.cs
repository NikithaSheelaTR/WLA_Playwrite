namespace Framework.Common.Api.Raw.IPhoneMode.Services
{
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Models.Responses;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Net;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// Search Service
    /// </summary>
    public class SearchService
    {
        /// <summary>
        /// The headers.
        /// </summary>
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// The host URL.
        /// </summary>
        private readonly string hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchService"/> class.
        /// </summary>
        public SearchService(EnvironmentInfo environment)
        {
            this.hostUrl = environment.Id.GetUrlForWestlawNext();
            this.headers = new WebHeaderCollection
                               {
                                   {
                                       "x-cobalt-host",
                                       $"search.int.next{environment.Name.ToLower()}.westlaw.com"
                                   }
                               };
        }

        /// <summary>
        /// Searches the sort.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="headersList">The headers List.</param>
        /// <param name="cookies">The cookies.</param>
        /// <returns>Endpoint Response</returns>
        public EndpointResponse<SearchSortResponse> SearchSort(
            SearchSortRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uri = this.hostUrl + "Search/v3/search/update";
            string body = "{\"resultGuid\":\"" + request.Guid
                          + "\",\"type\":\"TCO\",\"clientId\":\"TEST\",\"sortType\":\"DATE_DESCENDING\"}";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.PostRequest<SearchSortResponse>(uri, body, this.headers, cookies);
        }

        /// <summary>
        /// Searches the using unique identifier.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="headersList">The headers List.</param>
        /// <param name="cookies">The cookies.</param>
        /// <returns>Endpoint Response</returns>
        public EndpointResponse<SearchUsingGuidResponse> SearchUsingGuid(
            SearchUsingGuidRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uri = this.hostUrl
                         + $"Search/v1/results?jurisdiction={request.Jurisdiction}&query={request.Query}&result={request.Guid}&searchId={request.Guid}";
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<SearchUsingGuidResponse>(uri, this.headers, cookies);
        }
    }
}