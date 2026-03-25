// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HttpRequestUtil.cs" company="Thomson Reuters">
//   Copyright 2015: Thomson Reuters. All Rights Reserved. Proprietary
//   //   and Confidential information of Thomson Reuters. Disclosure, Use or
//   //   Reproduction without the written authorization of Thomson Reuters is
//   //   prohibited.
// </copyright>
// <summary>
//   HttpRequestUtil
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Framework.Common.Api.Raw.Foldering.Utilities.Foldering
{
    using System.IO;
    using System.Net;
    using System.Text;

    using Framework.Core.Net;
    using Framework.Core.Utils;

    /// <summary>
    /// HttpRequestUtil
    /// </summary>
    public static class HttpRequestUtil
    {
        /// <summary>
        /// Send an http Get, expecting a status code of 200
        /// </summary>
        /// <param name="url">
        /// Url to target
        /// </param>
        /// <param name="headers">
        /// headers
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <returns>
        /// EndpointRespone (headers, and body of type T)
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// if respone status code is not what we expected, throw this exception
        /// </exception>
        public static string GetRequest(string url, WebHeaderCollection headers, CookieCollection cookies = null)
        {
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Get,
                url,
                headers: headers,
                cookies: cookies);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK)
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        // return new EndpointResponse<T>(Deserialise<T>(streamReader), response.Headers);
                        return streamReader.ReadToEnd();
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(
                    response,
                    Encoding.UTF8,
                    request.Headers);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }

        /// <summary>
        /// Send an http Post, expecting a status code of 200
        /// </summary>
        /// <param name="url">
        /// Url to target
        /// </param>
        /// <param name="headers">
        /// headers
        /// </param>
        /// <param name="body">
        /// </param>
        /// <param name="cookies">
        /// The cookies.
        /// </param>
        /// <returns>
        /// EndpointRespone (headers, and body of type T)
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// if respone status code is not what we expected, throw this exception
        /// </exception>
        public static string PutRequest(
            string url,
            WebHeaderCollection headers,
            string body,
            CookieCollection cookies = null)
        {
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Put,
                url,
                headers: headers,
                cookies: cookies,
                requestBody: body,
                requestEncoding: Encoding.UTF8,
                requestMimeType: "application/json; charset=utf-8");

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.ResetContent)
                {
                    using (new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        // return new EndpointResponse<T>(Deserialise<T>(streamReader), response.Headers);
                        return string.Empty;

                        // return streamReader.ReadToEnd();
                    }
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(
                    response,
                    Encoding.UTF8,
                    request.Headers);
                throw new HttpResponseException(statusCode, exceptionMessage);
            }
        }

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
            CookieCollection cookies,
            string contentType = "application/x-www-form-urlencoded; charset=utf-8")
        {
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Post,
                url,
                null,
                headers,
                cookies,
                requestBody,
                Encoding.UTF8,
                contentType);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (statusCode == HttpStatusCode.OK || statusCode == HttpStatusCode.NoContent)
                {
                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
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