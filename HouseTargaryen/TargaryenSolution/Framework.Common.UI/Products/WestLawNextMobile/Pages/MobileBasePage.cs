namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base page for all Mobile pages
    /// </summary>
    public class MobileBasePage : BaseModuleRegressionPage
    {
        private const string ErrorMessage = "An error occurred when processing your request.";

        private static readonly By FooterSignOffButtonLocator = By.CssSelector("#coid_websiteFooter_signofflink,img[alt='Sign Off']");

        private static readonly By RecipientTextBoxLocator = By.Id("deliveryRecipientEmailTextbox");

        private static readonly By WestlawNextLogoLocator = By.Id("coid_website_logoImage");

        private static readonly By FooterLocator = By.Id("footer");

        private static readonly By PageBodyLocator = By.TagName("body");

        /// <summary>
        /// Determines if the page is sitting on an error page (404, WLN blocked, or other error page)
        /// </summary>
        /// <value>True if we got an error page, false otherwise</value>
        public override bool IsErrorPage
        {
            get
            {
                DriverExtensions.WaitForJavaScript();
                return DriverExtensions.IsTextInElement(By.Id("main"), ErrorMessage);
            }
        }

        /// <summary>
        /// Clicks the WestlawNext logo in the header of the page
        /// </summary>
        /// <returns>A Homepage page-object</returns>
        public MobileHomePage ClickWestlawNextLogo() => this.ClickElement<MobileHomePage>(WestlawNextLogoLocator);

        /// <summary>
        /// Determines if the page has a given text
        /// </summary>
        /// <param name="text">Text to search for</param>
        /// <returns>True if the text is on the page, false otherwise</returns>
        public bool ContainsText(string text) => DriverExtensions.WaitForElement(PageBodyLocator).Text.Contains(text);
        

        /// <summary>
        /// Determines if the Sign Off button is displayed
        /// </summary>
        /// <returns>True if the button is displayed. False otherwise.</returns>
        public bool IsSignOffButtonDisplayed() => DriverExtensions.IsDisplayed(FooterSignOffButtonLocator, 5);

        /// <summary>
        /// PauseToFinishPageLoad
        /// </summary>
        public void PauseToFinishPageLoad() => DriverExtensions.WaitForElement(FooterLocator);

        /// <summary>
        /// Set text to the recipient textbox
        /// </summary>
        /// <param name="recipient"> Recipient </param>
        public void SetTextToRecipientTextbox(string recipient)
            => DriverExtensions.SetTextField(recipient, RecipientTextBoxLocator);

        /// <summary>
        /// Clicks the sign off button in the footer of mobile
        /// </summary>
        /// <typeparam name="T">The class of the page to return</typeparam>
        /// <returns>The page-object of type T</returns>
        public T SignOff<T>() where T : CommonSignOnPage => this.ClickElement<T>(FooterSignOffButtonLocator);

        /// <summary>
        /// Verify that partial link is displayed
        /// </summary>
        /// <param name="partialLink"> Link to verify </param>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsLinkWithSpecifiedTextDisplayed(string partialLink)
            => DriverExtensions.IsDisplayed(By.PartialLinkText(partialLink), 5);

        /// <summary>
        /// Click link by partial text
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="partialLink"> Link to click </param>
        /// <returns> New instance of the page </returns>
        public T ClickLinkBySpecifiedText<T>(string partialLink) where T : ICreatablePageObject
            => this.ClickElement<T>(By.PartialLinkText(partialLink));

        /// <summary>
        /// Switches to the main drafting assistant test page
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> New Instance of the page </returns>
        public T SwitchToMainDraftingFrame<T>() where T : BaseModuleRegressionPage
        {
            BrowserPool.CurrentBrowser.Driver.SwitchTo().Window(BrowserPool.CurrentBrowser.TabHandles[0]);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on element by locator
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="elementLocator"> Locator </param>
        /// <returns> New instance of the page </returns>
        protected T ClickElement<T>(By elementLocator) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(elementLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}