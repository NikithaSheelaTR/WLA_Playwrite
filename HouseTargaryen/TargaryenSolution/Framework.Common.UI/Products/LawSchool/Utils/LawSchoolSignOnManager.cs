namespace Framework.Common.UI.Products.LawSchool.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.LawSchool.Pages;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// LawSchool SignOn manager.
    /// </summary>
    public class LawSchoolSignOnManager : ISignOnManager
    {
        /// <summary>
        /// Signs off of the product.
        /// </summary>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>
        public ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () =>
                    new WestlawNextHeaderComponent()
                        .OpenProfileSettingsDialog().ClickSignOff()).LogDetails();
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext) where TPage : ICreatablePageObject
                                                                                 where TSignOnContext :
                                                                                 ISignOnContext<IUserInfo>
        {
            ICommonSignOnPage signOnPage;

            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var userInfo = signOnContext.UserInfo as WlnUserInfo;
            var lawSchoolSignOnContext = signOnContext as LawSchoolSignOnContext<IUserInfo>;

            if (lawSchoolSignOnContext == null)
            {
                throw new ArgumentException(
                    "The sign-on context is not a valid LawSchoolSignOnContext",
                    nameof(signOnContext));
            }

            if (lawSchoolSignOnContext.TestEnvironment.Id != EnvironmentId.Prod
                && lawSchoolSignOnContext.TestEnvironment.Id != EnvironmentId.Qed)
            {
                throw new ArgumentException(
                    "The Environment can be only Qed or Prod for LawSchool project",
                    nameof(signOnContext));
            }

            if (lawSchoolSignOnContext.ForceNavigate)
            {
                string lawSchoolUrl = lawSchoolSignOnContext.TestEnvironment.Id.GetUrlForLawSchool();

                BrowserPool.CurrentBrowser.GoToUrl(lawSchoolUrl);
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }
            else
            {
                signOnPage = new CommonSignOnPage();
            }

            return signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo?.UserName, userInfo?.Password);
        }

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            where TSignOnContext : ISignOnContext<IUserInfo>
        {
            return this.SignOn<LawSchoolHomePage, TSignOnContext>(signOnContext);
        }
    }
}