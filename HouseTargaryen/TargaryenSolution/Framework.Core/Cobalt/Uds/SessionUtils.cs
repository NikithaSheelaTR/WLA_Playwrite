namespace Framework.Core.Cobalt.Uds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading;

    using Framework.Core.Cobalt.Url;
    using Framework.Core.CommonTypes.Enums.Environment;
    using Framework.Core.CommonTypes.Enums.Setup;
    using Framework.Core.CommonTypes.Settings;
    using Framework.Core.DataModel.Configuration.Settings;
    using Framework.Core.Url;
    using Framework.Core.Utils;

    /// <summary>
    /// Utility used to retrieve the session id for a WLN session
    /// </summary>
    public class SessionUtils
    {
        /// <summary>
        /// Gets the session for the current test
        /// </summary>
        /// <param name="urlManager"></param>
        /// <param name="testSettings"></param>
        /// <param name="actualSite"></param>
        /// <returns>the current session id</returns>
        public string GetOnlineCobaltSession(TestSettings testSettings, UrlManager urlManager, CobaltSite actualSite)
        {
            string udsUrl = urlManager.GetUrl(CobaltUrlType.UDS, testSettings);
            string userId = testSettings.GetValue<string>(CobaltTestSettingKeys.USER_PRISM_GUID);
            string site = actualSite.GetName();
            var product = testSettings.GetValue<IProduct>(CobaltTestSettingKeys.TEST_PRODUCT);

            string productName = product.GetProductName();

            const int SessionInitTimeout = 10;
            string sessionId = null;

            var service = new SessionService(udsUrl);

            try
            {
                var cookies = new CookieCollection { new Cookie("site", site.ToLower()) };
                IList<Session> sessions = service.RetrieveSessions(
                    site.ToUpper(),
                    Session.Status.Online,
                    userId: userId,
                    product: productName,
                    cookies: cookies);

                if (sessions != null && !sessions.Any())
                {
                    Thread.Sleep(SessionInitTimeout * 1000);
                    sessions = service.RetrieveSessions(
                        site.ToUpper(),
                        Session.Status.Online,
                        userId: userId,
                        product: productName,
                        cookies: cookies);
                }

                if (sessions != null && sessions.Any())
                {
                    sessionId = sessions.Last().SessionId;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Logger.LogError("Error retrieving session id - no sessions found after {0} seconds", SessionInitTimeout);
            }

            return sessionId;
        }

        /// <summary>
        /// GetSessionInfoLink
        /// </summary>
        /// <param name="testSettings">The test Settings.</param>
        /// <param name="sessionId">The session Id.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSessionInfoLink(CobaltTestSettings testSettings, string sessionId)
            => new CobaltUrlManager().GetUrl(CobaltUrlType.SESSION_INFO, testSettings, sessionId);
    }
}
