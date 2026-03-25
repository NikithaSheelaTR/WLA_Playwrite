namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System.Collections.Generic;
    using System.Net;

    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Core.DataModel.Security.Specialized;

    /// <summary>
    /// Session Utile
    /// </summary>
    public static class SessionUtil
    {
        /// <summary>
        /// The create sign on session cookies.
        /// </summary>
        /// <param name="url"> The URL. </param>
        /// <param name="userid"> The user id. </param>
        /// <param name="password"> The password. </param>
        /// <param name="clientId"> The client id. </param>
        /// <returns> The <see cref="CookieCollection"/>. </returns>
        public static CookieCollection CreateSignOnSessionCookies(
            string url,
            string userid,
            string password,
            string clientId)
        {
            return SessionUtil.GetSignOnCookies(new SignOnParams(url, userid, password, clientId));
        }

        /// <summary>
        /// Get Session
        /// </summary>
        /// <param name="userCredential"> The user. </param>
        /// <param name="environment"> The environment. </param>
        /// <returns> Cookie Collection  </returns>
        public static CookieCollection GetSessionCookies(UserCredential userCredential, string environment)
        {
            while (true)
            {
                CookieCollection cookies = null;
                try
                {
                    cookies = SessionUtil.CreateSignOnSessionCookies(
                        environment,
                        userCredential.UserName,
                        userCredential.Password,
                        "Test");
                }
                catch
                {
                    // ignored
                }

                if (cookies != null)
                {
                    return cookies;
                }
            }
        }

        /// <summary>
        /// The get all sign on stuff.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <returns> The <see cref="CobaltCookiesAndUdsData"/>. </returns>
        private static CobaltCookiesAndUdsData GetAllSignOnStuff(SignOnParams signOnParams)
        {
            var cookiesAndUdsData = new CobaltCookiesAndUdsData();
            WebsiteSessionEndpoint.ExecutePostToWebsiteSession(signOnParams);
            Dictionary<string, object> sessionDictionary = UdsSession.GetAuthSessionDictionary(signOnParams);
            WebsiteClientIdEndpoint.ExecutePostToWebsiteClientId(signOnParams);
            cookiesAndUdsData.CobaltCookies = signOnParams.Cookies;
            cookiesAndUdsData.UdsData = sessionDictionary;
            return cookiesAndUdsData;
        }

        /// <summary>
        /// The get sign on cookies.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <returns> The <see cref="CookieCollection"/>. </returns>
        private static CookieCollection GetSignOnCookies(SignOnParams signOnParams)
        {
            CobaltCookiesAndUdsData allSignOnStuff = SessionUtil.GetAllSignOnStuff(signOnParams);
            CookieCollection cobaltCookies = allSignOnStuff.CobaltCookies;
            cobaltCookies.Add(
                new Cookie(
                    "SessionId",
                    allSignOnStuff.UdsData["SessionId"].ToString(),
                    signOnParams.Cookies["Co_SessionToken"].Path,
                    signOnParams.Cookies["Co_SessionToken"].Domain));
            return cobaltCookies;
        }

        /// <summary>
        /// The cobalt cookies and UDS data.
        /// </summary>
        public class CobaltCookiesAndUdsData
        {
            /// <summary>
            /// Gets the cobalt cookies.
            /// </summary>
            public CookieCollection CobaltCookies { get; internal set; }

            /// <summary>
            /// Gets the UDS data.
            /// </summary>
            public Dictionary<string, object> UdsData { get; internal set; }
        }
    }
}