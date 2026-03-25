namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Alerts.NewsEnterSearchTermComponents;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Enter Search Terms Section for Alert
    /// </summary>
    public class EnterSearchTermsComponent : BaseAlertComponent
    {
        private const string ChaptersCheckboxLocator = "//li[@class='co_formInline']/label[text()={0}]/input";

        private const string ChaptersNumberLocator = "//li[@class='co_formInline']/label[text()={0}]";

        private static readonly By AlertMeToAllNewFilingsRadioButtonLocator = By.Id("co_search_alertMeToNewFilings");

        private static readonly By DoNotLimitResultsRadioButtonLocator = By.Id("do_not_limit_results");

        private static readonly By LimitResultsRadioButtonLocator = By.Id("limit_results");

        private static readonly By PartyPlaintiffCheckboxLocator = By.Id("plaintiff_checkbox");

        private static readonly By PreviewResultsButtonLocator = By.Id("co_button_previewResults_Search");

        private static readonly By SearchPartyInputLocator = By.XPath("//div[@id='co_search_alertSearchPanelPartyBox']/input");

        private static readonly By SelectNatureOfSuiteLinkLocator = By.Id("co_nos_link");

        private static readonly By SmartSearchComponentLocator =
            By.XPath("//div[@class='co_search_alertSearchPanelSmartTermsWrapper co_alertPanelBox']");

        private static readonly By ContainerLocator = By.Id("searchSection");

        private static readonly By AlertSearchTermsLocator = By.XPath("//textarea[@name = 'searchInputIdAlerts']");

        private static readonly By SearchQueryLocator = By.Id("summaryquery");

        private static readonly By EditLinkLocator = By.Id("coid_editLink_Search");

        private static readonly By EnterSearchTermsHeaderLabelLocator = By.XPath(".//h2[@id='searchBellowHeader']/strong");

        private static readonly By TermFrequencyLabelLocator = By.Id("co_search_alertSearchPanelTermfrequency");

        private static readonly By TermsAndConnectorsHelpLabelLocator = By.XPath(".//div[@id='co_search_alertSearchPanelHeaderBox']/span[2]");

        private static readonly By SortOrderLabelLocator = By.XPath(".//li[@id='co_sort_field']//label");

        private static readonly By DocumentsNoOlderThanLabelLocator = By.XPath(".//label[@for='co_search_alertSearchPanelDateFilterTypes']");

        /// <summary>
        /// Search Component
        /// </summary>
        public SearchComponent Search { get; set; } = new SearchComponent();

        /// <summary>
        ///  Enter search terms header label
        /// </summary>
        public ILabel EnterSearchTermsHeaderLabel => new Label(this.ComponentLocator, EnterSearchTermsHeaderLabelLocator);

        /// <summary>
        ///  Term frequency label
        /// </summary>
        public ILabel TermFrequencyLabel => new Label(this.ComponentLocator, TermFrequencyLabelLocator);

        /// <summary>
        ///  Sort order label
        /// </summary>
        public ILabel SortOrderLabel => new Label(this.ComponentLocator, SortOrderLabelLocator);

        /// <summary>
        ///  Terms and connectors help label
        /// </summary>
        public ILabel TermsAndConnectorsHelpLabel => new Label(this.ComponentLocator, TermsAndConnectorsHelpLabelLocator);

        /// <summary>
        ///  Documents no older than label
        /// </summary>
        public ILabel DocumentsNoOlderThanLabel => new Label(this.ComponentLocator, DocumentsNoOlderThanLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Checks Bankruptcy Chapter Checkbox
        /// </summary>
        /// <param name="checkboxName"> Bankruptcy Chapter </param>
        /// <param name="set"> If true - check, otherwise - uncheck </param>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent SetBankruptcyChapterCheckbox(string checkboxName, bool set = true)
        {
            DriverExtensions.SetCheckbox(set, SafeXpath.BySafeXpath(ChaptersCheckboxLocator, checkboxName));
            return this;
        }

        /// <summary>
        /// Checks plaintiff Checkbox for Party
        /// </summary>
        /// <param name="set"> If true - check, otherwise - uncheck </param>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent SetPartyPlaintiffCheckbox(bool set = true)
        {
            DriverExtensions.SetCheckbox(set, PartyPlaintiffCheckboxLocator);
            return this;
        }

        /// <summary>
        /// Clicks on the Preview Results Button
        /// </summary>
        /// <typeparam name="T">The type of the param</typeparam>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public T ClickPreviewResultsButton<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(PreviewResultsButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enters Party Name
        /// </summary>
        /// <param name="partyName"> party Name </param>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent EnterPartyName(string partyName)
        {
            DriverExtensions.SetTextField(partyName, SearchPartyInputLocator);
            return this;
        }

        /// <summary>
        /// Checks is Bankruptcy Chapter Checkbox Displayed.
        /// </summary>
        /// <param name="chaptersNumber"> chaptersNumber </param>
        /// <returns> True if Bankruptcy Chapter Checkbox present, false otherwise </returns>
        public bool IsBankruptcyChapterCheckboxDisplayed(string chaptersNumber) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(ChaptersNumberLocator, chaptersNumber), 5);

        /// <summary>
        /// Select 'Alert Me To All New Filings' RadioButton
        /// </summary>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent SelectAlertMeToAllNewFilingsRadioButton()
        {
            DriverExtensions.WaitForElementDisplayed(AlertMeToAllNewFilingsRadioButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// Select 'do not limit results' radio button
        /// </summary>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent SelectDoNotLimitResultsRadioButton()
        {
            DriverExtensions.WaitForElementDisplayed(DoNotLimitResultsRadioButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// Select 'limit results' radio button
        /// </summary>
        /// <returns> The <see cref="EnterSearchTermsComponent"/>. </returns>
        public EnterSearchTermsComponent SelectLimitResultsRadioButton()
        {
            DriverExtensions.WaitForElementDisplayed(LimitResultsRadioButtonLocator).Click();
            return this;
        }

        /// <summary>
        /// Click Select Nature Of Suite Link
        /// </summary>
        /// <returns> The <see cref="SelectNatureOfSuiteDialog"/>. </returns>
        public SelectNatureOfSuiteDialog ClickSelectNatureOfSuiteLink()
        {
            DriverExtensions.WaitForElement(SelectNatureOfSuiteLinkLocator).Click();
            return new SelectNatureOfSuiteDialog();
        }

        /// <summary>
        /// Verify that 'Smart Search' component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsSmartSearchComponentDisplayed()
            => DriverExtensions.IsDisplayed(SmartSearchComponentLocator, 5);

        /// <summary>
        /// Gets the Alert search terms
        /// </summary>
        /// <returns>Element text</returns>
        public string GetAlertSearchTerms() => DriverExtensions.GetText(AlertSearchTermsLocator);

        /// <summary>
        /// Get Search Query
        /// </summary>
        /// <returns> Search term </returns>
        public string GetSearchQuery() => DriverExtensions.GetText(SearchQueryLocator);

        /// <summary>
        /// Click Edit Link
        /// </summary>
        public EnterSearchTermsComponent ClickEditLink()
        {
            DriverExtensions.WaitForElement(EditLinkLocator).Click();
            return new EnterSearchTermsComponent();
        }
    }
}