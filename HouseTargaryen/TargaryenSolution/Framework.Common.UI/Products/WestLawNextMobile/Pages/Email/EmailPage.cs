namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Email
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Email List Page
    /// </summary>
    public class EmailPage : MobileBasePage
    {
        private static readonly By SendButtonLocator = By.Id("coid_website_deliveryEmailButton");

        /// <summary>
        /// Click on the Send button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSendButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SendButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}