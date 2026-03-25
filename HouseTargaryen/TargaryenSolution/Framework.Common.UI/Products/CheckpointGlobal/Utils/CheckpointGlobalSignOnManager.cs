namespace Framework.Common.UI.Products.CheckpointGlobal.Utils
{
    using System;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// Checkpoint Global Sign on Manager
    /// </summary>
    public class CheckpointGlobalSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Checkpoint Global.
        /// </summary>
        /// <returns> The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(() => new CommonSearchHomePage().Header.OpenProfileSettingsDialog().ClickSignOff());
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to Checkpoint global using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
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
                string cpGlobalUrl = signOnContext.TestEnvironment.Id.GetUrlForCheckpointGlobal();

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                          signOnContext.RoutingSettingsInfo,
                          signOnContext.TestEnvironment,
                          cpGlobalUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(cpGlobalUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }

            signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.UserName, userInfo.Password);

            return string.IsNullOrEmpty(userInfo.ClientId)
                       ? DriverExtensions.CreatePageInstance<TPage>()
                       : DriverExtensions.CreatePageInstance<CommonClientIdPage>()
                                         .EnterClientIdAndClickContinue<TPage>(userInfo.ClientId);
        }

        /// <summary>
        /// Signs on to Checkpoint global using the specified context.
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
