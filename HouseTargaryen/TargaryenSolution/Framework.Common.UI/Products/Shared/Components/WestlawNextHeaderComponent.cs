namespace Framework.Common.UI.Products.Shared.Components
{
    using System.Collections.Generic;
    using System.Linq;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Products.Shared.Dialogs;
    using Framework.Common.UI.Products.Shared.Dialogs.Alerts;
    using Framework.Common.UI.Products.Shared.Dialogs.HomePage;
    using Framework.Common.UI.Products.Shared.Dialogs.ResearchRecommendations;
    using Framework.Common.UI.Products.Shared.Enums.HomePage;
    using Framework.Common.UI.Products.Shared.Models.EnumProperties;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.RecentSearches;
    using Framework.Common.UI.Products.WestLawNext.Components.Header;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.DropDowns;
    using Framework.Common.UI.Utils.Core;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Enums;
    using System.Threading;
    using OpenQA.Selenium;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;

    /// <summary>
    /// WestlawNextHeaderComponent contains actions for interacting with the initial header
    /// that's available on the search home page and on the other WLN pages and contains the following components:
    /// - Logo
    /// - Header Link
    /// - Search Box
    /// - Jurisdiction Selector
    /// </summary>
    public class WestlawNextHeaderComponent : BaseModuleRegressionComponent, IWestlawNextHeaderComponent
    {
        private static readonly By WestlawNextLogoLocator = By.Id("coid_website_logo");

        private static readonly By CompartmentArrowLocator = By.XPath("//button[@aria-describedby = 'co_compartmentDropdownAnnouncment']");

        private static readonly By HeaderLabelLocator = By.Id("headerLogo");

        private static readonly By HeaderImageLocator = By.CssSelector("#coid_website_logo img");

        private static readonly By ParallelSearchLinkLocator = By.Id("co_search_parallelSearchLink");

        private static readonly By AdvancedSearchLinkLocator = By.Id("co_search_advancedSearchLink");

        private static readonly By SearchTipsLinkLocator = By.Id("co_search_searchTipsLink");

        private static readonly By SearchInputErrorMessageLocator = By.Id("co_searchInputErrorMessageContainer");

        private static readonly By BodyElementLocator = By.TagName("body");

        private static readonly By InfoMessageLocator = By.XPath("(//div[@class='co_infoBox_message' and not(contains(@id, 'coid_raButtonInfoBoxMessage'))])[last()]");

        private static readonly By SnippetInfoMessageLocator = By.XPath("//div[contains(@class, 'co_infoBox success')]//div[@class='co_infoBox_message']  | //div[contains(@id,'co_searchInputErrorMessageContainer')]//div[@class='co_infoBox_message']");

        private static readonly By KmFirmNameLocator = By.Id("co_kmFirmNameContainer");

        private static readonly By PoweredByWestSearchLocator = By.Id("co_poweredBySearch");

        private static readonly By RaNotificationGeneratedIconLocator = By.XPath("//div[@class='co_ra_recommendationsAvailable']");

        private static readonly By RecentSearchButtonLocator = By.Id("co_searchLast10Link");

        private static readonly By ResearchRecomendationsButtonLocator = By.Id("coid_website_researchAccelerator");

        private static readonly By ContentTypeDialogButtonLocator = By.Id("co_categorySearchButton");

        private static readonly By SelectedCategoryDropdownLocator = By.Id("co_currentSelectedCategoryText");

        private static readonly By ContainerLocator = By.Id("co_headerContainer");

        private static readonly By JurisdictionButtonLocator = By.Id("jurisdictionId");

        private static readonly By SearchButtonLocator = By.Id("searchButton");

        private static readonly By SearchInputLocator = By.Id("searchInputId");

        private static readonly By SearchInputLocatorforLitigation = By.Id("la_searchInputId");

        private static readonly By SpinnerLocator = By.ClassName("co_search_ajaxLoading");

        private static readonly By RrMessageLocator = By.XPath("//*[@id='coid_raButtonInfoBoxMessage']");

        private static readonly By BooleanTermsAndConnectors = By.XPath("//li[@id='tab_BooleanTermsConnectors']");

        private static readonly By ClientIDLocator = By.XPath("//*[@id='clientidr'] | //button[contains(@id,'co_clientID_recent')]");

        private static readonly By WestClipAlertIconLocator = By.Id("co_search_alertMenuLink");

        private EnumPropertyMapper<GlobalNavigationLink, WebElementInfo> globalNavigationLinkMap;

        /// <summary>
        /// Compartment drop down
        /// </summary>
        public CompartmentDropdown CompartmentDropdown { get; } = new CompartmentDropdown();

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        private EnumPropertyMapper<GlobalNavigationLink, WebElementInfo> GlobalNavigationLinkMap
            => this.globalNavigationLinkMap = this.globalNavigationLinkMap ?? EnumPropertyModelCache.GetMap<GlobalNavigationLink, WebElementInfo>();

        /// <summary>
        /// Parallel Search Link
        /// </summary>
        public ILink ParallelSearchLink => new Link(this.ComponentLocator, ParallelSearchLinkLocator);

        /// <summary>
        /// IsGlobalNavigationLinkDisplayed
        /// </summary>
        /// <param name="link">Link</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsGlobalNavigationLinkDisplayed(GlobalNavigationLink link)
            => DriverExtensions.IsDisplayed(By.XPath(this.GlobalNavigationLinkMap[link].LocatorString));

        /// <summary>
        /// Click on Global Navigation Link
        /// This method can be only used in NavigationManager
        /// </summary>
        /// <param name="link">Link</param>
        public void ClickGlobalNavigationLink(GlobalNavigationLink link)
        {
            DriverExtensions.WaitForAnimation();
            DriverExtensions.WaitForElement(By.XPath(this.GlobalNavigationLinkMap[link].LocatorString)).Click();
        }

        /// <summary>
        /// Open ChangeClientIdDialog
        /// </summary>
        /// <returns>The <see cref="ChangeClientIdDialog"/>.</returns>
        public CommunityDialog OpenCommunityDialog()
            => this.ClickGlobalNavigationLink<CommunityDialog>(GlobalNavigationLink.CommunityArrow);

        /// <summary>
        /// Open ChangeClientIdDialog
        /// </summary>
        /// <returns>The <see cref="ChangeClientIdDialog"/>.</returns>
        public ChangeClientIdDialog OpenChangeClientIdDialog()
            => this.ClickGlobalNavigationLink<ChangeClientIdDialog>(GlobalNavigationLink.ClientId);

        /// <summary>
        /// Open ChangeClientIdDialog
        /// </summary>
        /// <returns>The <see cref="ChangeClientIdDialog"/>.</returns>
        public RecentFoldersDialog OpenRecentFoldersDialog()
            => this.ClickGlobalNavigationLink<RecentFoldersDialog>(GlobalNavigationLink.FoldersArrow);

        /// <summary>
        /// Open ChangeClientIdDialog
        /// </summary>
        /// <returns>The <see cref="RecentHistoryDialog"/>.</returns>
        public RecentHistoryDialog OpenRecentHistoryDialog()
            => this.ClickGlobalNavigationLink<RecentHistoryDialog>(GlobalNavigationLink.HistoryArrow);

        /// <summary>
        /// Open OpenSessionPauseDialog
        /// </summary>
        /// <returns>The <see cref="SessionPauseDialog"/>.</returns>
        public SessionPauseDialog OpenSessionPauseDialog()
            => this.ClickGlobalNavigationLink<SessionPauseDialog>(GlobalNavigationLink.Pause);

        /// <summary>
        /// Open OpenSessionPauseDialog
        /// </summary>
        /// <returns>The <see cref="ProfileSettingsDialog"/>.</returns>
        public ProfileSettingsDialog OpenProfileSettingsDialog()
            => this.ClickGlobalNavigationLink<ProfileSettingsDialog>(GlobalNavigationLink.ProfileSettings);

        /// <summary>
        /// GetClientIdText
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetClientIdText()
            => DriverExtensions.WaitForElement(By.XPath(this.GlobalNavigationLinkMap[GlobalNavigationLink.ClientId].LocatorString)).GetText();

        /// <summary>
        /// HoverOverGlobalNavigationLink
        /// </summary>
        public void HoverOverGlobalNavigationLink()
            => DriverExtensions.WaitForElement(By.XPath(this.GlobalNavigationLinkMap[GlobalNavigationLink.Community].LocatorString)).Hover();

        /// <summary>
        /// Clicks the WLN logo in the header to return to the Homepage
        /// </summary>
        /// <typeparam name="T">Page Object</typeparam>
        /// <returns>SearchHomePage object</returns>
        public T ClickLogo<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(WestlawNextLogoLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Get Logo text.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetLogoText() => DriverExtensions.GetAttribute("data-compartment-start-page-id", HeaderLabelLocator);

        /// <summary>
        /// Get Logo title.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetLogoImageTitle() => DriverExtensions.GetAttribute("alt", HeaderImageLocator);

        /// <summary>
        /// Get Header Logo Text 
        /// </summary>
        /// <returns></returns>
        public string GetHeaderLogoText() => DriverExtensions.WaitForElement(WestlawNextLogoLocator).Text.Trim();

        /// <summary>
        /// Hover over Logo link.
        /// </summary>
        public void HoverOverLogoLink() => DriverExtensions.Hover(WestlawNextLogoLocator);

        /// <summary>
        /// Clicks the advanced search link
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// A new instance of the advanced search page object
        /// </returns>
        public T ClickAdvancedSearchLink<T>() where T : IAdvancedSearchPage
        {
            DriverExtensions.Click(AdvancedSearchLinkLocator);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is advanced link displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAdvancedLinkDisplayed() => DriverExtensions.IsDisplayed(AdvancedSearchLinkLocator, 5);

        /// <summary>
        /// Clicks search tips link
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// A new instance of type T
        /// </returns>
        public T ClickSearchTipsLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(SearchTipsLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Is search tips link displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchTipsLinkDisplayed() => DriverExtensions.IsDisplayed(SearchTipsLinkLocator, 5);

        /// <summary>
        /// Click Research Recommendations Button
        /// </summary>
        /// <returns>New instance of Research Recommendations Slider Dialog</returns>
        public RrSliderDialog ClickResearchRecomendationsButton()
        {
            //Work Around
            Thread.Sleep(5000);
            DriverExtensions.WaitForElement(ResearchRecomendationsButtonLocator).Click();
            return new RrSliderDialog();
        }

        /// <summary>
        /// Clicks the search button
        /// </summary>
        /// <typeparam name="T">
        /// Object
        /// </typeparam>
        /// <returns>
        /// A new instance of the search results page
        /// </returns>
        public T ClickSearchButton<T>() where T : ICreatablePageObject
        {
            this.ClickSearchButton();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks the search button
        /// </summary>       
        public void ClickSearchButton()
        {
            DriverExtensions.Click(DriverExtensions.WaitForElement(SearchButtonLocator));
            DriverExtensions.WaitForCondition(condition => DriverExtensions.SafeGetElement(SpinnerLocator) == null, 120);
        }

        /// <summary>
        /// Gets the error text
        /// </summary>
        /// <returns>The error text</returns>
        public string GetSearchErrorMessage() => DriverExtensions.GetText(SearchInputErrorMessageLocator).Trim();

        /// <summary>
        /// Determines if a search error is present
        /// </summary>
        /// <returns>True if its present, false otherwise</returns>
        public bool IsSearchErrorMessageDisplayed() => DriverExtensions.IsDisplayed(SearchInputErrorMessageLocator);

        /// <summary>
        /// Determines if client id is present
        /// </summary>
        /// <returns>True if its present, false otherwise</returns>
        public bool IsClientIdDisplayed() => DriverExtensions.IsDisplayed(By.XPath(this.GlobalNavigationLinkMap[GlobalNavigationLink.ClientId].LocatorString));

        /// <summary>
        /// Enter a search query and click the search button
        /// </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="searchText">search query to enter</param>
        /// <param name="wait">for sending the slow text</param>
        /// <returns>SearchResultPage object</returns>
        public T EnterSearchQueryAndClickSearch<T>(string searchText, bool wait = false) where T : ICreatablePageObject
        {
            // for headnote search normal send keys is not working,so using the slowsendkeys
            if (wait)
            {
                this.EnterSearchQuery(searchText, true, sendSlow: true);
            }
            else
            {
                this.EnterSearchQuery(searchText, true, false);
            }
            return this.ClickSearchButton<T>();
        }

        /// <summary>
        /// Enter a search query and click the search button for Litigation
        /// </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="searchText">search query to enter</param>
        /// <param name="wait">for sending the slow text</param>
        /// <returns>SearchResultPage object</returns>
        public T EnterSearchQueryAndClickSearchforLitigation<T>(string searchText, bool wait = false) where T : ICreatablePageObject
        {
            // for headnote search normal send keys is not working,so using the slowsendkeys
            if (wait)
            {
                this.EnterSearchQueryforLitigation(searchText, true, sendSlow: true);
            }
            else
            {
                this.EnterSearchQueryforLitigation(searchText, true, false);
            }
            return this.ClickSearchButton<T>();
        }

        /// <summary> Enter a search query and click the search button </summary>
        /// <typeparam name="T"> Page Object </typeparam>
        /// <param name="searchItems"> search item(s) to enter </param>
        /// <returns> SearchResultPage object </returns>
        public T EnterSearchQueryAndClickSearch<T>(params string[] searchItems) where T : ICreatablePageObject
            => this.EnterSearchQueryAndClickSearch<T>(string.Join(";", searchItems));

        /// <summary>
        /// Enter a search query
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="sendSlow">The send Slow.</param>
        /// <param name="clearFirst">The clear First.</param>
        /// <returns>The new instance of T page</returns>
        public T EnterSearchQuery<T>(string query, bool sendSlow = false, bool clearFirst = true)
            where T : BaseModuleRegressionDialog
        {
            this.EnterSearchQuery(query, clearFirst, sendSlow);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Enter a search query for Litigation
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="query">The query.</param>
        /// <param name="sendSlow">The send Slow.</param>
        /// <param name="clearFirst">The clear First.</param>
        /// <returns>The new instance of T page</returns>
        public T EnterSearchQueryforLitigation<T>(string query, bool sendSlow = false, bool clearFirst = true)
            where T : BaseModuleRegressionDialog
        {
            this.EnterSearchQueryforLitigation(query, clearFirst, sendSlow);
            return DriverExtensions.CreatePageInstance<T>();
        }
        /// <summary>
        /// Enter a search query for Litigation
        /// </summary>
        /// <param name="text">Text to enter</param> 
        /// <param name="clearFirst">True to clear the text field first, false otherwise</param>
        /// <param name="sendSlow">The send Slow.</param>
        /// <param name="wait">The send Slow.</param>
        public void EnterSearchQueryforLitigation(string text, bool clearFirst = true, bool sendSlow = true, bool wait = false)
        {

            if (clearFirst)
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocatorforLitigation).Clear();
            }
            if (wait)
            {
                DriverExtensions.WaitForAnimation();
                DriverExtensions.WaitForElementDisplayed(SearchInputLocatorforLitigation).Click();
            }
            if (sendSlow)
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocatorforLitigation).SendKeysSlow(text);
            }
            else
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocatorforLitigation).SendKeys(text);

            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Enter a search query
        /// </summary>
        /// <param name="text">Text to enter</param> 
        /// <param name="clearFirst">True to clear the text field first, false otherwise</param>
        /// <param name="sendSlow">The send Slow.</param>
        /// <param name="wait">The send Slow.</param>
        public void EnterSearchQuery(string text, bool clearFirst = true, bool sendSlow = true, bool wait = false)
        {

            if (clearFirst)
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocator).Clear();
            }
            if (wait)
            {
                DriverExtensions.WaitForAnimation();
                DriverExtensions.WaitForElementDisplayed(SearchInputLocator).Click();
            }
            if (sendSlow)
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocator).SendKeysSlow(text);
            }
            else
            {
                DriverExtensions.WaitForElementDisplayed(SearchInputLocator).SendKeys(text);

            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Clear search input and change focus.
        /// </summary>
        public void ClearSearchAndChangeFocus()
        {
            this.EnterSearchQuery(string.Empty, wait: true);
            DriverExtensions.WaitForElement(BodyElementLocator).Click();
            DriverExtensions.WaitForAnimation();
        }

        /// <summary>
        /// The expand collapse category dropdown.
        /// </summary>
        /// <returns>The <see cref="ContentTypeDialog"/>.</returns>
        public ContentTypeDialog OpenContentTypeDialog()
        {
            DriverExtensions.WaitForElement(ContentTypeDialogButtonLocator).Click();
            return new ContentTypeDialog();
        }

        /// <summary>
        /// returns the common confirmation/ failure message displayed on Search results Page, history page,folder page, document page
        ///     for any action performed on the pages.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        ///  <param name="wait">For snippet locator has changed.</param>
        ///  <param name="timeOut">for time out.</param>
        /// </returns>
        public string GetInfoMessage(int timeOut = WebDriverConstants.DefaultTimeoutInMilliseconds, bool wait = false)
        {
            IWebElement messageElement;
            if (wait)
            {
                //unable to find the sucess message in SearchLinkWidgetSearchTestfoldering changed the locator
                DriverExtensions.WaitForElementDisplayed(SnippetInfoMessageLocator, timeOut);
                messageElement = DriverExtensions.GetElements(SnippetInfoMessageLocator).FirstOrDefault(el => el.Displayed);
            }
            else
            {
                //WaitForElementDisplayed is not working
                DriverExtensions.WaitForElement(InfoMessageLocator, timeOut);
                messageElement = DriverExtensions.GetElements(InfoMessageLocator).FirstOrDefault(el => el.Displayed);
            }
            return messageElement.GetText();
        }



        /// <summary>
        /// Gets the current query typed in the search bar
        /// </summary>
        /// <returns>Current query</returns>
        public string GetCurrentQuery()
            => DriverExtensions.WaitForElementDisplayed(SearchInputLocator).GetAttribute("value");

        /// <summary>
        /// Get list of recent folder
        /// </summary>
        /// <returns>The List of strings</returns>
        public List<string> GetRecentFolders()
        {
            RecentFoldersDialog recentFolderDialog = this.OpenRecentFoldersDialog();
            List<string> recentFolders = recentFolderDialog.GetFoldersNames().ToList();
            recentFolderDialog.ClickCloseButton();
            return recentFolders;
        }

        /// <summary>
        /// Gets Jurisdiction Link Text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetJurisdictionButtonText() => DriverExtensions.GetText(JurisdictionButtonLocator);

        /// <summary>
        /// Gets Jurisdiction Current Selection Text.
        /// </summary>
        /// <returns>The <see cref="JurisdictionToolTipDialog"/>.</returns>
        public JurisdictionToolTipDialog OpenJurisdictionToolTip()
        {
            DriverExtensions.Hover(JurisdictionButtonLocator);
            return new JurisdictionToolTipDialog();
        }

        /// <summary>
        /// Checks if the advance search button displayed
        /// </summary>
        /// <returns>If the  search button displayed</returns>
        public bool IsAdvanceSearchLinkDisplayed() => DriverExtensions.IsDisplayed(AdvancedSearchLinkLocator);

        /// <summary>
        /// The is westlaw header displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// IsWestlawLogoDisplayed
        /// </summary>
        /// <returns> true  if logo is displayed otherwise false</returns>
        public bool IsWestlawLogoDisplayed() => DriverExtensions.IsDisplayed(WestlawNextLogoLocator, 5);

        /// <summary>
        /// IsCompartmentArrowDisplayed
        /// </summary>
        /// <returns> true  if arrow is displayed otherwise false</returns>
        public bool IsCompartmentArrowDisplayed() => DriverExtensions.IsDisplayed(CompartmentArrowLocator, 3);

        /// <summary>
        /// IsKmFirmNamePresent
        /// </summary>
        /// <returns> true  if KmFirmName container is present otherwise false</returns>
        public bool IsKmFirmNamePresent() => DriverExtensions.IsElementPresent(KmFirmNameLocator);

        /// <summary>
        /// Is powered by west search present
        /// </summary>
        /// <returns>true if PoweredByWestSearch is present</returns>
        public bool IsPoweredByWestSearchPresent() => DriverExtensions.IsElementPresent(PoweredByWestSearchLocator);

        /// <summary>
        /// Verify Recommendations generated icon is displayed
        /// </summary>
        /// <returns>true if Recommendations generated icon is displayed, false otherwise</returns>
        public bool IsRaGeneratedIconDisplayed() => DriverExtensions.IsDisplayed(RaNotificationGeneratedIconLocator, 30);

        /// <summary>
        /// Verify Research Recommendations icon is displayed
        /// </summary>
        /// <returns>true if Research Recommendations icon is displayed, false otherwise</returns>
        public bool IsResearchReccomendationsIconDisplayed()
            => DriverExtensions.IsDisplayed(ResearchRecomendationsButtonLocator, 5);

        /// <summary>
        /// Checks if the search button displayed
        /// </summary>
        /// <returns>If the  search button displayed</returns>
        public bool IsSearchButtonDisplayed() => DriverExtensions.IsDisplayed(SearchButtonLocator);

        /// <summary>
        /// Is search Text Field  exist.
        /// </summary>
        /// <returns> True if element exists, false otherwise </returns>
        public bool IsSearchInputDisplayed() => DriverExtensions.IsDisplayed(SearchInputLocator);

        /// <summary>
        /// Checks if the Jurisdiction dropdown exists
        /// </summary>
        /// <returns>If the dropdown exists</returns>
        public bool IsJurisdictionButtonDisplayed() => DriverExtensions.IsDisplayed(JurisdictionButtonLocator);

        /// <summary>
        /// Opens RecentSearches ListBox
        /// </summary>
        /// <returns>The <see cref="RecentSearchesListBoxDialog"/>.</returns>
        public RecentSearchesListBoxDialog OpenRecentSearchesDialog()
        {
            DriverExtensions.WaitForElement(RecentSearchButtonLocator).Click();
            return new RecentSearchesListBoxDialog();
        }

        /// <summary>
        /// TO DO: Delete that method and change all the references
        /// to use the same generic method below
        /// Click on Jurisdiction Selector link.
        /// </summary>
        /// <returns>The <see cref="JurisdictionOptionsDialog"/>.</returns>
        public JurisdictionOptionsDialog OpenJurisdictionDialog()
        {
            DriverExtensions.WaitForElement(JurisdictionButtonLocator).Click();
            return new JurisdictionOptionsDialog();
        }

        /// <summary>
        /// Click on Jurisdiction Selector link.
        /// </summary>
        public T OpenJurisdictionDialog<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(JurisdictionButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// IsFreeZone
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsFreeZone() => DriverExtensions.WaitForElement(BodyElementLocator).GetAttribute("class").Contains("co_free_zone");

        /// <summary>
        /// Verify Research Recommendation popup is displayed
        /// </summary>       
        /// <returns>true if pop up is displayed, false otherwise</returns>
        public bool IsRrMessageDisplayed() => DriverExtensions.IsDisplayed(RrMessageLocator, 5);

        /// <summary>
        /// The get search placeholder text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSearchPlaceholderText()
            => DriverExtensions.WaitForElement(SearchInputLocator).GetText();

        /// <summary>
        /// The get selected category text.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetSelectedCategoryText() => DriverExtensions.GetText(SelectedCategoryDropdownLocator);

        /// <summary>
        /// Get Folder Link Element to use in DragAndDropToFolder method
        /// </summary>
        /// <returns>The <see cref="IWebElement"/>.</returns>
        public IWebElement GetFoldersLinkElement()
            => DriverExtensions.WaitForElement(By.XPath(this.GlobalNavigationLinkMap[GlobalNavigationLink.Folders].LocatorString));

        private T ClickGlobalNavigationLink<T>(GlobalNavigationLink link) where T : ICreatablePageObject
        {
            this.ClickGlobalNavigationLink(link);
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Boolean Terms  And Connectors
        /// </summary>
        /// <typeparam name="T">
        /// Page Object
        /// </typeparam>
        /// <returns>
        /// A new instance of type T
        /// </returns>
        public T BooleanTermsConnectors<T>() where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(BooleanTermsAndConnectors).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// GetClientIdText
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public bool IsClientIdLabelDisplayed() => DriverExtensions.IsDisplayed(ClientIDLocator, 5);

        /// <summary>
        /// WestClipAlert icon is displayed.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsWestClipAlertIconDisplayed()
        {
            DriverExtensions.WaitForElement(WestClipAlertIconLocator).IsDisplayed();
            DriverExtensions.WaitForElement(WestClipAlertIconLocator).Click();
            return IsTextPresented("Create WestClip Alert");
        }
    }
}