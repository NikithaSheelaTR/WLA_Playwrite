namespace Framework.Common.Api.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;

    using Framework.Common.Api.Endpoints.DataModel;
    using Framework.Common.Api.Endpoints.OnePass;
    using Framework.Common.Api.Endpoints.Uds;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Enums.Uds;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Constants;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Execution;

    using RestSharp;

    /// <summary>
    /// The session manager class that helps to atart and terminate a UDS session as well as extract information 
    /// about the session for the specified OnePass user.
    /// </summary>
    public class CobaltSessionManager
    {
        /// <summary>
        /// The environment.
        /// </summary>
        public EnvironmentInfo Environment { get; private set; }

        /// <summary>
        /// The product.
        /// </summary>
        public CobaltProductInfo Product => this.product;

        /// <summary>
        /// Gets the session cookies.
        /// </summary>
        public CookieContainer SessionCookies { get; private set; }

        /// <summary>
        /// Gets the session headers.
        /// </summary>
        public NameValueCollection SessionHeaders { get; private set; }

        /// <summary>
        /// Gets the UDS session info.
        /// </summary>
        public UdsSessionInfo SessionInfo { get; private set; }

        /// <summary>
        /// The user info.
        /// </summary>
        public IOnePassUserInfo UserInfo => this.userInfo;

        /// <summary>
        /// Redirection address
        /// </summary>
        public Uri HomePageRedirectedUrl { get; private set; }

        /// <summary>
        /// The supported products.
        /// </summary>
        private static readonly CobaltProductId[] SupportedProducts =
        {
            CobaltProductId.WestlawNext,
            CobaltProductId.WlnTax,
            CobaltProductId.WlAnalytics,
            CobaltProductId.WestlawEdge,
            CobaltProductId.WestlawPrecisionAws,
            CobaltProductId.CaseNotebook,
            CobaltProductId.WestKm,
            CobaltProductId.Anz
        };

        /// <summary>
        /// The product.
        /// </summary>
        private readonly CobaltProductInfo product;

        /// <summary>
        /// The user info.
        /// </summary>
        private readonly IOnePassUserInfo userInfo;

        /// <summary>
        /// The cookie info.
        /// </summary>
        private CookieInfo cookieInfo;

        /// <summary>
        /// Flag to indicate that session is started
        /// </summary>
        private bool isSessionStarted;

        /// <summary>
        /// Represets the current uds session starting process
        /// </summary>
        private UdsSessionType SessionType { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="CobaltSessionManager"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="userInfo">
        /// The user Info.
        /// </param>
        public CobaltSessionManager(EnvironmentInfo environment, CobaltProductInfo product, IOnePassUserInfo userInfo)
        {
            if (!SupportedProducts.Contains(product.Id))
            {
                throw new ArgumentException($"Product '{product.Id}' is not supported", "product");
            }

            this.SessionInfo = new UdsSessionInfo();
            this.cookieInfo = new CookieInfo();
            this.Environment = environment;
            this.product = product;
            this.userInfo = userInfo;
            this.isSessionStarted = false;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CobaltSessionManager"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="userInfo">
        /// The user Info.
        /// </param>
        /// <param name="sessionCookies">
        /// Session cookies
        /// </param>
        public CobaltSessionManager(EnvironmentInfo environment, CobaltProductInfo product, IOnePassUserInfo userInfo, CookieContainer sessionCookies)
        {
            if (!SupportedProducts.Contains(product.Id))
            {
                throw new ArgumentException($"Product '{product.Id}' is not supported", "product");
            }

            this.SessionInfo = new UdsSessionInfo();
            this.SessionCookies = sessionCookies;
            this.Environment = environment;
            this.product = product;
            this.userInfo = userInfo;
            this.isSessionStarted = true;
        }

        /// <summary>
        /// Gets the info about session bindings for a user (IAC, FAC, application configuration and routing table settings)
        /// </summary>
        /// <param name="siteRetriever">
        /// The delegate that returns info about the current site for lower environment (e.g. "b" or "a" for QED)
        /// If NULL, this method will try to retrieve info from all possible sites but without a performance penalty.
        /// </param>
        /// <returns>
        /// The <see cref="SessionBindings"/>.
        /// </returns>
        public SessionBindings GetSessionBindings(Func<string> siteRetriever = null)
        {
            UdsSessionInfo sessionInfo = this.GetSessionInfo(siteRetriever);
            EnvironmentInfo currentEnvironment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    sessionInfo.Site,
                    this.Environment);

            var udsClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                this.userInfo,
                sessionInfo.SessionId,
                this.product,
                currentEnvironment,
                this.SessionCookies);

            SessionBindings sessionBindings = udsClient.GetSessionBindings(sessionInfo.SessionId);
            return sessionBindings;
        }

        /// <summary>
        /// Gets the session info for a user.
        /// </summary>
        /// <param name="siteRetriever">
        /// The delegate that returns info about the current site for lower environment (e.g. "b" or "a" for QED)
        /// If NULL, this method will try to retrieve info from all possible sites but without a performance penalty.
        /// </param>
        /// <returns>
        /// The <see cref="UdsSessionInfo"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Bad case
        /// </exception>
        public UdsSessionInfo GetSessionInfo(Func<string> siteRetriever = null)
        {
            if (this.isSessionStarted)
            {
                return this.SessionInfo;
            }

            // Try specify environment because UDS client only works with an environment specific site
            EnvironmentInfo[] possibleEnvironments = this.RetrieveEnviroments(siteRetriever);

           // var udsClient=String.Empty;
              UdsSessionInfo sessionInfo = null;

            for (int i = 0; sessionInfo == null && i < possibleEnvironments.Length; i++)
            {
                NameValueCollection headers = SecurityHeaderFactory.GetSecurityHeaders(
                    CobaltModuleId.Uds,
                    this.UserInfo,
                    possibleEnvironments[i],
                    this.Product);
                string text = possibleEnvironments[i].TagName;

                /* if (text == "QedBAWS" || text == "QedAAWS")
                 {
                     udsClient = new UdsSessionClient(possibleEnvironments[i], this.Product.Id, null, headers);
                 }
                 else
                 {*/
                var udsClient =new  UdsSessionClient(possibleEnvironments[i], this.Product.Id, null, headers);
                

                string encodedLongToken = udsClient.GetLongToken(this.UserInfo.PrismGuid, this.Product.InternalName);

                if (!string.IsNullOrEmpty(encodedLongToken))
                {
                    sessionInfo = udsClient.GetAuthSessionInfo(encodedLongToken);
                }
            }

            if (sessionInfo == null)
            {
                throw new ArgumentException(
                    $"There is no current online session for user {this.UserInfo.UserName} in {this.Environment.Name}");
            }

            return sessionInfo;
        }

        /// <summary>
        /// Gets the current UI session info for a user.
        /// </summary>
        /// <param name="sessionToken"></param>>
        /// <returns>
        /// The <see cref="UdsSessionInfo"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// Bad case
        /// </exception>
        public UdsSessionInfo GetCurrentUiSessionInfo(string sessionToken)
        {
            this.SessionHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                     CobaltModuleId.Uds,
                this.UserInfo,
                this.Environment,
                this.Product);

            var udsClient = new UdsSessionClient(
                this.Environment,
                this.Product.Id,
                null,
                this.SessionHeaders);

            UdsSessionInfo sessionInfo = udsClient.GetAuthSessionInfo(sessionToken, 5);

            if (sessionInfo == null)
            {
                throw new ArgumentException(
                    $"There is no current online session for user {this.UserInfo.UserName} in {this.Environment.Name}");
            }

            return sessionInfo;
        }

        /// <summary>
        /// The kill uds session.
        /// </summary>
        public void KillSession()
        {
            if (!this.isSessionStarted)
            {
                return;
            }

            switch (this.SessionType)
            {
                case UdsSessionType.Uds:
                    var udsClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                        this.SessionHeaders,
                        this.product,
                        this.Environment,
                        this.SessionCookies);
                    udsClient.KillSession(this.SessionInfo);
                    break;
                default:
                    var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                        this.SessionHeaders,
                        this.product,
                        this.Environment,
                        this.SessionCookies);
                    websiteClient.SignOffCurrentSession();
                    break;
            }

            this.isSessionStarted = false;
        }

        /// <summary>
        /// The start session.
        /// </summary>
        /// <param name="clientId">
        /// The client id.
        /// </param>
        public void StartSession(string clientId = null)
        {
            this.SessionCookies = this.CreateSignOnSessionCookies();

            this.Environment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    this.cookieInfo.Site,
                    this.Environment);

            this.SessionInfo = this.UpdateSessionInfo();

            this.SessionHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                this.userInfo,
                this.Environment,
                this.product,
                this.SessionInfo.SessionId);

            this.isSessionStarted = true;

            this.SessionType = UdsSessionType.Website;

            if (clientId != null && clientId != "ImagePlatformTests")
            {
                this.SetClientId(clientId);
            }
        }

        /// <summary>
        /// The start session
        /// </summary>
        /// <param name="iacsOn">
        /// List of IACs to turn on
        /// </param>
        /// <param name="iacsOff">
        /// List of IACs to turn off
        /// </param>
        /// <param name="facsOn">
        /// List of FACs to grant
        /// </param>
        /// <param name="facsOff">
        /// List of FACs to deny
        /// </param>
        public void StartSession(
            List<string> iacsOn,
            List<string> iacsOff = null,
            List<string> facsOn = null,
            List<string> facsOff = null)
        {
            // create Website session if none
            if (!this.isSessionStarted)
            {
                this.StartSession();
            }

            // Get Session bindings
            SessionBindings sessionBindings = this.GetSessionBindings();

            // Add/remove IACs
            sessionBindings = sessionBindings.SetIacs(iacsOn).UnsetIacs(iacsOff);

            // Add/remove FACs
            sessionBindings = sessionBindings.GrantFacs(facsOn).DenyFacs(facsOff);

            // reset current session
            UdsSessionInfo newSession = this.ResetSession();

            // Sign off
            this.KillSession();

            // create UDS session
            this.SessionCookies = this.CreateSignOnSessionCookies(newSession);

            // override current session
            this.SessionInfo = newSession;
            this.SessionType = UdsSessionType.Uds;

            this.SessionHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                this.userInfo,
                this.Environment,
                this.product,
                this.SessionInfo.SessionId);

            this.isSessionStarted = true;

            // set session bindings
            this.SetSessionBindings(sessionBindings);
        }

        /// <summary>
        /// Start Web Session through OnePass authentication
        /// </summary>
        /// <param name="routingSettings"></param>
        /// <param name="clientId"></param>
        public void StartSession(Dictionary<string, string> routingSettings, string clientId = null)
        {
            // Define session headers to access Website REST Module. (Assuming the same for OnePassWeb module, even thought OnePassWeb is not Cobalt Module)
            this.SessionHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                this.UserInfo,
                this.Environment,
                this.product);

            // Creating new CookieContainer for authentication purpose. This container will be shared between OnePassWeb and WebSite clients
            this.SessionCookies = new CookieContainer();

            // Creating OnePassWeb client to execute OnePassWeb Calls
            OnePassWebClient onePassClient = ApiClientFactory.GetInstance<OnePassWebClient>(
                 this.SessionHeaders,
                 this.Product,
                 this.Environment,
                 this.SessionCookies);
            // Creating WeibSite client to execute WebSite Calls
            WebsiteClient websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                this.SessionHeaders,
                this.Product,
                this.Environment,
                this.SessionCookies);

            //Represent authentication procedure response
            IRestResponse authResponse = onePassClient.SubmitOnePassAuthorizationForm(
                onePassClient.PassBrowserValidityVerification(
                    websiteClient.PostRoutingSettings(websiteClient.BuildRoutingFormData(routingSettings))),
                (OnePassUserInfo)this.userInfo);

            if (authResponse.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception("OnePass authorization procedure hasn't completed successful");
            }

            // HomePage url
            this.HomePageRedirectedUrl = authResponse.ResponseUri;

            // In case of WestlawNext it's required to submit Client id and etc. info to begin research.
            if (this.UserInfo.GetType() == typeof(WlnUserInfo))
            {
                IRestResponse beginResearch = websiteClient.BeginResearch((WlnUserInfo)this.UserInfo);
                if (beginResearch.StatusCode != HttpStatusCode.OK)
                {
                    throw new Exception("BeginResearch procedure hasn't completed successful");
                }
            }
            else if (clientId != null)
            {
                this.SetClientId(clientId);
            }

            CookieCollection cookies = this.SessionCookies.GetCookies(authResponse.ResponseUri);

            // Get Web_SessionId from RestResponseCookie
            Cookie sessionCookie = cookies["Web_SessionId"];

            if (sessionCookie != null)
            {
                this.cookieInfo.WebSessionId = sessionCookie.Value;

                // Get Co_SessionToken from RestResponseCookie
                sessionCookie = cookies["Co_SessionToken"];
                if (sessionCookie != null)
                {
                    this.cookieInfo.CoSessionToken = sessionCookie.Value;
                }

                // Get Site from RestResponseCookie
                sessionCookie = cookies["site"];
                if (sessionCookie != null)
                {
                    this.cookieInfo.Site = sessionCookie.Value;
                }
            }

            if (this.Environment.IsSiteSpecific)
            {
                this.SessionInfo = this.UpdateSessionInfo();
            }

            this.isSessionStarted = true;
        }

        /// <summary>
        /// Sets the info about session bindings for a user (IAC, FAC, application configuration and routing table settings)
        /// </summary>
        /// <param name="sessionBindings">
        /// Session bindins to update
        /// </param>
        /// <param name="siteRetriever">
        /// The delegate that returns info about the current site for lower environment (e.g. "b" or "a" for QED)
        /// If NULL, this method will try to retrieve info from all possible sites but without a performance penalty.
        /// </param>
        private void SetSessionBindings(SessionBindings sessionBindings, Func<string> siteRetriever = null)
        {
            UdsSessionInfo sessionInfo = this.GetSessionInfo(siteRetriever);

            EnvironmentInfo currentEnvironment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    sessionInfo.Site,
                    this.Environment);

            var udsClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                this.userInfo,
                sessionInfo.SessionId,
                this.product,
                currentEnvironment,
                this.SessionCookies);

            udsClient.SetSessionBindings(sessionInfo.SessionId, sessionBindings);
        }

        /// <summary>
        /// The prepare UdsSession
        /// </summary>
        /// <returns></returns>
        private UdsSessionInfo ResetSession()
        {
            var udsClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                this.SessionHeaders,
                this.product,
                this.Environment,
                this.SessionCookies);

            return udsClient.ResetSession(this.SessionInfo);
        }

        /// <summary>
        /// The create sign on session cookies.
        /// </summary>
        /// <returns>
        /// The <see cref="CookieContainer"/>.
        /// </returns>
        private CookieContainer CreateSignOnSessionCookies()
        {
            NameValueCollection header = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                this.userInfo,
                this.Environment,
                this.product);

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(header, this.product, this.Environment);
            IList<RestResponseCookie> cc = websiteClient.StartSession(this.userInfo.UserName, this.userInfo.Password);

            var cookies = new CookieContainer();

            foreach (RestResponseCookie c in cc)
            {
                cookies.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
            }

            // Get Web_SessionId from RestResponseCookie
            RestResponseCookie sessionCookie = cc.SingleOrDefault(x => x.Name == "Web_SessionId");

            if (sessionCookie != null)
            {
                this.cookieInfo.WebSessionId = sessionCookie.Value;

                // Get Co_SessionToken from RestResponseCookie
                sessionCookie = cc.SingleOrDefault(x => x.Name == "Co_SessionToken");
                if (sessionCookie != null)
                {
                    this.cookieInfo.CoSessionToken = sessionCookie.Value;
                }

                // Get Site from RestResponseCookie
                sessionCookie = cc.SingleOrDefault(x => x.Name == "site");
                if (sessionCookie != null)
                {
                    this.cookieInfo.Site = sessionCookie.Value;
                }
            }

            return cookies;
        }

        /// <summary>
        /// The create sign on session cookies for uds session
        /// </summary>
        /// <param name="session">
        /// Uds session to start
        /// </param>
        /// <returns></returns>
        private CookieContainer CreateSignOnSessionCookies(UdsSessionInfo session)
        {
            NameValueCollection header = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Uds,
                this.userInfo,
                this.Environment,
                this.product);

            var udsClient = ApiClientFactory.GetInstance<UdsSessionClient>(header, this.product, this.Environment, this.SessionCookies);

            IList<RestResponseCookie> cc = udsClient.StartSession(session);

            var cookies = new CookieContainer();

            foreach (RestResponseCookie c in cc)
            {
                cookies.Add(new Cookie(c.Name, c.Value, c.Path, c.Domain));
            }

            // Get Web_SessionId from RestResponseCookie
            RestResponseCookie sessionCookie = cc.SingleOrDefault(x => x.Name == "Web_SessionId");

            if (sessionCookie != null)
            {
                this.cookieInfo.WebSessionId = sessionCookie.Value;

                // Get Co_SessionToken from RestResponseCookie
                sessionCookie = cc.SingleOrDefault(x => x.Name == "Co_SessionToken");
                if (sessionCookie != null)
                {
                    this.cookieInfo.CoSessionToken = sessionCookie.Value;
                }

                // Get Site from RestResponseCookie
                sessionCookie = cc.SingleOrDefault(x => x.Name == "site");
                if (sessionCookie != null)
                {
                    this.cookieInfo.Site = sessionCookie.Value;
                }
            }

            return cookies;
        }

        /// <summary>
        /// The Retrieve Environment. Retrieves current environment
        /// </summary>
        /// <param name="siteRetriever"></param>
        /// <returns></returns>
        private EnvironmentInfo[] RetrieveEnviroments(Func<string> siteRetriever = null)
        {
            //string isDcExit = Environment.
            string[] sites = { this.Environment.Site };
      

            // Try specify environment because UDS client only works with an environment specific site
            if (!this.Environment.IsSiteSpecific)
            {
                
                string[] allPossibleSitesForEnv =
                    TestConfigurationRepository.DefaultInstance.Environments.Where(
                                                   env =>
                                                       env.IsSiteSpecific
                                                       && string.Equals(
                                                           env.Name,
                                                           this.Environment.Name,
                                                           StringComparison.InvariantCultureIgnoreCase))
                                               .Select(env => env.Site)
                                               .Distinct()
                                               .ToArray();

                sites = allPossibleSitesForEnv;

                if (siteRetriever != null)
                {
                    string siteFromRetriever = string.Empty;
                    SafeMethodExecutor.Execute(() => siteFromRetriever = siteRetriever());
                    if (allPossibleSitesForEnv.Contains(siteFromRetriever, StringComparer.InvariantCultureIgnoreCase))
                    {
                        sites = new[] { siteFromRetriever };
                    }
                }
            }

            return
                sites.Select(
                    site =>
                        TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                            site,
                            this.Environment)).ToArray();
        }

        /// <summary>
        /// The set client ID
        /// </summary>
        /// <param name="clientId"></param>
        private void SetClientId(string clientId)
        {
            NameValueCollection header = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                this.userInfo,
                this.Environment,
                this.product,
                this.cookieInfo.WebSessionId);

            var websiteClient = ApiClientFactory.GetInstance<WebsiteClient>(
                header,
                this.product,
                this.Environment,
                this.SessionCookies);
            websiteClient.SetClientId(clientId);
        }

        /// <summary>
        /// The udate session info. 
        /// </summary>
        /// <returns></returns>
        private UdsSessionInfo UpdateSessionInfo()
        {
            NameValueCollection header = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Uds,
                this.userInfo,
                this.Environment,
                this.product,
                this.cookieInfo.WebSessionId);
            this.SessionHeaders = header;

            var udsSessionClient = ApiClientFactory.GetInstance<UdsSessionClient>(
                header,
                this.product,
                this.Environment,
                this.SessionCookies);
            return udsSessionClient.GetAuthSessionInfo(this.cookieInfo.CoSessionToken);
        }
    }
}