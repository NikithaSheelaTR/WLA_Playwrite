namespace Framework.Common.UI.Products.WestLawNext.Pages.SearchResult
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Enums;
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Components;
    using Framework.Common.UI.Interfaces.Components.ResultLists;
    using Framework.Common.UI.Products.Shared.Components.Facets.LeftFacets.NarrowFacet;
    using Framework.Common.UI.Products.Shared.Components.ResultList;
    using Framework.Common.UI.Products.Shared.Enums.Toolbars;
    using Framework.Common.UI.Products.Shared.Items.ResultList;
    using Framework.Common.UI.Products.WestLawNext.Components.Facets.SearchResultsPageFacets;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchQuestionAnswerComponent;
    using Framework.Common.UI.Products.WestLawNext.Dialogs.Header;
    using Framework.Common.UI.Products.WestLawNext.Pages.SearchResult.ContentTypeSearchResultPages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <inheritdoc />
    /// <summary>
    /// The base category search result page.
    /// </summary>
    /// <typeparam name="TItem">
    /// the type of result list item
    /// </typeparam>
    public abstract class BaseCategorySearchResultPage<TItem> : BaseFindSearchResultPage where TItem : ResultListItem
    {
        // ReSharper disable StaticMemberInGenericType
        private static readonly By BackToCatPageLinkLocator = By.Id("coid_website_backtoCategoryPageLink");

        private static readonly By SkipViewLinkLocator = By.Id("co_search_skipview_link_id");

        private static readonly By SkipViewListLinkLocator = By.Id("co_search_skipview_listItem_ALL");

        /// <summary>
        /// The Narrow Pane (left side of search results page)
        /// ToDo - Should be replaced by NarrowPaneBase after refactoring
        /// </summary>
        public NarrowPaneComponent NarrowPane { get; } = new NarrowPaneComponent();

        /// <summary>
        /// View Results Facet
        /// </summary>
        public ViewFacetComponent ViewResultsFacet { get; } = new ViewFacetComponent();

        /// <summary>
        /// Question and Answer Component
        /// </summary>
        public SearchQuestionAnswerComponent QnAComponent { get; } = new SearchQuestionAnswerComponent();

        /// <summary>
        /// Gets the result list.
        /// </summary>
        public ISearchResultList<TItem> ResultList => new SearchResultList<TItem>(DriverExtensions.WaitForElement(this.SearchResultsListLocator));

        /// <summary>
        /// Gets the search results list locator.
        /// </summary>
        protected virtual By SearchResultsListLocator => By.XPath("//div[@id='coid_website_searchResults']");

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
        /// Checks if the back to browse page link exists
        /// </summary>
        /// <returns>if the link exists</returns>
        public bool IsBackToBrowsePageLinkDisplayed() => DriverExtensions.IsDisplayed(BackToCatPageLinkLocator, 30);

        /// <summary>
        /// Sets optional count of checkboxes of optional type (OOP or not OOP) 
        /// </summary>
        /// <param name="count"> Checkboxes count to set  </param>
        /// <param name="outOfPlane"> If true sets only out of plan otherwise excludes out of plane checkboxes </param>
        public void SetOptionalCountOfCheckboxes(int count, bool outOfPlane = false)
        {
            int checkBoxesCount = 0;
            while (checkBoxesCount != count)
            {
                List<TItem> itemsList =
                    this.ResultList.Where(item => outOfPlane ? item.IsOutOfPlan : !item.IsOutOfPlan)
                        .TakeWhile((ch, index) => index < count - checkBoxesCount)
                        .ToList();
                itemsList.ForEach(item => item.SetCheckBox());
                checkBoxesCount += itemsList.Count;
                if (checkBoxesCount >= count)
                {
                    break;
                }

                this.FooterToolbar.PaginationComponent.ClickPageButton<ContentTypeSearchResultsPage>(FooterPaginationOption.NextPage);
            }
        }

        /// todo should be implemented as dropdown
        /// <summary>
        /// Resets the default content type landing page for a user back to Overview
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <returns>The type of page to return.</returns>
        public T ResetDefaultGlobalSearchLandingPage<T>() where T : ICreatablePageObject
        {
            DriverExtensions.Click(SkipViewLinkLocator);
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(SkipViewListLinkLocator).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Copy or Move an item to a folder in the RecentFoldersDialog using Drag and Drop
        /// </summary>
        /// <param name="targetFolder">The name of Target Folder.</param>
        /// <param name="resultListItemNumber">The element to drag.</param>
        /// <param name="copyOrMoveEnum">The drag And Drop Enum.</param>
        public void DragAndDropResultListItemToRecentFolder(
            string targetFolder,
            int resultListItemNumber,
            CopyOrMoveEnum copyOrMoveEnum = CopyOrMoveEnum.Copy)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                ((IDraggableWebElement)this.ResultList[resultListItemNumber]).GetDraggableElement());

            this.DragAndDropToFolder(
                new RecentFoldersDialog().GetFolderElement(targetFolder),
                ((IDraggableWebElement)this.ResultList[resultListItemNumber]).GetDraggableElement(),
                copyOrMoveEnum);

        }

        /// <summary>
        /// Drag and hold item over folder in recent folders dialog
        /// </summary>
        /// <param name="targetFolder">The folder Name.</param>
        /// <param name="resultListItemNumber">The element to drag.</param>
        public void DragAndHoldOverRecentFolder(string targetFolder, int resultListItemNumber)
        {
            DriverExtensions.DragAndHold(
                this.Header.GetFoldersLinkElement(),
                ((IDraggableWebElement)this.ResultList[resultListItemNumber]).GetDraggableElement());

            this.DragAndHoldOverElement(
                new RecentFoldersDialog().GetFolderElement(targetFolder),
                ((IDraggableWebElement)this.ResultList[resultListItemNumber]).GetDraggableElement());
        }
    }
}