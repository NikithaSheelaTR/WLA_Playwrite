namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// KM Sign On page
    /// </summary>
    public class KmSignOnPage : BaseModuleRegressionPage
    {
        private static readonly By PasswordTextBoxLocator = By.Id("coid_website_passwordTextbox");

        private static readonly By RememberPasswordCheckboxLocator = By.Id("coid_website_rememberUsernamePasswordCheckbox");

        private static readonly By RememberUsernameCheckboxLocator = By.Id("coid_website_rememberMeCheckbox");

        private static readonly By SignOnButtonLocator = By.Id("coid_website_KM_signOnButton");

        private static readonly By SkipAuthenticationLinkLocator = By.Id("coid_website_skipAuthentication");

        private static readonly By UsernameTextBoxLocator = By.Id("coid_website_userNameTextbox");

        /// <summary>
        /// Enters the user id and password in their textboxes and clicks the sign on button
        /// </summary>
        /// <typeparam name="T"> page </typeparam>
        /// <param name="username"> user id to enter </param>
        /// <param name="password"> password to enter </param>
        /// <returns> The next page </returns>
        public T EnterUsernamePasswordAndClickSignOn<T>(string username, string password) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(UsernameTextBoxLocator).SetTextField(username);
            DriverExtensions.WaitForElement(PasswordTextBoxLocator).SetTextField(password);
            DriverExtensions.Click(SignOnButtonLocator);
            DriverExtensions.WaitForElementNotDisplayed(UsernameTextBoxLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determines if the Remember Password checkbox is selected
        /// </summary>
        /// <returns>true if checkbox is selected, false otherwise</returns>
        public bool IsRememberPasswordCheckboxChecked() => DriverExtensions.IsCheckboxSelected(RememberPasswordCheckboxLocator);

        /// <summary>
        /// Determines if the Remember Username checkbox is selected
        /// </summary>
        /// <returns>true if checkbox is selected, false otherwise</returns>
        public bool IsRememberUsernameCheckboxChecked() => DriverExtensions.IsCheckboxSelected(RememberUsernameCheckboxLocator);

        /// <summary>
        /// Set the Remember Password checkbox
        /// </summary>
        /// <param name="remember"> true if set check box selected </param>
        public void SetRememberPasswordCheckbox(bool remember)
            => DriverExtensions.WaitForElement(RememberPasswordCheckboxLocator).SetCheckbox(remember);

        /// <summary>
        /// Set the Remember Username checkbox
        /// </summary>
        /// <param name="remember"> true if set check box selected </param>
        public void SetRememberUsernameCheckbox(bool remember)
            => DriverExtensions.WaitForElement(RememberUsernameCheckboxLocator).SetCheckbox(remember);

        /// <summary>
        /// Clicks the 'Skip KM Authentication' link
        /// </summary>
        /// <typeparam name="T"> page instance to return </typeparam>
        /// <returns> The next page </returns>
        public T SkipAuthentication<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.Click(SkipAuthenticationLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}