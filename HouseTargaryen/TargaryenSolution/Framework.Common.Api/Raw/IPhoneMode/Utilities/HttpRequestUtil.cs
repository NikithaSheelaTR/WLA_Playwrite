namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Text;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.Net;
    using Framework.Core.Utils;

    /// <summary>
    /// Http Request Utile
    /// </summary>
    public static class HttpRequestUtil
    {
        /// <summary>
        /// The call http get.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="cookieCollection"> The cookie collection. </param>
        /// <param name="acceptValue"> The accept value. </param>
        /// <returns> The <see cref="HttpWebResponse"/>. </returns>
        public static HttpWebResponse CallHttpGet(string url, CookieCollection cookieCollection, string acceptValue)
        {
            return HttpRequestUtil.IssueRequest(url, "GET", cookieCollection, acceptValue);
        }

        /// <summary>
        /// Call Http Get
        /// </summary>
        /// <param name="url"> URL </param>
        /// <param name="cookieCollection"> The cookie collection. </param>
        /// <returns></returns>
        public static HttpWebResponse CallHttpGet(string url, CookieCollection cookieCollection)
        {
            return HttpRequestUtil.IssueRequest(url, "GET", cookieCollection, null);
        }

        /// <summary>
        /// SendRequest an http Get, expecting a status code of 200
        /// </summary>
        /// <typeparam name="T"> Endpoint Response type  </typeparam>
        /// <param name="url"> URL to target </param>
        /// <param name="headers"> headers </param>
        /// <param name="cookies"> cookies </param>
        /// <param name="acceptableResponses"> acceptable Responses </param>
        /// <returns> Endpoint Response (headers, and body of type T) </returns>
        /// <exception cref="HttpResponseException">
        /// if response status code is not what we expected, throw this exception
        /// </exception>
        public static EndpointResponse<T> DeleteRequest<T>(
            string url,
            WebHeaderCollection headers,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            // Define default acceptable responses if applicable and create request 
            acceptableResponses = acceptableResponses
                                  ?? new List<HttpStatusCode>
                                         {
                                             HttpStatusCode.OK,
                                             HttpStatusCode.Created,
                                             HttpStatusCode.NoContent
                                         };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Delete,
                url,
                headers: headers,
                cookies: cookies);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (acceptableResponses.Contains(statusCode))
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        return typeof(T) == typeof(string)
                                   ? new EndpointResponse<T>(
                                       (T)Convert.ChangeType(streamReader.ReadToEnd(), typeof(T)),
                                       response.Headers)
                                   : new EndpointResponse<T>(
                                       ObjectSerializer.DeserializeJsonToObject<T>(streamReader.ReadToEnd()),
                                       response.Headers);
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
        /// SendRequest an http Get, expecting a status code of 200
        /// </summary>
        /// <typeparam name="T"> Endpoint Response type </typeparam>
        /// <param name="url"> URL to target </param>
        /// <param name="headers"> headers </param>
        /// <param name="cookies"> cookies </param>
        /// <param name="acceptableResponses"> acceptable Responses </param>
        /// <returns> Endpoint Response (headers, and body of type T) </returns>
        /// <exception cref="HttpResponseException">
        /// if response status code is not what we expected, throw this exception
        /// </exception>
        public static EndpointResponse<T> GetRequest<T>(
            string url,
            WebHeaderCollection headers,
            CookieCollection cookies = null,
            List<HttpStatusCode> acceptableResponses = null)
        {
            // Define default acceptable responses if applicable and create request 
            acceptableResponses = acceptableResponses
                                  ?? new List<HttpStatusCode>
                                         {
                                             HttpStatusCode.OK,
                                             HttpStatusCode.Created,
                                             HttpStatusCode.NoContent
                                         };
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Get,
                url,
                headers: headers,
                cookies: cookies);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                HttpStatusCode statusCode = response.StatusCode;
                if (acceptableResponses.Contains(statusCode))
                {
                    using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        if (typeof(T) == typeof(string))
                        {
                            return new EndpointResponse<T>(
                                (T)Convert.ChangeType(streamReader.ReadToEnd(), typeof(T)),
                                response.Headers);
                        }

                        return new EndpointResponse<T>(
                            ObjectSerializer.DeserializeJsonToObject<T>(streamReader.ReadToEnd()),
                            response.Headers);
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
        /// The issue request.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="method"> The method. </param>
        /// <param name="cookieCollection"> The cookie collection. </param>
        /// <param name="acceptValue"> The accept value. </param>
        /// <returns> The <see cref="HttpWebResponse"/>. </returns>
        public static HttpWebResponse IssueRequest(
            string url,
            string method,
            CookieCollection cookieCollection,
            string acceptValue)
        {
            string isDcExit = Environment.GetEnvironmentVariable(EnvironmentConstants.IsDcExit);
            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.UserAgent =
                "Mozilla/5.0 (Windows; U; Windows NT 6.1; en-US; rv:1.9.2.10) Gecko/20100914 Firefox/3.6.10";
            httpWebRequest.Method = method;
            if (!string.IsNullOrEmpty(acceptValue))
            {
                httpWebRequest.Accept = acceptValue;
            }
            if (isDcExit != null && !isDcExit.Equals("Yes"))
            {
                //Below if loop commented as these cookie collection is not mandatory for all endpoints
                if (cookieCollection != null)
                {
                    httpWebRequest.CookieContainer = new CookieContainer(cookieCollection.Count);
                    httpWebRequest.CookieContainer.Add(new Uri(url), new CookieCollection());
                }
            }

            try
            {
                return (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine(httpWebRequest.RequestUri.ToString());
                foreach (Cookie cookie in cookieCollection)
                {
                    Console.WriteLine(cookie);
                }

                throw;
            }
        }

        /// <summary>
        /// SendRequest an http Post, expecting a status code of 200
        /// </summary>
        /// <param name="url">URL to target</param>
        /// <param name="requestBody">body of request</param>
        /// <param name="headers">headers</param>
        /// <param name="cookies">cookies</param>
        /// <param name="contentType"> content Type </param>
        /// <param name="acceptableResponses"> acceptable Responses </param>
        /// <typeparam name="T">Type of object to include in the EndpointResponse object</typeparam>
        /// <returns>EndpointResponse (headers, and body of type T)</returns>
        /// <exception cref="HttpResponseException">if response status code is not what we expected, throw this exception</exception>
        public static EndpointResponse<T> PostRequest<T>(
            string url,
            string requestBody,
            WebHeaderCollection headers,
            CookieCollection cookies,
            string contentType = "application/x-www-form-urlencoded; charset=utf-8",
            List<HttpStatusCode> acceptableResponses = null)
        {
            // Define default acceptable responses if applicable and create request 
            acceptableResponses = acceptableResponses
                                  ?? new List<HttpStatusCode>
                                         {
                                             HttpStatusCode.OK,
                                             HttpStatusCode.Created,
                                             HttpStatusCode.NoContent
                                         };
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
                        return typeof(T) == typeof(string)
                                   ? new EndpointResponse<T>(
                                       (T)Convert.ChangeType(streamReader.ReadToEnd(), typeof(T)),
                                       response.Headers)
                                   : new EndpointResponse<T>(
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

        /// <summary>
        /// The send post request.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="postData"> The post data. </param>
        /// <param name="cookies"> The cookies. </param>
        /// <returns> The <see cref="HttpWebResponse"/>. </returns>
        public static HttpWebResponse SendPostRequest(string url, string postData, CookieCollection cookies)
        {
            return HttpRequestUtil.SendPostRequest(url, postData, cookies, null, null, null);
        }

        /// <summary>
        /// The send post request.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="postData"> The post data. </param>
        /// <param name="cookies"> The cookies. </param>
        /// <param name="headers"> The headers. </param>
        /// <param name="contentType"> The content type. </param>
        /// <param name="accept"> The accept. </param>
        /// <returns> The <see cref="HttpWebResponse"/>. </returns>
        public static HttpWebResponse SendPostRequest(
            string url,
            string postData,
            CookieCollection cookies,
            WebHeaderCollection headers,
            string contentType,
            string accept)
        {
            return HttpRequestUtil.SendPostRequest(url, postData, cookies, headers, contentType, accept, new int?());
        }

        /// <summary>
        /// The send post request.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="postData"> The post data. </param>
        /// <param name="cookies"> The cookies. </param>
        /// <param name="headers"> The headers. </param>
        /// <param name="contentType"> The content type. </param>
        /// <param name="accept"> The accept. </param>
        /// <param name="timeout"> The timeout. </param>
        /// <returns> The <see cref="HttpWebResponse"/>. </returns>
        public static HttpWebResponse SendPostRequest(
            string url,
            string postData,
            CookieCollection cookies,
            WebHeaderCollection headers,
            string contentType,
            string accept,
            int? timeout)
        {
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, certificate, chain, sslPolicyErrors) => true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = "POST";
            if (timeout.HasValue)
            {
                httpWebRequest.Timeout = timeout.Value;
            }

            if (cookies != null)
            {
                httpWebRequest.CookieContainer = new CookieContainer();
                httpWebRequest.CookieContainer.Add(new Uri(url), cookies);
            }

            if (headers != null)
            {
                httpWebRequest.Headers.Add(headers);
            }

            if (!string.IsNullOrEmpty(contentType))
            {
                httpWebRequest.ContentType = contentType;
            }

            if (!string.IsNullOrEmpty(accept))
            {
                httpWebRequest.Accept = accept;
            }

            var encoding = new UTF8Encoding();
            using (Stream requestStream = httpWebRequest.GetRequestStream())
            {
                requestStream.Write(encoding.GetBytes(postData), 0, encoding.GetByteCount(postData));
                requestStream.Close();
            }

            var response = (HttpWebResponse)httpWebRequest.GetResponse();
            return (HttpWebResponse)httpWebRequest.GetResponse();
        }
    }
}