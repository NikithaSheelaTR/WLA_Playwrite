namespace Framework.Common.UI.Products.WestLawNext.Utils
{
    using System;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Enums;
    using Framework.Common.UI.Products.Shared.Enums.Core;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.ClientId;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;    

    /// <summary>
    /// The sign-on manager for Westlaw.
    /// </summary>
    public class WestlawSignOnManager : SignOnManagerWithRouting
    {
          /// <summary>
        /// Signs off of Westlaw.
        /// </summary>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>        
        public override ICreatablePageObject SignOff()
        {          
            SafeMethodExecutor.Execute(
                () =>
                    new WestlawNextHeaderComponent()
                        .OpenProfileSettingsDialog().ClickSignOff()).LogDetails();

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {

                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to Westlaw using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
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
                string westLawNextUrl = signOnContext.TestEnvironment.Id.GetUrlForWestlawNext();

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        westLawNextUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(westLawNextUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }

            this.CheckForEnvironmentError(signOnPage);

            var enableCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.EnableCiam);

            var blockCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.BlockCiam);

            // Sign-on logic based on CIAM setting
            if (enableCiam.Value == RoutingSettingDropdownOption.True || blockCiam.Value == RoutingSettingDropdownOption.True)
            {
                // If PoolValue is "true", CIAM is enabled, use Email and Password for sign-on
                landingPage = signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.Email, userInfo.Password);
            }
            else
            {
                // If PoolValue is not "true", CIAM is disabled, use Username and Password for sign-on
                landingPage = signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.UserName, userInfo.Password);
            }

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
                    WestlawSignOnManager.ProcessKmAuthorisation<TPage>(
                        signOnContext as KmSignOnContext<IUserInfo>,
                        userInfo,
                        clientIdPage)
                    ?? clientIdPage.EnterClientIdAndClickContinue<TPage>(
                        userInfo.ClientId,
                        userInfo.RetryClientIdSelectionOnFailure);
            }

            if ((!signOnContext.TestEnvironment.IsLower || signOnContext.TestEnvironment.IsLower || signOnContext.CloseWelcomeDialog) && ((BaseModuleRegressionPage)landingPage).IsDisplayed(Dialogs.Welcome))
            {
                landingPage = new WelcomeDialog().CloseButton.Click<TPage>();
            }

            return landingPage == null ? default(TPage) : (TPage)landingPage;
        }

        /// <summary>   
        /// Signs on to Westlaw using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            => this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);

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