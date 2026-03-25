namespace Framework.Common.UI.Products.ANZ.Utils
{
    using System;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.ClientId;
    using Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// ANZ SignOn Manager
    /// </summary>
    public class AnzSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of ANZ.
        /// </summary>
        /// <returns> The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(() => new CommonSearchHomePage().Header.OpenProfileSettingsDialog().ClickSignOff());

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {

                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to ANZ using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICreatablePageObject landingPage;
            CommonSignOnPage signOnPage;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;

            if (userInfo == null)
            {
                throw new ArgumentException(
                    "The sign-on context does not contain valid UserInfo",
                    nameof(signOnContext));
            }

            if (signOnContext.ForceNavigate)
            {
                string anzUrl = signOnContext.TestEnvironment.Id.GetUrlForAnz();

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                          signOnContext.RoutingSettingsInfo,
                          signOnContext.TestEnvironment,
                          anzUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl($"{anzUrl}routing?SkipAnonymousAuthenticationKey=True&routingOptions=%5B%7B\"includeSignOnClick\"%3Atrue%7D%5D");
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
                var clientIdPage = new CommonClientIdPage();

                // if user has multiple regKeys, we should enter only UserName, Password an navigate to RegistrationKeyPage
                if (!string.IsNullOrEmpty(userInfo.CurrentRegKey) || clientIdPage.IsTextPresented("Select a registration key"))
                {
                    var regKeyPage = new RegistrationKeyPage();
                    regKeyPage.SelectRegKeyByName(!string.IsNullOrEmpty(userInfo.CurrentRegKey) ? userInfo.CurrentRegKey : "");
                    clientIdPage = regKeyPage.ClickContinue<CommonClientIdPage>();
                }
                this.CheckForEnvironmentError(clientIdPage);

                landingPage = clientIdPage.EnterClientIdAndClickContinue<TPage>(userInfo.ClientId);
            }
            else
            {
                landingPage = DriverExtensions.CreatePageInstance<TPage>();
            }
            if (signOnPage.IsAIPendoCloseButtonDisplayed())
            {
                signOnPage.CloseAIPendoGuide();
            }
            return landingPage == null ? default(TPage) : (TPage)landingPage;
        }

        /// <summary>
        /// Signs on to Westlaw Patron using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
        }
    }
}

