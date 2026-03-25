namespace Framework.Common.UI.Products.Shared.DomainObjects.SignOn
{
    using System;

    using Framework.Common.UI.DataModel;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Core.DataModel;
    using Framework.Core.DataModel.Configuration.Proxies;
    using Framework.Core.DataModel.Security.Proxies;

    /// <summary>
    /// Sign on manager for products with routing page
    /// </summary>
    public abstract class SignOnManagerWithRouting : ISignOnManager
    {
        /// <summary>
        /// Signs off of the product.
        /// </summary>
        /// <returns>The page.</returns>
        public abstract ICreatablePageObject SignOff();

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TPage">The type of a page to return.</typeparam>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>The page to return.</returns>
        public abstract TPage SignOn<TPage, TSignOnContext>(TSignOnContext signOnContext)
            where TPage : ICreatablePageObject where TSignOnContext : ISignOnContext<IUserInfo>;

        /// <summary>
        /// Signs on to the product using the specified context.
        /// </summary>
        /// <param name="signOnContext">The sign-on context that includes account information and other details.</param>
        /// <typeparam name="TSignOnContext">The type of the sign-on context (<see cref="ISignOnContext{T}"/>).</typeparam>
        /// <returns>
        /// The <see cref="ICreatablePageObject"/>.
        /// </returns>
        public abstract ICreatablePageObject SignOn<TSignOnContext>(TSignOnContext signOnContext)
            where TSignOnContext : ISignOnContext<IUserInfo>;

        /// <summary>
        /// Navigates to a product sign on page after routing using the Test Setting
        /// </summary>
        /// <param name="routingPageSettings">The routing page settings.</param>
        /// <param name="environment">The environment.</param>
        /// <param name="productUrl">The product URL.</param>
        /// <typeparam name="T">The type of a sign-on page to return.</typeparam>
        /// <returns>The page to return. </returns>
        protected T RouteToProductSignOnPage<T>(
            RoutingSettingsInfo routingPageSettings,
            EnvironmentInfo environment,
            string productUrl) where T : class, ICreatablePageObject
        {
            var routingPage = new CommonRoutingPage(
                productUrl,
                routingPageSettings.RoutingUrlSettings);

            this.CheckForEnvironmentError(routingPage);

            CommonRoutingPage.SetRoutingOptions(
                routingPageSettings.RoutingDropdownSettings,
                CommonRoutingPage.SetRoutingOption);
            CommonRoutingPage.SetRoutingOptions(
                routingPageSettings.RoutingTextboxSettings,
                CommonRoutingPage.SetRoutingOption);

            if (environment.IsLower)
            {
                CommonRoutingPage.SetRoutingOptions(
                    routingPageSettings.SupportedFeatureSettings,
                    CommonRoutingPage.SetRoutingOption);
                CommonRoutingPage.SetRoutingOptions(
                    routingPageSettings.FeatureAccessControls,
                    CommonRoutingPage.SetRoutingOption);
            }

            return routingPage.ClickSaveButton<T>();
        }

        /// <summary>
        /// Checks if the environment works and throws exception if does not
        /// </summary>
        protected void CheckForEnvironmentError(IEnvironmentCheckPage checkPage)
        {
            if (checkPage.IsEnvironmentErrorsOnPage())
            {
                throw new Exception("The environment is down");
            }
        }
    }
}