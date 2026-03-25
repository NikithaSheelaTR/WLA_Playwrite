namespace Framework.Common.Api.Raw.IPhoneMode.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Security.Policy;
    using Framework.Common.Api.Raw.IPhoneMode.Models.Requests;
    using Framework.Common.Api.Raw.IPhoneMode.Utilities.Enums;
    using Framework.Core.CommonTypes.Constants;

    /// <summary>
    /// The UDS session.
    /// </summary>
    public static class UdsSession
    {
        /// <summary>
        /// Gets or sets the b authentication session.
        /// </summary>
        private static string BAuthSession { get; set; }

        /// <summary>
        /// Gets or sets the c authentication session.
        /// </summary>
        private static string CAuthSession { get; set; }

        /// <summary>
        /// Gets or sets the CI authentication session.
        /// </summary>
        private static string CiAuthSession { get; set; }

        /// <summary>
        /// Gets or sets the demo authentication session.
        /// </summary>
        private static string DemoAuthSession { get; set; }

        /// <summary>
        /// Gets or sets the hot prod session.
        /// </summary>
        private static string HotProdSession { get; set; }

        /// <summary>
        /// Gets or sets the next authentication session.
        /// </summary>
        private static string NextAuthSession { get; set; }

        /// <summary>
        /// Gets or sets the QED authentication session.
        /// </summary>
        private static string QedAuthSession { get; set; }

        /// <summary>
        /// The get authentication session dictionary.
        /// </summary>
        /// <param name="signOnParams"> The sign on parameters. </param>
        /// <returns> The Dictionary. </returns>
        public static Dictionary<string, object> GetAuthSessionDictionary(SignOnParams signOnParams)
        {
            return UdsSession.GetAuthSession(
                signOnParams.Domain,
                signOnParams.CobaltEnvironment,
                signOnParams.Cookies["Co_SessionToken"].Value,
                signOnParams.Cookies,
                signOnParams.Url
              ) ;
        }

        /// <summary>
        /// The set authentication session.
        /// </summary>
        /// <param name="domain"> The domain. 
        /// <param name="url"> The cookies. </param>
        /// </param>
        /// 
        public static void SetAuthSession(string domain,string url)
        {
            if (url.ToLowerInvariant().Contains("region") & url.ToLowerInvariant().Contains("use2"))
            {
                string site = "use2";
                domain = domain.Replace('.', '-').ToLower();
                var domainNames = domain.Split('-').ToList();
                string hostId = domainNames.Any(x => x.StartsWith("demo") || x.Contains("ci") || x.Contains("demoaws")) ? "30962" : "92615";

                DemoAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                CiAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                BAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v5/authsession/";
                CAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v5/authsession/";
                QedAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                NextAuthSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v2/authsession/";
                HotProdSession = $"https://uds-int-{domain}-{site}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
            }
            else if (url.ToLowerInvariant().Contains("region"))
            {
         
                domain = domain.Replace('.', '-').ToLower();
                var domainNames = domain.Split('-').ToList();
                string hostId = domainNames.Any(x => x.StartsWith("demo") || x.Contains("ci") || x.Contains("demoaws")) ? "30962" : "92615";

                DemoAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                CiAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                BAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v5/authsession/";
                CAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v5/authsession/";
                QedAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
                NextAuthSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v2/authsession/";
                HotProdSession = $"https://uds-int-{domain}.{hostId}.aws-int.thomsonreuters.com" + "/UDS/v8/authsession/";
            }
            else
            {

                DemoAuthSession = "http://uds.int." + domain + "/UDS/v8/authsession/";
                CiAuthSession = "https://uds.int." + domain + "/UDS/v8/authsession/";
                BAuthSession = "http://uds.int." + domain + "/UDS/v5/authsession/";
                CAuthSession = "http://uds.int." + domain + "/UDS/v5/authsession/";
                QedAuthSession = "http://uds.int." + domain + "/UDS/v8/authsession/";
                NextAuthSession = "http://uds.int." + domain + "/UDS/v2/authsession/";
                HotProdSession = "http://uds.int." + domain + "/UDS/v8/authsession/";
            }
        }

        /// <summary>
        /// The get authentication session.
        /// </summary>
        /// <param name="domain"> The domain. </param>
        /// <param name="env"> The environment. </param>
        /// <param name="longToken"> The long token. </param>
        /// <param name="cookies"> The cookies. </param>
        ///  <param name="url"> The cookies. </param>
        /// <returns> The Dictionary. </returns>
        private static Dictionary<string, object> GetAuthSession(
            string domain,
            CobaltEnvironment env,
            string longToken,
            CookieCollection cookies,
            string url)
        {
            
            UdsSession.SetAuthSession(domain, url);
            string str;
            switch (env)
            {
                case CobaltEnvironment.Ci:
                    str = CiAuthSession;
                    break;
                case CobaltEnvironment.Demo:
                    str = DemoAuthSession;
                    break;
                case CobaltEnvironment.Qed:
                    str = QedAuthSession;
                    break;
                case CobaltEnvironment.Next:
                    str = NextAuthSession;
                    break;
                case CobaltEnvironment.B:
                    str = BAuthSession;
                    break;
                case CobaltEnvironment.C:
                    str = CAuthSession;
                    break;
                case CobaltEnvironment.HotProd:
                    str = HotProdSession;
                   break;
                default:
                    str = DemoAuthSession;
                    break;
            }

            Dictionary<string, object> dictionary;
            using (HttpWebResponse response = HttpRequestUtil.CallHttpGet(str + longToken, cookies, "application/json"))
            {
                dictionary =
                    JsonUtilities.DeserializeFromJsonStringToDictionaryObject(
                        WebServiceExtensions.GetStringFromResponse(response));
                response.Close();
            }

            return dictionary;
        }
    }
}