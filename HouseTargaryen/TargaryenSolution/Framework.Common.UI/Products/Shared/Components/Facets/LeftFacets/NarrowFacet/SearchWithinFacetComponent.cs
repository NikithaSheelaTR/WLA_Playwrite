namespace Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Search within facet
    /// </summary>
    public class SearchWithinFacetComponent : BaseFacetComponent
    {
        private static readonly By ListOfPreviousSearchQueriesLocator = By.XPath("//button[@id='co_searchWithinRecentQueries']/following-sibling::ul/li");

        private static readonly By LookInTheSameParagraphCheckboxLocator = By.Id("co_searchWithinParagraphWidget_checkbox");

        private static readonly By MagnifyingGlassWithArrowLocator = By.Id("co_searchWithinRecentQueries");

        private static readonly By NarrowResultsMessageBoxLocator = By.Id("coid_relatedInfo_keywordErrorMessage");

        private static readonly By SearchWithinCloseButtonLocator = By.Id("co_searchWithinWidget_closeButton");

        private static readonly By SearchWithinDialogCancelLinkLocator = By.Id("co_searchWithinWidget_cancelButton");

        private static readonly By SearchWithinHeaderLocator = By.Id("co_searchWithinWidget_header");

        private static readonly By SearchWithinHelpBoxLocator = By.Id("co_searchWithinWidget_help");

        private static readonly By SearchWithinRecentQueryListBoxLocator = By.XPath("//button[@id='co_searchWithinRecentQueries']/following-sibling::ul[@aria-label='Last 20 Searches']");

        private static readonly By ContainerLocator = By.Id("co_searchWithinWidget");

        private static readonly By SearchWithinFacetErrorLocator = By.Id("co_searchWithinWidget_error");

        private static readonly By SearchButtonLocator = By.XPath(".//*[contains(@class, 'co_secondaryBtn') and contains(@id, 'co_searchWithinWidget') and not(contains(@class,'co_hideState'))]");

        private static readonly By RemoveSearchButtonLocator = By.Id("co_searchWithinWidget_removeSearchButton");
        
        private static readonly By SearchWithinTextAreaLocator = By.Id("co_searchWithinWidget_textArea");

        private static readonly By UndoSearchWithinLinkLocator = By.CssSelector("#coid_relatedInfo_facet_undoFilter_link, #co_searchWithinWidget_removeSearchButton, #co_searchWithinWidget_undoButton, #co_undoAllSelectionsHistoryButton");

        private static readonly By SearchWithinResultsLocator = By.Id("SearchFacetSearchWithinButton");

        private static readonly By DisabledPreviousSearchQueryLinkLocator = By.XPath(".//div[@class = 'co_disabled']");

        private static readonly By InfoIconLocator = By.XPath("//span[@class='icon25 icon_help-blueOutline']");

        private static readonly By HoverMessageLocator = By.XPath("//div[@class='a11yTooltip-content a11yTooltip--right']");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Search within dialog cancel link
        /// </summary>
        public ILink SearchWithinDialogCancelLink = new Link(SearchWithinDialogCancelLinkLocator);

        /// <summary>
        /// Remove search button
        /// </summary>
        public IButton RemoveSearchButton = new Button(RemoveSearchButtonLocator);

        /// <summary>
        /// Narrow results message box text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetNarrowResultsMessageBoxText() => DriverExtensions.GetText(NarrowResultsMessageBoxLocator);

        /// <summary>
        /// Get search within textbox element text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetTextboxText() => DriverExtensions.GetText(SearchWithinTextAreaLocator);

        /// <summary>
        /// Get title text
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string GetHeaderText() => DriverExtensions.GetElement(this.ComponentLocator, SearchWithinHeaderLocator).GetText();

        /// <summary>
        /// Apply a search within facet
        /// Look In The Same Paragraph Checkbox exists in the Search Within facet on the Related Info pages
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="query">Query to search for</param>
        /// <param name="lookInTheSameParagraphCheckboxSetTo">The search In The Same Paragraph Checkbox.</param>
        /// <returns>New instance of the page</returns>
        public T ApplySearchWithinFacet<T>(string query, bool lookInTheSameParagraphCheckboxSetTo = false) where T : ICreatablePageObject
        {
            
            DriverExtensions.WaitForElement(SearchWithinResultsLocator).Click();
            DriverExtensions.WaitForElement(DriverExtensions.GetElement(this.ComponentLocator), SearchWithinTextAreaLocator).SetTextField(query);
            IWebElement lookInTheSameParagraphCheckbox
                = DriverExtensions.SafeGetElement(DriverExtensions.GetElement(this.ComponentLocator), LookInTheSameParagraphCheckboxLocator);
            if (lookInTheSameParagraphCheckbox != null && lookInTheSameParagraphCheckbox.IsDisplayed())
            {
                lookInTheSameParagraphCheckbox.SetCheckbox(lookInTheSameParagraphCheckboxSetTo);
            }

            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on close button in search within facet
        /// </summary>
        public void ClickSearchWithinCloseButton()
        {
            DriverExtensions.WaitForElement(SearchWithinCloseButtonLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Click on previous search queries from drop-down
        /// </summary>
        /// <typeparam name="T">Page type</typeparam>
        /// <param name="query"> Previous search query text </param>
        /// <returns>New instance of the page</returns>
        public T ApplyPreviousSearchQueriesFromDropDown<T>(string query) where T : ICreatablePageObject
        {
            this.ClickOnPreviousSearchQueriesFromDropDown(query);
            DriverExtensions.WaitForElement(SearchButtonLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Click on previous search queries from drop-down
        /// </summary>
        /// <param name="query"> Previous search query text </param>
        /// <returns>New instance of the page</returns>
        public void ClickOnPreviousSearchQueriesFromDropDown(string query)  =>
            this.GetPreviouslySearchedTerms().FirstOrDefault(term => term.GetText().Contains(query)).Click();
        
        /// <summary>
        /// Expand search within drop-down (if collapsed)
        /// </summary>
        public T ExpandPreviouslySearchedDropdown<T>() where T : ICreatablePageObject
        {
            if (!DriverExtensions.IsDisplayed(SearchWithinRecentQueryListBoxLocator))
            {
                DriverExtensions.WaitForElement(MagnifyingGlassWithArrowLocator).Click();
                DriverExtensions.WaitForElementDisplayed(SearchWithinRecentQueryListBoxLocator);
            }           
            
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Expand or collapse search within drop-down (if collapsed)
        /// </summary>
        public void ExpandCollapsePreviouslySearchedDropdown()
        {
            DriverExtensions.WaitForElement(MagnifyingGlassWithArrowLocator).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// Get list of previous search queries
        /// </summary>
        /// <returns> list of previous search queries </returns>
        public List<string> GetListOfPreviousSearchQueries()
        {
            List<string> queriesList = this.GetPreviouslySearchedTerms().Select(el => el.Text).ToList();
            this.ExpandCollapsePreviouslySearchedDropdown();
            return queriesList;
        }

        /// <summary>
        /// Get list of previous search queries that are disabled 
        /// </summary>
        /// <returns> list of previous search queries </returns>
        public List<string> GetListOfDisabledPreviousSearchQueries()
        {
            List<string> queriesList = this.GetPreviouslySearchedTerms().Where(el => DriverExtensions.SafeGetElement(el, DisabledPreviousSearchQueryLinkLocator) != null).Select(el => el.Text).ToList();
            this.ExpandCollapsePreviouslySearchedDropdown();
            return queriesList;
        }

        /// <summary>
        /// Gets the error text
        /// </summary>
        /// <returns>The error text</returns>
        public string GetSearchWithinErrorText()
            => DriverExtensions.WaitForElement(SearchWithinFacetErrorLocator).GetText();

        /// <summary>
        /// Is arrow beside magnifying glass displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsMagnifyingGlassDisplayed() => DriverExtensions.IsDisplayed(MagnifyingGlassWithArrowLocator);

        /// <summary>
        /// Is narrow results message box enabled
        /// </summary>
        /// <returns> True if narrow results message box is enabled, false otherwise </returns>
        public bool IsNarrowResultsMessageBoxDisplayed() => DriverExtensions.IsDisplayed(NarrowResultsMessageBoxLocator);

        /// <summary>
        /// Is search within drop-down Expanded
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsPreviouslySearchedDropdownExpanded() => DriverExtensions.IsDisplayed(ListOfPreviousSearchQueriesLocator);

        /// <summary>
        /// Determines if there is a search within header.
        /// </summary>
        /// <returns>True if there is a header, false otherwise</returns>
        public override bool IsHeaderDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, SearchWithinHeaderLocator);

        /// <summary>
        /// Is search within textbox element is displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTextboxDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, SearchWithinTextAreaLocator);

        /// <summary>
        /// Is undo search within link displayed
        /// </summary>
        /// <returns> True if undo search within link is displayed, false otherwise </returns>
        public bool IsUndoSearchWithinLinkDisplayed() => DriverExtensions.IsDisplayed(UndoSearchWithinLinkLocator);

        /// <summary>
        /// Is info icon (?) displayed
        /// </summary>
        /// <returns>
        /// Return true if the info icon is displayed
        /// </returns>
        public bool IsInfoIconDisplayed() => DriverExtensions.IsDisplayed(ContainerLocator, InfoIconLocator);

        /// <summary>
        /// Check is hover message displayed
        /// </summary>
        /// <returns>
        /// Return true if the hover message is displayed
        /// </returns>
        public bool IsHoverMessageDisplayed()
        {
            DriverExtensions.Hover(InfoIconLocator);
            return DriverExtensions.WaitForElement(HoverMessageLocator).GetAttribute("aria-hidden").Equals("false");
        }

        /// <summary>
        /// Click on the sUndo search within link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> New instance of the page </returns>
        public T UndoSearchWithinLinkClick<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(UndoSearchWithinLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        #region present checks
        /// <summary>
        /// Determines if there is a search within hel[ box.
        /// </summary>
        /// <returns>True if there is a search within help box, false otherwise</returns>
        public bool IsSearchWithinHelpBoxPresent() => DriverExtensions.IsElementPresent(SearchWithinHelpBoxLocator);

        /// <summary>
        /// Determines if there is a search within error.
        /// </summary>
        /// <returns>True if there is an error, false otherwise</returns>
        public bool IsSearchWithinErrorPresent() => DriverExtensions.IsElementPresent(SearchWithinFacetErrorLocator);

        /// <summary>
        /// Is search within cancel button present
        /// </summary>
        /// <returns> True if search within close button is present, false otherwise </returns> 
        public bool IsSearchWithinCancelButtonPresent() => DriverExtensions.IsElementPresent(SearchWithinDialogCancelLinkLocator);

        /// <summary>
        /// Is search within close button present
        /// </summary>
        /// <returns> True if search within close button is present, false otherwise </returns> 
        public bool IsSearchWithinCloseButtonPresent() => DriverExtensions.IsElementPresent(SearchWithinCloseButtonLocator);

        /// <summary>
        /// Is search within search button present
        /// </summary>
        /// <returns> True if search within search button is present, false otherwise </returns> 
        public bool IsSearchWithinSearchButtonPresent() => DriverExtensions.IsElementPresent(SearchButtonLocator);

        /// <summary>
        /// Is search within recent query list box present
        /// </summary>
        /// <returns> True if search within recent query lisbox is present, false otherwise </returns>
        public bool IsSearchWithinRecentQueryListBoxPresent() => DriverExtensions.IsElementPresent(SearchWithinRecentQueryListBoxLocator);

        /// <summary>
        /// Gets the SearchWithinFacetContainer class attribute
        /// </summary>
        /// <returns>class attribute text</returns>
        public string GetSearchWithinFacetContainerClassAttribute() => DriverExtensions.GetAttribute("class", this.ComponentLocator);

        /// <summary>
        /// Gets the SearchWithinRecentQueryListBox style attribute
        /// </summary>
        /// <returns>style attribute text</returns>
        public string GetSearchWithinRecentQueryListBoxStyleAttribute() => DriverExtensions.GetAttribute("style", SearchWithinRecentQueryListBoxLocator);

        /// <summary>
        /// Gets the SearchWithinHelpBox class attribute
        /// </summary>
        /// <returns>class attribute text</returns>
        public string GetSearchWithinHelpBoxClassAttribute() => DriverExtensions.GetAttribute("class", SearchWithinHelpBoxLocator);

        /// <summary>
        /// Gets the Search Within error class attribute
        /// </summary>
        /// <returns>class attribute text</returns>
        public string GetSearchWithinErrorClassAttribute() => DriverExtensions.GetAttribute("class", SearchWithinFacetErrorLocator);

        /// <summary>
        /// Gets the text of search withing help box
        /// </summary>
        /// <returns>Text of Help box</returns>
        public string GetSearchWithinHelpBoxText() => DriverExtensions.GetElement(SearchWithinHelpBoxLocator).Text;
        #endregion

        /// <summary>
        /// Opens Search within dialog
        /// </summary>
        /// <returns>SearchWithinFacetComponent</returns>
        public SearchWithinFacetComponent OpenSearchWithinDialog()
        {
            DriverExtensions.GetElement(SearchWithinResultsLocator).Click();
            return this;
        }

        private IReadOnlyCollection<IWebElement> GetPreviouslySearchedTerms()
        {
            this.ExpandCollapsePreviouslySearchedDropdown();
            DriverExtensions.WaitForElement(ListOfPreviousSearchQueriesLocator);
            return DriverExtensions.GetElements(ListOfPreviousSearchQueriesLocator);
        }
    }
}