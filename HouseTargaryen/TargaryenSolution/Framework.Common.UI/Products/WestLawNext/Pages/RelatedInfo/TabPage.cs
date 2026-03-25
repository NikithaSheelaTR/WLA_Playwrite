namespace Framework.Common.UI.Products.WestLawNext.Pages.RelatedInfo
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Elements.Checkboxes;
    using Framework.Common.UI.Products.Shared.Elements.Labels;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Pages.Document;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.RiPageFacets;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// WestlawNext RITabPage
    /// </summary>
    public class TabPage : CommonDocumentPage
    {
        private const string LinkByGuidLctMask = ".//a[contains(@href, '{0}')]";

        private const string TocSectionLctMask = "//h3[contains(text(),'{0}')]";

        private static readonly By OrderedListLocator = By.XPath("//ol[@id='co_relatedInfo_orderedList']/li");

        private static readonly By CategoryLabelLocator = By.Id("co_categoryLabelContainer");

        private static readonly By CategoryLabelResponsiveLocator = By.Id("co_docResponsiveCategoryTitleContainer");

        private static readonly By ClearAllCheckboxesLinkLocator = By.Id("coid_clearAllCheckboxes");

        private static readonly By ContentResultContainerLocator = By.Id("coid_relatedInfo_contentResult_container");

        private static readonly By DidYouMeanSuggestionLinkLocator =
            By.XPath("//div[@id='coid_relatedinfo_suggestion']/a");

        private static readonly By DidYouMeanSuggestionMessageLocator = By.Id("coid_relatedinfo_suggestion");

        private static readonly By FacetPaneElementLocator = By.Id("co_relatedinfo_facets");

        private static readonly By ItemsSelectedMessageLocator = By.Id("coid_relatedInfo_Toolbar_ItemsSelected");

        private static readonly By ListOfSearchResultElementsLocator = By.CssSelector(".co_detailsTable tbody tr");

        private static readonly By ListOfSearchResultContentTypeLocator = By.XPath("./td[5]");

        private static readonly By ListOfSearchResultElementTitlesLocator = By.CssSelector(".co_relatedInfo_grid_documentLink");

        private static readonly By MainContentWrapperLocator = By.Id("coid_relatedInfo_contentResult_container");

        private static readonly By NegativeDirectHistoryOrderedListLocator = By.Id("co_relatedInfo_orderedList");

        private static readonly By OrderedItemIsCheckedLocator =
            By.XPath(".//input[contains(@id, 'coid_relatedInfo_resultList_checkbox_')]");

        private static readonly By SelectAllGraphicalCheckboxLocator = By.Id("co_checkbox_selectAll");

        private static readonly By SelectAllItemsLabelLocator =
            By.XPath("//label[@for='coid_relatedinfo_results_select_all']");

        private static readonly By SelectAllResultsCheckBoxLocator = By.Id("coid_relatedinfo_results_select_all");

        private static readonly By AllResultsSelectedMessageLocator = By.Id("coid_relatedInfo_allResultsSelected");

        private static readonly By UndoFiltersLinkInDidYouMeanSuggestionLocator =
            By.Id("coid_relatedInfo_content_undoFilter_link");

        private static readonly By CheckboxLocator = By.XPath(".//input[@type='checkbox']");

        /// <summary>
        ///  Gets or sets Narrow Pane
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; set; } = new NarrowPaneComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public LegacyResultList ResultList { get; set; } = new LegacyResultList();

        /// <summary>
        /// Gets or sets View Pane for citing references page
        /// </summary>
        public RiViewFacetComponent ViewPane { get; set; } = new RiViewFacetComponent();

        /// <summary>
        /// Items Selected Message
        /// </summary>
        public ILabel ItemsSelectedLabel => new Label(ItemsSelectedMessageLocator);

        /// <summary>
        /// Select all items label
        /// </summary>
        public ILabel SelectAllItemsLabel => new Label(SelectAllItemsLabelLocator);

        /// <summary>
        /// document title
        /// </summary>
        public ILabel CategoryLabel => new Label(CategoryLabelLocator);

        /// <summary>
        /// document title responsive
        /// </summary>
        public ILabel CategoryLabelResponsive => new Label(CategoryLabelResponsiveLocator);

        /// <summary>
        /// Select all checkbox
        /// </summary>
        public ICheckBox SelectAllResultsCheckBox => new CheckBox(SelectAllResultsCheckBoxLocator);

        /// <summary>
        /// ClearAllCheckboxes link
        /// </summary>
        public ILink ClearAllCheckboxesLink => new Link(ClearAllCheckboxesLinkLocator);

        /// <summary>
        /// Content Result Container
        /// </summary>
        protected IWebElement ContentResultContainer => DriverExtensions.WaitForElement(ContentResultContainerLocator);

        /// <summary>
        /// Click on 'Did You Mean Suggestion' link 
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <returns>
        /// New instance of T page
        /// </returns>
        public T ClickOnDidYouMeanSuggestionLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(DidYouMeanSuggestionLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Navigate to a DocumentPage by clicking on a link
        /// </summary>
        /// <typeparam name="T">T
        /// </typeparam>
        /// <param name="linkText">Link text
        /// </param>
        /// <returns>
        /// Document Page from the link that was clicked on
        /// </returns>
        public T ClickOnLinkByTextLink<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.PartialLinkText(linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Clicks on a specific document based on the result's rank
        /// </summary>
        /// <param name="resultRank">Document's rank
        /// </param>
        /// <returns>
        /// DocumentPage from the given result rank
        /// </returns>
        public CommonDocumentPage ClickOnSearchResultDocumentByRank(int resultRank)
        {
            DriverExtensions.WaitForElementDisplayed(ListOfSearchResultElementTitlesLocator);
            DriverExtensions.GetElements(ListOfSearchResultElementTitlesLocator).ToList()[resultRank].Click();
            return new CommonDocumentPage();
        }

        /// <summary>
        /// Click On Select All Graphical Checkbox Element
        /// </summary>
        public void ClickOnSelectAllGraphicalCheckbox()
            => DriverExtensions.WaitForElement(SelectAllGraphicalCheckboxLocator).Click();

        /// <summary>
        /// Click on undo filters link in 'Did YouMean Suggestion' message 
        /// </summary>
        public void ClickOnUndoFiltersLinkInDidYouMeanSuggestion()
            => DriverExtensions.WaitForElement(UndoFiltersLinkInDidYouMeanSuggestionLocator).Click();

        /// <summary>
        /// Get Content Result Container Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetContentResultContainerText() => DriverExtensions.GetText(ContentResultContainerLocator);

        /// <summary>
        /// Get Document Title
        /// </summary>
        /// <param name="index"> Document index. </param>
        /// <returns> Document title </returns>
        public string GetDocumentTitleByIndex(int index)
            => DriverExtensions.GetElements(ListOfSearchResultElementTitlesLocator).ElementAt(index).Text;

        /// <summary>
        /// Get Facet Pane Element Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetFacetPaneElementText() => DriverExtensions.GetText(FacetPaneElementLocator);

        /// <summary>
        /// Return a list of  objects
        /// </summary>
        /// <returns>list of references</returns>
        public List<string> GetListOfSearchResultsGridItems()
            => DriverExtensions.GetElements(ListOfSearchResultElementsLocator).Select(row => row.Text).ToList();

        /// <summary>
        /// Return a list of Column text in Content Types
        /// </summary>
        /// <returns>list of references</returns>
        public List<string> GetListOfSearchResultsGridContentTypeColumns()
            => DriverExtensions.GetElements(ListOfSearchResultElementsLocator, ListOfSearchResultContentTypeLocator).Select(row => row.Text).ToList();

        /// <summary>
        /// Get Negative Direct History Ordered List Text
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetNegativeDirectHistoryOrderedListText()
            => DriverExtensions.GetText(NegativeDirectHistoryOrderedListLocator);

        /// <summary>
        /// Verify that content was loaded
        /// </summary>
        /// <returns>Actual status loaded content</returns>
        public bool IsContentLoaded() => DriverExtensions.WaitForElement(MainContentWrapperLocator).Displayed;

        /// <summary>
        /// Is Content Result Container Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsContentResultContainerDisplayed() => DriverExtensions.IsDisplayed(ContentResultContainerLocator);

        /// <summary>
        /// Is 'Did You Mean Suggestion' link Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDidYouMeanSuggestionLinkDisplayed()
            => DriverExtensions.IsDisplayed(DidYouMeanSuggestionLinkLocator);

        /// <summary>
        /// Is 'Did YouMean Suggestion' message  displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsDidYouMeanSuggestionMessageDisplayed()
            => DriverExtensions.IsDisplayed(DidYouMeanSuggestionMessageLocator);

        /// <summary>
        /// Is Facet Pane Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsFacetPaneDisplayed() => DriverExtensions.IsDisplayed(FacetPaneElementLocator);

        /// <summary>
        /// Is Link Displayed
        /// </summary>
        /// <param name="link">
        /// The link. 
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsLinkDisplayed(string link) => DriverExtensions.IsDisplayed(By.PartialLinkText(link));

        /// <summary>
        /// Is Negative Direct History Ordered List Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNegativeDirectHistoryOrderedListDisplayed()
            => DriverExtensions.IsDisplayed(NegativeDirectHistoryOrderedListLocator);

        /// <summary>
        /// Determines if the given item in the ordered list has its checkbox checked.
        /// </summary>
        /// <returns>True if the item's checkbox is checked.</returns>
        public bool IsOrderedItemChecked()
            => DriverExtensions.GetElements(OrderedListLocator).ToList()
               .TrueForAll(u => DriverExtensions.IsCheckboxSelected(DriverExtensions.WaitForElement(u, OrderedItemIsCheckedLocator)));

        /// <summary>
        /// Is AllResultsSelectedMessageElement Displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsAllResultsSelectedMessageElementDisplayed()
            => DriverExtensions.IsDisplayed(AllResultsSelectedMessageLocator);

        /// <summary>
        /// Is undo filters link in 'Did YouMean Suggestion' message displayed
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsUndoFiltersLinkInDidYouMeanSuggestionDisplayed()
            => DriverExtensions.IsDisplayed(UndoFiltersLinkInDidYouMeanSuggestionLocator, 40);

        /// <summary>
        /// Checks that TocSection is displayed on the page
        /// </summary>
        /// <param name="sectionName">The toc Section Name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTocSectionDisplayed(string sectionName)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(TocSectionLctMask, sectionName)), 5);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="guid"></param>
        /// <returns></returns>
        public T ClickDocumentByGuid<T>(string guid) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(ListOfSearchResultElementsLocator, By.XPath(string.Format(LinkByGuidLctMask, guid))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Find all the checkboxes elements within the given element.
        /// </summary>
        /// <param name="parentElement">parentLocator element</param>
        /// <returns>List of checkboxes found in the parentLocator element</returns>
        protected List<IWebElement> GetChildCheckboxes(IWebElement parentElement)
            => DriverExtensions.GetElements(parentElement, CheckboxLocator).ToList();
    }
}