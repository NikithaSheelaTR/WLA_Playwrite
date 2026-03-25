namespace Framework.Common.Api.Endpoints.Search
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.Search.DataModel;
    using Framework.Common.Api.Endpoints.Search.DataModel.DocumentAnalyzer;
    using Framework.Common.Api.Endpoints.Search.DataModel.DynamicQa;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The search client
    /// </summary>
    public sealed class SearchClient : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchClient"/> class.
        /// </summary>
        /// <param name="environment"> environment </param>
        /// <param name="productId"> product Id </param>
        /// <param name="cobaltCookies"> cobalt Cookies </param>
        /// <param name="securityHeaders"> security Headers </param>
        public SearchClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.Search, productId, cobaltCookies, securityHeaders)
        {
            this.BaseUrl =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.Search),
                    TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                    environment).Uri;
        }

        /// <summary>
        /// The POST validate query request
        /// </summary>
        /// <param name="parameterValue"> The parameter Value.  </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public QueryValidateResponse GetQueryValidateResponse(string parameterValue)
        {
            string url = "Search/v1/query/validate";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"/v1/query/validate endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, QueryValidateResponse>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// The POST Search Start 
        /// </summary>
        /// <param name="requestBody"> The request Body. </param>
        /// <returns> The <see cref="SearchStartResponse"/>. </returns>
        public SearchStartResponse GetSearchStartResponse(DataModel.TrdLa.SearchStartRequest requestBody = null)
        {
            string url = "/Search/v3/search/start?jurisdiction=ALLCASES&showFoundDocumentCount=false&clientId=1&forceTnc=pref_phr&findJurisdictions=1s";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Data = requestBody,
                DataFormat = DataFormat.Json
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"/v3/search/start endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, SearchStartResponse>(
                                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// The get Trd Search Result response
        /// </summary>
        /// <param name="searchStartResponse"> The search Start Response. </param>
        /// <returns> The <see cref="SearchResultResponse"/>. </returns>
        public SearchResultResponse GetTrdSearchResult(SearchStartResponse searchStartResponse)
        {
            string url = $"/Search/v4/search/verify?result={searchStartResponse.SearchGuid}&type=TRDISCOVER-CASE";

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 2);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"/v4/search endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, SearchResultResponse>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Get recommendation topanswer (GET)
        /// </summary>
        /// <param name="additionalUrl">remaining url part</param>
        /// <returns>New instance of TopanswerResponseModel</returns>
        public TopanswerResponseModel GetRecommendationTopanswer(string additionalUrl)
        {
            string url = "Search/v3/recommendation/topanswer?" + additionalUrl;

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = url
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<TopanswerResponseModel>(this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }
    }
}