namespace Framework.Common.UI.Raw.WestlawEdge.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Products.Shared.Components.BreadCrumb;
    using Framework.Common.UI.Products.Shared.Components.Facets.RightFacets;
    using Framework.Common.UI.Products.Shared.Components.Toolbar.FooterToolbar;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane;
    using Framework.Common.UI.Products.WestlawEdge.Components.ResultList;
    using Framework.Common.UI.Products.WestlawEdge.Components.SearchQuestionAnswerComponent;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Products.WestLawNext.Components;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.ProceduralPosture;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Custom;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using OpenQA.Selenium;
    using Framework.Common.UI.Products.WestlawEdge.Dialogs.Header;
    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.WrapperEements.InfoBox;
    using Framework.Common.UI.Products.WestlawEdge.Components.NarrowPane.NarrowPanel;
    using Framework.Common.UI.Products.WestlawEdge.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.Shared.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Links;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Elements.Labels;

    /// <summary>
    /// Indigo CommonSearchResultPage
    /// </summary>
    public class EdgeCommonSearchResultPage : EdgeCommonAuthenticatedWestlawNextPage
    {
        private static readonly By ResultListContainerLocator = By.Id("coid_website_searchResults");
        private static readonly By PageHeadingLocator = By.XPath("//*[@class='co_search_result_heading_content']/h1");
        private static readonly By BackToCatPageLinkLocator = By.Id("coid_website_backtoCategoryPageLink");
        private static readonly By SearchFindErrorLocator = By.Id("co_infoBox_searchFindErrors");
        private static readonly By SearchHeaderInfoLocator = By.Id("co_searchHeaderInfo");
        private static readonly By SearchErrorLocator = By.Id("co_infoBox_searchErrors");
        private static readonly By SnippetNavigationTooltipLocator = By.Id("snippetNavigationOnboarding_infoBox_Id");
        private static readonly By SnippetNavigationTooltiTextLocator = By.XPath(".//p");
        private static readonly By SnippetNavigationTooltiOkayButtonLocator = By.Id("SnippetNavigationOnboarding-primaryButton");
        private static readonly By SnippetNavigationTooltiDoNotShowAgainCheckboxeLocator = By.XPath(".//label[@for='snippetNavigationOnboardingCheckbox']");
        private static readonly By SnippetNavigationTooltiTitleLocator = By.XPath(".//h3");
        private static readonly By CaseVersionLocator = By.XPath("//a[@class='CaseVersion-button']");
        private static readonly By AllLinksLocator = By.XPath("//div[@id='co_search_results_inner']//h3//a");
        private static readonly By ViewAllLinkLocator = By.XPath(".//*[@class='co_search_header']/a");
        private static readonly By RelevancyExplainerButtonLocator = By.XPath("//*[text()='Why is this relevant?']");
        private static readonly By RelevancyHeaderLocator = By.XPath("//div[@data-skill='WestSearchRelevancy']//h3");
        private static readonly By RelevancyExplainerSummaryLocator = By.XPath("//saf-card[contains(@class,'ResultContent-module__westSearchRelevancyContentCard')]");

        /// <summary>
        /// "Learn About" section on the search results page
        /// </summary>
        public LearnAboutFacetComponent LearnAbout { get; set; } = new LearnAboutFacetComponent();

        /// <summary>
        /// Narrow Tab Panel (left side of search results page) for Edge
        /// </summary>
        public NarrowTabPanel NarrowTabPanel { get; } = new NarrowTabPanel();

        /// <summary>
        ///IT SHOULDN'T BE USED FOR EDGE 
        /// New Narrow Pane Component (left side of search results page) 
        /// </summary>
        public EdgeNarrowPaneComponent NarrowPane { get; } = new EdgeNarrowPaneComponent();

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// ToDo - Should be replaced by NarrowPaneBase after refactoring
        /// </summary>
        public NarrowPaneComponent NarrowPaneComponent { get; } = new NarrowPaneComponent();

        /// <summary>
        /// Toolbar component
        /// </summary>
        public EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary>
        /// YourSearchResultComponent
        /// </summary>
        public YourSearchComponent YourSearchComponent { get; set; } = new YourSearchComponent();

        /// <summary>
        /// The result list section of the page
        /// </summary>
        public EdgeLegacyResultListComponent ResultList => new EdgeLegacyResultListComponent(DriverExtensions.GetElement(ResultListContainerLocator));

        /// <summary>
        /// The results list footer, with options for next page, previous page, etc.
        /// </summary>
        public FooterToolbarComponent FooterToolbar { get; } = new FooterToolbarComponent();

        /// <summary>
        /// Snapshots Facet
        /// </summary>
        public SnapshotsFacetComponent Snapshots { get; } = new SnapshotsFacetComponent();

        /// <summary>
        /// Question and Answer Component
        /// </summary>
        public EdgeSearchQuestionAnswerComponent QnAComponent { get; set; } = new EdgeSearchQuestionAnswerComponent();

        /// <summary>
        /// Gets the breadcrumb
        /// </summary>
        public BreadCrumbComponent BreadCrumb { get; } = new BreadCrumbComponent();

        /// <summary>
        /// Snippet Navigation Tooltip
        /// </summary>
        public IInfoboxWithCheckbox SnippetNavigationWithCheckbox => new InfoBoxWithCheckbox(SnippetNavigationTooltipLocator, SnippetNavigationTooltiOkayButtonLocator, SnippetNavigationTooltiDoNotShowAgainCheckboxeLocator, SnippetNavigationTooltiTextLocator, SnippetNavigationTooltiTitleLocator);

        /// <summary>
        /// Are names of category page in view pane match names in heading
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool AreNameOfCategoryPagesInViewPaneMatchNamesInHeading()
        {
            List<string> categoriesList = this.NarrowPane.ContentType.GetDocumentTypeFacetLinkList();

            var categoriesListWithCounts = new List<string>();

            foreach (string cat in categoriesList)
            {
                int result = this.NarrowPane.ContentType.GetContentTypeCount(cat);
                categoriesListWithCounts.Add(cat + " " + "(" + $"{result:n0}" + ")");
            }

            var pageHeadingWithCount = new List<string>();

            foreach (string cat in categoriesList)
            {
                string result = this.NarrowPane.ContentType.ClickContentTypeLink<EdgeCommonSearchResultPage>(cat).GetPageHeading();
                if (result.Contains("Key Numbers - Points of Law Found in Cases (10)"))
                {
                    result = result.Replace("Key Numbers - Points of Law Found in Cases (10)", "Key Numbers (10)");
                }
                pageHeadingWithCount.Add(result);
            }

            return categoriesListWithCounts.SequenceEqual(pageHeadingWithCount);
        }

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="resultListItemNumber">The element to drag.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string DragAndDropResultListItemToRecentFolder(
            string targetFolder,
            int resultListItemNumber)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                this.ResultList.GetDocumentTitleElementByIndex(resultListItemNumber));

            this.DragAndDropToFolder(
                new EdgeRecentFoldersDialog().GetFolderElement(targetFolder),
                this.ResultList.GetDocumentTitleElementByIndex(resultListItemNumber),
                CopyOrMoveEnum.Move);

            return this.Header.GetInfoMessage();
        }

        /// <summary>
        /// Gets the search results page title/heading
        /// </summary>
        /// <returns>the search results page title/heading</returns>
        public string GetPageHeading()
            => DriverExtensions.WaitForElementDisplayed(PageHeadingLocator) != null ? DriverExtensions.GetText(PageHeadingLocator) : string.Empty;

        /// <summary>
        /// Bug 1308385:WlnEdge: some terms in the snippet are not highlighted if FocusHighlight FAC is granted
        /// Check index of first element in the array with empty highlighting. If it differ to -1, then some highlighting missed.
        /// </summary>
        /// <returns>True, if there is no highlighting missed</returns>
        public bool HasNoMissedHighlighting() => CustomExtensions.GetIndexOfFirstEmptySnippetHighlight().Equals("-1");

        /// <summary>
        /// Checks if the back to browse page link exists
        /// </summary>
        /// <returns>if the link exists</returns>
        public bool IsBackToBrowsePageLinkDisplayed() => DriverExtensions.IsDisplayed(BackToCatPageLinkLocator, 30);

        /// <summary>
        /// Returns a boolean indicating whether or not a Search error is displayed
        /// </summary>
        /// <returns>true if a Search error message is displayed</returns>
        public bool IsSearchErrorMessageDisplayed() => DriverExtensions.IsDisplayed(SearchErrorLocator);

        /// <summary>
        /// Returns a boolean indicating whether or not a Find error is displayed (which 
        /// is different from a Search error)
        /// </summary>
        /// <returns>true if a Find error message is displayed</returns>
        public bool IsSearchFindErrorMessageDisplayed() => DriverExtensions.IsDisplayed(SearchFindErrorLocator);

        /// <summary>
        /// IsSearchHeaderInfoDisplayed
        /// </summary>
        /// <returns>boolean</returns>
        public bool IsSearchHeaderInfoDisplayed() => DriverExtensions.IsDisplayed(SearchHeaderInfoLocator);

        /// <summary>
        /// Get GUID document by origination context
        /// </summary>
        /// <param name="originationContext"> Origination content </param>
        /// <returns> GUID document </returns>
        public string GetDocGuidByOriginationContext(string originationContext)
        {
            ResultListItem documentItem =
                this.ResultList.GetAllSearchResultItems<ResultListItem>()
                    .ToList()
                    .FirstOrDefault(
                        elem => this.GetOriginationContextParameter(elem.Link).Equals(originationContext));
            return documentItem == null ? string.Empty : documentItem.Guid;
        }

        /// <summary>
        /// Gets <see cref="ProceduralPostureOnBoardingDialog"/>
        /// </summary>
        /// <returns><see cref="ProceduralPostureOnBoardingDialog"/></returns>
        public ProceduralPostureOnBoardingDialog GetProceduralPostureOnBoardingDialog() => new ProceduralPostureOnBoardingDialog();

        /// <summary>
        /// Back to browse page link element
        /// </summary>
        public ILink BackToBrowsePageLink => new Link(BackToCatPageLinkLocator, By.TagName("a"));

        /// <summary>
        /// Clicks the back to browse page link
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>A new page</returns>
        public T ClickBackToBrowsePageLink<T>() where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(BackToCatPageLinkLocator);
            DriverExtensions.Click(BackToCatPageLinkLocator, By.TagName("a"));
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Parameter 'origination context' from the URL
        /// </summary>
        /// <param name="link"> URL </param>
        /// <returns> Origination Context </returns>
        private string GetOriginationContextParameter(string link)
        {
            int startIndex = link.IndexOf("originationContext", StringComparison.Ordinal);
            int endIndex = link.IndexOf("transitionType=SearchItem", StringComparison.Ordinal);
            string originalContext = link.Substring(startIndex, endIndex - startIndex - 1);
            originalContext = originalContext.Replace("%20", string.Empty).Replace("originationContext=", string.Empty);
            return originalContext;
        }

        /// <summary>
        /// CaseVersion Button
        /// </summary>
        public IButton CaseVersionButton = new Button(CaseVersionLocator);

        /// <summary>
        /// Retrieve all the Links in the given page
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<ILink> GetAllLinks() => new ElementsCollection<Link>(ResultListContainerLocator, AllLinksLocator);

        /// <summary>
        /// View all link
        /// </summary>
        /// <returns></returns>
        public ILink ViewAllLink => new Link(ResultListContainerLocator, ViewAllLinkLocator);

        /// <summary>
        /// List of relevancy explainer buttons
        /// </summary>
        /// <returns></returns>
        public IReadOnlyCollection<IButton> RelevancyExplainerButtons() => new ElementsCollection<Button>(RelevancyExplainerButtonLocator);

        /// <summary>
        /// Relevancy explainer header
        /// </summary>
        /// <returns></returns>
        public ILabel RelevancyHeaderLabel = new Label(RelevancyHeaderLocator);

        /// <summary>
        /// Relevancy explainer summary
        /// </summary>
        /// <returns></returns>
        public ILabel RelevancyExplainerSummary = new Label(RelevancyExplainerSummaryLocator);
    }
}
