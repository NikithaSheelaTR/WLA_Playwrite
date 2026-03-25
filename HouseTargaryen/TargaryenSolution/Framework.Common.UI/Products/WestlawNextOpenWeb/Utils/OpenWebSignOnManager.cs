namespace Framework.Common.UI.Products.WestlawNextOpenWeb.Utils
{
    using System;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;

    /// <summary>
    /// OpenWebSignOnManager
    /// </summary>
    public class OpenWebSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Sign Off
        /// </summary>
        /// <returns> New instance of the object </returns>
        public override ICreatablePageObject SignOff()
        {
            // There is no way to sign off
            return new CommonSearchHomePage();
        }

        /// <summary>
        /// SignOn
        /// </summary>
        /// <typeparam name="TPage"> Page type </typeparam>
        /// <typeparam name="TSignOnContext"> SignOnContext </typeparam>
        /// <param name="signOnContext"> signOnContext </param>
        /// <returns> New instance of the page </returns>
        public override TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
        {
            if (signOnContext == null)
            {
                throw new ArgumentNullException("Sign on context was not set");
            }

            var context = signOnContext as OpenWebSignOnContext<IUserInfo>;
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (signOnContext.ForceNavigate)
            {
                string openWebUrl = signOnContext.TestEnvironment.Id.GetUrlForWlnOpenWeb();
                if (signOnContext.ForceRouting)
                {
                    this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        openWebUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(openWebUrl);
                }
            }

            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// SignOn
        /// </summary>
        /// <typeparam name="TSignOnContext"> SignOnContext </typeparam>
        /// <param name="signOnContext"> signOnContext </param>
        /// <returns> New instance of the page </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<CommonSearchHomePage, TSignOnContext>(signOnContext);
        }
    }
}
