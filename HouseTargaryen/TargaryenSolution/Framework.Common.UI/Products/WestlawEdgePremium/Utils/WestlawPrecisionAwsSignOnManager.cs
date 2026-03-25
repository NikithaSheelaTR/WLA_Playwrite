namespace Framework.Common.UI.Products.WestlawEdgePremium.Utils
{

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Enums.Core;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.ClientId;
    using Framework.Common.UI.Products.WestLawNext.Utils;
    using Framework.Common.UI.Raw.WestlawEdge.Utils;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;
    using System;
    using Framework.Common.UI.Products.Shared.Enums;

    /// <summary>
    /// Westlaw Precision Aws sign on manager
    /// </summary>
    public class WestlawPrecisionAwsSignOnManager : EdgeSignOnManager
    {
        /// <summary>
        /// Signs on to Westlaw Precision AWS using the specified context.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICreatablePageObject landingPage = null;
            ICommonSignOnPage signOnPage;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;

            if (userInfo == null)
            {
                throw new ArgumentException("The sign-on context does not contain valid WlnUserInfo", nameof(signOnContext));
            }

            if (signOnContext.ForceNavigate)
            {
                string productUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                        CobaltModuleId.Website,
                        CobaltProductId.WestlawPrecisionAws,
                        signOnContext.TestEnvironment.Id).Uri;

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        productUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(productUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }

            this.CheckForEnvironmentError(signOnPage);

            landingPage = signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.UserName, userInfo.Password);

            if (!string.IsNullOrEmpty(userInfo.ClientId))
            {
                CommonClientIdPage clientIdPage = new CommonClientIdPage();

                // if user has multiple regKeys, we should enter only UserName, Password an navigate to RegistrationKeyPage
                if (!string.IsNullOrEmpty(userInfo.CurrentRegKey))
                {
                    var regKeyPage = new RegistrationKeyPage();
                    regKeyPage.SelectRegKeyByName(userInfo.CurrentRegKey);
                    clientIdPage = regKeyPage.ClickContinue<CommonClientIdPage>();
                }

                this.CheckForEnvironmentError(clientIdPage);

                if (!string.IsNullOrEmpty(userInfo.MatterId))
                {
                    clientIdPage.WestHostedClientIdComponent.EnterMatterId(userInfo.MatterId);
                }

                landingPage =
                    ProcessKmAuthorisation<TPage>(
                        signOnContext as KmSignOnContext<IUserInfo>,
                        userInfo,
                        clientIdPage)
                    ?? clientIdPage.EnterClientIdAndClickContinue<TPage>(
                        userInfo.ClientId,
                        userInfo.RetryClientIdSelectionOnFailure);
            }

            if ((!signOnContext.TestEnvironment.IsLower || signOnContext.CloseWelcomeDialog) && ((BaseModuleRegressionPage)landingPage).IsDisplayed(Dialogs.Welcome))
            {
                landingPage = new WelcomeDialog().CloseButton.Click<TPage>();
            }

            return landingPage == null ? default(TPage) : (TPage)landingPage;
        }


        /// <summary>
        /// Signs on to Westlaw Precision AWS using the specified context.
        /// </summary>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
        }

        private static ICreatablePageObject ProcessKmAuthorisation<TPage>(
            KmSignOnContext<IUserInfo> signOnContext,
            WlnUserInfo userInfo,
            CommonClientIdPage currenPage) where TPage : ICreatablePageObject
        {
            ICreatablePageObject result = null;

            if (signOnContext != null && signOnContext.KmAuthMode == KmAuthenticationMode.Km)
            {
                var signOnPage = currenPage.EnterClientIdAndClickContinue<KmSignOnPage>(
                    userInfo.ClientId,
                    userInfo.RetryClientIdSelectionOnFailure);
                var westKmUserInfo = signOnContext.KmUserInfo as WlnUserInfo;

                if (westKmUserInfo == null)
                {
                    throw new ArgumentException(
                        "The sign-on context does not contain valid WlnUserInfo",
                        nameof(signOnContext));
                }

                result = signOnPage.EnterUsernamePasswordAndClickSignOn<TPage>(
                    westKmUserInfo.UserName,
                    westKmUserInfo.Password);
            }

            return result;
        }
    }
}
