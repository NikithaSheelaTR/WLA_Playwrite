namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Key Cite a Document Page
    /// </summary>
    public class KeyCiteADocumentPage : MobileBasePage
    {
        private static readonly By KeyCiteKeyButtonLocator = By.Id("coid_website_keyciteSearchButton");

        private static readonly By KeyciteTextBoxLocator = By.Id("q");
        
        /// <summary>
        /// Enter Text
        /// </summary>
        /// <param name="text"> Text </param>
        public void EnterText(string text) => DriverExtensions.SetTextField(text, KeyciteTextBoxLocator);

        /// <summary>
        /// Determine if the text box is displayed.
        /// </summary>
        /// <returns>True if displayed.</returns>
        public bool IsKeyCiteTextBoxDisplayed() => DriverExtensions.IsDisplayed(KeyciteTextBoxLocator);

        /// <summary>
        /// Press Key Cite Button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> new instance of the page </returns>
        public T ClickKeyCiteButton<T>() where T : MobileBasePage
        {
            DriverExtensions.WaitForElement(KeyCiteKeyButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determine if the search button is displayed.
        /// </summary>
        /// <returns>True if displayed.</returns>
        public bool IsKeyCiteButtonDisplayed() => DriverExtensions.IsDisplayed(KeyCiteKeyButtonLocator, 5);
    }
}