namespace Framework.Common.Api.Endpoints
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;

    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;
    
    /// <summary>
    /// The security header factory.
    /// </summary>
    public static class SecurityHeaderFactory
    {
        /// <summary>
        /// The map that set connections with endpoints and required additional headers for this endpoint.
        /// </summary>
        private static Dictionary<CobaltModuleId, Action> addHeaderForModule = new Dictionary<CobaltModuleId, Action>();

        /// <summary>
        /// The get security headers.
        /// </summary>
        /// <param name="moduleId">
        /// The module id for API client.
        /// </param>
        /// <param name="user">
        /// The onePass user info.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="sessionId">
        /// The session id for current user.
        /// </param>
        /// <param name="workProductToken">
        /// The work product token.
        /// </param>
        /// <param name="additionalHeaders">
        /// The additional Headers.
        /// </param>
        /// <returns>
        /// The <see cref="NameValueCollection"/>.
        /// </returns>
        public static NameValueCollection GetSecurityHeaders(
            CobaltModuleId moduleId,
            IOnePassUserInfo user,
            EnvironmentInfo environment,
            CobaltProductInfo product,
            string sessionId = null,
            string workProductToken = null,
            params KeyValuePair<string, string>[] additionalHeaders)
        {
            return SecurityHeaderFactory.GetSecurityHeaders(
                moduleId,
                user.PrismGuid,
                user.UserName,
                environment,
                product,
                sessionId,
                workProductToken,
                additionalHeaders);
        }

        /// <summary>
        /// The get security headers.
        /// </summary>
        /// <param name="moduleId">
        /// The module id.
        /// </param>
        /// <param name="userPrismGuid">
        /// The user prism GUID.
        /// </param>
        /// <param name="onePassUsername">
        /// The one pass username.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <param name="sessionId">
        /// The session id.
        /// </param>
        /// <param name="workProductToken">
        /// The work product token.
        /// </param>
        /// <param name="additionalHeaders">
        /// The additional Headers.
        /// </param>
        /// <returns>
        /// The <see cref="NameValueCollection"/>.
        /// </returns>
        public static NameValueCollection GetSecurityHeaders(
            CobaltModuleId moduleId,
            string userPrismGuid,
            string onePassUsername,
            EnvironmentInfo environment,
            CobaltProductInfo product,
            string sessionId = null,
            string workProductToken = null,
            params KeyValuePair<string, string>[] additionalHeaders)
        {
            var securityHeader = new NameValueCollection
                                     {
                                         { "x-cobalt-security-userguid", userPrismGuid },
                                         {
                                             "x-cobalt-security-onepassusernameencoded",
                                             onePassUsername
                                         },
                                         {
                                             "x-cobalt-product-container",
                                             string.Format("{0}-{1}", product.Id, environment.Id)
                                         },
                                         {
                                             "x-cobalt-security-uds",
                                             string.Format(
                                                 "{0}/",
                                                 TestConfigurationRepository.DefaultInstance
                                                                            .FindEndpoint(
                                                                                CobaltModuleId.Uds,
                                                                                product.Id,
                                                                                environment.Id).Uri)
                                         },
                                         { "x-trmr-businessunit", "LEGAL-US-CORE" },
                                         {
                                             "X-TRMR-Product",
                                             string.Format("{0}-{1}", product.Id, environment.Id)
                                         }
                                     };

            if (!string.IsNullOrEmpty(workProductToken))
            {
                string auth = string.Format("dataroom.workproducttoken.v1 {0}", workProductToken);
                securityHeader.Add("Authorization", auth);
            }

            if (!string.IsNullOrEmpty(sessionId))
            {
                securityHeader.Add("x-cobalt-security-sessionid", sessionId);
            }

            SecurityHeaderFactory.InitializeModuleHeaderMap(securityHeader, environment, product);

            if (addHeaderForModule.Keys.Contains(moduleId))
            {
                addHeaderForModule[moduleId]();
            }

            foreach (KeyValuePair<string, string> header in additionalHeaders)
            {
                securityHeader.Add(header.Key, header.Value);
            }

            return securityHeader;
        }

        /// <summary>
        /// The initialize module-header map.
        /// </summary>
        /// <param name="header">
        /// The header.
        /// </param>
        /// <param name="environment">
        /// The environment.
        /// </param>
        /// <param name="product">
        /// The product.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// </exception>
        private static void InitializeModuleHeaderMap(
            NameValueCollection header,
            EnvironmentInfo environment,
            CobaltProductInfo product)
        {
            addHeaderForModule[CobaltModuleId.Uds] =
                () => header.Add("X-TRMR-Product", string.Format("{0}-{1}", product.Id, environment.Id));
            addHeaderForModule[CobaltModuleId.Document] = () =>
                {
                    header.Add("X-TRMR-Product", string.Format("{0}-{1}", product.Id, environment.Id));
                    header.Add("x-trmr-businessunit", "LEGAL-US-CORE");
                };
            addHeaderForModule[CobaltModuleId.Image] = () =>
                {
                    header.Add("x-cobalt-pcid", "QED-GUNMETAL-TEST-FEATURE");
                    header.Add("x-trmr-businessunit", "LEGAL-US-CORE");
                };
            addHeaderForModule[CobaltModuleId.CobaltServices] = () =>
                {
                    string prefferedSite = string.Format(
                        "preferredSite={0};product={1}",
                        environment.Site.ToLower(),
                        product.InternalName);
                    header.Add("x-cs-route-map", prefferedSite);
                };
        }
    }
}