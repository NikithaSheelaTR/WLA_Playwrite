// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestUtil.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   and Confidential information of Thomson Reuters. Disclosure, Use or
//   Reproduction without the written authorization of Thomson Reuters is
//   prohibited.  
// </copyright>
// <summary>
//   HttpRequestUtil
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.BusinessLawTransition.Utilities
{
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;

    using Framework.Core.Net;
    using Framework.Core.Utils;

    /// <summary>
    /// The HTTP request utility.
    /// </summary>
    public static class HttpRequestUtil
    {
        /// <summary>
        /// Send an http Post, expecting a status code of 200
        /// </summary>
        /// <param name="url">
        /// Url to target
        /// </param>
        /// <param name="requestBody">
        /// body of request
        /// </param>
        /// <param name="headers">
        /// headers
        /// </param>
        /// <param name="cookies">
        /// cookies
        /// </param>
        /// <param name="contentType">
        /// </param>
        /// <param name="acceptableResponses">
        /// </param>
        /// <typeparam name="T">
        /// Type of object to include in the EndpointResponse object
        /// </typeparam>
        /// <returns>
        /// EndpointRespone (headers, and body of type T)
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// if respone status code is not what we expected, throw this exception
        /// </exception>
        public static EndpointResponse<T> SendPostRequest<T>(
            string url,
            string requestBody,
            WebHeaderCollection headers,
            ref CookieCollection cookies,
            string contentType = "application/x-www-form-urlencoded; charset=utf-8",
            List<HttpStatusCode> acceptableResponses = null)
        {
            // Define default accpectable responses if applicable and create request 
            acceptableResponses = acceptableResponses ?? new List<HttpStatusCode> { HttpStatusCode.OK };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Post,
                url,
                MimeType.Application.Json,
                headers,
                cookies,
                requestBody,
                Encoding.UTF8,
                MimeType.Application.Json);
            request.Accept = MimeType.Application.Json;

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (acceptableResponses.Contains(statusCode))
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        // 11/5/2015 RA: due to specifics of the DO calls cookies of the previous call should be passed along to the next
                        // so that if a document is created in Site A by one endpoint, it will be retrieved from Site A by the next. 
                        // Otherwise delay would be required between calls to allow the replication between sites be completed, so that the next enpoint will not fall with 404NotFound exceptions.
                        cookies = response.Cookies;

                        return new EndpointResponse<T>(
                            ObjectSerializer.DeserializeJsonToObject<T>(streamReader.ReadToEnd()),
                            response.Headers);
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(
                    response,
                    Encoding.UTF8,
                    request.Headers,
                    requestBody);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }
    }
}