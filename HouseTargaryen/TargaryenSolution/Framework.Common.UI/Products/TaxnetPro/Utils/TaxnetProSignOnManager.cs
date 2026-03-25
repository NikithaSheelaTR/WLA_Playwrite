namespace Framework.Common.UI.Products.TaxnetPro.Utils
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Execution;

    using System;

    using Framework.Core.DataModel.Security;
    using Framework.Core.DataModel.Security.Specialized;

    /// <summary>
    /// Taxnet Pro sign on manager
    /// </summary>
    public class TaxnetProSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () =>
                    new WestlawNextHeaderComponent()
                        .OpenProfileSettingsDialog().ClickSignOff()).LogDetails();
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to TaxnetPro using the specified context.
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICommonSignOnPage signOnPage;

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
                string taxnetProUrl =
                    TestConfigurationRepository.DefaultInstance.FindEndpoint(
                        CobaltModuleId.Website,
                        CobaltProductId.TaxNetPro3,
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

            var clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonClientIdPage>(
                userInfo.UserName,
                userInfo.Password);

            SafeMethodExecutor.Execute(
                () => clientIdPage?.EnterClientIdAndClickContinue<CommonSearchHomePage>(
                    CredentialPool.GetFirstOrDefaultUser<IUserCredential>()?.ClientId,
                    true));
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Signs on to TaxnetPro using the specified context.
        /// </summary>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <param name="signOnContext"></param>
        /// <returns></returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext) => 
            this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
    }
}
