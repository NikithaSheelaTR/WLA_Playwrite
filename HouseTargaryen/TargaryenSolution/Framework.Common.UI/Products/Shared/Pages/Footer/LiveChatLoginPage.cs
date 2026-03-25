namespace Framework.Common.UI.Products.Shared.Pages.Footer
{
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Live Chat login page
    /// </summary>
    public class LiveChatLoginPage : BaseModuleRegressionPage
    {
        private const string LiveChatClosedMessage = "Sorry, Live Chat is closed";

        private static readonly By EGainLabelLocator = By.XPath("//a[text()='eGain']");

        private static readonly By EmailTextBoxLocator = By.XPath("//input[@name='email_address']");

        private static readonly By NameTextBoxLocator = By.XPath("//input[@name='full_name']");

        private static readonly By QuestionTextBoxLocator = By.XPath("//textarea[@name='subject']");

        private static readonly By StartChatButtonLocator = By.XPath("//button[text()='Start Chat']");

        private static readonly By OffHoursMessageLocator = By.ClassName("co_chatOffHoursMessage");

        /// <summary>
        /// Clicks the Chat button
        /// </summary>
        /// <returns>a Live Chat stream page</returns>
        public LiveChatStreamPage ClickChatButton()
        {
            DriverExtensions.Click(StartChatButtonLocator);
            return new LiveChatStreamPage();
        }

        /// <summary>
        /// Types the given text into the Email textbox
        /// </summary>
        /// <param name="email">Email text</param>
        /// <returns>Live Chat Login Page</returns>
        public LiveChatLoginPage EnterEmail(string email)
        {
            DriverExtensions.SetTextField(email, EmailTextBoxLocator);
            return this;
        }

        /// <summary>
        /// Types the given text into the Name textbox
        /// </summary>
        /// <param name="name">Name text</param>
        /// <returns>Live Chat Login Page</returns>
        public LiveChatLoginPage EnterName(string name)
        {
            DriverExtensions.WaitForElementDisplayed(NameTextBoxLocator);
            DriverExtensions.SetTextField(name, NameTextBoxLocator);
            return this;
        }

        /// <summary>
        /// Types the given text into the Question textbox
        /// </summary>
        /// <param name="question">Question text</param>
        /// <returns>Live Chat Login Page</returns>
        public LiveChatLoginPage EnterQuestion(string question)
        {
            DriverExtensions.SetTextField(question, QuestionTextBoxLocator);
            return this;
        }

        /// <summary>
        /// Verifies that eGain logo is displayed.
        /// </summary>
        /// <returns>True if eGain logo is displayed.</returns>
        public bool IsEGainLogoDisplayed() => DriverExtensions.IsDisplayed(EGainLabelLocator, 5);

        /// <summary>
        /// Checks if live chat is closed.
        /// </summary>
        /// <returns>If live chat is closed based on the screen text.</returns>
        public bool IsLiveChatClosed() => DriverExtensions.IsTextOnPage(LiveChatClosedMessage);

        /// <summary>
        /// Get Off Hours Message.
        /// </summary>
        /// <returns>Off Hours Message.</returns>
        public string GetOffHoursMessage() => DriverExtensions.WaitForElement(OffHoursMessageLocator).Text;
    }
}