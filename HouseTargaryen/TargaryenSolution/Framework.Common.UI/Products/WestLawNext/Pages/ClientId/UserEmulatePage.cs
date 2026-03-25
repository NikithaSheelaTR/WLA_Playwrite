namespace Framework.Common.UI.Products.WestLawNext.Pages.ClientId
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page for user emulation textbox on login
    /// </summary>
    public class UserEmulatePage : BaseModuleRegressionPage
    {
        private static readonly By CancelButtonLocator = By.Id("co_cancelSignon");

        private static readonly By ContinueButtonLocator = By.Id("co_enterEmulateeUserButton");

        private static readonly By UserEmulationUsernameTextboxLocator = By.Id("coid_website_userNameTextbox");
        

        /// <summary>
        /// Clicks the cancel button
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickCancelButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(CancelButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the continue button
        /// </summary>
        /// <typeparam name="T">type of object to return</typeparam>
        /// <returns>New instance of the page</returns>
        public T ClickContinueButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();     
            return DriverExtensions.CreatePageInstance<T>();            
        }

        /// <summary>
        /// Enters input text into the user to emulate box
        /// </summary>
        /// <param name="user">Username to emulate</param>
        public void EnterUserToEmulate(string user) => DriverExtensions.WaitForElement(UserEmulationUsernameTextboxLocator).SetTextField(user);     

        /// <summary>
        /// If emulation user button exists
        /// </summary>
        /// <returns>The <see cref="bool"/>.
        /// </returns>
        public bool IsUsernameTextboxDisplayed() => DriverExtensions.IsDisplayed(UserEmulationUsernameTextboxLocator, 5);
    }
}