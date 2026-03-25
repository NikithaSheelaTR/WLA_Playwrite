namespace Framework.Common.UI.Products.WestLawAnalytics.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The base page.  Inherited by all other pages.
    /// </summary>
    public abstract class BasePage : BaseModuleRegressionPage
    {
        private static readonly By LogoLocator = By.XPath("//a[@id='coid_website_logo' and @title = 'Westlaw Analytics Home']");

        /// <summary>
        /// Header navigation links for every page object
        /// </summary>
        public virtual HeaderComponent AnalyticsHeader => new HeaderComponent();

        /// <summary>
        /// Checks to see if the West Law Analytics logoLocator is present.
        /// </summary>
        /// <returns>True if logoLocator is present.  False if not.</returns>
        public bool IsLogoDisplayed() => DriverExtensions.IsDisplayed(LogoLocator, 5);
    }
}