namespace Framework.Common.Api.Endpoints
{
    using System.Collections.Specialized;
    using System.Net;

    using Framework.Common.Api.Endpoints.CobaltServices;
    using Framework.Common.Api.Endpoints.CobaltServices.DataModel;
    using Framework.Common.Api.Endpoints.Uds;
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.Api.Endpoints.Website;
    using Framework.Common.Api.Enums;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// The website manager.
    /// </summary>
    public class WebsiteManager
    {
        /// <summary>
        /// Delete DataRoom User Propety
        /// </summary>
        /// <param name="propertyName"> property name </param>
        /// <param name="cobaltProduct"> cobalt product </param>
        /// <param name="environment"> environment </param>
        public static void DeleteDataRoomUserPropety(DataRoomUserProperties propertyName, CobaltProductInfo cobaltProduct, EnvironmentInfo environment)
        {
            // Get User Info for Current Test Execution
            var user = CredentialPool.GetFirstOrDefaultUser<IOnePassUserInfo>();

            // Start Session
            var cobaltSessionManager = new CobaltSessionManager(environment, cobaltProduct, user);
            cobaltSessionManager.StartSession();
            string sessionId = cobaltSessionManager.GetSessionInfo().SessionId;
            EnvironmentInfo actualTestEnvironment =
                TestConfigurationRepository.DefaultInstance.ResolveEnvironmentForSite(
                    cobaltSessionManager.SessionInfo.Site, environment);

            // Get Token
            var udsServicesClient = ApiClientFactory.GetInstance<UdsSessionClient>(user, sessionId, cobaltProduct, actualTestEnvironment);
            WorkProductTokenResponse workProductTokenjson = udsServicesClient.GetWorkProductToken(sessionId);
            string workProductToken = workProductTokenjson.WorkProductToken;
            string dataroomId = workProductTokenjson.TokenHolderUserId;

            // Get Security Header
            NameValueCollection securityHeader = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.CobaltServices,
                user,
                actualTestEnvironment,
                cobaltProduct,
                sessionId,
                workProductToken);

            // Create Client
            var cobaltServicesClient = ApiClientFactory.GetInstance<CobaltServicesClient>(securityHeader, cobaltProduct, actualTestEnvironment);
            cobaltServicesClient.DeleteDataRoomUserProperty(dataroomId, propertyName);

            // Sign Off Session
            cobaltSessionManager.KillSession();
        }

        /// <summary>
        /// The set preferences.
        /// </summary>
        /// <param name="userInfo">
        /// The user info.
        /// </param>
        /// <param name="cobaltProduct">
        /// The cobalt product.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="sessionCookies">
        /// The session cookies.
        /// </param>
        /// <param name="vertical">
        /// The vertical.
        /// </param>
        /// <param name="preferenceName">
        /// The preference name.
        /// </param>
        /// <param name="preferenceValue">
        /// The preference value.
        /// </param>
        public static void SetPreferences(
            IOnePassUserInfo userInfo,
            CobaltProductInfo cobaltProduct,
            EnvironmentInfo environment,
            CookieContainer sessionCookies,
            VerticalName vertical,
            PreferenceName preferenceName,
            string preferenceValue) =>
            WebsiteManager.GetWebsiteClient(userInfo, cobaltProduct, environment, sessionCookies)
                          .SetPreferences(vertical, preferenceName, preferenceValue);

        /// <summary>
        /// Set user settings
        /// </summary>
        /// <param name="userInfo">The onePass user info.</param>
        /// <param name="cobaltProduct">The product.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="sessionCookies">The cookies.</param>
        /// <param name="preferenceName">The preference name.</param>
        /// <param name="preferenceValue">The preference value.</param>
        public static void SetUserSettings(
           IOnePassUserInfo userInfo,
           CobaltProductInfo cobaltProduct,
           EnvironmentInfo environment,
           CookieContainer sessionCookies,
           PreferenceName preferenceName,
           string preferenceValue) =>
            WebsiteManager.GetWebsiteClient(userInfo, cobaltProduct, environment, sessionCookies)
                          .SetUserSettings(preferenceName, preferenceValue);

        /// <summary>
        /// The get website client.
        /// </summary>
        /// <param name="userInfo">
        /// The user info.
        /// </param>
        /// <param name="cobaltProduct">
        /// The cobalt product.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="sessionCookies">
        /// The session cookies.
        /// </param>
        /// <returns>
        /// The <see cref="WebsiteClient"/>.
        /// </returns>
        private static WebsiteClient GetWebsiteClient(
            IOnePassUserInfo userInfo,
            CobaltProductInfo cobaltProduct,
            EnvironmentInfo environment,
            CookieContainer sessionCookies)
        {
            NameValueCollection sessionHeaders = SecurityHeaderFactory.GetSecurityHeaders(
                CobaltModuleId.Website,
                userInfo,
                environment,
                cobaltProduct);

            // Creating WeibSite client to execute WebSite Calls
            return ApiClientFactory.GetInstance<WebsiteClient>(
                sessionHeaders,
                cobaltProduct,
                environment,
                sessionCookies);
        }
    }
}
