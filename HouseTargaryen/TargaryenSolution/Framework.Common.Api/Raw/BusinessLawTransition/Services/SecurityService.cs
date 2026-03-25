// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SecurityService.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   The security service.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Services
{
    using System.Net;

    using Framework.Common.Api.Raw.BusinessLawTransition.Contracts;
    using Framework.Common.Api.Raw.BusinessLawTransition.Requests;
    using Framework.Common.Api.Raw.BusinessLawTransition.Utilities;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Net;
    using Framework.Core.Utils;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// The security service.
    /// </summary>
    public sealed class SecurityService
    {
        private readonly WebHeaderCollection headers;

        /// <summary>
        /// Url that these services target.
        /// </summary>
        private readonly string hostUrl;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityService"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        public SecurityService(EnvironmentInfo environment)
        {
            this.hostUrl = environment.Id.GetSecurityUrlForEnv();
            this.headers = new WebHeaderCollection { { "x-cobalt-security-encoding", "base64" } };
        }

        /// <summary>
        /// The get security token.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <param name="headerCollections">
        /// The headers.
        /// </param>
        /// <param name="expectedResponseCode">
        /// The expected response code.
        /// </param>
        /// <returns>
        /// The <see cref="EndpointResponse{T}"/>.
        /// </returns>
        public EndpointResponse<SecurityTokenContract> GetSecurityToken(
            SecurityTokenRequest request,
            CookieCollection cookies = null,
            WebHeaderCollection headerCollections = null,
            HttpStatusCode expectedResponseCode = HttpStatusCode.OK)
        {
            string url = this.hostUrl + "//Security/v1/token";

            if (headerCollections != null)
            {
                this.headers.Add(headerCollections);
            }

            EndpointResponse<SecurityTokenContract> response =
                HttpRequestUtil.SendPostRequest<SecurityTokenContract>(
                    url,
                    request.GetRequestBody(),
                    this.headers,
                    ref cookies);

            Logger.LogInfo("End Point Call: SecurityToken");
            Logger.LogInfo("URL: " + url);
            Logger.LogInfo("Headers: " + this.headers);
            Logger.LogInfo("Cookies: " + cookies);
            Logger.LogInfo("Request: " + request.GetRequestBody());
            Logger.LogInfo("Response Headers: " + ObjectSerializer.SerializeJsonObject(response.ResponseBody));
            Logger.LogInfo("Response: " + response);

            return response;
        }
    }
}