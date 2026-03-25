namespace Framework.Common.Api.Utilities
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Threading;

    using Framework.Core.Net;

    using RestSharp;

    /// <summary>
    /// The class for rest client extensions method.
    /// </summary>
    public static class RestClientExtensions
    {
        /// <summary>
        /// The execute put request throw HttpUtils functionality
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="originalRequest">
        /// The original request.
        /// </param>
        /// <returns>
        /// The <see cref="IRestResponse"/>.
        /// </returns>
        /// <exception cref="HttpResponseException">
        /// The endpoint exception
        /// </exception>
        public static IRestResponse ExecuteCustomPut(this IRestClient client, IRestRequest originalRequest)
        {
            var headers = new WebHeaderCollection();

            foreach (Parameter header in originalRequest.Parameters.Where(par => par.Type == ParameterType.HttpHeader))
            {
                headers.Add(header.Name + ":" + header.Value);
            }

            string url = client.BuildUri(originalRequest).AbsoluteUri;

            var cookies = new CookieCollection();

            foreach (Cookie cookie in client.CookieContainer.GetCookies(client.BaseUrl))
            {
                cookies.Add(new Cookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain));
            }

            Parameter body = originalRequest.Parameters.First(par => par.Type == ParameterType.RequestBody);
            HttpWebRequest request = HttpUtils.ConstructHttpWebRequest(
                HttpUtils.HttpMethod.Put,
                url,
                headers: headers,
                cookies: cookies,
                requestBody: body.Value.ToString(),
                requestEncoding: Encoding.UTF8,
                requestMimeType: body.Name);

            // send request to endpoint & parse response
            using (HttpWebResponse response = HttpUtils.GetHttpWebResponse(request))
            {
                var expectedStatusCodes = new[]
                                              {
                                                  HttpStatusCode.OK,
                                                  HttpStatusCode.ResetContent,
                                                  HttpStatusCode.Accepted
                                              };

                if (expectedStatusCodes.Contains(response.StatusCode))
                {
                    var restResponse = new RestResponse
                                           {
                                               ContentEncoding = response.ContentEncoding,
                                               ContentLength = response.ContentLength,
                                               ContentType = response.ContentType,
                                               Request = originalRequest,
                                               ResponseStatus = ResponseStatus.Completed,
                                               ResponseUri = response.ResponseUri,
                                               Server = response.Server,
                                               StatusDescription = response.StatusDescription
                                           };

                    return restResponse;
                }

                string exceptionMessage = HttpUtils.HandleHttpResponseException(
                    response,
                    Encoding.UTF8,
                    request.Headers);
                throw new HttpResponseException(response.StatusCode, exceptionMessage);
            }
        }

        /// <summary>
        /// The perform request call until status of response will become expected.
        /// </summary>
        /// <param name="client">
        /// The client.
        /// </param>
        /// <param name="request">
        /// The request.
        /// </param>
        /// <param name="expectedStatusCodes">
        /// The expected response status.
        /// </param>
        /// <param name="numOfAttempts">
        /// The number of attempts.
        /// </param>
        /// <param name="timeoutBetweenRequests">
        /// The timeout between endpoint calls in milliseconds.
        /// </param>
        /// <returns>
        /// The <see cref="IRestResponse"/>.
        /// </returns>
        public static IRestResponse ExecuteUntil(
            this IRestClient client,
            IRestRequest request,
            HttpStatusCode[] expectedStatusCodes = null,
            int numOfAttempts = 3,
            int timeoutBetweenRequests = 5000)
        {
            expectedStatusCodes = expectedStatusCodes ?? new[] { HttpStatusCode.OK };
            IRestResponse response = client.Execute(request);

            for (int i = 0; !expectedStatusCodes.Contains(response.StatusCode) && i < numOfAttempts - 1; i++)
            {
                Thread.Sleep(timeoutBetweenRequests);
                response = client.Execute(request);
            }

            return response;
        }

        /// <summary>
        /// Invoke expression until it succeed
        /// </summary>
        /// <param name="client">Rest client.</param>
        /// <param name="expression">The expression.</param>
        /// <param name="numOfAttempts">The number of attempts.</param>
        /// <param name="timeoutBetweenRequests">The timeout between endpoint calls in milliseconds.</param>
        public static void ExecuteUntilTrue(
            this IRestClient client,
            Func<bool> expression,
            int numOfAttempts = 3,
            int timeoutBetweenRequests = 5000)
        {
            for (int i = 0; !expression.Invoke() && i < numOfAttempts - 1; i++)
            {
                Thread.Sleep(timeoutBetweenRequests);
            }
        }
    }
}