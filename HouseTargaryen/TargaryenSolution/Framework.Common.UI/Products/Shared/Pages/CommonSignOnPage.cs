namespace Framework.Common.UI.Products.Shared.Pages
{
    using Framework.Common.Api.Endpoints.Uds.DataModel;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.DataModel.Configuration.Constants;
    using Framework.Core.DataModel.Security.Proxies;
    using Framework.Core.Utils.Configuration;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext SignOn page
    /// </summary>
    public class CommonSignOnPage : BaseModuleRegressionPage, ICommonSignOnPage
    {
        private static readonly By PasswordTextboxLocator = By.CssSelector("#Password,#coid_website_passwordTextbox");

        private static readonly By SignOnButtonLocator = By.XPath("//button[@type='submit']");

        private static readonly By UsernameTextboxLocator = By.CssSelector("#Username,#coid_website_userNameTextbox");

        private static readonly By AccessibilityLinkLocator = By.LinkText("Accessibility");

        private static readonly By InformationalMessageLocator = By.XPath("//div[@class='Informational']/span");

        private static readonly By PasswordErrorMessageLocator = By.XPath("//span[@id='Password_validationMessage']");

        private static readonly By SaveUsernameAndPasswordCheckboxLocator =
            By.CssSelector("#SaveUsernamePassword,#coid_website_rememberUsernamePasswordCheckbox");

        private static readonly By SaveUsernameCheckboxLocator =
            By.CssSelector("#SaveUsername,#coid_website_rememberMeCheckbox");

        private static readonly By UserNameErrorMessageLocator = By.XPath("//span[@id='Username_validationMessage']");

        private static readonly By ErrorMessageBoxLocator = By.XPath("//*[@id='error-element-password']");

        private static readonly By PendoCloseButtonLocator = By.XPath("//button[contains(@class, 'pendo-close-guide')]");

        private static readonly By CookieSettingButtonLocator = By.XPath("//button[contains(text(),'Cookie Settings')]");

        private static readonly By AllowAllCookieButtonLocator = By.XPath("//button[contains(text(),'Allow All')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonSignOnPage"/> class. 
        /// Constructs the WestlawNext sign on page - navigates to the page based on the url
        /// and waits for the username text box
        /// </summary>
        /// <param name="environmentUnderTest"> Environment under test </param>
        public CommonSignOnPage(EnvironmentId environmentUnderTest)
        {
            BrowserPool.CurrentBrowser.GoToUrl(environmentUnderTest.GetUrlForWestlawNext());
            this.WaitForUsernameTextboxDisplayed();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommonSignOnPage"/> class. 
        /// Opens the sign on page and waits for the username textbox -
        /// this should only be used after a test has started - otherwise use the other constructor and pass in the environment
        /// </summary>
        public CommonSignOnPage()
        {
        }

        /// <summary>
        /// Selects Close button in AI Pendo Guide
        /// </summary>
        public IButton CloseAIPendoGuideButton => new Button(PendoCloseButtonLocator);

        /// <summary>
        /// Cookie button
        /// </summary>
        public IButton CookieSettingButton => new Button(CookieSettingButtonLocator);

        /// <summary>
        /// Determines if the accessibility Link is on the page
        /// </summary>
        /// <returns> True if it is displayed, false otherwise </returns>
        public bool IsAccessibilityLinkDisplayed() => DriverExtensions.IsDisplayed(AccessibilityLinkLocator, 5);

        /// <summary>
        /// Clears any text in the username or password boxes
        /// </summary>
        public void ClearUsernameAndPasswordTextboxes()
        {
            DriverExtensions.GetElement(UsernameTextboxLocator).Clear();
            DriverExtensions.GetElement(PasswordTextboxLocator).Clear();
        }

        /// <summary>
        /// clicks the sign on button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSignOn<T>() where T : ICreatablePageObject
        {
            DriverExtensions.ClickUsingJavaScript(SignOnButtonLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clears the password box and inputs the given text
        /// </summary>
        /// <param name="password"> The password. </param>
        public void EnterPassword(string password) => DriverExtensions.WaitForElement(PasswordTextboxLocator).SetTextField(password);

        /// <summary>
        /// Clears the user id box and inputs the given text
        /// </summary>
        /// <param name="userId"> The user Id. </param>
        public void EnterUserId(string userId) => DriverExtensions.WaitForElement(UsernameTextboxLocator).SetTextField(userId);

        /// <summary>
        /// Enters a given username and password into their respective fields.
        /// </summary>
        /// <param name="userName"> The username that is to be entered. </param>
        /// <param name="password"> The password that is to be entered. </param>
        public void EnterUserNameAndPassword(string userName, string password)
        {
            this.EnterUserId(userName);
            this.EnterPassword(password);
        }      

        /// <summary> Enters the UserId and password in their textboxes and clicks the sign on button </summary>
        /// <typeparam name="T"> page object </typeparam> 
        /// <param name="userid"> UserId to enter </param> 
        /// <param name="password"> Password to enter </param>
        /// <returns> ClientIdPage </returns>
        public T EnterUserIdPasswordAndClickSignOn<T>(string userid, string password) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(UsernameTextboxLocator).SendKeys(userid);

            // Extra wait for IE
            DriverExtensions.WaitForTextInElement(2000, userid, UsernameTextboxLocator);

            if (DriverExtensions.WaitForElement(SignOnButtonLocator).IsDisplayed())
            {
                DriverExtensions.GetElement(SignOnButtonLocator).JavascriptClick();
            }

            // Workaround for Ciam related Changes
            DriverExtensions.WaitForJavaScript(3000);

            DriverExtensions.WaitForElement(PasswordTextboxLocator).SendKeysSlow(password);
            DriverExtensions.WaitForElement(SignOnButtonLocator).JavascriptClick();
            DriverExtensions.WaitForPageLoad();

            if (new CommonSignOnPage().CookieSettingButton.Displayed)
            {
                new CommonSignOnPage().CookieSettingButton.Click();
                DriverExtensions.WaitForElement(AllowAllCookieButtonLocator).JavascriptClick();
            }
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enters a user id and password and then clicks the sign on button
        /// </summary>
        /// <typeparam name="T"> Type of the page we will end up on </typeparam>
        /// <param name="passwordInfo">password info object to log in with</param>
        /// <returns> A new instance of the page </returns>
        public T EnterUserIdPasswordAndClickSignOn<T>(IOnePassUserInfo passwordInfo) where T : ICreatablePageObject
                => this.EnterUserIdPasswordAndClickSignOn<T>(passwordInfo.UserName, passwordInfo.Password);

        /// <summary>
        /// Enters a user id and password and then clicks the sign on button
        /// </summary>
        /// <typeparam name="T"> Type of the page we will end up on </typeparam>
        /// <param name="passwordInfo">password info object to log in with</param>
        /// <returns> A new instance of the page </returns>
        public T EnterPasswordAndClickSignOn<T>(WlnUserInfo passwordInfo) where T : ICreatablePageObject
                => this.EnterUserIdPasswordAndClickSignOn<T>(passwordInfo.Email, passwordInfo.Password);

        /// <summary>
        /// Gets the informational message text
        /// </summary>
        /// <returns>The informational message text</returns>
        public string GetInformationalMessage() => DriverExtensions.GetText(InformationalMessageLocator);

        /// <summary>
        /// Checks if the password box has text
        /// </summary>
        /// <returns> True is password textbox contains text, false otherwise </returns>
        public bool DoesPasswordTextboxContainsText() => DriverExtensions.GetText(PasswordTextboxLocator).Length > 0;

        /// <summary>
        /// Determines if the password error prompt is on the page
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPasswordErrorMessageDisplayed() => DriverExtensions.IsDisplayed(PasswordErrorMessageLocator, 5);

        /// <summary>
        /// Checks if the save username and password checkbox is checked
        /// </summary>
        /// <returns> True If the checkbox is checked, false otherwise </returns>
        public bool IsSaveUsernameAndPasswordCheckboxChecked()
            => DriverExtensions.WaitForElement(SaveUsernameAndPasswordCheckboxLocator).Selected;

        /// <summary>
        /// Checks if the save username checkbox is checked
        /// </summary>
        /// <returns> True if the checkbox is checked, false otherwise </returns>
        public bool IsSaveUsernameCheckboxChecked() => DriverExtensions.WaitForElement(SaveUsernameCheckboxLocator).Selected;

        /// <summary>
        /// Determines if the sign on button is on the page
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSignOnButtonDisplayed() => DriverExtensions.IsDisplayed(SignOnButtonLocator, 5);

        /// <summary>
        /// Verify that Password textbox is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsPasswordTextboxDisplayed() => DriverExtensions.IsDisplayed(PasswordTextboxLocator, 5);

        /// <summary>
        /// Verify that Username textbox is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsUsernameTextboxDisplayed() => DriverExtensions.IsDisplayed(UsernameTextboxLocator, 5);

        /// <summary>
        /// Checks or unchecks the save username and password checkbox, depending on input
        /// </summary>
        /// <param name="setCheckbox"> Whether to set the checkbox to checked. True if checked, false to uncheck </param>
        public void SetSaveUsernameAndPasswordCheckbox(bool setCheckbox) => DriverExtensions.SetCheckbox(setCheckbox, SaveUsernameAndPasswordCheckboxLocator);

        /// <summary>
        /// Checks or uncheck the save username checkbox, depending on input
        /// </summary>
        /// <param name="setCheckbox"> Whether to set the checkbox to checked. True if checked, false to uncheck </param>
        public void SetSaveUsernameCheckbox(bool setCheckbox) => DriverExtensions.SetCheckbox(setCheckbox, SaveUsernameCheckboxLocator);

        /// <summary>
        /// Checks if the username box has text
        /// </summary>
        /// <returns> True is username textbox contains text, false otherwise </returns>
        public bool DoesUsernameBoxContainsText() => DriverExtensions.GetText(UsernameTextboxLocator).Length > 0;

        /// <summary>
        /// Determines if the username error prompt is on the page
        /// </summary>
        /// <returns> True if displayed, false otherwise</returns>
        public bool IsUsernameErrorMessageDisplayed() => DriverExtensions.IsDisplayed(UserNameErrorMessageLocator, 5);

        /// <summary>
        /// WaitForPage is used in both constructors - can be slimmed down once COSI is in all environments
        /// By default waits for non-COSI pages
        /// </summary>
        /// <param name="timeToWait"> The time To Wait. </param>
        public void WaitForUsernameTextboxDisplayed(int timeToWait = 30)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(timeToWait * 1000, UsernameTextboxLocator);
        }

        /// <summary>
        /// Get the text of an 'Password' validation message.
        /// </summary>
        /// <returns> Password validation message</returns>
        public string GetPasswordValidationMessageText() => DriverExtensions.GetText(PasswordErrorMessageLocator);

        /// <summary>
        /// Get the text of an 'Username' validation message.
        /// </summary>
        /// <returns> Username validation message</returns>
        public string GetUsernameValidationMessageText() => DriverExtensions.GetText(UserNameErrorMessageLocator);

        /// <summary>
        /// Get the text of an 'Password' validation message.
        /// </summary>
        /// <returns> Error message</returns>
        public string GetErrorMessageText() => DriverExtensions.GetText(ErrorMessageBoxLocator);

        /// <summary>
        /// Checks that there is any errors on the page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsEnvironmentErrorsOnPage()
        {
            string informationalMessage = DriverExtensions.IsDisplayed(InformationalMessageLocator)
                ? this.GetInformationalMessage()
                : string.Empty;

            return this.IsErrorPage
                || this.IsEnvironmentErrorMessageDisplayed()
                || DriverExtensions.IsDisplayed(ErrorMessageBoxLocator)
                || (DriverExtensions.IsDisplayed(InformationalMessageLocator) && !informationalMessage.Contains("You have been signed out due to inactivity."));
        }

        /// <summary>
        /// Checks Pendo Close Button
        /// </summary>
        /// <returns> True if it is displayed, false otherwise </returns>
        public bool IsAIPendoCloseButtonDisplayed() => DriverExtensions.IsDisplayed(PendoCloseButtonLocator, 10);

        /// <summary>
        /// Closes Pendo Guide
        /// </summary>
        /// <returns> </returns>
        public void CloseAIPendoGuide() => DriverExtensions.Click(PendoCloseButtonLocator);        
    }
}