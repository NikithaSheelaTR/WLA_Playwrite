namespace Framework.Common.UI.Products.WestLawAnalytics.Components
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.WestLawAnalytics.Enums;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Core.Utils.Enums;

    using OpenQA.Selenium;

    /// <summary>
    /// Header navigation links
    /// </summary>
    public class HeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By SignOffLinkLocator = By.XPath("//li[@id='co_signOffContainer']/a");

        private static readonly By UserNameLoggedLocator = By.Id("wa_golbalNav_name");

        private static readonly By ContainerLocator = By.Id("co_header");

        private EnumPropertyMapper<WestlawAnalyticsHeaderButtons, WebElementInfo> headerButtonsMap;

        /// <summary>
        /// Header Buttons Map
        /// </summary>
        protected EnumPropertyMapper<WestlawAnalyticsHeaderButtons, WebElementInfo> HeaderButtonsMap
            => this.headerButtonsMap =
                    this.headerButtonsMap ?? EnumPropertyModelCache.GetMap<WestlawAnalyticsHeaderButtons, WebElementInfo>();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click the button on the header
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="button"> Tab to click </param>
        /// <returns> New instance of the page </returns>
        public T ClickAnalyticsHeaderButton<T>(WestlawAnalyticsHeaderButtons button) where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(By.XPath(this.HeaderButtonsMap[button].LocatorString)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the sign off link and returns a new Sign Off page.
        /// </summary>
        /// <returns>A new sign off page.</returns>
        public CommonSignOffPage ClickSignOff()
        {
            DriverExtensions.WaitForElementDisplayed(SignOffLinkLocator).Click();
            return new CommonSignOffPage();
        }

        /// <summary>
        /// Gets logged in user
        /// </summary>
        /// <returns>User name</returns>
        public string GetUserNameLoggedIn() => DriverExtensions.GetText(UserNameLoggedLocator);
    }
}