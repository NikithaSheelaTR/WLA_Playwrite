namespace Framework.Common.UI.Products.Shared.Items.ResultList
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// Regulations Search Result Item
    /// Part of search result list
    /// todo: make this class internal when Search Manager is implemented
    /// </summary>
    public class StatutesAndRegulationsResultListItem : ResultListItem
    {
        /// <summary>
        /// The TOC items locator.
        /// </summary>
        private static readonly By SummaryTocItemsLocator =
            By.XPath(".//div[contains(@id,'co_searchResults_summary_')]/ul/li");

        /// <summary>
        /// The statutes preview content locator (used in RR pages).
        /// </summary>
        private static readonly By PreviewContentLocator = By.XPath(".//div[contains(@class, 'co_searchResults_previewContent')]");

        /// <summary>
        /// The preview toggle locator (used in RR pages).
        /// </summary>
        private static readonly By PreviewToggleLocator = By.XPath(".//a[contains(@class, 'co_searchResults_previewToggle')]");

        /// <summary>
        /// Initializes a new instance of the <see cref="StatutesAndRegulationsResultListItem"/> class. 
        ///  </summary>
        /// <param name="containerElement">
        /// container
        /// </param>
        public StatutesAndRegulationsResultListItem(IWebElement containerElement)
            : base(containerElement)
        {
        }

        /// <summary>
        /// Search result item summary text
        /// Snippet Text
        /// </summary>
        public List<string> SummaryTocItems
            =>
                DriverExtensions.GetElements(this.Container, SummaryTocItemsLocator)
                                .Select(iw => iw.Text)
                                .ToList();

        /// <summary>
        /// Expand Preview of the result item (used in RR pages).
        /// </summary>
        public void ExpandPreview()
        {
            if (DriverExtensions.IsDisplayed(this.Container, PreviewToggleLocator) && !this.IsPreviewExpanded())
            {
                DriverExtensions.WaitForElement(this.Container, PreviewToggleLocator).Click();
                DriverExtensions.WaitForJavaScript();
            }
        }

        /// <summary>
        /// Expand Preview of the result item (used in RR pages).
        /// </summary>
        /// <returns>True if section is expanded</returns>
        public bool IsPreviewExpanded() =>
            DriverExtensions.IsDisplayed(this.Container, PreviewContentLocator);
    }
}