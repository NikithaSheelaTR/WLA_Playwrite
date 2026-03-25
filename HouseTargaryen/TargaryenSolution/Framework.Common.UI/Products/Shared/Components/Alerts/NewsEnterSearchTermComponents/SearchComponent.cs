namespace Framework.Common.UI.Products.Shared.Components.Alerts.NewsEnterSearchTermComponents
{
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Support.PageObjects;

    /// <summary>
    /// WestClip alert Enter Search terms section Search Component for News content type
    /// </summary>
    public class SearchComponent : BaseModuleRegressionComponent
    {
        private static readonly By CaseSensitivityLinkLocator = By.Id("co_search_alertSearchPanelCaseSensitive");

        private static readonly By SearchInputTextBoxLocator = By.Id("searchInputIdAlerts");

        private static readonly By DocketTrackSearchInputTextBoxLocator = By.XPath("//div[@id='co_search_boxes']//input");

        private static readonly By SearchOnlyHeadlinesAndLeadParagraphCheckboxLocator = By.Id("co_search_alertSearchHlead");

        private static readonly By SearchOnlyHeadlinesAndLeadParagraphDisabledCheckboxLocator =
            By.XPath("//input[@id='co_search_alertSearchHlead' and @disabled]");

        private static readonly By TermFrequencyLinkLocator = By.Id("co_search_alertSearchPanelTermfrequency");

        private static readonly By ContainerLocator = By.Id("co_search_alertSearchPanel");

        private static readonly By PreciseCaseLawSearchHeaderLocator = By.XPath(".//h3[@class = 'co_wizardInternalBoxHeader']");

        private static readonly By PreciseCaseLawSearchInfoBoxLocator = By.XPath(".//div[contains(@class,'co_infoBox-preciseCaseLawSearch')]");

        private static readonly By TypeaheadInputLocator = By.XPath(".//input[@id='Typeahead-id']");

        private static readonly By XClearButtonLocator = By.XPath("./following-sibling::button[@class='Typeahead-clearButton']");

        /// <summary>
        ///  Precise Case Law Search Header label
        /// </summary>
        public ILabel PreciseCaseLawSearchHeaderLabel => new Label(this.ComponentLocator, PreciseCaseLawSearchHeaderLocator);

        /// <summary>
        ///  Precise Case Law Search InfoBox label
        /// </summary>
        public ILabel PreciseCaseLawSearchInfoBoxLabel => new Label(this.ComponentLocator, PreciseCaseLawSearchInfoBoxLocator);

        /// <summary>
        /// Precise case law type ahead textbox on WestClip
        /// </summary>
        public ITextbox TypeaheadTextbox => new TextboxWithClearButton(DriverExtensions.GetElement(this.ComponentLocator, TypeaheadInputLocator), XClearButtonLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Enters Search Text
        /// </summary>
        /// <param name="searchText">
        /// Search text 
        /// </param>
        /// <returns>
        /// The <see cref="EnterSearchTermsComponent"/>.
        /// </returns>
        public EnterSearchTermsComponent EnterSearchText(string searchText)
        {
            DriverExtensions.SetTextField(
                searchText,
                DriverExtensions.IsDisplayed(SearchInputTextBoxLocator)
                    ? SearchInputTextBoxLocator
                    : DocketTrackSearchInputTextBoxLocator);
            if (DriverExtensions.IsDisplayed(TermFrequencyLinkLocator))
                DriverExtensions.GetElement(TermFrequencyLinkLocator).Focus();
            return new EnterSearchTermsComponent();
        }

        /// <summary>
        /// Get Set Search Term
        /// </summary>
        /// <returns> Search term </returns>
        public string GetCurrentSearchTerm() => DriverExtensions.GetElement(SearchInputTextBoxLocator).GetAttribute("value");

        /// <summary>
        /// Checks if the search box is displayed.
        /// </summary>
        /// <returns>true if element is displayed</returns>
        public bool IsSearchTextBoxDisplayed() => DriverExtensions.IsDisplayed(SearchInputTextBoxLocator, 5);

        /// <summary>
        /// Is Case Sensitivity Link Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCaseSensitivityLinkDisplayed() => DriverExtensions.IsDisplayed(CaseSensitivityLinkLocator);

        /// <summary>
        /// Checks if the Search Only Headlines and Lead Paragraph Checkbox disabled
        /// </summary>
        /// <returns> True if Checkbox is disabled, false otherwise </returns>
        public bool IsSearchOnlyHeadlinesAndLeadParagraphCheckboxDisabled()
            => DriverExtensions.IsDisplayed(SearchOnlyHeadlinesAndLeadParagraphDisabledCheckboxLocator);

        /// <summary>
        /// Get 'Search Only Headlines And LeadParagraph' checkbox state
        /// </summary>
        /// <returns> True if the check box is checked, false otherwise </returns>
        public bool IsSearchOnlyHeadlinesAndLeadParagraphCheckboxSelected()
            => DriverExtensions.IsCheckboxSelected(SearchOnlyHeadlinesAndLeadParagraphCheckboxLocator);

        /// <summary>
        /// Is Term Frequency Link Displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsTermFrequencyLinkDisplayed() => DriverExtensions.IsDisplayed(TermFrequencyLinkLocator);

        /// <summary>
        /// Sets Search only headlines and lead paragraphs check box
        /// </summary>
        /// <param name="state"> The state  </param>
        public void SetSearchOnlyHeadlinesAndLeadParagraphCheckbox(bool state)
            => DriverExtensions.SetCheckbox(state, SearchOnlyHeadlinesAndLeadParagraphCheckboxLocator);
    }
}