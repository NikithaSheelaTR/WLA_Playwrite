namespace Framework.Common.UI.Products.GovernmentWeblinks
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// Government Sites Sign-on manager
    /// </summary>
    public class GovernmentWeblinksSignOnManager : ISignOnManager
    {
        private GovSignOnContext<IUserInfo> govtSignOnContext;

        /// <summary>
        /// Signs off of Westlaw Patron.
        /// </summary>
        /// <returns> The page.</returns>
        public ICreatablePageObject SignOff()
        {
            throw new NotImplementedException(
                "There is no Sign off functionality for Government Weblinks product");
        }

        /// <summary>
        /// Signs on to Westlaw Government Sites using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext) where TPage : ICreatablePageObject
                                                                                 where TSignOnContext :
                                                                                 ISignOnContext<IUserInfo>
        {
            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            this.govtSignOnContext = signOnContext as GovSignOnContext<IUserInfo>;

            if (this.govtSignOnContext == null)
            {
                throw new ArgumentException(
                    "The sign-on context is not a valid GovtSignOnContext",
                    nameof(signOnContext));
            }

            string govtUrl = 
                this.govtSignOnContext.TestEnvironment.Id.GetUrlForGovernmentSites(this.govtSignOnContext.SiteName);
            BrowserPool.CurrentBrowser.GoToUrl(govtUrl);

            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Signs on to Westlaw Patron using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            where TSignOnContext : ISignOnContext<IUserInfo>
        {
            return this.SignOn<ICreatablePageObject, TSignOnContext>(signOnContext);
        }
    }
}