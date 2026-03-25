namespace Framework.Common.UI.Raw.WestlawEdge.Pages.SecondarySources
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Components.Document;
    using Framework.Common.UI.Products.WestlawEdge.Components.Toolbar;
    using Framework.Common.UI.Raw.WestlawEdge.Pages.Document;
    using Framework.Common.UI.Utils.Browser;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// EdgePublicationDocumentPage
    /// </summary>
    public class EdgePublicationDocumentPage : EdgeDocumentWithFootnotesPage
    {
        private const string InfiniteScrollPageUrlPartMask = "/Document/{0}/InfiniteScroll";

        private static readonly By BaselineCopyrightLocator = By.CssSelector(".co_copyright");
        private static readonly By DocumentLogoLocator = By.XPath("//div[@class='co_treatisesHeaderImage']/img");
        private static readonly By DynamicScrollingHeaderTitleLocator = By.XPath("//div[@id='co_docContentHeader']//div[@class='co_headtext']");

        /// <summary> Toolbar </summary>
        public new EdgeToolbarComponent Toolbar { get; } = new EdgeToolbarComponent();

        /// <summary> Toc Doc View component </summary>
        public TocDocViewComponent TocDocViewComponent { get; } = new TocDocViewComponent();

        /// <summary>
        /// Get list of logos in the document
        /// </summary>
        /// <returns> Document logos </returns>
        public List<string> GetDocumentLogosText()
            => DriverExtensions.GetElements(DocumentLogoLocator).Select(element => element.GetAttribute("alt")).ToList();

        /// <summary>
        /// Get dynamic scrolling header title
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public string GetDynamicScrollingHeaderTitle() => DriverExtensions.GetText(DynamicScrollingHeaderTitleLocator);

        /// <summary>
        /// Is Baseline Copyright displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsBaselineCopyrightDisplayed() => DriverExtensions.IsDisplayed(BaselineCopyrightLocator, 5);

        /// <summary>
        /// Is Infinite Scroll Page opened
        /// </summary>
        /// <param name="documentGuid"> Document guid </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsInfiniteScrollPageOpened(string documentGuid) =>
            BrowserPool.CurrentBrowser.Url.Contains(string.Format(InfiniteScrollPageUrlPartMask, documentGuid));
    }
}
