namespace Framework.Common.UI.Products.WestLawNextCanada.Pages.Browse
{
    using Framework.Common.UI.Products.WestLawNext.Components.BrowsePage;
    using Framework.Common.UI.Products.WestLawNext.Components.IpTools;
    using Framework.Common.UI.Raw.WestlawEdge.Pages;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using OpenQA.Selenium;

    /// <summary>
    /// The expanded toc page.
    /// </summary>
    public class ExpandedTocPage : EdgeCommonBrowsePage
    {
        private const string CollapseButtonLctMask = "//*[contains(text(), '{0}') and contains(@class, 'co_genericCollapse')]";

        private const string ExpanderButtonLctMask = "//*[contains(text(), '{0}') and @class='co_genericExpand']";

        private const string TocItemLctMask = "//*[contains(text(),'{0}')]/parent::div/preceding-sibling::input";

        /// <summary>
        /// BrowsePageCheckboxComponent
        /// </summary>
        public BrowsePageCheckboxComponent CheckboxComponent { get; private set; } = new BrowsePageCheckboxComponent();

        /// <summary>
        /// Select Content For Search Component
        /// </summary>
        public SpecifyContentToSearchComponent SpecifyContentToSearchComponent { get; } = new SpecifyContentToSearchComponent();

        /// <summary>
        /// Collapses toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public void CollapseNodeByName(string docTitle) =>
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(CollapseButtonLctMask, docTitle))).Click();

        /// <summary>
        /// Expands toc nodes.
        /// </summary>
        /// <param name="docTitle">The title of the document</param>
        public void ExpandNodeByName(string docTitle) =>
            DriverExtensions.WaitForElementDisplayed(By.XPath(string.Format(ExpanderButtonLctMask, docTitle))).Click();

        /// <summary>
        /// The select toc item checkbox.
        /// </summary>
        /// <param name="tocItem">
        /// The toc item.
        /// </param>
        /// <param name="state">
        /// The state of the checkbox
        /// </param>
        public void SelectTocItem(string tocItem, bool state = true) =>
            DriverExtensions.WaitForElement(By.XPath(string.Format(TocItemLctMask, tocItem))).SetCheckbox(state);
    }
}
