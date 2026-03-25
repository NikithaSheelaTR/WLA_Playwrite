namespace Framework.Common.Api.Endpoints.OnePass
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Net;
    using System.Text.RegularExpressions;

    using Framework.Common.Api.Utilities;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    using HtmlAgilityPack;

    using RestSharp;

    /// <summary>
    /// Emulates WEB UI authorization
    /// </summary>
    internal sealed class OnePassWebClient : BaseCobaltServiceClient
    {
        public OnePassWebClient(
            EnvironmentInfo environment,
            CobaltProductId productId,
            CookieContainer cobaltCookies,
            NameValueCollection securityHeaders)
            : base(environment, CobaltModuleId.OnePassWeb, CobaltProductId.None, cobaltCookies, securityHeaders)
        {
            this.RestClient.UserAgent =
               "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.106 Safari/537.36";
        }

        /// <summary>
        /// Proceed browser validity verification
        /// </summary>
        /// <param name="afterRouting"> after Routing </param>
        /// <returns><see cref="IRestResponse"/> IRestResponse </returns>
        internal IRestResponse PassBrowserValidityVerification(IRestResponse afterRouting)
        {
            string unparseableHtml = afterRouting.Content;
            Regex filterTokenRegex = new Regex("(?<=href=\")(.*)(?=\")");
            string filteredToken = filterTokenRegex.Match(unparseableHtml).Value;
            filteredToken = filteredToken.Replace("bhjs=-1", string.Empty);

            IRestRequest request = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.GET,
                Resource = filteredToken,
                Parameters = new List<Parameter> { new Parameter { Name = "bhcp", Value = 1, Type = ParameterType.GetOrPost } }
            });
            request.AddCookie("bhResults", "bhfx=&bhfv=0&bhav=&bhsh=1080&bhsw=1920&bhiw=1064&bhih=567&bhtz=8&bhlu=en-us&bhon=-3&bhov=-3&bhsp=0");

            return this.RestClient.Execute(request);
        }

        /// <summary>
        /// Submits user credentials and track-tokens
        /// </summary>
        /// <param name="afterBrowserValidityPassing"></param>
        /// <param name="userInfo">WlnUserInfo</param>
        /// <returns><see cref="IRestResponse"/> IRestResponse </returns>
        internal IRestResponse SubmitOnePassAuthorizationForm(IRestResponse afterBrowserValidityPassing, OnePassUserInfo userInfo)
        {
            string authForm = afterBrowserValidityPassing.Content;
            var formData = new HtmlDocument();
            formData.LoadHtml(authForm);
            string endpointAddr = formData.DocumentNode.SelectSingleNode("//form[@action]").GetAttributeValue("action", string.Empty).Replace("&amp;", "&");
            string requestVerificationToken = formData.DocumentNode
                                                   .SelectSingleNode("//input[@name='__RequestVerificationToken']")
                                                   .GetAttributeValue("value", string.Empty);
            string rememberMeKey = formData.DocumentNode
                                        .SelectSingleNode("//input[@name='SuperRememberMeProductKey']")
                                        .GetAttributeValue("value", string.Empty);
            string siteKey = formData.DocumentNode
                                  .SelectSingleNode("//input[@name='SiteKey']")
                                  .GetAttributeValue("value", string.Empty);
            string isCdna = formData.DocumentNode
                                  .SelectSingleNode("//input[@name='IsCDNAvailable']")
                                  .GetAttributeValue("value", "False");
            
            var authFormDataDictionary = new List<Parameter>
            {
                new Parameter { Name = "IsCDNAvailable", Value = isCdna, Type = ParameterType.GetOrPost },
                new Parameter { Name = "SaveUsername", Value = "false", Type = ParameterType.GetOrPost },
                new Parameter { Name = "SaveUsernamePassword", Value = "false", Type = ParameterType.GetOrPost },
                new Parameter { Name = "SuperRememberMe", Value = "false", Type = ParameterType.GetOrPost },
                new Parameter { Name = "CultureCode", Value = "en", Type = ParameterType.GetOrPost },
                new Parameter { Name = "OverrideCaptchaFlags", Value = "False", Type = ParameterType.GetOrPost },
                new Parameter { Name = "recaptcha_response_field", Value = string.Empty, Type = ParameterType.GetOrPost },
                new Parameter { Name = "SignIn", Value = "submit", Type = ParameterType.GetOrPost },
                new Parameter { Name = "__RequestVerificationToken", Value = requestVerificationToken, Type = ParameterType.GetOrPost },
                new Parameter { Name = "MinutesToMidnight", Value = this.GetMinutesToMidnight().ToString(), Type = ParameterType.GetOrPost },
                new Parameter { Name = "Username", Value = userInfo.UserName, Type = ParameterType.GetOrPost },
                new Parameter { Name = "Password", Value = userInfo.Password, Type = ParameterType.GetOrPost },
                new Parameter { Name = "Password-clone", Value = userInfo.Password, Type = ParameterType.GetOrPost },
                new Parameter { Name = "SuperRememberMeProductKey", Value = rememberMeKey, Type = ParameterType.GetOrPost },
                new Parameter { Name = "SiteKey", Value = siteKey, Type = ParameterType.GetOrPost }
            };

            var headers = new Dictionary<string, string>
                                             {
                                                 { "Upgrade-Insecure-Requests", "1" },
                                                 { "Origin", this.BaseUrl }
                                             };

            if (this.CheckUserName(userInfo.UserName, requestVerificationToken).StatusCode != HttpStatusCode.OK)
            {
                throw new Exception($"UserName is not valid for {this.Environment.Name}");
            }

            IRestRequest finalAuthRequest = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = endpointAddr,
                Headers = headers,
                Parameters = authFormDataDictionary
            });

            return this.RestClient.Execute(finalAuthRequest);
        }

        /// <summary>
        /// Performs checking username and updates Cookie 
        /// </summary>
        /// <param name="userName">UserName to check</param>
        /// <param name="requestVerificationToken">Required token</param>
        /// <returns> IRestResponse </returns>
        private IRestResponse CheckUserName(string userName, string requestVerificationToken)
        {
            const string Url = "/v2/captchasrm/check/username/";

            var parameters = new List<Parameter>
            {
                new Parameter { Name = "__RequestVerificationToken", Value = requestVerificationToken, Type = ParameterType.GetOrPost },
                new Parameter { Name = "captchaIsAlreadyDisplayed", Value = "false", Type = ParameterType.GetOrPost },
                new Parameter { Name = "overrideCaptchaFlags", Value = "false", Type = ParameterType.GetOrPost },
                new Parameter { Name = "username", Value = userName, Type = ParameterType.GetOrPost }
            };
            
            IRestRequest checkUserNameRequest = this.RequestBuilder.BuildRequest(new RequestArguments
            {
                Method = Method.POST,
                Resource = Url,
                Parameters = parameters
            });
            
            return this.RestClient.Execute(checkUserNameRequest);
        }

        /// <summary>
        /// Returns Minutes to midnight
        /// </summary>
        /// <returns><see cref="int"/>></returns>
        private int GetMinutesToMidnight() => (int)((DateTime.Today.AddDays(1) - DateTime.Now).TotalMinutes);
    }
}