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
    /// Web Content Service
    /// </summary>
    public class WebContentService
    {
        /// <summary>
        /// Request Headers
        /// </summary>
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// URL that these services target
        /// </summary>
        private readonly string hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="WebContentService"/> class. 
        /// </summary>
        public WebContentService(EnvironmentInfo environment)
        {
            // Define Host URL and Request Headers 
            this.hostUrl = environment.Id.GetUrlForWestlawNext();
            this.headers = new WebHeaderCollection
                               {
                                   {
                                       "x-cobalt-host",
                                       $"webcontent.int.next.{environment.Name.ToLower()}.westlaw.com"
                                   }
                               };
        }

        /// <summary>
        /// PracticeAreas
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <returns>Endpoint Response</returns>
        public EndpointResponse<PracticeAreaNewsResponse> ListPracticeAreas(
            ListPracticeAreaRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uriTemplate =
                $"WebContent/v1/mobile/blurbs/{request.Content}/list/?area={request.Area1}&area={request.Area2}&count={request.Count}&beginIndex={request.BeginIndex}&order={request.Order}";
            string url = this.hostUrl + uriTemplate;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<PracticeAreaNewsResponse>(url, this.headers, cookies);
        }

        /// <summary>
        /// Practice Areas
        /// </summary>
        /// <param name="content"> content </param>
        /// <param name="order"> order </param>
        /// <param name="headersList"> headers List </param>
        /// <param name="cookies"> cookies </param>
        /// <returns> Endpoint Response </returns>
        public EndpointResponse<PracticeAreaNewsResponse> PracticeAreas(
            string content,
            string order,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uriTemplate = $"WebContent/v1/mobile/blurbs/{content}/full?order={order}";
            string url = this.hostUrl + uriTemplate;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<PracticeAreaNewsResponse>(url, this.headers, cookies);
        }

        /// <summary>
        /// PracticeAreas
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">cookies</param>
        /// <returns>Endpoint Response</returns>
        public EndpointResponse<PracticeAreaNewsResponse> PracticeAreasNewAnalysisList(
            PracticeAreasNewAnalysisListRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uriTemplate =
                $"WebContent/v1/mobile/newsanalysis/list/?area={request.Area1}&area={request.Area2}&count={request.Count}&beginIndex={request.Index}&order={request.Order}";
            string url = this.hostUrl + uriTemplate;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<PracticeAreaNewsResponse>(url, this.headers, cookies);
        }

        /// <summary>
        /// Practice Areas
        /// </summary>
        /// <param name="request">Practice Area News Analysis Request</param>
        /// <param name="headersList">headers List</param>
        /// <param name="cookies">Cookie Collection</param>
        /// <returns>Endpoint Response</returns>
        public EndpointResponse<PracticeAreaNewsResponse> PracticeAreasNewsAnalysis(
            PracticeAreaNewsAnalysisRequest request,
            WebHeaderCollection headersList = null,
            CookieCollection cookies = null)
        {
            string uriTemplate =
                $"WebContent/v1/mobile/newsanalysis/{request.PracticeArea}?count={request.Count}&beginIndex={request.Index}&order={request.Order}";
            string url = this.hostUrl + uriTemplate;
            if (headersList != null)
            {
                this.headers.Add(headersList);
            }

            return HttpRequestUtil.GetRequest<PracticeAreaNewsResponse>(url, this.headers, cookies);
        }
    }
}