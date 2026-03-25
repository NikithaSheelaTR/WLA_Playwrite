namespace Framework.Common.UI.Products.CaseNotebook.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.CaseNotebook.Pages;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// Case notebook sign on manager
    /// </summary>
    public class CaseNotebookSignOnManager : ISignOnManager
    {
        /// <summary>
        /// Signs off of the product.
        /// </summary>
        /// <returns>The <see cref="ICreatablePageObject"/>.</returns>
        public ICreatablePageObject SignOff()
        {
            throw new NotImplementedException(
                "There is no Sign off functionality for Case Notebook product");
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
            if (signOnContext == null)
            {
                throw new ArgumentNullException(nameof(signOnContext), "The sign-on context was not set");
            }

            var context = signOnContext as CaseNotebookSignOnContext<IUserInfo>;

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            string caseNotebookUrl = context.StartFromWestlawPage
                                 ? context.TestEnvironment.Id.GetUrlForWestlawNext()
                                 : context.TestEnvironment.Id.GetUrlForCaseNotebook();

            BrowserPool.CurrentBrowser.GoToUrl($"{caseNotebookUrl}{context.UrlPath}");
            return DriverExtensions.CreatePageInstance<TPage>();
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
            return this.SignOn<CaseNotebookPage, TSignOnContext>(signOnContext);
        }
    }
}