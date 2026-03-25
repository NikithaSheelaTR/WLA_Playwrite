namespace Framework.Common.UI.Products.TaxnetPro.Utils
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.DataModel.Security.Specialized;
    using Framework.Core.Utils.Execution;
    using System;
    using System.Linq;

    /// <summary>
    /// Taxnet Pro AWS sign on manager
    /// </summary>
    public class TaxnetProAwsSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () => new WestlawNextHeaderComponent().OpenProfileSettingsDialog().ClickSignOff()).LogDetails();

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {

                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to TaxnetPro AWS using the specified context.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICommonSignOnPage signOnPage;

            CommonClientIdPage clientIdPage;

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
                string taxnetProUrl = TestConfigurationRepository.DefaultInstance.FindEndpoint(
                    CobaltModuleId.Website,
                    CobaltProductId.TaxnetPro3Aws,
                    signOnContext.TestEnvironment.Id).Uri;

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        taxnetProUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(taxnetProUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = new CommonSignOnPage();
            }
            var enableCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.EnableCiam);

            var blockCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.BlockCiam);

            // Sign-on logic based on CIAM setting
            if (enableCiam.Value == RoutingSettingDropdownOption.True || blockCiam.Value == RoutingSettingDropdownOption.True)
            {
                // If PoolValue is "true", CIAM is enabled, use Email and Password for sign-on
                clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonClientIdPage>(userInfo.Email, userInfo.Password);

            }
            else
            {
                // If PoolValue is not "true", CIAM is disabled, use Username and Password for sign-on
               clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonClientIdPage>(
                 userInfo.UserName,
                 userInfo.Password);
            }

            Console.WriteLine($"Navigating to URL : {BrowserPool.CurrentBrowser.Url}");

            SafeMethodExecutor.Execute(
              () => clientIdPage?.EnterClientIdAndClickContinue<CommonSearchHomePage>(
                  CredentialPool.GetFirstOrDefaultUser<IUserCredential>()?.ClientId,
                  true));
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Signs on to Taxnet Pro AWS using the specified context.
        /// </summary>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
        }
    }
}