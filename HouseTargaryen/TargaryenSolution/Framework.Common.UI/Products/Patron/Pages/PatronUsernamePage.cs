namespace Framework.Common.UI.Products.Patron.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Page for restricted patron accounts to enter credentials
    /// </summary>
    public class PatronUsernamePage : BaseModuleRegressionPage
    {
        private static readonly By ContinueButtonLocator = By.Id("coid_button_continue");

        private static readonly By EmailTextboxLocator = By.Id("coid_promptEmail");

        private static readonly By FirstNameTextboxLocator = By.Id("coid_promptFirstName");

        private static readonly By LastNameTextboxLocator = By.Id("coid_promptLastName");

        private static readonly By SecretCodeTextboxLocator = By.Id("coid_promptSecretCode");

        private static readonly By UsernameTextboxLocator = By.Id("coid_promptUsername");

        private static readonly By SpinnerLocator = By.ClassName("co_search_ajaxLoading");

        /// <summary>
        /// Initializes a new instance of the <see cref="PatronUsernamePage"/> class. 
        /// </summary>
        public PatronUsernamePage()
        {
            DriverExtensions.WaitForElementNotDisplayed(SpinnerLocator);
        }

        /// <summary>
        /// Click on the continue button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickContinue<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(ContinueButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clears the email box and inputs the given text
        /// </summary>
        /// <param name="email"> The email. </param>
        public void EnterEmailAddress(string email) => DriverExtensions.SetTextField(email, EmailTextboxLocator);

        /// <summary>
        /// Clears the name box and inputs the given text
        /// </summary>
        /// <param name="firstName"> The first Name. </param>
        public void EnterFirstName(string firstName) => DriverExtensions.SetTextField(firstName, FirstNameTextboxLocator);

        /// <summary>
        /// Clears the last name box and inputs the given text
        /// </summary>
        /// <param name="lastName"> The last Name. </param>
        public void EnterLastName(string lastName) => DriverExtensions.SetTextField(lastName, LastNameTextboxLocator);

        /// <summary>
        /// Clears the secret code box and inputs the given text
        /// </summary>
        /// <param name="secretCode"> The secret Code. </param>
        public void EnterSecretCode(string secretCode) => DriverExtensions.SetTextField(secretCode, SecretCodeTextboxLocator);

        /// <summary>
        /// Clears the username box and inputs the given text
        /// </summary>
        /// <param name="userId"> The user Id. </param>
        public void EnterUsername(string userId) => DriverExtensions.SetTextField(userId, UsernameTextboxLocator);
    }
}