namespace Framework.Common.Api.Endpoints.TypeAhead
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;

    using Framework.Common.Api.Endpoints.TypeAhead.DataModel.V2DataModel;
    using Framework.Common.Api.Endpoints.TypeAhead.DataModel.V3DataModel;
    using Framework.Common.Api.Utilities;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;

    using RestSharp;

    /// <summary>
    /// The TypeAhead module client
    /// </summary>
    public sealed class TypeAheadClient : BaseCobaltServiceClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TypeAheadClient"/> class. 
        /// </summary>
        /// <param name="environment"> environment </param>
        /// <param name="productId"> product Id </param>
        /// <param name="cobaltCookies"> cobalt Cookies </param>
        /// <param name="securityHeaders"> security Headers </param>
        public TypeAheadClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.TypeAhead, productId, cobaltCookies, securityHeaders)
        {
            {
                this.BaseUrl =
                    TestConfigurationRepository.DefaultInstance.FindEndpoint(
                        TestConfigurationRepository.DefaultInstance.FindModule(CobaltModuleId.TypeAhead),
                        TestConfigurationRepository.DefaultInstance.FindProduct(this.ProductId),
                        environment).Uri;
            }
        }

        /// <summary>
        /// The Get TypeaheadV3 Suggestions
        /// </summary>
        /// <param name="requestBody"> request Body </param>
        /// <returns>Typeahead V2 Response</returns>
        public TypeaheadV3Response GetTypeaheadV3SuggestionsResponse(TypeaheadV2Request requestBody)
        {
            string url = "/Typeahead/v3/recommendation/suggestions/entity";
            string parameterName = "application/json; charset=utf-8";
            string parameterValue = ObjectSerializer.SerializeJsonObject(requestBody);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = parameterName, Value = parameterValue, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.ExecuteUntil(request, numOfAttempts: 5);

            if (!this.LastResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new Exception($"{url} endpoint returned {this.LastResponse.StatusCode}");
            }

            try
            {
                return ObjectSerializer.DeserializeObject<DataContractJsonSerializer, TypeaheadV3Response>(
                    this.LastResponse.Content);
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }
    }
}