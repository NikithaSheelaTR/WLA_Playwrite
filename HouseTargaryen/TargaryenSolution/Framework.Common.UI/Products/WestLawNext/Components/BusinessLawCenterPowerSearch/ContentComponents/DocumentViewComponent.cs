namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The document view section.
    /// </summary>
    public class DocumentViewComponent : DeliveryAndSaveToProjectComponent
    {
        private static readonly By ContainerLocator = By.Id("co_document_0");

        private static readonly By HeaderColumnNamesLocator = By.XPath("//div[@id='documentFilingHeader']//li");

        private static readonly By HighlightTermInDocumentLocator = By.XPath("//span[@class='co_searchWithinTerm']");

        private static readonly By SearchTermsInDocumentLocator = By.XPath("//span[@class='co_searchTerm']");

        private static readonly By SearchTermsNavBarLocator = By.XPath("//div[@ng-show='showSearchTermNavigation()']//a");

        private static readonly By SearchWithinDocumentIconLocator =
            By.XPath("//div[@class='co_navSelect']//div[@class='co_dropDownButton']/a[1]");

        private static readonly By SearchWithinDocumentSearchButtonLocator = By.Id("searchButton");

        private static readonly By SearchWithinDocumentTextAreaLocator =
            By.XPath("//div[@class='co_dropDownMenuContent']/div/div[@id='co_formTextSelect']/textarea");

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentViewComponent"/> class.
        /// </summary>
        public DocumentViewComponent()
        {
            DriverExtensions.WaitForElementDisplayed(this.ComponentLocator);
            DriverExtensions.WaitForElement(HeaderColumnNamesLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// To verify that the search items and navigation bars are displayed
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool AreSearchTermsToolsDisplayed()
            => DriverExtensions.WaitForElementDisplayed(SearchTermsNavBarLocator).Displayed
            && DriverExtensions.WaitForElement(SearchTermsInDocumentLocator).Displayed;

        /// <summary>
        /// The do found terms of search within document match.
        /// </summary>
        /// <param name="testSearchTerm">The search term.</param>
        /// <param name="lstSearchTerm">The list of the expected collection terms.</param>
        /// <returns>
        /// A flag that denotes whether or not all highlighted search within terms match the expected collection.
        /// </returns>
        public bool DoFoundTermsOfSearchWithinDocumentMatch(string testSearchTerm, IEnumerable<string> lstSearchTerm)
        {
            this.PerformSearchWithinDocument(testSearchTerm);
            DriverExtensions.WaitForElement(HighlightTermInDocumentLocator);

            return !DriverExtensions.GetElements(HighlightTermInDocumentLocator)
                .Any(el => !lstSearchTerm.Contains(el.Text, StringComparer.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// To verify if the header display attributes are displayed correctly or not.
        /// </summary>
        /// <param name="expectedColumnsNames">The expected Columns Names.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IsHeaderDisplayed(string[] expectedColumnsNames)
        {
            DriverExtensions.WaitForElementDisplayed(HeaderColumnNamesLocator);

            ICollection<string> actualColumnsNames = DriverExtensions.GetElements(HeaderColumnNamesLocator)
                .Select(el =>
                    {
                        int indexOfLineBreak =
                            el.Text.IndexOf(
                                "\r",
                                StringComparison
                                    .InvariantCultureIgnoreCase);
                        return indexOfLineBreak < 0
                                   ? el.Text
                                   : el.Text.Substring(
                                       0,
                                       indexOfLineBreak);
                    }).ToList();

            return actualColumnsNames.CollectionEquals(expectedColumnsNames);
        }

        /// <summary>
        /// The perform search within document.
        /// </summary>
        /// <param name="testSearchTerm">The test search term.</param>
        private void PerformSearchWithinDocument(string testSearchTerm)
        {
            if (!DriverExtensions.IsDisplayed(SearchWithinDocumentTextAreaLocator, 5))
            {
                DriverExtensions.WaitForElement(SearchWithinDocumentIconLocator).Click();
            }

            IWebElement searchWithinTextBoxIwe = DriverExtensions.WaitForElement(SearchWithinDocumentTextAreaLocator);
            DriverExtensions.Click(searchWithinTextBoxIwe);
            searchWithinTextBoxIwe.SendKeysSlow(testSearchTerm);
            DriverExtensions.WaitForElement(SearchWithinDocumentSearchButtonLocator).Click();
        }
    }
}