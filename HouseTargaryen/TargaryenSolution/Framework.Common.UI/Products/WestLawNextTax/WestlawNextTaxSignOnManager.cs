namespace Framework.Common.UI.Products.WestLawNextTax
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The sign-on manager for WLN Tax.
    /// </summary>
    public class WestlawNextTaxSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of WLN Tax.
        /// </summary>
        /// <returns>The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () =>
                    new WestlawNextHeaderComponent()
                        .OpenProfileSettingsDialog().ClickSignOff()).LogDetails();
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to WLN Tax using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ICommonSignOnPage signOnPage;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;

            if (signOnContext.ForceNavigate)
            {
                string westLawNextTaxUrl = signOnContext.TestEnvironment.Id.GetUrlForWestlawNextTax();

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        westLawNextTaxUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(westLawNextTaxUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = new CommonSignOnPage();
            }

            if (string.IsNullOrEmpty(userInfo?.ClientId))
            {
                return signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo?.UserName, userInfo?.Password);
            }

            var clientIdPage = signOnPage.EnterUserIdPasswordAndClickSignOn<CommonClientIdPage>(
                userInfo.UserName,
                userInfo.Password);

            return clientIdPage.EnterClientIdAndClickContinue<TPage>(
                userInfo.ClientId,
                userInfo.RetryClientIdSelectionOnFailure);
        }

        /// <summary>
        /// Signs on to WLN Tax using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            => this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
    }
}