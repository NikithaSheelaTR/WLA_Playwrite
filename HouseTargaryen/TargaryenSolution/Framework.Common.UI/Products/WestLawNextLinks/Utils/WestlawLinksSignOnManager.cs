namespace Framework.Common.UI.Products.WestLawNextLinks.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Error;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult;
    using Framework.Common.UI.Products.WestLawNextLinks.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Configuration;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils;
    /// <summary>
    /// The westlaw next links sign on manager.
    /// </summary>
    public class WestlawLinksSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Westlaw Links.
        /// </summary>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOff()
        {
            // there is no way to sign off
            return new BaseWestlawLinksPage();
        }

        /// <summary>
        /// Signs on to Westlaw Links using the specified context.
        /// </summary>
        /// <param name="signOnContext"> The sign-on context that includes account information and other details. </param>
        /// <typeparam name="TPage"> The type of a page to return. </typeparam>
        /// <typeparam name="TSignOnContext"> The type of the sign-on context (<see cref="ISignOnContext{T}"/>). </typeparam>
        /// <returns> The page to return. </returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            if (signOnContext == null)
            {
                throw new ArgumentNullException("Sign on context was not set");
            }

            var context = signOnContext as WestlawLinksSignOnContext<IUserInfo>;

            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (signOnContext.ForceNavigate)
            {
                string wlnLinksUrl =
                    TestConfigurationRepository.DefaultInstance.FindEndpoint(
                        CobaltModuleId.Website,
                        CobaltProductId.WlnLinks,
                        signOnContext.TestEnvironment.Id).Uri;

                string productUrl = LinksUrlManager.GenerateWestlawLinksUrl(context, wlnLinksUrl);

                if (signOnContext.ForceRouting)
                {
                    // No matter what page will be created
                    this.RouteToProductSignOnPage<PageNotFoundErrorPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        wlnLinksUrl);
                }

                BrowserPool.CurrentBrowser.GoToUrl(productUrl);
                Logger.LogInfo("User was navigated to " + productUrl);


            }

            if (context.AgreementPage)
            {
                var agreementPage = DriverExtensions.CreatePageInstance<TermsAgreementPage>();
                agreementPage.SelectAgreeOption();
                agreementPage.ClickContinue<TPage>();
            }

            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Signs on to Westlaw Links using the specified context.
        /// </summary>
        /// <param name="signOnContext">  The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext"> The type of the sign-on context (<see cref="ISignOnContext{T}"/>). </typeparam>
        /// <returns>
        /// The <see cref="Interfaces.ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CategorySearchResultPage, TSignOnContext>(signOnContext);
        }
    }
}