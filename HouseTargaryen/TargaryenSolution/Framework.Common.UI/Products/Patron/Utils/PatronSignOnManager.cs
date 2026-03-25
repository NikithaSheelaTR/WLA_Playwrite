namespace Framework.Common.UI.Products.Patron.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Patron.Pages;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The sign-on manager for Westlaw Patron.
    /// </summary>
    public class PatronSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Westlaw Patron.
        /// </summary>
        /// <returns> The page.</returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(() => new CommonSearchHomePage().Header.OpenProfileSettingsDialog().ClickSignOff());
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to Westlaw Patron using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var patronSignOnContext = signOnContext as PatronSignOnContext<IUserInfo>;
            var userInfo = signOnContext.UserInfo as PatronUserInfo;

            if (patronSignOnContext == null)
            {
                throw new ArgumentException(
                    "The sign-on context is not a valid PatronSignOnContext",
                    nameof(signOnContext));
            }

            if (userInfo == null)
            {
                throw new ArgumentException(
                    "The sign-on context does not contain valid PatronUserInfo",
                    nameof(signOnContext));
            }

            if (signOnContext.ForceNavigate)
            {
                string patronUrl = signOnContext.TestEnvironment.Id.GetUrlForWestlawNext();

                if (signOnContext.ForceRouting)
                {
                    signOnContext.RoutingSettingsInfo.RoutingUrlSettings.Append("sp", userInfo.UserName, true);
                    this.RouteToProductSignOnPage<CommonSearchHomePage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        patronUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl($"{patronUrl}?sp={userInfo.UserName}");
                }
            }

            if (patronSignOnContext.AcceptAgreement)
            {
                var agreePage = new TermsAgreementPage();
                agreePage.SelectAgreeOption();
                agreePage.ClickContinue<CommonSearchHomePage>();
            }

            if (!string.IsNullOrEmpty(userInfo.PatronUserName))
            {
                var usernamePage = new PatronUsernamePage();

                usernamePage.EnterUsername(userInfo.PatronUserName);

                if (!string.IsNullOrEmpty(userInfo.FirstName))
                {
                    usernamePage.EnterFirstName(userInfo.FirstName);
                }

                if (!string.IsNullOrEmpty(userInfo.LastName))
                {
                    usernamePage.EnterLastName(userInfo.LastName);
                }

                if (!string.IsNullOrEmpty(userInfo.Email))
                {
                    usernamePage.EnterEmailAddress(userInfo.Email);
                }

                if (!string.IsNullOrEmpty(userInfo.SecretCode))
                {
                    usernamePage.EnterSecretCode(userInfo.SecretCode);
                }

                usernamePage.ClickContinue<CommonSearchHomePage>();
            }

            return string.IsNullOrEmpty(userInfo.ClientId)
                       ? DriverExtensions.CreatePageInstance<TPage>()
                       : DriverExtensions.CreatePageInstance<CommonClientIdPage>()
                                         .EnterClientIdAndClickContinue<TPage>(userInfo.ClientId);
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