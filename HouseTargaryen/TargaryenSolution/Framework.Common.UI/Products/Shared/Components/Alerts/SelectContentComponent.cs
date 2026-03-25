namespace Framework.Common.UI.Products.Shared.Components.Alerts
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Textboxes;
    using Framework.Common.UI.Products.Shared.Enums.Alerts;
    using Framework.Common.UI.Products.Shared.Pages.Alerts;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.DataModel;
    using Framework.Core.Utils.Enums;
    using OpenQA.Selenium;
    using TRGR.Quality.QedArsenal.QualityLibrary.Core.Enums.WebDriver;

    /// <summary>
    /// Select Content Section for Alert
    /// </summary>
    public class SelectContentComponent : BaseAlertComponent
    {
        private const string AddTextForContentLctMask =
            "div.co_genericBoxContent div:nth-child(3) div.co_column ul li:nth-child({0}) i";

        private const string BreadcrumbLinkLctMask = "nav.co_wizardStep_left_breadcrumb ol li a:nth-child({0})";

        private const string ClickableTitleForSpecificHeadingLctMask = "((//h4[text()='{0}']//ancestor::div)/following-sibling::div)[1]//a";

        private const string ContentLinkLctMask = "co_wizardStep_left_Home_{0}";

        private const string ContentTabLctMask = "//button[text() = '{0}'] | //a[text() = '{0}']";

        private const string NotClickableTitleForSpecificHeadingLctMask = "((//h4[text()='{0}']//ancestor::div)/following-sibling::div)[1]//span";

        private const string SelectedStateFederalDocketLctMask =
            "//li[contains(@id, 'StateFederalDockets' )]/i[@class = 'co_checked'][{0}]";

        private const string StateFederalDocketLctMask = "(//li[contains(@id, 'StateFederalDockets' )]/i)[{0}]";

        private const string StateLinkLctMask =
            "(//div[@class = 'co_genericBox']//li[contains(@id, 'StateFederalDockets' )]/*[last()])[{0}]";

        private const string YourContentItemLctMask = "(//li[./*[text()={0}]]/i ) | (//i[@role='checkbox'][@title={0}])";

        private static readonly By SelectedContentTextLocator = By.CssSelector("#contentSummaryDiv");

        private static readonly By ActiveContentTabItemNameLocator =
            By.CssSelector(".co_genericBoxContent .co_wizardStep_left_tab:not([style*='none']) i + *");

        private static readonly By ChicagoTributeOptionLocator =
            By.CssSelector("#co_wizardStep_left_Home_News_UnitedStatesNews_IllinoisNews_ChicagoTribune i");

        private static readonly By BreadCrumbContainerLocator = By.ClassName("co_wizardStep_left_breadcrumb");

        private static readonly By BreadCrumbCurrentNodeLocator = By.CssSelector("div.co_wizardStep_left_breadcrumb span");

        private static readonly By ContentAutoSuggestContainerLocator = By.Id("co_searchSuggestionAlerts");

        private static readonly By ContentCitingReferencesCheckboxLocator = By.Id("citing_references_checkbox");

        private static readonly By ContentHistoryReferencesCheckboxLocator = By.Id("history_references_checkbox");

        private static readonly By ContentItemLocator = By.CssSelector(".co_genericBoxContent li span,.co_genericBoxContent li a, div[class='Tab-panel Tab-panel--show'] li *");

        private static readonly By ContentSearchBoxLocator = By.XPath("//*[contains(@id,'co_search_widget')]");

        private static readonly By ContentTabCategoriesHeadingLocator = By.CssSelector("h4.co_browseHeading");

        private static readonly By ContentToAddAtYourSelectionsLocator = By.XPath("//ul[@id='co_searchSuggestionAlerts']/li[@tabindex='-1']");

        private static readonly By ContentWidgetLocator = By.CssSelector("div[id='contentWidgetDiv']");

        private static readonly By DocketNumberTextboxLocator = By.Id("co_docketNumber_input");

        private static readonly By FavoritesLinkLocator = By.XPath(".//button[contains(@aria-label, 'Favorites')]");

        private static readonly By FilterSummaryLocator = By.Id("otherFiltersSummaryOpen");

        private static readonly By NarrowByContentLinkLocator = By.Id("keycite_alerts_open_facets_lightbox");

        private static readonly By SelectedItemListLocator = By.XPath("//ul[@id='selectedItemsControlId']//li");

        private static readonly By ContentCheckBoxesListLocator = By.XPath("//*[@id='contentOptionsDiv']//input");

        private static readonly By CloseWarningMessageButtonLocator = By.XPath("//li[@id='contentSection']//a[@class='co_infoBox_closeButton']");

        private static readonly By ContentWarningMessageLocator = By.XPath("//li[@id='contentSection']//div[@class='co_infoBox_message']");

        private static readonly By EditLinkLocator = By.Id("coid_editLink_Content");

        private static readonly By AllStateAndFederalLabelLocator = By.XPath("//*[@class='selectedItemsControlId_jurisdiction']");

        private static readonly By YourSelectionsFilterItemLocator = By.XPath("//li[contains(@id, 'SelectedItems-Home')]");

        private static readonly By TrackProceedingNumberInputLocator = By.XPath("//input[contains(@id,'TrackInput')]");

        private static readonly By ContentHeaderLabelLocator = By.XPath("//h2[@id='contentBellowHeader']/strong");

        private static readonly By YourSelectionsLabelLocator = By.XPath("//div[@id='co_wizardStep_right']/h3");

        private static readonly By SearchInputTextBoxLocator = By.Id("searchInputIdAlerts");

        private EnumPropertyMapper<AlertsContentTab, BaseTextModel> alertsContentTabMap;

        /// <summary>
        ///  Select content header label
        /// </summary>
        public ILabel ContentHeaderLabel => new Label(ContentHeaderLabelLocator);

        /// <summary>
        ///  Your selections label
        /// </summary>
        public ILabel YourSelectionsLabel => new Label(YourSelectionsLabelLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContentWidgetLocator;

        /// <summary>
        /// Gets the AlertsContentTab enumeration to BaseTextModel map.
        /// </summary>
        protected EnumPropertyMapper<AlertsContentTab, BaseTextModel> AlertsContentTabMap
            =>
                this.alertsContentTabMap =
                    this.alertsContentTabMap ?? EnumPropertyModelCache.GetMap<AlertsContentTab, BaseTextModel>();

        /// <summary>
        /// Select an item in the auto suggest list given by index
        /// </summary>
        /// <param name="index"> The index to select </param>
        public void AddAutoSuggestItemByIndex(int index)
        {
            DriverExtensions.WaitForElement(ContentToAddAtYourSelectionsLocator);
            DriverExtensions.GetElements(ContentToAddAtYourSelectionsLocator).ElementAt(index).Click();
        }

        /// <summary>
        /// Select an item in the auto suggest list given by name
        /// </summary>
        /// <param name="name"> Item with name to select  </param>
        /// <returns> The <see cref="JurisdictionOptionsDialog"/>. </returns>
        public JurisdictionOptionsDialog AddAutoSuggestItemByName(string name)
        {
            DriverExtensions.WaitForElement(ContentToAddAtYourSelectionsLocator);
            DriverExtensions.GetElements(ContentToAddAtYourSelectionsLocator, By.TagName("label"))
                            .First(element => element.Text == name)
                            .Click();
            return new JurisdictionOptionsDialog();
        }

        /// <summary>
        /// Clicks on Add State for the specific index
        /// </summary>
        /// <param name="indexToAdd"> index of state to add </param>
        public void AddState(int indexToAdd) => DriverExtensions.GetElement(By.XPath(string.Format(StateFederalDocketLctMask, indexToAdd))).Click();

        /// <summary>
        /// Checks if a content exists on the current tab
        /// </summary>
        /// <param name="contentName"> The name of the content to check the link of </param>
        /// <returns> True if content category is visible, false otherwise </returns>
        public bool CheckIfContentCategoryIsVisible(string contentName)
            =>
                DriverExtensions.GetElementsByText(
                    contentName,
                    new TextSearchOption[0],
                    this.ComponentLocator,
                    ContentItemLocator).Any(element => element.Displayed);

        /// <summary>
        /// Clears the search text and escapes out of the auto suggest box
        /// </summary>
        public void ClearSearchAndAutoSuggest() => DriverExtensions.GetElement(ContentSearchBoxLocator).Clear();

        /// <summary>
        /// Clicks add next to a given Content item
        /// </summary>
        /// <param name="contentName"> The name of the content to add </param>
        /// <returns> The <see cref="SelectContentComponent"/>. </returns>
        public SelectContentComponent ClickAddContentCategory(string contentName) => this.ClickOnContentCategoryByTag(contentName, "i");

        /// <summary>
        /// Clicks the specified breadcrumb link by index.
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <param name="index"> The index. </param>
        /// <returns> page object </returns>
        public T ClickBreadcrumbByIndex<T>(int index) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(By.CssSelector(string.Format(BreadcrumbLinkLctMask, index))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks a given Content link
        /// </summary>
        /// <param name="contentName"> The name of the content to click the link of </param>
        /// <returns> The <see cref="SelectContentComponent"/>. </returns>
        public SelectContentComponent ClickContentCategory(string contentName) => this.ClickOnContentCategoryByTag(contentName, "a");

        /// <summary>
        /// Clicks a given Content tab
        /// </summary>
        /// <param name="tab"> Tab to click </param>
        /// <returns> The <see cref="SelectContentComponent"/>. </returns>
        public SelectContentComponent ClickContentTab(AlertsContentTab tab)
        {
            DriverExtensions.Click(By.XPath(string.Format(ContentTabLctMask, this.AlertsContentTabMap[tab].Text)));
            return this;
        }

        /// <summary>
        /// Click on the Favorites link
        /// </summary>
        public void ClickFavoritesLink() => DriverExtensions.Click(ComponentLocator, FavoritesLinkLocator);

        /// <summary>
        /// Click on the Narrow By content link
        /// </summary>
        /// <returns> The <see cref="NarrowByContentDialog"/>. </returns>
        public NarrowByContentDialog ClickNarrowByContentLink()
        {
            DriverExtensions.Click(NarrowByContentLinkLocator);
            return new NarrowByContentDialog();
        }

        /// <summary>
        /// Enter text into the search content textbox
        /// </summary>
        /// <param name="queryText"> Query </param>
        public void EnterContentSearch(string queryText) => DriverExtensions.SetTextField(queryText, ContentSearchBoxLocator);

        /// <summary>
        /// Enters the docket number
        /// Specific for Docket Track alert type
        /// </summary>
        /// <param name="docketNumber"> Docket Number </param>
        /// <returns> The <see cref="SelectContentComponent"/>. </returns>
        public SelectContentComponent EnterDocketNumber(string docketNumber)
        {
            DriverExtensions.GetElement(DocketNumberTextboxLocator).SetTextField(docketNumber);
            return this;
        }

        /// <summary>
        /// Gets the names of content items in the current tab selected in the Content bellow
        /// </summary>
        /// <returns>Names of content items</returns>
        public List<string> GetActiveContentTabItemNames() => DriverExtensions.GetElements(ActiveContentTabItemNameLocator).Select(e => e.Text).ToList();

        /// <summary>
        /// Get list of clickable titles for specific heading
        /// </summary>
        /// <param name="heading"> The Heading </param>
        /// <returns> Names of clickable titles </returns>
        public List<string> GetClickableTitlesListForSpecificHeading(string heading) => DriverExtensions
            .GetElements(By.XPath(string.Format(ClickableTitleForSpecificHeadingLctMask, heading)))
            .Select(el => el.Text).ToList();

        /// <summary>
        /// Gets the breadcrumb text for the cases
        /// </summary>
        /// <returns> Breadcrumb Node text </returns>
        public string GetContentBreadcrumbText() => DriverExtensions.GetText(BreadCrumbContainerLocator);

        /// <summary>
        /// Get all names of Content Tab Categories Headings
        /// </summary>
        /// <returns>List with all names of headings</returns>
        public List<string> GetContentTabCategoriesHeading() => DriverExtensions
            .GetElements(ContentTabCategoriesHeadingLocator).Select(x => x.Text).ToList();

        /// <summary>
        /// Get the current breadcrumb node
        /// </summary>
        /// <returns> Current Breadcrumb Node text </returns>
        public string GetCurrentContentBreadcrumbNodeText() => DriverExtensions.GetText(BreadCrumbCurrentNodeLocator);

        /// <summary>
        /// Get list of not clickable titles for specific heading
        /// </summary>
        /// <param name="heading"> The Heading </param>
        /// <returns> Names of not clickable titles</returns>
        public List<string> GetNotClickableTitlesListForSpecificHeading(string heading) => DriverExtensions
            .GetElements(By.XPath(string.Format(NotClickableTitleForSpecificHeadingLctMask, heading)))
            .Select(el => el.Text).ToList();

        /// <summary>
        /// Get all the auto-suggest text given when you type into the search box
        /// </summary>
        /// <returns> All the auto-suggest text </returns>
        public string GetSearchAutoSuggestText() => DriverExtensions.WaitForElementDisplayed(ContentAutoSuggestContainerLocator).Text;

        /// <summary>
        /// Gets the number of selected items in the sidebar
        /// </summary>
        /// <returns>The number of selected items</returns>
        public int GetSelectedItemCount() => DriverExtensions.GetElements(SelectedItemListLocator).Count;

        /// <summary>
        /// Gets the number of selected items in the sidebar
        /// </summary>
        /// <returns>The number of selected items</returns>
        public List<string> GetSelectedItemNames() => DriverExtensions.GetElements(SelectedItemListLocator).Select(element => element.Text).ToList();

        /// <summary>
        /// Gets the state name of the index
        /// </summary>
        /// <param name="index"> Index </param>
        /// <returns> State Name </returns>
        public string GetStateName(int index) => DriverExtensions.GetText(By.XPath(string.Format(StateLinkLctMask, index))).Trim();

        /// <summary>
        /// Checks if the Chicago tribute link is selected
        /// If content is selected text link will be 'Remove' otherwise text will be 'Add'
        /// </summary>
        /// <returns> True if Chicago tribute link is selected, false otherwise</returns>
        public bool IsChicagoTributeOptionSelected() => DriverExtensions.GetText(ChicagoTributeOptionLocator).Contains("Remove");

        /// <summary>
        /// Checks if a specific state has Add text 
        /// </summary>
        /// <param name="indexToAdd"> index to check add for </param>
        /// <returns> True if the state add text is shown, false otherwise </returns>
        public bool IsAddStateTextByIndexDisplayed(int indexToAdd) => DriverExtensions.GetText(By.CssSelector(string.Format(AddTextForContentLctMask, indexToAdd)))
                                                                                      .ToUpper()
                                                                                      .Contains("ADD");

        /// <summary>
        /// Checks if a specific state has Add text by name. must have proper capitalization.
        /// </summary>
        /// <param name="stateName"> state to check add for </param>
        /// <returns> True If the state add text is shown, false otherwise </returns>
        public bool IsAddTextByNameDisplayed(string stateName)
            => DriverExtensions.WaitForElement(By.Id(string.Format(ContentLinkLctMask, stateName.Replace(" ", string.Empty))))
                                .Text.Contains("Add");

        /// <summary>
        /// Checks if the auto suggest item has a green checkbox next to it.
        /// </summary>
        /// <param name="index"> Element index </param>
        /// <returns> True if element is selected, false otherwise </returns>
        public bool IsAutoSuggestItemByIndexSelected(int index)
        {
            DriverExtensions.WaitForElement(ContentToAddAtYourSelectionsLocator);
            return
                DriverExtensions.GetElements(ContentToAddAtYourSelectionsLocator)
                                .ElementAt(index)
                                .GetAttribute("class")
                                .Contains("co_selected");
        }

        /// <summary>
        /// Checks if a content add link exists on the current tab
        /// </summary>
        /// <param name="contentName"> The name of the content to check the link of </param>
        /// <returns> True if the content category add link is displayed, false otherwise </returns>
        public bool IsContentCategoryAddLinkDisplayed(string contentName) => this.IsContentCategoryLinkDisplayedByTag(contentName, "i");

        /// <summary>
        /// Checks if a content link exists on the current tab
        /// </summary>
        /// <param name="contentName"> The name of the content to check the link of </param>
        /// <returns> True if the content category link is displayed, false otherwise </returns>
        public bool IsContentCategoryLinkDisplayed(string contentName) => this.IsContentCategoryLinkDisplayedByTag(contentName, "a");

        /// <summary>
        /// Checks if a content component is opened </summary>
        /// <returns>True if the select content component is open</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// Add X states in the Select window and return a confirmation of whether they were added or not
        /// </summary>
        /// <param name="numStates"> Number of states to add </param>
        /// <returns> True if the states were all added, false otherwise </returns>
        public bool IsStatesAdded(int numStates)
        {
            bool greenCheck = true;

            for (int x = 1; x <= numStates; x++)
            {
                // Make sure to test this a different way
                bool stateFound =
                    DriverExtensions.GetElement(By.XPath(string.Format(StateFederalDocketLctMask, x))).Displayed;
                if (stateFound)
                {
                    DriverExtensions.GetElement(By.XPath(string.Format(StateFederalDocketLctMask, x))).Click();
                }
                else if (
                    !DriverExtensions.GetElement(By.XPath(string.Format(SelectedStateFederalDocketLctMask, x)))
                                     .Displayed)
                {
                    greenCheck = false;
                }
            }

            return greenCheck;
        }

        /// <summary>
        /// Removes a specific selected item by clicking it
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The <see cref="SelectContentComponent"/>.
        /// </returns>
        public SelectContentComponent RemoveSelectedItem(int index)
        {
            DriverExtensions.GetElements(SelectedItemListLocator).ElementAt(index).Click();
            return new SelectContentComponent();
        }

        /// <summary>
        /// Click Add button for the specified item in Your Content
        /// </summary>
        /// <param name="contentToSelect"> Item to add </param>
        /// <returns> The <see cref="SelectContentComponent"/>. </returns>
        public SelectContentComponent SelectYourContentItem(string contentToSelect)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(YourContentItemLctMask, contentToSelect)).Click();
            return new SelectContentComponent();
        }

        /// <summary>
        /// Select/Unselect 'Citing References' checkbox
        /// </summary>
        /// <param name="setTo"> True to select, false to unselect </param>
        public void SetContentCitingReferencesCheckbox(bool setTo) => DriverExtensions.SetCheckbox(setTo, ContentCitingReferencesCheckboxLocator);

        /// <summary>
        /// Select/Unselect 'History References' checkbox
        /// </summary>
        /// <param name="setTo"> True to select, false to unselect </param>
        public void SetContentHistoryReferencesCheckbox(bool setTo) => DriverExtensions.SetCheckbox(setTo, ContentHistoryReferencesCheckboxLocator);

        /// <summary>
        /// Waits for the filter summary section text to appear
        /// </summary>
        /// <param name="query"> The query. </param>
        public void WaitForFilterSummaryText(string query) => DriverExtensions.WaitForTextInElement(query, FilterSummaryLocator);

        /// <summary>
        /// Gets the label text of selected content in a folded/compact according view
        /// </summary>
        /// <returns>List of label text</returns>
        public string GetSummarySelectedContentText() => DriverExtensions.GetText(SelectedContentTextLocator);

        /// <summary>
        /// Select checkbox with content (for Company Investigator alert)
        /// </summary>
        /// <param name="contentToSelect"> Content to select </param>
        public void SelectCheckboxWithContent(string contentToSelect) =>
            DriverExtensions.GetElements(ContentCheckBoxesListLocator).Where(e => e.GetAttribute("displayname") == contentToSelect).ToList().ForEach(e => e.Click());

        /// <summary>
        /// Is checkbox selected
        /// </summary>
        /// <param name="contentToSelect"> The content to select. </param>
        /// <returns> True if checkbox is selected </returns>
        public bool IsCheckboxWithContentSelected(string contentToSelect) =>
            DriverExtensions.GetElements(ContentCheckBoxesListLocator).First(e => e.GetAttribute("displayname") == contentToSelect).Selected;

        /// <summary>
        /// Close the warning message
        /// </summary>
        public void CloseWarningMessage() => DriverExtensions.GetElement(CloseWarningMessageButtonLocator).Click();

        /// <summary>
        /// Get text from the Warning message
        /// </summary>
        /// <returns> Warning message </returns>
        public string GetWarningMessageText() => DriverExtensions.GetText(ContentWarningMessageLocator);

        /// <summary>
        /// Verify that Warning Message is displayed for Content section
        /// </summary>
        /// <returns> True if warning message is displayed, false otherwise </returns>
        public bool IsWarningMessageDisplayed() => DriverExtensions.IsDisplayed(ContentWarningMessageLocator);

        /// <summary>
        /// Click edit link
        /// </summary>
        /// <returns> The <see cref="CreateAlertPage"/>. </returns>
        public CreateAlertPage ClickEditLink()
        {
            DriverExtensions.WaitForElement(EditLinkLocator).Click();
            return new CreateAlertPage();
        }

        /// <summary>
        /// Verify that 'All State and Federal' item in 'Your Selection' component is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsAllStateAndFederalItemDisplayed() => DriverExtensions.IsDisplayed(AllStateAndFederalLabelLocator);

        /// <summary>
        /// Get count of 'Your Selection' items
        /// </summary>
        /// <returns> Count of items </returns>
        public int GetYourSelectionFilterItemsCount()
            => DriverExtensions.GetElements(YourSelectionsFilterItemLocator).Count;

        /// <summary>
        /// Proceeding number textbox
        /// </summary>
        public ITextbox TrackProceedingNumberTextbox => new Textbox(TrackProceedingNumberInputLocator);

        private SelectContentComponent ClickOnContentCategoryByTag(string contentName, string tag)
        {
            foreach (
                IWebElement contentLinkElment in
                DriverExtensions.GetElementsByText(
                    contentName,
                    new TextSearchOption[5],
                    this.ComponentLocator,
                    ContentItemLocator))
            {
                if (contentLinkElment.IsDisplayed())
                {
                    IWebElement linkParent = contentLinkElment.GetParentElement();
                    DriverExtensions.Click(linkParent, By.TagName(tag));
                    break;
                }
            }

            return this;
        }

        private bool IsContentCategoryLinkDisplayedByTag(string contentName, string tagName)
        {
            List<IWebElement> elementsList = DriverExtensions.GetElementsByText(
                contentName,
                new TextSearchOption[5],
                this.ComponentLocator,
                ContentItemLocator);
            return
                elementsList.Where(element => element.Displayed)
                            .Any(
                                elem =>
                                    DriverExtensions.IsDisplayed(
                                        elem.GetParentElement(),
                                        By.TagName(tagName)));
        }

        /// <summary>
        /// Enters Search Text
        /// </summary>
        /// <param name="searchText">
        /// Search text 
        /// </param>
        /// <returns>
        /// The <see cref="EnterSearchTermsComponent"/>.
        /// </returns>
        public EnterSearchTermsComponent EnterTextField(string searchText)
        {
            DriverExtensions.WaitForElementDisplayed(SearchInputTextBoxLocator).SetTextField(searchText);
            return new EnterSearchTermsComponent();
        }
    }
}