namespace Framework.Core.Net
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// Provides the ability to execute HTTP methods and retrieve responses.
    /// </summary>
    /// <remarks>
    /// The following HTTP methods are supported:
    /// <ul>
    /// <li><c>HEAD</c></li>
    /// <li><c>OPTIONS</c></li>
    /// <li><c>TRACE</c></li>
    /// <li><c>GET</c></li>
    /// <li><c>PUT</c></li>
    /// <li><c>POST</c></li>
    /// <li><c>DELETE</c></li>
    /// </ul>
    /// Used in conjunction with <see cref="HttpWebRequest"/>, the following code will execute a call to an endpoint and handle the response:
    /// <code>
    /// // 1. Construct request
    /// var url = "http://cobaltservices.ci.int.thomsonreuters.com/DataRoom/v1/constraints/userdocuments";
    /// var request = HttpUtils.ConstructHttpWebRequest(HttpUtils.HttpMethod.Get, url, MimeType.Application.Json);
    /// 
    /// // 2. Send request to endpoint &amp; parse response
    /// using (var response = HttpUtils.GetHttpWebResponse(request))
    /// {
    ///     EndpointResponse&lt;string&gt; endpointResponse;
    ///     var statusCode = response.StatusCode;
    ///     if (statusCode == HttpStatusCode.OK)
    ///     {
    ///         using (var streamReader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
    ///         {
    ///             endpointResponse = new EndpointResponse&lt;string&gt;(streamReader.ReadToEnd(), response.Headers);
    ///         }
    ///     }
    ///     else
    ///     {
    ///         var exceptionMessage = HttpUtils.HandleHttpResponseException(response, Encoding.UTF8, request.Headers);
    ///         throw new HttpResponseException(statusCode, exceptionMessage);
    ///     }
    /// 
    ///     // 3. Validate
    ///     Assert.IsTrue(endpointResponse.ResponseBody.Length > 0, "Response Body is empty.");
    ///     Assert.IsTrue(endpointResponse.Headers.Count > 0, "No headers returned.");
    /// }
    /// </code>
    /// </remarks>
    /// <seealso cref="Framework.Core"/>
    /// <seealso cref="Core"/>
    /// <seealso cref="Framework"/>
    public static class HttpUtils
    {
        /// <summary>
        /// Enumerates all valid HTTP methods for use with <see cref="HttpUtils.ConstructHttpWebRequest"/>.
        /// </summary>
        public enum HttpMethod
        {
            /// <summary>
            /// Indicates an HTTP method of <c>HEAD</c>.
            /// </summary>
            Head,

            /// <summary>
            /// Indicates an HTTP method of <c>OPTIONS</c>.
            /// </summary>
            Options,

            /// <summary>
            /// Indicates an HTTP method of <c>TRACE</c>.
            /// </summary>
            Trace,

            /// <summary>
            /// Indicates an HTTP method of <c>GET</c>.
            /// </summary>
            Get,

            /// <summary>
            /// Indicates an HTTP method of <c>PUT</c>.
            /// </summary>
            Put,

            /// <summary>
            /// Indicates an HTTP method of <c>POST</c>.
            /// </summary>
            Post,

            /// <summary>
            /// Indicates an HTTP method of <c>DELETE</c>.
            /// </summary>
            Delete
        }


        /// <summary>
        /// Retrieves an <see cref="HttpWebResponse"/> from an <see cref="HttpWebRequest"/>, suppressing a <see cref="WebException"/>, should one occur.
        /// </summary>
        /// <param name="request">An HTTP web request.</param>
        /// <returns>An HTTP web response.</returns>
        /// <remarks>
        /// An <see cref="ArgumentNullException"/> is thrown if <c>request</c> is provided as <c>null</c>.
        /// </remarks>
        public static HttpWebResponse GetHttpWebResponse(HttpWebRequest request)
        {
            // validate input
            if (request == null)
            {
                throw new ArgumentNullException("request", "An HttpWebRequest is required.");
            }

            try
            {
                return (HttpWebResponse)request.GetResponse();
            }
            catch (WebException webException)
            {
                return (HttpWebResponse)webException.Response;
            }
        }

        /// <summary>
        /// Constructs an HTTP request.
        /// </summary>
        /// <param name="httpMethod">The HTTP method of the request.</param>
        /// <param name="url">The URL being targeted by the request.</param>
        /// <param name="responseMimeType">The MIME type value to be placed on the <c>Accept</c> header of an HTTP request.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="headers">The set of HTTP headers to be sent on the request.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="cookies">The set of HTTP cookies to be sent on the request.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="requestBody">The message body to be sent as part of a <c>PUT</c> request or <c>POST</c> request.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="requestEncoding">The encoding associated with the provided <c>requestBody</c>.  Defaults to <c>null</c> if not provided.</param>
        /// <param name="requestMimeType">The MIME type value to be placed on the <c>Content-Type</c> header of the request.  Defaults to <c>null</c> if not provided.</param>
        /// <returns>An HTTP web request with all necessary fields populated.</returns>
        /// <remarks>
        /// An <see cref="ArgumentNullException"/> is thrown if <c>url</c> is provided as <c>null</c>.
        /// </remarks>
        public static HttpWebRequest ConstructHttpWebRequest(HttpMethod httpMethod, string url, string responseMimeType = null, WebHeaderCollection headers = null, CookieCollection cookies = null, string requestBody = null, Encoding requestEncoding = null, string requestMimeType = null)
        {
            // validate input
            if (url == null)
            {
                throw new ArgumentNullException("url", "A URL is required.");
            }

            // create basic request
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = httpMethod.ToString();

            // add response MIME type
            if (responseMimeType != null)
            {
                request.Accept = responseMimeType;
            }

            // add headers and cookies
            if (headers != null)
            {
                request.Headers = headers;
            }
            request.CookieContainer = new CookieContainer();
            if (cookies != null)
            {
                request.CookieContainer.Add(new Uri(url), cookies);
            }

            // add request body
            if (requestBody != null)
            {
                if (requestEncoding != null)
                {
                    byte[] requestBodyBytes = requestEncoding.GetBytes(requestBody);
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(requestBodyBytes, 0, requestBodyBytes.Length);
                    }
                    if (requestMimeType != null)
                    {
                        request.ContentType = requestMimeType;
                    }
                    else
                    {
                        throw new InvalidOperationException("A requestMimeType must be provided when a requestBody is provided.");
                    }
                }
                else
                {
                    throw new InvalidOperationException("A requestEncoding must be provided when a requestBody is provided.");
                }
            }

            return request;
        }

        /// <summary>
        /// Determines an exception message based upon an HTTP request/response.
        /// </summary>
        /// <param name="response">An HTTP web response</param>
        /// <param name="responseEncoding">An encoding associated with the provided <c>response</c>.</param>
        /// <param name="requestHeaders">A list of HTTP headers sent as part of the HTTP request.</param>
        /// <param name="requestBody">An HTTP request body.  Defaults to <c>null</c> if not provided.</param>
        /// <returns>An exception message based upon an HTTP request/response.</returns>
        /// <remarks>
        /// An <see cref="ArgumentNullException"/> is thrown if any of the parameters besides <c>requestBody</c> are <c>null</c>.  The return from this method should be used as the <c>message</c> argument of an <see cref="Framework.Core.Net.HttpResponseException"/>.
        /// </remarks>
        public static string HandleHttpResponseException(HttpWebResponse response, Encoding responseEncoding, WebHeaderCollection requestHeaders, string requestBody = null)
        {
            // validate input
            if (response == null)
            {
                throw new ArgumentNullException("response", "An HttpWebResponse is required.");
            }
            if (responseEncoding == null)
            {
                throw new ArgumentNullException("responseEncoding", "A response body encoding is required.");
            }
            if (requestHeaders == null)
            {
                throw new ArgumentNullException("requestHeaders", "A WebHeaderCollection is required.");
            }

            // build exception message based upon available information
            var exceptionMessage = new StringBuilder();
            exceptionMessage.Append("Status " + (int)response.StatusCode + " " + response.StatusCode + " was received when attempting to retrieve response from " + response.ResponseUri + ".\r\n\r\n");
            if (requestHeaders.Count > 0)
            {
                exceptionMessage.Append("Request Headers:\r\n" + requestHeaders);
            }
            if (requestBody != null)
            {
                exceptionMessage.Append("Request Body:\r\n" + requestBody + "\r\n\r\n");
            }
            if (response.Headers.Count > 0)
            {
                exceptionMessage.Append("Response Headers:\r\n" + response.Headers);
            }
            using (var receiveStream = response.GetResponseStream())
            {
                if (receiveStream != null)
                {
                    using (var readStream = new StreamReader(receiveStream, responseEncoding))
                    {
                        exceptionMessage.Append("Response Body:\r\n" + readStream.ReadToEnd() + "\r\n");
                    }
                }
            }

            return exceptionMessage.ToString();
        }
    }
}
