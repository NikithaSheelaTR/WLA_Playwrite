namespace Framework.Common.UI.Products.WestLawNext.Pages.Search
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Advanced Search: Dockets page
    /// </summary>
    public class DocketsAdvancedSearchPage : CommonAdvancedSearchPage
    {
        private const string SelectionNameLctMask = "id('selectedItemsControlId')/li/div[contains(text(),{0})]";

        private static readonly By AdvancedSearchButtonLocator = By.Id("co_search_advancedSearchButton_bottom");

        private static readonly By CurrentSelectionsLocator = By.XPath("id('selectedItemsControlId')//div");

        private static readonly By FederalBankruptcySelectionLocator =
            By.XPath("id('co_wizardStep_left_Home_Dockets_FederalDockets_FederalBankruptcyCourtDockets')/i");

        private static readonly By FederalTabLocator = By.XPath("//div[@class='co_genericBoxTabs']//a[text()='Federal']");

        private static readonly By ShowCourtSelectorLinkLocator = By.Id("courtSelectShow");

        private static readonly By UsSupremeCourtSelectionLocator =
            By.XPath("id('co_wizardStep_left_Home_Dockets_FederalDockets_USSupremeCourtDockets')/i");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocketsAdvancedSearchPage"/> class. 
        /// Constructs the Advanced Search: Dockets page
        /// </summary>
        public DocketsAdvancedSearchPage()
        {
            DriverExtensions.WaitForElement(AdvancedSearchButtonLocator);
        }

        /// <summary>
        /// Adds U.S. Supreme Court and Federal Bankruptcy Courts
        /// </summary>
        public void AddPreDefinedCourts()
        {
            DriverExtensions.WaitForElement(FederalTabLocator).Click();
            DriverExtensions.WaitForElement(UsSupremeCourtSelectionLocator).Click();
            DriverExtensions.WaitForElement(FederalBankruptcySelectionLocator).Click();
        }

        /// <summary>
        /// The click advanced search.
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>New instance of T page</returns>
        public T ClickAdvancedSearch<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(AdvancedSearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the Show Court Selector link
        /// </summary>
        public void ClickShowCourtSelector() => DriverExtensions.WaitForElement(ShowCourtSelectorLinkLocator).Click();

        /// <summary>
        /// Returns all the selected courts in the Your Selections section
        /// </summary>
        /// <returns>List of strings of selected items</returns>
        public List<string> GetSelections() => DriverExtensions.GetElements(CurrentSelectionsLocator).Select(el => el.Text).ToList();

        /// <summary>
        /// Removes the specified court i.e., selection text
        /// </summary>
        /// <param name="selectionName">Selection name to remove</param>
        public void RemoveSelection(string selectionName)
            => DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SelectionNameLctMask, selectionName)).Click();
    }
}