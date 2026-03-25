namespace Framework.Common.UI.Products.WestLawNext.Pages.PortalManagerSearch.HtmlFilePages
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// This POM is for the html document that is downloaded or created using the GetHTML functionality
    /// </summary>
    public class GlobalSearchPortalPage : BaseModuleRegressionPage
    {
        private static readonly By BrowseHeaderLocator = By.XPath("//div[@class='co_browseHeader']");

        private static readonly By DropdownLocator = By.XPath("//select[@id='_dropdown']");

        private static readonly By ErrorMessageLocator = By.XPath("//div[@id='wln_errorMsgDiv']/span[@id='_validate']");

        private static readonly By FormDropdownOptionsLocator = By.XPath("//select[@id='_dropdown']/option");

        private static readonly By SearchButtonLocator = By.XPath("//div[@id='searchDiv']/img[@id='searchIcn']");

        private static readonly By SearchInputLocator = By.XPath("//div[@id='searchDiv']/input[@id='_searchQueryID']");

        /// <summary>
        /// Returns the options as string list
        /// </summary>
        /// <returns> The list of Dropdown Options</returns>
        public List<string> GetDropdownOptions()
            => DriverExtensions.GetElements(FormDropdownOptionsLocator).Select(el => el.Text.Trim()).ToList();

        /// <summary>
        /// returns true if there is a error on the page
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsErrorMessageDisplayed() => DriverExtensions.IsDisplayed(ErrorMessageLocator, 5);

        /// <summary>
        /// returns true if search bar is enabled.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsSearchEnabled() => DriverExtensions.WaitForElement(SearchInputLocator).Enabled;

        /// <summary>
        /// Clicks on the search icon
        /// </summary>
        /// <typeparam name="TPage">Page Object instance</typeparam>
        /// <returns>New Page Object instance</returns>
        public TPage ClickSearch<TPage>() where TPage : BaseModuleRegressionPage
        {
            DriverExtensions.Click(SearchButtonLocator);
            return DriverExtensions.CreatePageInstance<TPage>();
        }

        /// <summary>
        /// Enters the search term
        /// </summary>
        /// <param name="searchTerm">String to search for</param>
        public void EnterSearchTerm(string searchTerm)
            => DriverExtensions.WaitForElement(SearchInputLocator).SendKeysSlow(searchTerm);

        /// <summary>
        /// Selects the content link and returns true if the proper WLN page is displayed.
        /// Navigates back to original page.
        /// </summary>
        /// <param name="itemName">Item name</param>
        /// <returns>true if content is displayed.</returns>
        public bool SelectAndVerifyContentLink(string itemName)
        {
            DriverExtensions.WaitForElement(By.LinkText(itemName)).Click();
            return DriverExtensions.WaitForElement(BrowseHeaderLocator).Text.Contains(itemName);
        }

        /// <summary>
        /// selects the item from dropdown.
        /// </summary>
        /// <param name="itemName">Item name</param>
        public void SelectDropDownItem(string itemName)
        {
            DriverExtensions.WaitForElementPresent(DropdownLocator);
            DriverExtensions.SetDropdown(itemName, DropdownLocator);
        }
    }
}