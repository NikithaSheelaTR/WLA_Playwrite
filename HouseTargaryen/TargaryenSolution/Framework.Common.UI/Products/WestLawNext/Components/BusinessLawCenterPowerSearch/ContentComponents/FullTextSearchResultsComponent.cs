namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.WestLawNext.Models;
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;

    using OpenQA.Selenium;

    /// <summary>
    /// The full text search result section.
    /// </summary>
    public class FullTextSearchResultsComponent : BaseFilingResultsComponent
    {
        private const string ListItemByNumberStringMask =
            "//div[@class='listItems itemClickable ng-scope'][{0}]//li//span[2]";

        private const string All8kDocumentsFileDatesLctMask =
            "//div[@class='listItems itemClickable ng-scope'][.//span[contains(text(),'{0}')]]//li[4]/span";

        private static readonly By FormTypeOfBlockOfSnippetsLocator =
            By.XPath("//div[contains(@class,'listItems itemClickable ng-scope')]/ul/li[3]/span");

        private static readonly By SearchResultsHeaderNameLocator = By.XPath("//h2[contains(.,'Search Results')]");

        private static readonly By SearchResultsItemsLocator =
            By.XPath("//div[@class='listItems itemClickable ng-scope']//input");

        private static readonly By SearchTermLocator = By.XPath("span[@class='co_searchTerm']");

        private static readonly By SnippetOfSearchResultsLocator =
            By.XPath("//div[@class='searchResults sevenColSnippet']//div[contains(@class, 'snippet')]");

        private static readonly By ContainerLocator = By.ClassName("searchResults");

        /// <summary>
        /// Initializes a new instance of the <see cref="FullTextSearchResultsComponent"/> class.
        /// </summary>
        public FullTextSearchResultsComponent()
        {
            DriverExtensions.WaitForElementDisplayed(SearchResultsHeaderNameLocator);
            DriverExtensions.WaitForElementDisplayed(SearchResultsItemsLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// The click on list item by number.
        /// </summary>
        /// <returns>The <see cref="DocumentDetailsPage"/>.</returns>
        public DocumentDetailsPage ClickOnListItemByNumber(int number = 0)
        {
            DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(ListItemByNumberStringMask, number + 1)).Click();
            return new DocumentDetailsPage();
        }

        /// <summary>
        /// Open doc for testing search within doc functionality:
        /// 8-K at 10/20/2016
        /// </summary>
        /// <returns>New instance of DocumentDetailsPage</returns>
        public DocumentDetailsPage OpenTestDocumentForSearchWithin(string formatType, string date)
        {
            By documentFileLocator = By.XPath(string.Format(All8kDocumentsFileDatesLctMask, formatType));
            DriverExtensions.WaitForElement(documentFileLocator);
            DriverExtensions.GetElements(documentFileLocator).First(el => string.Equals(el.Text, date)).Click();
            return new DocumentDetailsPage();
        }

        /// <summary>
        /// The verify full text search term functionality.
        /// </summary>
        /// <param name="firstTermVariants">The first term variants.</param>
        /// <param name="secondTermVariants">The second term variants.</param>
        /// <returns>The <see cref="SnippetVerificationResults"/>.</returns>
        public SnippetVerificationResults VerifyFullTextSearchTermFunctionality(
            IEnumerable<string> firstTermVariants,
            IEnumerable<string> secondTermVariants)
            => this.VerifyFullTextSearchTermFunctionality(firstTermVariants, secondTermVariants, 3);

        /// <summary>
        /// The verify full text search term functionality.
        /// </summary>
        /// <param name="listOfTerms">The list of terms.</param>
        /// <param name="formType">The form type.</param>
        /// <returns>The <see cref="SnippetVerificationResults"/>.</returns>
        public SnippetVerificationResults VerifyFullTextSearchTermFunctionality(
            IEnumerable<string> listOfTerms,
            string formType)
        {
            listOfTerms = listOfTerms.ToList();
            return this.VerifyFullTextSearchTermFunctionality(listOfTerms, listOfTerms, 2, formType);
        }

        /// <summary>
        /// The verify full text search term functionality.
        /// </summary>
        /// <param name="firstTermVariants">The first term variants.</param>
        /// <param name="secondTermVariants">The second term variants.</param>
        /// <param name="numOfIterations">The number of iterations.</param>
        /// <param name="formType">The form type.</param>
        /// <returns>The <see cref="SnippetVerificationResults"/>.</returns>
        private SnippetVerificationResults VerifyFullTextSearchTermFunctionality(
            IEnumerable<string> firstTermVariants,
            IEnumerable<string> secondTermVariants,
            int numOfIterations,
            string formType = "")
        {
            SnippetVerificationResults result = SnippetVerificationResults.ContainsCorrectTerms
                                                | SnippetVerificationResults.ContainsAtLeastOneHighlightedTerm
                                                | SnippetVerificationResults.ContainsExactMatch
                                                | SnippetVerificationResults.IsOfCorrectFormType;

            string[] firstTermVariantsArray = firstTermVariants.ToArray();
            string[] secondTermVariantsArray = secondTermVariants.ToArray();
            const int PageOffset = 2;

            result &= this.VerifySnippetOnThePage(firstTermVariantsArray, secondTermVariantsArray, formType);

            for (int pageIndex = PageOffset;
                 result != SnippetVerificationResults.None && pageIndex < numOfIterations + PageOffset - 1;
                 pageIndex++)
            {
                this.SelectPage(pageIndex);
                result &= this.VerifySnippetOnThePage(firstTermVariantsArray, secondTermVariantsArray, formType);
            }

            return result;
        }

        private SnippetVerificationResults VerifySnippetOnThePage(
            IList<string> firstTermVariants,
            IList<string> secondTermVariants,
            string formType = "")
        {
            SnippetVerificationResults result = SnippetVerificationResults.ContainsCorrectTerms
                                                | SnippetVerificationResults.ContainsAtLeastOneHighlightedTerm
                                                | SnippetVerificationResults.ContainsExactMatch
                                                | SnippetVerificationResults.IsOfCorrectFormType;
            bool areTermVariantsEquals = object.ReferenceEquals(firstTermVariants, secondTermVariants);
            bool isFormTypeVerificationRequired = !string.IsNullOrEmpty(formType);

            DriverExtensions.WaitForElement(SnippetOfSearchResultsLocator);

            IList<IWebElement> currentPageSnippets = DriverExtensions.GetElements(SnippetOfSearchResultsLocator).ToList();

            DriverExtensions.WaitForElement(FormTypeOfBlockOfSnippetsLocator);

            foreach (IWebElement snippet in currentPageSnippets)
            {
                snippet.WaitForElementDisplayed();

                IList<IWebElement> iweListOfSearchTermsInSnippet = DriverExtensions.GetElements(snippet, SearchTermLocator).ToList();

                if (!iweListOfSearchTermsInSnippet.Any())
                {
                    return SnippetVerificationResults.None;
                }

                if (areTermVariantsEquals)
                {
                    if (
                        !iweListOfSearchTermsInSnippet.Any(
                            el => firstTermVariants.Contains(el.Text, StringComparer.InvariantCultureIgnoreCase)))
                    {
                        result &= ~SnippetVerificationResults.ContainsCorrectTerms
                                  | ~SnippetVerificationResults.ContainsExactMatch;
                    }
                }
                else
                {
                    if (
                        !iweListOfSearchTermsInSnippet.Any(
                            el =>
                                firstTermVariants.Contains(el.Text, StringComparer.InvariantCultureIgnoreCase)
                                || secondTermVariants.Contains(el.Text, StringComparer.InvariantCultureIgnoreCase)))
                    {
                        result &= ~SnippetVerificationResults.ContainsCorrectTerms;
                    }

                    IEnumerable<string> snippetTextValues =
                        iweListOfSearchTermsInSnippet.Select(el => el.Text).ToArray();
                    if (
                        !firstTermVariants.Contains(
                            snippetTextValues.First(),
                            StringComparer.InvariantCultureIgnoreCase)
                        && secondTermVariants.Contains(
                            snippetTextValues.Skip(1).First(),
                            StringComparer.InvariantCultureIgnoreCase))
                    {
                        result &= ~SnippetVerificationResults.ContainsExactMatch;
                    }
                }

                if (isFormTypeVerificationRequired
                    && !DriverExtensions.GetElements(FormTypeOfBlockOfSnippetsLocator).ToList().Any(el => el.Text.Contains(formType)))
                {
                    result &= ~SnippetVerificationResults.IsOfCorrectFormType;
                }
            }

            return result;
        }
    }
}