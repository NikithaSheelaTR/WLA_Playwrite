namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities.Enums;
    using Framework.Core.CommonTypes.Constants;
    using Thomson.Novus.Search;

    /// <summary>
    /// The website session endpoint.
    /// </summary>
    public static class WebsiteSessionEndpoint
    {
        /// <summary>
        /// The execute post to website session.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <exception cref="Exception"> if Did not get all required cookies back from the session request </exception>
        public static void ExecutePostToWebsiteSession(SignOnParams signOnParams)
        {
            string isDcExit = null; // Environment.GetEnvironmentVariable(EnvironmentConstants.IsDcExit);
           
            string url = signOnParams.Url;

            if (url.ToLowerInvariant().Contains("region"))
            {
                isDcExit = "Yes";

            }

                string postData = WebsiteSessionEndpoint.GeneratePostBody(signOnParams);
            using (
                HttpWebResponse response = HttpRequestUtil.SendPostRequest(
                    signOnParams.Url + "/V1/Session",
                    postData,
                    signOnParams.Cookies))
            {
                Dictionary<string, object> dictionary =
                    JsonUtilities.DeserializeFromJsonStringToDictionaryObject(
                        WebServiceExtensions.GetStringFromResponse(response));
                if ((int)dictionary["StatusCode"] != 0)
                {
                    throw new Exception(dictionary["Message"].ToString());
                }

                if ((signOnParams.Url + "/V1/Session").Contains("/V1/Session"))
                {
                    signOnParams.Cookies = new CookieCollection();
                }

                foreach (Cookie cookie in response.Cookies)
                {
                    if (cookie.Domain.StartsWith("ip."))
                    {
                        cookie.Domain = cookie.Domain.Substring(3);
                    }
                }

                signOnParams.Cookies.Add(response.Cookies);
                response.Close();
            }
            if (isDcExit != null && isDcExit.Equals("Yes"))
            {
                if (signOnParams.Cookies["Co_SessionToken"] == null || signOnParams.Cookies["site"] == null)
               
                {
                    throw new Exception("Did not get all required cookies back from the session request");
                }
            }
            else
            {
                if (signOnParams.Cookies["Co_SessionToken"] == null || signOnParams.Cookies["site"] == null
                || signOnParams.Cookies["ig"] == null)
                {
                    throw new Exception("Did not get all required cookies back from the session request");
                }
            }
        }

        /// <summary>
        /// The generate post body.
        /// </summary>
        /// <param name="signOnParams">
        /// The sign on parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GeneratePostBody(SignOnParams signOnParams)
        {
            string url = signOnParams.Url;
            string str1;
            if (url.ToLowerInvariant().Contains("ci") || url.ToLowerInvariant().Contains("demo")
                || url.ToLowerInvariant().Contains("qed") || url.ToLowerInvariant().Contains("demoaws")
                || url.ToLowerInvariant().Contains("qedaws"))
            {
                string str2 = signOnParams.CobaltApplication.Equals(CobaltApplication.Mobile) ? "MobileWeb" : "Web";
                str1 = "{\"UserId\":\"" + signOnParams.UserId + "\",\"Password\":\"" + signOnParams.Password
                       + "\",\"SourceType\":\"" + str2 + "\"}";
            }
            else
            {
                str1 = "{\"UserId\":\"" + signOnParams.UserId + "\",\"Password\":\"" + signOnParams.Password + "\"}";
            }

            return str1;
        }
    }
}