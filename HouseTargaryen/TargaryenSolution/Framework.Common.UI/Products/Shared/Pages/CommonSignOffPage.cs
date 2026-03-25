namespace Framework.Common.UI.Products.Shared.Pages
{
    using System.Linq;
  
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.SignOff;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// SignOffPage - result of clicking the sign off link
    /// </summary>
    public class CommonSignOffPage : BaseModuleRegressionPage, ICommonSignOffPage
    {
        private static readonly By DynamicMessageLocator = By.Id("co_dynamicMessageContainer");

        private static readonly By SignOnButtonLocator = By.Id("coid_website_signBackOnButton");

        private static readonly By ForceSignoutLocator = By.XPath("//a[text()='Return to sign in']");        

        private static readonly By AccessibilityLinkLocator = By.LinkText("Accessibility");

        private static readonly By SignOffReturnToLinkLocator = By.ClassName("co_signOff_returnLink");

        private static readonly By SessionSummaryEmailMessageLocator = By.Id("coid_signoff_historyemail");

        private static readonly By DescriptionLocator = By.XPath("//div[@id='coid_SessionActivityDetails']/table//td[2]");

        private static readonly By EventLocator = By.XPath("//div[@id='coid_SessionActivityDetails']/table//td[1]");

        private static readonly By HeaderLogoLocator = By.Id("co_logo");

        private static readonly By FooterLogoLocator = By.Id("co_trLogo");

        private static readonly By ForceSignOffLocator = By.XPath("//a[text()='Return to sign in']");

        private static readonly By SignOffMesageLocator = By.ClassName("co_signOff_message");

        private static readonly By CopyrightMessageLocator = By.XPath("//ul[@class='co_inlineList']/li");

        /// <summary>
        /// The history sign off table component.
        /// </summary>
        public SignOffSessionDetailsComponent SignOffSessionDetailsComponent => new SignOffSessionDetailsComponent();

        /// <summary>
        /// Clicks the accessibility link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickAccessibilityLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(AccessibilityLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the SignOn button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> SignOnPage object </returns>
        public T ClickSignOn<T>() where T : ICreatablePageObject
        { 
            DriverExtensions.WaitForElement(SignOnButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Returns Description of the column
        /// </summary>
        /// <param name="index">Index of the tab. Starts from zero</param>
        /// <returns>string</returns>
        public string GetDescription(int index) => DriverExtensions.GetElements(DescriptionLocator).ElementAt(index).Text;

        /// <summary>
        /// returns content value 
        /// </summary>
        /// <param name="index">Index of the tab. Starts from zero</param>
        /// <returns>string</returns>
        public string GetEvent(int index) => DriverExtensions.GetElements(EventLocator).ElementAt(index).Text;

        /// <summary>
        /// Returns Sign Off message
        /// </summary>
        /// <returns>The sign off text</returns>
        public string GetSignOffMessage() => DriverExtensions.GetText(SignOffMesageLocator);

        /// <summary>
        /// Checks if the custom signOff url is visible
        /// </summary>
        /// <returns>If the url is visible</returns>
        public bool IsReturnToLinkDisplayed() => DriverExtensions.IsDisplayed(SignOffReturnToLinkLocator, 5);

        /// <summary>
        /// gets the text of the custom return url
        /// </summary>
        /// <returns>The text of the url</returns>
        public string GetReturnToLinkText() => DriverExtensions.GetText(SignOffReturnToLinkLocator);

        /// <summary>
        /// Returns the session summary email message text
        /// </summary>
        /// <returns>The message text</returns>
        public string GetSessionSummaryEmailMessageText() => DriverExtensions.WaitForElement(SessionSummaryEmailMessageLocator).Text;

        /// <summary>
        /// Determines if the Dynamic Messaging promo is displayed
        /// </summary>
        /// <returns>True if displayed, false otherwise</returns>
        public bool IsDynamicMessagingPromoDisplayed() => DriverExtensions.IsDisplayed(DynamicMessageLocator);

        /// <summary>
        /// Returns if the session summary email message exists
        /// </summary>
        /// <returns>If the session summary message exists</returns>
        public bool IsSessionSummaryEmailMessageDisplayed() => DriverExtensions.IsDisplayed(SessionSummaryEmailMessageLocator);

        /// <summary>
        /// Verify that Sign On button is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsSignOnButtonDisplayed() => DriverExtensions.IsDisplayed(SignOnButtonLocator, 5);

        /// <summary>
        ///  Verify that Header logo is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsHeaderLogoDisplayed() => DriverExtensions.IsDisplayed(HeaderLogoLocator);

        /// <summary>
        /// Verify that Footer logo is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsFooterLogoDisplayed() => DriverExtensions.IsDisplayed(FooterLogoLocator);

        /// <summary>
        /// Verify that Copyright Message is displayed
        /// </summary>
        /// <returns> True if button is displayed, false otherwise </returns>
        public bool IsCopyrightMessageDisplayed() => DriverExtensions.IsDisplayed(CopyrightMessageLocator);

        /// <summary>
        /// ForcesignoffLink 
        /// </summary>
        public ILink ForcesignoffLink => new Link (ForceSignoutLocator);
    }
}