namespace Framework.Common.UI.Products.Shared.Pages.Footer
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Live Chat stream page
    /// </summary>
    public class LiveChatStreamPage : BaseModuleRegressionPage
    {
        private static readonly By MessageButtonLocator = By.XPath("//*[@id='embeddedMessagingConversationButton']");

        private static readonly By ChatMessageBoxLocator = By.Id("egain-chat-transcript");

        private static readonly By CloseButtonLocator = By.ClassName("closechat");

        private static readonly By CopyButtonLocator = By.ClassName("btnCopy-box");

        private static readonly By EnterChatMessageBoxLocator =
            By.XPath("//textarea[@ng-model='customerMessage']");

        private static readonly By LiveChatOffHoursMessageLocator = By.ClassName("co_chatOffHoursMessage");

        private static readonly By PrintButtonLocator = By.ClassName("print-transcript js-print");

        private static readonly By SendButtonLocator = By.Id("submit");

        private static readonly By TextEditorFrameLocator = By.XPath("//div[@id='cke_1_contents']//iframe");

        /// <summary>
        /// Agent Help button
        /// </summary>
        public IButton MessageTitleButton => new Button(MessageButtonLocator);  

        /// <summary>
        /// Live chat off hours label
        /// </summary>
        public ILabel LiveChatOffHoursLabel => new Label(LiveChatOffHoursMessageLocator);

        /// <summary>
        /// Click the 'Send' button
        /// </summary>
        public void ClickSendButton() => DriverExtensions.Click(SendButtonLocator);

        /// <summary>
        /// Enter the chat message. 
        /// </summary>
        /// <param name="message">The text of the message</param>
        /// <returns>Live Chat Stream Page</returns>
        public LiveChatStreamPage EnterChatMessage(string message)
        {
            DriverExtensions.WaitForElementDisplayed(EnterChatMessageBoxLocator);
            DriverExtensions.SetTextField(message, EnterChatMessageBoxLocator);
            BrowserPool.CurrentBrowser.Driver.SwitchTo().DefaultContent();
            return this;
        }

        /// <summary>
        /// Gets the test of the chat messages
        /// </summary>
        /// <returns>The text of the chat messages box</returns>
        public string GetChatText() => DriverExtensions.GetText(ChatMessageBoxLocator);

        /// <summary>
        /// Determines if the chat messages box is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsChatMessagesBoxDisplayed() => DriverExtensions.IsDisplayed(ChatMessageBoxLocator, 5);

        /// <summary>
        /// Determines if the Copy button is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsCopyButtonDisplayed() => DriverExtensions.IsDisplayed(CopyButtonLocator);

        /// <summary>
        /// Determines if the enter chat message box is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsEnterChatMessageBoxDisplayed()
        {
            this.SwitchToTextEditorFrame();
            bool isEnterChatMessageBoxDisplayed = DriverExtensions.IsDisplayed(EnterChatMessageBoxLocator);
            BrowserPool.CurrentBrowser.Driver.SwitchTo().DefaultContent();
            return isEnterChatMessageBoxDisplayed;
        }

        /// <summary>
        /// Determines if the Exit button is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsExitButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);

        /// <summary>
        /// Determines if the Print button is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsPrintButtonDisplayed() => DriverExtensions.IsDisplayed(PrintButtonLocator);

        /// <summary>
        /// Determines if the Print button is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsSendButtonDisplayed() => DriverExtensions.IsDisplayed(SendButtonLocator);

        private void SwitchToTextEditorFrame()
            =>
                BrowserPool.CurrentBrowser.Driver.SwitchTo()
                           .Frame(DriverExtensions.WaitForElementDisplayed(TextEditorFrameLocator));
    }
}