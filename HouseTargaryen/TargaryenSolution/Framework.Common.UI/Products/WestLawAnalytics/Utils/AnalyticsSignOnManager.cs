namespace Framework.Common.UI.Products.WestLawAnalytics.Utils
{
    using System;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.DomainObjects.SignOn;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Pages;
    using Framework.Common.UI.Products.WestLawNext.Pages.IphoneMode;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.CommonTypes.Enums;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;
    using Framework.Core.Utils.Execution;

    /// <summary>
    /// The sign on manager for Westlaw Analytics.
    /// </summary>
    public class AnalyticsSignOnManager : SignOnManagerWithRouting
    {
        /// <summary>
        /// Signs off of Westlaw Analytics.
        /// </summary>
        /// <returns> The page. </returns>
        public override ICreatablePageObject SignOff()
        {
            SafeMethodExecutor.Execute(
                () => new AnalyticsPage().AnalyticsHeader.ClickSignOff());

            if (new CommonSignOffPage().ForcesignoffLink.Displayed)
            {

                new CommonSignOffPage().ForcesignoffLink.Click();
            }

            return new CommonSignOffPage();
        }

        /// <summary>
        /// Signs on to Westlaw Analytics using the specified context.
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
                    "The sign-on context does not contain valid OnePassUserInfo",
                    nameof(signOnContext));
            }

            if (signOnContext.ForceNavigate)
            {
                string analyticsUrl = signOnContext.TestEnvironment.Id.GetUrlForWestlawAnalytics();
                if (signOnContext.ForceRouting)
                {
                    signOnPage = this.RouteToProductSignOnPage<CommonSignOnPage>(
                        signOnContext.RoutingSettingsInfo,
                        signOnContext.TestEnvironment,
                        analyticsUrl);
                }
                else
                {
                    BrowserPool.CurrentBrowser.GoToUrl(analyticsUrl);
                    signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
                }
            }
            else
            {
                signOnPage = DriverExtensions.CreatePageInstance<CommonSignOnPage>();
            }

            this.CheckForEnvironmentError(signOnPage);

            var enableCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.EnableCiam);

            var blockCiam = signOnContext.RoutingSettingsInfo.RoutingDropdownSettings.FirstOrDefault(s => s.Key == RoutingSettingDropdown.BlockCiam);

            // Sign-on logic based on CIAM setting
            if (enableCiam.Value == RoutingSettingDropdownOption.True || blockCiam.Value == RoutingSettingDropdownOption.True)
            {
                // If PoolValue is "true", CIAM is enabled, use Email and Password for sign-on
                return signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.Email, userInfo.Password);
            }
            else
            {
                // If PoolValue is not "true", CIAM is disabled, use Username and Password for sign-on
                return signOnPage.EnterUserIdPasswordAndClickSignOn<TPage>(userInfo.UserName, userInfo.Password);
            }
        }

        /// <summary>
        /// Signs on to Westlaw Analytics using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public override ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
        {
            return this.SignOn<AnalyticsPage, TSignOnContext>(signOnContext);
        }
    }
}