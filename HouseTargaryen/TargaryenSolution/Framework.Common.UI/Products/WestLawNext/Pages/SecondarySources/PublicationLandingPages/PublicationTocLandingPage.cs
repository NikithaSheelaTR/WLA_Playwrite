namespace Framework.Common.UI.Products.WestLawNext.Pages.SecondarySources.PublicationLandingPages
{
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Components.BrowsePage;
    using Framework.Common.UI.Products.WestLawNext.Components.SearchResults;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The publication toc landing page.
    /// </summary>
    public class PublicationTocLandingPage : PublicationLandingPage
    {
        private const string CollapseButtonLctMask = "//div[@class='co_selectable co_tocItem' and .//*[contains(text(), '{0}')]]/a[@class='co_genericCollapse']";

        private const string ExpanderButtonLctMask = "//div[@class='co_treeItemContent' and .//*[contains(text(), '{0}')]]/preceding-sibling::span";

        private const string TocItemXPathLctMask =
            "(//div[@class='co_selectable co_tocItem']/a[text()=\"{0}\"])|(//div[@class='co_selectable co_tocItem']/span[text()=\"{0}\"])";

        private const string TocItemLctMask = "//div[contains(@class,'co_tocItem') and .//a[@class='co_tocItemLink' and contains(., '{0}')]]";

        private const string PublicationItemLctMask = "//*[contains(text(),\"{0}\")]/preceding-sibling::input";

        private static readonly By AllElementsCollapsedLocator = By.XPath("//*[@class='co_genericCollapse']");

        private static readonly By FormerDocumentIconLocator =
            By.XPath("//*[@class='co_websiteTableOfContentsDocumentLinkImage']");

        private static readonly By PublicationLandingPageTitleLocator = By.Id("co_browsePageLabel");

        private static readonly By PublicationTocLocator = By.Id("coid_browseToc");

        private static readonly By SearchCheckboxLocator = By.XPath("//input[@name='searchCheckbox']");

        private static readonly By SearchHeadingsIconLocator = By.Id("co_docSearchWithinContainer");

        /// <summary>
        /// Gets the pub toolbar.
        /// </summary>
        public CustomToolbarComponent Toolbar { get; } = new CustomToolbarComponent();

        /// <summary>
        /// BrowsePageCheckboxComponent
        /// </summary>
        public BrowsePageCheckboxComponent CheckboxComponent { get; private set; } = new BrowsePageCheckboxComponent();

        /// <summary>
        /// Verifies are all nodes collapsed
        /// </summary>
        /// <returns>
        /// True if expander collapsed
        /// </returns>
        public bool AreAllExpandersCollapsed() =>
            DriverExtensions.GetElements(AllElementsCollapsedLocator).Count == 0;

        /// <summary>
        /// Collapses toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public void CollapseNodeByName(string docTitle)
        {
            DriverExtensions.Click(By.XPath(string.Format(CollapseButtonLctMask, docTitle)));
        }

        /// <summary>
        /// Expands toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public void ExpandNodeByName(string docTitle)
        {
            IWebElement node = DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(ExpanderButtonLctMask, docTitle)));
            DriverExtensions.Click(node);
        }

        /// <summary>
        /// Is expander for documentName displayed.
        /// </summary>
        /// <param name="documentName">Document name</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsExpanderDisplayed(string documentName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(ExpanderButtonLctMask, documentName)));

        /// <summary>
        /// Is former document icon displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsFormerDocumentIconDisplayed() => DriverExtensions.IsDisplayed(FormerDocumentIconLocator);

        /// <summary>
        /// Verifies is node collapsed
        /// </summary>
        /// <param name="nodeName">the name of the node</param>
        /// <param name="childNodeName">The name of the child Node</param>
        /// <returns>
        /// True if node is collapsed
        /// </returns>
        public bool IsNodeCollapsed(string nodeName, string childNodeName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(ExpanderButtonLctMask, nodeName)), 5)
            && !DriverExtensions.IsDisplayed(By.XPath(string.Format(TocItemXPathLctMask, childNodeName)), 5);

        /// <summary>
        /// The verify toc appears.
        /// </summary>
        /// <param name="documentName">
        /// The title of the toc item
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsNodeDisplayed(string documentName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(TocItemLctMask, documentName)), 5);

        /// <summary>
        /// Verifies is node expanded
        /// </summary>
        /// <param name="nodeName">the name of the node</param>
        /// <param name="childNodeName">The name of the child Node</param>
        /// <returns>
        /// True if node is expanded
        /// </returns>
        public bool IsNodeExpanded(string nodeName, string childNodeName) =>
            DriverExtensions.IsDisplayed(By.XPath(string.Format(CollapseButtonLctMask, nodeName)), 5)
            && DriverExtensions.IsDisplayed(By.XPath(string.Format(TocItemXPathLctMask, childNodeName)), 5);

        /// <summary>
        /// The is search headings icon present.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsSearchHeadingsIconPresent() => DriverExtensions.IsElementPresent(SearchHeadingsIconLocator);

        /// <summary>
        /// The verify toc appears.
        /// </summary>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool IsPublicationTocDisplayed() => DriverExtensions.IsDisplayed(PublicationTocLocator);

        /// <summary>
        /// Verifies is page displayed
        /// </summary>
        /// <returns>
        /// True if page is displayed
        /// </returns>
        public bool IsTocPageDisplayed() => DriverExtensions.IsDisplayed(PublicationLandingPageTitleLocator, 5);

        /// <summary>
        /// The select all displayed checkboxes.
        /// </summary>
        public void SelectAllDisplayedCheckboxes() =>
            DriverExtensions.GetElements(SearchCheckboxLocator).Where(e => e.Displayed).ToList().ForEach(e => e.Click());

        /// <summary>
        /// The select publication item checkbox.
        /// </summary>
        /// <param name="publicationItem">
        /// The publication item.
        /// </param>
        /// <param name="state">
        /// The state of the checkbox
        /// </param>
        public void SelectPublicationItem(string publicationItem, bool state = true)
            =>
                DriverExtensions.WaitForElement(By.XPath(string.Format(PublicationItemLctMask, publicationItem)))
                                .SetCheckbox(state);

        /// <summary>
        /// selects All the publication items checkbox with the given Item Name.
        /// </summary>
        /// <param name="publicationItem">
        /// The publication item.
        /// </param>
        /// <param name="state">
        /// The state of the checkbox
        /// </param>
        public void SelectMultiplePublicationItems(string publicationItem, bool state = true)
            =>
                DriverExtensions.GetElements(By.XPath(string.Format(PublicationItemLctMask, publicationItem))).ToList().ForEach(item => item.SetCheckbox(state));

    }
}