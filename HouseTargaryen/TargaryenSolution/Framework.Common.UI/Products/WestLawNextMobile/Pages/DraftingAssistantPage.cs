namespace Framework.Common.UI.Products.WestLawNextMobile.Pages
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Drafting Assistant page
    /// </summary>
    public class DraftingAssistantPage : MobileBasePageWithHeader
    {
        private static readonly By JurisdictionTextLocator = By.Id("jurisdictions");

        private static readonly By QueryTextboxLocator = By.Id("authorityQuery");

        private static readonly By SearchButtonLocator = By.XPath("//input[@value='Search']");

        private static readonly By FrameLocator = By.XPath("//iframe");

        /// <summary>
        /// Click link by name and switch to WLN Mobile Frame
        /// </summary>
        /// <param name="linkName"> The link Name. </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickLinkByTextAndSwitchToMobileFrame<T>(string linkName) where T : BaseModuleRegressionPage
        {
            DriverExtensions.Click(By.LinkText(linkName));
            DriverExtensions.WaitForJavaScript();
            this.SwitchToMobileWlnFrame<T>();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the search button and switch to Mobile frame
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickSearchButtonAndAwitchToMobileFrame<T>() where T : BaseModuleRegressionPage
        {
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return this.SwitchToMobileWlnFrame<T>();
        }

        /// <summary>
        /// Gets the current jurisdiction text
        /// </summary>
        /// <returns>The current jurisdiction text</returns>
        public string GetJurisdictionText() => DriverExtensions.WaitForElement(JurisdictionTextLocator).Text;

        /// <summary>
        /// Enters text into the authority 
        /// </summary>
        /// <param name="queryText"> The query Text. </param>
        public void EnterQuery(string queryText) => DriverExtensions.WaitForElement(QueryTextboxLocator).SendKeys(queryText);

        /// <summary>
        /// Switches to the WLN Mobile frame
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T SwitchToMobileWlnFrame<T>() where T : BaseModuleRegressionPage
        {
            BrowserPool.CurrentBrowser.SwitchToFrame(FrameLocator);
            DriverExtensions.WaitForPageLoad();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}