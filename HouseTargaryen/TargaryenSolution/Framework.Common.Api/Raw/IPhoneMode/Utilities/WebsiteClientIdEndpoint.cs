namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities.Enums;

    /// <summary>
    /// The website client id endpoint.
    /// </summary>
    public static class WebsiteClientIdEndpoint
    {
        /// <summary>
        /// The execute post to website client id.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <exception cref="Exception"> if HttpStatusCode != OK </exception>
        public static void ExecutePostToWebsiteClientId(SignOnParams signOnParams)
        {
            if (signOnParams.BypassClientId)
            {
                return;
            }

            string str = string.Empty;
            string postData;
            if (signOnParams.CobaltApplication.Equals(CobaltApplication.Mobile))
            {
                postData = "{\"ClientMatter\":{\"ClientID\":\"" + signOnParams.ClientId
                           + "\"},\"BillingMethod\":null,\"BillingDoNotAskAtSignOn\":false}";
            }
            else
            {
                str = WebsiteClientIdEndpoint.GetPcid(signOnParams);
                postData = "{\"ClientMatter\":{\"ClientID\":\"" + signOnParams.ClientId + "\"}}";
            }

            var headerCollection = new WebHeaderCollection { { "X-Cobalt-Pcid", str } };
            WebHeaderCollection headers = headerCollection;
            using (
                HttpWebResponse httpWebResponse =
                    HttpRequestUtil.SendPostRequest(
                        signOnParams.Url + "/V1/Session/BeginResearch",
                        postData,
                        signOnParams.Cookies,
                        headers,
                        "application/json",
                        "application/json"))
            {
                if (httpWebResponse.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception(
                        "Client ID was not set in the request to " + signOnParams.Url + "/V1/Session/BeginResearch");
                }

                httpWebResponse.Close();
            }
        }

        /// <summary>
        /// The get PCid.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <returns> The <see cref="string"/>. </returns>
        private static string GetPcid(SignOnParams signOnParams)
        {
            var cookieCollection = new CookieCollection();
            foreach (Cookie cookie in signOnParams.Cookies)
            {
                if (cookie.Name != "SessionId")
                {
                    cookieCollection.Add(cookie);
                }
            }

            List<string> linesFromResponse;
            using (
                HttpWebResponse httpWebResponse =
                    HttpRequestUtil.CallHttpGet(signOnParams.Url + "/Search/Home.html?bhskip=1", cookieCollection))
            {
                linesFromResponse = WebServiceExtensions.GetLinesFromResponse(httpWebResponse.GetResponseStream());
                httpWebResponse.Close();
            }

            string str1 = string.Empty;
            foreach (string str2 in linesFromResponse)
            {
                if (str2.StartsWith("Cobalt.Website.Events.PageEventIdentifier"))
                {
                    str1 = str2.Split('=')[1].Split('"')[1];
                    break;
                }
            }

            return str1;
        }
    }
}