namespace Framework.Common.UI.Products.Shared.Dialogs.TableOfContents
{
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Interfaces;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// Contains all methods pertaining to the Terms of Condition Modal
    /// </summary>
    public class TableOfContentsDialog : BaseModuleRegressionDialog
    {
        private const string SectionLinkLctMask = "//li/ul/li[{0}]/a[2]/img";

        private static readonly By TocContainerLocator = By.XPath("//div[@id='co_dropdownInternalToc_container']");

        private static readonly By CurrentSearchTermLocator = By.ClassName("co_currentSearchTerm");

        private static readonly By DownloadTocLinkLocator = By.ClassName("co_docTocOverlay_search_downloadTocLink");

        private static readonly By SearchInputLocator = By.Id("co_docTocOverlay_searchInput");

        private static readonly By SearchResultLocator = By.Id("co_docTocOverlay_search_results");

        private static readonly By TocOptionLocator = By.XPath("//div[contains(@id,'co_document_internaltoc')]//label");

        private static readonly By ApplyButtonLocator = By.Id("co_internalToc_applyButton");

        private static readonly By KeyCiteFlagLocator = By.ClassName("co_tocKeyCiteFlag");

        private static readonly By CloseButtonLocator = By.Id("co_docTocOverlay_popupClose");

        /// <summary>
        /// Checks to see if the Search terms are highlighted or not
        /// </summary>
        /// <returns> True if search terms are highlighted </returns>
        public bool AreSearchTermsHighlighted() => DriverExtensions.IsDisplayed(CurrentSearchTermLocator);

        /// <summary>
        /// Clicks the 'Download Table of Contents' Link
        /// </summary>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> The dialog that is opened </returns>
        public T ClickDownloadTocLink<T>() where T : BaseModuleRegressionDialog
            => this.ClickElement<T>(DownloadTocLinkLocator);

        /// <summary>
        /// Checks if the section link for the WashingtonPracticeTocLinks tests has a document image
        /// </summary>
        /// <param name="counter"></param>
        /// <returns> True if section link has a document image </returns>
        public bool DoesSectionLinkHaveDocImage(int counter)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(SectionLinkLctMask, counter))
                && DriverExtensions.GetAttribute("alt", SafeXpath.BySafeXpath(SectionLinkLctMask, counter)).Contains("Section " + counter);

        /// <summary>
        /// Gets the value of the SearchInput textbox
        /// </summary>
        /// <returns> Search value </returns>
        public string GetCurrentQuery() => DriverExtensions.GetText(SearchInputLocator);

        /// <summary>
        /// Checks the checkbox by index and returns its name
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public string CheckCheckboxByIndex(int index)
        {
            IWebElement tocOption = this.GetTocOptions().ElementAt(index);
            tocOption.SetCheckbox(true);
            string tocOptionName = tocOption.GetText();
            DriverExtensions.WaitForElement(ApplyButtonLocator).Click();
            return tocOptionName;
        }

        /// <summary>
        /// Gets count of toc options
        /// </summary>
        /// <returns>The <see cref="int"/>.</returns>
        public int GetTocOptionsCount() => this.GetTocOptions().Count;

        /// <summary>
        /// Checks if the doc Image link has no text
        /// </summary>
        /// <param name="counter">The counter.</param>
        /// <returns>True if doc Image link has no text</returns>
        public bool IsDocImageLinkEmpty(int counter)
            => DriverExtensions.IsDisplayed(SafeXpath.BySafeXpath(SectionLinkLctMask, counter))
                && DriverExtensions.GetText(SafeXpath.BySafeXpath(SectionLinkLctMask, counter)).Equals(string.Empty);

        /// <summary>
        /// Determines if the 'Download Table of Contents' Link is displayed
        /// </summary>
        /// <returns> True if displayed, false otherwise </returns>
        public bool IsDownloadTocLinkDisplayed() => DriverExtensions.IsDisplayed(DownloadTocLinkLocator);

        /// <summary>
        /// Checks to see if there are no search results for the given search
        /// </summary>
        /// <returns> True if search result is not found </returns>
        public bool IsSearchResultsNotFound()
            => DriverExtensions.GetText(SearchResultLocator).Contains("No results found");

        /// <summary>
        /// Navigate to a link By link text
        /// </summary>
        /// <param name="link"> link </param>
        /// <typeparam name="T"> Page type </typeparam>
        /// <returns> T Page</returns>
        public T NavigateToDocument<T>(string link) where T : ICreatablePageObject
            => this.ClickElement<T>(By.LinkText(link));

        /// <summary>
        /// Sends keys to the search Result field
        /// </summary>
        /// <param name="value"> Text value </param>
        public void SetTextToSearchResultField(string value) => DriverExtensions.SetTextField(value, SearchInputLocator);

        /// <summary>
        /// Verify is KeyCiteFlag displayed on the dialog
        /// </summary>
        /// <returns>true if displayed, false otherwise</returns>
        public bool IsKeyCiteFlagDisplayed() => DriverExtensions.IsDisplayed(KeyCiteFlagLocator);

        /// <summary>
        /// Click close button
        /// </summary>
        public void ClickCloseButton() => DriverExtensions.Click(CloseButtonLocator);

        /// <summary>
        /// Gets the list of toc options
        /// </summary>
        /// <returns>The list of toc options.</returns>
        private IList<IWebElement> GetTocOptions()
            => DriverExtensions.GetElements(DriverExtensions.WaitForElement(TocContainerLocator), TocOptionLocator);
    }
}