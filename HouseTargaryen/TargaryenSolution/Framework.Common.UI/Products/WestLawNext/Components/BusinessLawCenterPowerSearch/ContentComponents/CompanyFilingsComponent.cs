namespace Framework.Common.UI.Products.WestLawNext.Components.BusinessLawCenterPowerSearch.ContentComponents
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Framework.Common.UI.Products.Shared.Enums.SortingTypes;
    using Framework.Common.UI.Products.WestLawNext.Pages.BusinessLawCenterPowerSearch;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Extensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.IWebElementExtensions;
    using Framework.Common.UI.Utils.QualityLibraryFacade.Library.Utils;
    using Framework.Core.Utils.Extensions;

    using OpenQA.Selenium;

    /// <summary>
    /// The company filings section.
    /// </summary>
    public class CompanyFilingsComponent : BaseFilingResultsComponent
    {
        private const string SelectSpecificItemLctMask = "//div[@class='listItems itemClickable ng-scope'][.//span[text()={0}]]//input";

        private static readonly By CompanyFilingsResultHeaderLocator = By.XPath("//h2[contains(.,'Company Filings')]");

        private static readonly By FileDataValuesLocator = By.XPath("//div[@class='searchResults sevenCol']//ul/li[3]/span");

        private static readonly By FilingHeaderLocator = By.ClassName("filingHeader");

        private static readonly By FilingHeaderCompanyInformationListLocator = By.XPath("//div[@class='filingHeader']//ul");

        private static readonly By FilingItemAsClickableRowLocator =
            By.XPath("//div[contains(@class, 'searchResults sevenCol')]//div[contains(@class, 'listItems')]/ul/li[2]");

        private static readonly By FilingItemAsLinkLocator =
            By.XPath("//div[contains(@class, 'searchResults sevenCol')]//div[contains(@class, 'listItems')]/ul/li[2]/a");

        private static readonly By FilingsNameLinkLocator =
            By.XPath("//div[@class='listItems ng-scope']//li/a[@class='ng-binding']");

        private static readonly By HeaderColumnNamesLocator = By.XPath("//div[@class='filingHeader']/ul/li");

        private static readonly By ResultListItemsLocator = By.XPath("//div[@class='listItems ng-scope']//li");

        private static readonly By ContainerLocator = By.ClassName("searchResults");

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyFilingsComponent"/> class.
        /// </summary>
        public CompanyFilingsComponent()
        {
            DriverExtensions.WaitForElement(CompanyFilingsResultHeaderLocator);
            DriverExtensions.WaitForElement(FilingHeaderCompanyInformationListLocator);
            DriverExtensions.WaitForElement(ResultListItemsLocator);
        }

        /// <summary>
        /// Component locator
        /// </summary>
        protected override By ComponentLocator => ContainerLocator;

        /// <summary>
        /// Validates if the expected Company attributes are displayed in the Company Header
        /// </summary>
        /// <param name="expectedColumnsNames">The expected Columns Names.</param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public bool AreExpectedCompanyAttributesDisplayedInHeader(string[] expectedColumnsNames)
        {
            ICollection<string> actualColumnsNames = DriverExtensions.GetElements(HeaderColumnNamesLocator)
                .Select(el => el.Text.Contains("\r") ? el.Text.Substring(0, el.Text.IndexOf("\r", StringComparison.InvariantCultureIgnoreCase)) : el.Text)
                .ToList();

            return actualColumnsNames.CollectionEquals(expectedColumnsNames);
        }

        /// <summary>
        /// The are insider filings present on several pages.
        /// </summary>
        /// <param name="numberOfPages"> The number of pages. </param>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool AreInsiderFilingsPresentOnSeveralPages(int numberOfPages)
        {
            int cntInsiderFilings = 0;
            for (int i = 2; i <= numberOfPages; i++)
            {
                this.SelectPage(i);

                if (this.AreInsiderFilingsDisplayed())
                {
                    cntInsiderFilings++;
                }
            }

            return cntInsiderFilings > 0;
        }

        /// <summary>
        /// The click on first item.
        /// </summary>
        /// <param name="row"> The row: numeration is from 0  </param>
        /// <returns> The <see cref="DocumentDetailsPage"/>. </returns>
        public DocumentDetailsPage ClickOnGridRowByIndex(int row = 0)
        {
            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForJavaScript();
            DriverExtensions.WaitForElementDisplayed(FilingItemAsClickableRowLocator);

            IList<IWebElement> filingList =
                DriverExtensions.GetElements(
                    DriverExtensions.IsDisplayed(FilingItemAsLinkLocator, 3)
                        ? FilingItemAsLinkLocator
                        : FilingItemAsClickableRowLocator).ToList();

            DriverExtensions.Click(filingList.ElementAt(row));
            return new DocumentDetailsPage();
        }

        /// <summary>
        /// Validates the sort functionality for File Date
        /// </summary>
        /// <returns> The <see cref="string"/>. </returns>
        public SortOrder GetSortingTypeForFilings()
        {
            List<DateTime> actualFileDataList =
                DriverExtensions.GetElements(FileDataValuesLocator).Skip(3).Select(el => Convert.ToDateTime(el.Text)).ToList();
            List<DateTime> sortedFileDataList = actualFileDataList.CopyCollection();

            // check sorting by descending
            sortedFileDataList.Sort((a, b) => b.CompareTo(a));
            if (sortedFileDataList.SequenceEqual(actualFileDataList))
            {
                return SortOrder.Descending;
            }

            // check sorting by ascending
            sortedFileDataList.Sort((a, b) => a.CompareTo(b));
            if (sortedFileDataList.SequenceEqual(actualFileDataList))
            {
                return SortOrder.Ascending;
            }

            return SortOrder.Unsorted;
        }

        /// <summary>
        /// Returns true if the company filings result header is displayed
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        public bool IsResultsHeaderDisplayed() => DriverExtensions.WaitForElement(FilingHeaderLocator).Displayed;

        /// <summary>
        /// The select specific item.
        /// </summary>
        /// <param name="namFileType"> The name file type. </param>
        public void SelectSpecificItem(string namFileType)
        {
            IWebElement specificItemCheckBox = DriverExtensions.WaitForElement(SafeXpath.BySafeXpath(SelectSpecificItemLctMask, namFileType));

            specificItemCheckBox.Hover();
            specificItemCheckBox.Click();
        }

        /// <summary>
        /// The are insiders filings forms present.
        /// </summary>
        /// <returns> The <see cref="bool"/>. </returns>
        private bool AreInsiderFilingsDisplayed()
        {
            string[] insiderFilingsFormTypeNumbers = { "3", "3/A", "4", "4/A", "5", "5/A", "144", "144/A" };

            DriverExtensions.WaitForPageLoad();
            DriverExtensions.WaitForElement(FilingsNameLinkLocator);

            return
                DriverExtensions.GetElements(FilingsNameLinkLocator)
                                .Any(doc => insiderFilingsFormTypeNumbers.Contains(doc.Text));
        }
    }
}