namespace Framework.Common.Api.Endpoints.OnePass
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.Net;
    using System.Runtime.Serialization;
    using System.Security.Cryptography;
    using System.Text;
    using Framework.Common.Api.Endpoints.OnePass.DataModel;
    using Framework.Common.Api.OnePassV3;
    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Utils;
    using RestSharp;

    /// <summary>
    /// The OnePassV3 client
    /// </summary>
    public class OnePassV3Client : BaseCobaltServiceClient
    {
        private const string ParameterName = "application/json; charset=utf-8";

        /// <summary>
        /// Initializes a new instance of the <see cref="OnePassV3Client"/> class.
        /// </summary>
        /// <param name="environment"> The environment. </param>
        /// <param name="productId"> The product id. </param>
        /// <param name="cobaltCookies"> The cobalt cookies. </param>
        /// <param name="securityHeaders"> The security headers. </param>
        public OnePassV3Client(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.OnePassV3, productId, cobaltCookies, securityHeaders)
        {
            this.RestClient.UserAgent =
                 "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/64.0.3282.140 Safari/537.36";
        }

        /// <summary>
        /// Create sign on token
        /// </summary>
        /// <param name="productIdentifier">Product identifier</param>
        /// <param name="productKey">Product key</param>
        /// <param name="accessToken">Access token</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiSecret">Api secret</param>
        /// <returns>Sign on token.</returns>
        public string CreateSignOnToken(string productIdentifier, string productKey, string accessToken, string apiKey, string apiSecret)
        {
            string url = "/onepass/v3/create/signontoken";

            CreateSignonTokenRequest tokenRequest = this.CreateSignonTokenRequest(productIdentifier, productKey, accessToken, apiKey, apiSecret);

            string jsonTokenRequest = ObjectSerializer.SerializeJsonObject(tokenRequest);

            IRestRequest restRequest = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = url,
                Parameters = new List<Parameter> { new Parameter { Name = ParameterName, Value = jsonTokenRequest, Type = ParameterType.RequestBody } }
            });

            this.LastResponse = this.RestClient.Execute(restRequest);

            try
            {
                return ObjectSerializer.DeserializeJsonToObject<SignOnTokenModel>(this.LastResponse.Content).Token;
            }
            catch
            {
                throw new InvalidDataContractException("Invalid Data Contract");
            }
        }

        /// <summary>
        /// Create sign on token request
        /// </summary>
        /// <param name="productIdentifier">Product identifier</param>
        /// <param name="productKey">Product key</param>
        /// <param name="accessToken">Access token</param>
        /// <param name="apiKey">Api key</param>
        /// <param name="apiKeySecret">Api key secret</param>
        /// <returns>New instance of 'sign on token' request.</returns>
        private CreateSignonTokenRequest CreateSignonTokenRequest(string productIdentifier, string productKey,
            string accessToken, string apiKey, string apiKeySecret)
        {
            // Initialization of sign on token request 
            var requestData = new CreateSignonTokenRequest
            {
                Header =
                                      new Header()
                                      {
                                          Version = "3",
                                          ProductIdentifier = productIdentifier,
                                      },
                Credentials =
                                      new Credentials()
                                      {
                                          ProductKey = productKey,
                                          AccessToken = accessToken
                                      },
                APIKey = apiKey,
                TargetProductCode = productIdentifier,
                TargetViewProductCode = null
            };

            // Cast request to Secure request
            var request = (SecureRequest)requestData;

            // Hashing apiKeySecret
            request.Nonce = Guid.NewGuid().ToString("N");

            string data = request.APIKey + request.Nonce
                          + DateTime.Now.ToUniversalTime().ToString("yyyyMMdd", CultureInfo.InvariantCulture);

            using (
                var hash =
                    new HMACSHA256(Encoding.UTF8.GetBytes(apiKeySecret)))
            {
                request.APIKeyHash = Convert.ToBase64String(hash.ComputeHash(Encoding.UTF8.GetBytes(data)));
            }

            return requestData;
        }
    }
}
