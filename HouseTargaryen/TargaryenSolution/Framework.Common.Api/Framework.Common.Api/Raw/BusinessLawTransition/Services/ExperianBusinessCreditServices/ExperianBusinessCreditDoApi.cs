// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExperianBusinessCreditDoApi.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited. 
// </copyright>
// <summary>
//   OrderDocumentService
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Services.ExperianBusinessCreditServices
{
    using System.Net;
    using System.Threading;

    using Framework.Common.Api.Raw.BusinessLawTransition.Contracts.ExperianBusinessCreditContracts;
    using Framework.Common.Api.Raw.BusinessLawTransition.Utilities;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.Net;
    using Framework.Core.Utils;

    /// <summary>
    /// OrderDocumentService
    /// </summary>
    public sealed class ExperianBusinessCreditDoApi : DoGatewayV1
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExperianBusinessCreditDoApi"/> class.
        /// </summary>
        /// <param name="environment">The environment.</param>
        /// <param name="token">The token.</param>
        public ExperianBusinessCreditDoApi(EnvironmentInfo environment, string token = null)
            : base(environment, token)
        {
            Thread.Sleep(1000);
        }

        /// <summary>
        /// The get data orchestration meta data.
        /// </summary>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <param name="expectedResposneCode">
        /// The expected response code.
        /// </param>
        /// <returns>
        /// </returns>
        public EndpointResponse<EbcMetaDataContract> GetDataOrchestrationMetaData(
            EbcGetDocumentRequest request,
            ref CookieCollection cookies,
            WebHeaderCollection headers = null,
            HttpStatusCode expectedResposneCode = HttpStatusCode.OK)
        {
            string url = this.HostUrl + "/businesscreditreports/document/" + request.DocGuid + "/metadata";

            if (headers != null)
            {
                this.Headers.Add(headers);
            }

            EndpointResponse<EbcMetaDataContract> response = HttpRequestUtil.SendPostRequest<EbcMetaDataContract>(
                url,
                string.Empty,
                this.Headers,
                ref cookies);

            this.LogInfo(
                "GetDataOrchestrationMetaData",
                url,
                request.GetRequestBody(),
                this.Headers.ToString(),
                response.Headers.ToString(),
                ObjectSerializer.SerializeJsonObject(response.ResponseBody));
            return response;
        }

        /// <summary>
        /// The log info.
        /// </summary>
        /// <param name="methodName">
        /// The method name.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="requestBody">
        /// The request body.
        /// </param>
        /// <param name="headers">
        /// The headers.
        /// </param>
        /// <param name="responseHeaders">
        /// The response headers.
        /// </param>
        /// <param name="response">
        /// The response.
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        private void LogInfo(
            string methodName,
            string url,
            string requestBody,
            string headers,
            string responseHeaders,
            string response,
            string cookies = "")
        {
            Logger.LogInfo("====================================================");
            Logger.LogInfo("***End Point Call (" + methodName + ")***");
            Logger.LogInfo("====================================================");
            Logger.LogInfo("URL      *** " + url);
            Logger.LogInfo("Headers  *** " + headers);
            Logger.LogInfo("Cookies  *** " + cookies);
            Logger.LogInfo("Request  *** " + requestBody);
            Logger.LogInfo("Response Headers  *** " + responseHeaders);
            Logger.LogInfo("Response *** " + response);
        }
    }
}