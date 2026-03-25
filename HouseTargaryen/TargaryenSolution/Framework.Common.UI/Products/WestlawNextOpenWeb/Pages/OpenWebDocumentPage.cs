namespace Framework.Common.UI.Products.WestlawNextOpenWeb.Pages
{
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Open web document page
    /// </summary>
    public class OpenWebDocumentPage : CommonDocumentPage
    {
        private static readonly By SignInLinkLocator = By.LinkText("Sign in");

        private static readonly By BlueBoxTextLocator = By.XPath("//*[@class='co_genericBoxContent']//p");

        /// <summary>
        /// sign on
        /// </summary>
        /// <typeparam name="T"> Page type</typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSignInLink<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(SignInLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get text from Blue box 
        /// </summary>
        /// <returns> Text from blue box </returns>
        public string GetBlueBoxText() => DriverExtensions.WaitForElement(BlueBoxTextLocator).Text;
    }
}
