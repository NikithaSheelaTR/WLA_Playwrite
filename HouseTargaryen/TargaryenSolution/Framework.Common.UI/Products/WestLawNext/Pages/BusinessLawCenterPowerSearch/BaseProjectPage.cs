namespace Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch
{
    using Framework.Common.UI.Products.WestLawNext.Utils.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Base class for project and project list pages
    /// </summary>
    public abstract class BaseProjectPage : GlobalHeaderFooterPage
    {
        /// <summary>
        /// The toggle all.
        /// </summary>
        protected static readonly By ToggleAllLocator = By.XPath("//input[@id='searchHeader_selectAll' and @type='checkbox']");

        private static readonly By ClearSelectedLinkLocator = By.XPath("//a[@ng-show='areAnySelected()']");

        private static readonly By DeleteSelectedLocator = By.Id("cobalt_ro_detail_trash");

        private static readonly By DeleteYesButtonLocator =
            By.XPath("//div[@class='co_overlayBox_optionsBottom']/ul/li[1]/input");

        private static readonly By SpinnerLocator = By.Id("addProjectSpinner");

        /// <summary>
        /// Select all items - Select Toggle all project check box on Projects page or all filings for project page.
        /// </summary>
        public void SelectAllItems()
        {
            IWebElement toggleAllCheckbox = DriverExtensions.WaitForElement(ToggleAllLocator);

            ActionExtensions.DoUntilConditionWillBecomeTrue(
                () => toggleAllCheckbox.JavascriptClick(),
                () => DriverExtensions.IsDisplayed(ClearSelectedLinkLocator, 3));
        }

        /// <summary>
        /// The delete selected items.
        /// </summary>
        /// <param name="messageLocator">The message locator.</param>
        protected void DeleteSelectedItems(By messageLocator)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();

            ActionExtensions.DoUntilConditionWillBecomeTrue(
                DriverExtensions.WaitForElement(DeleteSelectedLocator).Click,
                () => DriverExtensions.IsDisplayed(DeleteYesButtonLocator, 3));

            DriverExtensions.WaitForElement(DeleteYesButtonLocator).Click();

            this.WaitforSpinnerToGo(80000);

            DriverExtensions.WaitForElement(messageLocator, 60000);
            DriverExtensions.RefreshPage();
        }

        /// <summary>
        /// Wait for spinner
        /// </summary>
        /// <param name="timeout">timeout</param>
        protected void WaitforSpinnerToGo(int timeout = 60000)
        {
            DriverExtensions.WaitForElementNotDisplayed(timeout, SpinnerLocator);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForPageLoad();
        }
    }
}