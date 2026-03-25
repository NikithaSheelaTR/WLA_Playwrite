namespace Framework.Common.UI.Products.WestLawNextLinks.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Products.WestLawNext.Enums;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    using OpenQA.Selenium;

    /// <summary>
    /// The link resolver url manager.
    /// </summary>
    public static class LinksUrlManager
    {
        /// <summary>
        /// Generate Westlaw Links Url
        /// </summary>
        /// <param name="westlawLinksSignOnContext"></param>
        /// <param name="westlawLinksUrl"></param>
        /// <returns></returns>
        /// <exception cref="NotSupportedException"></exception>
        public static string GenerateWestlawLinksUrl(
            WestlawLinksSignOnContext<IUserInfo> westlawLinksSignOnContext,
            string westlawLinksUrl)
        {
            string productUrl;

            switch (westlawLinksSignOnContext.UriResolutionMethod)
            {
                case UriResolutionMethod.None:
                    productUrl = LinksUrlManager.GenerateLinksUrl(
                        westlawLinksUrl,
                        westlawLinksSignOnContext.UrlPath,
                        westlawLinksSignOnContext.UrlParameters);
                    break;
                case UriResolutionMethod.UseLinkResolver:
                    productUrl =
                        LinksUrlManager.GenerateLinksUrlViaLinkResolver(
                            westlawLinksSignOnContext.TestEnvironment,
                            westlawLinksSignOnContext.UrlPath,
                            westlawLinksSignOnContext.UrlParameters);
                    break;
                case UriResolutionMethod.UseTokenGeneration:
                    productUrl = LinksUrlManager.GenerateLinksUrlWithToken(
                        westlawLinksSignOnContext.TestEnvironment,
                        westlawLinksSignOnContext.UserInfo.UserName,
                        LinksUrlManager.GenerateLinksUrl(
                            westlawLinksUrl,
                            westlawLinksSignOnContext.UrlPath,
                            westlawLinksSignOnContext.UrlParameters));
                    break;
                default:
                    throw new NotSupportedException(
                        $"Cannot generate URL for {westlawLinksSignOnContext.UriResolutionMethod} method");
            }

            return productUrl;
        }

        /// <summary>
        /// The concat url parameters.
        /// </summary>
        /// <param name="parameters">
        /// The paramerets.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string ConcatUrlParams(Dictionary<string, string> parameters)
        {
            var resultUrl = new StringBuilder();

            foreach (KeyValuePair<string, string> param in parameters)
            {
                resultUrl.AppendFormat($"{param.Key}={param.Value}&");
            }

            return resultUrl.ToString().TrimEnd('&');
        }

        /// <summary>
        /// Generate url for WLN Links
        /// </summary>
        /// <param name="westlawLinksUrl"></param>
        /// <param name="urlPath"></param>
        /// <param name="urlParameters">url parameters</param>
        /// <returns></returns>
        private static string GenerateLinksUrl(
            string westlawLinksUrl,
            string urlPath,
            Dictionary<string, string> urlParameters)
        {
            return string.Concat(westlawLinksUrl, urlPath, LinksUrlManager.ConcatUrlParams(urlParameters));
        }

        /// <summary>
        /// The generate links url via link resolver.
        /// </summary>
        /// <param name="testEnvironment">
        /// The test Environment.
        /// </param>
        /// <param name="page">
        /// The page.
        /// </param>
        /// <param name="parameters">
        /// The parameters.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GenerateLinksUrlViaLinkResolver(
            EnvironmentInfo testEnvironment,
            string page,
            Dictionary<string, string> parameters)
        {
            string url =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.LinkResolver,
                    CobaltProductId.None,
                    testEnvironment.Id).Uri;
            url = string.Concat(url, page, "/default.wl?");

            return url + LinksUrlManager.ConcatUrlParams(parameters);
        }

        /// <summary>
        /// The generate links url with token.
        /// </summary>
        /// <param name="testEnvironment">
        /// The test Environment.
        /// </param>
        /// <param name="userName"></param>
        /// <param name="linksUrl">
        /// The links Url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GenerateLinksUrlWithToken(
            EnvironmentInfo testEnvironment,
            string userName,
            string linksUrl)
        {
            string url =
                TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.LinkResolver,
                    CobaltProductId.None,
                    testEnvironment.Id).Uri;

            return string.Concat(
                linksUrl,
                "&ResourceToken=",
                LinksUrlManager.GetResourceToken(string.Concat(url, $"sourceLink/{userName}?target=", linksUrl)));
        }

        /// <summary>
        /// The get resource token.
        /// </summary>
        /// <param name="tokenUrl">
        /// The token url.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        private static string GetResourceToken(string tokenUrl)
        {
            BrowserPool.CurrentBrowser.GoToUrl(tokenUrl);
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            return DriverExtensions.GetElement(By.XPath("/html/body")).Text;
        }
    }
}