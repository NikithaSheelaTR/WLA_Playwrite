namespace Framework.Common.UI.Products.WestlawNextOpenWeb.Pages
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Open web home page
    /// </summary>
    public class OpenWebHomePage : CommonSearchHomePage
    {
        private const string ContentButtonLctMask = "//a[text()='View {0}']";
        private static readonly By CookiePolicyContainerLocator = By.Id("co_cookiePolicyContainer");
        private static readonly By CookiePolicyCloseButtonLocator = By.Id("coid_website_cookiePolicyAcknowledged");
        private static readonly By MoreInformationButtonLocator = By.XPath("//button[@id = 'coid_headerTrialButton' and text() = 'More information']");

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenWebHomePage" /> class.
        /// </summary>
        public OpenWebHomePage()
        {
            if (DriverExtensions.IsDisplayed(CookiePolicyContainerLocator))
            {
                DriverExtensions.WaitForElement(CookiePolicyCloseButtonLocator).Click();
            }
        }

        /// <summary>
        ///  More Information Button
        /// </summary>
        public IButton MoreInformationButton => new Button(MoreInformationButtonLocator);

        /// <summary>
        /// Access to content page
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="text"> Content text</param>
        /// <returns> New instance of the page </returns>
        public T GoToContentPage<T>(string text) where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ContentButtonLctMask, text))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}