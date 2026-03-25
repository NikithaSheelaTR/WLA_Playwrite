namespace Framework.Common.UI.Products.Concourse.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Concourse.Pages;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The sign-on manager for Concourse product.
    /// </summary>
    public class OrionSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Concourse.
        /// </summary>
        /// <returns>The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(() => new ConcourseHomePage().ClickSignOff());
            return DriverExtensions.CreatePageInstance<ConcourseSignOffPage>();
        }

        /// <summary>
        /// Signs on to Concourse using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<ConcourseHomePage, TSignOnContext>(signOnContext);
        }

        /// <summary>
        /// Signs on to Concourse using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            ConcourseSignOnPage signOnPage;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            if (signOnContext.ForceNavigate)
            {
                string concoureUrl = signOnContext.TestEnvironment.Id.GetUrlForConcourse();

                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<ConcourseSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        concoureUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(concoureUrl);
                    signOnPage = new ConcourseSignOnPage();
                }
            }
            else
            {
                signOnPage = new ConcourseSignOnPage();
            }

            var userInfo = signOnContext.UserInfo as IOnePassUserInfo;

            return signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.UserName, userInfo.Password);
        }
    }
}