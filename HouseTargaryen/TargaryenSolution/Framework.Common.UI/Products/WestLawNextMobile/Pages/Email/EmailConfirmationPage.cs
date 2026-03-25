namespace Framework.Common.UI.Products.WestLawNextMobile.Pages.Email
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Email List Confirmation Page
    /// </summary>
    public class EmailConfirmationPage : MobileBasePage
    {
        private static readonly By BackButtonLocator = By.Id("emailConfirmBackToOrigin");

        /// <summary>
        /// Click on the Back button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickBackButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}