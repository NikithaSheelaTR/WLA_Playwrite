namespace Framework.Common.UI.Products.WestLawNextMobile.Components
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Component to present the Search header at the top of WLN Mobile pages
    /// </summary>
    public class MobileSearchHeaderComponent : BaseModuleRegressionComponent
    {
        private static readonly By ChangeJurisdictionLinkLocator = By.Id("coid_website_changeJurisdictionLink");

        private static readonly By ClientIdLinkLocator = By.Id("coid_website_changeClientIDLink");

        private static readonly By CopyrightLinkLocator = By.Id("footerCopyright");

        private static readonly By CurrentJurisdictionLocator = By.Id("coid_website_changeJurisdictionLinkTitle");

        private static readonly By HelpAndFeedbackLinkLocator = By.Id("coid_website_helpLink");

        private static readonly By SearchButtonLocator = By.Id("coid_website_searchButton");

        private static readonly By SearchTextboxLocator = By.XPath("//div[@class='page']//input[@class='txtIn']");

        private static readonly By WestlawNextLogoLocator = By.Id("coid_website_logoImage");

        private static readonly By ContainerLocator = By.Id("footer");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Click on the Change Jurisdiction link
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickChangeJurisdictionLink<T>() where T : ICreatablePageObject
            => this.ClickLink<T>(ChangeJurisdictionLinkLocator);

        /// <summary>
        /// Click on the Copyright link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickCopyrightLink<T>() where T : ICreatablePageObject => this.ClickLink<T>(CopyrightLinkLocator);

        /// <summary>
        /// Click on the Help And Feedback link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickHelpAndFeedbackLink<T>() where T : ICreatablePageObject
            => this.ClickLink<T>(HelpAndFeedbackLinkLocator);

        /// <summary>
        /// Click on the Client Id link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickOnClientIdLink<T>() where T : ICreatablePageObject => this.ClickLink<T>(ClientIdLinkLocator);

        /// <summary>
        /// Click on the Westlaw Next logo image
        /// </summary>
        /// <typeparam name="T"> Page Type </typeparam>
        /// <returns> New instance of the page </returns>
        public T ClickOnWestlawNextLogo<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(WestlawNextLogoLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get text from Client Id link
        /// </summary>
        /// <returns> Client Id </returns>
        public string GetClientIdLinkText() => DriverExtensions.GetText(ClientIdLinkLocator);

        /// <summary>
        /// Gets the current jurisdiction string
        /// </summary>
        /// <returns>Current jurisdiction</returns>
        public string GetCurrentJurisdiction() => DriverExtensions.GetText(CurrentJurisdictionLocator);

        /// <summary>
        /// Get the text of the search text box
        /// </summary>
        /// <returns> Text from the Search text box </returns>
        public string GetSearchQueryText() => DriverExtensions.GetText(SearchTextboxLocator);

        /// <summary>
        /// Verify that client Id link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsClientIdLinkDisplayed() => DriverExtensions.IsDisplayed(ClientIdLinkLocator, 5);

        /// <summary>
        /// Verify that search text box is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSearchTextboxDisplayed() => DriverExtensions.IsDisplayed(SearchTextboxLocator, 5);

        /// <summary>
        /// Enter a search query and submits the search
        /// </summary>
        /// <typeparam name="T"> The class of the page-object to return </typeparam>
        /// <param name="query"> The query to search for </param>
        /// <param name="isSlow"> The is Slowly. </param>
        /// <returns> New instance of the page </returns>
        public T SetSearchQueryAndSubmit<T>(string query, bool isSlow = false) where T : BaseModuleRegressionPage
        {
            if (isSlow)
            {
                DriverExtensions.WaitForElement(SearchTextboxLocator).SendKeysSlow(query);
            }
            else
            {
                DriverExtensions.GetElement(SearchTextboxLocator).SetTextField(query);
            }

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        private T ClickLink<T>(By elementLocator) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(elementLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }
    }
}