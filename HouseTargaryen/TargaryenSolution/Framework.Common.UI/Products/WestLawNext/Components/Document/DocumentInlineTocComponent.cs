namespace Framework.Common.UI.Products.WestLawNext.Components.Document
{
    using Framework.Common.UI.Products.Shared.Components;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// ToC of a document which placed after document header for document content navigation
    /// </summary>
    public class DocumentInlineTocComponent : BaseModuleRegressionComponent
    {
        private const string InternalLinkLctMask = "//a[@class='co_internalLink' and text()={0}]";

        private const string LinkInArticleOutlineLctMask = "//div[@class='co_analysis']//a[text()={0}]";

        private const string LinkInTocHeaderLctMask = "//div[@class='co_list']//a[text()={0}]";

        private static readonly By CoaActionGuideDocumentPartTitleLocator = By.XPath("//strong[text()='COA2d ACTION GUIDE']");

        private static readonly By ContainerLocator = By.CssSelector("#co_document_0 .x_article");

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The click document internal link.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        public void ClickDocumentInternalLink(string linkName) => this.ClickLink(InternalLinkLctMask, linkName);

        /// <summary>
        /// The click link in article outline.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        public void ClickLinkInArticleOutline(string linkName) => this.ClickLink(LinkInArticleOutlineLctMask, linkName);

        /// <summary>
        /// The click link in toc header.
        /// 500 is application 200 and document 300  headers height
        /// </summary>
        /// <param name="linkName">The link name.</param>
        public void ClickLinkInTocHeader(string linkName)
        {
            By linkInTocHeaderXPath = SafeXpath.BySafeXpath(LinkInTocHeaderLctMask, linkName);
            DriverExtensions.ScrollIntoView(linkInTocHeaderXPath, 500);
            DriverExtensions.WaitForElement(linkInTocHeaderXPath).Click();
            DriverExtensions.WaitForJavaScript();
        }

        /// <summary>
        /// The is Coa action guide document part displayed.
        /// </summary>
        /// <returns>True if the CoaActionGuideDocumentPart is displayed<see cref="bool"/>.</returns>
        public bool IsCoaActionGuideDocumentPartDisplayed() => DriverExtensions.IsDisplayed(CoaActionGuideDocumentPartTitleLocator, 5);

        /// <summary>
        /// The is document internal link displayed.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsDocumentInternalLinkDisplayed(string linkName)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(InternalLinkLctMask, linkName), 5);

        /// <summary>
        /// The is link in article outline displayed.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsLinkInArticleOutlineDisplayed(string linkName)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(LinkInArticleOutlineLctMask, linkName), 5);

        /// <summary>
        /// The is link in toc header displayed.
        /// </summary>
        /// <param name="linkName">The link name.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsLinkInTocHeaderDisplayed(string linkName)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(LinkInTocHeaderLctMask, linkName), 5);

        /// <summary>
        /// The click link 
        /// 500 is application 200 and document 300 headers height
        /// </summary>
        /// <param name="mask">The link mask.</param>
        /// <param name="linkName">The link name.</param>
        private void ClickLink(string mask, string linkName)
        {
            By link = SafeXpath.BySafeXpath(mask, linkName);
            DriverExtensions.ScrollIntoView(link, 500);
            DriverExtensions.WaitForElement(link).Click();
            DriverExtensions.WaitForJavaScript();
        }
    }
}