namespace Framework.Common.UI.Products.WestlawNextCorrectional.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Enums.Content;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestlawNextCorrectional.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The sign-on manager for Correctional.
    /// </summary>
    public class WestlawNextCorrectionalManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Correctional
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () =>
                    new WestlawNextHeaderComponent()
                        .OpenProfileSettingsDialog().ClickSignOff()).LogDetails();
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to Correctional using the specified context.
        /// </summary>
        /// <param name="signOnContext"></param>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            if (signOnContext == null)
            {
                throw new ArgumentNullException("Sign on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;
            var context = signOnContext as WestlawNextCorrectionalSignOnContext<IUserInfo>;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (signOnContext.ForceNavigate)
            {
                string correctionalWebUrl = signOnContext.TestEnvironment.Id.GetUrlForCorrectional();
                if (signOnContext.ForceRouting)
                {
                    this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        correctionalWebUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(correctionalWebUrl);
                }
               
            }
            var signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            var termsAgreementPage =  signOnPage.EnterUserIdPasswordAndClickSignOn<TermsAgreementPage>(
                userInfo.UserName,
                userInfo.Password);
            termsAgreementPage.SelectAgreeOption();
            return termsAgreementPage.ClickContinue<SelectJurisdictionPage>()
                                     .ClickLinkByText<TPage>(Jurisdiction.National.ToString());
        }

        /// <summary>
        /// Signs on to Correctional using the specified context.
        /// </summary>
        /// <param name="signOnContext"></param>
        /// <typeparam name="TSignOnContext"></typeparam>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
        }
    }
}
