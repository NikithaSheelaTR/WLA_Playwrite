namespace Framework.Common.UI.Products.WestLawNext.Components.Document
{
    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Interfaces.Elements;
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Products.Shared.Elements.Buttons;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// ToC of a publication that located in the left panel on a document page
    /// </summary>
    public class TocDocViewComponent : BaseModuleRegressionComponent
    {
        private const string CollapserLctMask = "//*[text()={0}]/preceding-sibling::a[@class='co_genericCollapse']";

        private const string DocLinkTocItemLctMask = "//div[@class='co_selectable co_tocItem']//a[contains(text(),{0})]";

        private const string ExpanderLctMask =
            "//*[contains(text(),'{0}')]//preceding-sibling::a[@class='co_genericExpand']";

        private const string TocItemLctMask = "//div[@class='co_selectable co_tocItem' and .//span[contains(text(), '{0}')]]//a";

        private const string PublicationCheckboxLctMask = "//*[contains(text(),\"{0}\")]/preceding-sibling::input";

        private static readonly By ViewFullTocButtonLocator = By.Id("co_viewFullTocLink");

        private static readonly By ContainerLocator = By.Id("coid_browseToc");

        private static readonly By ActiveTocNodeLocator =
            By.XPath("//a[@id][@class='co_tocItemLink co_active'][//parent::ul[@class='co_browseContent co_selectable']]");

        private static readonly By TocToggleArrowLocator = By.XPath("//*[@id='co_collapseActionLeft']");

        private static readonly By LeftPanelLocator = By.Id("co_leftColumn");

        /// <summary>
        /// View Full ToC button
        /// </summary>
        public IButton ViewFullTocButton => new Button(ViewFullTocButtonLocator);

        /// <summary>
        /// Toc Toggle Arrow Button
        /// </summary>
        public IButton TocToggleArrowButton => new Button(TocToggleArrowLocator);

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The is toc panel expanded.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTocPanelExpanded()
            => DriverExtensions.WaitForElement(By.TagName("body")).GetAttribute("class").Contains("co_hideLeftColumn");

        /// <summary>
        /// The get selected node text.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public string GetSelectedNodeText() => DriverExtensions.GetText(ActiveTocNodeLocator);

        /// <summary>
        /// The is table of contents displayed.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public override bool IsDisplayed() => DriverExtensions.IsDisplayed(this.ComponentLocator, 5);

        /// <summary>
        /// The is toc present in left panel.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTocDisplayedInLeftPanel() => DriverExtensions.IsDisplayed(DriverExtensions.GetElement(LeftPanelLocator), this.ComponentLocator);

        /// <summary>
        /// The is horizontal scroll absent in toc.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHorizontalScrollAbsentInToc()
        {
            IWebElement toc = DriverExtensions.GetElement(this.ComponentLocator);
            int scrollWidth = toc.GetElementScrollWidth();
            int width = toc.Size.Width;
            return scrollWidth <= width;
        }

        /// <summary>
        /// The is doc link active in toc.
        /// </summary>
        /// <param name="linkText">The link text.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDocLinkActiveInToc(string linkText) => this.GetSelectedNodeText().Equals(linkText);

        /// <summary>
        /// Is publication item checked
        /// </summary>
        /// <returns>True if item is checked, false otherwise.</returns>
        public bool IsPublicationItemChecked(string publicationItem)
            => DriverExtensions.IsCheckboxSelected(DriverExtensions.WaitForElement(By.XPath(string.Format(PublicationCheckboxLctMask, publicationItem))));

        /// <summary>
        /// Clicks Document link.
        /// </summary>
        /// <param name="linkText">The link Text.</param>
        /// <typeparam name="T">T page type</typeparam>
        /// <returns>T page type</returns>
        public T ClickDocumentLink<T>(string linkText) where T : ICreatablePageObject
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(DocLinkTocItemLctMask, linkText)).Click();
            return DriverExtensions.CreatePageInstance<T>();
        }

        /// <summary>
        /// The is doc link has vertical line.
        /// </summary>
        /// <param name="linkText"> The link text. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsDocLinkHasVerticalLine(string linkText)
        {
            IWebElement docLinkElement = DriverExtensions.GetElement(SafeXpath.BySafeXpath(DocLinkTocItemLctMask, linkText));
            return !docLinkElement.GetComputedStylePropertyValue("border-left-width").Equals("0px")
                   || !docLinkElement.GetComputedStylePropertyValue("border-left-width", "after").Equals("0px");
        }

        /// <summary>
        /// The click toc expander.
        /// </summary>
        /// <param name="nodeName">The node name.</param>
        public void ClickTocExpander(string nodeName)
            => DriverExtensions.WaitForElement(By.XPath(string.Format(ExpanderLctMask, nodeName))).Click();

        /// <summary>
        /// The is Toc Collapser displayed.
        /// </summary>
        /// <param name="nodeName">The node name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTocCollapserDisplayed(string nodeName) => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(CollapserLctMask, nodeName));

        /// <summary>
        /// The is toc expander displayed.
        /// </summary>
        /// <param name="nodeName">The node name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsTocExpanderDisplayed(string nodeName) => DriverExtensions.IsDisplayed(By.XPath(string.Format(ExpanderLctMask, nodeName)));

        /// <summary>
        /// The click toc collapser.
        /// </summary>
        /// <param name="nodeName">The node name.</param>
        public void ClickTocCollapser(string nodeName) =>
            DriverExtensions.JavascriptClick(By.XPath(string.Format(TocItemLctMask, nodeName)));

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
                DriverExtensions.WaitForElement(By.XPath(string.Format(PublicationCheckboxLctMask, publicationItem)))
                                .SetCheckbox(state);
    }
}