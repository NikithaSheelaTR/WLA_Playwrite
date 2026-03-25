namespace Framework.Common.UI.Products.Shared.Pages.Browse
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Products.WestlawEdge.Components.ToC;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Object representing a TOC browse page with collapsible sections
    /// </summary>
    public class CollapsibleTableOfContentsBrowsePage : CheckboxBrowsePage
    {
        private const string HeadingCollapseButtonLctMask = ".//a[contains(.,'{0}')]/ancestor::div[contains(@class,'co_selectable co_tocItem')]/a[contains(@class, 'co_genericCollapse')]";
        private const string HeadingExpandButtonLctMask = ".//a[contains(.,'{0}')]/ancestor::div[contains(@class,'co_selectable co_tocItem')]/a/span[contains(text(), 'Expand')]";
        private const string ItemTitleLctMask = "//a[@class='co_tocItemLink' and contains(text(),'{0}')]";
        private static readonly By CollapseAllLinkLocator = By.XPath("//*[text()='Collapse All']");
        private static readonly By ExpandAllLinkLocator = By.XPath("//*[text()='Expand All']");
        private static readonly By ResultsLocator = By.Id("co_browsePageLabel");
        private static readonly By ChapterViewLinkLocator = By.XPath("./following-sibling::a[contains(@id,'_chapterView')]");
        private static readonly By TocSearchButtonLocator = By.Id("co_searchTocRevealButton");
        private static readonly By HighlightedItemLocator = By.XPath("//*[contains(@class,'co_searchTocFound')]");

        /// <summary>
        /// ToC Search button locator
        /// </summary>
        public IButton TocSearchButton => new Button(TocSearchButtonLocator);

        /// <summary>
        /// ToC Search Component
        /// </summary>
        public TocSearchComponent TocSearchComponent { get; } = new TocSearchComponent();

        /// <summary>
        /// Click on the Collapse All link
        /// </summary>
        public void ClickCollapseAllLink() => DriverExtensions.WaitForElement(CollapseAllLinkLocator).Click();

        /// <summary>
        /// Verify that Collapse All link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsCollapseAllLinkDisplayed() => DriverExtensions.IsDisplayed(CollapseAllLinkLocator, 5);

        /// <summary>
        /// Click on the Expand All Link
        /// </summary>
        public void ClickExpandAllLink() => DriverExtensions.WaitForElement(ExpandAllLinkLocator).Click();

        /// <summary>
        /// Verify that Expand All link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsExpandAllLinkDisplayed() => DriverExtensions.IsDisplayed(ExpandAllLinkLocator, 5);

        /// <summary>
        /// Gets the number of search results that were returned
        /// </summary>
        /// <returns>the number of search results</returns>
        public int GetResultsCount()
            => DriverExtensions.GetText(ResultsLocator).RetrieveCountFromBrackets();

        /// <summary>
        /// Expands a section of the browse page
        /// </summary>
        /// <param name="section">Title of the selection to expand</param>
        /// <param name="expand">True if we should expand the section, false otherwise</param>
        public void SetSectionExpanded(string section, bool expand = true)
        {
            if (expand && !this.IsSectionExpanded(section))
            {
                DriverExtensions.JavascriptClick(By.XPath(string.Format(HeadingExpandButtonLctMask, section)));
                DriverExtensions.WaitForElement(By.XPath(string.Format(HeadingCollapseButtonLctMask, section)));
            }

            if (!expand && this.IsSectionExpanded(section))
            {
                DriverExtensions.JavascriptClick(By.XPath(string.Format(HeadingCollapseButtonLctMask, section)));
                DriverExtensions.WaitForElement(By.XPath(string.Format(HeadingExpandButtonLctMask, section)));
            }

            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// ClickChapterViewLink
        /// </summary>
        /// <typeparam name="T">T page</typeparam>
        /// <param name="section">Title of the selection to expand</param>
        /// <returns>New instance of T page</returns>
        public T ClickChapterViewLink<T>(string section) where T : ICreatablePageObject
        {
            DriverExtensions.GetElement(
                SafeXpath.BySafeXpath(HeadingCollapseButtonLctMask, section),
                ChapterViewLinkLocator).CustomClick();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// GetHighlightedItemsColors
        /// </summary>
        /// <returns>List of colors</returns>
        public List<string> GetHighlightedItemsColors() =>
            DriverExtensions.GetElements(HighlightedItemLocator).Select(el => el.GetCssValue("background-color")).ToList();

        /// <summary>
        /// ClickItemTitleByName
        /// </summary>
        /// <typeparam name="T">T page</typeparam>
        /// <param name="title">Title</param>
        /// <returns>New instance of T page</returns>
        public T ClickItemTitleByName<T>(string title) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(By.XPath(string.Format(ItemTitleLctMask, title))).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// Determines if a section is expanded or not
        /// </summary>
        /// <param name="section">Section to look at</param>
        /// <returns>True if it is expanded, false otherwise</returns>
        public bool IsSectionExpanded(string section)
            => DriverExtensions.IsDisplayed(By.XPath(string.Format(HeadingCollapseButtonLctMask, section)), 5);
    }
}
