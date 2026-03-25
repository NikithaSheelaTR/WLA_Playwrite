namespace Framework.Common.Api.Endpoints.DoGateway
{
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Endpoints.DoGateway.DataModel;
    using Framework.Common.Api.Endpoints.DoGateway.DataModel.ImageSearch;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;

    using RestSharp;

    /// <summary>
    /// Rtc Client
    /// </summary>
    public class DoGatewayClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DoGatewayClient"/> class.
        /// </summary>
        /// <param name="environment"> environment </param>
        /// <param name="productId"> product Id </param>
        /// <param name="cobaltCookies"> cobalt Cookies </param>
        /// <param name="securityHeaders"> security Headers </param>
        public DoGatewayClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.DOGateway, CobaltProductId.WestlawNext, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.DOGateway),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

		/// <summary>
		/// Start AWS image search process
		/// </summary>
		/// <param name="imageSearchRequest"> Image search request </param>
		/// <param name="userPrizmGuid"> The user Prizm Guid. </param>
		/// <returns> <see cref="StartImageSearchResponse"/>Image search response </returns>
		public StartImageSearchResponse StartAwsImageSearch(StartImageSearchRequest imageSearchRequest, string userPrizmGuid)
		{
			string endpointUrl = "/DOGateway/v1/imagesearch/resultlist";

			IRestRequest startImageSearchRequest = this.RequestBuilder.BuildRequest(new RequestArguments()
			{
				Method = Method.POST,
				Resource = endpointUrl,
				Data = imageSearchRequest,
				DataFormat = DataFormat.Json,
			});

			this.SecurityHeaders.Set("x-cobalt-security-userguid", userPrizmGuid);
			return this.RestClient.Execute<StartImageSearchResponse>(startImageSearchRequest).Data;
		}

		/// <summary>
		/// Retrieves ImageSearchStatus
		/// </summary>
		/// <param name="resultListGuid">ID of the AWS search process</param>
		/// <returns><see cref="ImageSearchStatusResponse"/>Image Search Status Request</returns>
		public ImageSearchStatusResponse GetAwsImageSearchProgress(string resultListGuid)
		{
			string endpointUrl = $"/DOGateway/v1/imagesearch/resultlist/{resultListGuid}/metadata";

			IRestRequest imageSearchStatusRequest = this.RequestBuilder.BuildRequest(new RequestArguments()
			{
				Method = Method.POST,
				Resource = endpointUrl,
				Data = new { },
				DataFormat = DataFormat.Json,
			});

			return this.RestClient.Execute<ImageSearchStatusResponse>(imageSearchStatusRequest).Data;
		}

		/// <summary>
		/// Get Image Search Results
		/// </summary>
		///  <param name="imageSearchResultsRequest"> The Image Search Results Request</param>
		/// <param name="resultListGuid"> The result List Guid. </param>
		/// <returns> <see cref="ImageSearchResultsResponse"/> Image Search Results Response</returns>
		public ImageSearchResultsResponse GetAwsImageSearchResults(ImageSearchResultsRequest imageSearchResultsRequest, string resultListGuid)
		{
			string endpointUrl = $"/DOGateway/v2/imagesearch/resultlist/{resultListGuid}/directory";

			IRestRequest getImageSearchResultsRequest = this.RequestBuilder.BuildRequest(new RequestArguments()
			{
				Method = Method.POST,
				Resource = endpointUrl,
				Data = imageSearchResultsRequest,
				DataFormat = DataFormat.Json,
			});

			return this.RestClient.Execute<ImageSearchResultsResponse>(getImageSearchResultsRequest).Data;
		}

		/// <summary>
		/// Get Do Gateway response from court whether the call made to court for PDF Docket meta data
		/// </summary>
		/// <param name="requestId">request id</param>
		/// <param name="lastNDays"> last date of request</param>
		/// <returns><see cref="DoGatewayResponseModel"/></returns>
		public DoGatewayResponseModel GetDoGatewayResponseFromCourt(string[] requestId, string lastNDays = null)
        {
            DoGatewayRequestModel doGatewayRequestModel = new DoGatewayRequestModel { RequiredKeywords = requestId, LastNDays = lastNDays };
            var requestArguments = new RequestArguments
            {
                Method = Method.POST,
                Resource = "/DOGateway//v2/rtc-docket/logs/requestIds",
                DataFormat = DataFormat.Json,
                Data = doGatewayRequestModel
            };

            return this.RestClient.Execute<DoGatewayResponseModel>(this.RequestBuilder.BuildRequest(requestArguments)).Data;
        }
    }
}