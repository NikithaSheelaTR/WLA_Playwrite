namespace Framework.Common.UI.Products.WestLawNextMobile.Utils
{
    using System;
    using System.Linq;
    using Framework.Common.Api.Endpoints.Website.DataModel.Products.Concourse;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.ClientId;
    using Framework.Common.UI.Products.WestLawNextMobile.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Sign-on manager for WestLaw Mobile
    /// </summary>
    public class WestlawMobileSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Westlaw Mobile.
        /// </summary>
        /// <returns> The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () => new MobileBasePage().SignOff<CommonSignOnPage>());

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {

                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return DriverExtensions.CreatePageInstance<CommonSignOnPage>();
        }

        /// <summary>
        /// Signs on to Westlaw Mobile using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICommonSignOnPage signOnPage;           
            CommonMobileClientIdPage clientIdPage = null;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;

            if (userInfo == null)
            {
                throw new ArgumentException(
                    "The sign-on context does not contain valid WlnUserInfo",
                    nameof(signOnContext));
            }

            if (signOnContext.ForceNavigate)
            {
                string westLawNextMobileUrl = signOnContext.TestEnvironment.Id.GetUrlForWestlawNextMobile();
                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        westLawNextMobileUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(westLawNextMobileUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }

            var enableCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.EnableCiam);
            var blockCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.BlockCiam);

            // Sign-on logic based on CIAM setting
            if (enableCiam.Value == RoutingSettingDropdownOption.True || blockCiam.Value == RoutingSettingDropdownOption.True)
            {
                // If PoolValue is "true", CIAM is enabled, use Email and Password for sign-on
                clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonMobileClientIdPage>(userInfo.Email, userInfo.Password);
            }
            else
            {
                // If PoolValue is not "true", CIAM is disabled, use Username and Password for sign-on
                clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonMobileClientIdPage>(userInfo.UserName,userInfo.Password);
            }

            // if user has multiple regKeys, we should enter only UserName, Password an navigate to RegistrationKeyPage
            if (!string.IsNullOrEmpty(userInfo.CurrentRegKey))
            {
                var regKeyPage = new RegistrationKeyPage();
                regKeyPage.SelectRegKeyByName(userInfo.CurrentRegKey);
                clientIdPage = regKeyPage.ClickContinue<CommonMobileClientIdPage>();
            }
            
            if (!string.IsNullOrEmpty(userInfo.MatterId))
            {
                clientIdPage.EnterMatterId(userInfo.MatterId);
            }

           return clientIdPage.EnterClientIdAndClickContinue<TPage>(userInfo.ClientId);
        }

        /// <summary>
        /// Signs on to Westlaw Mobile using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<MobileHomePage, TSignOnContext>(signOnContext);
        }
    }
}