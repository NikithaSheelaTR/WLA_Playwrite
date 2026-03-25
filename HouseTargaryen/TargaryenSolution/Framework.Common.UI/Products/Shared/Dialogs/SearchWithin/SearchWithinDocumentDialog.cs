namespace Framework.Common.UI.Products.Shared.Dialogs.SearchWithin
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search Within Document Dialog
    /// </summary>
    public class SearchWithinDocumentDialog : BaseModuleRegressionDialog
    {
        private const string SavedSearchLctMask = ".//ul//li/*[text()='{0}']";

        private static readonly By CloseButtonLocator = By.Id("co_docSearchWithinCloseButton");

        private static readonly By SearchButtonLocator = By.Id("co_docSearchWithin_searchButton");

        private static readonly By SearchInputLocator = By.XPath("//textarea[contains(@id, 'co_docSearchWithin_searchInput')]");

        private static readonly By SearchTermSuggestionLocator = By.Id("co_docSearchWithinTermSuggestion");

        private static readonly By SearchWithinHelpLocator = By.Id("co_docSearchWithin_help");

        private static readonly By CloseButtonInfoBoxLocator = By.XPath(".//button[@class = 'co_infoBox_closeButton']");

        private static readonly By InfoBoxContainerLocator = By.ClassName("co_infoBox_container");

        private static readonly By InfoBoxTitleLocator = By.Id("co_search_advancedSearch_tncHelpTitle");

        private static readonly By VerticalRightDocToolbarInfoBoxContainerLocator = By.Id("co_docToolbarVerticalMenuRight");

        private static readonly By DialogContainerLocator = By.XPath("//div[@id = 'co_docSearchWithinDropdownContainerInnerContent']");

        private static readonly By NextTermArrowLocator = By.XPath(".//*[contains(@class, 'co_next')]");

        private static readonly By PreviousTermArrowLocator = By.XPath(".//button[contains(@class, 'co_prev')]");

        private static readonly By RecentSearchesLocator = By.XPath(".//button[@class = 'co_recentSearchesLink']");

        private static readonly By RecentSearchesQueryLocator = By.XPath(".//ul[contains(@class ,'co_searchSuggestionContainer')]//li/*");

        private static readonly By InfoBoxMessageLocator = By.XPath(".//div[@class='co_infoBox_message']");

        private static readonly By RemoveSearchLocator = By.ClassName("co_searchWithinWidget_removeSearch");

        /// <summary>
        /// InfoBox
        /// </summary>
        public IInfoBox InfoBox => new InfoBox(InfoBoxContainerLocator, InfoBoxMessageLocator, CloseButtonInfoBoxLocator);

        /// <summary>
        /// Remove search link
        /// </summary>
        public ILink RemoveSearchLink => new Link(DialogContainerLocator, RemoveSearchLocator);

        /// <summary>
        /// InfoLabel
        /// </summary>
        public ILabel InfoLabel => new Label(DialogContainerLocator, InfoBoxMessageLocator);

        /// <summary>
        /// Label of the info box for VerticalRightDocToolbar (Search Within Help icon)
        /// </summary>
        /// <returns>String Value</returns>
        public ILabel SearchWithinHelpInfoBoxLabel => new Label(VerticalRightDocToolbarInfoBoxContainerLocator, InfoBoxTitleLocator);

        /// <summary>
        /// Container element
        /// </summary>
        private IWebElement Container => DriverExtensions.GetElement(DialogContainerLocator);

        /// <summary>
        /// Click on the search close button
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> DocumentPage </returns>
        public T ClickSearchCloseButton<T>() where T : ICreatablePageObject => this.ClickElement<T>(CloseButtonLocator);

        /// <summary>
        /// Clicks NextTermArrow
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T page</returns>
        public T ClickNextTermArrowButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(this.Container, NextTermArrowLocator);

        /// <summary>
        /// Clicks NextTermArrow
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>T page</returns>
        public T ClickPreviousTermArrowButton<T>() where T : ICreatablePageObject
            => this.ClickElement<T>(this.Container, PreviousTermArrowLocator);      

        /// <summary>
        /// Click on the search Info box button
        /// </summary>
        /// <returns> SearchWithinDialog </returns>
        public SearchWithinDocumentDialog ClickInfoBoxButton()
            => this.ClickElement<SearchWithinDocumentDialog>(SearchWithinHelpLocator);

        /// <summary>
        /// Click on the Recent Searches button
        /// </summary>
        /// <returns>The <see cref="SearchWithinDocumentDialog"/>.</returns>
        public SearchWithinDocumentDialog ClickRecentSearchesButton()
            => this.ClickElement<SearchWithinDocumentDialog>(this.Container, RecentSearchesLocator);

        /// <summary>
        /// Click Recent Searches Query
        /// </summary>
        /// <param name="index">
        /// </param>
        /// <returns>The <see cref="SearchWithinDocumentDialog"/>.</returns>
        public SearchWithinDocumentDialog ClickRecentSearchesQuery(int index = 0)
            => this.ClickElement<SearchWithinDocumentDialog>(DriverExtensions.GetElements(this.Container, RecentSearchesQueryLocator)
                                                                             .ElementAt(index));

        /// <summary>
        /// Get Recent Searches queries
        /// </summary>
        /// <returns>The list of strings.</returns>
        public List<string> GetRecentSearchesQueries()
            => DriverExtensions.GetElements(this.Container, RecentSearchesQueryLocator).Select(query => query.Text)
                               .ToList();

        /// <summary>
        /// Verify is recently search query enabled
        /// </summary>
        /// <param name="query">Query</param>
        /// <returns>True if search query enabled</returns>
        public bool IsRecentSearchQueryEnabled(string query)
            => !DriverExtensions.SafeGetElement(this.Container, By.XPath(string.Format(SavedSearchLctMask, query)))
                                ?.GetAttribute("class").Contains("disabled") ?? false;

        /// <summary>
        /// Gets the value of the SearchInputId for the current document
        /// </summary>
        /// <returns>String Value</returns>
        public string GetSearchInputIdValue() => DriverExtensions.GetText(SearchInputLocator);

        /// <summary>
        /// Gets the info box title text
        /// </summary>
        /// <returns>String Value</returns>
        public string GetInfoBoxTitle() => DriverExtensions.GetElement(InfoBoxContainerLocator, InfoBoxTitleLocator).Text;

        /// <summary>
        /// verifies if the search term suggestion is displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsDocSearchTermSuggestionDisplayed() => DriverExtensions.IsDisplayed(SearchTermSuggestionLocator);
                
        /// <summary>
        /// This methods searches within the current document
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="searchQuery"> Search term </param>
        /// <returns> New instance of the page </returns>
        public T SearchWithinDocument<T>(string searchQuery) where T : ICreatablePageObject
        {
            this.EnterSearchWithinQuery(searchQuery);
            return this.ClickElement<T>(SearchButtonLocator);
        }

        /// <summary>
        /// This methods enters Search Within query
        /// </summary>
        /// <param name="searchQuery">Search term</param>
        public void EnterSearchWithinQuery(string searchQuery)
            => DriverExtensions.SetTextField(searchQuery, SearchInputLocator);

        /// <summary>
        /// verifies if the text box is displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsSearchWithinTextBoxDisplayed() => DriverExtensions.IsDisplayed(SearchInputLocator, 5);

        /// <summary>
        /// verifies if the close button is displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsSearchWithinCloseButtonDisplayed() => DriverExtensions.IsDisplayed(CloseButtonLocator);

        /// <summary>
        /// verifies if the search button is displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsSearchWithinSearchButtonDisplayed() => DriverExtensions.IsDisplayed(SearchButtonLocator, 5);

        /// <summary>
        /// verifies if the search within help is displayed
        /// </summary>
        /// <returns>Boolean Value</returns>
        public bool IsSearchWithinHelpDisplayed() => DriverExtensions.IsDisplayed(SearchWithinHelpLocator);
    }
}